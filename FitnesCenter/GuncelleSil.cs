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
using System.Data.SqlClient;
namespace FitnesCenter
{
    public partial class GuncelleSil : Form
    {
        public GuncelleSil()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=GOKTUG\SQLEXPRESS;Initial Catalog=SporDb;Integrated Security=True;TrustServerCertificate=True");
        private void uyeler()
        {
            baglanti.Open();
            string query = "select *from UyeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            UyeDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
            
        private void GuncelleSil_Load(object sender, EventArgs e)
        {
            uyeler();
        }
        int key = 0;
        private void UyeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key=Convert.ToInt32(UyeDGV.SelectedRows[0].Cells[0].Value.ToString());
            AdSoyadTb.Text = UyeDGV.SelectedRows[0].Cells[1].Value.ToString();
            TelefonTb.Text = UyeDGV.SelectedRows[0].Cells[2].Value.ToString();
            CinsiyetCb.Text = UyeDGV.SelectedRows[0].Cells[3].Value.ToString();
            YasTb.Text = UyeDGV.SelectedRows[0].Cells[4].Value.ToString();
            OdemeTb.Text = UyeDGV.SelectedRows[0].Cells[5].Value.ToString();
            ZamanlamaCb.Text = UyeDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdSoyadTb.Text = "";
            TelefonTb.Text = "";
            CinsiyetCb.Text = "";
            YasTb.Text = "";
            OdemeTb.Text = "";
            ZamanlamaCb.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(key==0)
            {
                MessageBox.Show("Silinecek Üyeyi seçiniz");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "delete from UyeTbl where UId=" + key + ";";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    DialogResult dr = MessageBox.Show("Üye Kaydı Silinecektir Onaylıyor Musunuz ? ", "UYARI", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Üye Başarıyla Silindi");
                        baglanti.Close();
                        uyeler();
                    }
                    baglanti.Close();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message); baglanti.Close();
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (key == 0 || AdSoyadTb.Text == "" || TelefonTb.Text == "" || CinsiyetCb.Text == "" || YasTb.Text == "" || OdemeTb.Text == "" || ZamanlamaCb.Text == "")
            {
                MessageBox.Show("Eksik bilgi");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "UPDATE UyeTbl SET UAdSoyad = @AdSoyad, UTelefon = @Telefon, UCinsiyet = @Cinsiyet, UYas = @Yas, UOdeme = @Odeme, UZamanlama = @Zamanlama WHERE UId = @Id;";
                    SqlCommand komut = new SqlCommand(query, baglanti);

                    
                    komut.Parameters.AddWithValue("@AdSoyad", AdSoyadTb.Text);
                    komut.Parameters.AddWithValue("@Telefon", TelefonTb.Text);
                    komut.Parameters.AddWithValue("@Cinsiyet", CinsiyetCb.Text);
                    komut.Parameters.AddWithValue("@Yas", YasTb.Text);
                    komut.Parameters.AddWithValue("@Odeme", OdemeTb.Text);
                    komut.Parameters.AddWithValue("@Zamanlama", ZamanlamaCb.Text);
                    komut.Parameters.AddWithValue("@Id", key);

                    DialogResult dr = MessageBox.Show("Müşteri Güncellensin mi ?", "UYARI", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Üye Başarıyla Güncellendi");

                    }
                    else
                    {
                        MessageBox.Show("Müşteri Güncellemesi İptal Edildi.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        baglanti.Close();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message); baglanti.Close();
                }
                finally
                {
                    baglanti.Close();
                    uyeler();
                }
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
