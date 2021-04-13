using QL_NhaHang.DAO;
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
    public partial class fReport : Form
    {
        public fReport()
        {
            InitializeComponent();
            LoadDate();
            dtgvReport.AutoGenerateColumns = false;
            LoadDataBillByDate(dateFrom.Value, dateTo.Value);
        }
        #region Method
        private void LoadDate()
        {
            DateTime today = DateTime.Now;
            dateFrom.Value = new DateTime(today.Year, today.Month, 1);
            dateTo.Value = dateFrom.Value.AddMonths(1).AddDays(-1);
        }

        public void LoadDataBillByDate(DateTime checkin, DateTime checkout)
        {
            dtgvReport.DataSource = BillDAO.Instance.GetListBillByDate(checkin, checkout);
        }

        #endregion


        #region Event
        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadDataBillByDate(dateFrom.Value, dateTo.Value);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Hepler.ExportToExcel(dtgvReport);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtgvReport.CurrentRow != null)
            {
               // Hepler.ExportToPDF(dtgvReport);
                fReportToPrint f = new fReportToPrint();
                f.ReportViewName = "Bill";
                f.Checkin = dateFrom.Value;
                f.Checkout = dateTo.Value;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa có hóa đơn nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvReport.Columns[e.ColumnIndex].Name.Equals("TrangThai"))
            {
                int? type = Int32.Parse(e.Value.ToString());

                if (type == 1)
                    e.Value = "Đã Thanh Toán";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadDate();
            LoadDataBillByDate(dateFrom.Value, dateTo.Value);
        }
        private void btnViewBillInfo_Click(object sender, EventArgs e)
        {
            if (dtgvReport.CurrentRow != null)
            {
                int idBill = (int)dtgvReport.CurrentRow.Cells[0].Value;
                fReportToPrint f = new fReportToPrint();
                f.ReportViewName = "BillDetail";
                f.IdBill = idBill;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng xem hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

    }
}
