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
    /// Logique d'interaction pour logwin.xaml
    /// </summary>
    public partial class logwin : Window
    {
        
        IFirebaseConfig con = new FirebaseConfig()
        {
            AuthSecret = "xse0qTokr0TpZ2dE8JaMJ9aFI1OCxPwnsmLVMDAO",
            BasePath = "https://itc-mnagr-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        public logwin()
        {
            client = new FirebaseClient(con);
            InitializeComponent();
        }

        private async void log11_Click(object sender, RoutedEventArgs e)
        {
            if (client is null)
            {
                MessageBox.Show("chek your conection!!");
            }
            else
            {
                data d = new data()
                {
                    name = log_name.Text,
                    password = log_password.Text,
                    spec = log_speciality.Text,
                    post = log_post.Text,
                    year = log_year.Text,


                };
                var rs = await client.SetTaskAsync(d.post +"/"+ d.name, d);
                data result=rs.ResultAs<data>();
                if(rs!=null)
                {
                    MessageBoxResult r = MessageBox.Show("login succec" + d.name, "info", MessageBoxButton.OK);
                    if(r!=MessageBoxResult.Yes)
                    {
                        MainWindow win=new MainWindow();
                        this.Close();
                        win.ShowDialog();
                    }
                }

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
