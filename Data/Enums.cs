
namespace PokemonEncCalc
{                              // 0           1         2       3       4      5   6     7      8      9     10     11       12       13     14     15    16     17
    public enum Type : byte { Normal = 0, Fighting, Flying, Poison, Ground, Rock, Bug, Ghost, Steel, Fire, Water, Grass, Electric, Psychic, Ice, Dragon, Dark, Fairy }
    public enum Version : byte { Red = 1, Blue, Yellow, Gold, Silver, Crystal, Ruby, Sapphire, Emerald, FireRed, LeafGreen, Diamond, Pearl, Platinum, HeartGold, SoulSilver, Black, White, Black2, White2, X, Y, OmegaRuby, AlphaSapphire, Sun, Moon, UltraSun, UltraMoon}
    public enum EncounterType : byte { Walking, Surf, RockSmash, OldRod, GoodRod, SuperRod, DarkGrass, ShakingGrass, RipplingSurf, RipplingFish, TallGrass, RedFlowers, YellowFlowers, PurpleFlowers, ShallowWater, Diving, Hordes}
    public enum Ability : byte { None, Static, MagnetPull, Pressure, CuteCharm, Intimidate}
    public enum Language : int { None = 0, English, French, German, Spanish, Italian, Japanese, Korean }
    public enum Ball : int { PokeBall = 0, GreatBall, UltraBall, SafariBall, PremierBall, LuxuryBall, DiveBall, NetBall, TimerBall, RepeatBall, NestBall, HealBall, QuickBall, DuskBall, LureBall, FriendBall, MoonBall, LevelBall, HeavyBall, FastBall, LoveBall, SportBall, BeastBall }
}
