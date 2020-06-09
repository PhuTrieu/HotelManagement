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
    public partial class FormManager : Form
    {
        public FormManager()
        {
            InitializeComponent();
        }

        private void FormManager_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.data_image;
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            PhongKS frmPKS = new PhongKS();
            this.Hide();
            frmPKS.ShowDialog();
            this.Close();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            KhachHang frmKH = new KhachHang();
            this.Hide();
            frmKH.ShowDialog();
            this.Close();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            DatPhong frmDP = new DatPhong();
            this.Hide();
            frmDP.ShowDialog();
            this.Close();
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            NhanVien frmNV = new NhanVien();
            this.Hide();
            frmNV.ShowDialog();
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLSKH_Click(object sender, EventArgs e)
        {
            HistoryKH frmLSKH = new HistoryKH();
            this.Hide();
            frmLSKH.ShowDialog();
            this.Close();
        }
    }
}
