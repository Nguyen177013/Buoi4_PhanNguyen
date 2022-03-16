using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buoi4_PhanNguyen.Models;
namespace Buoi4_PhanNguyen.Controllers
{
    public class GioHangController : Controller
    {
        MyDataDataContext context = new MyDataDataContext();
        public List<GioHang> layGioHang()
        {
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["Giohang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int id,string strURL)
        {
            List<GioHang> lstGioHang = layGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.masach == id);
            if(sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.soluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(n => n.soluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                tsl = lstGioHang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tt = lstGioHang.Sum(n => n.thanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> gioHang = layGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> lstgioHang = layGioHang();
            GioHang sanpham = lstgioHang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                lstgioHang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<GioHang> lstGiohang = layGioHang();
            GioHang sanpham = lstGiohang.SingleOrDefault(p => p.masach == id);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGioHang = layGioHang();
            lstGioHang.Clear();
            return RedirectToAction("GioHang");
        }
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
    }
}