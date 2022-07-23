using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BusinessObejct.Object;

namespace eStore.Controllers
{
    public class ProfileController : Controller
    {
        IMemberRepository memberRepository = null;
        public ProfileController() => memberRepository = new MemberRepository();
        [HttpGet]
        public IActionResult Index()
        {
            var userID = HttpContext.Session.GetInt32("ID");
            if (userID == null || userID < 0)
            {
                return NotFound();
            }
            var details = memberRepository.GetMemberByID(userID.Value);
            return View(details);
        }

        public ActionResult Edit()
        {
            var userID = HttpContext.Session.GetInt32("ID");
            if (userID == null || userID < 0)
            {
                return NotFound();
            }
            var member = memberRepository.GetMemberByID(userID.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            try
            {
                var userID = HttpContext.Session.GetInt32("ID");
                if (userID == null || userID < 0)
                {
                    return NotFound();
                }
                var id = HttpContext.Session.GetInt32("ID");
                if (id != member.MemberId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    memberRepository.UpdateMember(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
