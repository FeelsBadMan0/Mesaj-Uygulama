using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MesajTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PNOIT9G\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where NUMARA=@p1 and SIFRE=@p2", conn);
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                Form2 fr2 = new Form2();
                fr2.numara = maskedTextBox1.Text;
                fr2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi");
            }

            conn.Close();

        }

    }
}
