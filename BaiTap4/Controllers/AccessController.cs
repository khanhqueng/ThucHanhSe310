﻿using Microsoft.AspNetCore.Mvc;
using BaiTap4.Models;

namespace BaiTap4.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if(HttpContext.Session.GetString("Username") == null)
            {
                var u = db.TUsers.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    if(u.LoaiUser == 0)
                    {
                        HttpContext.Session.SetString("Username", u.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (u.LoaiUser == 1)
                    {
                        HttpContext.Session.SetString("Username", u.Username);
                        return RedirectToAction("Index", "HomeAdmin", new { area = "admin" });
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
    }
}
