namespace DomainSpace.Common.Util;

/// <summary>
/// String util
/// </summary>
public class StringUtil
{
    private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";
    private const string SpecialCharacters = "!@#$%&*";
    private static readonly Random random = new();

    /// <summary>
    /// Generate random string
    /// </summary>
    /// <param name="length">Minimum length is 4</param>
    /// <returns>Random string with the given length</returns>
    public static string GenerateRandomString(int length)
    {
        if (length < 4)
        {
            throw new ArgumentException("Length can't be less than 4 characters.");
        }

        var stringBuilder = new StringBuilder();

        var allCharacters = LowercaseLetters + UppercaseLetters + Numbers;
        while (stringBuilder.Length < length)
        {
            stringBuilder.Append(GetRandomCharacter(allCharacters));
        }

        return stringBuilder.ToString();
    }

    private static string GetRandomCharacter(string category)
    {
        int index = random.Next(category.Length);
        return category[index].ToString();
    }
}
