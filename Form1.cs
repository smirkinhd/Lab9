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

namespace Lab9
{
    public partial class Form1 : Form
    {
        string aPath;
        int nImg = 0;
        public Form1()
        {
            InitializeComponent();
            listBox1.Sorted = true;
            DirectoryInfo di;
            di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            aPath = di.FullName;
            label1.Text = "";
            FillListBox(aPath);
        }
        private Boolean FillListBox(string aPath)
        {
            DirectoryInfo di = new DirectoryInfo(aPath);
            FileInfo[] fi = di.GetFiles("*.jpg");
            listBox1.Items.Clear();
            foreach (FileInfo fc in fi)
            {
                listBox1.Items.Add(fc.Name);
            }
            fi = di.GetFiles("*.bmp");
            foreach (FileInfo fc in fi)
            {
                listBox1.Items.Add(fc.Name);
            }
            label1.Text = aPath;
            if (listBox1.Items.Count == 0)
                return false;
            else
            { 
                listBox1.SelectedIndex = 0;
                return true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox1.Image = new Bitmap(aPath + "\\" + listBox1.SelectedItem.ToString());
            if ((pictureBox1.Image.Width > pictureBox1.Width) ||
            (pictureBox1.Image.Height > pictureBox1.Height))
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Выберите папку,\n" + "в которой находятся иллюстрации";
            fb.ShowNewFolderButton = false;
            if (fb.ShowDialog() == DialogResult.OK)
                aPath = fb.SelectedPath;
            label1.Text = aPath;
            if (!FillListBox(fb.SelectedPath))
                pictureBox1.Image = null;

        }

    }
}
