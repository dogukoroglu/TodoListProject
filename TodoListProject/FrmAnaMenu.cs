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
    public partial class FrmAnaMenu : Form
    {
        public FrmAnaMenu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-GPMKKC5N\\SQLEXPRESS;Initial Catalog=DbTodolist;Integrated Security=True");
        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            Listele();
            Turler();
        }

        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * from TblTodos", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgwTodoList.DataSource = dt;
        }

        void Temizle()
        {
            rchTodos.Text = "";
            cmbKategori.Text = "";
            txtTodoid.Text = "";
        }

        void Turler()
        {
            SqlCommand komut = new SqlCommand("select * from TblKategori",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKategori.ValueMember = "KategoriId";
            cmbKategori.DisplayMember = "Kategori";
            cmbKategori.DataSource = dt;
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblTodos(TodoCategory,Todo) values (@p1,@p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbKategori.Text);
            komut.Parameters.AddWithValue("@p2", rchTodos.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Todo başarılı bir şekilde eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutSil = new SqlCommand("delete from TblTodos where Todoid=@s1", baglanti);
            komutSil.Parameters.AddWithValue("@s1", txtTodoid.Text);
            komutSil.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Todo başarılı bir şekilde silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutGuncelle = new SqlCommand("update TblTodos set TodoCategory=@g1,Todo=@g2 where Todoid=@g3",baglanti);
            komutGuncelle.Parameters.AddWithValue("@g1", cmbKategori.Text);
            komutGuncelle.Parameters.AddWithValue("@g2", rchTodos.Text);
            komutGuncelle.Parameters.AddWithValue("@g3", txtTodoid.Text);
            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Todo başarılı bir şekilde güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void dgwTodoList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTodoid.Text = dgwTodoList.Rows[e.RowIndex].Cells[0].Value.ToString();
            rchTodos.Text = dgwTodoList.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbKategori.Text = dgwTodoList.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
