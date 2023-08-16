using Domain.Models;
using ConsoleAppProject.Controllers;
using Service.Helpers.Extentions;


while (true)
{
    ConsoleColor.DarkYellow.WriteConsole("Location Operations:  Create -1, GetAll - 2 ");
    LocationController locationController = new LocationController();

  Operation:  string operation = Console.ReadLine();

    int operationNum;
    bool isTrueOperation = int.TryParse(operation, out operationNum);

    if (isTrueOperation)
    {
        switch (operationNum)
        {
            case 1:
                locationController.Create();
                break;
            case 2:
                locationController.GetAll();
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