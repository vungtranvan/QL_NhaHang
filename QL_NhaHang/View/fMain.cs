using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QL_NhaHang.DTO;
using QL_NhaHang.Event;

namespace QL_NhaHang.View
{
    public partial class fMain : Form
    {
        private Account accountLogin;

        public Account AccountLogin { get => accountLogin; private set => accountLogin = value; }

        public fMain(Account acc)
        {
            InitializeComponent();
            this.accountLogin = acc;
            ChangerAccountLogin(acc);
            AbrirFormInPanel(new fHome(), btnHome);
            this.WindowState = FormWindowState.Maximized;

        }

        #region Method
        private void AbrirFormInPanel(object Formhijo, Button btn)
        {
            ChangerColorButtonClickMenuBar(btn);

            if (this.panelContentedor.Controls.Count > 0)
                this.panelContentedor.Controls.RemoveAt(0);
            Form f = Formhijo as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelContentedor.Controls.Add(f);
            this.panelContentedor.Tag = f;
            f.Show();
        }

        public void ChangerColorButtonClickMenuBar(Button btn)
        {
            foreach (Control item in MenuVetical.Controls)
            {
                if (item.GetType() == typeof(Button))
                {
                    item.BackColor = Color.FromArgb(0, 117, 214);
                }
            }
            btn.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void ChangerAccountLogin(Account acc)
        {
            if (acc.Type == 0)
            {
                btnAdminManager.Visible = false;
                btnReport.Visible = false;
            }
            txtUserNameAdmin.Text = acc.DisplayName;
        }
        #endregion

        #region Event

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (MenuVetical.Width == 250)
            {
                MenuVetical.Width = 70;
            }
            else
            {
                MenuVetical.Width = 250;
            }
        }

        private void iconClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Hide();
                fLogin f = new fLogin();
                f.ShowDialog();
            }
        }

        private void iconMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconMaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconMaximizar.Visible = true;
        }

        private void iconMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0xA1, 0x2, 0);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new fHome(), btnHome);
        }

        private void btnAdminManager_Click(object sender, EventArgs e)
        {
            fAdmin fa = new fAdmin(AccountLogin);
            fa.UpdateAccount += F_UpdateAccount;
            AbrirFormInPanel(fa, btnAdminManager);
        }

        private void F_UpdateAccount(object sender, AccountEvent e)
        {
            txtUserNameAdmin.Text = e.Acc.DisplayName;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new fReport(), btnReport);
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {
            if (MenuVetical.Width == 250)
            {
                MenuVetical.Width = 70;
                btnLogo.Height = 32;
                btnLogo.Width = 64;
                btnLogo.Padding = new Padding(12, 0, 0, 0);
                this.btnLogo.Location = new System.Drawing.Point(3, 10);

                btnLogo.Image = Properties.Resources.menu_32px;
            }
            else
            {
                MenuVetical.Width = 250;
                btnLogo.Height = 90;
                btnLogo.Width = 245;
                btnLogo.Padding = new Padding(0, 0, 0, 0);
                this.btnLogo.Location = new System.Drawing.Point(3, 3);
                btnLogo.Image = Properties.Resources.logo_restaurant_leadership;
            }
        }

        private void btnChangerPassword_Click(object sender, EventArgs e)
        {
            fChangerPassword f = new fChangerPassword(AccountLogin);
            f.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Hide();
                fLogin f = new fLogin();
                f.ShowDialog();
            }
        }
        private void btnChangerInfoAcc_Click(object sender, EventArgs e)
        {
            fChangerInfomationAcc f = new fChangerInfomationAcc(AccountLogin);
            f.UpdateAcc += F_UpdateAcc;
            f.ShowDialog();
        }

        private void F_UpdateAcc(object sender, AccountEvent e)
        {
            txtUserNameAdmin.Text = e.Acc.DisplayName;
        }
        #endregion

    }
}
