using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Plexware.Assessment.XSD
{
    public partial class MainForm : Form
    {        
        List<Member> _Members = new List<Member>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void GenerateXmlButton_Click(object sender, EventArgs e)
        {
            _Members.Add(new Member
            {
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Email = textBox3.Text
            });

            txtDisplay.Clear();

            txtDisplay.AppendText("<members>\r\n");
            foreach(var member in _Members)
            {
                txtDisplay.AppendText("  <member>\r\n");
                txtDisplay.AppendText($"    <name>{member.Name}</name>\r\n");
                txtDisplay.AppendText($"    <surname>{member.Surname}</surname>\r\n");
                txtDisplay.AppendText($"    <email>{member.Email}</email>\r\n");
                txtDisplay.AppendText("  </member>\r\n");
            }
            txtDisplay.AppendText("</members>");



            //using (XmlWriter writer = XmlWriter.Create("Members.xml"))
            //{
            //    writer.WriteStartElement("Members");
            //    writer.WriteElementString("name", textBox1.Text);
            //    writer.WriteElementString("surname", textBox2.Text);
            //    writer.WriteElementString("email", textBox3.Text);
            //    writer.WriteEndElement();
            //    writer.Flush();
            //}

        }
    }

    public class Member
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
