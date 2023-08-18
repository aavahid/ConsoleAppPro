using Domain.Models;
using ConsoleAppProject.Controllers;
using Service.Helpers.Extentions;

bool isRuning = true;
while (isRuning)
{
    ConsoleColor.Yellow.WriteConsole(
        "Main Operations:" +
        "\n1. Location Operations" +
        "\n2. Restaurant Operations" +
        "\n3. Product Operations" +
        "\n4. Exit");
    LocationController locationController = new LocationController();
    ProductController productController = new ProductController();
    RestaurantController restaurantController = new RestaurantController();

Operation: string operation = Console.ReadLine();

    int operationNum;
    bool isTrueOperation = int.TryParse(operation, out operationNum);

    if (isTrueOperation)
    {
        switch (operationNum)
        {
            case 1:
                locationController.LocationMenu();
                break;
            case 2:
                restaurantController.RestaurantMenu();
                break;
            case 3:
                productController.ProductMenu();
                break;
            case 4:
                isRuning = false;
                ConsoleColor.White.WriteConsole("Good Bye!");
                break;
            default:
                ConsoleColor.Red.WriteConsole("Choose correct operation format:");
                break;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Choose correct operation format:");
        goto Operation;
    }
}