using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using QL_NhaHang.Event;
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

namespace QL_NhaHang.View
{
    public partial class fAdmin : Form
    {
        private Account accountLogin;

        public Account AccountLogin { get => accountLogin; private set => accountLogin = value; }

        private event EventHandler<AccountEvent> updateAccount;

        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        public fAdmin(Account acc)
        {
            InitializeComponent();
            this.accountLogin = acc;
            LoadData();
        }

        #region Method

        public void LoadData()
        {
            dtgvFoodCategory.AutoGenerateColumns = false;
            dgvFood.AutoGenerateColumns = false;
            dtgvAccount.AutoGenerateColumns = false;


            ResetFood();
            ResetFoodCategory();
            ResetAccount();
            LoadcbxSearchFoodCategory();


        }

        public void LoadCbxTypeAccount()
        {
            var lstTypeAcc = new BindingList<KeyValuePair<int, string>>();

            lstTypeAcc.Add(new KeyValuePair<int, string>(0, "Nhân Viên"));
            lstTypeAcc.Add(new KeyValuePair<int, string>(1, "Admin"));

            cbTypeAccount.DataSource = lstTypeAcc;
            cbTypeAccount.DisplayMember = "Value";
            cbTypeAccount.ValueMember = "Key";
            cbTypeAccount.SelectedIndex = 0;
        }

        public void LoadDataAccount(string key)
        {
            if (key != null)
            {
                dtgvAccount.DataSource = AccountDAO.Instance.GetListAccount(key);
            }
        }

        public void LoadcbxFoodCategory()
        {
            cbxLoaiMon.DataSource = FoodCategoryDAO.Instance.GetCategory("");
            cbxLoaiMon.DisplayMember = "Name";
            cbxLoaiMon.ValueMember = "Id";
        }

        public void LoadcbxSearchFoodCategory()
        {
            List<FoodCategory> lstcbxSearch = new List<FoodCategory>();
            lstcbxSearch.Add(new FoodCategory(0, "---- Chọn Danh Mục -----"));
            foreach (var item in FoodCategoryDAO.Instance.GetCategory(""))
            {
                lstcbxSearch.Add(item);
            }

            cbxSearchLoaiMon.DataSource = lstcbxSearch;
            cbxSearchLoaiMon.DisplayMember = "Name";
            cbxSearchLoaiMon.ValueMember = "Id";
        }

        public void LoadListFood(string nameFood)
        {
            if (nameFood != null)
            {
                dgvFood.DataSource = FoodDAO.Instance.GetListFood(nameFood);
            }
        }

        public void LoadListFoodCategory(string nameSearch)
        {
            if (nameSearch != null)
            {
                dtgvFoodCategory.DataSource = FoodCategoryDAO.Instance.GetCategoryToDataTable(nameSearch);
            }
        }

        public bool CheckInputFood()
        {
            bool checkInput = true;

            checkInput = Hepler.CheckInputNotNull(txtTenMon, txtErrorTenMon, checkInput);
            checkInput = Hepler.CheckInputNotNull(txtDonViTinh, txtErrDonViTinh, checkInput);

            return checkInput;
        }

        public bool CheckInputFoodCategory()
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtTenFoodCategory, txtErrorNameFoodCategory, checkInput);

