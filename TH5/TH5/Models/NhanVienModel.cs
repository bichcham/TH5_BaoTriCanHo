namespace TH5.Models
{
    public class NhanVienModel
    {
        private string manhanvien;
        private string tennhanvien;
        private string sodienthoai;
        private bool gioitinh;

        public string MaNhanVien
        {
            get { return manhanvien; }
            set { manhanvien = value; }
        }

        public string TenNhanVien
        {
            get { return tennhanvien;}
            set { tennhanvien = value; }
        }

        public string SoDienThoai
        {
            get
            {
                return sodienthoai;
            }
            set { sodienthoai = value; }
        }

        public bool GioiTinh
        {
            get { return gioitinh;}
            set { gioitinh = value; }
        }
        public int SoLan { get; set; }
    }
}
