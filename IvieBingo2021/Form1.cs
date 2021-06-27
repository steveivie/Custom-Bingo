using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CustomBingoCaller
{
    public partial class frmBingo : Form
    {
        private static readonly string ITEMS_PATH = "C:\\Steve\\BINGO\\Names2021.txt";
        private static readonly string imageFolder = "C:\\Steve\\BINGO\\BingoPictures\\";
        
        private static List<String> currentGameList;
        private static int currentIndex = 0;
        private static readonly Random rnd = new Random();

        public frmBingo()
        {
            InitializeComponent();
            currentGameList = loadListFromFile( ITEMS_PATH );
            lblCurrentCall.Text = "";
            listBox1.Items.Clear();
        }

        private List<String> loadListFromFile(String file)
        {
            string[] itemStrings = File.ReadAllLines(file);
            return new List<string>(itemStrings);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            nextBingoItem(); 
        }

        private void pbxCurrentPicture_Click(object sender, EventArgs e)
        {
            nextBingoItem();
        }

        private void lblCurrentCall_Click(object sender, EventArgs e)
        {
            nextBingoItem();
        }

        private void nextBingoItem()
        {
            if (currentIndex < currentGameList.Count) { 
                lblCurrentCall.Text = currentGameList.ElementAt(currentIndex++);
                listBox1.Items.Insert(0, lblCurrentCall.Text);
                setPicture(lblCurrentCall.Text);
            }
        }

        private void setPicture(string text)
        {
            String s = getIntelligentFilePath(text, imageFolder);
            if (s != null && s.Length > 0 && File.Exists(s))
            {
                pbxCurrentPicture.Image = new Bitmap(s);
                //new Bitmap(imageFolder + text + ".jpeg");
            }
            else
            {
                pbxCurrentPicture.Image = null;
            }
        }

        private static string getIntelligentFilePath(String itemName, String @folder)
        {
            String result = "";
            if (Directory.Exists(folder))
            {
                
                string[] matches = Directory.GetFiles(@folder, itemName + "*");
                //Try just getting the first word
                if (matches.Length == 0)
                {
                    int index = itemName.IndexOf(" ");
                    if (index > 0) { 
                        String shortenedItemName = itemName.Substring(0, itemName.IndexOf(" "));
                        matches = Directory.GetFiles(@folder, shortenedItemName + "*");
                    }
                }
                if (matches.Length > 0)
                {
                    result = matches[rnd.Next(0, matches.Length)];
                }
            }
            return result;
        }

        private String getFirstWord(String s)
        {
            return s.Substring(0, s.IndexOf(" "));
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

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void newGame()
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to clear and start a new game?", "Start New Game?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.Equals(DialogResult.Yes))
            {
                Shuffle(currentGameList);
                listBox1.Items.Clear();
                lblCurrentCall.Text = "";
                currentIndex = 0;
            }
        }

        private void menuStrip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            nextBingoItem();
        }

    }
}
