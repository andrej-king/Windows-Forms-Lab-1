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
using System.Text.RegularExpressions;

namespace Forms_Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // private void Form1_load(object sender, EventArgs e)
        // {
        //     comboBox1.SelectedItem = null;
        //     comboBox1.SelectedText = "---select---";
        // }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();

            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(OpenDlg.FileName, Encoding.UTF8);
                richTextBox1.Text = Reader.ReadToEnd();
                Reader.Close();
            }

            OpenDlg.Dispose();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();

            if (SaveDlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(SaveDlg.FileName);

                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    Writer.WriteLine((string) listBox2.Items[i]);
                }

                Writer.Close();
            }

            SaveDlg.Dispose();
        }

        private void выходAltXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Тестовое приложение.\nCreate by Pedchenko Andrii");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            listBox1.BeginUpdate();

            string[] Strings =
                richTextBox1.Text.Split(new char[] {'\n', '\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in Strings)
            {
                string Str = s.Trim();

                if (Str == String.Empty) continue;
                if (radioButton1.Checked) listBox1.Items.Add(Str);
                if (radioButton2.Checked)
                {
                    if (Regex.IsMatch(Str, @"\d")) listBox1.Items.Add(Str);
                }

                if (radioButton3.Checked)
                {
                    if (Regex.IsMatch(Str, @"\w+@\w+\.\w+")) listBox1.Items.Add(Str);
                }
            }

            listBox1.EndUpdate();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            richTextBox1.Clear();
            textBox1.Clear();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            String Find = textBox1.Text;

            if (checkBox1.Checked)
            {
                foreach (string String in listBox1.Items)
                {
                    if (String.Contains(Find)) listBox3.Items.Add(String);
                }
            }

            if (checkBox2.Checked)
            {
                foreach (string String in listBox2.Items)
                {
                    if (String.Contains(Find)) listBox3.Items.Add(String);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form_Add AddRec = new Form_Add();
            AddRec.Owner = this;
            AddRec.ShowDialog();
        }

        // delete items
        private void DeleteSelectedStrings(ListBox ListBox)
        {
            for (int i = ListBox.Items.Count - 1; i >= 0; i--)
            {
                if (ListBox.GetSelected(i)) ListBox.Items.RemoveAt(i);
            }
        }

        // move all item to
        private void MoveAllTo(ListBox ListBoxFrom, ListBox ListBoxTo)
        {
            ListBoxTo.Items.AddRange(ListBoxFrom.Items);
            ListBoxFrom.Items.Clear();

            ListBoxTo.BeginUpdate();

            foreach (object Item in ListBoxFrom.SelectedItems)
            {
                ListBoxTo.Items.Add(Item);
            }

            ListBoxTo.EndUpdate();
        }

        // move item to
        private void MoveItem(ListBox ListBoxFrom, ListBox ListBoxTo)
        {
            for (int i = ListBoxFrom.Items.Count - 1; i >= 0; i--)
            {
                if (ListBoxFrom.GetSelected(i))
                {
                    ListBoxTo.Items.Add(ListBoxFrom.Items[i]);
                    ListBoxFrom.Items.RemoveAt(i);
                }
            }
        }
        
        private List<String> listBoxItems(ListBox listBox) // Получить все элементы listBox в список и очистить listBox
        {
            List<String> items = new List<String>();
            
            foreach (string String in listBox.Items)
            {
                items.Add(String);
            }
            listBox.Items.Clear();
            return items;
        }
        
        
        private void SortByAbc(ListBox listBoxName, ComboBox comboBoxText, String fieldName) // Сортировка по алфавиту
        {
            List<string> items = listBoxItems(listBoxName);

            if (comboBoxText.Text == fieldName)
            {
                items.Sort();

                foreach (string String in items)
                {
                    listBoxName.Items.Add(String);
                }
            }
        }

        private void SortByAbcDesc(ListBox listBoxName, ComboBox comboBoxText, String fieldName) // Сортировка по алфавиту (по убыванию)
        {
            List<string> items = listBoxItems(listBoxName);
            
            items.Sort();
            items.Reverse();

            if (comboBoxText.Text == fieldName)
            {
                items.Sort();
                items.Reverse();

                foreach (string String in items)
                {
                    listBoxName.Items.Add(String);
                }
            }
        }

        private void SortByLength(ListBox listBoxName, ComboBox comboBoxText, String fieldName) // Сортировка по длине слова (по возрастанию)
        {
            List<string> items = listBoxItems(listBoxName);
            
            items.Sort();
            items.Reverse();

            if (comboBoxText.Text == fieldName)
            {
                var newOrderBy = items.OrderBy(s => s.Length);
                foreach (var item in newOrderBy)
                    listBoxName.Items.Add(item);
            }
        }
        
        private void SortByLengthDesc(ListBox listBoxName, ComboBox comboBoxText, String fieldName) // Сортировка по длине слова (по убыванию)
        {
            List<string> items = listBoxItems(listBoxName);
            
            items.Sort();
            items.Reverse();

            if (comboBoxText.Text == fieldName)
            {
                var newOrderBy = items.OrderByDescending(s => s.Length);
                foreach (var item in newOrderBy)
                    listBoxName.Items.Add(item);
            }
        }
        

        private void button8_Click(object sender, EventArgs e)
        {
            DeleteSelectedStrings(listBox1);
            DeleteSelectedStrings(listBox2);
            DeleteSelectedStrings(listBox3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MoveAllTo(listBox1, listBox2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoveAllTo(listBox2, listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveItem(listBox1, listBox2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MoveItem(listBox2, listBox1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Алфавиту (по возрастанию)":
                    SortByAbc(listBox1, comboBox1, "Алфавиту (по возрастанию)");
                    break;
                
                case "Алфавиту (по убыванию)":
                    SortByAbcDesc(listBox1, comboBox1, "Алфавиту (по убыванию)");
                    break;
                
                case "Длине слова (по возрастанию)":
                    SortByLength(listBox1, comboBox1, "Длине слова (по возрастанию)");
                    break;
                
                case "Длине слова (по убыванию)":
                    SortByLengthDesc(listBox1, comboBox1, "Длине слова (по убыванию)");
                    break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {
                case "Алфавиту (по возрастанию)":
                    SortByAbc(listBox2, comboBox2, "Алфавиту (по возрастанию)");
                    break;
                
                case "Алфавиту (по убыванию)":
                    SortByAbcDesc(listBox2, comboBox2, "Алфавиту (по убыванию)");
                    break;
                
                case "Длине слова (по возрастанию)":
                    SortByLength(listBox2, comboBox2, "Длине слова (по возрастанию)");
                    break;
                
                case "Длине слова (по убыванию)":
                    SortByLengthDesc(listBox2, comboBox2, "Длине слова (по убыванию)");
                    break;
            }
        }
    }
}