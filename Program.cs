Console.WriteLine("Running Decoder");


if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: dotnet run -- <input-file>");
    return;
}

string inputFile = args[0];
Decoder decoder = new Decoder();

byte[] bytes = File.ReadAllBytes(inputFile);

for (int i = 0; i < bytes.Length; i += 2)
{
    Console.WriteLine($"Byte {i}: {bytes[i]:X2}");
    decoder.Decode(bytes[i], bytes[i + 1]);
}


public class Decoder
{
    public void Decode(byte first, byte second)
    {
        int d = (first >> 1) & 1; // Shift the d bit to the rightmost position, then mask out all other bits.
        int w = first & 1;        // Mask out all bits except the rightmost bit, which is w.

        int mod = (second >> 6 ) & 0b11; // shift 6 and take 2
        int reg = (second >> 3) & 0b111; // shift 3 and take 3
        int rm = (second) & 0b111; // take 3
        // as in 2-3-3 
        // NÄSTA STEG: Konvertera fälten till instruktioner.
    }
}