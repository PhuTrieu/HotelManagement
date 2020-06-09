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
    public partial class NhanVien : Form
    {
        FormManager frmmng = new FormManager();

        private List<CNhanVien> arrNV;
        private int i = -1;
        public NhanVien()
        {
            InitializeComponent();
        }
        public void hienthi()
        {
            lvwNV.Items.Clear();
            foreach (CNhanVien nv in arrNV)
            {
                ListViewItem li = lvwNV.Items.Add(nv.MaNV);
                li.SubItems.Add(nv.HoTen);
                if (nv.GioiTinh == true)
                {
                    li.SubItems.Add("Nam");
                }
                else li.SubItems.Add("Nu");
                li.SubItems.Add(nv.NgaySinh.ToShortDateString());
                li.SubItems.Add(nv.Sdt.ToString());
                li.SubItems.Add(nv.QueQuan);
                li.SubItems.Add(nv.ChucVu);
                li.SubItems.Add(nv.Luong);
            }
        }

        public void hienthiNV(int j)
        {
            CNhanVien nv = (CNhanVien)arrNV[j];
            txtMaNV.Text = nv.MaNV;
            txtTenNV.Text = nv.HoTen;
            chkGioiTinh.Checked = nv.GioiTinh;
            dtpNgaySinh.Value = nv.NgaySinh;
            txtSDT.Text = nv.Sdt.ToString();
            txtQueQuan.Text = nv.QueQuan;
            cbxChucVu.Text = nv.ChucVu;
            txtLuong.Text = nv.Luong;
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            arrNV = new List<CNhanVien>();
        }

        private void cbxChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChucVu.SelectedIndex == 0)
            {
                txtLuong.Text = "40.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 1)
            {
                txtLuong.Text = "20.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 2)
            {
                txtLuong.Text = "20.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 3)
            {
                txtLuong.Text = "20.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 4)
            {
                txtLuong.Text = "10.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 5)
            {
                txtLuong.Text = "7.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 6)
            {
                txtLuong.Text = "7.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 7)
            {
                txtLuong.Text = "8.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 8)
            {
                txtLuong.Text = "5.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 9)
            {
                txtLuong.Text = "5.000.000 VND";
            }
            else if (cbxChucVu.SelectedIndex == 10)
            {
                txtLuong.Text = "6.000.000 VND";
            }
        }

        private void NhanVien_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void lvwNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwNV.SelectedIndices)
            {
                i = j;
                hienthiNV(j);
                break;
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            CNhanVien nv = (CNhanVien)arrNV[i];
            nv.MaNV = txtMaNV.Text;
            nv.HoTen = txtTenNV.Text;
            nv.GioiTinh = chkGioiTinh.Checked;
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.Sdt = Convert.ToInt32(txtSDT.Text);
            nv.QueQuan = txtQueQuan.Text;
            nv.ChucVu = cbxChucVu.Text;
            nv.Luong = txtLuong.Text;
            hienthi();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            CNhanVien nv = new CNhanVien();
            nv.MaNV = txtMaNV.Text;
            nv.HoTen = txtTenNV.Text;
            nv.GioiTinh = chkGioiTinh.Checked;
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.Sdt = Convert.ToInt32(txtSDT.Text);
            nv.QueQuan = txtQueQuan.Text;
            nv.ChucVu = cbxChucVu.Text;
            nv.Luong = txtLuong.Text;
            arrNV.Add(nv);
            i++;
            hienthi();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            arrNV.RemoveAt(i);
            i--;
            if (i < 0 && arrNV.Count > 0)
            {
                i = 0;
            }
            if (i >= 0)
            {
                hienthiNV(i);
            }
            hienthi();
        }

        private void chkGioiTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGioiTinh.Checked == true)
            {
                chkGioiTinh.Text = "Nam";
            }
            else chkGioiTinh.Text = "Nu";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }
    }
}