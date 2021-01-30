using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace trie
{

    public partial class Form1 : Form
    {
        Stopwatch time = new Stopwatch();
        Trie<string> trie;
       
        Form2 fm2 = new Form2();

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("test2.txt", Encoding.UTF8);
            trie = new Trie<string>();

            for (int i = 0; i < lines.Length; i += 3)
            {
                trie.Add(lines[i], lines[i + 2]);
            }
           

        }
        string Search(Trie<string> trie, string word)
        {

            if (trie.TrySearch(word, out string value))
            {
                string v = value;
                return v;
            }

            else
            {
                return "-1";
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            string res, fin_text = ""; ;
            string[] tx;
            res = textBox1.Text;
            res= res.ToLower();
            string outstr;
            outstr = Search(trie, res);
            listBox1.Items.Clear();
            if (res.Length > 0)
            {
                time.Restart();
                time.Start();
                fin_text = trie.SearchAll(res, trie.root, 0, fin_text);
                time.Stop();
                label5.Text = "Время - "+time.ElapsedMilliseconds.ToString();
                tx = fin_text.Split('_');
                listBox1.Items.AddRange(tx);
                if (listBox1.Items.Count != 0)
                    listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                label5.Text += " мс \tКоличество элементов - " + listBox1.Items.Count;
            }
            if (outstr != "-1")
            {
                textBox2.Clear();
                textBox2.Text = outstr.ToLower();
               
            }
            if (textBox1.Text.Length == 0) 
            {
                label5.Text = "";
            }
            

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (string)listBox1.SelectedItem;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            fm2.ShowDialog();
        }

        
    }
}
