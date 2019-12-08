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
        private int i = -1;
        int lcbBepT, lcbBep, lcbServe, lcbPhaChe, lcbLeTan, lcbHanhLy, lcbVeSinh, lcbKyThuat;

        public NhanVien()
        {
            InitializeComponent();
        }
        public void hienthi()
        {
            lvwNV.Items.Clear();
            foreach (CNhanVien nv in frmmng.Data.ArrNV)
            {
                ListViewItem li = lvwNV.Items.Add(nv.MaNV);
                li.SubItems.Add(nv.HoTen);
                li.SubItems.Add(nv.GioiTinh?"Nam":"Nu");
                li.SubItems.Add(nv.NgaySinh.ToShortDateString());
                li.SubItems.Add(nv.Sdt.ToString());
                li.SubItems.Add(nv.QueQuan);
                li.SubItems.Add(nv.ChucVu);
                li.SubItems.Add(nv.TongLuong().ToString());
            }
        }

        public void hienthiNV(int j)
        {
            CNhanVien nv = (CNhanVien)frmmng.Data.ArrNV[j];
            txtMaNV.Text = nv.MaNV;
            txtTenNV.Text = nv.HoTen;
            chkGioiTinh.Checked = nv.GioiTinh;
            dtpNgaySinh.Value = nv.NgaySinh;
            txtSDT.Text = nv.Sdt.ToString();
            txtQueQuan.Text = nv.QueQuan;
            cbxChucVu.Text = nv.ChucVu;
            txtLuongcb.Text = nv.Luongcb.ToString();
            txtSongaylam.Text = nv.Songaylam.ToString();
            txtTongLuong.Text = nv.TongLuong().ToString();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            frmmng.Data.OpenNV("dsnv.txt");
            if (frmmng.Data.ArrNV.Count > 0)
            {
                CNhanVien bt = TimNVTheoChucVu("Bếp trưởng");
                if (bt != null)     setupLuongcb(bt.ChucVu, bt.Luongcb);
                CNhanVien nvb = TimNVTheoChucVu("Nhân viên bếp");
                if (nvb != null)  setupLuongcb(nvb.ChucVu, nvb.Luongcb);
                CNhanVien nvs = TimNVTheoChucVu("Nhân viên phục vụ, tạp vụ");
                if (nvs != null)  setupLuongcb(nvs.ChucVu, nvs.Luongcb);
                CNhanVien nvpc = TimNVTheoChucVu("Nhân viên pha chế");
                if (nvpc != null)  setupLuongcb(nvpc.ChucVu, nvpc.Luongcb);
                CNhanVien nvlt = TimNVTheoChucVu("Nhân viên lễ tân");
                if (nvlt != null)  setupLuongcb(nvlt.ChucVu, nvlt.Luongcb);
                CNhanVien nvhl = TimNVTheoChucVu("Nhân viên hành lý");
                if (nvhl != null)  setupLuongcb(nvhl.ChucVu, nvhl.Luongcb);
                CNhanVien nvvs = TimNVTheoChucVu("Nhân viên vệ sinh");
                if (nvvs != null)  setupLuongcb(nvvs.ChucVu, nvvs.Luongcb);
                CNhanVien nvkt = TimNVTheoChucVu("Nhân viên kỹ thuật");
                if (nvkt != null)  setupLuongcb(nvkt.ChucVu, nvkt.Luongcb);
            }
            if (frmmng.Data.ArrNV.Count > 0)
                hienthi();
        }

        //Cách cũ ko sai nhưng khá nhiều hàm 
        #region Simple Tìm NV theo Chức Vụ
        private CNhanVien TimNVTheoChucVu(string chucvu)
        {
            CNhanVien nvtimthay = null;
            if (frmmng.Data.ArrNV.Count > 0)
            {
                foreach (CNhanVien nv in frmmng.Data.ArrNV)
                {
                    if (string.Compare(nv.ChucVu, chucvu) == 0)
                    {
                        nvtimthay = nv;
                        break;
                    }
                }
            }
            return nvtimthay;
        }
        #endregion

        public void setupLuongcb(string chucvu, int luongcb)
        {
            if (string.Compare(chucvu, "Bếp trưởng") == 0 && luongcb != lcbBepT)
                lcbBepT = luongcb;
            else if (string.Compare(chucvu, "Nhân viên bếp") == 0 && luongcb != lcbBep)
                lcbBep = luongcb;
            else if (string.Compare(chucvu, "Nhân viên phục vụ, tạp vụ") == 0 && luongcb != lcbServe)
                lcbServe = luongcb;
            else if (string.Compare(chucvu, "Nhân viên pha chế") == 0 && luongcb != lcbPhaChe)
                lcbPhaChe = luongcb;
            else if (string.Compare(chucvu, "Nhân viên lễ tân") == 0 && luongcb != lcbLeTan)
                lcbLeTan = luongcb;
            else if (string.Compare(chucvu, "Nhân viên hành lý") == 0 && luongcb != lcbHanhLy)
                lcbHanhLy = luongcb;
            else if (string.Compare(chucvu, "Nhân viên vệ sinh") == 0 && luongcb != lcbVeSinh)
                lcbVeSinh = luongcb;
            else if (string.Compare(chucvu, "Nhân viên kỹ thuật") == 0 && luongcb != lcbKyThuat)
                lcbKyThuat = luongcb;
        }

        public void syncLuongcb(string chucvu)
        {
            if (frmmng.Data.ArrNV.Count > 0)
            {
                foreach (CNhanVien nv in frmmng.Data.ArrNV)
                {
                    if (string.Compare(nv.ChucVu, chucvu) == 0)
                    {
                        if (string.Compare(chucvu, "Bếp trưởng") == 0 && lcbBepT != nv.Luongcb)
                            nv.Luongcb = lcbBepT;
                        else if (string.Compare(chucvu, "Nhân viên bếp") == 0 && lcbBep != nv.Luongcb)
                            nv.Luongcb = lcbBep;
                        else if (string.Compare(chucvu, "Nhân viên phục vụ, tạp vụ") == 0 && lcbServe != nv.Luongcb)
                            nv.Luongcb = lcbServe;
                        else if (string.Compare(chucvu, "Nhân viên pha chế") == 0 && lcbPhaChe != nv.Luongcb)
                            nv.Luongcb = lcbPhaChe;
                        else if (string.Compare(chucvu, "Nhân viên lễ tân") == 0 && lcbLeTan != nv.Luongcb)
                            nv.Luongcb = lcbLeTan;
                        else if (string.Compare(chucvu, "Nhân viên hành lý") == 0 && lcbHanhLy != nv.Luongcb)
                            nv.Luongcb = lcbHanhLy;
                        else if (string.Compare(chucvu, "Nhân viên vệ sinh") == 0 && lcbVeSinh != nv.Luongcb)
                            nv.Luongcb = lcbVeSinh;
                        else if (string.Compare(chucvu, "Nhân viên kỹ thuật") == 0 && lcbKyThuat != nv.Luongcb)
                            nv.Luongcb = lcbKyThuat;
                    }
                }
            }
        }
        private void cbxChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChucVu.SelectedIndex == 0)
            {
                try
                {
                    txtLuongcb.Text = lcbBepT.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 1)
            {
                try
                {
                    txtLuongcb.Text = lcbBep.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 2)
            {
                try
                {
                    txtLuongcb.Text = lcbServe.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 3)
            {
                try
                {
                    txtLuongcb.Text = lcbPhaChe.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 4)
            {
                try
                {
                    txtLuongcb.Text = lcbLeTan.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 5)
            {
                try
                {
                    txtLuongcb.Text = lcbHanhLy.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
            else if (cbxChucVu.SelectedIndex == 6)
            {
                try
                {
                    txtLuongcb.Text = lcbVeSinh.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0"; ;
                }
            }
            else if (cbxChucVu.SelectedIndex == 7)
            {
                try
                {
                    txtLuongcb.Text = lcbKyThuat.ToString();
                }
                catch (Exception)
                {
                    txtLuongcb.Text = "0";
                }
            }
        }

        //  Chưa biết cần ko do:
        //  Lúc ng ta nhập, sửa:
        //  ko tăng -> thôi 
        //  Tăng mà gặp hàm này -> ko tăng đc 
        //  vì nếu có ng thì lấy giá cũ, ko thì mới đc lấy giá mới
        #region Có nên Del ko: Tìm lương cb
        public int timLuongcb(string chucvu)
        {
            foreach (CNhanVien nv in frmmng.Data.ArrNV)
            {
                if (string.Compare(nv.ChucVu, chucvu) == 0)
                {
                    return nv.Luongcb;
                }
            }
            return Convert.ToInt32(txtLuongcb.Text);
        }
        #endregion


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
            CNhanVien nv = (CNhanVien)frmmng.Data.ArrNV[i];
            nv.MaNV = txtMaNV.Text;
            nv.HoTen = txtTenNV.Text;
            nv.GioiTinh = chkGioiTinh.Checked;
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.Sdt = Convert.ToInt32(txtSDT.Text);
            nv.QueQuan = txtQueQuan.Text;
            nv.ChucVu = cbxChucVu.Text;
            //nv.Luongcb = timLuongcb(nv.ChucVu);
            nv.Luongcb = Convert.ToInt32(txtLuongcb.Text);
            txtTongLuong.Text = nv.TongLuong().ToString();
            setupLuongcb(nv.ChucVu, nv.Luongcb);
            syncLuongcb(nv.ChucVu);
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
            //nv.Luongcb = timLuongcb(nv.ChucVu);
            nv.Luongcb = Convert.ToInt32(txtLuongcb.Text);
            nv.Songaylam = Convert.ToInt32(txtSongaylam.Text);

            frmmng.Data.ArrNV.Add(nv);
            txtTongLuong.Text = nv.TongLuong().ToString();
            i++;
            setupLuongcb(nv.ChucVu, nv.Luongcb);
            syncLuongcb(nv.ChucVu);
            hienthi();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            frmmng.Data.ArrNV.RemoveAt(i);
            i--;
            if (i < 0 && frmmng.Data.ArrNV.Count > 0)
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
            frmmng.Data.SaveNV("dsnv.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }
    }
}