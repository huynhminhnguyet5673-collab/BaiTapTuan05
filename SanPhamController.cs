using LTWeb5_Bai03.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb5_Bai03.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        private string connectionString = "LAPTOP-OH9DSGKI\\SQLPROD2012";
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LAPTOP-OH9DSGKI\\SQLPROD2012"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<SanPham> sanPhams = new List<SanPham>();

            SqlDataAdapter da = new SqlDataAdapter("Select * from SANPHAM", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSP = int.Parse(dr["MASP"].ToString());
                sp.TenSP = dr["TENSP"].ToString();
                sp.DuongDan = dr["DUONGDAN"].ToString();
                sp.Gia = decimal.Parse(dr["GIA"].ToString());
                sp.MoTa = dr["MOTA"].ToString();
                sp.MaLoai = int.Parse(dr["MALOAI"].ToString());

                sanPhams.Add(sp);
            }
            return View(sanPhams);
        }
        public ActionResult HienThiLoaiSanPham()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<Loai> cats = new List<Loai>();

            SqlDataAdapter da = new SqlDataAdapter("Select * from LOAI", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var l = new Loai();
                l.MaLoai = int.Parse(dr["MALOAI"].ToString());
                l.TenLoai = dr["TENLOAI"].ToString();

                cats.Add(l);
            }
            return View(cats);
        }
        public ActionResult HienThiSanPham()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<SanPham> sanPhams = new List<SanPham>();

            SqlDataAdapter da = new SqlDataAdapter("Select * from SANPHAM", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.MaSP = int.Parse(dr["MASP"].ToString());
                sp.TenSP = dr["TENSP"].ToString();
                sp.DuongDan = dr["DUONGDAN"].ToString();
                sp.Gia = decimal.Parse(dr["GIA"].ToString());
                sp.MoTa = dr["MOTA"].ToString();
                sp.MaLoai = int.Parse(dr["MALOAI"].ToString());

                sanPhams.Add(sp);
            }
            return View(sanPhams);
        }
        public ActionResult HienThiSanPhamTheoLoai(int maLoai)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<SanPham> dsSP = new List<SanPham>();

            SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT SP.MASP, SP.TENSP, SP.DUONGDAN, SP.GIA, SP.MOTA, SP.MALOAI
                FROM SANPHAM SP, LOAI L 
                WHERE SP.MALOAI = L.MALOAI
                AND SP.MALOAI = @MALOAI", con);

            da.SelectCommand.Parameters.AddWithValue("@MALOAI", maLoai);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MaSP"] != DBNull.Value)
                {
                    var sp = new SanPham
                    {
                        MaSP = int.Parse(dr["MASP"].ToString()),
                        TenSP = dr["TENSP"].ToString(),
                        DuongDan = dr["DUONGDAN"].ToString(),
                        Gia = decimal.Parse(dr["GIA"].ToString()),
                        MoTa = dr["MOTA"].ToString(),
                        MaLoai = int.Parse(dr["MALOAI"].ToString())
                    };
                    dsSP.Add(sp);
                }
            }

            return View(dsSP);
        }
        public ActionResult DanhSachSanPham(string search)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<SanPham> dsSP = new List<SanPham>();

            SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT MASP, TENSP, DUONGDAN, GIA, MOTA, MALOAI
                FROM SANPHAM", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham
                {
                    MaSP = int.Parse(dr["MASP"].ToString()),
                    TenSP = dr["TENSP"].ToString(),
                    DuongDan = dr["DUONGDAN"].ToString(),
                    Gia = decimal.Parse(dr["GIA"].ToString()),
                    MoTa = dr["MOTA"].ToString(),
                    MaLoai = int.Parse(dr["MALOAI"].ToString())
                };

                dsSP.Add(sp);
            }


            if (!string.IsNullOrEmpty(search))
            {
                dsSP = dsSP
                    .Where(sp => sp.TenSP.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            ViewBag.Search = search;
            return View(dsSP);
        }
        public ActionResult DanhSachLoai(string search)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            List<Loai> dsLoai = new List<Loai>();

            SqlDataAdapter da = new SqlDataAdapter("SELECT MALOAI, TENLOAI FROM LOAI", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var loai = new Loai
                {
                    MaLoai = int.Parse(dr["MALOAI"].ToString()),
                    TenLoai = dr["TENLOAI"].ToString()
                };
                dsLoai.Add(loai);
            }


            if (!string.IsNullOrEmpty(search))
            {
                dsLoai = dsLoai
                    .Where(l => l.TenLoai.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            ViewBag.Search = search;
            return View(dsLoai);
        }

    }
}
