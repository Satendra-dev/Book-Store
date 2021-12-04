This Book-Store app is build on .Net 5
It returns 
- total cost of the books ordered
- total billing cost which would include 10% GST and delivery charges (if any).
- books with specific genres are billed at discounted price.

#pre-requisite for running the application on Visual Studio Code:
- .Net 5 SDK on the system
- C# Extention

#build and run instructions
- press crtl F5 or if you are using terminal then run command: dotnet run
- for the first time it will create .vscode folder for launch.json and tasks.json. Therefore you might need to run the above step again (crtl F5) to get the desired output.

#data-repository
- book-collection.json file is used as a repository for the book store which could be coming from the book-store database in real time
    you can add more book item into this file.
- order-items.json file is used as an input for oder to be placed and billed. 
    you can change order items to amend your order and then generate the new billing amount

#Assumptions
- Order values in problem statment has book title specified. therefore, I have used title while iterating the book collection. Ideally we should use some unique key (like bookId).
- I am assuming two different books cannot have same name with diff case Sensitivity, considering copyrights with book name.
    Therefore, I have used ToLower() and Trim() methods while looking for the book in book collection.
- discountedCategory enum can be used to add more categories to discount 5%.
- I am assuming discount price will be same amount (stated 5% in problem statement) to all the discount categories. Therefore, used one variable discountPercentage.


