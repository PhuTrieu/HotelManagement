using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        int lcbBepT, lcbBep, lcbServe, lcbPhaChe, lcbLeTan, lcbHanhLy, lcbVeSinh, lcbKyThuat;
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
                else li.SubItems.Add("Nữ");
                li.SubItems.Add(nv.NgaySinh.ToShortDateString());
                li.SubItems.Add(nv.Sdt.ToString());
                li.SubItems.Add(nv.QueQuan);
                li.SubItems.Add(nv.ChucVu);
                li.SubItems.Add(nv.Luongcb.ToString());
                li.SubItems.Add(nv.Songaylam.ToString());
                li.SubItems.Add(nv.TongLuong().ToString());
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
            txtLuongcb.Text = nv.Luongcb.ToString();
            txtSongaylam.Text = nv.Songaylam.ToString();
            txtTongLuong.Text = nv.TongLuong().ToString();
        }

        public void OpenNV(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrNV = (List<CNhanVien>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có dữ liệu nhân viên", "Error");
            }
        }

        public void SaveNV(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrNV);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không lưu được", "Error");
            }
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            arrNV = new List<CNhanVien>();
            OpenNV("dsnv.txt");
            if (arrNV.Count>0)
            {
                CNhanVien bt = TimBepT();
                setupLuongcb(bt.ChucVu, bt.Luongcb);
                CNhanVien nvb = TimNVBep();
                setupLuongcb(nvb.ChucVu, nvb.Luongcb);
                CNhanVien nvs = TimNVServe();
                setupLuongcb(nvs.ChucVu, nvs.Luongcb);
                CNhanVien nvpc = TimNVPhaChe();
                setupLuongcb(nvpc.ChucVu, nvpc.Luongcb);
                CNhanVien nvlt = TimNVLeTan();
                setupLuongcb(nvlt.ChucVu, nvlt.Luongcb);
                CNhanVien nvhl = TimNVHanhLy();
                setupLuongcb(nvhl.ChucVu, nvhl.Luongcb);
                CNhanVien nvvs = TimNVVeSinh();
                setupLuongcb(nvvs.ChucVu, nvvs.Luongcb);
                CNhanVien nvkt = TimNVKyThuat();
                setupLuongcb(nvkt.ChucVu, nvkt.Luongcb);
            }
        }

        private CNhanVien TimBepT()
        {
            CNhanVien bt = null;
            if (arrNV.Count>0)
            {
                foreach (CNhanVien nv  in arrNV)
                {
                    if (string.Compare(nv.ChucVu,"Bếp trưởng")==0)
                    {
                        bt = nv;
                        break;
                    }
                }
            }
            return bt;
        }
        private CNhanVien TimNVBep()
        {
            CNhanVien nvb = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên bếp") == 0)
                    {
                        nvb = nv;
                        break;
                    }
                }
            }
            return nvb;
        }
        private CNhanVien TimNVServe()
        {
            CNhanVien nvs = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên phục vụ, tạp vụ") == 0)
                    {
                        nvs = nv;
                        break;
                    }
                }
            }
            return nvs;
        }
        private CNhanVien TimNVPhaChe()
        {
            CNhanVien nvpc = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên pha chế") == 0)
                    {
                        nvpc = nv;
                        break;
                    }
                }
            }
            return nvpc;
        }
        private CNhanVien TimNVLeTan()
        {
            CNhanVien nvlt = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên lễ tân") == 0)
                    {
                        nvlt = nv;
                        break;
                    }
                }
            }
            return nvlt;
        }
        private CNhanVien TimNVHanhLy()
        {
            CNhanVien nvhl = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên hành lý") == 0)
                    {
                        nvhl = nv;
                        break;
                    }
                }
            }
            return nvhl;
        }
        private CNhanVien TimNVVeSinh()
        {
            CNhanVien nvvs = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên vệ sinh") == 0)
                    {
                        nvvs = nv;
                        break;
                    }
                }
            }
            return nvvs;
        }
        private CNhanVien TimNVKyThuat()
        {
            CNhanVien nvkt = null;
            if (arrNV.Count > 0)
            {
                foreach (CNhanVien nv in arrNV)
                {
                    if (string.Compare(nv.ChucVu, "Nhân viên kỹ thuật") == 0)
                    {
                        nvkt = nv;
                        break;
                    }
                }
            }
            return nvkt;
        }

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
            if (arrNV.Count>0)
            {
                foreach (CNhanVien nv  in arrNV)
                {
                    if (string.Compare(nv.ChucVu,chucvu)==0)
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
                    txtLuongcb.Text =lcbPhaChe.ToString();
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

        public int timLuongcb(string chucvu)
        {
            foreach (CNhanVien nv  in arrNV)
            {
                if (string.Compare(nv.ChucVu,chucvu)==0)
                {
                    return nv.Luongcb;
                }
            }
            return 0;
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
            nv.Luongcb = timLuongcb(nv.ChucVu);
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
            nv.Luongcb = timLuongcb(nv.ChucVu);

            arrNV.Add(nv);
            txtTongLuong.Text = nv.TongLuong().ToString();
            i++;
            setupLuongcb(nv.ChucVu, nv.Luongcb);
            syncLuongcb(nv.ChucVu);
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
            else chkGioiTinh.Text = "Nữ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }
    }
}