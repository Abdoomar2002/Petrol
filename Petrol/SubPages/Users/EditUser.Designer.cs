namespace Petrol.SubPages.Users
{
    partial class EditUser
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
            this.Header = new Guna.UI2.WinForms.Guna2Panel();
            this.BackBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.DeleteBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SaveBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RoleBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.RePasswordTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.UserNameTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NameTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FinanceNumTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.Header.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.Header.Size = new System.Drawing.Size(1310, 159);
            this.Header.TabIndex = 5;
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
            this.DeleteBtn.Location = new System.Drawing.Point(159, 55);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(153, 58);
            this.DeleteBtn.TabIndex = 12;
            this.DeleteBtn.Text = "مسح";
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
            this.SaveBtn.Location = new System.Drawing.Point(343, 55);
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
            this.label19.Location = new System.Drawing.Point(1143, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(158, 50);
            this.label19.TabIndex = 0;
            this.label19.Text = "تعديل بيانات";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.RoleBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.RePasswordTxt, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.PasswordTxt, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.UserNameTxt, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.NameTxt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FinanceNumTxt, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 159);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1310, 622);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // RoleBox
            // 
            this.RoleBox.BackColor = System.Drawing.Color.Transparent;
            this.RoleBox.BorderColor = System.Drawing.Color.Black;
            this.RoleBox.BorderRadius = 10;
            this.RoleBox.BorderThickness = 2;
            this.RoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RoleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoleBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleBox.Font = new System.Drawing.Font("Cairo Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.RoleBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.RoleBox.ItemHeight = 40;
            this.RoleBox.Items.AddRange(new object[] {
            "مستخدم",
            "ادمن"});
            this.RoleBox.Location = new System.Drawing.Point(5, 5);
            this.RoleBox.Margin = new System.Windows.Forms.Padding(5);
            this.RoleBox.Name = "RoleBox";
            this.RoleBox.Size = new System.Drawing.Size(445, 46);
            this.RoleBox.TabIndex = 66;
            // 
            // RePasswordTxt
            // 
            this.RePasswordTxt.BorderColor = System.Drawing.Color.Black;
            this.RePasswordTxt.BorderRadius = 10;
            this.RePasswordTxt.BorderThickness = 2;
            this.RePasswordTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RePasswordTxt.DefaultText = "";
            this.RePasswordTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.RePasswordTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.RePasswordTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.RePasswordTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.RePasswordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RePasswordTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RePasswordTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RePasswordTxt.ForeColor = System.Drawing.Color.Black;
            this.RePasswordTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RePasswordTxt.Location = new System.Drawing.Point(660, 405);
            this.RePasswordTxt.Margin = new System.Windows.Forms.Padding(5);
            this.RePasswordTxt.Name = "RePasswordTxt";
            this.RePasswordTxt.PlaceholderText = "";
            this.RePasswordTxt.SelectedText = "";
            this.RePasswordTxt.Size = new System.Drawing.Size(445, 90);
            this.RePasswordTxt.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1113, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 100);
            this.label6.TabIndex = 64;
            this.label6.Text = "تأكيد كلمة السر";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(458, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 100);
            this.label1.TabIndex = 56;
            this.label1.Text = "الدور";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.BorderColor = System.Drawing.Color.Black;
            this.PasswordTxt.BorderRadius = 10;
            this.PasswordTxt.BorderThickness = 2;
            this.PasswordTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTxt.DefaultText = "";
            this.PasswordTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTxt.ForeColor = System.Drawing.Color.Black;
            this.PasswordTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTxt.Location = new System.Drawing.Point(660, 305);
            this.PasswordTxt.Margin = new System.Windows.Forms.Padding(5);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.PlaceholderText = "";
            this.PasswordTxt.SelectedText = "";
            this.PasswordTxt.Size = new System.Drawing.Size(445, 90);
            this.PasswordTxt.TabIndex = 55;
            // 
            // UserNameTxt
            // 
            this.UserNameTxt.BorderColor = System.Drawing.Color.Black;
            this.UserNameTxt.BorderRadius = 10;
            this.UserNameTxt.BorderThickness = 2;
            this.UserNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UserNameTxt.DefaultText = "";
            this.UserNameTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.UserNameTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.UserNameTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UserNameTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UserNameTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserNameTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UserNameTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameTxt.ForeColor = System.Drawing.Color.Black;
            this.UserNameTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UserNameTxt.Location = new System.Drawing.Point(660, 205);
            this.UserNameTxt.Margin = new System.Windows.Forms.Padding(5);
            this.UserNameTxt.Name = "UserNameTxt";
            this.UserNameTxt.PlaceholderText = "";
            this.UserNameTxt.SelectedText = "";
            this.UserNameTxt.Size = new System.Drawing.Size(445, 90);
            this.UserNameTxt.TabIndex = 52;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1113, 300);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(194, 100);
            this.label17.TabIndex = 32;
            this.label17.Text = "كلمة السر";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1113, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(194, 100);
            this.label14.TabIndex = 26;
            this.label14.Text = "اسم المستخدم";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1113, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(194, 100);
            this.label13.TabIndex = 24;
            this.label13.Text = "الاسم";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.NameTxt.Location = new System.Drawing.Point(660, 105);
            this.NameTxt.Margin = new System.Windows.Forms.Padding(5);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.PlaceholderText = "";
            this.NameTxt.SelectedText = "";
            this.NameTxt.Size = new System.Drawing.Size(445, 90);
            this.NameTxt.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Cairo Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1113, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 100);
            this.label3.TabIndex = 4;
            this.label3.Text = "الرقم المالي";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FinanceNumTxt
            // 
            this.FinanceNumTxt.BorderColor = System.Drawing.Color.Black;
            this.FinanceNumTxt.BorderRadius = 10;
            this.FinanceNumTxt.BorderThickness = 2;
            this.FinanceNumTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FinanceNumTxt.DefaultText = "";
            this.FinanceNumTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.FinanceNumTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.FinanceNumTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FinanceNumTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FinanceNumTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinanceNumTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.FinanceNumTxt.Font = new System.Drawing.Font("Cairo Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinanceNumTxt.ForeColor = System.Drawing.Color.Black;
            this.FinanceNumTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.FinanceNumTxt.Location = new System.Drawing.Point(660, 5);
            this.FinanceNumTxt.Margin = new System.Windows.Forms.Padding(5);
            this.FinanceNumTxt.Name = "FinanceNumTxt";
            this.FinanceNumTxt.PlaceholderText = "";
            this.FinanceNumTxt.SelectedText = "";
            this.FinanceNumTxt.Size = new System.Drawing.Size(445, 90);
            this.FinanceNumTxt.TabIndex = 5;
            // 
            // EditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Header);
            this.Name = "EditUser";
            this.Size = new System.Drawing.Size(1310, 781);
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel Header;
        private Guna.UI2.WinForms.Guna2GradientButton BackBtn;
        private Guna.UI2.WinForms.Guna2GradientButton DeleteBtn;
        private Guna.UI2.WinForms.Guna2Button SaveBtn;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2ComboBox RoleBox;
        private Guna.UI2.WinForms.Guna2TextBox RePasswordTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTxt;
        private Guna.UI2.WinForms.Guna2TextBox UserNameTxt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private Guna.UI2.WinForms.Guna2TextBox NameTxt;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox FinanceNumTxt;
    }
}
