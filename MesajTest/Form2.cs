using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MesajTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string numara;

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PNOIT9G\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");

        void gelenKutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT (AD+' '+SOYAD) AS GONDEREN,BASLIK,ICERIK FROM TBLMESAJLAR INNER JOIN TBLKISILER ON TBLKISILER.NUMARA=TBLMESAJLAR.GONDEREN WHERE ALICI=" + numara, conn);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        void temizle()
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            richTextBox1.Text = "";
        }

        void gidenKutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT (AD+' '+SOYAD) AS GONDEREN,BASLIK,ICERIK FROM TBLMESAJLAR \r\nINNER JOIN TBLKISILER ON TBLKISILER.NUMARA=TBLMESAJLAR.GONDEREN WHERE GONDEREN=" + numara, conn);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;

            gelenKutusu();

            gidenKutusu();

            //Ad Soyad Çekme

            conn.Open();

            SqlCommand komut = new SqlCommand("select Ad,Soyad from TBLKISILER where NUMARA=" + numara, conn);

            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut2 = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", conn);
            komut2.Parameters.AddWithValue("@p1", lblNumara.Text);
            komut2.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut2.Parameters.AddWithValue("@p3", textBox1.Text);
            komut2.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Mesajınız Başarı İle Gönderildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gelenKutusu();
            gidenKutusu();
            temizle();
        }
    }
}
