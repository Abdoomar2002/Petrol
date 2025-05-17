namespace Petrol.SubPages.Finances
{
    partial class MainFinances
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.EmployeeCostBtn = new Guna.UI2.WinForms.Guna2Button();
            this.ProgramCostBtn = new Guna.UI2.WinForms.Guna2Button();
            this.GeneralCostBtn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.tableLayoutPanel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1345, 158);
            this.guna2Panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.EmployeeCostBtn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ProgramCostBtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.GeneralCostBtn, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1345, 158);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // EmployeeCostBtn
            // 
            this.EmployeeCostBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EmployeeCostBtn.BorderRadius = 10;
            this.EmployeeCostBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.EmployeeCostBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.EmployeeCostBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.EmployeeCostBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.EmployeeCostBtn.FillColor = System.Drawing.Color.Navy;
            this.EmployeeCostBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.EmployeeCostBtn.ForeColor = System.Drawing.Color.White;
            this.EmployeeCostBtn.Location = new System.Drawing.Point(1024, 50);
            this.EmployeeCostBtn.Name = "EmployeeCostBtn";
            this.EmployeeCostBtn.Size = new System.Drawing.Size(192, 58);
            this.EmployeeCostBtn.TabIndex = 4;
            this.EmployeeCostBtn.Text = "تكلفة موظف";
            this.EmployeeCostBtn.Click += new System.EventHandler(this.EmployeeCostBtn_Click);
            // 
            // ProgramCostBtn
            // 
            this.ProgramCostBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProgramCostBtn.BorderRadius = 10;
            this.ProgramCostBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ProgramCostBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ProgramCostBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ProgramCostBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ProgramCostBtn.FillColor = System.Drawing.Color.Navy;
            this.ProgramCostBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.ProgramCostBtn.ForeColor = System.Drawing.Color.White;
            this.ProgramCostBtn.Location = new System.Drawing.Point(576, 50);
            this.ProgramCostBtn.Name = "ProgramCostBtn";
            this.ProgramCostBtn.Size = new System.Drawing.Size(192, 58);
            this.ProgramCostBtn.TabIndex = 3;
            this.ProgramCostBtn.Text = "تكلفة برنامج";
            this.ProgramCostBtn.Click += new System.EventHandler(this.ProgramCostBtn_Click);
            // 
            // GeneralCostBtn
            // 
            this.GeneralCostBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GeneralCostBtn.BorderRadius = 10;
            this.GeneralCostBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GeneralCostBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GeneralCostBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GeneralCostBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GeneralCostBtn.FillColor = System.Drawing.Color.Navy;
            this.GeneralCostBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.GeneralCostBtn.ForeColor = System.Drawing.Color.White;
            this.GeneralCostBtn.Location = new System.Drawing.Point(128, 50);
            this.GeneralCostBtn.Name = "GeneralCostBtn";
            this.GeneralCostBtn.Size = new System.Drawing.Size(192, 58);
            this.GeneralCostBtn.TabIndex = 2;
            this.GeneralCostBtn.Text = "تكلفة عامة";
            this.GeneralCostBtn.Click += new System.EventHandler(this.GeneralCostBtn_Click);
            // 
            // MainFinances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "MainFinances";
            this.Size = new System.Drawing.Size(1345, 694);
            this.guna2Panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button EmployeeCostBtn;
        private Guna.UI2.WinForms.Guna2Button ProgramCostBtn;
        private Guna.UI2.WinForms.Guna2Button GeneralCostBtn;
    }
}
