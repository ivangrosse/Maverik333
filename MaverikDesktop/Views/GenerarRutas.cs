using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaverikDesktop.Views
{
    public partial class GenerarRutas : Form
    {
        private const string URL = "http://maverik-project.com";
        private string urlParameters = "/api/v1/rutas/generar_cola_de_carga";
        private string token1;
        private string token2;
        private string token3;
        private string token4;

        public GenerarRutas(string uid,string expiry, string clientheader, string accesstoken)
        {
            token1 = uid;
            token2 = expiry;
            token3 = clientheader;
            token4 = accesstoken;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("uid", token1);
            client.DefaultRequestHeaders.Add("expiry", token2);
            client.DefaultRequestHeaders.Add("client", token3);
            client.DefaultRequestHeaders.Add("access-token", token4);

            //This code lists the RESTful service response//
            var response = client.GetAsync(urlParameters).Result;
            if(response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                Models.RootObject dataObject = JsonConvert.DeserializeObject<Models.RootObject>(jsonString.Result);            
                ListaCamiones listaCamiones = new ListaCamiones(dataObject,token1,token2,token3,token4);
                listaCamiones.Show();
                this.Close();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private void GenerarRutas_Load(object sender, EventArgs e)
        {

        }

        private void logOutButton_Click(object sender, EventArgs e)
        {

            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
