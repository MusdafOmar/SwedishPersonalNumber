using PersonalNumber.Core;

Console.WriteLine("Swedish Personal Identity Number Validator");
Console.WriteLine("Enter a personal number (YYMMDD-XXXX / YYYYMMDDXXXX). Type 'exit' to quit.");

while (true)
{
    Console.Write("> ");
    string? input = Console.ReadLine();

    if (input is null)
        continue;

    if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    bool valid = PersonalNumberValidator.IsValid(input);

    Console.WriteLine(valid ? "✅ Valid" : "❌ Invalid");
}
