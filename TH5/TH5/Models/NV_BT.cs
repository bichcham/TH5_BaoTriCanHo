using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TH5.Models
{
    public class NV_BT
    {
        private string maNhanVien;
        private string maThietBi;
        private string maCanHo;
        private int lanThu;
        private DateTime ngayBaoTri;

        public NV_BT()
        {
        }

        public NV_BT(string maNV, string maTB, string maCH, int lanThu, DateTime ngBT)
        {
            this.maNhanVien = maNV;
            this.maThietBi = maTB;
            this.maCanHo = maCH;
            this.lanThu = lanThu;
            this.ngayBaoTri = ngBT;
        }

        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string MaThietBi { get => maThietBi; set => maThietBi = value; }
        public string MaCanHo { get => maCanHo; set => maCanHo = value; }
        public int LanThu { get => lanThu; set => lanThu = value; }
        public DateTime NgayBaoTri { get => ngayBaoTri; set => ngayBaoTri = value; }
    }
}
