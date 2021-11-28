using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TH5.Models
{
    public class DataContext
    {
        readonly IConfiguration configuration;
        public string ConnectionString { get; set; }

        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Them canHo

        public int Create(CanHoModel ch)
        {
            int count = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "insert into CanHo value(?mach, ?tench)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("macanho", ch.MaCanHo.ToString());
                cmd.Parameters.AddWithValue("tenchuho", ch.TenChuHo.ToString());
                cmd.ExecuteNonQuery();
                count++;
            }
            return count;
        }

        // Liet ke can ho

        public List<NhanVienModel> getNhanVien(int soLan)
        {
            List<NhanVienModel> list = new List<NhanVienModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select nv.manhanvien, tennhanvien, sum(a.lanthu) as solan" +
                               " from nhanvien nv,  (select manhanvien, mathietbi, macanho, max(lanthu) as lanthu from nv_bt group by manhanvien, mathietbi, macanho) a" +
                               " where nv.manhanvien=a.manhanvien" +
                               "  group by nv.manhanvien, tennhanvien" +
                               " having sum(a.lanthu) >= @solan";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@solan", soLan);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVienModel()
                        {
                            MaNhanVien = reader["manhanvien"].ToString(),
                            TenNhanVien = reader["tennhanvien"].ToString(),
                            SoLan = Convert.ToInt32(reader["solan"].ToString()),
                        });

                    }
                }
            }
            return list;
        }

        // Liet ke ten nhan vien
        public List<NhanVienModel> getTenNhanVien()
        {
            List<NhanVienModel> list = new List<NhanVienModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select manhanvien, tennhanvien from nhanvien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVienModel()
                        {
                            MaNhanVien = reader["manhanvien"].ToString(),
                            TenNhanVien = reader["tennhanvien"].ToString(),
                        });

                    }
                }
            }
            return list;
        }

        // Liet ke danh sach thiet bi nhan vien da sua
        public List<NV_BT> getListTB(string MaNhanVien)
        {
            List<NV_BT> list = new List<NV_BT>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from nv_bt where manhanvien =@manhanvien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhanvien", MaNhanVien);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NV_BT()
                        {
                            MaNhanVien = reader["manhanvien"].ToString(),
                            MaThietBi = reader["mathietbi"].ToString(),
                            MaCanHo = reader["macanho"].ToString(),
                            LanThu = Convert.ToInt32(reader["lanthu"]),
                            NgayBaoTri = Convert.ToDateTime(reader["ngaybaotri"].ToString())
                        });

                    }
                }
            }
            return list;
        }

        public NV_BT getNVBT(string manhanvien, string mathietbi, string macanho, int lanthu)
        {
            NV_BT nv = new NV_BT();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from nv_bt where  manhanvien = @manhanvien and mathietbi =@mathietbi and macanho = @macanho and lanthu=@lanthu  ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhanvien", manhanvien); //Binding
                cmd.Parameters.AddWithValue("@mathietbi", mathietbi);
                cmd.Parameters.AddWithValue("@macanho", macanho);
                cmd.Parameters.AddWithValue("@lanthu", lanthu);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nv.MaNhanVien = reader["manhanvien"].ToString();
                        nv.MaThietBi = reader["mathietbi"].ToString();
                        nv.MaCanHo = reader["macanho"].ToString();
                        nv.LanThu = Convert.ToInt32(reader["lanthu"]);
                        nv.NgayBaoTri = Convert.ToDateTime(reader["ngaybaotri"]);
                    }
                }
            }
            return nv;
        }

        public int Update(NV_BT nv)
        {
            int count = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "update nv_bt set ngaybaotri =@ngaybaotri where manhanvien = @manhanvien and mathietbi =@mathietbi and macanho = @macanho and lanthu=@lanthu";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhanvien", nv.MaNhanVien); //Binding
                cmd.Parameters.AddWithValue("@mathietbi", nv.MaThietBi);
                cmd.Parameters.AddWithValue("@macanho", nv.MaCanHo);
                cmd.Parameters.AddWithValue("@lanthu", nv.LanThu);
                cmd.Parameters.AddWithValue("ngaybaotri", Convert.ToDateTime(nv.NgayBaoTri));
                cmd.ExecuteNonQuery();
                count++;
            }
            return count;
        }
        public int Delete(string manhanvien, string mathietbi, string macanho, int lanthu)
        {
            int count = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "delete from nv_bt where manhanvien = @manhanvien and mathietbi =@mathietbi and macanho = @macanho and lanthu=@lanthu  ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@manhanvien", manhanvien); //Binding
                cmd.Parameters.AddWithValue("@mathietbi", mathietbi);
                cmd.Parameters.AddWithValue("@macanho", macanho);
                cmd.Parameters.AddWithValue("@lanthu", lanthu);
                cmd.ExecuteNonQuery();
                count++;
            }
            return count;
        }
    }
}