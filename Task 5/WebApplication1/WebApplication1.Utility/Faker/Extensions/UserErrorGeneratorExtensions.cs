using System.Reflection;
using WebApplication1.Models;

namespace WebApplication1.Utility.Faker.Extensions
{
    internal static class UserErrorGeneratorExtensions
    {
        private static Random _random;

        private static User GenerateErrorUser(this User user, float factor)
        {
            var errorsAmount = GetErrorsAmount(factor);
            for (var i = 0; i < errorsAmount; i++)
            {
                ModifyRandomProperty(user);
            }

            return user;
        }

        public static IEnumerable<User> GenerateErrorUsers(this IEnumerable<User> users, float factor, int seed)
        {
            _random = new Random(seed);
            foreach (var user in users)
            {
                yield return user.GenerateErrorUser(factor);
            }
        }

        private static int GetErrorsAmount(float factor)
        {
            int errorsAmount = (int)factor;
            errorsAmount += _random.NextDouble() < factor - errorsAmount ? 1 : 0;
            return errorsAmount;
        }

        private static string RemoveRandomSymbol(this string line)
        {
            if (line.Length < 2) return line;
            return line.Remove(_random.Next(line.Length), 1);
        }

        private static string AddRandomSymbol(this string line)
        {
            if (line.Length > 30) return line;
            var symbol = line[_random.Next(line.Length)];
            return line.Insert(_random.Next(line.Length), symbol.ToString());
        }

        private static string SwapRandomAdjacentSymbols(this string line)
        {
            if (line.Length < 2) return line;
            int index1 = _random.Next(line.Length - 1), index2 = index1 + 1;
            var lineArray = line.ToCharArray();
            (lineArray[index1], lineArray[index2]) = (lineArray[index2], lineArray[index1]);
            return string.Join("", lineArray);
        }

        private static Func<string, string>[] ErrorGenerationFuncs =
        {
            RemoveRandomSymbol, AddRandomSymbol, SwapRandomAdjacentSymbols
        };
        private static Func<string, string> GetRandomErrorGeneratingFunc()
        {
            return ErrorGenerationFuncs[_random.Next(ErrorGenerationFuncs.Length)];
        }

        private static PropertyInfo GetRandomProperty()
        {
            var properties = typeof(User)
                .GetProperties()
                .Where(u => u.PropertyType == typeof(string))
                .ToList();

            return properties[_random.Next(properties.Count)];
        }

        private static string GetPropertyValue(this PropertyInfo property, User user)
        {
            return (string)property.GetValue(user);
        }

        private static string GetModifiedPropertyValue(this PropertyInfo property, User user)
        {
            var propertyValue = property.GetPropertyValue(user);
            return GetRandomErrorGeneratingFunc().Invoke(propertyValue);
        }

        private static void SetPropertyValue(this PropertyInfo property, User user, string propertyValue)
        {
            property.SetValue(user, propertyValue);
        }

        private static void ModifyRandomProperty(User user)
        {
            var randomProperty = GetRandomProperty();
            var modifiedPropertyValue = randomProperty.GetModifiedPropertyValue(user);
            randomProperty.SetPropertyValue(user, modifiedPropertyValue);
        }
    }
}
