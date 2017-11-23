using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class PokemonUSUM : PokemonSuMo
    {
        new internal const int RELEASED_POKEMON = 807;
        new internal const int RELEASED_FORMS = 168;

        new internal const int SIZE = 84;

        internal PokemonUSUM(short id, byte[] data) : base(id, data)
        {
            
        }
    }
}
