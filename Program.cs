Console.WriteLine("Running Decoder");


if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: dotnet run -- <input-file>");
    return;
}

string inputFile = args[0];
byte[] bytes = File.ReadAllBytes(inputFile);

for (int i = 0; i < bytes.Length; i += 2)
{
    Decode(bytes[i], bytes[i + 1]);
}

void Decode(byte first, byte second)
{
    int d = (first >> 1) & 1;
    int w = first & 1;

    int mod = (second >> 6) & 0b11;
    int reg = (second >> 3) & 0b111;
    int rm = second & 0b111;
}