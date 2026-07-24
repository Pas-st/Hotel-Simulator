using System;

class Program
{
  static void Main()
  {
    //Print(0b_01_10_11_00_1101_0010__0011_0101_1010_1110);
    Console.WriteLine("Enter 8-digit hex code (or 'exit' to quit):");
    while (Console.ReadLine() is string s && s != "exit")
      if (s.Length == 8 && uint.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out uint d))
        Print(d);
  }

  static void Print(uint d)
  {
    ushort cfg = (ushort)(d >> 16), col = (ushort)d;
    var floor = C((byte)(cfg >> 4 & 15));
    var use = Uses[cfg & 15];
    var colors = WallColors(col);
    string[] dir = { "North", "East", "South", "West" };

    Console.WriteLine($"Room: Color {floor}; Use {use.n} - {use.d}");

    for (int i = 0; i < 4; i++)
    {
      int t = cfg >> (14 - i * 2) & 3;
      Console.WriteLine($"{dir[i]} Wall: {colors[i]} - {new[] { "Nothing", "Wall", "Door", "Window" }[t]}");
    }
  }

  static Color[] WallColors(ushort c) =>
  [
      C((byte)(c&15)),
        C((byte)(c>>4&15)),
        C((byte)(c>>8&15)),
        C((byte)(c>>12&15))
  ];

  static Color C(byte i) => Colors[i < 16 ? i : 0];

  static readonly Color[] Colors =
  [
      new(172,172,172),new(180,40,40),new(180,100,40),new(180,160,40),
        new(160,180,40),new(60,150,60),new(40,140,100),new(40,140,160),
        new(40,90,160),new(70,60,140),new(130,60,140),new(170,80,120),
        new(120,80,50),new(150,120,90),new(100,100,100),new(220,220,200)
  ];

  static readonly (string n, string d)[] Uses =
  [
      ("Unassigned","No room type"),
        ("Guest Room","Sleep and stay"),
        ("Suite","Premium guest room"),
        ("Bathroom","Washing facilities"),
        ("Reception","Check-in area"),
        ("Restaurant","Food area"),
        ("Kitchen","Food preparation"),
        ("Storage","Supplies"),
        ("Staff Room","Employees"),
        ("Conference Room","Meetings"),
        ("Gym","Fitness"),
        ("Spa","Wellness"),
        ("Laundry Room","Laundry"),
        ("Office","Administration"),
        ("Maintenance Room","Repairs"),
        ("Lobby","Public area")
  ];

  readonly record struct Color(byte r, byte g, byte b)
  {
    public override string ToString() => $"0x{r:X2}{g:X2}{b:X2}";
  }
}