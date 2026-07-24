using System;
using System.Collections.Generic;

namespace HelloWorld
{
  class Program
  {
    static void Main(string[] args)
    {
      PrintRoomConfiguration();
    }

    /// <summary>
    /// 00 = nothing, 01 = wall, 10 = door, 11 = window <br/>
    /// For the first 8 bits. <br/>
    /// 
    /// The next 4 bits are for the floor color. <br/>
    /// The last 4 bits are for the room use. <br/>
    /// 
    /// <br/>
    /// 16 Bit Wandfarben: <br/>
    /// Bits 0-3   = North <br/>
    /// Bits 4-7   = East <br/>
    /// Bits 8-11  = South <br/>
    /// Bits 12-15 = West <br/>
    /// Jede Farbe ist ein Index von 0-15. <br/>
    /// 
    /// </summary>
    static void PrintRoomConfiguration(ushort roomConfig = 0b_01_10_11_00_1101_0010, ushort roomWallColor = 0b_0011_0101_1010_1110)
    {
      ColorRGB floorColor = Color((byte)((roomConfig >> 4) & 0b_1111));
      RoomUse roomUse = GetRoomUse((byte)(roomConfig & 0b_00_00_00_00_0000_1111));
      ColorRGB[] colors = DecodeWallColors(roomWallColor);
      string[] directions = { "North", "East", "South", "West" };

      Console.WriteLine($"\n\nRoom Configuration: Color: 0x{floorColor.r:X2}{floorColor.g:X2}{floorColor.b:X2}; Use: {roomUse.Name} - {roomUse.Description}");
      for (int i = 0; i < 4; i++)
      {
        int wt = (roomConfig >> (14 - i * 2)) & 0b11;
        string wtName = wt switch { 1 => "Wall", 2 => "Door", 3 => "Window", _ => "Nothing", };
        Console.WriteLine($"{directions[i]} Wall: Color: 0x{colors[i].r:X2}{colors[i].g:X2}{colors[i].b:X2} - Wall Type: {wtName}");
      }
    }

    static ColorRGB[] DecodeWallColors(ushort roomWallColor)
    {
      ColorRGB[] result = new ColorRGB[4];


      // Die unteren 4 Bit
      result[0] = Color((byte)(roomWallColor & 0b1111));

      // Bits 4-7
      result[1] = Color((byte)((roomWallColor >> 4) & 0b1111));

      // Bits 8-11
      result[2] = Color((byte)((roomWallColor >> 8) & 0b1111));

      // Bits 12-15
      result[3] = Color((byte)((roomWallColor >> 12) & 0b1111));


      return result;
    }


    static ColorRGB Color(byte colorIndex)
    {
      ColorRGB[] Colors =
      {
                new ColorRGB() { r = 172, g = 172, b = 172 }, // Standard
                new ColorRGB() { r = 180, g = 40,  b = 40  }, // Rot
                new ColorRGB() { r = 180, g = 100, b = 40  }, // Orange
                new ColorRGB() { r = 180, g = 160, b = 40  }, // Ocker

                new ColorRGB() { r = 160, g = 180, b = 40  }, // Oliv
                new ColorRGB() { r = 60,  g = 150, b = 60  }, // Grün
                new ColorRGB() { r = 40,  g = 140, b = 100 }, // Türkis
                new ColorRGB() { r = 40,  g = 140, b = 160 }, // Cyan

                new ColorRGB() { r = 40,  g = 90,  b = 160 }, // Blau
                new ColorRGB() { r = 70,  g = 60,  b = 140 }, // Violett
                new ColorRGB() { r = 130, g = 60,  b = 140 }, // Lila
                new ColorRGB() { r = 170, g = 80,  b = 120 }, // Rosa

                new ColorRGB() { r = 120, g = 80,  b = 50  }, // Braun
                new ColorRGB() { r = 150, g = 120, b = 90  }, // Sand
                new ColorRGB() { r = 100, g = 100, b = 100 }, // Grau
                new ColorRGB() { r = 220, g = 220, b = 200 }  // Creme
            };

      if (colorIndex >= Colors.Length)
      {
        return Colors[0];
      }

      return Colors[colorIndex];
    }


    struct ColorRGB
    {
      public byte r;
      public byte g;
      public byte b;
    }

    static RoomUse GetRoomUse(byte roomUseIndex)
    {
      RoomUse[] roomUses =
      {
          new RoomUse() { Name = "Unassigned", Description = "No room type has been assigned yet." },

          new RoomUse() { Name = "Guest Room", Description = "A room for hotel guests to sleep and stay." },
          new RoomUse() { Name = "Suite", Description = "A larger premium guest room with additional space and comfort." },
          new RoomUse() { Name = "Bathroom", Description = "A room with facilities for washing and personal hygiene." },
          new RoomUse() { Name = "Reception", Description = "The main area where guests check in, check out, and receive assistance." },

          new RoomUse() { Name = "Restaurant", Description = "A room where guests can eat meals and socialize." },
          new RoomUse() { Name = "Kitchen", Description = "A room for preparing food and beverages." },
          new RoomUse() { Name = "Storage", Description = "A room for storing supplies, equipment, and inventory." },
          new RoomUse() { Name = "Staff Room", Description = "A room for hotel employees to rest and organize their work." },

          new RoomUse() { Name = "Conference Room", Description = "A room for meetings, presentations, and business events." },
          new RoomUse() { Name = "Gym", Description = "A room with equipment for exercise and fitness." },
          new RoomUse() { Name = "Spa", Description = "A room for relaxation, wellness treatments, and beauty services." },
          new RoomUse() { Name = "Laundry Room", Description = "A room for washing and maintaining linens and towels." },

          new RoomUse() { Name = "Office", Description = "A room for hotel management and administration." },
          new RoomUse() { Name = "Maintenance Room", Description = "A room for repairs, tools, and technical equipment." },
          new RoomUse() { Name = "Lobby", Description = "A public area for guests to wait, relax, and socialize." }
      };

      if (roomUseIndex >= roomUses.Length)
      {
        return roomUses[0];
      }

      return roomUses[roomUseIndex];
    }

    struct RoomUse
    {
      public string Name;
      public string Description;
    }

  }

}