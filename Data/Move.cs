using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class Move
    {
        public Type MoveType { get; set; }
        public byte Category { get; set; }
        public byte contestType { get; set; }

        public byte PP { get; set; }
        public byte Power { get; set; }
        public byte Accuracy { get; set; }


        public Move(byte[] data)
        {
            // Gen 3 : no Category (set to 255)

            if(data.Length == 5)
            {
                MoveType = (Type)data[0];
                Category = 255;
                contestType = data[1];
                PP = data[2];
                Power = data[3];
                Accuracy = data[4];
            }

            // Other gens
            if (data.Length == 6)
            {
                MoveType = (Type)data[0];
                Category = data[1];
                contestType = data[2];
                PP = data[3];
                Power = data[4];
                Accuracy = data[5];
            }
        }

    }
}
