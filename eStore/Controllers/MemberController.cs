using BusinessObejct.Object;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository memberRepository = null;
        public MemberController() => memberRepository = new MemberRepository();

        // GET: MemberController
        public ActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("ID");
            if (id != 1) 
            { 
                return NotFound(); 
            }
            var members = memberRepository.GetMemberList();
            return View(members);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            { 
                return NotFound(); 
            }
            if (id == null) 
            { 
                return NotFound(); 
            }
            var member = memberRepository.GetMemberByID(id.Value);
            if (member == null) 
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            {
                return NotFound();
            }
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            {
                return NotFound();
            }
            try
            {
                string message;
                if (ModelState.IsValid) 
                { 
                    memberRepository.InsertMember(member); 
                }
                if (ValidEmailAddress(member.Email, out message)) 
                { 
                    throw new Exception(message); 
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(member);
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            {
                return NotFound();
            }

            if (id == null) 
            {
                return NotFound();
            }
            var member = memberRepository.GetMemberByID(id.Value);
            if (member == null) 
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) 
            {
                return NotFound();
            }
            try
            {
                string message;
                if (id != member.MemberId) 
                {
                    return NotFound();
                }
                if (ModelState.IsValid) 
                {
                    memberRepository.UpdateMember(member);
                }
                if (ValidEmailAddress(member.Email, out message)) 
                {
                    throw new Exception(message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int? id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) return NotFound();

            if (id == null) return NotFound();
            var member = memberRepository.GetMemberByID(id.Value);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var adminID = HttpContext.Session.GetInt32("ID");
            if (adminID != 1) return NotFound();
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            if (emailAddress.Length == 0)
            {
                errorMessage = "*Email address is required.";
                return false;
            }

            if (Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "*Email address must be valid email address format.";
            return false;
        }
    }
}
