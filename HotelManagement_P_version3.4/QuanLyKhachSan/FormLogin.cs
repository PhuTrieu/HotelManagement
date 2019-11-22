using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.data_image;
            txtPass.Focus();
        }

        public void dangnhap()
        {
            if (txtPass.Text.Length==0)
            {
                MessageBox.Show("Bạn chưa đăng nhập mật khẩu!");
            }
            else if (txtPass.Text!="1234")
            {
                txtPass.Text = "";
                MessageBox.Show("Mật khẩu không đúng!");
            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            FormManager frmmng = new FormManager();
            if (txtPass.Text == "1234")
            {
                this.Hide();
                frmmng.ShowDialog();
                this.Close();
            }
            else dangnhap();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDN.PerformClick();
        }
    }
}
