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
    public partial class HistoryKH : Form
    {
        #region Attributes
        FormManager frmmng = new FormManager();
        private int icmnd;
        private string shoten;
        private DateTime dtngaythanhtoan;
        #endregion

        #region Method
        public bool sosanhTime(DateTime a, DateTime b)
        {
            if (a.Date != b.Date)   return false;
            if (a.Hour != b.Hour) return false;
            if (a.Minute != b.Minute) return false;
            if (a.Second != b.Second) return false;
            return true;
        }
        public void clearLSKH()
        {
            txtTenKH.Text = "";
            cbxLoaiphong.SelectedIndex = -1;
            txtQuoctich.Text = "";
            lvwChooseP.Items.Clear();
            lvwChooseDSDV.Items.Clear();
        }
        public bool ChonTheo1TieuChi()
        {
            bool kq = false;
            int d = 0;
            d += (txtTenKH.Text == "") ? 0 : 1;
            d += (cbxLoaiphong.SelectedIndex == -1) ? 0 : 1;
            d += (txtQuoctich.Text == "") ? 0 : 1;
            if (d < 1)
            {
                MessageBox.Show("Bạn chưa chọn 1 trong 3 tiêu chí");
            }
            else if (d > 1)
            {
                clearLSKH();
                MessageBox.Show("Bạn chỉ có thể chọn 1 tiêu chí");
            }
            else kq = true;
            return kq;
        }
        private CHistory timKH(int cmnd, string hoten, DateTime ngaythanhtoan)
        {
            CHistory cus = null;
            if (frmmng.Data.ArrLS.Count > 0)
            {
                foreach (CHistory itemls in frmmng.Data.ArrLS)
                {
                    CBill a = itemls.Ctmbill;
                    if (a.Kh.CMND == cmnd && string.Equals(a.Kh.Hoten, hoten) && sosanhTime(ngaythanhtoan,a.Ngaythanhtoan))
                    {
                        cus = itemls;
                        break;
                    }
                }
            }
            return cus;
        }
        private void hienThiTextBox(CHistory cus)
        {
            txtTenKH.Text = cus.Ctmbill.Kh.Hoten;
            txtQuoctich.Text = cus.Ctmbill.Kh.Quoctich;
            cbxLoaiphong.Text = (cus.Ctmbill.Dp == null) ? "Không Đặt Phòng" : ((cus.Ctmbill.Dp.Phong.Count == 1) ? cus.Ctmbill.Dp.Phong[0].Loaiphong : "Khách đặt nhiều Phòng");
        }
        public void hienThiDSKH()
        {
            lvwInfoKH.Items.Clear();
            if(frmmng.Data.ArrLS.Count>0)
            {
                foreach (CHistory itemls in frmmng.Data.ArrLS)
                {
                    CBill a = itemls.Ctmbill;
                    ListViewItem li = lvwInfoKH.Items.Add(a.Kh.Hoten);
                    li.SubItems.Add(a.Kh.CMND.ToString());
                    li.SubItems.Add(a.Dp != null ? a.Dp.Ngayden.ToString() : a.Ngaythanhtoan.ToString());
                    li.SubItems.Add(a.Dp != null ? a.Dp.Ngaydi.ToString() : a.Ngaythanhtoan.ToString());
                    li.SubItems.Add(a.Dp != null ? a.Dp.SoNgayO().ToString() : "Không Đặt Phòng");
                    li.SubItems.Add(a.Dp != null ? a.Dp.TongThanhTien().ToString() : "Không Đặt Phòng");
                    li.SubItems.Add(a.Ddv != null ? a.Ddv.tinhTongGiaTien().ToString() : "Không Đặt DV");
                    li.SubItems.Add(a.tinhTongThanhTien().ToString());
                    li.SubItems.Add(a.Ngaythanhtoan.ToString());
                    li.SubItems.Add(a.Kh.Quoctich);
                }
            }
        }
        public void hienThiChiTietDPCuaKH(int cmnd, string hoten, DateTime ngaythanhtoan)
        {
            lvwChooseP.Items.Clear();
            if (frmmng.Data.ArrLS.Count > 0)
            {
                foreach (CHistory itemls in frmmng.Data.ArrLS)
                {
                    CBill a = itemls.Ctmbill;
                    if(a.Kh.CMND==cmnd && string.Equals(a.Kh.Hoten,hoten) && sosanhTime(ngaythanhtoan, a.Ngaythanhtoan))
                    {
                        if (a.Dp != null)
                        {
                            foreach (CPhong itemp in a.Dp.Phong)
                            {
                                ListViewItem li = lvwChooseP.Items.Add(itemp.Sophong.ToString());
                                li.SubItems.Add(itemp.Loaiphong);
                                li.SubItems.Add(a.Dp.ThanhTien(itemp.Loaiphong).ToString());
                            }
                        }
                        break;
                    }
                }
            }
        }
        public void hienThiChiTietDDVCuaKH(int cmnd, string hoten, DateTime ngaythanhtoan)
        {
            lvwChooseDSDV.Items.Clear();
            if (frmmng.Data.ArrLS.Count > 0)
            {
                foreach (CHistory itemls in frmmng.Data.ArrLS)
                {
                    CBill a = itemls.Ctmbill;
                    if(a.Kh.CMND==cmnd && string.Equals(a.Kh.Hoten,hoten) && sosanhTime(ngaythanhtoan, a.Ngaythanhtoan))
                    {
                        if (a.Ddv != null)
                        {
                            foreach (CDichVu itemdv in a.Ddv.Arrdv)
                            {
                                ListViewItem li = lvwChooseDSDV.Items.Add(itemdv.Smadv);
                                li.SubItems.Add(itemdv.Stendv);
                                li.SubItems.Add(a.Ddv.tinhTongGiaTien().ToString());
                            }
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        public HistoryKH()
        {
            InitializeComponent();
        }

        private void cbxLoaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvwInfoKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwInfoKH.SelectedIndices)
            {
                icmnd = Convert.ToInt32(lvwInfoKH.Items[j].SubItems[1].Text);
                shoten = lvwInfoKH.Items[j].Text;
                dtngaythanhtoan = Convert.ToDateTime(lvwInfoKH.Items[j].SubItems[8].Text);
                CHistory cus = timKH(icmnd, shoten, dtngaythanhtoan);
                hienThiTextBox(cus);
                hienThiChiTietDPCuaKH(icmnd, shoten, dtngaythanhtoan);
                hienThiChiTietDDVCuaKH(icmnd, shoten, dtngaythanhtoan);
                break;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (frmmng.Data.ArrLS.Count > 0)
            {
                hienThiDSKH();
            }
            clearLSKH();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearLSKH();
            hienThiDSKH();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveLSKH("dslskh.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void HistoryKH_Load(object sender, EventArgs e)
        {
            frmmng.Data.OpenLSKH("dslskh.txt");

            icmnd = -1;
            shoten = "";
            dtngaythanhtoan = DateTime.Now;

            if (frmmng.Data.ArrLS.Count > 0)
            {
                hienThiDSKH();
            }
            cbxLoaiphong.SelectedIndex = -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!(ChonTheo1TieuChi()))
            {
                return;
            }
            lvwInfoKH.Items.Clear();
            if (txtTenKH.Text != "")
            {
                foreach (CHistory ls in frmmng.Data.ArrLS)
                {
                    if (string.Compare(ls.Ctmbill.Kh.Hoten, txtTenKH.Text) == 0)
                    {
                        ListViewItem li = lvwInfoKH.Items.Add(ls.Ctmbill.Kh.Hoten);
                        li.SubItems.Add(ls.Ctmbill.Kh.CMND.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngayden.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngaydi.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.SoNgayO().ToString() : "Không Đặt Phòng");
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.TongThanhTien().ToString() : "Không Đặt Phòng");
                        li.SubItems.Add(ls.Ctmbill.Ddv != null ? ls.Ctmbill.Ddv.tinhTongGiaTien().ToString() : "Không Đặt DV");
                        li.SubItems.Add(ls.Ctmbill.tinhTongThanhTien().ToString());
                        li.SubItems.Add(ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Kh.Quoctich);
                    }
                }
            }
            if (cbxLoaiphong.SelectedIndex >= 0)
            {
                foreach (CHistory ls in frmmng.Data.ArrLS)
                {
                    if(ls.Ctmbill.Dp!=null)
                    {
                        bool timthayphong = false;
                        foreach(CPhong p in ls.Ctmbill.Dp.Phong)
                        {
                            if (timthayphong) continue;
                            if(string.Equals(p.Loaiphong, cbxLoaiphong.Text))
                            {
                                timthayphong = true;
                                ListViewItem li = lvwInfoKH.Items.Add(ls.Ctmbill.Kh.Hoten);
                                li.SubItems.Add(ls.Ctmbill.Kh.CMND.ToString());
                                li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngayden.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                                li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngaydi.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                                li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.SoNgayO().ToString() : "Không Đặt Phòng");
                                li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.TongThanhTien().ToString() : "Không Đặt Phòng");
                                li.SubItems.Add(ls.Ctmbill.Ddv != null ? ls.Ctmbill.Ddv.tinhTongGiaTien().ToString() : "Không Đặt DV");
                                li.SubItems.Add(ls.Ctmbill.tinhTongThanhTien().ToString());
                                li.SubItems.Add(ls.Ctmbill.Ngaythanhtoan.ToString());
                                li.SubItems.Add(ls.Ctmbill.Kh.Quoctich);
                            }
                        }
                    }
                }
            }
            if (txtQuoctich.Text != "")
            {
                foreach (CHistory ls in frmmng.Data.ArrLS)
                {
                    if (string.Compare(ls.Ctmbill.Kh.Quoctich, txtQuoctich.Text) == 0)
                    {
                        ListViewItem li = lvwInfoKH.Items.Add(ls.Ctmbill.Kh.Hoten);
                        li.SubItems.Add(ls.Ctmbill.Kh.CMND.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngayden.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.Ngaydi.ToString() : ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.SoNgayO().ToString() : "Không Đặt Phòng");
                        li.SubItems.Add(ls.Ctmbill.Dp != null ? ls.Ctmbill.Dp.TongThanhTien().ToString() : "Không Đặt Phòng");
                        li.SubItems.Add(ls.Ctmbill.Ddv != null ? ls.Ctmbill.Ddv.tinhTongGiaTien().ToString() : "Không Đặt DV");
                        li.SubItems.Add(ls.Ctmbill.tinhTongThanhTien().ToString());
                        li.SubItems.Add(ls.Ctmbill.Ngaythanhtoan.ToString());
                        li.SubItems.Add(ls.Ctmbill.Kh.Quoctich);
                    }
                }
            }

        }
    }
}
