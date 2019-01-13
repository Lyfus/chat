using Chat.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Chat
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    
    public sealed partial class MessagePage : Page
    {
        private ObservableCollection<Messages> _messages = new ObservableCollection<Messages>();
        public ObservableCollection<Messages> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        private List<Group> _group = new List<Group>();
        public List<Group> Group
        {
            get { return _group; }
            set { _group = value; }
        }

        User ConnectedUser;
        private static readonly HttpClient client = new HttpClient();
        string JsonObject;
        string listMessages;

        public MessagePage()
        {
            DataContext = this;
            this.InitializeComponent();
        }

        private async Task<bool> getListGroupByUserId(int Id)
        {
            var values = new Dictionary<string, string>
            {
                { "idUser", Id.ToString() },
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost/Chat/getListGroupByUserId.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString == "\t")
            {
                return false;
            }

            JsonObject = responseString;
            return true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ConnectedUser = e.Parameter as User;

            // Recup Group dans TaskAsync
            var task = Task.Run<bool>(() =>
            {
                return getListGroupByUserId(ConnectedUser.IdUser);
            });

            // Serialiser la liste de group récupérée en objets group
            if (task.Result)
            {
                List<Group> _group = JsonConvert.DeserializeObject<List<Group>>(JsonObject);
                // Object User List
                ConnectedUser.Groups = _group;
                // Display List
                Group = ConnectedUser.Groups;
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            // Voir avec Byron pour distribution du message
            /*
             * à faire
             */

            // Mise en mémoire dans BDD
            var message = textbox1.Text;
            var pubdate = DateTime.Now.ToString();

            var task = Task.Run<int>(() =>
            {
                return saveMessage(message, pubdate, ConnectedUser.IdUser.ToString());
            });

            int idMessage = task.Result;
            string _pubdate = pubdate;
            Messages sendMessage = new Messages(idMessage, _pubdate, message, ConnectedUser.Pseudo);

            Messages.Add(sendMessage);
            textbox1.Text = "";
            double count = messageListView.Items.Count();
            messageScrollView.ChangeView(0, count*50, 1);
        }
        
        private async Task<int> saveMessage(string contenu, string pubdate, string idUser)
        {
            var values = new Dictionary<string, string>
            {
                { "Content", contenu },
                { "Pubdate", pubdate },
                { "IdUser", idUser }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost/Chat/insertMessage.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return Int32.Parse(responseString);
        }

        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock TappedGroup = (TextBlock)sender;
            int idGroup = Int32.Parse(TappedGroup.Tag.ToString());
            // 1 - Vider liste message
            Messages.Clear();
            // 2 - Lancer restApi pour récupérer la liste des messages inscrits dans le groupe
            var task2 = Task.Run<bool>(() =>
            {
                return getMessageByGroupId(idGroup);
            });
            bool boule = task2.Result;
            // 3 - Ajouter la liste à Messages
            ObservableCollection<Messages> _BddMessages = JsonConvert.DeserializeObject<ObservableCollection<Messages>>(listMessages);
            foreach(Messages child in _BddMessages)
            {
                Messages.Add(child);
            }
        }

        private async Task<bool> getMessageByGroupId(int Id)
        {
            var values = new Dictionary<string, string>
            {
                { "idGroup", Id.ToString() },
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost/Chat/getMessageByGroupId.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString == "\t")
            {
                return false;
            }

            listMessages = responseString;
            return true;
        }
    }
}
