using BackupCafeApp.DomainEntityModel;
using CafeApp.Persistence.Repositories;
using System.Net;
using System.Web.Mvc;
using static BackupCafeApp.DomainEntityModel.User;

namespace BackupCafeApp.Web.Controllers
{
    public class EmployeeController : Controller
    {

        // private AppDbContext db = new AppDbContext();
        private UserRepository UserRepo = new UserRepository();
        private TableRepository TableRepo = new TableRepository();
        
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["EmployeeId"] != null)
            {
                return View(TableRepo.GetTables());
            }
            return RedirectToAction("EmployeeLogin");
        }

        public ActionResult EmployeeLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeLogin(User user)
        {
            var CheckUser = UserRepo.GetUserbyUsername(user);

            if (CheckUser != null)
            {
                if (CheckUser.Password != user.Password)
                {
                    ViewBag.Error = "Invalid user";
                    return View();
                }
                if(CheckUser.Password == user.Password && CheckUser.Role == Roles.Cashier)
                {
                    Session["EmployeeId"] = CheckUser.UserId;
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Error = "Invalid user";
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult EditTableStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = TableRepo.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTableStatus(Table table)
        {
            if (ModelState.IsValid)
            {
                if (table.Status == Table.TableStatus.Empty)
                {
                    table.UserId = null;
                    TableRepo.UpdateTable(table);
                }
                TableRepo.UpdateTable(table);
                return RedirectToAction("Index");
            }
            return View(table);
        }
        public ActionResult Logout()
        {
            Session["EmployeeId"] = null;
            return RedirectToAction("EmployeeLogin");
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
