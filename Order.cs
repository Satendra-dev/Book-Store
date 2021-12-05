using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Book_Store
{
    class Order : IDisposable
    {
        List<OrderItem> orderList;
        List<Book> bookCollection;
        decimal totalCost = 0.00m;
        int discountPercentage = 5;

        //mention the categories with discounted price applied 
        enum discountedCategory
        {
            Crime
        };
        public Order()
        {
            try
            {
                //get book collection database (using json file)            
                using (StreamReader r = new StreamReader("data-repository/book-collection.json"))
                {
                    string json = r.ReadToEnd();
                    bookCollection = JsonConvert.DeserializeObject<List<Book>>(json);
                }

                //get order items
                using (StreamReader rd = new StreamReader("data-repository/order-items.json"))
                {
                    string json = rd.ReadToEnd();
                    orderList = JsonConvert.DeserializeObject<List<OrderItem>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Something went wrong while reading data from json file. Error: {0}", ex.Message));
            }
        }
        public decimal GetTotalCost()
        {
            try
            {
                //if there are any items in the orderList, start billing
                if (orderList.Count > 0)
                {
                    //calculate cost of each item (book) in the order
                    foreach (OrderItem order in orderList)
                    {
                        if (order.bookTitle != null)
                        {
                            //find the book details from repository that matches the order item
                            var book = bookCollection.FirstOrDefault(x => x.title != null && x.title.Trim().ToLower().Equals(order.bookTitle.Trim().ToLower()));
                            if (book != null)
                            {
                                //add the book amount to total amount
                                totalCost = totalCost + getBookPrice(book);
                            }
                            else
                            {
                                Console.WriteLine(string.Format("No item found in the Book collection with book title -'{0}'", order.bookTitle));
                                Console.WriteLine("Billing other items...");
                            }
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Empty book title. Please review order items."));
                            Console.WriteLine("Billing other items...");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No item found in the order. Please add item to the OrderList");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Something went wrong while calculating the billing amount. Error: {0}", ex.Message));
            }
            return totalCost;
        }

        //this method returns the book price after calculating discount, if any
        public decimal getBookPrice(Book book)
        {
            //check if genre of the book falls under the discounted categories (5%). discountPercentage/100m to get precision decimal value.
            if (Enum.IsDefined(typeof(discountedCategory), book.category.name))
            {
                return book.price - (book.price * (discountPercentage / 100m));
            }
            else
            {
                return book.price;
            }

        }

        public void Dispose()
        {
            //dispose all the objects
        }
    }

    class OrderItem
    {
        public int quantity { get; set; }
        public string bookTitle { get; set; }
    }
}