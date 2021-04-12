using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.View
{
    public partial class fChangerPassword : Form
    {
        private Account accountLogin;

        public Account AccountLogin { get => accountLogin; private set => accountLogin = value; }
        public fChangerPassword(Account acc)
        {
            InitializeComponent();
            this.accountLogin = acc;
            txtUserName.Text = acc.UserName;
            ResetAccount();
        }
        public void ResetAccount()
        {
            txtPassword.Text = "";
            txtErrorPassword.Text = "";

            txtPasswordNew.Text = "";
            txtErrorPasswordNew.Text = "";

            txtConfPassword.Text = "";
            txtErrorConfPassword.Text = "";
        }
        public bool CheckInputEditAccount()
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtPassword, txtErrorPassword, checkInput);
            checkInput = Hepler.CheckInputNotNull(txtPasswordNew, txtErrorPasswordNew, checkInput);
            checkInput = Hepler.CheckInputNotNull(txtConfPassword, txtErrorConfPassword, checkInput);

            if (!txtPasswordNew.Text.Equals(txtConfPassword.Text))
            {
                txtErrorConfPassword.Text = "Mật khẩu mới chưa trùng khớp";
                checkInput = false;
            }
            else
            {
                txtErrorConfPassword.Text = "";
            }
            if (!AccountDAO.Instance.CheckLogin(txtUserName.Text, txtPassword.Text))
            {
                txtErrorPassword.Text = "Mật khẩu không đúng";
                checkInput = false;
            }
            else
            {
                txtErrorPassword.Text = "";
            }

            return checkInput;
        }
        private void btnResetAccount_Click(object sender, EventArgs e)
        {
            ResetAccount();
        }

        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (CheckInputEditAccount())
            {
                if (AccountDAO.Instance.UpdatePasswordAccount(txtUserName.Text, txtPasswordNew.Text))
                {
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowHidePassOld_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtPassword, btnShowHidePassOld);
        }

        private void btnShowHidePassOld_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtPassword, btnShowHidePassOld);
        }

        private void btnShowHidePassNew_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtPasswordNew, btnShowHidePassNew);
        }

        private void btnShowHidePassNew_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtPasswordNew, btnShowHidePassNew);
        }

        private void btnShowHidePassConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtConfPassword, btnShowHidePassConfirm);
        }

        private void btnShowHidePassConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtConfPassword, btnShowHidePassConfirm);
        }
    }
}
