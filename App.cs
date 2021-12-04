using System;

namespace Book_Store
{
    class App
    {
        //Genre for which discounts are available
        
        static void Main(string[] args)
        {
            decimal totalCost = 0.00m;
            decimal billingCost = 0.00m;
            decimal deliveryFee = 5.95m;         

            var order = new Order();
            totalCost = order.GetTotalCost();

            //add 10% GST on the totalCost
            billingCost = (0.1m * totalCost) + totalCost;

            //Order above $20 gets free delivery else $5.95 delivery charges applied.
            if(totalCost < 20)      
            {
                billingCost = billingCost + deliveryFee;
            }
            
            Console.WriteLine("Total Cost: $" + totalCost);
            Console.WriteLine("Total Billing Cost with GST: $" + billingCost);
        } 
    }
}
