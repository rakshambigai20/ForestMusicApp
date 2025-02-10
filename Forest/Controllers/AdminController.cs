﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forest.Controllers
{

    public class AdminController : Controller
    {
        
        ApplicationDbContext context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminController(SignInManager<IdentityUser> signInManager)
        {
            context = new ApplicationDbContext();
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            return View(context.Users.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetRoles()
        {
            return View(context.Roles.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(IFormCollection collection)
        {
            IdentityRole role = new IdentityRole();
            role.Name = collection["RoleName"].ToString();
            role.NormalizedName = collection["RoleName"].ToString().ToUpper();
            context.Roles.Add(role);
            context.SaveChanges();
            return RedirectToAction("GetRoles");
        }

      
        public ActionResult ManageUserRoles()
        {
            FillInDropDowns();
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageUserRoles(string userName, string roleName)
        {
            IdentityUser user = _signInManager.UserManager.FindByNameAsync(userName).Result;
            await _signInManager.UserManager.AddToRoleAsync(user, roleName);
            FillInDropDowns();
            return RedirectToAction("AddUserToRole");
        }
        
        public ActionResult GetRolesForUser()
        {
            FillInDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserToRole(string userName, string roleName)
        {
            IdentityUser user = _signInManager.UserManager.FindByNameAsync(userName).Result;
            await _signInManager.UserManager.AddToRoleAsync(user, roleName);
            FillInDropDowns();
            return RedirectToAction("AddUserToRole");
        }

        void FillInDropDowns()
        {
            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select
                (
                uu => new SelectListItem
                {
                    Value = uu.UserName.ToString(),
                    Text = uu.UserName
                }
                ).ToList();
            ViewData["Users"] = userList;
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(
                rr => new SelectListItem
                {
                    Value = rr.Name.ToString(),
                    Text = rr.Name
                }
                ).ToList();
            ViewData["Roles"] = roleList;
        }
      
        public ActionResult AddUserToRole()
        {
            FillInDropDowns();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRolesForUser(string userName)
        {
            IdentityUser user =
                await _signInManager.UserManager.FindByNameAsync(userName);
            ViewData["UserName"] = user.UserName;
            IEnumerable<string> userRoles = 
                await _signInManager.UserManager.GetRolesAsync(user);
            return View("UserRoles", userRoles);
        }
        public ActionResult RemoveRoleForUser()
        {
            FillInDropDowns();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveRoleForUser(string userName, string roleName)
        {
            IdentityUser user = _signInManager.UserManager.FindByNameAsync(userName).Result;
            await _signInManager.UserManager.RemoveFromRoleAsync(user, roleName);
            FillInDropDowns();
            return RedirectToAction("RemoveRoleForUser");
        }
      
        public ActionResult Dashboard() 
        {
            return View();
        }


    }
}
