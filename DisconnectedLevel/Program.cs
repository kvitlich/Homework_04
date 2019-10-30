using System;
using System.Data;
using System.Data.SqlTypes;

namespace DisconnectedLevel
{
  class Program
  {
    static void Main(string[] args)
    {
      DataSet ShopDb = new DataSet("ShopDb");

      DataTable Orders = new DataTable("Orders");
      Orders.Columns.Add(new DataColumn("Id", SqlDbType.UniqueIdentifier.GetType()));
      Orders.Columns["Id"].AllowDBNull = false;
      Orders.PrimaryKey = new DataColumn[] { Orders.Columns["Id"] };
      Orders.Columns.Add(new DataColumn("CustomerId", SqlDbType.UniqueIdentifier.GetType()));
      Orders.Columns.Add(new DataColumn("OrderDate", SqlDbType.DateTime.GetType()));

      DataTable Customers = new DataTable("Customers");
      Customers.Columns.Add(new DataColumn("Id", SqlDbType.UniqueIdentifier.GetType()));
      Customers.Columns["Id"].AllowDBNull = false;
      Customers.PrimaryKey = new DataColumn[] { Customers.Columns["Id"] };
      Customers.Columns.Add(new DataColumn("FullName", SqlDbType.NVarChar.GetType()));
      Customers.Columns.Add(new DataColumn("Address", SqlDbType.NVarChar.GetType()));
      Customers.Columns.Add(new DataColumn("PhoneNumber", SqlDbType.NVarChar.GetType()));
      Customers.Columns.Add(new DataColumn("Email", SqlDbType.NVarChar.GetType()));


      DataTable Employees = new DataTable("Employees");
      Employees.Columns.Add(new DataColumn("Id", SqlDbType.UniqueIdentifier.GetType()));
      Employees.PrimaryKey = new DataColumn[] { Employees.Columns["Id"] };
      Employees.Columns["Id"].AllowDBNull = false;
      Employees.Columns.Add(new DataColumn("OrderId", SqlDbType.UniqueIdentifier.GetType()));
      Employees.Columns.Add(new DataColumn("FullName", SqlDbType.NVarChar.GetType()));
      Employees.Columns.Add(new DataColumn("PhoneNumber", SqlDbType.NVarChar.GetType()));


      DataTable OrderDetails = new DataTable("OrderDetails");
      OrderDetails.Columns.Add(new DataColumn("Id", SqlDbType.UniqueIdentifier.GetType()));
      OrderDetails.Columns["Id"].AllowDBNull = false;
      OrderDetails.PrimaryKey = new DataColumn[] { OrderDetails.Columns["Id"] };
      OrderDetails.Columns.Add(new DataColumn("OrderId", SqlDbType.UniqueIdentifier.GetType()));
      OrderDetails.Columns.Add(new DataColumn("ProductId", SqlDbType.UniqueIdentifier.GetType()));
      OrderDetails.Columns.Add(new DataColumn("PriceMain", SqlDbType.Float.GetType()));
      OrderDetails.Columns.Add(new DataColumn("CountProducts", SqlDbType.Int.GetType()));


      DataTable Products = new DataTable("Products");
      Products.Columns.Add(new DataColumn("Id", SqlDbType.UniqueIdentifier.GetType()));
      Products.Columns["Id"].AllowDBNull = false;
      Products.PrimaryKey = new DataColumn[] { Products.Columns["Id"] };
      Products.Columns.Add(new DataColumn("Name", SqlDbType.NVarChar.GetType()));
      Products.Columns.Add(new DataColumn("Price", SqlDbType.Float.GetType()));
      Products.Columns.Add(new DataColumn("HaveIn", SqlDbType.Int.GetType()));


      ShopDb.Tables.Add(Orders);
      ShopDb.Tables.Add(Customers);
      ShopDb.Tables.Add(Products);
      ShopDb.Tables.Add(Employees);
      ShopDb.Tables.Add(OrderDetails);

      var ordersOrderDetailsParent = new DataColumn[] { Orders.Columns["Id"] };
      var ordersOrderDetailsChild = new DataColumn[] { OrderDetails.Columns["OrderId"] };
      DataRelation ordersOrderDetails = new DataRelation("OrderDetails_Orders", ordersOrderDetailsParent, ordersOrderDetailsChild);

      var productsOrderDetailsParent = new DataColumn[] { Products.Columns["Id"] };
      var productsOrderDetailsChild = new DataColumn[] { OrderDetails.Columns["ProductId"] };
      DataRelation productsOrderDetails = new DataRelation("OrderDetails_Products", productsOrderDetailsParent, productsOrderDetailsChild);

      var customersOrdersParent = new DataColumn[] { Customers.Columns["Id"] };
      var customersOrdersChild = new DataColumn[] { Orders.Columns["CustomerId"] };
      DataRelation customersOrders = new DataRelation("Orders_Customer", customersOrdersParent, customersOrdersChild);

      var ordersEmployeesParent = new DataColumn[] { Orders.Columns["Id"] };
      var ordersEmployeesChild = new DataColumn[] { Employees.Columns["OrderId"] };
      DataRelation ordersEmployees = new DataRelation("Employees_Orders", ordersEmployeesParent, ordersEmployeesChild);

      ShopDb.Relations.Add(ordersOrderDetails);
      ShopDb.Relations.Add(productsOrderDetails);
      ShopDb.Relations.Add(customersOrders);
      ShopDb.Relations.Add(ordersEmployees);
    
    }
  }
}
