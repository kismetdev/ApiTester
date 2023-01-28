namespace ApiTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            domainUpDown1.SelectedIndex = 0;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string method = domainUpDown1.Text;
            string uri = textBox1.Text;
            HttpContent content = null;
            if (method == "POST" || method == "PUT")
            {
                content = new StringContent(textBox3.Text);
            }
            try
            {
                textBox2.Text = await SendHttpRequest(method, uri, content);
            }
            catch (Exception ex)
            {
                textBox2.Text = "Error: " + ex.Message;
            }
        }

        static async Task<string> SendHttpRequest(string method, string uri, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "omg c# app");

                HttpResponseMessage response;
                if (method == "GET")
                    response = await client.GetAsync(uri);
                else if (method == "POST")
                    response = await client.PostAsync(uri, content);
                else if (method == "PUT")
                    response = await client.PutAsync(uri, content);
                else if (method == "DELETE")
                    response = await client.DeleteAsync(uri);
                else
                    throw new ArgumentException("Invalid method");

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private void domainUpDown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (domainUpDown1.Text == "GET")
            {
                label2.Visible = false;
                textBox3.Visible = false;
            }
            else if (domainUpDown1.Text == "POST" || domainUpDown1.Text == "PUT")
            {
                label2.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label2.Visible = false;
                textBox3.Visible = false;
            }
        }
    }
}
