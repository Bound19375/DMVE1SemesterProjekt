﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class Ejendomsmægler_OpretBolig : Form
    {
        public Ejendomsmægler_OpretBolig()
        {
            InitializeComponent();
            this.boligTilSalgTableAdapter.Fill(this.ejendomsmæglerDataSet.BoligTilSalg);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();

                MySqlConnection conn = new MySqlConnection(db.ConnStr);


                string sql = "INSERT INTO BoligTilSalg(BoligID, SælgerID, Pris, M2, PostNr, OprettelsesDato) " +
                             "VALUES(@BoligID, @SælgerID, @Pris, @M2, @PostNr, CURRENT_TIMESTAMP);";


                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@BoligID", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@SælgerID", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@Pris", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@M2", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@PostNr", textBox5.Text);

                conn.Open();
                cmd.ExecuteNonQuery();

                #region PassToGrid
                DataTable tbl = new DataTable();
                string sqlshow = "SELECT * FROM BoligTilSalg;";
                MySqlCommand cmd1 = new MySqlCommand(sqlshow, conn);
                cmd1.Parameters.AddWithValue("@Id1", int.Parse(textBox1.Text));
                tbl.Load(cmd1.ExecuteReader());
                dataGridView1.DataSource = tbl;
                MessageBox.Show("Done");
                conn.Close();
                #endregion PassToGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        private void Ejendomsmægler_OpretBolig_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ejendomsmæglerDataSet1.BoligTilSalg' table. You can move, or remove it, as needed.
            this.boligTilSalgTableAdapter1.Fill(this.ejendomsmæglerDataSet1.BoligTilSalg);

        }
    }
}
