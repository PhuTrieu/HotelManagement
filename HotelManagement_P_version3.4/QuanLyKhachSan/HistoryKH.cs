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
    public partial class HistoryKH : Form
    {
        FormManager frmmng = new FormManager();
        private List<CHistory> arrLS;

        private List<CKhachHang> arrKH;
        private List<CDatPhong> arrDP;

        private int i = -1;
        public HistoryKH()
        {
            InitializeComponent();
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
                MessageBox.Show("Không có dữ liệu Lịch sử khách hàng", "Error");
            }
        }
        private void HistoryKH_Load(object sender, EventArgs e)
        {
            arrLS = new List<CHistory>();
            OpenKH("dskh.txt");
            OpenDP("dsdp.txt");
            OpenLSKH("dslskh.txt");
            if (arrLS.Count > 0)
                hienthi();
            cbxLoaiphong.SelectedIndex = -1;
        }

        public void hienthi()
        {
            lvwLS.Items.Clear();
            foreach (CHistory ls  in arrLS)
            {
                ListViewItem li = lvwLS.Items.Add(ls.Dp.Kh.Hoten);
                li.SubItems.Add(ls.Dp.Kh.CMND.ToString());
                if (ls.Kh.Gioitinh == true)
                {
                    li.SubItems.Add("Nam");
                }
                else li.SubItems.Add("Nữ");
                li.SubItems.Add(ls.Kh.Tuoi.ToString());
                li.SubItems.Add(ls.Kh.Quoctich);
                li.SubItems.Add(ls.Kh.Sdt.ToString());
                li.SubItems.Add(ls.Dp.Phong.Loaiphong);
                li.SubItems.Add(ls.Dp.Phong.Sophong.ToString());
                li.SubItems.Add(ls.Dp.Ngayden.ToShortDateString());
                li.SubItems.Add(ls.Dp.Ngaydi.ToShortDateString());
                li.SubItems.Add(ls.Dp.SoNgayO().ToString());
                li.SubItems.Add(ls.Dp.ThanhTien().ToString());
            }
        }

        public void clearLSKH()
        {
            txtTenKH.Text = "";
            txtQuoctich.Text = "";
            cbxLoaiphong.SelectedIndex = -1;
        }

        public bool checkTheo1TieuChi()
        {
            bool kq = false;
            int dem = 0;
            dem += (txtTenKH.Text == "") ? 0 : 1;
            dem += (txtQuoctich.Text == "") ? 0 : 1;
            dem += (cbxLoaiphong.SelectedIndex == -1) ? 0 : 1;

            if (dem < 1)
                MessageBox.Show("Bạn chưa chọn tiêu chí", "Error");
            else if (dem > 1)
            {
                clearLSKH();
                MessageBox.Show("Bạn chỉ có thể chọn theo 1 tiêu chí", "Error");
            }
            else
                kq = true;
            return kq;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!checkTheo1TieuChi()) return;
            lvwLS.Items.Clear();
            if(txtTenKH.Text != "")
            {
                foreach(CHistory lskh in arrLS)
                {
                    if(string.Compare(lskh.Kh.Hoten,txtTenKH.Text)==0)
                    {
                        ListViewItem li = lvwLS.Items.Add(lskh.Dp.Kh.Hoten);
                        li.SubItems.Add(lskh.Dp.Kh.CMND.ToString());
                        li.SubItems.Add(lskh.Kh.Gioitinh ? "Nam" : "Nữ");
                        li.SubItems.Add(lskh.Kh.Tuoi.ToString());
                        li.SubItems.Add(lskh.Kh.Quoctich);
                        li.SubItems.Add(lskh.Kh.Sdt.ToString());
                        li.SubItems.Add(lskh.Dp.Phong.Loaiphong);
                        li.SubItems.Add(lskh.Dp.Phong.Sophong.ToString());
                        li.SubItems.Add(lskh.Dp.Ngayden.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.Ngaydi.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.SoNgayO().ToString());
                        li.SubItems.Add(lskh.Dp.ThanhTien().ToString());
                    }
                }
            }
            if(txtQuoctich.Text != "")
            {
                foreach(CHistory lskh in arrLS)
                {
                    if(string.Compare(lskh.Kh.Quoctich,txtQuoctich.Text)==0)
                    {
                        ListViewItem li = lvwLS.Items.Add(lskh.Dp.Kh.Hoten);
                        li.SubItems.Add(lskh.Dp.Kh.CMND.ToString());
                        li.SubItems.Add(lskh.Kh.Gioitinh ? "Nam" : "Nữ");
                        li.SubItems.Add(lskh.Kh.Tuoi.ToString());
                        li.SubItems.Add(lskh.Kh.Quoctich);
                        li.SubItems.Add(lskh.Kh.Sdt.ToString());
                        li.SubItems.Add(lskh.Dp.Phong.Loaiphong);
                        li.SubItems.Add(lskh.Dp.Phong.Sophong.ToString());
                        li.SubItems.Add(lskh.Dp.Ngayden.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.Ngaydi.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.SoNgayO().ToString());
                        li.SubItems.Add(lskh.Dp.ThanhTien().ToString());
                    }
                }
            }
            if (cbxLoaiphong.SelectedIndex >= 0)
            {
                foreach (CHistory lskh in arrLS)
                {
                    if (string.Compare(lskh.Dp.Phong.Loaiphong, cbxLoaiphong.SelectedItem.ToString()) == 0)
                    {
                        ListViewItem li = lvwLS.Items.Add(lskh.Dp.Kh.Hoten);
                        li.SubItems.Add(lskh.Dp.Kh.CMND.ToString());
                        li.SubItems.Add(lskh.Kh.Gioitinh ? "Nam" : "Nữ");
                        li.SubItems.Add(lskh.Kh.Tuoi.ToString());
                        li.SubItems.Add(lskh.Kh.Quoctich);
                        li.SubItems.Add(lskh.Kh.Sdt.ToString());
                        li.SubItems.Add(lskh.Dp.Phong.Loaiphong);
                        li.SubItems.Add(lskh.Dp.Phong.Sophong.ToString());
                        li.SubItems.Add(lskh.Dp.Ngayden.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.Ngaydi.ToShortDateString());
                        li.SubItems.Add(lskh.Dp.SoNgayO().ToString());
                        li.SubItems.Add(lskh.Dp.ThanhTien().ToString());
                    }
                }
            }

        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            clearLSKH();
            hienthi();
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
                MessageBox.Show("Lưu không được LS KH này", "Error");
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            SaveLSKH("dslskh.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        public void hienThiLSKH(int j)
        {
            CHistory lskh = arrLS[j];
            txtTenKH.Text = lskh.Kh.Hoten;
            txtQuoctich.Text = lskh.Kh.Quoctich;
            cbxLoaiphong.Text = lskh.Dp.Phong.Loaiphong;
        }

        private void lvwLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(int j in lvwLS.SelectedIndices)
            {
                i = j;
                hienThiLSKH(i);
                break;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearLSKH();
        }
    }
}
