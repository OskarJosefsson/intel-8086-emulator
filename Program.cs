Console.WriteLine("Running Decoder");

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: dotnet run -- <input-file>");
    return;
}

var RegFieldDictionary = new Dictionary<(int reg, int w), string>
{
    //Table 4-10 page 263 https://codeberg.org/bolt/8086-Users-Manual/src/branch/main/INTEL_The-8086-Family-Users-Manual.pdf
    [(0, 0)] = "AL",
    [(1, 0)] = "CL",
    [(2, 0)] = "DL",
    [(3, 0)] = "BL",
    [(4, 0)] = "AH",
    [(5, 0)] = "CH",
    [(6, 0)] = "DH",
    [(7, 0)] = "BH",

    [(0, 1)] = "AX",
    [(1, 1)] = "CX",
    [(2, 1)] = "DX",
    [(3, 1)] = "BX",
    [(4, 1)] = "SP",
    [(5, 1)] = "BP",
    [(6, 1)] = "SI",
    [(7, 1)] = "DI"
};

string inputFile = args[0];
byte[] bytes = File.ReadAllBytes(inputFile);

for (int i = 0; i < bytes.Length; i += 2)
{
    Decode(bytes[i], bytes[i + 1]);
}


void Decode(byte first, byte second)
{
    int d = (first >> 1) & 1; // Shift the d bit to the rightmost position, 
    // then take the one furthest to the right to get the one originally in the second to the right position.
    int w = first & 1;        // Take the rightmost integer

    // int mod = (second >> 6) & 0b11;  // Shift 6 bits and take 2.Not needed, yet?
    int reg = (second >> 3) & 0b111; // Shift 3 bits and take 3.
    int rm = second & 0b111;         // Take the last 3 bits.
    // The fields are split as 2-3-3 bits.

    string regName = RegFieldDictionary[(reg, w)];
    string rmName = RegFieldDictionary[(rm, w)];

    if (d == 0)
    {
        (regName, rmName) = (rmName, regName);
    }

    string output = "mov " + regName + ", " + rmName;

    Console.WriteLine(output.ToLower());

}


