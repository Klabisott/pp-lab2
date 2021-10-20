using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pp_lab2_3
{
    public partial class ChooseTable : Form
    {
        public ChooseTable()
        {
            InitializeComponent();
        }
        private void ChooseTable_Load(object sender, EventArgs e)
        {
            tablesList.Items.Clear();
            foreach (DataTable t in this._PP_Lab_2__2_DataSet.Tables)
            {
                tablesList.Items.Add(t.TableName);
            }
            if (tablesList.Items.Count != 0) tablesList.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableView tv = new TableView(tablesList.SelectedItem.ToString());
            tv.Show();
        }
    }
}
