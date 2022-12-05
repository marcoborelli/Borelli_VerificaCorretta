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
    public partial class Form1 : Form
    {
        private Elenco elenco;
        infoVoto infoForm = new infoVoto();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ID", 45);
            listView1.Columns.Add("MATERIA", 100);
            listView1.Columns.Add("DATA", 100);
            listView1.Columns.Add("VOTO", 100);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;


            elenco = new Elenco();
        }
        private void button1_Click(object sender, EventArgs e)//aggiungi
        {
            //textbox1=materia textBox2=data textBox3=voto
            elenco.AggiungiVerifica(textBox1.Text, textBox2.Text, float.Parse(textBox3.Text));

            InserisciInListViewEComboBox(listView1, comboBox1, elenco);
        }
        private void button2_Click(object sender, EventArgs e)//calcola media
        {
            MessageBox.Show($"LA TUA MEDIA IN {comboBox1.Text} È {elenco.CalcoloMedia(comboBox1.Text)}");
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                infoForm.id = listView1.SelectedItems[0].SubItems[0].Text;
                infoForm.materia = listView1.SelectedItems[0].SubItems[1].Text;
                infoForm.data = listView1.SelectedItems[0].SubItems[2].Text;
                infoForm.voto = float.Parse(listView1.SelectedItems[0].SubItems[3].Text);

                infoForm.ShowDialog(); //quando chiudo la form torno qui
                elenco.ModificaVerifica(infoForm.materia, infoForm.data, infoForm.voto, int.Parse(infoForm.id));

                InserisciInListViewEComboBox(listView1, comboBox1, elenco);
            }
        }
        private void button3_Click(object sender, EventArgs e)//elimina
        {
            if (listView1.SelectedItems.Count == 1)
            {
                //MessageBox.Show($"{listView1.SelectedItems[0].SubItems[0].Text}");
                elenco.EliminaVoto(int.Parse(listView1.SelectedItems[0].SubItems[0].Text));
            }

            InserisciInListViewEComboBox(listView1, comboBox1, elenco);
        }
        private void button4_Click(object sender, EventArgs e)//ordina
        {
            elenco.OrdinaData();
            InserisciInListViewEComboBox(listView1, comboBox1, elenco);
        }
        public void InserisciInListViewEComboBox(ListView listino, ComboBox combino, Elenco elenchino)
        {
            listino.Items.Clear();

            for (int i = 0; i < elenchino.IdVerifiche; i++)
            {
                string[] fields = elenchino.ToString(i).Split(';');
                //MessageBox.Show($"{fields[2]}");
                ListViewItem item = new ListViewItem(fields);
                listino.Items.Add(item);

                //parte comboBox
                bool aggiungiInCombo = false;

                if (combino.Items.Count > 0)
                {
                    for (int j = 0; j < combino.Items.Count; j++)
                        if (fields[1].ToUpper() != combino.Items[j].ToString())
                            aggiungiInCombo = true;
                }
                else
                    combino.Items.Add($"{fields[1]}");

                if (aggiungiInCombo)
                    combino.Items.Add($"{fields[1]}");
            }
        }
    }
}
