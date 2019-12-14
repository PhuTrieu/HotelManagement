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
        private static CData data;
        internal CData Data { get => data; set => data = value; }
        public FormManager()
        {
            InitializeComponent();
        }

        private void FormManager_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.data_image;
            data = new CData();
        }

        #region Hover
        private void btnPhong_MouseHover(object sender, EventArgs e)
        {            
            btnPhong.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnPhong_MouseLeave(object sender, EventArgs e)
        {
            btnPhong.BackColor = Color.Indigo;
        }

        private void btnKH_MouseHover(object sender, EventArgs e)
        {
            btnKH.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnKH_MouseLeave(object sender, EventArgs e)
        {           
            btnKH.BackColor = Color.Indigo;

        }

        private void btnDatPhong_MouseHover(object sender, EventArgs e)
        {
            btnDatPhong.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnDatPhong_MouseLeave(object sender, EventArgs e)
        {
            btnDatPhong.BackColor = Color.Indigo;
        }

        private void btnDichVu_MouseHover(object sender, EventArgs e)
        {
            btnDichVu.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnDichVu_MouseLeave(object sender, EventArgs e)
        {
            btnDichVu.BackColor = Color.Indigo;
        }

        private void btnDatDichVu_MouseHover(object sender, EventArgs e)
        {
            btnDatDichVu.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnDatDichVu_MouseLeave(object sender, EventArgs e)
        {
            btnDatDichVu.BackColor = Color.Indigo;
        }

        private void btnLichSu_MouseHover(object sender, EventArgs e)
        {
            btnLichSu.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnLichSu_MouseLeave(object sender, EventArgs e)
        {
            btnLichSu.BackColor = Color.Indigo;
        }

        private void btnNV_MouseHover(object sender, EventArgs e)
        {
            btnNV.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnNV_MouseLeave(object sender, EventArgs e)
        {
            btnNV.BackColor = Color.Indigo;
        }

        private void btnBill_MouseHover(object sender, EventArgs e)
        {
            btnBill.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnBill_MouseLeave(object sender, EventArgs e)
        {
            btnBill.BackColor = Color.Indigo;
        }
        private void btnThoat_MouseHover(object sender, EventArgs e)
        {
            btnThoat.BackColor = Color.FromArgb(153, 23, 100);
        }

        private void btnThoat_MouseLeave(object sender, EventArgs e)
        {
            btnThoat.BackColor = Color.Indigo;
        }
        #endregion

        private void btnPhong_Click(object sender, EventArgs e)
        {
            this.Hide();
            PhongKS frmPKS = new PhongKS();
            frmPKS.ShowDialog();
            this.Close();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            this.Hide();
            KhachHang frmKH = new KhachHang();
            frmKH.ShowDialog();
            this.Close();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            this.Hide();
            DatPhong frmDP = new DatPhong();
            frmDP.ShowDialog();
            this.Close();
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien frmNV = new NhanVien();
            frmNV.ShowDialog();
            this.Close();
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
            this.Close();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            this.Hide();
            DichVu frmDV = new DichVu();
            frmDV.ShowDialog();
            this.Close();
        }

        private void btnDatDichVu_Click(object sender, EventArgs e)
        {
            this.Hide();
            DatDichVu frmDDV = new DatDichVu();
            frmDDV.ShowDialog();
            this.Close();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoaDonThanhToan frmB = new HoaDonThanhToan();
            frmB.ShowDialog();
            this.Close();
        }
    }
}
