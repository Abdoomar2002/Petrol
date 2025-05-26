namespace Petrol.SubPages.Reports
{
    partial class FinanceReport
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
            this.BackBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgramTypeBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.PrintBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.StartDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.EndDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.SearchBtn = new Guna.UI2.WinForms.Guna2Button();
            this.FinanceData = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinanceData)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.BackBtn);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.ProgramTypeBox);
            this.guna2Panel1.Controls.Add(this.PrintBtn);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.label19);
            this.guna2Panel1.Controls.Add(this.StartDate);
            this.guna2Panel1.Controls.Add(this.EndDate);
            this.guna2Panel1.Controls.Add(this.SearchBtn);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1369, 158);
            this.guna2Panel1.TabIndex = 8;
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.Transparent;
            this.BackBtn.BorderRadius = 8;
            this.BackBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BackBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BackBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BackBtn.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BackBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BackBtn.FillColor = System.Drawing.Color.Black;
            this.BackBtn.FillColor2 = System.Drawing.Color.Black;
            this.BackBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.BackBtn.ForeColor = System.Drawing.Color.White;
            this.BackBtn.Location = new System.Drawing.Point(12, 62);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(123, 58);
            this.BackBtn.TabIndex = 43;
            this.BackBtn.Text = "رجوع";
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(614, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 32);
            this.label2.TabIndex = 42;
            this.label2.Text = "نوع البرنامج";
            // 
            // ProgramTypeBox
            // 
            this.ProgramTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramTypeBox.BackColor = System.Drawing.Color.Transparent;
            this.ProgramTypeBox.BorderColor = System.Drawing.Color.Black;
            this.ProgramTypeBox.BorderRadius = 10;
            this.ProgramTypeBox.BorderThickness = 2;
            this.ProgramTypeBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ProgramTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProgramTypeBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ProgramTypeBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ProgramTypeBox.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.ProgramTypeBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.ProgramTypeBox.ItemHeight = 40;
            this.ProgramTypeBox.Items.AddRange(new object[] {
            "",
            "مركزي",
            "لا مركزي",
            "خارج البلاد"});
            this.ProgramTypeBox.Location = new System.Drawing.Point(460, 62);
            this.ProgramTypeBox.Margin = new System.Windows.Forms.Padding(5);
            this.ProgramTypeBox.Name = "ProgramTypeBox";
            this.ProgramTypeBox.Size = new System.Drawing.Size(146, 46);
            this.ProgramTypeBox.TabIndex = 41;
            // 
            // PrintBtn
            // 
            this.PrintBtn.BackColor = System.Drawing.Color.Transparent;
            this.PrintBtn.BorderRadius = 8;
            this.PrintBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PrintBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PrintBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PrintBtn.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PrintBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PrintBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PrintBtn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PrintBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.PrintBtn.ForeColor = System.Drawing.Color.White;
            this.PrintBtn.Location = new System.Drawing.Point(150, 62);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(123, 58);
            this.PrintBtn.TabIndex = 13;
            this.PrintBtn.Text = "طباعة";
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(997, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "الي";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1320, 62);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 32);
            this.label19.TabIndex = 5;
            this.label19.Text = "من";
            // 
            // StartDate
            // 
            this.StartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartDate.Checked = true;
            this.StartDate.FillColor = System.Drawing.Color.White;
            this.StartDate.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.StartDate.Location = new System.Drawing.Point(1062, 62);
            this.StartDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.StartDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(252, 36);
            this.StartDate.TabIndex = 3;
            this.StartDate.Value = new System.DateTime(2025, 5, 7, 18, 43, 58, 149);
            // 
            // EndDate
            // 
            this.EndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EndDate.Checked = true;
            this.EndDate.FillColor = System.Drawing.Color.White;
            this.EndDate.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.EndDate.Location = new System.Drawing.Point(727, 62);
            this.EndDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.EndDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(252, 36);
            this.EndDate.TabIndex = 2;
            this.EndDate.Value = new System.DateTime(2025, 5, 7, 18, 43, 58, 149);
            // 
            // SearchBtn
            // 
            this.SearchBtn.BorderRadius = 10;
            this.SearchBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SearchBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SearchBtn.FillColor = System.Drawing.Color.Navy;
            this.SearchBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.SearchBtn.ForeColor = System.Drawing.Color.White;
            this.SearchBtn.Location = new System.Drawing.Point(288, 62);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(153, 58);
            this.SearchBtn.TabIndex = 1;
            this.SearchBtn.Text = "بحث";
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // FinanceData
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.FinanceData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.FinanceData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.FinanceData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FinanceData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cairo", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FinanceData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.FinanceData.ColumnHeadersHeight = 100;
            this.FinanceData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.FinanceData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column5,
            this.Column4,
            this.Column7,
            this.Column8,
            this.Column1,
            this.Column3,
            this.Column2,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column16,
            this.Column13,
            this.Column14,
            this.Column15});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FinanceData.DefaultCellStyle = dataGridViewCellStyle3;
            this.FinanceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinanceData.GridColor = System.Drawing.Color.White;
            this.FinanceData.Location = new System.Drawing.Point(0, 158);
            this.FinanceData.Name = "FinanceData";
            this.FinanceData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FinanceData.RowHeadersVisible = false;
            this.FinanceData.RowHeadersWidth = 51;
            this.FinanceData.RowTemplate.Height = 24;
            this.FinanceData.Size = new System.Drawing.Size(1369, 524);
            this.FinanceData.TabIndex = 9;
            this.FinanceData.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.FinanceData.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.FinanceData.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.FinanceData.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.FinanceData.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.FinanceData.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.FinanceData.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.FinanceData.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.FinanceData.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.FinanceData.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinanceData.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.FinanceData.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.FinanceData.ThemeStyle.HeaderStyle.Height = 100;
            this.FinanceData.ThemeStyle.ReadOnly = false;
            this.FinanceData.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.FinanceData.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.FinanceData.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinanceData.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.FinanceData.ThemeStyle.RowsStyle.Height = 24;
            this.FinanceData.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.FinanceData.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column6
            // 
            this.Column6.FillWeight = 32.08556F;
            this.Column6.HeaderText = "م";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 28;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "اسم البرنامج";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 87;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "رسوم";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 87;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "بريك";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 87;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "أخرى";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 87;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 113.5829F;
            this.Column1.HeaderText = "اجمالي";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 98;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 113.5829F;
            this.Column3.HeaderText = "العدد";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 99;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 113.5829F;
            this.Column2.HeaderText = "ذكور";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 99;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "سيدات";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 87;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "التاريخ";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.Width = 87;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "نوع التدريب";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.Width = 86;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "الجهة المنفذه";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.Width = 87;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "المشرف";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.Width = 87;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "الاقامة";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.Width = 87;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "الملاحظات";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.Width = 87;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "اذون صرف";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.Width = 87;
            // 
            // FinanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FinanceData);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "FinanceReport";
            this.Size = new System.Drawing.Size(1369, 682);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinanceData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2GradientButton BackBtn;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox ProgramTypeBox;
        private Guna.UI2.WinForms.Guna2GradientButton PrintBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private Guna.UI2.WinForms.Guna2DateTimePicker StartDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker EndDate;
        private Guna.UI2.WinForms.Guna2Button SearchBtn;
        private Guna.UI2.WinForms.Guna2DataGridView FinanceData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
    }
}
