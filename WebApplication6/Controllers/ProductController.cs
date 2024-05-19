using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductsModel _context;
        private readonly ViewModel _viewModel;

        public ProductController()
        {
            _context = new ProductsModel();
            _viewModel = new ViewModel();
        }

        public ActionResult AddProduct(int id = 0)
        {
            List<ProductsModel> Product = new List<ProductsModel>();

            if(id > 0)
            {
                var GetProduct = _context.GetProduct(id).Rows;
                
                if(GetProduct != null && GetProduct[0] != null)
                {
                    ViewBag.product = GetProduct[0];
                } else
                {
                    TempData["error"] = true;
                    TempData["message"] = "Product Not found!";

                    return RedirectToAction("ProductList", "Product");
                }

                
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddProductHandle(ProductsModel product)
        {
            /*if (ModelState.IsValid)
            {*/
                if (product.ImageFile != null && product.ImageFile.ContentLength > 0)
                {
                    /*TempData["error"] = true;
                    TempData["message"] = "Invalid image";

                    return RedirectToAction("AddProduct");*/

                    var fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    var extension = Path.GetExtension(product.ImageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    product.Image = "/Images/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    product.ImageFile.SaveAs(fileName);
                }

                var Image = product.Image;

                _context.SetProduct(product.Name, Image, product.Price, product.Quantity, product.ProductId);
                return RedirectToAction("ProductList");
            /*} else
            {
                TempData["error"] = true;
                TempData["message"] = "ModelState Is Not Valid!";

                return RedirectToAction("AddProduct");
            }*/
        }

        public ActionResult ProductList()
        {
            List<ProductsModel> productList = new List<ProductsModel>();

            var Products = _context.GetProducts();

            foreach(DataRow row in Products.Rows)
            {
                ProductsModel product = new ProductsModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Name = row["Name"].ToString(),
                    Image = row["Image"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Quantity = Convert.ToInt32(row["Quantity"])
                };

                productList.Add(product);
            }

            var model = new ViewModel()
            {
                  Products = productList
            };

            return View(model);
        }

        public ActionResult RemoveProduct(int id)
        {
            int Remove = _context.RemoveProduct(id);

            if (Remove == 0) {
                TempData["error"] = true;
                TempData["message"] = "Failed";
            } else
            {
                TempData["error"] = false;
                TempData["message"] = "Success";
            }

            return RedirectToAction("ProductList", "Product");
        }
    }
}