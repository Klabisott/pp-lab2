using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pp_lab2_3
{
    public partial class TableView : Form
    {
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;
        string table;
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\PP Lab 2 (2).mdf"";Integrated Security=True";

        public TableView(string table)
        {
            InitializeComponent();

            this.table = table;

            dataGridView1.VirtualMode = false;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = true;

            GetData();

            foreach(DataColumn dc in sTable.Columns)
            {
                columns.Items.Add(dc.ColumnName);
            }
            columns.SelectedIndex = 0;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

        void GetData()
        {
            string sql = $"SELECT * FROM {table}";
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();
            sCommand = new SqlCommand(sql, connection);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            sAdapter.Fill(sDs, table);
            sTable = sDs.Tables[table];
            connection.Close();
            dataGridView1.DataSource = sDs.Tables[table];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sAdapter.Update(sDs, table);
                MessageBox.Show("Сохранено!");
            }
            catch(Exception)
            {
                MessageBox.Show("Произошла ошибка! Наведите на красный круг для получения информации, либо нажмите на 🔄 для возвращения таблицы в актуальный вид");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string colName = columns.SelectedItem.ToString();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле \"Значение\" не должно быть пустым!");
                return;
            }

            Result res = new Result(table, sTable.Columns[colName], textBox1.Text);
            try
            {
                res.Show();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void columns_SelectedIndexChanged(object sender, EventArgs e)
        {
            string colName = columns.SelectedItem.ToString();
            if (sTable.Columns[colName].DataType.Name == "DateTime")
                textBox1.Text = "ГОД/МЕСЯЦ/ДЕНЬ";
        }
    }
}
