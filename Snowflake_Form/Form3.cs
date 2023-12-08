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

            // Execute the detailed info query for the selected database
                string query = @"
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

            DataTable dataTable = ExecuteSnowflakeQueryAndGetDataTable(query, connection);

            // Create a DataGridView to display detailed information
            detailedDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            detailedDataGridView.DataSource = dataTable;


            detailedDataGridView.Visible = true;



            // Add the DataGridView to the form
            this.Controls.Add(detailedDataGridView);
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

       
    }
}