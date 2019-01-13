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
                return getListGroupByUserId(ConnectedUser.Id);
            });

            // Serialiser la liste de group récupérée en objets group
            if (task.Result)
            {
                List<Group> _group = JsonConvert.DeserializeObject<List<Group>>(JsonObject);
                Group = _group;
            }
        }
    }
}
