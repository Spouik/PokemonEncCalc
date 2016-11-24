using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class PokemonHGSS : Pokemon
    {
        internal const int RELEASED_POKEMON = 493;
        internal const int RELEASED_FORMS = 47;

        internal const int SIZE = 44;

        // alternative forms
        internal override List<Pokemon> Forms { get; }

        // Accessing data..
        internal override Type Type1 { get { return (Type)Data[6]; } set { Data[6] = (byte)value; } }
        internal override Type Type2 { get { return (Type)Data[7]; } set { Data[7] = (byte)value; } }
        internal override int Height { get { return BitConverter.ToUInt16(Data, 36); } set { BitConverter.GetBytes((ushort)value).CopyTo(Data, 36); } }
        internal override int Weight { get { return BitConverter.ToUInt16(Data, 38); } set { BitConverter.GetBytes((ushort)value).CopyTo(Data, 38); } }
        internal override byte CatchRate { get { return Data[8]; } set { Data[8] = value; } }
        internal override byte GenderRatio { get { return Data[18]; } set { Data[18] = value; } }
        // Accessing Base stats data..
        internal override byte HP { get { return Data[0]; } set { Data[0] = value; } }
        internal override byte Atk { get { return Data[1]; } set { Data[1] = value; } }
        internal override byte Def { get { return Data[2]; } set { Data[2] = value; } }
        internal override byte SpA { get { return Data[4]; } set { Data[4] = value; } }
        internal override byte SpD { get { return Data[5]; } set { Data[5] = value; } }
        internal override byte Spe { get { return Data[3]; } set { Data[3] = value; } }

        internal PokemonHGSS(short id, byte[] data)
        {
            if (data.Length != SIZE) throw new ArgumentException();

            NatID = id;
            Data = new byte[SIZE];
            data.CopyTo(Data, 0);
            if (Data[6] > 9) Data[6]--;
            if (Data[7] > 9) Data[7]--;
            Forms = new List<Pokemon>();

        }

        internal void addForm(PokemonHGSS pokemon)
        {
            Forms.Add(pokemon);
        }

        internal override int FormCount()
        {
            return Forms.Count;
        }

        internal override int getNbReleased()
        {
            return RELEASED_POKEMON;
        }
    }
}
