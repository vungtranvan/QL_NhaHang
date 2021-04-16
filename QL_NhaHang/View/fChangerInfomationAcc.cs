using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using QL_NhaHang.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.View
{
    public partial class fChangerInfomationAcc : Form
    {
        private Account accountLogin;

        public Account AccountLogin { get => accountLogin; private set => accountLogin = value; }

        private event EventHandler<AccountEvent> updateAcc;

        public event EventHandler<AccountEvent> UpdateAcc
        {
            add { updateAcc += value; }
            remove { updateAcc -= value; }
        }

        public fChangerInfomationAcc(Account Acc)
        {
            InitializeComponent();
            accountLogin = Acc;
            ResetForm(Acc);
        }

        #region METHOD
        public bool CheckInputAccount()
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtDisplayName, txtErrorDisplayName, checkInput);
            if (!string.IsNullOrEmpty(txtPhone.Text.ToString()))
            {
                if (string.IsNullOrEmpty(Hepler.CheckPhoneNumber(txtPhone.Text)))
                {
                    txtErrorPhone.Text = "Ko đúng định dạng";
                    checkInput = false;
                }
                else
                {
                    txtErrorPhone.Text = "";
                }
            }

            checkInput = Hepler.CheckInputNotNull(txtPassword, txtErrorPassword, checkInput);
            checkInput = Hepler.CheckInputNotNull(txtConfPassword, txtErrorConfPassword, checkInput);

            if (!string.IsNullOrEmpty(txtPassword.Text.ToString()))
            {
                if (!txtPassword.Text.Equals(txtConfPassword.Text))
                {
                    txtErrorConfPassword.Text = "Mật khẩu chưa trùng khớp";
                    checkInput = false;
                }
                else
                {
                    txtErrorConfPassword.Text = "";
                }

                if (!txtPassword.Text.Equals(AccountLogin.Password))
                {
                    txtErrorPassword.Text = "Mật khẩu chưa đúng";
                    checkInput = false;
                }
                else
                {
                    txtErrorPassword.Text = "";
                }
            }

            return checkInput;
        }


        public void ResetForm(Account _acc)
        {
            Account acc = AccountDAO.Instance.GetAccountByUserName(_acc.UserName);
            txtUserName.Text = acc.UserName;
            txtDisplayName.Text = acc.DisplayName;
            txtPhone.Text = acc.Phone;
            txtAddress.Text = acc.Address;
            txtErrorDisplayName.Text = txtErrorPhone.Text = txtErrorAddress.Text = txtPassword.Text = txtErrorPassword.Text = txtConfPassword.Text = txtErrorConfPassword.Text = "";

            if (_acc.Sex)
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
        }
#endregion

        #region EVENT
        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetAccount_Click(object sender, EventArgs e)
        {
            ResetForm(AccountLogin);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (CheckInputAccount())
            {
                string userName = txtUserName.Text;
                string displayname = txtDisplayName.Text;
                string phone = txtPhone.Text;
                string address = txtAddress.Text;
                string password = txtPassword.Text;
                bool sex = true;
                if (rdNu.Checked)
                {
                    sex = false;
                }

                Account acc = new Account(displayname, userName, password, phone, address, sex, AccountLogin.Type);

                if (AccountDAO.Instance.UpdateAccount(acc))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Account accSuccessUpdate = AccountDAO.Instance.GetAccountByUserName(userName);
                    if (updateAcc != null)
                    {
                        updateAcc(this, new AccountEvent(accSuccessUpdate));
                    }
                    ResetForm(accSuccessUpdate);
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShowHidePass_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtPassword, btnShowHidePass);
        }

        private void btnShowHidePass_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtPassword, btnShowHidePass);
        }

        private void btnShowHidePassConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            Hepler.SetShowPass(txtConfPassword, btnShowHidePassConfirm);

        }

        private void btnShowHidePassConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            Hepler.SetHidePass(txtConfPassword, btnShowHidePassConfirm);
        }

        #endregion
    }
}
