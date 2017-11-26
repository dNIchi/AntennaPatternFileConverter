using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class Odyssey
        {
        public void ConvertToOdysseyBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var cont1 = -1;
                var cont2 = -1;
                var totalWords = ConvertDep.Words.Length;
                var saveState = "";
                var saveState2 = "";

                if (totalWords <= 0) ConvertDep.ErrorLog.Add( $"Odyssey {totalWords} Empty\nCheck file source is correct" );
                var start = "NAME\t" + ConvertDep.Words[1] + "\r\n" +
                                "FREQUENCY\t" + ConvertDep.Words[5] + "\r\n" +
                                "BEAM_WIDTH\t" + ConvertDep.Words[7] + "\r\n" +
                                "GAIN\t" + ConvertDep.Gain + "\r\n" +
                                "TILT\t" + ConvertDep.Words[15] + "\r\n" +
                                "CLASS\t" + ConvertDep.Family + "\r\n" +
                                "FREQUENCY_BAND" + "\t" + ConvertDep.LowerFrequency + " - " + ConvertDep.HighFrequency + "\r\n" +
                                "ELECTRICAL_TILT\t" + ConvertDep.ElectricalTilt + "\r\n" +
                                "HORIZONTAL	360";
                for (var i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    saveState += cont1 + "\t" + (Convert.ToDouble( newValue )).ToString( "0.0" ) + "\r\n";
                    }
                for (var i = 743; i <= totalWords - 1; i += 2)
                    {
                    cont2++;
                    var newvalue = ConvertDep.Words[i];
                    saveState2 += cont2 + "\t" + (Convert.ToDouble( newvalue )).ToString( "0.0" ) + "\r\n";
                    }

                ConvertDep.CurrentHorzConversionResults = start + "\r\n" + saveState + "VERTICAL\t360\r\n" + saveState2;
                ConvertDep.SuccessLog.Add( $"Odyssey {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Odyssey Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }

            }
        public void DownloadOdysseyBatch( )
            {
            var fileName = ConvertDep.CurrentFileName;

            try
                {
                // string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                //var commaValue = value.Replace( "\t", "\r\n" );
                // string[] words = commaValue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var odysseyDir = ConvertDep.TargetDirectory + $"Odyssey";

                if (!Directory.Exists( odysseyDir )) Directory.CreateDirectory( odysseyDir );
                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( odysseyDir + fileName ))
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{odysseyDir}\\{fileName}", value );
                        tempVar.Close( );
                            ConvertDep.SuccessLog.Add( $"Odyssey {ConvertDep.CurrentFileName} Downloaded Successfully" );
                        }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Odyssey Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }
            }
        }
    }
