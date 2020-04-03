using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace RealmBansApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encoding enc = Encoding.GetEncoding("UTF-8");
            string url = $"https://realmbans.midofree.xyz/api/?user={textBox1.Text}";
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            Stream st = res.GetResponseStream();
            StreamReader sr = new StreamReader(st, enc);
            Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
            sr.Close();
            st.Close();

            listView1.Items.Clear();
            if (result["playername"] == "No players found")
            {
                MessageBox.Show("Player not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] item1 = { "Banned", result["ban"] };
            listView1.Items.Add(new ListViewItem(item1));
            string[] item2 = { "Muted", result["mute"] };
            listView1.Items.Add(new ListViewItem(item2));
            string[] item3 = { "Temp-Banned", result["tempban"] };
            listView1.Items.Add(new ListViewItem(item3));
        }
    }
}
