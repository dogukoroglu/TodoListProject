using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TodoListProject
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-GPMKKC5N\\SQLEXPRESS;Initial Catalog=DbTodolist;Integrated Security=True");

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TblKullanici", baglanti);

            baglanti.Close();
        }

        private void btnGirisyap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGiris = new SqlCommand("select * from TblUsers where Username=@p1 and Password=@p2", baglanti);
            komutGiris.Parameters.AddWithValue("@p1", txtKullanicadi.Text);
            komutGiris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komutGiris.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaMenu fr = new FrmAnaMenu();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKullanicadi.Clear();
                txtSifre.Clear();
            }
            baglanti.Close();
        }
    }
}
