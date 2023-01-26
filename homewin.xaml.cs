using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace education
{
    /// <summary>
    /// Logique d'interaction pour homewin.xaml
    /// </summary>
    public partial class homewin : Window
    {
        string[] tab = { "student", "halper", "prof" };
        data d = new data();

        IFirebaseConfig con = new FirebaseConfig()
        {
            AuthSecret = "xse0qTokr0TpZ2dE8JaMJ9aFI1OCxPwnsmLVMDAO",
            BasePath= "https://itc-mnagr-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        public homewin()
        {
              client = new FirebaseClient(con);
              InitializeComponent();
            star();
           
            
           
        }



        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            switch (titl_page.Text) 
            {
                case "home":
                addcours w = new addcours();
                w.ShowDialog();
                    break;
                    default: 
                    break;
                case "Coumunity":
                    sqtwin we=new sqtwin();
                    we.ShowDialog();
                    break;
                    
             }
        }

        private void gridload()
        {
            
            importdata();
            Dictionary<string, fil> list = new Dictionary<string, fil>();
            ObservableCollection<fil> fils = new ObservableCollection<fil>();

            
            if(!string.IsNullOrEmpty(d.spec) && !string.IsNullOrEmpty(d.year))
            {

               
                FirebaseResponse re = client.Get(@""+ d.spec + "/" + d.year);
                list = JsonConvert.DeserializeObject<Dictionary<string, fil>>(re.Body.ToString());
                try
                {
                    foreach (var item in list)
                    {
                        if (item.Value is not null)
                        {
                            fils.Add(new fil { name = item.Value.name, spec = item.Value.spec, modul = item.Value.modul, year = item.Value.year, url = item.Value.url });

                        }



                    }
                    gridcours.ItemsSource = fils;
                }catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
         
        }

        private async void importdata()
        {
            data de = new data();
           
           
            if (client is null)
            {
                return;

            }
            else
            {
                bool exist = false;
                int i = 0;
                while (i < 3 && !exist)
                {
                    var rs = await client.GetTaskAsync(tab[i] +"/"+NameT.Text);
                    try
                    {
                        de = rs.ResultAs<data>();
                    }
                    catch (Exception) { }
                    if (rs is null)
                        i++;

                    else if (de is not null)
                    {
                        d = de;
                        exist=true;
                     
                    }

                }


            }
           


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while(d is null)
            {
                
            }
           
            gridload();
            gridload();
           
        }
        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hyperlink link = (Hyperlink)e.OriginalSource;

                Process.Start(link.NavigateUri.AbsoluteUri);
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void max_btn_Click(object sender, RoutedEventArgs e)
        {
            bool ismax = false;
            if (!ismax)
            {
                this.WindowState= WindowState.Maximized;
                ismax= true;
            }
            else
            {
                this.WindowState= WindowState.Normal;
                this.Width = 1080;
                this.Height = 720;

                ismax= false;
            }
        }

        private void star()
        {
            switch (d.post)
            {
                case "student":
                    addbtn.Visibility = Visibility.Visible;
                    break;
                default:
                    addbtn.Visibility = Visibility.Visible;
                    break;

            }
            dockqst.Visibility = Visibility.Hidden;
            dockcours.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            titl_page.Text = "home";
            gridload();
            star();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gridqstload();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow win =new MainWindow();
            this.Close();
            win.ShowDialog();
        }

        private void gridqstload()
        {
            dockqst.Visibility = Visibility.Visible;
            dockcours.Visibility = Visibility.Hidden;
            addbtn.Visibility = Visibility.Visible;
            titl_page.Text = "Coumunity";

            ObservableCollection<qst> fils = new ObservableCollection<qst>();
            FirebaseResponse re = client.Get(@"qst");
            Dictionary<string, qst> list = JsonConvert.DeserializeObject<Dictionary<string, qst>>(re.Body.ToString());
            try
            {
                foreach (var item in list)
                {
                    if (item.Value is not null)
                    {
                        fils.Add(new qst { theme = item.Value.theme, question = item.Value.question });

                    }



                }
                gridqst.ItemsSource = fils;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
