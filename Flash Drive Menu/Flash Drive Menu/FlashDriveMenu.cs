using System;
using System.ComponentModel;
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
        ProgressDialog progressDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        string maindir;
        string syncdir;

        public FlashDriveMenu() {
            InitializeComponent();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(".\\properties.txt");
            while ((line = file.ReadLine()) != null) {
                if (counter == 0)
                    maindir = line;
                if (counter == 1)
                    syncdir = line;
                counter++;
                if (counter == 2) break;
            }

            file.Close();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progressDialog.Hide();
            MessageBox.Show(this, "Download complete!");
        }
        
        private void FlashDriveMenu_Load(object sender, System.EventArgs e) {
            pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDownload = Path.Combine(pathUser, "..\\Downloads\\");

            comboBox1.Items.Add("Select Video and click View Button...");
            DirectoryInfo dInfo1 = new DirectoryInfo(".\\" + maindir + " \\" + syncdir +  "\\Media\\");
            Files1 = dInfo1.GetFiles("*.mp4");
            foreach (FileInfo file1 in Files1) {
                comboBox1.Items.Add(file1.Name);
            }

            comboBox2.Items.Add("Select Exhibit and click View Button...");
            DirectoryInfo dInfo2 = new DirectoryInfo(".\\" + maindir + " \\" + syncdir + "\\Exhibits\\");
            Files2 = dInfo2.GetFiles("*");
            foreach (FileInfo file2 in Files2) {
                comboBox2.Items.Add(file2.Name);
            }

            DirectoryInfo dInfo3 = new DirectoryInfo(".\\" + maindir + " \\" + syncdir + "\\iPad\\");
            Files3 = dInfo3.GetFiles("*");
            
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


            DirectoryInfo dInfo4 = new DirectoryInfo(".\\" + maindir + "\\");
            Files4 = dInfo4.GetFiles("*.jpg");
            if (Files4.Length > 0)
                pictureBox2.Image = Image.FromFile(Files4[0].FullName);
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Run Sync
            Process autoRun = new Process();
            autoRun.StartInfo.FileName = new FileInfo(".\\" + maindir + " \\" + syncdir + "\\Autorun.exe").FullName;
            autoRun.Start();
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

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download Videos
            bool exists = false;
            foreach (FileInfo file1 in Files1) {
                if (File.Exists(System.IO.Path.Combine(pathDownload, file1.Name))) {
                    exists = true;
                    break;
                }
            }
            if (exists)
                if (MessageBox.Show("Files already exist! Do you want to overwrite?",
                        "File exists", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    // Do Nothing; Skip over to copy;
                } else {
                    return;
                }

            if (progressDialog == null) progressDialog = new ProgressDialog();

            backgroundWorker1.DoWork +=
                 new DoWorkEventHandler(downloadVideos);
            backgroundWorker1.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void downloadVideos(object sender,
            DoWorkEventArgs e) {
            foreach (FileInfo file1 in Files1) {
                System.IO.File.Copy(file1.FullName, System.IO.Path.Combine(pathDownload, file1.Name), true);
            }
            backgroundWorker1.DoWork -= downloadVideos;
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download iPad videos
            bool exists = false;
            foreach (FileInfo file3 in Files3) {
                if (File.Exists(System.IO.Path.Combine(pathDownload, file3.Name))) {
                    exists = true;
                    break;
                }
            }
            if (exists)
                if (MessageBox.Show("Files already exist! Do you want to overwrite?",
                        "File exists", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    // Do Nothing; Skip over to copy;
                } else {
                    return;
                }

            if (progressDialog == null) progressDialog = new ProgressDialog();

            backgroundWorker1.DoWork +=
                 new DoWorkEventHandler(downloadIpadVideos);
            backgroundWorker1.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void downloadIpadVideos(object sender,
            DoWorkEventArgs e) {
            foreach (FileInfo file3 in Files3) {
                System.IO.File.Copy(file3.FullName, System.IO.Path.Combine(pathDownload, file3.Name), true);
            }
            backgroundWorker1.DoWork -= downloadIpadVideos;
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download Exhibits
            bool exists = false;
            foreach (FileInfo file2 in Files2) {
                if (File.Exists(System.IO.Path.Combine(pathDownload, file2.Name))) {
                    exists = true;
                    break;
                }
            }
            if (exists)
                if (MessageBox.Show("Files already exist! Do you want to overwrite?",
                        "File exists", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    // Do Nothing; Skip over to copy;
                } else {
                    return;
                }

            if (progressDialog == null) progressDialog = new ProgressDialog();

            backgroundWorker1.DoWork +=
                 new DoWorkEventHandler(downloadExhibits);
            backgroundWorker1.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void downloadExhibits(object sender,
            DoWorkEventArgs e) {
            foreach (FileInfo file2 in Files2) {
                System.IO.File.Copy(file2.FullName, System.IO.Path.Combine(pathDownload, file2.Name), true);
            }
            backgroundWorker1.DoWork -= downloadExhibits;
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Download Full Sync
            DirectoryInfo dInfo5 = new DirectoryInfo(".\\" + maindir + " \\" + syncdir + "");
            if (Directory.Exists(System.IO.Path.Combine(pathDownload, dInfo5.Name)))
            if (MessageBox.Show("Files already exist! Do you want to overwrite?",
                    "File exists", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                // Do Nothing; Skip over to copy;
            } else {
                return;
            }

            if (progressDialog == null) progressDialog = new ProgressDialog();

            backgroundWorker1.DoWork +=
                 new DoWorkEventHandler(downloadFullsync);
            backgroundWorker1.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void downloadFullsync(object sender,
            DoWorkEventArgs e) {
            DirectoryInfo dInfo5 = new DirectoryInfo(".\\" + maindir + " \\" + syncdir);
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(pathDownload, dInfo5.Name));
            DirectoryInfo dInfo6 = new DirectoryInfo(System.IO.Path.Combine(pathDownload, dInfo5.Name));
            CopyFilesRecursively(dInfo5, dInfo6);
            backgroundWorker1.DoWork -= downloadFullsync;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            button1.Enabled = comboBox1.SelectedIndex > 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            button2.Enabled = comboBox2.SelectedIndex > 0;
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}
