using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverikDesktop.Views
{
    public partial class Login : Form
    {
        int contadorUsuario = 0;
        int contadorContraseña = 0;

        private const string URL = "http://maverik-project.com";
        private string urlParameters = "/auth/sign_in";
        
        public Login()
        {
            InitializeComponent();
            this.MaximumSize = new Size(310, 415);
            this.MinimumSize= new Size(310,415);
        }

        private void nombreUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("email",nombreUsuario.Text);
            client.DefaultRequestHeaders.Add("password",contraseñaUsuario.Text);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", "")
            });
            //This code lists the RESTful service response//
            var response = client.PostAsync(urlParameters,content).Result;
            if (response.IsSuccessStatusCode)
            {                  
                IEnumerable<string> headerValues1 = response.Headers.GetValues("uid");
                IEnumerable<string> headerValues2 = response.Headers.GetValues("expiry");
                IEnumerable<string> headerValues3 = response.Headers.GetValues("client");
                IEnumerable<string> headerValues4 = response.Headers.GetValues("access-token");
                string uid = headerValues1.FirstOrDefault();
                string expiry = headerValues2.FirstOrDefault();
                string clientheader = headerValues3.FirstOrDefault();
                string accesstoken = headerValues4.FirstOrDefault();
                        
                GenerarRutas generarRutas = new GenerarRutas(uid,expiry,clientheader,accesstoken);
                this.Hide();
                generarRutas.Show();
            }
            else {            
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error credenciales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nombreUsuario.Clear();
                contraseñaUsuario.Clear();
                nombreUsuario.Focus();
            }
        }




        /*GenerarRutas generarRutas = new GenerarRutas();
            this.Hide();
            generarRutas.Show();
            es lo q sigue, dejalo.

        }*/

        private void contraseñaUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void nombreUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (contadorUsuario < 1)
            {
                nombreUsuario.Text = "";
            }
            contadorUsuario += 1;
            

        }

        private void contraseñaUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (contadorContraseña < 1)
            {
                contraseñaUsuario.Text = "";
            }
            contadorContraseña += 1;
            contraseñaUsuario.UseSystemPasswordChar = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}