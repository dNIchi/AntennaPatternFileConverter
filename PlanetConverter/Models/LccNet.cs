using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class LccNet
        {
        public string LccHreturnPattern( int cont, string[] words, int start, int fin )
            {
            var saveState = "";
            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newValue = ConvertDep.Words[i];
                    var value = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue );
                    saveState += "AE\tH\t" + cont.ToString( "0.0" ) + "\t" + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"LCC Horizontal Return Pattern Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            return saveState;
            }
        public string LccVreturnPattern( int cont, string[] words, int start, int fin )
            {

            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont--;
                    var newValue = ConvertDep.Words[i];
                    var value = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue );
                    saveState += "AE\tV\t" + cont.ToString( "0.0" ) + "\t" + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"LCC Vertical Return Pattern Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            return saveState;
            }
        public void ConvertToLccBatchTest( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -1;
                // var cont2 = -1;
                var cont3 = 181;
                var cont4 = 1;
                var saveState1 = "";
                var saveState3 = "";
                var saveState4 = "";
                if (totalWords <= 0) ConvertDep.ErrorLog.Add( $"LCCNet {totalWords} Empty\nCheck file source is correct" );
                    

                var start = "AA  " + ConvertDep.Words[1] + "\r\n" +
                            "AB\r\n" +
                            "AC  S  " + ConvertDep.Words[7] + " " + ConvertDep.MaxGain + " 0\r\n" +
                            "AD  " + ConvertDep.Length + " 0 Amphenol " + ConvertDep.Words[1] + "\r\n";
                var fin = "AF  " + ConvertDep.MinFrequency + "-" + ConvertDep.MinFrequency + " MHz\r\n" +
                          "AG  50 Ohms\r\n" +
                          "AH  <=1.5:1\r\n" +
                          "AI  0\r\n" +
                          "AJ  19\r\n" +
                          "AK  500 W\r\n" +
                          "AL  NE or EDIN\r\n" +
                          "AM  " + ConvertDep.Words[11] + "\r\n" +
                          "AN  29.1 lbs\r\n" +
                          "AO\r\n" +
                          "AP\r\n" +
                          "AQ\r\n" +
                          "AR";
                saveState1 = LccHreturnPattern( cont1, ConvertDep.Words, 21, 739 );
                saveState3 = LccVreturnPattern( cont3, ConvertDep.Words, 1103, totalWords - 1 );
                saveState4 = LccVreturnPattern( cont4, ConvertDep.Words, 743, 1101 );
                ConvertDep.ConversionResults = start + saveState1 + saveState3 + saveState4 + fin;
                ConvertDep.SuccessLog.Add( $"LCC {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"LCC Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadLccBatchAnt( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var lccNetDir = ConvertDep.TargetDirectory + $"Lcc_Net";

                if (!Directory.Exists( lccNetDir )) Directory.CreateDirectory( lccNetDir );

                //.ant

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".ant";

                if (!File.Exists( lccNetDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{lccNetDir}\\{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"LCC {ConvertDep.CurrentFileName} .adf Converted Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"LCC Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }

            }
        public void DownloadLccBatchTxt( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var lccNetDir = ConvertDep.TargetDirectory + $"Lcc_Net";

                if (!Directory.Exists( lccNetDir )) Directory.CreateDirectory( lccNetDir );

               //.txt

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( lccNetDir ))
                    {
                    if (!File.Exists( lccNetDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{lccNetDir}\\{fileName}", value );
                            tempVar.Close( );
                            ConvertDep.SuccessLog.Add( $"LCC {ConvertDep.CurrentFileName} .txt Downloaded Successfully" );
                            }
                        }
                    }

                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"LCC Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }

            }

        }
    }
