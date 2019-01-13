using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Chat.Classes;
using System.Net.Http;
using Newtonsoft.Json;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chat
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient client = new HttpClient();
        string JsonObject;
        string message;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string _username = username.Text.ToString();
            string _password = password.Password.ToString();

            // Gestion de la saisie user
            if (_username != "" && _password != "")
            {
                var task = Task.Run<bool>(() =>
                {
                    return Login(_username, _password);
                });

                if (!task.Result)
                {
                    error.Text = "Username ou Mot de passe incorrect";
                }
                else
                {
                    User User = JsonConvert.DeserializeObject<User>(JsonObject);
                    Frame.Navigate(typeof(MessagePage), User);
                }
            }
            else
            {
                error.Text = "Veuillez renseigner un username et un mot de passe";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }

        private async Task<bool> Login(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
                { "login", username },
                 { "password", password }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost/Chat/Login.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if(responseString == "\r\n\t")
            {
                return false;
            }

            JsonObject = responseString;
            return true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            message = e.Parameter.ToString();
            if (message != null)
            {
                affichage.Text = message;
            }
        }
    }
}
