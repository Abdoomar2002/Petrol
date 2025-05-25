using Petrol.Data;
using System.Windows.Forms;

namespace Petrol.SubPages.Departments
{
    partial class AddDepartment
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
        if (string.IsNullOrWhiteSpace(NameTxt.Text))
        {
            MessageBox.Show("Name is required.");
            return;
        }

        dbContext.SaveChanges();
        
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CodeTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NameTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.Header = new Guna.UI2.WinForms.Guna2Panel();
            this.BackBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.DeleteBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SaveBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CodeTxt, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NameTxt, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 159);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1364, 559);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // CodeTxt
            // 
            this.CodeTxt.BorderColor = System.Drawing.Color.Black;
            this.CodeTxt.BorderRadius = 10;
            this.CodeTxt.BorderThickness = 2;
            this.CodeTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CodeTxt.DefaultText = "";
            this.CodeTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.CodeTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CodeTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CodeTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CodeTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CodeTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeTxt.ForeColor = System.Drawing.Color.Black;
            this.CodeTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CodeTxt.Location = new System.Drawing.Point(5, 5);
            this.CodeTxt.Margin = new System.Windows.Forms.Padding(5);
            this.CodeTxt.Name = "CodeTxt";
            this.CodeTxt.PlaceholderText = "";
            this.CodeTxt.SelectedText = "";
            this.CodeTxt.Size = new System.Drawing.Size(472, 90);
            this.CodeTxt.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(485, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 100);
            this.label1.TabIndex = 56;
            this.label1.Text = "الكود";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1167, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 100);
            this.label3.TabIndex = 4;
            this.label3.Text = "الاسم";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NameTxt
            // 
            this.NameTxt.BorderColor = System.Drawing.Color.Black;
            this.NameTxt.BorderRadius = 10;
            this.NameTxt.BorderThickness = 2;
            this.NameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NameTxt.DefaultText = "";
            this.NameTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.NameTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.NameTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.NameTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.NameTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.NameTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTxt.ForeColor = System.Drawing.Color.Black;
            this.NameTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.NameTxt.Location = new System.Drawing.Point(687, 5);
            this.NameTxt.Margin = new System.Windows.Forms.Padding(5);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.PlaceholderText = "";
            this.NameTxt.SelectedText = "";
            this.NameTxt.Size = new System.Drawing.Size(472, 90);
            this.NameTxt.TabIndex = 5;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.White;
            this.Header.Controls.Add(this.BackBtn);
            this.Header.Controls.Add(this.DeleteBtn);
            this.Header.Controls.Add(this.SaveBtn);
            this.Header.Controls.Add(this.label19);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1364, 159);
            this.Header.TabIndex = 6;
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
            this.BackBtn.Location = new System.Drawing.Point(5, 55);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(123, 58);
            this.BackBtn.TabIndex = 13;
            this.BackBtn.Text = "رجوع";
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.BackColor = System.Drawing.Color.Transparent;
            this.DeleteBtn.BorderRadius = 8;
            this.DeleteBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DeleteBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DeleteBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DeleteBtn.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DeleteBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.DeleteBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteBtn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.DeleteBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteBtn.Location = new System.Drawing.Point(166, 55);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(153, 58);
            this.DeleteBtn.TabIndex = 12;
            this.DeleteBtn.Text = "تفريغ";
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BorderRadius = 10;
            this.SaveBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SaveBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SaveBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SaveBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SaveBtn.FillColor = System.Drawing.Color.Navy;
            this.SaveBtn.Font = new System.Drawing.Font("Cairo", 12F);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(357, 55);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(153, 58);
            this.SaveBtn.TabIndex = 2;
            this.SaveBtn.Text = "حفظ";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Cairo", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1111, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(231, 50);
            this.label19.TabIndex = 0;
            this.label19.Text = "أضافة ادارة جديده";
            // 
            // AddDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Header);
            this.Name = "AddDepartment";
            this.Size = new System.Drawing.Size(1364, 718);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TextBox CodeTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox NameTxt;
        private Guna.UI2.WinForms.Guna2Panel Header;
        private Guna.UI2.WinForms.Guna2GradientButton BackBtn;
        private Guna.UI2.WinForms.Guna2GradientButton DeleteBtn;
        private Guna.UI2.WinForms.Guna2Button SaveBtn;
        private System.Windows.Forms.Label label19;
        private AppDbContext dbContext;
    }
}
