using System.Text;
using Aplication.Interfaces.Helpers;

namespace Infrastructure.Services;

public class CodeGeneratorService : ICodeGeneratorService
{
    private static readonly Random Random = new();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string GenerateMandaditoCode(int length)
    {
        var code = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            var index = Random.Next(Chars.Length);
            code.Append(Chars[index]);
        }

        return code.ToString();
    }
}