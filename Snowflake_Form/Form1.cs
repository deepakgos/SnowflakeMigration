using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snowflake.Data;
using Snowflake.Data.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Snowflake_Form
{
    public partial class Snowflake_DataMigration : Form
    {

        private SnowflakeDbConnection connection;
        public Snowflake_DataMigration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Password_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Source_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Warehouse_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserName_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Source_label_Click(object sender, EventArgs e)
        {

        }

        private void Warehouse_label_Click(object sender, EventArgs e)
        {

        }

        private void UserName_label_Click(object sender, EventArgs e)
        {

        }

        private void Password_label_Click(object sender, EventArgs e)
        {

        }

        public void Connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SnowflakeDbConnection();
                connection.ConnectionString = GetConnectionString();
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Connection successful!");
                    ExecuteQuery();
                }
                else
                {
                    MessageBox.Show("Connection failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to Snowflake: {ex.Message}");
            }
            finally
            {
                //connection?.Close();
            }
        }

        private void ExecuteQuery()
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SHOW DATABASES;";

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.FieldCount == 0)
                    {
                        MessageBox.Show("No fields returned by the query.");
                        return;
                    }

                    DataTable dataTable = new DataTable();

                    // Create columns for the DataTable dynamically
                    dataTable.Load(reader);

                    // Filter rows based on the retention_time column
                    DataTable filteredDataTable = dataTable.AsEnumerable()
                       .Where(row => Convert.ToInt32(row["retention_time"]) != 0)
                       .CopyToDataTable();

                    if (filteredDataTable.Rows.Count > 0)
                    {
                        // Show only the desired columns (created_on, name, owner, kind) in form2
                        Show_Snowflake_Objects form2 = new Show_Snowflake_Objects(filteredDataTable, connection);
                        form2.Show();
                    }
                    else
                    {
                        MessageBox.Show("No rows with retention_time <> 0 returned by the query.");
                    }
                }
            }
        }


        private string GetConnectionString()
        {
            // Construct the Snowflake connection string based on user input
            //string connectionString = $"account={Source_textbox.Text};user={UserName_textbox.Text};password={Password_textbox.Text};warehouse={Warehouse_textbox.Text}";
            //string connectionString = "account=aw66369.ap-southeast-1;user=deepak;password=Pass@123;warehouse=WSNOW_TEST";
            string connectionString = "account=sn60464.central-india.azure;user=SNOWFLAKE1;password=1122OJASojas@@;warehouse=COMPUTE_WH";

            return connectionString;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
