
namespace QL_NhaHang.View
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MenuVetical = new System.Windows.Forms.Panel();
            this.ctMenuAdminDasboad = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnChangerInfoAcc = new System.Windows.Forms.ToolStripMenuItem();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.panelContentedor = new System.Windows.Forms.Panel();
            this.tLBtnHome = new System.Windows.Forms.ToolTip(this.components);
            this.tLBtnManager = new System.Windows.Forms.ToolTip(this.components);
            this.tLReport = new System.Windows.Forms.ToolTip(this.components);
            this.tLClose = new System.Windows.Forms.ToolTip(this.components);
            this.tLMinimize = new System.Windows.Forms.ToolTip(this.components);
            this.tLMaxmimize = new System.Windows.Forms.ToolTip(this.components);
            this.tLiconrestaurar = new System.Windows.Forms.ToolTip(this.components);
            this.iconMinimizar = new System.Windows.Forms.PictureBox();
            this.iconrestaurar = new System.Windows.Forms.PictureBox();
            this.iconMaximizar = new System.Windows.Forms.PictureBox();
            this.iconClose = new System.Windows.Forms.PictureBox();
            this.txtUserNameAdmin = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnAdminManager = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnLogo = new System.Windows.Forms.PictureBox();
            this.btnChangerPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuVetical.SuspendLayout();
            this.ctMenuAdminDasboad.SuspendLayout();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconrestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuVetical
            // 
            this.MenuVetical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.MenuVetical.Controls.Add(this.txtUserNameAdmin);
            this.MenuVetical.Controls.Add(this.btnReport);
            this.MenuVetical.Controls.Add(this.btnAdminManager);
            this.MenuVetical.Controls.Add(this.btnHome);
            this.MenuVetical.Controls.Add(this.btnLogo);
            this.MenuVetical.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuVetical.Location = new System.Drawing.Point(0, 0);
            this.MenuVetical.Name = "MenuVetical";
            this.MenuVetical.Size = new System.Drawing.Size(250, 650);
            this.MenuVetical.TabIndex = 0;
            // 
            // ctMenuAdminDasboad
            // 
            this.ctMenuAdminDasboad.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnChangerInfoAcc,
            this.btnChangerPassword,
            this.btnLogout});
            this.ctMenuAdminDasboad.Name = "ctMenuAdminDasboad";
            this.ctMenuAdminDasboad.Size = new System.Drawing.Size(146, 70);
            // 
            // btnChangerInfoAcc
            // 
            this.btnChangerInfoAcc.Image = global::QL_NhaHang.Properties.Resources.registration_32px;
            this.btnChangerInfoAcc.Name = "btnChangerInfoAcc";
            this.btnChangerInfoAcc.Size = new System.Drawing.Size(145, 22);
            this.btnChangerInfoAcc.Text = "Đổi thông tin";
            this.btnChangerInfoAcc.Click += new System.EventHandler(this.btnChangerInfoAcc_Click);
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.White;
            this.BarraTitulo.Controls.Add(this.iconMinimizar);
            this.BarraTitulo.Controls.Add(this.iconrestaurar);
            this.BarraTitulo.Controls.Add(this.iconMaximizar);
            this.BarraTitulo.Controls.Add(this.iconClose);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(250, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(1050, 50);
            this.BarraTitulo.TabIndex = 1;
            this.BarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraTitulo_MouseDown);
            // 
            // panelContentedor
            // 
            this.panelContentedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentedor.Location = new System.Drawing.Point(250, 50);
            this.panelContentedor.Name = "panelContentedor";
            this.panelContentedor.Size = new System.Drawing.Size(1050, 600);
            this.panelContentedor.TabIndex = 2;
            // 
            // iconMinimizar
            // 
            this.iconMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconMinimizar.Image = global::QL_NhaHang.Properties.Resources.minimize;
            this.iconMinimizar.Location = new System.Drawing.Point(966, 12);
            this.iconMinimizar.Name = "iconMinimizar";
            this.iconMinimizar.Size = new System.Drawing.Size(20, 20);
            this.iconMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconMinimizar.TabIndex = 1;
            this.iconMinimizar.TabStop = false;
            this.tLMinimize.SetToolTip(this.iconMinimizar, "Minimize");
            this.iconMinimizar.Click += new System.EventHandler(this.iconMinimizar_Click);
            // 
            // iconrestaurar
            // 
            this.iconrestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconrestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconrestaurar.Image = global::QL_NhaHang.Properties.Resources.unnamed;
            this.iconrestaurar.Location = new System.Drawing.Point(992, 12);
            this.iconrestaurar.Name = "iconrestaurar";
            this.iconrestaurar.Size = new System.Drawing.Size(20, 20);
            this.iconrestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconrestaurar.TabIndex = 1;
            this.iconrestaurar.TabStop = false;
            this.tLiconrestaurar.SetToolTip(this.iconrestaurar, "Restore Down");
            this.iconrestaurar.Click += new System.EventHandler(this.iconrestaurar_Click);
            // 
            // iconMaximizar
            // 
            this.iconMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconMaximizar.Image = global::QL_NhaHang.Properties.Resources.Maximize_55;
            this.iconMaximizar.Location = new System.Drawing.Point(992, 12);
            this.iconMaximizar.Name = "iconMaximizar";
            this.iconMaximizar.Size = new System.Drawing.Size(20, 20);
            this.iconMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconMaximizar.TabIndex = 1;
            this.iconMaximizar.TabStop = false;
            this.tLMaxmimize.SetToolTip(this.iconMaximizar, "Maximizar");
            this.iconMaximizar.Visible = false;
            this.iconMaximizar.Click += new System.EventHandler(this.iconMaximizar_Click);
            // 
            // iconClose
            // 
            this.iconClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconClose.Image = global::QL_NhaHang.Properties.Resources.icon_close;
            this.iconClose.Location = new System.Drawing.Point(1018, 12);
            this.iconClose.Name = "iconClose";
            this.iconClose.Size = new System.Drawing.Size(20, 20);
            this.iconClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconClose.TabIndex = 1;
            this.iconClose.TabStop = false;
            this.tLClose.SetToolTip(this.iconClose, "Close");
            this.iconClose.Click += new System.EventHandler(this.iconClose_Click);
            // 
            // txtUserNameAdmin
            // 
            this.txtUserNameAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtUserNameAdmin.ContextMenuStrip = this.ctMenuAdminDasboad;
            this.txtUserNameAdmin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.txtUserNameAdmin.FlatAppearance.BorderSize = 0;
            this.txtUserNameAdmin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.txtUserNameAdmin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtUserNameAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtUserNameAdmin.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserNameAdmin.ForeColor = System.Drawing.Color.White;
            this.txtUserNameAdmin.Image = global::QL_NhaHang.Properties.Resources.user_settings_32px;
            this.txtUserNameAdmin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUserNameAdmin.Location = new System.Drawing.Point(0, 608);
            this.txtUserNameAdmin.Name = "txtUserNameAdmin";
            this.txtUserNameAdmin.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.txtUserNameAdmin.Size = new System.Drawing.Size(250, 42);
            this.txtUserNameAdmin.TabIndex = 1;
            this.txtUserNameAdmin.TabStop = false;
            this.txtUserNameAdmin.Text = "Trần Văn Vững";
            this.txtUserNameAdmin.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Image = global::QL_NhaHang.Properties.Resources.combo_chart_32px;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(0, 211);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnReport.Size = new System.Drawing.Size(250, 42);
            this.btnReport.TabIndex = 1;
            this.btnReport.TabStop = false;
            this.btnReport.Text = "Thống Kê";
            this.tLReport.SetToolTip(this.btnReport, "Thống kê");
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnAdminManager
            // 
            this.btnAdminManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminManager.FlatAppearance.BorderSize = 0;
            this.btnAdminManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminManager.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminManager.ForeColor = System.Drawing.Color.White;
            this.btnAdminManager.Image = global::QL_NhaHang.Properties.Resources.management_32px;
            this.btnAdminManager.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminManager.Location = new System.Drawing.Point(0, 166);
            this.btnAdminManager.Name = "btnAdminManager";
            this.btnAdminManager.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAdminManager.Size = new System.Drawing.Size(250, 42);
            this.btnAdminManager.TabIndex = 1;
            this.btnAdminManager.TabStop = false;
            this.btnAdminManager.Text = "Quản Lý";
            this.tLBtnManager.SetToolTip(this.btnAdminManager, "Quản Lý");
            this.btnAdminManager.UseVisualStyleBackColor = true;
            this.btnAdminManager.Click += new System.EventHandler(this.btnAdminManager_Click);
            // 
            // btnHome
            // 
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = global::QL_NhaHang.Properties.Resources.small_business_32px;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(0, 120);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(250, 42);
            this.btnHome.TabIndex = 1;
            this.btnHome.TabStop = false;
            this.btnHome.Text = "Trang Chủ";
            this.tLBtnHome.SetToolTip(this.btnHome, "Trang Chủ");
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnLogo
            // 
            this.btnLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogo.Image = global::QL_NhaHang.Properties.Resources.logo_restaurant_leadership;
            this.btnLogo.Location = new System.Drawing.Point(3, 3);
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.Size = new System.Drawing.Size(245, 90);
            this.btnLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnLogo.TabIndex = 0;
            this.btnLogo.TabStop = false;
            this.btnLogo.Click += new System.EventHandler(this.btnLogo_Click);
            // 
            // btnChangerPassword
            // 
            this.btnChangerPassword.Image = global::QL_NhaHang.Properties.Resources.key_32px;
            this.btnChangerPassword.Name = "btnChangerPassword";
            this.btnChangerPassword.Size = new System.Drawing.Size(145, 22);
            this.btnChangerPassword.Text = "Đổi mật khẩu";
            this.btnChangerPassword.Click += new System.EventHandler(this.btnChangerPassword_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Image = global::QL_NhaHang.Properties.Resources.exit_black_32px;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(145, 22);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 650);
            this.Controls.Add(this.panelContentedor);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.MenuVetical);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fMain";
            this.MenuVetical.ResumeLayout(false);
            this.ctMenuAdminDasboad.ResumeLayout(false);
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconrestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuVetical;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.Panel panelContentedor;
        private System.Windows.Forms.PictureBox btnLogo;
        private System.Windows.Forms.PictureBox iconMinimizar;
        private System.Windows.Forms.PictureBox iconrestaurar;
        private System.Windows.Forms.PictureBox iconMaximizar;
        private System.Windows.Forms.PictureBox iconClose;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnAdminManager;
        private System.Windows.Forms.Button txtUserNameAdmin;
        private System.Windows.Forms.ToolTip tLBtnHome;
        private System.Windows.Forms.ToolTip tLBtnManager;
        private System.Windows.Forms.ToolTip tLReport;
        private System.Windows.Forms.ToolTip tLClose;
        private System.Windows.Forms.ToolTip tLMinimize;
        private System.Windows.Forms.ToolTip tLMaxmimize;
        private System.Windows.Forms.ToolTip tLiconrestaurar;
        private System.Windows.Forms.ContextMenuStrip ctMenuAdminDasboad;
        private System.Windows.Forms.ToolStripMenuItem btnChangerPassword;
        private System.Windows.Forms.ToolStripMenuItem btnLogout;
        private System.Windows.Forms.ToolStripMenuItem btnChangerInfoAcc;
    }
}