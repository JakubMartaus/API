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
using System.Net.Http;
using Newtonsoft.Json;
namespace API
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Get();
            string jmeno1;
            string prijmeni;
            string rc;
            string datum;
            string pohlavi;
        }
        public async void Get()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://studujelektro.jakubmartaus.cz/api.php");

            string json = await response.Content.ReadAsStringAsync();
            //dynamic c = JsonConvert.DeserializeObject(json);
            List<ApiObject> c = JsonConvert.DeserializeObject<List<ApiObject>>(json);
            
            //string text = c[0].Gender;
         
           // test.Content = text;
            int ctr = c.Count();
            for(int i = 0; i < ctr; i++)
            {
                ApiObject api = new ApiObject();
                api.ID = c[i].ID;
                api.Name = c[i].Name;
                api.Surname = c[i].Surname;
                api.PIN = c[i].PIN;
                api.Birthdate = c[i].Birthdate;
                api.Gender = c[i].Gender;

                listView.Items.Add(api);
                    
            }
            
           
        }
        public async void Send()
        {
            // Vytvoření klienta
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://studujelektro.jakubmartaus.cz/api.php");
            // Data, která se přidají k POST dotazu -> klíč je typu string a data jsou typu string
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Name", jmeno1));
            keyValues.Add(new KeyValuePair<string, string>("Surname", prijmeni1));
            keyValues.Add(new KeyValuePair<string, string>("PIN", rc1 ));
            keyValues.Add(new KeyValuePair<string, string>("Birthdate", datum1 ));
            keyValues.Add(new KeyValuePair<string, string>("Gender", pohlavi1 ));
            keyValues.Add(new KeyValuePair<string, string>("Overeni", "post"));
            // Přidání dat do dotazu
            request.Content = new FormUrlEncodedContent(keyValues);
            // Zaslání POST dotazu
            var response = await client.SendAsync(request);
            // Získání odpovědi
            string responseContent = await response.Content.ReadAsStringAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            Send();
            
            Get();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        string jmeno1;
        string prijmeni1;
        string rc1;
        string datum1;
        string pohlavi1;
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            jmeno1 = jmeno.Text;
            prijmeni1 = prijmeni.Text;
            rc1 = rodcislo.Text;
            datum1 = datumnar.Text;
            pohlavi1 = pohlavi.Text;

            Send();
            listView.Items.Clear();
            Get();
        }
 
        public async void Update()
        {
            Object nevim = listView.SelectedItem;
            ApiObject test;
            test = (ApiObject)nevim;
            string selectedID = test.ID;

            // Vytvoření klienta
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, "http://studujelektro.jakubmartaus.cz/api.php");
            // Data, která se přidají k POST dotazu -> klíč je typu string a data jsou typu string
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Name", jmeno1));
            keyValues.Add(new KeyValuePair<string, string>("Surname", prijmeni1));
            keyValues.Add(new KeyValuePair<string, string>("PIN", rc1));
            keyValues.Add(new KeyValuePair<string, string>("Birthdate", datum1));
            keyValues.Add(new KeyValuePair<string, string>("Gender", pohlavi1));
            keyValues.Add(new KeyValuePair<string, string>("ID", selectedID));
   
            // Přidání dat do dotazu
            request.Content = new FormUrlEncodedContent(keyValues);
            // Zaslání POST dotazu
            var response = await client.SendAsync(request);
            // Získání odpovědi
            string responseContent = await response.Content.ReadAsStringAsync();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Update();
            listView.Items.Clear();
            Get();



        }

    }
}
