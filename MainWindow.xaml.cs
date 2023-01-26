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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace education
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        string []tab = { "student", "halper", "prof" };
        IFirebaseConfig con = new FirebaseConfig()
        {
            AuthSecret = "xse0qTokr0TpZ2dE8JaMJ9aFI1OCxPwnsmLVMDAO",
            BasePath = "https://itc-mnagr-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        public MainWindow()
        {
            client = new FirebaseClient(con);
            InitializeComponent();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            logwin win =new logwin();
            this.Close();
            win.ShowDialog();
           
        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            data d = new data();
            if (client is null)
            {
                MessageBox.Show("chek your conection!!");

            }
            else
            {
                bool exist = false;
                int i = 0;
                while (i < 3 && !exist)
                {
                    var rs = await client.GetTaskAsync(tab[i] + "/" + nome.Text);
                  
                    try
                    {
                        d = rs.ResultAs<data>();
                    }
                    catch (Exception) { restar(); }


                    if (rs is null)
                    {
                        i++;
                    }
                    else
                    {

                        exist = true;
                        try
                        {
                          

                            if (passw.Password == d.password)
                            {
                                homewin wind = new homewin();
                                wind.NameT.Text = d.name;
                                
                                this.Close();

                                wind.ShowDialog();

                            }
                        }
                        catch (Exception) { }
                    }
                }

                if (!exist)
                {
                    restar();
                }


            }


        }
        private void restar()
        {
            MessageBox.Show("chek your name or your password !!");
            passw.Password = "";
            nome.Text = "";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
