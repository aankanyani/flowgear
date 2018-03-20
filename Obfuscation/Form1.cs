using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plexware.Assessment.Obfuscation
{
    public partial class Form1 : Form
    {
        public enum ErrorType
        {
            LostConnection,
            Incomplete,
            Unknown
        }

        ErrorType _lastError = ErrorType.LostConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "";

            switch (_lastError.ToString())
            {
                case "LostConnection":
                    msg = "Your connection was lost!";
                    break;
                case "Incomplete":
                    msg = "The transaction could not be completed!";
                    break;
                case "Unknown":
                    msg = "An unknown error occured!";
                    break;
                default:
                    throw new Exception("Unknown error type!");
            }

            MessageBox.Show(msg);
        }
    }
}