            if (checkInput == true)
            {
                if (FoodCategoryDAO.Instance.GetCategoryByName(txtTenFoodCategory.Text))
                {
                    txtErrorNameFoodCategory.Text = "Danh mục này đã tồn tại";
                    checkInput = false;
                }
                else
                {
                    txtErrorNameFoodCategory.Text = "";
                }
            }
            return checkInput;
        }

        public bool CheckInputAccount(bool edit = false)
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtUserName, txtErrorUserName, checkInput);
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

            if (!edit)
            {
                checkInput = Hepler.CheckInputNotNull(txtPassword, txtErrorPassword, checkInput);
                if (checkInput == true)
                {
                    Account accCheck = AccountDAO.Instance.GetAccountByUserName(txtUserName.Text);
                    if (accCheck != null)
                    {
                        txtErrorUserName.Text = "UserName này đã tồn tại";
                        checkInput = false;
                    }
                    else
                    {
                        txtErrorUserName.Text = "";
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtPassword.Text.ToString()))
                {
                    checkInput = Hepler.CheckInputNotNull(txtConfPassword, txtErrorConfPassword, checkInput);
                    if (!txtPassword.Text.Equals(txtConfPassword.Text))
                    {
                        txtErrorConfPassword.Text = "Mật khẩu mới chưa trùng khớp";
                        checkInput = false;
                    }
                    else
                    {
                        txtErrorConfPassword.Text = "";
                    }
                }
            }

            return checkInput;
        }

        public void ResetFood()
        {
            txtMaFood.Text = txtTenMon.Text = txtDonViTinh.Text = txtSearchFood.Text = txtErrorTenMon.Text = txtErrDonViTinh.Text = "";
            txtGia.Value = 0;
            rdShowFood.Checked = true;
            Hepler.SetImageToPictureBox(pictureFood, Hepler.ImageNameDefault);
            LoadcbxFoodCategory();
            LoadcbxSearchFoodCategory();
            LoadListFood("");
        }

        public void ResetFoodCategory()
        {
            txtSearchFoodCategory.Text = txtErrorNameFoodCategory.Text = txtMaFoodCategory.Text = txtTenFoodCategory.Text = "";
            LoadListFoodCategory("");
            LoadcbxFoodCategory();
        }

        public void ResetAccount()
        {
            txtUserName.Text = txtErrorUserName.Text = txtDisplayName.Text = txtErrorDisplayName.Text = txtPhone.Text =
            txtErrorPhone.Text = txtAddress.Text = txtErrorAddress.Text = txtPassword.Text = txtErrorPassword.Text =
            txtConfPassword.Text = txtErrorConfPassword.Text = txtSearchAccount.Text = "";
            rdNam.Checked = true;
            LoadCbxTypeAccount();
            LoadDataAccount("");
        }

        #endregion


        #region Event

        #region ===================== Event Foood =======================
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (CheckInputFood())
            {
                bool status = true;
                if (rdHideFood.Checked)
                {
                    status = false;
                }
                if (FoodDAO.Instance.InsertFood(txtTenMon.Text, (int)cbxLoaiMon.SelectedValue, (float)txtGia.Value, txtDonViTinh.Text, Hepler.GetFileByTagPicture(pictureFood), status))
                {
                    MessageBox.Show("Thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFood();
                }
                else
                {
                    MessageBox.Show("Thêm món thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnuploadimage_click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureFood.Image = Image.FromFile(ofd.FileName);
                    pictureFood.Tag = ofd.FileName;
                }
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string idFood = txtMaFood.Text;

            if (idFood != "")
            {
                if (CheckInputFood())
                {
                    bool status = true;
                    if (rdHideFood.Checked)
                    {
                        status = false;
                    }
                    if (FoodDAO.Instance.UpdateFood(Int32.Parse(idFood), txtTenMon.Text, (int)cbxLoaiMon.SelectedValue, (float)txtGia.Value, txtDonViTinh.Text, Hepler.GetFileByTagPicture(pictureFood), status))
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFood();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            List<int> lstIdFood = Hepler.CheckDataGridViewCheckBox<int>(dgvFood, 0, 7);
            if (lstIdFood.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string successDelete = "";
                    string errorDelete = "";
                    foreach (int item in lstIdFood)
                    {
                        if (BillInfoDAO.Instance.GetByIdFood(item) == 0)
                        {
                            //string nameImageFood = (FoodDAO.Instance.GetFoodById(item).ImageFood).ConvertByteArrayToString();
                            FoodDAO.Instance.DeleteFood(item);
                            successDelete += "," + item;
                            // Hepler.SetImageToPictureBox(pictureFood, Hepler.ImageNameDefault);
                            //Hepler.DeleteFileImage(nameImageFood);
                        }
                        else
                        {
                            errorDelete += "," + item;
                        }
                    }
                    Hepler.ShowMessageDelete("mã", successDelete, errorDelete, "Vì đang có hóa đơn");
                    ResetFood();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetFood_Click(object sender, EventArgs e)
        {
            ResetFood();
        }
        private void dgvFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                DataGridViewRow row = dgvFood.CurrentRow;
                txtMaFood.Text = row.Cells[0].Value.ToString();
                txtTenMon.Text = row.Cells[1].Value.ToString();
                cbxLoaiMon.SelectedValue = row.Cells[2].Value;
                txtGia.Value = decimal.Parse(row.Cells[4].Value.ToString());
                txtDonViTinh.Text = row.Cells[5].Value.ToString();

                bool status = (bool)row.Cells[6].Value;
                if (status)
                {
                    rdShowFood.Checked = true;
                }
                else
                {
                    rdHideFood.Checked = true;
                }

                Food f = FoodDAO.Instance.GetFoodById((int)row.Cells[0].Value);
                string nameImageFood = (f.ImageFood).ConvertByteArrayToString();
                Hepler.SetImageToPictureBox(pictureFood, nameImageFood);

            }
        }

        private void txtSearchFood_TextChanged(object sender, EventArgs e)
        {
            LoadListFood(txtSearchFood.Text);
        }
        private void cbxSearchLoaiMon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if ((int)cb.SelectedValue == 0)
            {
                LoadListFood("");
                return;
            }
            dgvFood.DataSource = FoodDAO.Instance.GetListFood((int)cbxSearchLoaiMon.SelectedValue);
        }
        private void dgvFood_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvFood.Columns[e.ColumnIndex].Name.Equals("StatusFood"))
            {
                bool? status = e.Value as bool?;

                if (status == true)
                {
                    e.Value = "Hiển Thị";
                }
                else
                {
                    e.Value = "Ẩn";
                }
            }
        }
        #endregion 

        #region ===================== Event FooodCategory =======================
        private void btnAddFoodCategory_Click(object sender, EventArgs e)
        {
            if (CheckInputFoodCategory())
            {
                string nameFoodCate = txtTenFoodCategory.Text;
                if (!FoodCategoryDAO.Instance.GetCategoryByName(nameFoodCate))
                {
                    if (FoodCategoryDAO.Instance.InsertFoodCategory(nameFoodCate))
                    {
                        MessageBox.Show("Thêm danh mục món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFoodCategory();
                    }
                    else
                    {
                        MessageBox.Show("Thêm danh mục món thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtErrorNameFoodCategory.Text = "Tên danh mục này đã tồn tại";
                }
            }
        }

        private void btnEditFoodCategory_Click(object sender, EventArgs e)
        {
            string idFoodCate = txtMaFoodCategory.Text;

            if (idFoodCate != "")
            {
                if (CheckInputFoodCategory())
                {
                    if (FoodCategoryDAO.Instance.UpdateFoodCategory(Int32.Parse(idFoodCate), txtTenFoodCategory.Text))
                    {
                        MessageBox.Show("Cập nhật danh mục món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFoodCategory();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật danh mục món thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteFoodCategory_Click(object sender, EventArgs e)
        {
            List<int> lstIdFoodCate = Hepler.CheckDataGridViewCheckBox<int>(dtgvFoodCategory, 0, 2);
            if (lstIdFoodCate.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string successDelete = "";
                    string errorDelete = "";
                    foreach (int item in lstIdFoodCate)
                    {
                        if (FoodDAO.Instance.GetFoodByCategoryId(item).Count == 0)
                        {
                            FoodCategoryDAO.Instance.DeleteFoodCategory(item);
                            successDelete += "," + item;
                        }
                        else
                        {
                            errorDelete += "," + item;
                        }
                    }
                    Hepler.ShowMessageDelete("mã", successDelete, errorDelete, "Vì đang có món ăn");
                    ResetFoodCategory();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetFoodCategory_Click(object sender, EventArgs e)
        {
            ResetFoodCategory();
        }

        private void txtSearchFoodCategory_TextChanged(object sender, EventArgs e)
        {
            LoadListFoodCategory(txtSearchFoodCategory.Text);
        }
        private void dtgvFoodCategory_Click(object sender, EventArgs e)
        {
            if (dtgvFoodCategory.CurrentRow != null)
            {
                DataGridViewRow row = dtgvFoodCategory.CurrentRow;
                txtMaFoodCategory.Text = row.Cells[0].Value.ToString();
                txtTenFoodCategory.Text = row.Cells[1].Value.ToString();
            }
        }
        #endregion



        #region ===================== Event Account =======================
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (CheckInputAccount())
            {
                bool sex = true;
                if (rdNu.Checked)
                {
                    sex = false;
                }
                Account accInput = new Account(txtDisplayName.Text, txtUserName.Text, txtPassword.Text, txtPhone.Text, txtAddress.Text, sex, (int)cbTypeAccount.SelectedValue);

                if (AccountDAO.Instance.InsertAccount(accInput))
                {
                    MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetAccount();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            if (userName != "")
            {
                if (AccountDAO.Instance.GetAccountByUserName(userName) != null)
                {
                    if (CheckInputAccount(true))
                    {
                        bool sex = true;
                        if (rdNu.Checked)
                        {
                            sex = false;
                        }
                        Account accInput = new Account(txtDisplayName.Text, txtUserName.Text, txtPassword.Text, txtPhone.Text, txtAddress.Text, sex, (int)cbTypeAccount.SelectedValue);
                        if (AccountDAO.Instance.UpdateAccount(accInput))
                        {
                            MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetAccount();
                            if (updateAccount != null)
                            {
                                updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản này không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng hoặc nhập 'Tên đăng nhập' để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            List<string> lstIdAccount = Hepler.CheckDataGridViewCheckBox<string>(dtgvAccount, 1, 6);
            if (lstIdAccount.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string successDelete = "";
                    string errorDelete = "";
                    foreach (string item in lstIdAccount)
                    {
                        if (!AccountLogin.UserName.Equals(item))
                        {
                            AccountDAO.Instance.DeleteAccount(item);
                            successDelete += "," + item;
                        }
                        else
                        {
                            errorDelete += "," + item;
                        }
                    }
                    Hepler.ShowMessageDelete("tên đăng nhập", successDelete, errorDelete, "Vì đang đăng nhập");
                    ResetAccount();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetAccount_Click(object sender, EventArgs e)
        {
            ResetAccount();
        }

        private void txtSearchAccount_TextChanged(object sender, EventArgs e)
        {
            LoadDataAccount(txtSearchAccount.Text);
        }
        private void dtgvAccount_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvAccount.Columns[e.ColumnIndex].Name.Equals("ColumnAccountSex"))
            {
                bool? sex = e.Value as bool?;

                if (sex == true)
                {
                    e.Value = "Nam";
                }
                else if (sex == false)
                {
                    e.Value = "Nữ";
                }
                else
                {
                    e.Value = "Unknown";
                }
            }
            if (dtgvAccount.Columns[e.ColumnIndex].Name.Equals("ColumnAccountType"))
            {
                int? type = e.Value as int?;

                if (type == 0)
                {
                    e.Value = "Nhân Viên";
                }
                else if (type == 1)
                {
                    e.Value = "Admin";
                }
                else
                {
                    e.Value = "Unknown";
                }
            }
        }
        private void dtgvAccount_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.CurrentRow != null)
            {
                DataGridViewRow row = dtgvAccount.CurrentRow;
                txtDisplayName.Text = row.Cells[0].Value.ToString();
                txtUserName.Text = row.Cells[1].Value.ToString();
                txtPhone.Text = row.Cells[2].Value.ToString();
                txtAddress.Text = row.Cells[3].Value.ToString();
                bool sex = (bool)row.Cells[4].Value;
                if (sex)
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                cbTypeAccount.SelectedValue = (int)row.Cells[5].Value;
            }
        }

        #endregion

        #endregion
    }
}
