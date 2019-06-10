namespace SmartPlate.API.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            str = str.Replace("_", string.Empty);
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}