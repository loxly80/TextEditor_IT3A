using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace TextEditor_IT3A
{
    public partial class Form1 : Form
    {
        string filePath;
        string configFilePath = "config.cfg";

        public Form1()
        {
            InitializeComponent();
            if (File.Exists(configFilePath))
            {
                filePath = File.ReadAllText(configFilePath);
                if (File.Exists(filePath))
                {
                    txtContent.Text = File.ReadAllText(filePath);
                    btnSave.Enabled = true;
                    btnSaveAs.Enabled = true;
                }
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ofd.FileName = "";
            ofd.Filter = "All files|*.*|C# class|*.cs|Text file|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                Text = filePath;
                if (File.Exists(filePath))
                {
                    txtContent.Text = File.ReadAllText(filePath);
                    btnSave.Enabled = true;
                    btnSaveAs.Enabled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(filePath, txtContent.Text);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            sfd.FileName = "NewFile.txt";
            sfd.Filter = "Text file|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                Text = filePath;
                File.WriteAllText(filePath, txtContent.Text);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Opravdu chcete ukončit bez uložení?","Zavírání aplikace",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            //var json = JsonConvert.SerializeObject(filePath);
            File.WriteAllText(configFilePath,filePath);
        }
    }
}
