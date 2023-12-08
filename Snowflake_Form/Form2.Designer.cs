namespace Snowflake_Form
{
    partial class Show_Snowflake_Objects
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Button_Generate_Script = new System.Windows.Forms.Button();
            this.Detailed_info = new System.Windows.Forms.Button();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.Search_label = new System.Windows.Forms.Label();
            this.Snowflake_DDL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 55);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 48;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(776, 354);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Button_Generate_Script
            // 
            this.Button_Generate_Script.Location = new System.Drawing.Point(152, 416);
            this.Button_Generate_Script.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Generate_Script.Name = "Button_Generate_Script";
            this.Button_Generate_Script.Size = new System.Drawing.Size(205, 23);
            this.Button_Generate_Script.TabIndex = 1;
            this.Button_Generate_Script.Text = "Generate Script";
            this.Button_Generate_Script.UseVisualStyleBackColor = true;
            this.Button_Generate_Script.Click += new System.EventHandler(this.Button_Generate_Script_Click);
            // 
            // Detailed_info
            // 
            this.Detailed_info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Detailed_info.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Detailed_info.Location = new System.Drawing.Point(375, 416);
            this.Detailed_info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Detailed_info.Name = "Detailed_info";
            this.Detailed_info.Size = new System.Drawing.Size(169, 23);
            this.Detailed_info.TabIndex = 2;
            this.Detailed_info.Text = "See Detailed Information";
            this.Detailed_info.UseVisualStyleBackColor = true;
            this.Detailed_info.Click += new System.EventHandler(this.Detailed_info_Click);
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.AutoSize = true;
            this.selectAllCheckBox.Location = new System.Drawing.Point(13, 13);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(82, 20);
            this.selectAllCheckBox.TabIndex = 3;
            this.selectAllCheckBox.Text = "Select All";
            this.selectAllCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCheckBox_CheckedChanged_1);
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(176, 12);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(148, 22);
            this.SearchBox.TabIndex = 4;
            this.SearchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged_1);
            // 
            // Search_label
            // 
            this.Search_label.AutoSize = true;
            this.Search_label.Location = new System.Drawing.Point(330, 15);
            this.Search_label.Name = "Search_label";
            this.Search_label.Size = new System.Drawing.Size(50, 16);
            this.Search_label.TabIndex = 5;
            this.Search_label.Text = "Search";
            // 
            // Snowflake_DDL
            // 
            this.Snowflake_DDL.Location = new System.Drawing.Point(573, 416);
            this.Snowflake_DDL.Name = "Snowflake_DDL";
            this.Snowflake_DDL.Size = new System.Drawing.Size(215, 23);
            this.Snowflake_DDL.TabIndex = 6;
            this.Snowflake_DDL.Text = "Generate Script for Synapse";
            this.Snowflake_DDL.UseVisualStyleBackColor = true;
            this.Snowflake_DDL.Click += new System.EventHandler(this.Snowflake_DDL_Click);
            // 
            // Show_Snowflake_Objects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Snowflake_DDL);
            this.Controls.Add(this.Search_label);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.selectAllCheckBox);
            this.Controls.Add(this.Detailed_info);
            this.Controls.Add(this.Button_Generate_Script);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Show_Snowflake_Objects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Objects";
            this.Load += new System.EventHandler(this.Show_Snowflake_Objects_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Button_Generate_Script;
        private System.Windows.Forms.Button Detailed_info;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label Search_label;
        private System.Windows.Forms.Button Snowflake_DDL;
    }
}