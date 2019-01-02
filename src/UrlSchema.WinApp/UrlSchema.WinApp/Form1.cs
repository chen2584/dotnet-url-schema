using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrlSchema.WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uriSchemeName = "Demo";

            using (var key = Registry.CurrentUser.CreateSubKey(string.Format(@"SOFTWARE\Classes\{0}", uriSchemeName)))
            {
                key.SetValue("", string.Format("URL:{0} Protocol", uriSchemeName));
                key.SetValue("URL Protocol", "");

                using (var defaultIcon = key.CreateSubKey("DefaultIcon"))
                {
                    defaultIcon.SetValue("", string.Format("{0},1", Application.ExecutablePath));
                }

                using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", string.Format("\"{0}\" \"%1\"", Application.ExecutablePath));
                }
            }
            MessageBox.Show("Install Success!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            string[] args = Environment.GetCommandLineArgs();
            foreach(var text in args)
            {
                textBox1.AppendText(text);
            }
            textBox1.AppendText(string.Empty);

            if(args.Length > 1) // demo://chenzzzzz?name=555 ถ้าเข้าด้วย url จะมี args == 2
            {
                var url = new Uri(args[1]);
                textBox1.AppendText($"\n\nSchema {url.Scheme}\n");
                textBox1.AppendText($"Query {url.Query}\n");
                textBox1.AppendText($"AbsolutePath {url.AbsolutePath}\n");
                textBox1.AppendText($"Host {url.Host}");
            }
            
        }
    }
}
