using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Chat
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private ObservableCollection<User> Users = new ObservableCollection<User>();
        private static readonly HttpClient client = new HttpClient();
        string JsonObject;

        public Register()
        {
            this.InitializeComponent();
        }

        private void Register_User(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string confpassword = Confirm.Password;

            if (username != "" && password != "" && confpassword != "") {
                if (password == confpassword)
                {
                    var task = Task.Run<bool>(() =>
                    {
                        return RegisterUser(username, password);
                    });

                    if (!task.Result)
                    {
                        error.Text = "Ce nom d'utilisateur est déjà utilisé, veuillez en selectionner un nouveau.";
                    }
                    else
                    {
                        string message = "Votre compte a bien été crée";
                        Frame.Navigate(typeof(MainPage), message);
                    }
                }
                else
                {
                    error.Text = "Les mot de passe ne sont pas identiques";
                }
            }
            else {
                error.Text = "Tous les champs ne sont pas renseignés";
            }
        }

        private async Task<bool> RegisterUser(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
                { "username", username },
                 { "password", password },
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost/Chat/Register.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString == "\tfalse")
            {
                return false;
            }

            JsonObject = responseString;
            return true;
        }
    }
}
