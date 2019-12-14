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
        private int i = -1;

        private string sTenKH;
        private int iCMND;
        #endregion

        public DatPhong()
        {
            InitializeComponent();
        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            frmmng.Data.OpenKH("dskh.txt");
            frmmng.Data.OpenP("dsp.txt");
            frmmng.Data.OpenDP("dsdp.txt");
            frmmng.Data.OpenDSBill("dsbill.txt");
            dtpNgayden.Value = DateTime.Now;
            dtpNgaydi.Value = DateTime.Now;
            hienthi();
            ShowDataTenKH();
            cbxHoten.Select();
        }

        public void hienthi()
        {
            lvwDP.Items.Clear();
            foreach (CDatPhong dp  in frmmng.Data.ArrDP)
            {
                ListViewItem li = lvwDP.Items.Add(dp.Kh.Hoten);
                li.SubItems.Add(dp.Kh.CMND.ToString());
                li.SubItems.Add(dp.Ngayden.ToShortDateString());
                li.SubItems.Add(dp.Ngaydi.ToShortDateString());
                li.SubItems.Add(dp.SoNgayO().ToString());
            }
        }

        public void hienthiDP(int j)
        {
            CDatPhong dp = (CDatPhong)frmmng.Data.ArrDP[j];
            cbxHoten.Text = dp.Kh.Hoten + " (" + dp.Kh.CMND.ToString() + ")";
            dtpNgayden.Value = dp.Ngayden;
            dtpNgaydi.Value = dp.Ngaydi;
            txtSoNgayO.Text = dp.SoNgayO().ToString();
            if(dp.Phong.Count==1)
            {
                cbxLoaiphong.Text = dp.Phong[0].Loaiphong;
                txtSoPhong.Text = dp.Phong[0].Sophong.ToString();
                txtThanhTien.Text = dp.ThanhTien(cbxLoaiphong.Text).ToString();
                txtTongGiaTien.Text = dp.TongThanhTien().ToString();
            }
            else if(dp.Phong.Count>1)
            {
                try
                {
                    foreach (int pos in lvwChooseP.SelectedIndices)
                    {
                        txtSoPhong.Text = lvwChooseP.Items[pos].Text;
                        cbxLoaiphong.Text = lvwChooseP.Items[pos].SubItems[1].Text;
                        txtThanhTien.Text = lvwChooseP.Items[pos].SubItems[2].Text;
                        txtTongGiaTien.Text = dp.TongThanhTien().ToString();
                        break;
                    }
                }
                catch (Exception)
                {
                        txtSoPhong.Text = "";
                        cbxLoaiphong.Text = "";
                        txtThanhTien.Text = "";
                        txtTongGiaTien.Text = "";
                }
            }
        }

        public void setupBookedP(int sophong,string loaiphong)
        {
            foreach(CPhong p in frmmng.Data.ArrPKS)
            {
                if (string.Compare(p.Loaiphong, loaiphong) == 0 && p.Sophong == sophong)
                    p.Trangthai = "Booked";
            }
            frmmng.Data.SaveP("dsp.txt");
        }
        public void ShowDataTenKH()
        {
            cbxHoten.Items.Clear();
            if (frmmng.Data.ArrKH.Count > 0)
            {
                foreach (CKhachHang kh in frmmng.Data.ArrKH)
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
            iCMND = int.Parse(temp.Substring(charfrom, charlength));
        }
        public void CleanDP()
        {
            cbxHoten.Text="";
            txtSoPhong.Text = "";
            txtSoNgayO.Text = "";
            cbxLoaiphong.Text = "";
            txtThanhTien.Text = "";
            txtTongGiaTien.Text = "";
        }

        public int timGiaPhong(string loai)
        {
            foreach(CPhong p in frmmng.Data.ArrPKS)
            {
                if(string.Equals(loai,p.Loaiphong))
                {
                    return p.Gia;
                }
            }
            return -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (radSetup.Checked == true)
            {
                bool tontai_kh_old = false;
                if (txtSoPhong.Text == "")
                {
                    MessageBox.Show("Các TH:\n" +
                        "- NV chưa chọn Loại Phòng để có số Phòng\n" +
                        "- Loại Phòng đó đã Hết", "Error");
                    return;
                }
                layTenKHvaCMND(cbxHoten.Text);
                string hotenkh = sTenKH;
                int socmnd = iCMND;
                int sophong = int.Parse(txtSoPhong.Text);
                string loaiphong = cbxLoaiphong.Text;
                if (frmmng.Data.ArrDP.Count > 0)
                {
                    foreach (CDatPhong item in frmmng.Data.ArrDP)
                    {
                        if (item.Kh.CMND == iCMND)
                        {
                            tontai_kh_old = true;
                            CPhong p = new CPhong();
                            p.Sophong = sophong;
                            p.Loaiphong = loaiphong;
                            p.Gia = timGiaPhong(p.Loaiphong);

                            item.Phong.Add(p);

                            txtSoNgayO.Text = item.SoNgayO().ToString();

                            txtThanhTien.Text = item.ThanhTien(loaiphong).ToString();
                            setupBookedP(sophong, loaiphong);

                            CleanDP();
                            hienthi();
                            
                            foreach (CBill bill in frmmng.Data.ArrBill)
                            {
                                if (bill.Kh.CMND == iCMND)
                                {
                                    bill.Dp= item;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                if (!tontai_kh_old)
                {
                    DateTime ngayden = dtpNgayden.Value;
                    DateTime ngaydi = dtpNgaydi.Value;

                    CDatPhong dp = new CDatPhong();
                    dp.Kh.Hoten = hotenkh;
                    dp.Kh.CMND = socmnd;
                    dp.Ngayden = ngayden;
                    dp.Ngaydi = ngaydi;

                    CPhong Phong = new CPhong();
                    Phong.Sophong = sophong;
                    Phong.Loaiphong = loaiphong;
                    Phong.Gia = timGiaPhong(Phong.Loaiphong);

                    dp.Phong.Add(Phong);

                    frmmng.Data.ArrDP.Add(dp);
                    txtSoNgayO.Text = dp.SoNgayO().ToString();

                    txtThanhTien.Text = dp.ThanhTien(loaiphong).ToString();
                    setupBookedP(sophong, loaiphong);

                    i++;
                    CleanDP();
                    hienthi();

                    foreach (CBill item in frmmng.Data.ArrBill)
                    {
                        if (item.Kh.CMND == dp.Kh.CMND)
                        {
                            item.Dp = dp;
                            break;
                        }
                    }
                }
                lvwChooseP.Items.Clear();
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
                foreach (CPhong p in frmmng.Data.ArrPKS)
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
            foreach (CKhachHang kh  in frmmng.Data.ArrKH)
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
            frmmng.Data.SaveDP("dsdp.txt");
            frmmng.Data.SaveDSBill("dsbill.txt");
            this.Hide();
            HoaDonThanhToan frmB = new HoaDonThanhToan(cbxHoten.Text);
            frmB.ShowDialog();
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveDP("dsdp.txt");
            frmmng.Data.SaveDSBill("dsbill.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }
        public void hienThiCacPhongChoose(int cmnd)
        {
            lvwChooseP.Items.Clear();
            foreach (CDatPhong item in frmmng.Data.ArrDP)
            {
                if(item.Kh.CMND==cmnd)
                {
                    foreach (CPhong Phong in item.Phong)
                    {
                        ListViewItem li = lvwChooseP.Items.Add(Phong.Sophong.ToString());
                        li.SubItems.Add(Phong.Loaiphong);
                        li.SubItems.Add(item.ThanhTien(Phong.Loaiphong).ToString());
                    }
                    break;
                }
            }
        }
        private void lvwDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwDP.SelectedIndices)
            {
                hienThiCacPhongChoose(Convert.ToInt32(lvwDP.Items[j].SubItems[1].Text));
                lvwChooseP.Items[0].Selected = true;
                i = j;
                hienthiDP(i);
                break;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (frmmng.Data.ArrDP.Count > 0 && radSetup.Checked == true)
            {
                int sophongchange=-1;
                string loaiphongchange = "";
                foreach(int pos in lvwChooseP.SelectedIndices)
                {
                    sophongchange = Convert.ToInt32(lvwChooseP.Items[pos].Text);
                    loaiphongchange = lvwChooseP.Items[pos].SubItems[1].Text;
                    break;
                }
                foreach (CPhong p in frmmng.Data.ArrPKS)
                {
                    if (p.Sophong == sophongchange && string.Compare(p.Loaiphong, loaiphongchange) == 0)
                    {
                        p.Trangthai = "Empty";
                        frmmng.Data.SaveP("dsp.txt");
                        break;
                    }
                }

                layTenKHvaCMND(cbxHoten.Text);
                string hotenkh = sTenKH;
                int socmnd = iCMND;

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

                CDatPhong dp = frmmng.Data.ArrDP[i];
                dp.Kh.Hoten = hotenkh;
                dp.Kh.CMND = socmnd;
                dp.Ngayden = ngayden;
                dp.Ngaydi = ngaydi;

                foreach(CPhong Phong in frmmng.Data.ArrDP[i].Phong)
                {
                    if(Phong.Sophong == sophongchange && string.Compare(Phong.Loaiphong, loaiphongchange) == 0)
                    {
                        Phong.Sophong = sophong;
                        Phong.Loaiphong = loaiphong;
                        Phong.Gia = timGiaPhong(Phong.Loaiphong);
                    }
                    break;
                }

                txtSoNgayO.Text = dp.SoNgayO().ToString();
                txtThanhTien.Text = dp.ThanhTien(loaiphong).ToString();
                setupBookedP(sophong, loaiphong);

                i++;
                hienthi();
                lvwChooseP.Items.Clear();

                foreach (CBill item in frmmng.Data.ArrBill)
                {
                    if (item.Kh.CMND == dp.Kh.CMND)
                    {
                        item.Dp = dp;
                        break;
                    }
                }
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

        private void lvwChooseP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwDP.SelectedIndices)
            {
                i = j;
                hienthiDP(i);
                break;
            }
        }
    }
}
