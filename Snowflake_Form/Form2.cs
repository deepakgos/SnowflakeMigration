using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowflake_Form
{
    public partial class Show_Snowflake_Objects : Form
    {
        private SnowflakeDbConnection connection;
        
        public Show_Snowflake_Objects(DataTable dataTable, SnowflakeDbConnection connection)
        {

            InitializeComponent();
            this.connection = connection;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }


            // Create a checkbox column
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Select";
            checkBoxColumn.Name = "checkBoxColumn";
            dataGridView1.Columns.Insert(0, checkBoxColumn);

            // Set the DataTable as the DataSource for the DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a checkbox
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                // Toggle the checkbox value or set it to true if it's currently null
                dataGridView1.Rows[e.RowIndex].Cells[0].Value =
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value == null
                        ? true
                        : !(bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            }
        }

        private void Show_Snowflake_Objects_Load(object sender, EventArgs e)
        {

        }


        private void Button_Generate_Script_Click(object sender, EventArgs e)
        {
            try
            {
                HashSet<string> uniqueDatabaseNames = new HashSet<string>();
                List<string> filePaths = new List<string>();

                // Iterate through selected rows in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                    {
                        // Extract the database name from the DataGridView
                        string databaseName = row.Cells["name"].Value.ToString();

                        // Check if the database name is not already processed
                        if (uniqueDatabaseNames.Add(databaseName))
                        {
                            // Execute the SELECT GET_DDL query for the current database
                            string query = $"SELECT GET_DDL('DATABASE', '{databaseName}') AS schema_ddl;";
                            string ddlScript = ExecuteGetDDLQuery(query, connection);

                            // Generate a unique filename based on the timestamp and database name
                            string fileName = $"{databaseName}_Script_{DateTime.Now:yyyyMMddHHmmss}.sql";

                            // Save the script to a text file with the database-specific filename
                            string filePath = SaveScriptToFile(ddlScript, fileName);

                            // Store the file path for later display
                            filePaths.Add(filePath);
                        }
                    }
                }

                // Display the file paths after all scripts are generated and saved
                if (filePaths.Count > 0)
                {
                    string message = $"Scripts generated and saved successfully. File paths:{Environment.NewLine}{string.Join(Environment.NewLine, filePaths)}";
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating scripts: {ex.Message}");
            }
        }

        private string SaveScriptToFile(string script, string fileName)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Snowflake_Scripts");

            // Check if the folder exists, and create it if it doesn't
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Combine the folder path with the file name
            string filePath = Path.Combine(folderPath, fileName);

            // Write the script to the file
            File.WriteAllText(filePath, script);

            return filePath;
        }


        private string ExecuteGetDDLQuery(string query, SnowflakeDbConnection connection)
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Assuming the result is in the first column
                        return reader.GetValue(0).ToString();
                    }
                }
            }

            return string.Empty;
        }

        private string SaveScriptToFile(string script, string fileName, string folderPath)
        {
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Snowflake_Scripts");

            // Check if the folder exists, and create it if it doesn't
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Combine the folder path with the file name
            string filePath = Path.Combine(folderPath, fileName);

            // Write the script to the file
            File.WriteAllText(filePath, script);

            return filePath;
        }

        private void Detailed_info_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> selectedDatabaseNames = GetSelectedDatabaseNames();
                if (selectedDatabaseNames.Count > 0)
                {
                    // Open form3 to execute and display detailed info for selected databases
                    ShowObjects form3 = new ShowObjects(connection,selectedDatabaseNames);
                    form3.Show();
                    //form3.ExecuteDetailedInfoQuery(selectedDatabaseNames);
                }
                else
                {
                    MessageBox.Show("No databases selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing detailed info query: {ex.Message}");
            }
        }


        private List<string> GetSelectedDatabaseNames()
        {
            List<string> selectedDatabaseNames = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                {
                    selectedDatabaseNames.Add(row.Cells["name"].Value.ToString());
                }
            }

            return selectedDatabaseNames;
        }

        private void searchBox_TextChanged_1(object sender, EventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            // Filter rows based on the entered text in the DataTable
            DataView dataView = ((DataTable)dataGridView1.DataSource).DefaultView;
            dataView.RowFilter = string.Empty;

            if (!string.IsNullOrEmpty(searchText))
            {
                StringBuilder filterExpression = new StringBuilder();

                for (int i = 0; i < dataView.Table.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        filterExpression.Append(" OR ");
                    }

                    filterExpression.Append($"CONVERT([{dataView.Table.Columns[i].ColumnName}], 'System.String') LIKE '%{searchText}%'");
                }

                dataView.RowFilter = filterExpression.ToString();
            }
        }

        private void selectAllCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            bool selectAll = ((CheckBox)sender).Checked;

            // Set the value of all checkboxes in the DataGridView based on "Select All"
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = selectAll;
            }
        }

        private void Snowflake_DDL_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> selectedDatabaseNames = GetSelectedDatabaseNames();

                if (selectedDatabaseNames.Count > 0)
                {
                    // Create a folder on the desktop to store the converted scripts
                    string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Converted_Scripts");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    List<string> successMessages = new List<string>();

                    // Iterate through selected databases and convert the scripts
                    foreach (string databaseName in selectedDatabaseNames)
                    {
                        // Generate Snowflake script for the current database
                        string scriptPath = GenerateScriptForDatabase(databaseName);

                        if (!string.IsNullOrEmpty(scriptPath))
                        {
                            // Generate a unique filename based on the timestamp and database name
                            string outputFileName = $"Converted_{databaseName}_Script_{DateTime.Now:yyyyMMddHHmmss}.sql";
                            string outputFilePath = Path.Combine(folderPath, outputFileName);

                            // Perform the conversion from Snowflake to Azure Synapse SQL
                            ConvertSnowflakeToSynapse(scriptPath, outputFilePath, databaseName);

                            // Add the success message to the list
                            successMessages.Add($"Script for '{databaseName}' converted and saved to {outputFilePath}");
                        }
                        else
                        {
                            MessageBox.Show($"Error generating script for '{databaseName}'.");
                        }
                    }

                    // Display a single message with all the success messages
                    MessageBox.Show(string.Join(Environment.NewLine, successMessages));
                }
                else
                {
                    MessageBox.Show("No databases selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting scripts: {ex.Message}");
            }
        }

        private string GenerateScriptForDatabase(string databaseName)
        {
            try
            {
                // Execute the SELECT GET_DDL query for the current database
                string query = $"SELECT GET_DDL('DATABASE', '{databaseName}') AS schema_ddl;";
                string ddlScript = ExecuteGetDDLQuery(query, connection);

                // Check if ddlScript is not empty before proceeding
                if (!string.IsNullOrEmpty(ddlScript))
                {
                    // Generate a unique filename based on the timestamp and database name
                    string fileName = $"{databaseName}_Script_{DateTime.Now:yyyyMMddHHmmss}.sql";

                    // Save the script to a text file with the database-specific filename
                    return SaveScriptToFile(ddlScript, fileName, "");
                }
                else
                {
                    MessageBox.Show($"Error: Script for '{databaseName}' is empty or null.");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating script for '{databaseName}': {ex.Message}");
                return string.Empty;
            }
        }

        private void ConvertSnowflakeToSynapse(string inputFilePath, string outputFilePath, string DataBase)
        {
            try
            {
                // Read the Snowflake SQL script from the input file
                string snowflakeSql;
                using (StreamReader snowflakeSqlFile = new StreamReader(inputFilePath))
                {
                    snowflakeSql = snowflakeSqlFile.ReadToEnd();
                }

                string azure_synapse_sql = snowflakeSql;
                azure_synapse_sql = azure_synapse_sql.Replace($"{DataBase}.", "");

                // Remove the full file format schema
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE FILE FORMAT MY_JSON_FORMAT.*?;", "", RegexOptions.Singleline);

                // Remove transient tables completely from the script
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE TRANSIENT TABLE.*?;", "", RegexOptions.Singleline);


                // Convert transient table to permanent table        
                //azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE TRANSIENT TABLE", "GO\nCREATE TABLE");

                // Replace Snowflake specific syntax with Azure Synapse specific syntax
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE TABLE", "GO\nCREATE TABLE");
                //azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE TRANSIENT TABLE (\w+)", "GO\nCREATE TABLE #\\1");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE DATABASE (\w+);", " ");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE SCHEMA (\w+);", "CREATE SCHEMA $1");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE VIEW", "GO\nCREATE VIEW");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE FUNCTION", "GO\nCREATE FUNCTION");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CREATE OR REPLACE PROCEDURE", "GO\nCREATE PROCEDURE");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"NUMBER\s*\(\s*(\d+)\s*,\s*(\d+)\s*\)", "NUMERIC($1,$2)");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"FLOAT", "REAL");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)CONSTRAINT \w+", " ");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)PRIMARY KEY\s*\(([^)]+)\)", " ");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)UNIQUE\s*\(([^)]+)\),", "");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)UNIQUE\s*\(([^)]+)\)", "");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"COMMENT\s*=\s*'([^']+)\'", "");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)OBJECT", "NVARCHAR");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)VARIANT", "NVARCHAR");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)ARRAY", "NVARCHAR");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)TIMESTAMP_NTZ", "DATETIME2");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)TIMESTAMP", "DATETIME2");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)TIMESTAMP_ITZ", "DATETIMEOFFSET");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)TIMESTAMP_TZ", "DATETIMEOFFSET");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)STRING", "VARCHAR");
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"(?i)TEXT", "VARCHAR");

                // Remove trailing comma after the last column
                azure_synapse_sql = Regex.Replace(azure_synapse_sql, @"\s*,\s*\);", "\n);");

                // Write the converted script to the output file
                using (StreamWriter synapseSqlFile = new StreamWriter(outputFilePath))
                {
                    synapseSqlFile.Write(azure_synapse_sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting scripts: {ex.Message}");
            }
        }
    }
}


