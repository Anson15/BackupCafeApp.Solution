using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BackupCafeApp.DomainEntityModel;
using CafeApp.Persistence;
using CafeApp.Persistence.Repositories;
using static BackupCafeApp.DomainEntityModel.User;

namespace BackupCafeApp.Web.Controllers
{
    public class AdminController : Controller
    {
        private UserRepository UserRepo = new UserRepository();
        private CartRepository CartRepo = new CartRepository();
        private MenuRepository MenuRepo = new MenuRepository();
        private TableRepository TableRepo = new TableRepository();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View(UserRepo.GetUsers());
        }

        // GET: Users/Details/5
        public ActionResult DetailsOfUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(User user)
        {
            var CheckUser = UserRepo.GetUserbyUsername(user);

            if (CheckUser != null)
            {
                if (CheckUser.Password != user.Password)
                {
                    ViewBag.Error = "Invalid user";
                    return View();
                }
                if (CheckUser.Password==user.Password&&CheckUser.Role==Roles.Admin)
                {
                    Session["Id"] = CheckUser.UserId;
                    return RedirectToAction("Index");
                }
               
            }
            ViewBag.Error = "Invalid user";
            return View();
        }

        public ActionResult CreateUser()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            var findUserinDatabase = UserRepo.GetUserbyUsername(user);
            if (findUserinDatabase != null)
            {
                return ViewBag.Error = "User name is exist";
            }
            UserRepo.AddUser(user);
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {
            //var updateUser = db.Users.Where(u => u.UserId == user.UserId).SingleOrDefault();
            var findUserinDatabase = UserRepo.GetUserbyUsername(user);
            if (findUserinDatabase != null)
            {
                return ViewBag.Error = "Username is exist";
            }
            UserRepo.UpdateUser(user);
            return RedirectToAction("Index");

        }

        // GET: Users/Delete/5
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = UserRepo.GetUser(id);
            UserRepo.DeleteUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult ViewAllMenu()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View(MenuRepo.GetMenus());
        }

        public ActionResult AddMenu()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMenu(Menu menu)
        {
            if (menu.Price <= 0)
            {
                return ViewBag.Error = "Price cannot lower than RM 0.10";   
            }
            string filename = Path.GetFileNameWithoutExtension(menu.ImageFile.FileName);
            string extension = Path.GetExtension(menu.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            menu.Image = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            menu.ImageFile.SaveAs(filename);
            MenuRepo.AddMenu(menu);
            return RedirectToAction("ViewAllMenu");
        }

        public ActionResult EditMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = MenuRepo.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost]
        public ActionResult EditMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.Price <= 0)
                {
                    ViewBag.Error = "Price cannot less than RM 0.10";
                }
                string filename = Path.GetFileNameWithoutExtension(menu.ImageFile.FileName);
                string extension = Path.GetExtension(menu.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                menu.Image = "~/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                menu.ImageFile.SaveAs(filename);
                MenuRepo.UpdateMenu(menu);
                return RedirectToAction("ViewAllMenu");
            }
            return View(menu);
        }

        public ActionResult DeleteMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = MenuRepo.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMenuConfirmed(int id)
        {
            Menu menu = MenuRepo.GetMenu(id);
            MenuRepo.DeleteMenu(menu);
            return RedirectToAction("Index");
        }

        public ActionResult DetailsOfMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = MenuRepo.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        public ActionResult AddTable()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddTable(Table table)
        {
            TableRepo.AddTable(table);
            return RedirectToAction("ViewTableStatus");
        }

        public ActionResult ViewTableStatus()
        {
            if (Session["Id"] != null)
            {
                return View(TableRepo.GetTables());
            }
            return RedirectToAction("AdminLogin");
        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            return RedirectToAction("AdminLogin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

