using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny
{
    public class Candidate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Solution { get; set; }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, F: {Solution}";
        }


    }
}
