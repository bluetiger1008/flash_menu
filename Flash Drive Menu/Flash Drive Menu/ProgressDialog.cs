using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Flash_Drive_Menu {
    public partial class ProgressDialog : Form {
        public ProgressDialog() {
            InitializeComponent();
            progressBar1.MarqueeAnimationSpeed = 10;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }
    }
}
