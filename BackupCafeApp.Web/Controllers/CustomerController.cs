using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BackupCafeApp.DomainEntityModel;
using CafeApp.Persistence.Repositories;
using static BackupCafeApp.DomainEntityModel.Table;
using static BackupCafeApp.DomainEntityModel.User;

namespace BackupCafeApp.Web.Controllers
{
    public class CustomerController : Controller
    {
       //private AppDbContext db = new AppDbContext();
        private UserRepository UserRepo = new UserRepository();
        private TableRepository TableRepo = new TableRepository();
        private MenuRepository MenuRepo = new MenuRepository();
        private CartRepository CartRepo = new CartRepository();
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(MenuRepo.GetMenus());

            }
            return RedirectToAction("CustomerLogin");
        }

        public ActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerLogin(User user)
        {
            var CheckUser = UserRepo.GetUserbyUsername(user);
            

            if (CheckUser != null)
            {
                if (CheckUser.Password != user.Password)
                {
                    ViewBag.Error = "Invalid user";
                    return View();
                }

                if(CheckUser.Password == user.Password && CheckUser.Role == Roles.Customer)
                {
                    Session["UserId"] = CheckUser.UserId;
                    return RedirectToAction("Index");
                }  
            }
            ViewBag.Error = "Invalid user";
            return View();
        }

        [HttpPost]
        public ActionResult AddMenuToCart(int id)
        {
            Cart cart = new Cart();
            cart.UsersId = Convert.ToInt32(Session["UserId"]);
            var getFoodId = MenuRepo.GetMenu(id);
            var getCart = CartRepo.GetCart(id);

            if (getCart != null)
            {

                getCart.Quantity += 1;
                getCart.TotalAmount = getCart.Quantity * getFoodId.Price;

                CartRepo.UpdateCart(getCart);
                ViewBag.Success = "Added Successfully";
                return Json(JsonRequestBehavior.AllowGet);
            }

            else
            {
                cart.MenusId = id;
                cart.Price = getFoodId.Price;
                cart.Quantity = 1;
                cart.TotalAmount = getFoodId.Price * cart.Quantity;
                cart.FoodName = getFoodId.FoodName;
                CartRepo.AddCart(cart);
                ViewBag.Success = "Added Successfully";
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewCart()
        {
            if (Session["UserId"] != null)
            {
                int id = Convert.ToInt32(Session["UserId"]);
                return View(CartRepo.GetUserCarts(id));
            }
            return RedirectToAction("CustomerLogin");
        }

        [HttpPost]
        public ActionResult AddQuantity(int id)
        {
            var getCart = CartRepo.GetCart(id);
            int getCartId = getCart.MenusId;
            var getFoodId = MenuRepo.GetMenu(getCartId);
            if (getCart != null)
            {
                getCart.Quantity += 1;
                getCart.TotalAmount = getCart.Quantity * getFoodId.Price;
                CartRepo.UpdateCart(getCart);

                ViewBag.Success = "Added Successfully";
                return Json(JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = false, Msg = "Invalid Item" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeductQuantity(int id)
        {
            var getCart = CartRepo.GetCart(id);
            int getCartId = getCart.MenusId;
            var getFoodId = MenuRepo.GetMenu(getCartId);
            if (getCart != null)
            {
                getCart.Quantity -= 1;
                getCart.TotalAmount = getCart.Quantity * getFoodId.Price;
                if (getCart.Quantity == 0)
                {
                    CartRepo.DeleteCart(getCart);
                }
                CartRepo.UpdateCart(getCart);
                ViewBag.Success = "Remove Successfully";
                return Json( JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = false, Msg = "Invalid Item" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFood(int id)
        {
            var getCart = CartRepo.GetCart(id);
            int getCartId = getCart.MenusId;
            var getFoodId = MenuRepo.GetMenu(getCartId);

            CartRepo.DeleteCart(getCart);
            ViewBag.Success = "Added Successfully";
            return Json(new { Message = true, Msg = getFoodId.FoodName + " remove from cart" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmOrder()
        {
            Table table = new Table();
            if (Session["UserId"] != null)
            {
                int TableId = 1;
                var GetTable =TableRepo.GetTable(TableId);
                int Id = Convert.ToInt32(Session["UserId"]);
                var User = UserRepo.GetUser(Id);
                if (User != null)
                {

                    if (table.TableId == TableId && table.Status == TableStatus.Occupied)
                    {
                        TableId++;

                        if (table.TableId == TableId && table.Status != TableStatus.Occupied)
                        {
                            
                            GetTable.UserId = User.UserId;
                            GetTable.Status = TableStatus.Occupied;
                            TableRepo.UpdateTable(GetTable);
                        }
                    }
                    GetTable.UserId = User.UserId;
                    GetTable.Status = TableStatus.Occupied;
                    TableRepo.UpdateTable(GetTable);
                }
                return View(GetTable);
            }
            return RedirectToAction("CustomerLogin");
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            return RedirectToAction("CustomerLogin");
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