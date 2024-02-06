namespace ConsoleApp1.HelperClasses;
public static class ArgsValidation
{
    public static string Example =>
        "Examples:\n" +
        "Rock Paper Scissors\n" +
        "1 2 3 4 5\n";

    public static bool IsArgsValid(string[] args, out string message)
    {
        if (args.Length < 3)
        {
            message = $"There needs to be at least 3 arguments!\n{Example}";
            return false;
        }

        if (args.Length % 2 == 0)
        {
            message = $"There can be only odd number of arguments!\n{Example}";
            return false;
        }

        if (args.GroupBy(x => x)
            .Any(x => x.Count() > 1))
        {
            message = $"Arguments can't be repeated!\n{Example}";
            return false;
        }

        message = "";
        return true;
    }
}
