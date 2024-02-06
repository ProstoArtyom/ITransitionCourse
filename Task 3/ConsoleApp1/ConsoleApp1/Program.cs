using ConsoleApp1.HelperClasses;
using ConsoleApp1.Services;

if (!ArgsValidation.IsArgsValid(args, out string message))
{
    Console.WriteLine(message);
    return;
}

var service = new GameService(args);
service.ShowMenu();