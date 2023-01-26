using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Firebase.Storage;

namespace education
{
    /// <summary>
    /// Logique d'interaction pour addcours.xaml
    /// </summary>
    public partial class addcours : Window
    {
        string pathe;
       public string urle;
        IFirebaseConfig con = new FirebaseConfig()
        {
            AuthSecret = "xse0qTokr0TpZ2dE8JaMJ9aFI1OCxPwnsmLVMDAO",
            BasePath = "https://itc-mnagr-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient clt;
        public addcours()
        {
            InitializeComponent();
            clt = new FirebaseClient(con);
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            
            if (clt is null)
            {
                MessageBox.Show("chek your conection!!");
            }
            else
            {
                upleadfile();
                
                

               
            }

        }

        private async void upleadfile()
        {
            var se = File.Open(@"" + pathe, FileMode.Open);

            var st = new FirebaseStorage("itc-mnagr.appspot.com")
                .Child("files")
                .Child(add_speciality.Text)
                .Child(add_year.Text)
                .Child(add_modul.Text)
                .PutAsync(se);

           var url = await st;
            MessageBox.Show(await st);
            downl(await st);

          
          

        }




        private void opene_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Title = "chose item ";
            f.Filter = "Pdf Files(*.pdf)|*.pdf|txt files(*.txt)|*.txt";
            f.ShowDialog();
            pathe= f.FileName;
            add_name.Text = pathe;


        }

        private async void downl(string s1)
        {
            fil file = new fil()
            {
                name = add_namefile.Text,
                spec = add_speciality.Text,
                modul = add_modul.Text,
                year = add_year.Text,
                url = s1

            };

            var rs = await clt.SetTaskAsync(file.spec + "/" + file.year + "/" + file.name, file);
            fil result = rs.ResultAs<fil>();
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
