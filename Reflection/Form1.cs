using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Plexware.Assessment.Reflection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Assembly assembly;
        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;

            string fullpath = path + "\\" + "SampleData.dll";
            assembly = Assembly.LoadFile(fullpath);

            var att = assembly.GetCustomAttributes(true);
            Type type = assembly.GetType("SampleData.Reflection.SampleClass");

            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod("TestMethod");

                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(type);

                    
                    if (parameters.Length == 0)
                    {
                        // This works fine.
                        result = methodInfo.Invoke(classInstance, null);
                    }
                    else
                    {
                        object[] parametersArray = new object[] { textBox1.Text };

                        // The invoke does NOT work;erwwer
                        // it throws "Object does not match target type"             
                        result = methodInfo.Invoke(classInstance, parametersArray);

                        MessageBox.Show("Called from reflection: " + result.ToString(),"REFLECTION", MessageBoxButtons.OK,MessageBoxIcon.Information);

                    }
                }
            }
            //call the method here using reflection only, then display the return value in a message box
        }

    }
}
