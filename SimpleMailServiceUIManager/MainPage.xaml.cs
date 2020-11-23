using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleMailServiceUIManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageHelper mainHelper;
        private Dictionary<int, string> addressesReferences;

        public MainPage()
        {
            this.InitializeComponent();
            mainHelper = new MainPageHelper();
            InitAddressesList();
        }

        private void InitAddressesList()
        {
            addressesReferences = mainHelper.GetMailObjectReferences();
            foreach (KeyValuePair<int, string> entry in addressesReferences)
            {
                EmailAddressesList.Items.Add(entry.Value);
            }
        }

        private void switchLockOnAllListElements(int currentIndex, bool setExclusive)
        {

            foreach (var item in EmailAddressesList.Items)
            {
                ListViewItem i = (ListViewItem)EmailAddressesList.ContainerFromItem(item);
                if (setExclusive)
                {
                    if (!(currentIndex == EmailAddressesList.IndexFromContainer(i)))
                    {
                        i.IsEnabled = false;
                    }
                } else
                {
                    i.IsEnabled = true;
                }
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMail = EmailAddressesList.SelectedItem.ToString();
            switchLockOnAllListElements(EmailAddressesList.SelectedIndex, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            switchLockOnAllListElements(0, false);
        }
    }

    public class MainPageHelper
    {
        public MailObject GetMailObject(int id)
        {
            //implement login to retrieve Mail object from config file
            return new MailObject();
        }

        public void AddMailObject(MailObject obj)
        {

        }
        public void RemoveMailObject(int MailObjectID)
        {

        }

        public Dictionary<int, string> GetMailObjectReferences()
        {
            var dict = new Dictionary<int, string>();
            dict.Add(1, "bl@ppsa.ch");
            dict.Add(2, "facture@ppsa.ch");
            dict.Add(3, "nc@ppsa.ch");
            dict.Add(4, "fh@ppsa.ch");
            return dict;
        }
    }


    public class MailObject
    {
        enum PROTOCOL
        {
            IMAP, POP3
        }

        int id { get; set; }
        string address { get; set; }
        string password { get; set; }
        string server { get; set; }
        bool encryption { get; set; }
        PROTOCOL protocol { get; set; }
        int port { get; set; }
    }

    //need later : var myKey = types.FirstOrDefault(x => x.Value == "one").Key; -> gets a key from a value
}
