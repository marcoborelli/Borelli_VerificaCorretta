using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Borelli_Verifica
{
    public partial class infoVoto : Form
    {
        public string id { get; set; }
        public string materia { get; set; }
        public string data { get; set; }
        public float voto { get; set; }
        bool reload = true;
        public infoVoto()
        {
            InitializeComponent();
        }

        private void infoVoto_Load(object sender, EventArgs e)
        {
            if (reload)
            {
                textBox1.Text = id;
                textBox1.Enabled = false;

                textBox2.Text = materia;
                textBox3.Text = data;
                textBox4.Text = $"{voto}";
            }
            reload = false;

        }

        private void infoVoto_FormClosing(object sender, FormClosingEventArgs e)
        {
            reload = true;

            materia = textBox2.Text;
            data = textBox3.Text;
            voto = float.Parse(textBox4.Text);

            e.Cancel = true;
            this.Visible = false;
        }
    }
}
