using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Helpers;

namespace WebApplication6.Models
{
    public class ProductsModel
    {
        private readonly Database _database;

        public ProductsModel()
        {
            _database = new Database();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set;  }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public DataTable GetProduct(int ProductId)
        {
            string sql = "SELECT * FROM products WHERE ProductId = @productid";

            var parameters = new List<SqlParameter>(){
                new SqlParameter("@productid", ProductId)
            };

            return _database.Query(sql, parameters);
        }

        public int SetProduct(string name, string image, decimal price, int quantity, int productid = 0)
        {
            string sql = "";

            var Parameters = new List<SqlParameter>()
            {
                new SqlParameter("@name", name),
                new SqlParameter("@image", image),
                new SqlParameter("@price", price),
                new SqlParameter("@quantity", quantity)
            };

            if (productid > 0)
            {
                sql = "UPDATE products SET Name = @name, Image = @image, Price = @price, Quantity = @quantity WHERE ProductId = @productid; ";
                Parameters.Add(
                    new SqlParameter("@productid", productid)
                );
            }
                else
            {
                sql = "INSERT INTO products (Name, Image, Price, Quantity) VALUES (@name, @image, @price, @quantity); ";
            }

            return _database.Execute(sql,   Parameters);
        }

        public DataTable GetProducts()
        {
            string sql = "SELECT * FROM products";

            return _database.Query(sql);
        }

        public int RemoveProduct(int ProductId)
        {
            string sql = "DELETE FROM products WHERE ProductId = @productid";

            var parameters = new List<SqlParameter>(){
                new SqlParameter("@productid", ProductId),
            };

            return _database.Execute(sql, parameters);
        }
    }
}