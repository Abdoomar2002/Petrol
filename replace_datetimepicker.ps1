# PowerShell script to replace DateTimePicker with TextBox controls
# This script will help automate the remaining replacements

$files = @(
    "Petrol/SubPages/Programs/EditTrainigData.Designer.cs",
    "Petrol/SubPages/Programs/AddTraining.Designer.cs",
    "Petrol/SubPages/Places/PlaceData.Designer.cs",
    "Petrol/SubPages/Employees/EmployeeData.Designer.cs",
    "Petrol/SubPages/Employees/EditEmployee.Designer.cs",
    "Petrol/SubPages/Finances/GeneralCost.Designer.cs",
    "Petrol/SubPages/Employees/AddProgramToEmployee.Designer.cs",
    "Petrol/SubPages/Employees/AddEmployee.Designer.cs"
)

foreach ($file in $files) {
    if (Test-Path $file) {
        Write-Host "Processing $file..."
        
        # Read the file content
        $content = Get-Content $file -Raw
        
        # Replace DateTimePicker declarations with TextBox
        $content = $content -replace 'private Guna\.UI2\.WinForms\.Guna2DateTimePicker (\w+);', 'private Guna.UI2.WinForms.Guna2TextBox $1;'
        
        # Replace DateTimePicker instantiations with TextBox
        $content = $content -replace 'this\.(\w+) = new Guna\.UI2\.WinForms\.Guna2DateTimePicker\(\);', 'this.$1 = new Guna.UI2.WinForms.Guna2TextBox();'
        
        # Replace DateTimePicker styling with TextBox styling
        $content = $content -replace '// (\w+)\s*//\s*\n\s*this\.(\w+)\.Anchor = \(\(System\.Windows\.Forms\.AnchorStyles\)\(\(System\.Windows\.Forms\.AnchorStyles\.Top \| System\.Windows\.Forms\.AnchorStyles\.Right\)\)\);\s*this\.\2\.Checked = true;\s*this\.\2\.FillColor = System\.Drawing\.Color\.White;\s*this\.\2\.Font = new System\.Drawing\.Font\("Cairo Medium", 10\.2F, System\.Drawing\.FontStyle\.Bold, System\.Drawing\.GraphicsUnit\.Point, \(\(byte\)\(0\)\)\);\s*this\.\2\.Format = System\.Windows\.Forms\.DateTimePickerFormat\.Long;\s*this\.\2\.Location = new System\.Drawing\.Point\(([^)]+)\);\s*this\.\2\.MaxDate = new System\.DateTime\(9998, 12, 31, 0, 0, 0, 0\);\s*this\.\2\.MinDate = new System\.DateTime\(1753, 1, 1, 0, 0, 0, 0\);\s*this\.\2\.Name = "\2";\s*this\.\2\.Size = new System\.Drawing\.Size\(([^)]+)\);\s*this\.\2\.TabIndex = ([^;]+);\s*this\.\2\.Value = new System\.DateTime\([^)]+\);', "// `$1`n            // `n            this.`$2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));`n            this.`$2.BackColor = System.Drawing.Color.Transparent;`n            this.`$2.BorderColor = System.Drawing.Color.Black;`n            this.`$2.BorderRadius = 8;`n            this.`$2.BorderThickness = 2;`n            this.`$2.Cursor = System.Windows.Forms.Cursors.IBeam;`n            this.`$2.DefaultText = """";`n            this.`$2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));`n            this.`$2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));`n            this.`$2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));`n            this.`$2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));`n            this.`$2.FillColor = System.Drawing.Color.White;`n            this.`$2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));`n            this.`$2.Font = new System.Drawing.Font(""Cairo Medium"", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));`n            this.`$2.ForeColor = System.Drawing.Color.Black;`n            this.`$2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));`n            this.`$2.Location = new System.Drawing.Point(`$3);`n            this.`$2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);`n            this.`$2.Name = ""`$2"";`n            this.`$2.PlaceholderText = ""dd/MM/yyyy"";`n            this.`$2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;`n            this.`$2.SelectedText = """";`n            this.`$2.Size = new System.Drawing.Size(`$4);`n            this.`$2.TabIndex = `$5;`n            this.`$2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;"
        
        # Write the modified content back to the file
        Set-Content $file $content -Encoding UTF8
        Write-Host "Completed $file"
    } else {
        Write-Host "File not found: $file"
    }
}

Write-Host "Script completed. Please manually verify the changes and update the corresponding .cs files." 