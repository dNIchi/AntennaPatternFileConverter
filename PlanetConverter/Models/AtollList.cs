using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
   public class AtollList
        {
            public string Name { get; set; }
            public string Name2 { get; set; }
            public string Gain { get; set; }
            public string Manuf { get; set; }
            public string Comm { get; set; }
            public string Patt { get; set; }
            public string PET { get; set; }
            public string Beam { get; set; }
            public string Fmin { get; set; }
            public string Fmax { get; set; }
            public string Freq { get; set; }
            public string VWidth { get; set; }
            public string FTB { get; set; }
            public string Tilt { get; set; }
            public string Hwidth { get; set; }
            public string Fam { get; set; }
            public string Dim { get; set; }
            public string Weight { get; set; }
            public string PPD { get; set; }

            public override string ToString( )
            {
                return Name + "\t" + 
                       Name2 + "\t" + 
                       Gain + "\t" + 
                       Manuf + "\t" + 
                       Comm + "\t" + 
                       Patt + "\t" + 
                       PET + "\t" + 
                       Beam + "\t" + 
                       Fmin + "\t" + 
                       Fmax + "\t" + 
                       VWidth + "\t" + 
                       FTB + "\t" + 
                       Tilt + "\t" + 
                       Hwidth + "\t" + 
                       Fam + "\t" + 
                       Dim + "\t" + 
                       Weight + "\t" + 
                       PPD + "\r\n";
            }
        }
    }
