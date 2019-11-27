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
    public partial class DatPhong : Form
    {
        #region Attribute
        FormManager frmmng = new FormManager();
        private List<CDatPhong> arrDP;
        private int i = -1;

        private List<CKhachHang> arrKH;
        private List<CPhong> arrPKS;
        private List<CHistory> arrLS;

        private string sTenKH;
        private int sCMND;
        #endregion

        public DatPhong()
        {
            InitializeComponent();
        }

        public void OpenKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrKH = (List<CKhachHang>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có dữ liệu khách hàng", "Error");
            }
        }

        public void OpenP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrPKS = (List<CPhong>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có dữ liệu phòng", "Error");
            }
        }
        

        public void OpenDP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrDP = (List<CDatPhong>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có dữ liệu Đặt phòng", "Error");
            }
        }

        public void OpenLSKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrLS = (List<CHistory>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có dữ liệu lịch sử khách hàng", "Error");
            }
        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            arrDP = new List<CDatPhong>();
            OpenKH("dskh.txt");
            OpenP("dsp.txt");
            OpenDP("dsdp.txt");
            OpenLSKH("dslskh.txt");

            hienthi();
            ShowDataTenKH();
            cbxHoten.Select();
        }

        public void hienthi()
        { 
            lvwDP.Items.Clear();
            foreach (CDatPhong dp  in arrDP)
            {
                ListViewItem li = lvwDP.Items.Add(dp.Kh.Hoten);
                li.SubItems.Add(dp.Kh.CMND.ToString());
                li.SubItems.Add(dp.Phong.Sophong.ToString());
                li.SubItems.Add(dp.Ngayden.ToShortDateString());
                li.SubItems.Add(dp.Ngaydi.ToShortDateString());
                li.SubItems.Add(dp.Phong.Loaiphong);
                li.SubItems.Add(dp.SoNgayO().ToString());
                li.SubItems.Add(dp.ThanhTien().ToString());
            }
        }

        public void hienthiDP(int j)
        {
            CDatPhong dp = (CDatPhong)arrDP[j];
            cbxHoten.Text = dp.Kh.Hoten + " (" + dp.Kh.CMND.ToString() + ")";
            dtpNgayden.Value = dp.Ngayden;
            dtpNgaydi.Value = dp.Ngaydi;
            txtSoPhong.Text = dp.Phong.Sophong.ToString();
            cbxLoaiphong.Text = dp.Phong.Loaiphong;
            txtSoNgayO.Text = dp.SoNgayO().ToString();
            txtThanhTien.Text = dp.ThanhTien().ToString();
        }

        public void SaveP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrPKS);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu không được", "Error");
            }
        }
        public void SaveKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrKH);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu không được", "Error");
            }
        }
        public void SaveDP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrDP);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu không được", "Error");
            }
        }

        public void SaveLSKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrLS);
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu không được", "Error");
            }
        }

        public void setupBookedP(int sophong,string loaiphong)
        {
            foreach(CPhong p in arrPKS)
            {
                if (string.Compare(p.Loaiphong, loaiphong) == 0 && p.Sophong == sophong)
                    p.Trangthai = "Booked";
            }
            SaveP("dsp.txt");
        }
        public void ShowDataTenKH()
        {
            cbxHoten.Items.Clear();
            if (arrKH.Count > 0)
            {
                foreach (CKhachHang kh in arrKH)
                {
                    cbxHoten.Items.Add(kh.Hoten + " (" + kh.CMND + ")");
                }
            }
        }

        public void layTenKHvaCMND(string c)
        {
            string temp = c;
            int charfrom = temp.IndexOf('(', 0) + 1;
            int charto = temp.IndexOf(')', 0) - 1;
            int charlength = charto - charfrom + 1;
            sTenKH = temp.Substring(0, charfrom - 3 + 1);
            sCMND = int.Parse(temp.Substring(charfrom, charlength));
        }
        public void CleanDP()
        {
            cbxHoten.Text="";
            txtSoPhong.Text = "";
            txtSoNgayO.Text = "";
            cbxLoaiphong.Text = "";
            txtThanhTien.Text = "";
        }

        public int timGiaPhong(string loaiphong)
        {
            foreach (CPhong p  in arrPKS)
            {
                if (string.Equals(loaiphong,p.Loaiphong))
                {
                    return p.Gia;
                }
            }
            return 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (radSetup.Checked == true)
            {
                if (txtSoPhong.Text == "")
                {
                    MessageBox.Show("Các TH:\n" +
                        "- NV chưa chọn Loại Phòng để có số Phòng\n" +
                        "- Loại Phòng đó đã Hết", "Error");
                    return;
                }
                layTenKHvaCMND(cbxHoten.Text);
                string hotenkh = sTenKH;
                int socmnd = sCMND;
                if (arrDP.Count>0)
                {
                    foreach(CDatPhong dp_old in arrDP)
                    {
                        if(string.Equals(hotenkh,dp_old.Kh.Hoten)&&socmnd==dp_old.Kh.CMND)
                        {
                            MessageBox.Show("Khách Hàng đó đã đặt phòng rồi", "Error");
                            return;
                        }
                    }
                }

                int sophong = int.Parse(txtSoPhong.Text);
                string loaiphong = cbxLoaiphong.Text;

                DateTime ngayden = dtpNgayden.Value;
                DateTime ngaydi = dtpNgaydi.Value;

                CDatPhong dp = new CDatPhong();
                dp.Kh.Hoten = hotenkh;
                dp.Kh.CMND = socmnd;
                dp.Phong.Sophong = sophong;
                dp.Phong.Loaiphong = loaiphong;
                dp.Ngayden = ngayden;
                dp.Ngaydi = ngaydi;

                dp.Phong.Gia = timGiaPhong(dp.Phong.Loaiphong);

                arrDP.Add(dp);
                txtSoNgayO.Text = dp.SoNgayO().ToString();
                txtThanhTien.Text = dp.ThanhTien().ToString();
                setupBookedP(dp.Phong.Sophong, dp.Phong.Loaiphong);

                i++;
                CleanDP();
                hienthi();
            }
        }

        private void cbxLoaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radSetup.Checked == true)
            {
                if (cbxLoaiphong.Text == "")
                {
                    MessageBox.Show("Chưa chọn Loại Phòng", "Error");
                    return;
                }
                bool checkemptyp = false;
                foreach (CPhong p in arrPKS)
                {
                    if (string.Compare(p.Loaiphong, cbxLoaiphong.Text) == 0 && string.Compare(p.Trangthai, "Empty") == 0)
                    {
                        txtSoPhong.Text = p.Sophong.ToString();
                        checkemptyp = true;
                    }
                }
                if (!checkemptyp)
                    txtSoPhong.Text = "";
            }
        }

        private CKhachHang timInfoKH(string hoten, int cmnd)
        {
            CKhachHang timkh = null;
            foreach (CKhachHang kh  in arrKH)
            {
                if (string.Compare(kh.Hoten,hoten)==0 && cmnd==kh.CMND)
                {
                    timkh = kh;
                    break;
                }
            }
            return timkh;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (arrDP.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Error");
                return;
            }
            CHistory ls = new CHistory();
            ls.Dp.Kh.Hoten = arrDP[i].Kh.Hoten;
            ls.Dp.Kh.CMND = arrDP[i].Kh.CMND;
            ls.Dp.Ngayden = arrDP[i].Ngayden;
            ls.Dp.Ngaydi = arrDP[i].Ngaydi;
            ls.Dp.Phong.Sophong = arrDP[i].Phong.Sophong;
            ls.Dp.Phong.Loaiphong = arrDP[i].Phong.Loaiphong;
            ls.Dp.Phong.Gia = arrDP[i].Phong.Gia;
            CKhachHang timkh = timInfoKH(ls.Dp.Kh.Hoten, ls.Dp.Kh.CMND);
            if (timkh == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng trong danh sách khách hàng");
                return;
            }
            ls.Kh.Hoten = ls.Dp.Kh.Hoten;
            ls.Kh.CMND = ls.Dp.Kh.CMND;
            ls.Kh.Gioitinh = timkh.Gioitinh;
            ls.Kh.Tuoi = timkh.Tuoi;
            ls.Kh.Quoctich = timkh.Quoctich;
            ls.Kh.Sdt = timkh.Sdt;
            arrLS.Add(ls);

            CDatPhong dp = arrDP[i];
            foreach (CPhong p in arrPKS)
            {
                if (p.Sophong == dp.Phong.Sophong && string.Compare(p.Loaiphong, dp.Phong.Loaiphong) == 0)
                {
                    p.Trangthai = "Empty";
                    SaveP("dsp.txt");
                    break;
                }
            }
            foreach (CKhachHang kh in arrKH)
            {
                if(kh.CMND==dp.Kh.CMND)
                {
                    arrKH.Remove(kh);
                    SaveKH("dskh.txt");
                    break;
                }
            }
            ShowDataTenKH();
            arrDP.RemoveAt(i);
            i--;
            if (i < 0 && arrDP.Count > 0) i = 0;
            if (i >= 0)
                hienthiDP(i);
            hienthi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            SaveLSKH("dslskh.txt");
            SaveDP("dsdp.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void lvwDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwDP.SelectedIndices)
            {
                i = j;
                hienthiDP(i);
                break;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //  Vị trí đặt Phòng Thứ i của P cũ sẽ bị mất đi -> Empty
            // Setup như nhập, bỏ ik clearDP vs arr Add
            if (arrDP.Count > 0 && radSetup.Checked == true)
            {
                CDatPhong dp_old = arrDP[i];
                foreach (CPhong p in arrPKS)
                {
                    if (p.Sophong == dp_old.Phong.Sophong && string.Compare(p.Loaiphong, dp_old.Phong.Loaiphong) == 0)
                    {
                        p.Trangthai = "Empty";
                        SaveP("dsp.txt");
                        break;
                    }
                }
                layTenKHvaCMND(cbxHoten.Text);
                string hotenkh = sTenKH;
                int socmnd = sCMND;

                if (txtSoPhong.Text == "")
                {
                    MessageBox.Show("Các TH:\n" +
                        "- NV chưa chọn Loại Phòng để có số Phòng\n" +
                        "- Loại Phòng đó đã Hết", "Error");
                    return;
                }
                int sophong = int.Parse(txtSoPhong.Text);
                string loaiphong = cbxLoaiphong.Text;

                DateTime ngayden = dtpNgayden.Value;
                DateTime ngaydi = dtpNgaydi.Value;

                CDatPhong dp = arrDP[i];
                dp.Kh.Hoten = hotenkh;
                dp.Kh.CMND = socmnd;
                dp.Phong.Sophong = sophong;
                dp.Phong.Loaiphong = loaiphong;
                dp.Ngayden = ngayden;
                dp.Ngaydi = ngaydi;
                dp.Phong.Gia = timGiaPhong(dp.Phong.Loaiphong);
                txtSoNgayO.Text = dp.SoNgayO().ToString();
                txtThanhTien.Text = dp.ThanhTien().ToString();
                setupBookedP(dp.Phong.Sophong, dp.Phong.Loaiphong);

                i++;
                hienthi();
            }
        }

        private void radView_CheckedChanged(object sender, EventArgs e)
        {
            CleanDP();
        }

        private void radSetup_CheckedChanged(object sender, EventArgs e)
        {
            CleanDP();
        }
    }
}
