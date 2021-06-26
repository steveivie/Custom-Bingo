using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IvieBingo2021
{
    public partial class frmBingo : Form
    {
        private static readonly string ITEMS_PATH = "C:\\Steve\\BINGO\\Names2021.txt";
        private static List<String> items;
        private static List<String> currentGameList;
        private static int currentIndex = 0;
        private static readonly Random rnd = new Random();

        public frmBingo()
        {
            InitializeComponent();
            
            string[] itemStrings = File.ReadAllLines(ITEMS_PATH);
            items = new List<string>(itemStrings);
            currentGameList = new List<string>(items);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            lblCurrentCall.Text = currentGameList.ElementAt(currentIndex++);
            listBox1.Items.Insert(0,lblCurrentCall.Text);
            if (currentIndex >= currentGameList.Count)
            {
                btnNext.Enabled = false;
            }
            
        }

        private void Shuffle(IList<String> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = (rnd.Next(0, n));
                n--;
                String value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to clear and start a new game?", "Start New Game?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.Equals(DialogResult.Yes)){
                Shuffle(currentGameList);
                listBox1.Items.Clear();
                btnNext.Enabled = true;
                lblCurrentCall.Text = "";
                currentIndex = 0;
            }
        }
    }
}
