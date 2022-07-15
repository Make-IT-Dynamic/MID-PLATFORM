using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MID_PLATFORM_CLIENT
{
    public partial class LoginPopup : Form
    {
        public LoginPopup()
        {
            InitializeComponent();
        }

        private void button_ShowPassword_Click(object sender, EventArgs e)
        {
            if (textBox_password.UseSystemPasswordChar)
                textBox_password.UseSystemPasswordChar = false;
            else
                textBox_password.UseSystemPasswordChar = true;
        }

        public static string token { get; set; } = null;
        public string refreshToken { get; set; } = null;

        public class TokenReferenceClass
        {
            public string token = null;
            public string refreshToken = null;
        }

        public static void NoLogin()
        {
            LoginPopup login = new LoginPopup();
            login.MdiParent = MainMenu.ActiveForm;
            login.Show();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:7042/api/authenticate");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"username\":\"TEST\"," +
                                  "\"password\":\"PASS\"}";


                    //string json = "{\"username\":\"" + textBox_User.Text + "\"," +
                    //              "\"password\":\"" + textBox_password.Text + "\"}";


                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    //TokenReferenceClass token = new TokenReferenceClass();
                    var deserialized = JsonConvert.DeserializeObject<TokenReferenceClass>(result);

                    LoginPopup.token = deserialized.token;
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
    }
}
