using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowflake_Form
{
    public partial class ShowObjects : Form
    {
        private SnowflakeDbConnection connection;
        private string selectedDatabaseName;
        private DataTable dataTable;

        public ShowObjects(SnowflakeDbConnection connection, List<string> selectedDatabaseNames)
        {
            InitializeComponent();
            this.connection = connection;
            databaseTreeView.AfterSelect += DatabaseTreeView_AfterSelect;
            detailedDataGridView.Visible = false;

            foreach (string dbName in selectedDatabaseNames)
            {
                TreeNode databaseNode = new TreeNode(dbName);
                databaseTreeView.Nodes.Add(databaseNode);
            }

            this.Controls.Add(databaseTreeView);
        }

        private void ShowObjects_Load(object sender, EventArgs e)
        {

        }

        private void DatabaseTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Update the class-level selectedDatabaseName with the value of the selected node
            selectedDatabaseName = e.Node.Text;

            // Execute the query to get the size of the selected individual database
            string individualSizeQuery = @"
                SELECT ROUND(SUM(bytes) / POWER(1024, 3), 2) AS individualSizeGB
                FROM " + selectedDatabaseName + @".information_schema.tables;";

            DataTable individualSizeDataTable = ExecuteSnowflakeQueryAndGetDataTable(individualSizeQuery, connection);

            // Display size of the selected individual database in GB, MB, KB, or Bytes based on the value
            if (individualSizeDataTable.Rows.Count > 0 && individualSizeDataTable.Rows[0]["individualSizeGB"] != DBNull.Value)
            {
                double individualSizeGB = Convert.ToDouble(individualSizeDataTable.Rows[0]["individualSizeGB"]);

                // Convert to MB if the size is less than 1GB
                if (individualSizeGB < 1)
                {
                    double individualSizeMB = individualSizeGB * 1024;

                    // Convert to KB if the size is less than 1MB
                    if (individualSizeMB < 1)
                    {
                        double individualSizeKB = individualSizeMB * 1024;

                        // Convert to Bytes if the size is less than 1KB
                        if (individualSizeKB < 1)
                        {
                            double individualSizeBytes = individualSizeKB * 1024;
                            DB_Size.Text = $"Size of {selectedDatabaseName}: {individualSizeBytes.ToString("0.00")} Bytes";
                        }
                        else
                        {
                            DB_Size.Text = $"Size of {selectedDatabaseName}: {individualSizeKB.ToString("0.00")} KB";
                        }
                    }
                    else
                    {
                        DB_Size.Text = $"Size of {selectedDatabaseName}: {individualSizeMB.ToString("0.00")} MB";
                    }
                }
                else
                {
                    DB_Size.Text = $"Size of {selectedDatabaseName}: {individualSizeGB.ToString("0.00")} GB";
                }
            }
            else
            {
                DB_Size.Text = $"Size of {selectedDatabaseName}: Not available";
            }


            // Execute the query to get the total size in bytes of all selected databases
            string totalSizeQuery = @"
                SELECT ROUND(SUM(bytes) / POWER(1024, 3), 2) AS totalSizeGB
                FROM (";

            // Iterate through selected database nodes
            foreach (TreeNode databaseNode in databaseTreeView.Nodes)
            {
                string dbName = databaseNode.Text;
                totalSizeQuery += @"
                    SELECT SUM(bytes) AS bytes
                    FROM " + dbName + @".information_schema.tables
                    UNION ALL";
            }

            // Remove the last "UNION ALL" and complete the query
            totalSizeQuery = totalSizeQuery.TrimEnd("UNION ALL".ToCharArray()) + ") AS allDatabases;";

            // Execute the total size query
            DataTable totalSizeDataTable = ExecuteSnowflakeQueryAndGetDataTable(totalSizeQuery, connection);

            // Display total size in GB, MB, KB, or Bytes based on the value
            if (totalSizeDataTable.Rows.Count > 0)
            {
                double totalSizeGB = Convert.ToDouble(totalSizeDataTable.Rows[0]["totalSizeGB"]);

                // Convert to MB if the size is less than 1GB
                if (totalSizeGB < 1)
                {
                    double totalSizeMB = totalSizeGB * 1024;

                    // Convert to KB if the size is less than 1MB
                    if (totalSizeMB < 1)
                    {
                        double totalSizeKB = totalSizeMB * 1024;

                        // Convert to Bytes if the size is less than 1KB
                        if (totalSizeKB < 1)
                        {
                            double totalSizeBytes = totalSizeKB * 1024;
                            Total_Size_DB.Text = "Total Size: " + totalSizeBytes.ToString("0.00") + " Bytes";
                        }
                        else
                        {
                            Total_Size_DB.Text = "Total Size: " + totalSizeKB.ToString("0.00") + " KB";
                        }
                    }
                    else
                    {
                        Total_Size_DB.Text = "Total Size: " + totalSizeMB.ToString("0.00") + " MB";
                    }
                }
                else
                {
                    Total_Size_DB.Text = "Total Size: " + totalSizeGB.ToString("0.00") + " GB";
                }
            }
            else
            {
                Total_Size_DB.Text = "Error retrieving total size information";
            }

            // Execute the detailed info query for the selected database
            string detailedQuery = @"
                SELECT 'Schemas' as type, COUNT(*) AS No_of_count
                FROM " + selectedDatabaseName + @".INFORMATION_SCHEMA.schemata
                UNION ALL
                SELECT 'Tables' as type, COUNT(*) AS No_of_count
                FROM " + selectedDatabaseName + @".INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE'
                UNION ALL
                SELECT 'Views' as type, COUNT(*) AS No_of_count
                FROM " + selectedDatabaseName + @".INFORMATION_SCHEMA.views
                WHERE TABLE_OWNER = ''
                UNION ALL
                SELECT 'Procedures' as type, COUNT(*) AS No_of_count
                FROM " + selectedDatabaseName + @".INFORMATION_SCHEMA.procedures;";

            DataTable detailedDataTable = ExecuteSnowflakeQueryAndGetDataTable(detailedQuery, connection);

            // Display detailed information
            detailedDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            detailedDataGridView.DataSource = detailedDataTable;
            detailedDataGridView.Visible = true;

            // Display DB size label
            DB_Size.Visible = true;
            Total_Size_DB.Visible = true;
        }







        // Define a method to execute Snowflake query and return DataTable
        private DataTable ExecuteSnowflakeQueryAndGetDataTable(string query, SnowflakeDbConnection connection)
        {
            DataTable dataTable = new DataTable();

            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        private void show_detailed_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Go_back_Click(object sender, EventArgs e)
        {

            // Hide the current form
            this.Hide();

        }

        private void Total_Size_DB_Click(object sender, EventArgs e)
        {
            //// Execute the query to get the total size of the selected database
            //string sizeQuery = @"
            //    SELECT table_catalog AS database,
            //           ROUND(SUM(bytes) / POWER(1024, 3), 2) AS gigabytes
            //    FROM " + selectedDatabaseName + @".information_schema.tables
            //    GROUP BY database;";

            //DataTable sizeDataTable = ExecuteSnowflakeQueryAndGetDataTable(sizeQuery, connection);

            //// Check if there are rows in the result
            //if (sizeDataTable.Rows.Count > 0)
            //{
            //    // Extract the total size value from the result
            //    double totalSizeGB = Convert.ToDouble(sizeDataTable.Rows[0]["gigabytes"]);

            //    // Update the Total_Size_DB label with the total size information
            //    Total_Size_DB.Text = "Total Size: " + totalSizeGB.ToString() + " GB";
            //}
            //else
            //{
            //    // If no rows are returned, display an error or default message
            //    Total_Size_DB.Text = "Error retrieving size information";
            //}
        }
    }
}