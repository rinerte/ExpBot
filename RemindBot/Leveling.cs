using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBot
{
    internal class Leveling
    {
        public const int MAX_LEVEL = 130;
        public const int MAX_ROWS_PER_DAY = 52800;
        public const int MAX_ARTICLES_PER_DAY = 1000;
        public const int MAX_BOOKPAGES_PER_DAY = 2000;

        public static long ExpToNextLevel(int level)
        {
            if (level == 0) return 0;
            if (level > 50) return (long)(1.2 * ExpToNextLevel(level - 1));
            switch (level)
            {
                case 1: return 10000L;
                case 2: return 15000L;
                case 3: return 22500L;
                case 4: return 33750;
                case 5: return 50625;
                case 6: return 75938;
                case 7: return 113906;
                case 8: return 170859;
                case 9: return 256289;

                case 10: return 384434;
                case 11: return 576650;
                case 12: return 864976;
                case 13: return 1297463;
                case 14: return 1946195;
                case 15: return 2919293;
                case 16: return 4378939;
                case 17: return 6568408;
                case 18: return 9852613;
                case 19: return 14778919;

                case 20: return 22168378;
                case 21: return 33252567;
                case 22: return 49878851;
                case 23: return 74818276;
                case 24: return 112227415;
                case 25: return 168341122;
                case 26: return 252511683;
                case 27: return 378767524;
                case 28: return 568151287;
                case 29: return 852226930;

                case 30: return 1278340395;
                case 31: return 1917510592;
                case 32: return 2876265888;
                case 33: return 4314398833;
                case 34: return 6471598249;
                case 35: return 9707397374;
                case 36: return 14561096060;
                case 37: return 21841644091;
                case 38: return 32762466136;
                case 39: return 49143699204;

                case 40: return 73715548806;
                case 41: return 110573323209;
                case 42: return 165859984814;
                case 43: return 248789977221;
                case 44: return 373184965832;
                case 45: return 559777448748;
                case 46: return 839666173121;
                case 47: return 1259499259682;
                case 48: return 1889248889523;
                case 49: return 2833873334285;
                case 50: return 4250810001427;
                default: return -1;
            }
        }

        public static long CodeRowValue(int level)
        {
            if (level == 0) return 0;
            if (level > 50) return 8501620003;
            switch (level)
            {
                case 1: return 8;
                case 2: return 8;
                case 3: return 9;
                case 4: return 11;
                case 5: return 14;
                case 6: return 19;
                case 7: return 27;
                case 8: return 39;
                case 9: return 57;

                case 10: return 85;
                case 11: return 127;
                case 12: return 193;
                case 13: return 294;
                case 14: return 450;
                case 15: return 693;
                case 16: return 1071;
                case 17: return 1664;
                case 18: return 2592;
                case 19: return 4052;

                case 20: return 6352;
                case 21: return 9982;
                case 22: return 15722;
                case 23: return 24813;
                case 24: return 39235;
                case 25: return 62149;
                case 26: return 98602;
                case 27: return 156667;
                case 28: return 249268;
                case 29: return 397110;

                case 30: return 633391;
                case 31: return 1011382;
                case 32: return 1616630;
                case 33: return 2586609;
                case 34: return 4142377;
                case 35: return 6639639;
                case 36: return 10651088;
                case 37: return 17099314;
                case 38: return 27471398;
                case 39: return 44165556;

                case 40: return 71051338;
                case 41: return 114375324;
                case 42: return 184225969;
                case 43: return 296903713;
                case 44: return 478757237;
                case 45: return 772395009;
                case 46: return 1246746292;
                case 47: return 2013362629;
                case 48: return 3252838997;
                case 49: return 5257649971;
                case 50: return 8501620003; 
                default: return -1;
            }
        }

        public static long BookPageValue(int level)
        {
            if (level == 0) return 0;
            if (level > 50) return 442792708;
            switch (level)
            {
                case 1: return 52;
                case 2: return 39;
                case 3: return 39;
                case 4: return 44;
                case 5: return 53;
                case 6: return 66;
                case 7: return 85;
                case 8: return 111;
                case 9: return 148;

                case 10: return 200;
                case 11: return 273;
                case 12: return 375;
                case 13: return 520;
                case 14: return 724;
                case 15: return 1014;
                case 16: return 1425;
                case 17: return 2012;
                case 18: return 2851;
                case 19: return 4051;

                case 20: return 5773;
                case 21: return 8247;
                case 22: return 11808;
                case 23: return 16943;
                case 24: return 24355;
                case 25: return 35071;
                case 26: return 50583;
                case 27: return 73065;
                case 28: return 105683;
                case 29: return 153058;

                case 30: return 221934;
                case 31: return 322162;
                case 32: return 468142;
                case 33: return 680934;
                case 34: return 991360;
                case 35: return 1444553;
                case 36: return 2106640;
                case 37: return 3074556;
                case 38: return 4490470;
                case 39: return 6562994;

                case 40: return 9598379;
                case 41: return 14046408;
                case 42: return 20567954;
                case 43: return 30134445;
                case 44: return 44174357;
                case 45: return 64789057;
                case 46: return 95070898;
                case 47: return 139572170;
                case 48: return 204996624;
                case 49: return 301219530;
                case 50: return 442792708;
                default: return -1;
            }
        }
        public static long ArticleValue(int level)
        {
            return BookPageValue(level) * 2L;
        }
    }
}
