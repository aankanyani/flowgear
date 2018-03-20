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

namespace Q05.HTTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
    private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.google.com");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
                //((WebBrowser)sender).Document.GetElementById("Text").InnerHtml = "Zoogle";
            
        }
        private  void  webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var client = new WebClient();
            var reply = client.DownloadString("http://www.google.com");

            reply = reply.Replace("Google","Zoogle");
            webBrowser1.DocumentText = reply;


        }
    }
}
