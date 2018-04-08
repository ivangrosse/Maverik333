using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverikDesktop.Views
{
    public partial class ListaCamiones : Form
    {
        private Models.RootObject colaDeCarga1 = new Models.RootObject();
        public Models.RootObject ColaDeCarga1 { get => colaDeCarga1; set => colaDeCarga1 = value; }
        private Models.ColaDeCarga colaDeCarga2 = new Models.ColaDeCarga();
        public Models.ColaDeCarga ColaDeCarga2 { get => colaDeCarga2; set => colaDeCarga2 = value; }
        private string token1;
        private string token2;
        private string token3;
        private string token4;
        private string usuario;

        public ListaCamiones(Models.RootObject value, string uid, string expiry, string clientheader, string accesstoken, string nombreUser)
        {
            token1 = uid;
            token2 = expiry;
            token3 = clientheader;
            token4 = accesstoken;
            usuario = nombreUser;
            InitializeComponent();
            ColaDeCarga1 = value;
            this.MaximumSize = new Size(706, 475);
            this.MinimumSize = new Size(706, 475);

        }

        private void Button_Click(object sender, EventArgs e)
        {
            string identificador = Regex.Match(sender.ToString(), @"\d+").Value;
            foreach (Models.ColaDeCarga cdc in colaDeCarga1.cola_de_carga)
            {
                foreach (Models.Remito r in cdc.remitos)
                {
                    {
                       
                            if (r.unidad_de_distribucion_id == Int32.Parse(identificador))
                            {
                                    colaDeCarga2 = cdc;
                                    break;
                            }
                        
                    }
                }

            }

            ColaDeCarga colaDeCarga = new ColaDeCarga(colaDeCarga2, usuario);
            colaDeCarga.Width = 1200;
            colaDeCarga.Show();
        }

        private void ListaCamiones_Load(object sender, EventArgs e)
        {
            string unidad = "";
            List<Button> buttons = new List<Button>();
            int x = 50; int y = 75; int contador = 0;
            foreach (Models.ColaDeCarga cdc in colaDeCarga1.cola_de_carga)
            {

                Button newButton = new Button();
                foreach(Models.Remito r in cdc.remitos)
                {
                    unidad = r.unidad_de_distribucion_id.ToString();
                }
                newButton.Text = "Unidad " + unidad;
                newButton.Font = new Font(newButton.Font.FontFamily, 13);
                newButton.BackColor = Color.LightGreen;
                newButton.Location = new Point(x, y);
                newButton.Name = unidad;
                newButton.Click += Button_Click;
                newButton.Size = new Size(100, 75);
                buttons.Add(newButton);
                this.Controls.Add(newButton);
                if (contador < 5)
                {
                    x = x + 100;
                }
                else
                {
                    y = y + 75;
                    x = 50;
                    contador = 0;
                }

                contador++;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            GenerarRutas generarRutas = new GenerarRutas(token1,token2,token3,token4, usuario);
            generarRutas.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
