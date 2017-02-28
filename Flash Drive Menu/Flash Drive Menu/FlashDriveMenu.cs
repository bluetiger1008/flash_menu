using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Flash_Drive_Menu {
    public partial class FlashDriveMenu : Form {

        string pathUser;
        string pathDownload;
        FileInfo[] Files1;
        FileInfo[] Files2;
        FileInfo[] Files3;
        FileInfo[] Files4;
        ProgressBar progressBar = new ProgressBar();

        public FlashDriveMenu() {
            InitializeComponent();
        }

        private void label1_Click(object sender, System.EventArgs e) {

        }

        private void FlashDriveMenu_Load(object sender, System.EventArgs e) {
            pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDownload = Path.Combine(pathUser, "..\\Downloads\\");

            comboBox1.Items.Add("Select Video and click View Button...");
            DirectoryInfo dInfo1 = new DirectoryInfo(".\\November 03 - Warfel\\MW110316\\Media\\");
            Files1 = dInfo1.GetFiles("*.mp4");
            foreach (FileInfo file1 in Files1) {
                comboBox1.Items.Add(file1.Name);
            }

            comboBox2.Items.Add("Select Exhibit and click View Button...");
            DirectoryInfo dInfo2 = new DirectoryInfo(".\\November 03 - Warfel\\MW110316\\Exhibits\\");
            Files2 = dInfo2.GetFiles("*");
            foreach (FileInfo file2 in Files2) {
                comboBox2.Items.Add(file2.Name);
            }

            DirectoryInfo dInfo3 = new DirectoryInfo(".\\November 03 - Warfel\\MW110316\\iPad\\");
            Files3 = dInfo3.GetFiles("*");

            progressBar.Style = ProgressBarStyle.Marquee;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


            DirectoryInfo dInfo4 = new DirectoryInfo(".\\November 03 - Warfel\\");
            Files4 = dInfo4.GetFiles("*.jpg");
            if (Files4.Length > 0)
                pictureBox2.Image = Image.FromFile(Files4[0].FullName);
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process autoRun = new Process();
            autoRun.StartInfo.FileName = ".\\November 03 - Warfel\\MW110316\\Autorun.exe";
            autoRun.Start();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download Videos
            progressBar.Show();
            foreach (FileInfo file1 in Files1) {
                System.IO.File.Copy(file1.FullName, System.IO.Path.Combine(pathDownload, file1.Name), true);
            }            
            progressBar.Hide();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download iPad videos
            progressBar.Show();
            foreach (FileInfo file3 in Files3) {
                System.IO.File.Copy(file3.FullName, System.IO.Path.Combine(pathDownload, file3.Name), true);
            }
            progressBar.Hide();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download Exhibits
            progressBar.Show();
            foreach (FileInfo file2 in Files2) {
                System.IO.File.Copy(file2.FullName, System.IO.Path.Combine(pathDownload, file2.Name), true);
            }
            progressBar.Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
            // View Videos
            if (comboBox1.SelectedIndex < 1) return;
            System.Diagnostics.Process.Start(Files1[comboBox1.SelectedIndex - 1].FullName);
        }

        private void button2_Click(object sender, EventArgs e) {
            // View Exhibits
            if (comboBox2.SelectedIndex < 1) return;
            System.Diagnostics.Process.Start(Files2[comboBox2.SelectedIndex - 1].FullName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            button1.Enabled = comboBox1.SelectedIndex > 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            button2.Enabled = comboBox2.SelectedIndex > 0;
        }
    }
}
