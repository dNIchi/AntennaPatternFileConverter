using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class Wizard
        {

        public void ConvertToWizardBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -1;
                var cont3 = -91;
                var cont2 = -1;
                var cont4 = -91;
                var cont5 = -1;

                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var saveState5 = "";
                if (totalWords <= 0) ConvertDep.ErrorLog.Add( $"Wizard {totalWords} Empty\nCheck file source is correct" );

                var start = "A|TECC|ANTESD|01.00|930501|APF|01|Amphenol_Amphenol| \r\n" +
                            "|MFR|" + ConvertDep.Words[3] + "|" + "\r\n" +
                            "|MODEL|" + ConvertDep.Words[1] + "|\r\n" +
                            "|FILE|" + ConvertDep.Words[1] + ".apf|\r\n" +
                            "|DESC|" + ConvertDep.Description + "|\r\n" +
                            "|FCC ID|" + ConvertDep.FccId + "|\r\n" +
                            "|LENGTH|" + ConvertDep.Length + "|\r\n" +
                            "|DATE|" + ConvertDep.Date + "|\r\n" +
                            "|MFR ID|" + ConvertDep.MfrId + "|\r\n" +
                            "|FREQ|" + ConvertDep.Words[5] + " MHz|\r\n" +
                            "|POLARIZATION|" + ConvertDep.Polarization + "|\r\n" +
                            "|Hbeam|" + ConvertDep.Words[7] + "|\r\n" +
                            "|Vbeam|" + ConvertDep.Words[9] + "|\r\n" +
                            "|MaxGain|" + ConvertDep.MaxGain + " |\r\n" +
                            "|MinGain|" + ConvertDep.MinGain + "|\r\n" +
                            "|HORIZ|0|360|";
                //Horizontal
                for (var i = 21; i <= 739; i += 2)
                    {
                    cont1++;
                    var newvalue = ConvertDep.Words[i];
                    var dblVal = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue );
                    saveState += "\t" + cont1 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";
                    }

                //Verticals 1
                for (var i = 1283; i <= totalWords; i += 2)
                    {
                    cont3++;
                    var newvalue = ConvertDep.Words[i];
                    var dblVal = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue );
                    saveState3 += "\t" + cont3 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                for (var i = 743; i <= 923; i += 2)
                    {
                    cont2++;
                    var newvalue = ConvertDep.Words[i];
                    var dblVal = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue );
                    saveState2 += "\t" + cont2 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                //Verticals 2
                Stack<string> strStack = new Stack<string>( );
                for (var i = 1105; i <= 1283; i += 2)
                    {
                    var newvalue = ConvertDep.Words[i];
                    var dblVal = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue );
                    strStack.Push( dblVal.ToString( "0.000" ) );

                    }
                foreach (var strValue in strStack)
                    {
                    cont4++;
                    saveState4 += "\t" + cont4 + "\t" + strValue + "\t" + "\r\n";
                    }
                Stack<string> strStack2 = new Stack<string>( );
                for (var i = 923; i < 1105; i += 2)
                    {
                    var newvalue = ConvertDep.Words[i];
                    var dblVal = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue ));
                    strStack2.Push( dblVal.ToString( "0.000" ) );

                    }
                foreach (var strValue in strStack2)
                    {
                    cont5++;
                    saveState5 += "\t" + cont5 + "\t" + strValue + "\t" + "\r\n";
                    }

                ConvertDep.ConversionResults = start + "\r\n" + saveState + "|VERT|0|181| " + "\r\n" + saveState3 + saveState2 + "|VERT|180|181|\r\n" + saveState4 + saveState5;
                ConvertDep.ConversionResults.Replace( "\t", "|" );
                ConvertDep.SuccessLog.Add( $"Wizard {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Wizard Conversion Format Exception\n" +
                            $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                            $"Please check values in 'Textboxes' are correct\n"
                            + db.Message );
                }
            }
        public void DownLoadWizardBatchTxt( )
            {

            var fileName = ConvertDep.CurrentFileName;
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var wizardDir = ConvertDep.TargetDirectory + $"Wizard";

                if (!Directory.Exists( wizardDir )) Directory.CreateDirectory( wizardDir );

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( wizardDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{wizardDir}\\{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"Wizard {ConvertDep.CurrentFileName} .txt Downloaded Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Wizard Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }

            }
        public void DownloadWizardBatchApf( )
            {

            var fileName = ConvertDep.CurrentFileName;
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var wizardDir = ConvertDep.TargetDirectory + $"Wizard";

                if (!Directory.Exists( wizardDir )) Directory.CreateDirectory( wizardDir );


                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".apf";
                if (!File.Exists( wizardDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{wizardDir}\\{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"Wizard {ConvertDep.CurrentFileName} .apf Downloaded Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Wizard Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }

            }
        }
    }
