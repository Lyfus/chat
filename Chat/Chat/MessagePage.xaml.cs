using Chat.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<User> _friend = new List<User>();
        public List<User> Friends
        {
            get { return _friend; }
            set { _friend = value; }
        }

        public MessagePage()
        {
            DataContext = this;
            this.InitializeComponent();
        }
    }
}
