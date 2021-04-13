using QL_NhaHang.DAO;
using QL_NhaHang.DTO;
using QL_NhaHang.View.CrystalReportView;
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
    public partial class fReportToPrint : Form
    {
        public int IdBill { get; set; }
        public string ReportViewName { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

        public fReportToPrint()
        {
            InitializeComponent();
        }

        private void fReportToPrint_Load(object sender, EventArgs e)
        {
            DataTable data;
            switch (ReportViewName)
            {
                case "Bill":
                    data = BillDAO.Instance.GetListBillByDate(Checkin, Checkout);
                    CryReportBill crystalReportBill = new CryReportBill();
                    crystalReportBill.SetDataSource(data);
                    reportViewer.ReportSource = crystalReportBill;
                    reportViewer.Show();
                    break;
                case "BillDetail":
                    data = BillDAO.Instance.GetBillDetailByIdBill(IdBill);
                    CrystalReportBillInfoDetail cry = new CrystalReportBillInfoDetail();
                    cry.SetDataSource(data);
                    reportViewer.ReportSource = cry;
                    reportViewer.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
