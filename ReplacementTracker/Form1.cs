using ReplacementTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplacementTracker
{
    public partial class Form1 : Form
    {
        LoginView loginView = new LoginView();
        DetailView detailView = new DetailView();
        OperatorView operatorView = new OperatorView();
        RecordView recordView = new RecordView();

        public Form1()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(1024, 768);

            
            loginView.Dock = DockStyle.Fill;
            detailView.Dock = DockStyle.Fill;
            recordView.Dock = DockStyle.Fill;
            operatorView.Dock = DockStyle.Fill;

            this.plBaseMain.Controls.Add(loginView);
            this.plBaseMain.Controls.Add(detailView);
            this.plBaseMain.Controls.Add(operatorView);
            this.plBaseMain.Controls.Add(recordView);

            loginView.BringToFront();

            #region Navigation Bar
            List<Label> naviBtns = new List<Label>();

            naviBtns.Add(lblHome);
            naviBtns.Add(lblLogin);
            naviBtns.Add(lblSetting);
            naviBtns.Add(lblDown);
            naviBtns.Add(lblUp);

            foreach (var item in naviBtns)
            {
                item.MouseHover += Item_MouseHover;
                item.MouseLeave += Item_MouseLeave;
            }

            //lblHome.MouseHover += LblHome_MouseHover;
            //lblHome.MouseLeave += LblHome_MouseLeave;
            #endregion

        }

        private void Item_MouseLeave(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            lbl.ForeColor = Color.Black;
        }

        private void Item_MouseHover(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            lbl.ForeColor = Color.BlueViolet;
        }

        //private void LblHome_MouseLeave(object sender, EventArgs e)
        //{
        //    lblHome.ForeColor = Color.Black;
        //}

        //private void LblHome_MouseHover(object sender, EventArgs e)
        //{
        //    lblHome.ForeColor = Color.Blue;
        //}
    }
}
