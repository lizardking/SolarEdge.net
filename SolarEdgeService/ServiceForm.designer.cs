namespace SolarEdgeService
{
    partial class ServiceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.StartAllButton = new System.Windows.Forms.Button();
            this.StopAllButton = new System.Windows.Forms.Button();
            this.AutoStartServicesCheckBox = new System.Windows.Forms.CheckBox();
            this.ServicesDataGridView = new System.Windows.Forms.DataGridView();
            this.ServiceNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCommandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCommandResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServicesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ServicesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // StartAllButton
            // 
            this.StartAllButton.Location = new System.Drawing.Point(12, 12);
            this.StartAllButton.Name = "StartAllButton";
            this.StartAllButton.Size = new System.Drawing.Size(112, 23);
            this.StartAllButton.TabIndex = 0;
            this.StartAllButton.Text = "Start All Services";
            this.StartAllButton.UseVisualStyleBackColor = true;
            this.StartAllButton.Click += new System.EventHandler(this.StartAllButton_Click);
            // 
            // StopAllButton
            // 
            this.StopAllButton.Location = new System.Drawing.Point(130, 12);
            this.StopAllButton.Name = "StopAllButton";
            this.StopAllButton.Size = new System.Drawing.Size(116, 23);
            this.StopAllButton.TabIndex = 1;
            this.StopAllButton.Text = "Stop All Services";
            this.StopAllButton.UseVisualStyleBackColor = true;
            this.StopAllButton.Click += new System.EventHandler(this.StopAllButton_Click);
            // 
            // AutoStartServicesCheckBox
            // 
            this.AutoStartServicesCheckBox.AutoSize = true;
            this.AutoStartServicesCheckBox.Location = new System.Drawing.Point(265, 16);
            this.AutoStartServicesCheckBox.Name = "AutoStartServicesCheckBox";
            this.AutoStartServicesCheckBox.Size = new System.Drawing.Size(188, 17);
            this.AutoStartServicesCheckBox.TabIndex = 2;
            this.AutoStartServicesCheckBox.Text = "Autostart all Services on next Start";
            this.AutoStartServicesCheckBox.UseVisualStyleBackColor = true;
            this.AutoStartServicesCheckBox.Click += new System.EventHandler(this.AutoStartServicesCheckBox_Click);
            // 
            // ServicesDataGridView
            // 
            this.ServicesDataGridView.AllowUserToAddRows = false;
            this.ServicesDataGridView.AllowUserToDeleteRows = false;
            this.ServicesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ServicesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ServicesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServicesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServiceNameColumn,
            this.LastCommandColumn,
            this.LastCommandResultColumn});
            this.ServicesDataGridView.EnableHeadersVisualStyles = false;
            this.ServicesDataGridView.Location = new System.Drawing.Point(12, 64);
            this.ServicesDataGridView.Name = "ServicesDataGridView";
            this.ServicesDataGridView.ReadOnly = true;
            this.ServicesDataGridView.RowHeadersVisible = false;
            this.ServicesDataGridView.Size = new System.Drawing.Size(700, 233);
            this.ServicesDataGridView.TabIndex = 3;
            // 
            // ServiceNameColumn
            // 
            this.ServiceNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ServiceNameColumn.DataPropertyName = "ServiceName";
            this.ServiceNameColumn.HeaderText = "Service Name";
            this.ServiceNameColumn.Name = "ServiceNameColumn";
            this.ServiceNameColumn.ReadOnly = true;
            // 
            // LastCommandColumn
            // 
            this.LastCommandColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastCommandColumn.DataPropertyName = "LastCommand";
            this.LastCommandColumn.HeaderText = "Last Command";
            this.LastCommandColumn.Name = "LastCommandColumn";
            this.LastCommandColumn.ReadOnly = true;
            // 
            // LastCommandResultColumn
            // 
            this.LastCommandResultColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastCommandResultColumn.DataPropertyName = "LastCommandResult";
            this.LastCommandResultColumn.HeaderText = "Last Command Result";
            this.LastCommandResultColumn.Name = "LastCommandResultColumn";
            this.LastCommandResultColumn.ReadOnly = true;
            // 
            // ServicesLabel
            // 
            this.ServicesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServicesLabel.AutoEllipsis = true;
            this.ServicesLabel.Location = new System.Drawing.Point(12, 47);
            this.ServicesLabel.Name = "ServicesLabel";
            this.ServicesLabel.Size = new System.Drawing.Size(700, 14);
            this.ServicesLabel.TabIndex = 4;
            this.ServicesLabel.Text = "<Not set>";
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 309);
            this.Controls.Add(this.ServicesLabel);
            this.Controls.Add(this.ServicesDataGridView);
            this.Controls.Add(this.AutoStartServicesCheckBox);
            this.Controls.Add(this.StopAllButton);
            this.Controls.Add(this.StartAllButton);
            this.MinimumSize = new System.Drawing.Size(477, 188);
            this.Name = "ServiceForm";
            this.Text = "Service Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServiceForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ServicesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartAllButton;
        private System.Windows.Forms.Button StopAllButton;
        private System.Windows.Forms.CheckBox AutoStartServicesCheckBox;
        private System.Windows.Forms.DataGridView ServicesDataGridView;
        private System.Windows.Forms.Label ServicesLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCommandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCommandResultColumn;
    }
}