using System;

namespace Book_Store
{
    class App
    {

        static void Main(string[] args)
        {
            try
            {
                decimal totalCost = 0.00m;
                decimal billingCost = 0.00m;
                decimal deliveryFee = 5.95m;

                //using scope helps disposing the object after use
                using (var order = new Order())
                {
                    totalCost = order.GetTotalCost();
                }

                //add 10% GST on the totalCost
                billingCost = (0.1m * totalCost) + totalCost;

                //Order above $20 gets free delivery else $5.95 delivery charges applied.
                if (totalCost < 20)
                {
                    billingCost = billingCost + deliveryFee;
                }

                //print round of cost
                Console.WriteLine("Total Cost of items : $" + Math.Round(totalCost, 2));
                Console.WriteLine("Total Billing Cost with Tax: $" + Math.Round(billingCost, 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("App threw expection : " + ex.Message);
            }
        }
    }
}
