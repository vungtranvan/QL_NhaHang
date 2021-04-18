using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QL_NhaHang
{
    public static class Hepler
    {
        public const string pathFolderImage = @"..\..\Images\";
        public const string ImageNameDefault = "default_image.jpg";

        public static bool CheckInputNotNull(TextBox textInput, Label lbInputError, bool checkInput)
        {
            if (string.IsNullOrEmpty(textInput.Text.ToString()))
            {
                lbInputError.Text = "Trường này không được để trống";
                checkInput = false;
            }
            else
            {
                lbInputError.Text = "";
            }
            return checkInput;
        }

        public static string CheckPhoneNumber(string inputText)
        {
            var exp = new Regex("^((84|0)[3|5|7|8|9])+([0-9]{8})$", RegexOptions.IgnoreCase);

            var matchList = exp.Matches(inputText).Cast<Match>().Select(m => m.Groups[0].Value).ToArray();
            if (matchList.Length > 0)
            {
                return matchList[0];
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<T> CheckDataGridViewCheckBox<T>(DataGridView dtg, int indexCellAdded, int indexCellCheckBox)
        {
            List<T> termsList = new List<T>();
            foreach (DataGridViewRow row in dtg.Rows)
            {
                if (Convert.ToBoolean(row.Cells[indexCellCheckBox].Value))
                {
                    termsList.Add((T)row.Cells[indexCellAdded].Value);
                }
            }
            return termsList;
        }

        public static byte[] ConvertStringToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ConvertByteArrayToString(this byte[] data)
        {
            if (data != null)
            {
                var utf8 = new UTF8Encoding();
                return utf8.GetString(data);
            }
            return null;
        }

        public static byte[] GetFileByTagPicture(PictureBox pic)
        {
            string pathFileImage = (string)pic.Tag;
            string fileNameImage = Path.GetFileName(pathFileImage);

            if (!pathFileImage.Contains("QL_NhaHang\\QL_NhaHang\\Images\\"))
            {
                string[] lstImg = Path.GetFileName(pathFileImage).Split('.');
                string imgNameType = lstImg[lstImg.Count() - 1];

                fileNameImage = fileNameImage.Replace("." + imgNameType, "") + string.Format("-{0}.{1}", DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss"), imgNameType);

                File.Copy(pathFileImage, Path.Combine(pathFolderImage, fileNameImage), true);
            }

            byte[] image = fileNameImage.ConvertStringToByteArray();

            return image;
        }

        public static void SetImageToPictureBox(PictureBox pic, string nameImage)
        {
            string strPath = pathFolderImage;

            if (string.IsNullOrEmpty(nameImage))
            {
                strPath += ImageNameDefault;
            }
            else
            {
                strPath += nameImage;
            }

            pic.Image = System.Drawing.Image.FromFile(strPath);
            pic.Tag = Path.GetFullPath(strPath);
        }

        public static void DeleteFileImage(string nameImage)
        {
            if (!string.IsNullOrEmpty(nameImage) && !nameImage.Contains(ImageNameDefault))
            {
                string strPath = pathFolderImage + nameImage;

                if (File.Exists(strPath))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Delete(strPath);
                }
            }
        }

        public static void SetShowPass(TextBox txtText, PictureBox pic)
        {
            txtText.PasswordChar = '\0';
            pic.Image = Properties.Resources.eye_invisible_32px;
        }

        public static void SetHidePass(TextBox txtText, PictureBox pic)
        {
            txtText.PasswordChar = '*';
            pic.Image = Properties.Resources.eyeShow_32px;
        }

        public static void ShowMessageDelete(string successDelete, string errorDelete, string causeErro = null)
        {
            if (!string.IsNullOrEmpty(successDelete))
            {
                successDelete = ReplaceAtIndex(0, '<', successDelete);
                MessageBox.Show(string.Format($"Đã xóa {successDelete}> thành công"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!string.IsNullOrEmpty(errorDelete))
            {
                errorDelete = ReplaceAtIndex(0, '<', errorDelete);
                MessageBox.Show(string.Format($"Không thể xóa {errorDelete}>. {causeErro}"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static string ReplaceAtIndex(int i, char value, string word)
        {
            char[] letters = word.ToCharArray();
            letters[i] = value;
            return string.Join("", letters);
        }

        public static void ExportToExcel(DataGridView dtg)
        {
            Excel.Application app = new Excel.Application();
            Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "ReportExecl";

            for (int i = 1; i < dtg.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dtg.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dtg.Rows.Count; i++)
            {
                for (int j = 0; j < dtg.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dtg.Rows[i].Cells[j].Value.ToString();
                }
            }

            var savaFileDialoge = new SaveFileDialog();
            savaFileDialoge.Filter = "Excel Documents (*.xls)|*.xls";
            savaFileDialoge.FileName = "Export.xls";
            if (savaFileDialoge.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(savaFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            app.Quit();

            if (File.Exists(savaFileDialoge.FileName))
                System.Diagnostics.Process.Start(savaFileDialoge.FileName);
        }

        public static void ExportToPDF(DataGridView dtg)
        {
            if (dtg.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "ReportPDF.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        var arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
                        BaseFont bf = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dtg.Columns.Count);

                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.DefaultCell.BorderWidth = 1;

                            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                            foreach (DataGridViewColumn column in dtg.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dtg.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(cell.Value.ToString(), text)));
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            if (File.Exists(sfd.FileName))
                                System.Diagnostics.Process.Start(sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
        }

    }
}
