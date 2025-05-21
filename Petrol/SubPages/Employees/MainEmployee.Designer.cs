namespace Petrol.SubPages.Employees
{
    partial class MainEmployee
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.AddEmployeeBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SearchBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SearchTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.EmployeesData = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesData)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.AddEmployeeBtn);
            this.guna2Panel1.Controls.Add(this.SearchBtn);
            this.guna2Panel1.Controls.Add(this.SearchTxt);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1255, 158);
            this.guna2Panel1.TabIndex = 1;
            // 
            // AddEmployeeBtn
            // 
            this.AddEmployeeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddEmployeeBtn.BorderRadius = 10;
            this.AddEmployeeBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AddEmployeeBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AddEmployeeBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AddEmployeeBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AddEmployeeBtn.FillColor = System.Drawing.Color.Navy;
            this.AddEmployeeBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.AddEmployeeBtn.ForeColor = System.Drawing.Color.White;
            this.AddEmployeeBtn.Location = new System.Drawing.Point(1033, 56);
            this.AddEmployeeBtn.Name = "AddEmployeeBtn";
            this.AddEmployeeBtn.Size = new System.Drawing.Size(207, 58);
            this.AddEmployeeBtn.TabIndex = 2;
            this.AddEmployeeBtn.Text = "أضافة جديد";
            this.AddEmployeeBtn.Click += new System.EventHandler(this.AddEmployeeBtn_Click);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SearchBtn.BorderRadius = 10;
            this.SearchBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SearchBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SearchBtn.FillColor = System.Drawing.Color.Navy;
            this.SearchBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.SearchBtn.ForeColor = System.Drawing.Color.White;
            this.SearchBtn.Location = new System.Drawing.Point(341, 56);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(153, 58);
            this.SearchBtn.TabIndex = 1;
            this.SearchBtn.Text = "بحث";
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // SearchTxt
            // 
            this.SearchTxt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SearchTxt.BackColor = System.Drawing.Color.Transparent;
            this.SearchTxt.BorderColor = System.Drawing.Color.Black;
            this.SearchTxt.BorderRadius = 8;
            this.SearchTxt.BorderThickness = 2;
            this.SearchTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchTxt.DefaultText = "";
            this.SearchTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SearchTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SearchTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchTxt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SearchTxt.ForeColor = System.Drawing.Color.Black;
            this.SearchTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchTxt.Location = new System.Drawing.Point(500, 56);
            this.SearchTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchTxt.Name = "SearchTxt";
            this.SearchTxt.PlaceholderText = "";
            this.SearchTxt.SelectedText = "";
            this.SearchTxt.Size = new System.Drawing.Size(316, 58);
            this.SearchTxt.TabIndex = 0;
            // 
            // EmployeesData
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeesData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.EmployeesData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmployeesData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cairo", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmployeesData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeesData.ColumnHeadersHeight = 48;
            this.EmployeesData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.EmployeesData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeesData.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmployeesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmployeesData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.EmployeesData.GridColor = System.Drawing.Color.White;
            this.EmployeesData.Location = new System.Drawing.Point(0, 158);
            this.EmployeesData.Name = "EmployeesData";
            this.EmployeesData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EmployeesData.RowHeadersVisible = false;
            this.EmployeesData.RowHeadersWidth = 51;
            this.EmployeesData.RowTemplate.Height = 24;
            this.EmployeesData.Size = new System.Drawing.Size(1255, 622);
            this.EmployeesData.TabIndex = 2;
            this.EmployeesData.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.EmployeesData.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.EmployeesData.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.EmployeesData.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.EmployeesData.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.EmployeesData.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.EmployeesData.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.EmployeesData.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.EmployeesData.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.EmployeesData.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeesData.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.EmployeesData.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.EmployeesData.ThemeStyle.HeaderStyle.Height = 48;
            this.EmployeesData.ThemeStyle.ReadOnly = false;
            this.EmployeesData.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.EmployeesData.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.EmployeesData.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeesData.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmployeesData.ThemeStyle.RowsStyle.Height = 24;
            this.EmployeesData.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.EmployeesData.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmployeesData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeesData_CellDoubleClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Id";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 32.08556F;
            this.Column6.HeaderText = "م";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 113.5829F;
            this.Column1.HeaderText = "الرقم المالي";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 113.5829F;
            this.Column2.HeaderText = "الاسم";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 113.5829F;
            this.Column3.HeaderText = "الادارة العامة";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 113.5829F;
            this.Column4.HeaderText = "الوظيفة الحالية";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.FillWeight = 113.5829F;
            this.Column5.HeaderText = "نوع الوظيفة";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // MainEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EmployeesData);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "MainEmployee";
            this.Size = new System.Drawing.Size(1255, 780);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button AddEmployeeBtn;
        private Guna.UI2.WinForms.Guna2Button SearchBtn;
        private Guna.UI2.WinForms.Guna2TextBox SearchTxt;
        private Guna.UI2.WinForms.Guna2DataGridView EmployeesData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}
