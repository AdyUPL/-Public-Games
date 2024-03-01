using System;
using System.Windows.Forms;

namespace SupermarketSimulatorSaveChanger
{
    public partial class Form2 : Form
    {
        public string newName { get; private set; }

    public Form2(string name,int number)
        {
            InitializeComponent();
            this.Text = name+" "+number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newName = textBox1.Text;
        }
    }
}
