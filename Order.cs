using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Book_Store
{
    class Order
    {
        int quantity;
        string bookTitle;
        List<Order> orderList;
        List<Book> bookCollection;
        enum discountedCategory
        {
            Crime
        }

        decimal totalCost = 0.00m;
        decimal billingCost = 0.00m;
        decimal deliveryCost = 5.95m;


        public Order(int quantity, string title)
        {
            this.quantity = quantity;
            this.bookTitle = title;
        }

        public Order()
        {
            //get book collection database (using json file)            
            using (StreamReader r = new StreamReader("book-collection.json"))
            {
                string json = r.ReadToEnd();
                bookCollection = JsonConvert.DeserializeObject<List<Book>>(json);
            }
        }

        public void getOrderList()
        {
            //add book items to the list of order for billing
            orderList = new List<Order>();            
            orderList.Add(new Order(1, "Unsolved murders"));
            orderList.Add(new Order(1, "A Little Love Story"));
            orderList.Add(new Order(1, "Heresy"));
            orderList.Add(new Order(1, "Jack the Ripper"));
            orderList.Add(new Order(1, "The Tolkien Years"));
        }

        public decimal GetTotalCost()
        {
            //fill orderList with items in order
            getOrderList();

            //if there are any items in the orderList, start billing
            if (orderList.Count > 0)
            {
                foreach (Order order in orderList)
                {
                    var book = bookCollection.FirstOrDefault(x => x.title == order.bookTitle);
                    if (book != null)
                    {
                        //add the book amount to total amount
                        //check if genre of the book falls under the discounted categories (5%) 
                        if (Enum.IsDefined(typeof(discountedCategory), book.category.name))
                        {
                            totalCost = totalCost + (book.price - (book.price * 0.05m));
                        }
                        else
                        {
                            totalCost = totalCost + book.price;
                        }
                    }
                    else
                    {
                        Console.WriteLine(string.Format("No item found in the Book collection with book title -'{0}'" , order.bookTitle));
                        Console.WriteLine("Billing other items...");
                    }
                }
            }
            else
            {
                Console.WriteLine("No item found in the order. Please add item to the OrderList");
            }

            return totalCost;
        }

    }
}