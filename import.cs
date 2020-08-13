﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerProject
{
    public partial class import : Form
    {
        FileReader file = new FileReader();
        Scanner scanner;
        string text = "Lexeme \t\t\t Token" + Environment.NewLine + Environment.NewLine;
        
        public import()
        {
            InitializeComponent();
        }

        private void import_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = openFileDialog1.FileName;
                    file.readAllFile(textBox1.Text);
                    textBox2.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        void display()
        {
            scanner = new Scanner(file);

            while (file.lineno < file.lines)
            {
                scanner.getToken(file);
            }

            foreach (var lexeme in scanner.ScannedList)
            {


                //textBox2.Text = String.Join(Environment.NewLine, scanner.ScannedList);

                text += String.Format("{0} \t\t\t {1}", lexeme.Key.Trim('\0'), lexeme.Value);
                text += Environment.NewLine;
            }
            textBox2.Text = text;
            if (scanner.checkUnmatchedBraces())
                MessageBox.Show("Unmached Braces Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (scanner.checkUnmatchedParanthesis())
                MessageBox.Show("Unmached Parenhesis Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (scanner.checkUnmatchedQuotes())
                MessageBox.Show("Unmached Quotes Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParseTreeGraph page = new ParseTreeGraph(textBox1.Text);
            page.ShowDialog();
        }
    }
}
