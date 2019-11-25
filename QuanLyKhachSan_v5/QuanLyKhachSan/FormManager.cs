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
            this.Hide();
            PhongKS frmPKS = new PhongKS();
            frmPKS.ShowDialog();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            this.Hide();
            KhachHang frmKH = new KhachHang();
            frmKH.ShowDialog();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            this.Hide();
            DatPhong frmDP = new DatPhong();
            frmDP.ShowDialog();
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien frmNV = new NhanVien();
            frmNV.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            this.Hide();
            HistoryKH frmLSKH = new HistoryKH();
            frmLSKH.ShowDialog();
        }
    }
}
