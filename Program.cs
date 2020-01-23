using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class Program
    {
        static void Main(string[] args)
        {
            NORTHWNDEntities nWind = new NORTHWNDEntities();
            /************************Employees wholives in USA and age>50************************/
            /*   var Emp = from n in nWind.Employees  
                             join or in nWind.Orders  on n.EmployeeID equals or.EmployeeID
                         // where n.Country == "USA"
                         // where EntityFunctions.DiffYears(n.BirthDate,DateTime.Now) >50   //who are age>50
                        where or.ShipCountry.Contains("Belgium") //Ship to Belgium
                         select new
                         {
                             name = n.LastName + "," + n.FirstName,
                             n.Address,
                             n.City,
                             n.Region
                             ,or.ShipCountry
                            //, Age=EntityFunctions.DiffYears(n.BirthDate, DateTime.Now)

                         };
               */
            /********************************Employee and customers who lives in Brussels and Shipped by company name Speedy express***************/
            /*  var Emp =(from e in nWind.Employees
                        join O in nWind.Orders on e.EmployeeID equals O.EmployeeID
                        join C in nWind.Customers on O.CustomerID equals C.CustomerID
                        join S in nWind.Shippers on O.ShipVia equals S.ShipperID
                        where S.CompanyName == "Speedy Express"
                        && C.City == "Bruxelles"
                         select new
                        {
                            empname = e.LastName + "," + e.FirstName,
                             CustName = C.ContactName
                        }).Distinct();
              */
            /************************************Employees who sold below products***************************************/
            //var Emp = from e in nWind.Employees
            //          join O in nWind.Orders on e.EmployeeID equals O.EmployeeID
            //          join Or in nWind.Order_Details on O.OrderID equals Or.OrderID
            //          join p in nWind.Products on Or.ProductID equals p.ProductID
            //          where p.ProductName == "Gravad Lax" || p.ProductName == "Mishi Kobe Niku"
            //          select new
            //          {
            //              e.Title,
            //              name = e.LastName + "," + e.FirstName
            //          };
            /**********************************Employee name and title which they refer to ,if not display null***************/
            //var Emp = from e in nWind.Employees
            //          join M1 in nWind.Employees on e.ReportsTo equals M1.EmployeeID into M
            //          from M2 in M.DefaultIfEmpty()
            //          select new
            //          {
            //              ManagerTitle = (M2.Title == null ? String.Empty:M2.Title)
            //             ,ManagerName = (M2.FirstName== null ? String.Empty : M2.LastName+", "+M2.FirstName)
            //          };
            /***********************************Customer,Product,Supplier name who live in london and below suppliers*********/
            //var Emp = (from c in nWind.Customers
            //          join O in nWind.Orders on c.CustomerID equals O.CustomerID
            //          join Or in nWind.Order_Details on O.OrderID equals Or.OrderID
            //          join p in nWind.Products on Or.ProductID equals p.ProductID
            //          join s in nWind.Suppliers on p.SupplierID equals s.SupplierID
            //          where (s.CompanyName == "Pavlova, Ltd." || s.CompanyName == "Karkki Oy") && (c.City == "London")
            //          select new
            //          {
            //              CustomerName=c.ContactName,
            //              ProductName = p.ProductName,
            //              SupplierName = s.CompanyName
            //          }).Distinct();
            /*********************************** name of products that were bought or sold by people who live in London*/
            //var prod1 = (from P in nWind.Products
            //           from D in P.Order_Details
            //           join O in nWind.Orders on D.OrderID equals O.OrderID
            //           join E in nWind.Employees on O.EmployeeID equals E.EmployeeID
            //           join C in nWind.Customers on O.CustomerID equals C.CustomerID
            //           where (E.City == "London") || (C.City == "London")
            //           select new { P.ProductName}).Distinct();
            //Another way
            //var Prod = (from p in nWind.Products
            //            join or in nWind.Order_Details on p.ProductID equals or.ProductID
            //            join O in nWind.Orders on or.OrderID equals O.OrderID
            //            join E in nWind.Employees on O.EmployeeID equals E.EmployeeID
            //            join C in nWind.Customers on O.CustomerID equals C.CustomerID
            //            where (E.City == "London") || (C.City == "London")
            //            select new 
            //            { p.ProductName }).Distinct();
            //Another way with union
            var Prod1 = (from p in nWind.Products
                         join or in nWind.Order_Details on p.ProductID equals or.ProductID
                         join O in nWind.Orders on or.OrderID equals O.OrderID
                         join E in nWind.Employees on O.EmployeeID equals E.EmployeeID
                         // join C in nWind.Customers on O.CustomerID equals C.CustomerID
                         where (E.City == "London")
                         select new { p.ProductName }).Distinct().
                         Union
                         (from p in nWind.Products
                          join or in nWind.Order_Details on p.ProductID equals or.ProductID
                          join O in nWind.Orders on or.OrderID equals O.OrderID
                          // join E in nWind.Employees on O.EmployeeID equals E.EmployeeID
                          join C in nWind.Customers on O.CustomerID equals C.CustomerID
                          where (C.City == "London")
                          select new
                          { p.ProductName }).Distinct();
            foreach (var e in Prod1)
            {
            Console.WriteLine(e);

           // Console.WriteLine(Prod1.Count());
            }
            Console.Read();            
        }
    }
}
