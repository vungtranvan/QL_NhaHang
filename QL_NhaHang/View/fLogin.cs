using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using System;
using System.Windows.Forms;

namespace QL_NhaHang.View
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            txtErrPasword.Text = "";
            txtErrUserName.Text = "";
        }

        public bool checkInputLogin()
        {
            bool checkInput = true;

            checkInput = Hepler.CheckInputNotNull(txtUserName, txtErrUserName, checkInput);
            checkInput = Hepler.CheckInputNotNull(txtPassword, txtErrPasword, checkInput);

            return checkInput;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (checkInputLogin())
            {
                if (AccountDAO.Instance.CheckLogin(userName, password))
                {
                    Account account = AccountDAO.Instance.GetAccountByUserName(userName);
                    fMain f = new fMain(account);
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnShowHidePass_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtPassword, btnShowHidePass);
        }

        private void btnShowHidePass_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtPassword, btnShowHidePass);
        }
    }
}
