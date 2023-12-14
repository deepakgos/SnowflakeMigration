namespace Snowflake_Form
{
    partial class Recommendation
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
            this.totalChanges_label = new System.Windows.Forms.Label();
            this.totalChanges_num = new System.Windows.Forms.Label();
            this.recommendation_dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.recommendation_dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // totalChanges_label
            // 
            this.totalChanges_label.AutoSize = true;
            this.totalChanges_label.Location = new System.Drawing.Point(12, 25);
            this.totalChanges_label.Name = "totalChanges_label";
            this.totalChanges_label.Size = new System.Drawing.Size(154, 16);
            this.totalChanges_label.TabIndex = 0;
            this.totalChanges_label.Text = "Total Changes Required";
            this.totalChanges_label.Click += new System.EventHandler(this.totalChanges_label_Click);
            // 
            // totalChanges_num
            // 
            this.totalChanges_num.AutoSize = true;
            this.totalChanges_num.Location = new System.Drawing.Point(223, 25);
            this.totalChanges_num.Name = "totalChanges_num";
            this.totalChanges_num.Size = new System.Drawing.Size(44, 16);
            this.totalChanges_num.TabIndex = 1;
            this.totalChanges_num.Text = "label1";
            // 
            // recommendation_dataGridView1
            // 
            this.recommendation_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recommendation_dataGridView1.Location = new System.Drawing.Point(15, 91);
            this.recommendation_dataGridView1.Name = "recommendation_dataGridView1";
            this.recommendation_dataGridView1.RowHeadersWidth = 51;
            this.recommendation_dataGridView1.RowTemplate.Height = 24;
            this.recommendation_dataGridView1.Size = new System.Drawing.Size(773, 150);
            this.recommendation_dataGridView1.TabIndex = 2;
            this.recommendation_dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.recommendation_dataGridView1_CellContentClick);
            // 
            // Recommendation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.recommendation_dataGridView1);
            this.Controls.Add(this.totalChanges_num);
            this.Controls.Add(this.totalChanges_label);
            this.Name = "Recommendation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Recommendation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.recommendation_dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalChanges_label;
        private System.Windows.Forms.Label totalChanges_num;
        private System.Windows.Forms.DataGridView recommendation_dataGridView1;
    }
}