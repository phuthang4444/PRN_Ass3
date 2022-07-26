﻿using BusinessObejct.Object;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        MemberRepository memberRepository = null;
        public LoginController() => memberRepository = new MemberRepository();

        [Route("login")]
        [Route("/")]
        [HttpGet]
        public IActionResult Login() => View();

        [Route("login")]
        [HttpPost]
        public IActionResult Login(Member member)
        {
            if (member.Email != null && member.Password != null)
            {
                if (member.Email.ToLower().Equals("admin@fstore.com") && member.Password.Equals("admin@@"))
                {
                    HttpContext.Session.SetInt32("ID", 1);
                    return RedirectToAction("Index", "Home"); 

                }
                Member loggedInUser = memberRepository.Login(member.Email, member.Password);
                if (loggedInUser != null)
                {
                    HttpContext.Session.SetInt32("ID", loggedInUser.MemberId);
                    return RedirectToAction("Index", "Profile");
                }
                ViewBag.Message = "Invalid Username or Password";
                return View();

            }
            else
            {
                ViewBag.Message = "Please enter Email and Password";
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ID");
            return RedirectToAction("Index");
        }
    }
}
