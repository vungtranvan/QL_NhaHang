using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.View
{
    public partial class fHome : Form
    {
        public fHome()
        {
            InitializeComponent();
            ResetData();
        }

        #region Method
        private void LoadData()
        {
            dtgvBill.AutoGenerateColumns = false;
            dtgvBillInfo.AutoGenerateColumns = false;
            LoadCbxFoodCategory();
            LoadCbxFood((int)cbCategory.SelectedValue);
            LoadListBill();
            LoadImageFood();
        }
        private void LoadCbxFoodCategory()
        {
            cbCategory.DataSource = FoodCategoryDAO.Instance.GetCategory("");
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }
        private void LoadCbxFood(int idCate)
        {
            cbFood.DataSource = FoodDAO.Instance.GetFoodByCategoryId(idCate);
            cbFood.DisplayMember = "Name";
            cbFood.ValueMember = "Id";
        }
        private void LoadListBill()
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillUnCheckOut();
        }
        private void LoadBillInfo(int idBill)
        {
            dtgvBillInfo.DataSource = BillInfoDAO.Instance.GetListBillInfoByIdBill(idBill);
        }
        private void LoadImageFood()
        {
            if (cbFood.SelectedValue != null)
            {
                Food f = FoodDAO.Instance.GetFoodById((cbFood.SelectedItem as Food).Id);
                string nameImageFood = (f.ImageFood).ConvertByteArrayToString();
                Hepler.SetImageToPictureBox(pictureFood, nameImageFood);
                return;
            }

            Hepler.SetImageToPictureBox(pictureFood, Hepler.ImageNameDefault);
        }
        public void ResetData()
        {
            txtErrorTenBan.Text = "";
            txtErrorTenMon.Text = "";
            txtMaHoaDon.Text = "";
            txtTenBan.Text = "";
            txtThanhTien.Text = "0 ₫";
            txtTongTien.Text = "0 ₫";
            nmDiscount.Value = 0;
            nmQuantityFood.Value = 1;
            nmQuantityTable.Value = 1;
            LoadData();
            dtgvBillInfo.DataSource = null;
        }
        public bool CheckDataInsertBill()
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtTenBan, txtErrorTenBan, checkInput);
            if (cbFood.SelectedValue == null)
            {
                txtErrorTenMon.Text = "Trường này không được để trống";
                checkInput = false;
            }
            else
            {
                txtErrorTenMon.Text = "";
            }

            return checkInput;
        }
        public bool CheckDataEditBill()
        {
            bool checkInput = true;
            checkInput = Hepler.CheckInputNotNull(txtTenBan, txtErrorTenBan, checkInput);
            return checkInput;
        }
        #endregion


        #region Event
        private void dtgvBill_Click(object sender, EventArgs e)
        {
            if (dtgvBill.CurrentRow != null)
            {
                DataGridViewRow rowBill = dtgvBill.CurrentRow;
                int idBill = (int)rowBill.Cells[0].Value;
                txtMaHoaDon.Text = rowBill.Cells[0].Value.ToString();
                txtTenBan.Text = rowBill.Cells[1].Value.ToString();
                nmQuantityTable.Value = (int)rowBill.Cells[2].Value;
                LoadBillInfo(idBill);
                if (dtgvBillInfo.CurrentRow != null)
                {
                    float toTalPrice = 0;
                    for (int i = 0; i < dtgvBillInfo.Rows.Count; ++i)
                    {
                        toTalPrice += (float)Convert.ToDouble(dtgvBillInfo.Rows[i].Cells[5].Value);
                    }
                    float finalTotalPrice = toTalPrice - (toTalPrice * (int)nmDiscount.Value) / 100;
                    CultureInfo culture = new CultureInfo("vi-VN");
                    txtTongTien.Text = toTalPrice.ToString("c0", culture);
                    txtThanhTien.Text = finalTotalPrice.ToString("c0", culture);
                }
            }
        }
        private void dtgvBillInfo_Click(object sender, EventArgs e)
        {
            if (dtgvBillInfo.CurrentRow != null)
            {
                DataGridViewRow row = dtgvBillInfo.CurrentRow;
                int idBillInfo = (int)row.Cells[0].Value;
                Food f = FoodDAO.Instance.GetFoodById(BillInfoDAO.Instance.GetById(idBillInfo).IdFood);
                cbCategory.SelectedValue = f.IdCategory;
                cbFood.SelectedValue = f.Id;
            }
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;
            LoadCbxFood((cb.SelectedItem as FoodCategory).Id);
            LoadImageFood();
        }
        private void cbFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;
            LoadImageFood();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetData();
            dtgvBillInfo.DataSource = null;
        }
        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            List<int> lstIdBill = Hepler.CheckDataGridViewCheckBox<int>(dtgvBill, 0, 6);
            if (lstIdBill.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string successDelete = "";
                    string errorDelete = "";
                    foreach (int item in lstIdBill)
                    {
                        if (BillDAO.Instance.Delete(item))
                        {
                            successDelete += "," + item;
                        }
                        else
                        {
                            errorDelete += "," + item;
                        }

                    }
                    Hepler.ShowMessageDelete("mã", successDelete, errorDelete);
                    ResetData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (CheckDataInsertBill())
            {
                string tablename = txtTenBan.Text;
                int numberTable = (int)nmQuantityTable.Value;
                int idFood = (int)cbFood.SelectedValue;
                int count = (int)nmQuantityFood.Value;

                if (txtMaHoaDon.Text == "")
                {
                    BillDAO.Instance.Insert(tablename, numberTable);
                    BillInfoDAO.Instance.Insert(BillDAO.Instance.GetMaxBillId(), idFood, count);
                    // LoadBillInfo(BillDAO.Instance.GetMaxBillId());
                    MessageBox.Show("Thêm hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int idBill = Int32.Parse(txtMaHoaDon.Text);
                    BillInfoDAO.Instance.Insert(idBill, idFood, count);
                    // LoadBillInfo(idBill);
                    MessageBox.Show("Thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ResetData();
            }
        }
        private void btnEditBill_Click(object sender, EventArgs e)
        {
            string idBill = txtMaHoaDon.Text;

            if (idBill != "")
            {
                if (CheckDataEditBill())
                {
                    if (BillDAO.Instance.Update(Int32.Parse(idBill), txtTenBan.Text, (int)nmQuantityTable.Value))
                    {
                        ResetData();
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            string idHoaDon = txtMaHoaDon.Text;
            if (idHoaDon != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    float totalPrice = (float)Convert.ToDouble(txtThanhTien.Text.Split(' ')[0].Replace(".", ""));
                    if (BillDAO.Instance.CheckOut(Int32.Parse(idHoaDon), (int)nmDiscount.Value, totalPrice))
                    {
                        ResetData();
                        MessageBox.Show("Thanh toán thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        fReportToPrint f = new fReportToPrint();
                        f.ReportViewName = "BillDetail";
                        f.IdBill = Int32.Parse(idHoaDon);
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Thanh toán thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void nmDiscount_ValueChanged(object sender, EventArgs e)
        {
            float totalPrice = (float)Convert.ToDouble(txtTongTien.Text.Split(' ')[0].Replace(".", ""));
            float finalTotalPrice = totalPrice - (totalPrice * (int)nmDiscount.Value) / 100;
            CultureInfo culture = new CultureInfo("vi-VN");
            txtThanhTien.Text = finalTotalPrice.ToString("c0", culture);
        }

        private void dtgvBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvBill.Columns[e.ColumnIndex].Name.Equals("TrangThai"))
            {
                int? type = Int32.Parse(e.Value.ToString());

                if (type == 0)
                    e.Value = "Chưa Thanh Toán";
            }
        }
        #endregion
    }
}
