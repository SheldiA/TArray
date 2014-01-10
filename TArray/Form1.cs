using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TArray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TArray array = new TArray(6);
            for (int i = 5; i >= 0; --i)
                array.Insert(i);
            foreach (Object o in array)
                richTextBox1.Text += (o.ToString() + " ");
            richTextBox1.Text += "\n";
            array.Sort(new TArray.CompareOp(CompareString));
            foreach (Object o in array)
                richTextBox1.Text += (o.ToString() + " ");
        }

        private bool CompareString(Object el1, Object el2)
        {
            bool result = false;
            if (String.Compare(el1.ToString(), el2.ToString()) > 0)
                result = true;
            return result;
        }
    }
}
