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
using System.Web.UI.WebControls;
namespace FitnesCenter
{
    public partial class UyeEkle : Form
    {
        public UyeEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti =new SqlConnection(@"Data Source=GOKTUG\SQLEXPRESS;Initial Catalog=SporDb;Integrated Security=True;TrustServerCertificate=True");
        private void UyeEkle_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AdSoyadTb.Text == "" || TelefonTb.Text == "" || OdemeTb.Text == "" || YasTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "insert into UyeTbl values ('" + AdSoyadTb.Text + "','" + TelefonTb.Text + "','" + CinsiyetCb.SelectedItem.ToString() + "','" + YasTb.Text + "','" + OdemeTb.Text + "','" + ZamanlamaCb.SelectedItem.ToString() + "')";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    DialogResult dr = MessageBox.Show("Üye Kaydı Oluşturulacaktır Onaylıyor musunuz ?", "UYARI", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Üye Başarıyla Eklendi");
                        baglanti.Close();
                        AdSoyadTb.Text = "";
                        TelefonTb.Text = "";
                        OdemeTb.Text = "";
                        YasTb.Text = "";
                        CinsiyetCb.Text = "";
                        ZamanlamaCb.Text = "";
                    }
                    baglanti.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ex.Message"); baglanti.Close();
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdSoyadTb.Text = "";
            TelefonTb.Text = "";
            OdemeTb.Text = "";
            YasTb.Text = "";
            CinsiyetCb.Text = "";
            ZamanlamaCb.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
        AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
            
           


            
        }

        
        
           
        






    }
}

