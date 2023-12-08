using System;
using System.Windows.Forms;

namespace Snowflake_Form
{
    partial class ShowObjects
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
            this.databaseTreeView = new System.Windows.Forms.TreeView();
            this.detailedDataGridView = new System.Windows.Forms.DataGridView();
            this.Go_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.detailedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // databaseTreeView
            // 
            this.databaseTreeView.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.databaseTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.databaseTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.databaseTreeView.Location = new System.Drawing.Point(0, 0);
            this.databaseTreeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.databaseTreeView.Name = "databaseTreeView";
            this.databaseTreeView.Size = new System.Drawing.Size(160, 450);
            this.databaseTreeView.TabIndex = 0;
            // 
            // detailedDataGridView
            // 
            this.detailedDataGridView.AllowUserToAddRows = false;
            this.detailedDataGridView.AllowUserToDeleteRows = false;
            this.detailedDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.detailedDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.detailedDataGridView.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.detailedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detailedDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.detailedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailedDataGridView.Enabled = false;
            this.detailedDataGridView.Location = new System.Drawing.Point(160, 0);
            this.detailedDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailedDataGridView.Name = "detailedDataGridView";
            this.detailedDataGridView.RowHeadersWidth = 48;
            this.detailedDataGridView.RowTemplate.Height = 24;
            this.detailedDataGridView.Size = new System.Drawing.Size(644, 242);
            this.detailedDataGridView.TabIndex = 1;
            this.detailedDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailedDataGridView_CellContentClick);
            // 
            // Go_back
            // 
            this.Go_back.BackColor = System.Drawing.Color.Navy;
            this.Go_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Go_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Go_back.ForeColor = System.Drawing.Color.White;
            this.Go_back.Location = new System.Drawing.Point(668, 410);
            this.Go_back.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Go_back.Name = "Go_back";
            this.Go_back.Size = new System.Drawing.Size(115, 28);
            this.Go_back.TabIndex = 2;
            this.Go_back.Text = "Go Back";
            this.Go_back.UseVisualStyleBackColor = false;
            this.Go_back.Click += new System.EventHandler(this.Go_back_Click);
            // 
            // ShowObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Go_back);
            this.Controls.Add(this.detailedDataGridView);
            this.Controls.Add(this.databaseTreeView);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ShowObjects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.ShowObjects_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detailedDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        private void detailedDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TreeView databaseTreeView;
        private System.Windows.Forms.DataGridView detailedDataGridView;
        private System.Windows.Forms.Button Go_back;
    }
}