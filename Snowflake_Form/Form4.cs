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
    public partial class Recommendation : Form
    {
        public Recommendation(int replacementCounter)
        {
            InitializeComponent();
            totalChanges_num.Text = replacementCounter.ToString();
        }

        private void Recommendation_Load(object sender, EventArgs e)
        {

        }

        private void totalChanges_label_Click(object sender, EventArgs e)
        {

        }

        private void recommendation_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
