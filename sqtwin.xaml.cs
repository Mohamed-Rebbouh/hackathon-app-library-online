using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace education
{
    /// <summary>
    /// Logique d'interaction pour sqtwin.xaml
    /// </summary>
    public partial class sqtwin : Window
    {
        IFirebaseConfig con = new FirebaseConfig()
        {
            AuthSecret = "xse0qTokr0TpZ2dE8JaMJ9aFI1OCxPwnsmLVMDAO",
            BasePath = "https://itc-mnagr-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient clt;
        public sqtwin()
        {
            InitializeComponent();
            clt = new FirebaseClient(con);
        }

        private async void qst_add_Click(object sender, RoutedEventArgs e)
        {
            qst q = new qst()
            {
                theme = qst_them.Text,
                question = qst_qst.Text
            };

            var rs = await clt.SetTaskAsync("qst/"+q.theme,q);
            qst result = rs.ResultAs<qst>();
            if (result != null)
            {
                this.Close();
                MessageBox.Show("added succec");
            }
            else
            {
                MessageBox.Show("added filed");
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
