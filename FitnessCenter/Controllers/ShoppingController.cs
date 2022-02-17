using FitnessCenter.Models;
using FitnessCenter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ApplicationDbContext db = new ApplicationDbContext();
        private List<OrderDetailsModel> ListOfShoppingCartModels = new List<OrderDetailsModel>();
        public ActionResult Index()
        {
            var products = db.Items;
            return View(products.ToList());
        }
        [HttpPost]
        public JsonResult Index(int ItemId)
        {
            OrderDetailsModel objShoppingCartModel = new OrderDetailsModel();
            ItemViewModel objitem = db.Items.Single(model => model.ItemId == ItemId);
           
            if (Session["CartCounter"] != null)
            {
                ListOfShoppingCartModels = Session["CartItem"] as List<OrderDetailsModel>;

            }
          
    
                if (ListOfShoppingCartModels.Any(model => model.ItemId == ItemId))
            {
                objShoppingCartModel = ListOfShoppingCartModels.Single(model => model.ItemId == ItemId);
                objShoppingCartModel.Quantity = objShoppingCartModel.Quantity + 1;
                objShoppingCartModel.Total = objShoppingCartModel.Quantity * objShoppingCartModel.UnitPrice;
            }
            else
            {
                objShoppingCartModel.ItemId = ItemId;
                objShoppingCartModel.ImagePath = objitem.ImagePath;
                objShoppingCartModel.ItemName = objitem.ItemName;
                objShoppingCartModel.Quantity = 1;
                objShoppingCartModel.Total = objitem.ItemPrice;
                objShoppingCartModel.UnitPrice = objitem.ItemPrice;
                ListOfShoppingCartModels.Add(objShoppingCartModel);

            }
            Session["CartCounter"] = ListOfShoppingCartModels.Count;
            Session["CartItem"] = ListOfShoppingCartModels;
            return Json(data: new { Success = true, Counter = ListOfShoppingCartModels.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart()
        {
            ListOfShoppingCartModels = Session["CartItem"] as List<OrderDetailsModel>;
            return View(ListOfShoppingCartModels);
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
           
            if (ModelState.IsValid)
            {
                int orderId = 0;
                ListOfShoppingCartModels = Session["CartItem"] as List<OrderDetailsModel>;
                OrderModel orderObj = new OrderModel()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = string.Format("0:ddmmyyyyHHmmsss", DateTime.Now)

                };
                db.Orders.Add(orderObj);
                db.SaveChanges();
                orderId = orderObj.OrderId;
                foreach (var item in ListOfShoppingCartModels )
                {
                 
                    OrderDetailsModel objOrderDetail = new OrderDetailsModel();

                    objOrderDetail.Total = item.Total;
                    objOrderDetail.ItemId = item.ItemId;
                    objOrderDetail.OrderId = orderId;
                    objOrderDetail.Quantity = item.Quantity;
                    objOrderDetail.UnitPrice = item.UnitPrice;
                    objOrderDetail.ImagePath = item.ImagePath;
                    db.OrderDetails.Add(objOrderDetail);
                    db.SaveChanges();

                }
                Session["CartCounter"] = null;
                Session["CartItem"] = null;
            }
            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromCart(int? productId)
        {
            List<OrderDetailsModel> cart = (List<OrderDetailsModel>)Session["CartItem"];
            foreach (var item in cart)
            {
                if (item.OrderDetailId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["CartItem"] = cart;
            return Redirect("ShoppingCart");
        }

        public ActionResult Search()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Items.Where(a => a.ItemName.Contains(searchName) || a.Description.Contains(searchName)).ToList();
            return View(result);
        }

    }
}
