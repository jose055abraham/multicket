using System;
using System.Linq;
using System.Windows.Media;

namespace Multicket.Data.Common
{
    public sealed class Generate
    {
        static Random ran = new Random();

        public static string Colors
        {
            get
            {
                return Color.FromArgb(190,
                   (byte)ran.Next(0, 255),
                   (byte)ran.Next(0, 200),
                   (byte)ran.Next(0, 255)).ToString();
            }
        }

        public static int Folio
        {
            get
            {

                return Semilla();
            }
        }

        private static int Semilla()
        {
            string justNumbers = new string(Guid.NewGuid().ToString().Where(char.IsDigit).ToArray());
            int seed = int.Parse(justNumbers.Substring(0, 4));
            return seed;
        }
    }
}
