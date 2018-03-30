using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class Fixed20_12
    {
        public int Number { get; set; }
        public int IntegerPart { get; set; }
        public int DecimalPart { get; set; }
        public bool Sign { get; set; } // True = positive, false = negative

        public Fixed20_12(int value = 0)
        {
            Sign = (value & 0x80000000) != 0;
            IntegerPart = value & 0x7FFFF000 >> 12;
            DecimalPart = value & 0x00000FFF;
        }

        public Fixed20_12(decimal value = 0)
        {
            Sign = value >= 0;
            value = Math.Abs(value);
            IntegerPart = (int)Math.Floor(value);

            Number = (int)(value * 4096);
        }

        public Fixed20_12(int integerPart, int decimalPart)
        {
            bool positive = integerPart >= 0;
            if ((integerPart & 0x7FFFFFFF) >= 0x00080000) integerPart = 0x0007FFFF;
            decimalPart &= 0x00000FFF;
            Number = (integerPart << 20) + decimalPart;
            unchecked
            {
                if (!positive) Number |= (int)0x80000000;
            }
        }

        public static Fixed20_12 operator+(Fixed20_12 left, Fixed20_12 right)
        {
            return new Fixed20_12(left.Number + right.Number);
        }


    }
}
