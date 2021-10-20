using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pp_lab2_3
{
    public partial class Result : Form
    {
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        DataSet sDs;

        public Result(string table, DataColumn column, string val)
        {
            InitializeComponent();

            try
            {
                dataGridView1.VirtualMode = false;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;

                Text = $"Результат - таблица:{table}, столбец:{column.ColumnName}, значение:{val}";

                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\PP Lab 2 (2).mdf"";Integrated Security=True";
                string sql = $"SELECT * FROM {table} WHERE {column.ColumnName} = @val";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                sCommand = new SqlCommand(sql, connection);
                SqlParameter value = new SqlParameter("@val", column.DataType)
                {
                    SqlValue = val
                };
                sCommand.Parameters.Add(value);

                sAdapter = new SqlDataAdapter(sCommand);
                sDs = new DataSet();
                sAdapter.Fill(sDs, table);
                connection.Close();

                dataGridView1.DataSource = sDs.Tables[table];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Произошла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void Result_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_PP_Lab_2__2_DataSet.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter.Fill(this._PP_Lab_2__2_DataSet.Cars);

        }
    }
}
