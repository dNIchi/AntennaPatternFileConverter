using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;


namespace PlanetConverter.Models
    {
    public class Ce4
        {
        public void ConvertToCe4Batch( )
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

                if (totalWords <= 0)
                    {
                    ConvertDep.ErrorLog.Add( $"Ce4 {totalWords} Empty\nCheck file source is correct" );
                    }

                var dateMeasured = Convert.ToDateTime( ConvertDep.DateMeasured );
                var start = "|MANUF|Amphenol|\r\n" +
                            "|MODEL|" + ConvertDep.Words[1] + "|\r\n" +
                            "|FILE|" + ConvertDep.Words[1] + "|\r\n" +
                            "|DESCR|" + ConvertDep.Description + "|\r\n" +
                            "|FCC ID|\r\n" +
                            "|REVERSE ID|\r\n" +
                            "|DATE|" + dateMeasured.ToShortDateString( ) + "|\r\n" +
                            "|MANUF ID|Amphenol|\r\n" +
                            "|FREQ|" + ConvertDep.MinFrequency + "-" + ConvertDep.MaxFrequency +
                            " MHz|\r\n" +
                            "|DBD/DBI Flag|dBd|\r\n" +
                            "|POLARIZATION|" + ConvertDep.Polarization + "|\r\n" +
                            "|HORIZ BEAM WIDTH|" + ConvertDep.Words[7] + "|\r\n" +
                            "|VERT BEAM WIDTH|" + ConvertDep.Words[9] + "|\r\n" +
                            "|HORIZ OFFSET|0|\r\n" +
                            "|HORIZ|0|360|";
                //Horizontal
                for (var i = 21; i <= 739; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    var dblVal = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue );
                    saveState += "\t" + cont1 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";
                    }

                //Verticals 1
                for (var i = 1283; i <= totalWords; i += 2)
                    {
                    cont3++;
                    var newValue = ConvertDep.Words[i];
                    var dblVal = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    saveState3 += "\t" + cont3 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                for (var i = 743; i <= 923; i += 2)
                    {
                    cont2++;
                    var newValue = ConvertDep.Words[i];
                    var dblVal = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    saveState2 += "\t" + cont2 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                //Verticals 2
                Stack<string> stringQue = new Stack<string>( );
                for (var i = 1105; i <= 1283; i += 2)
                    {
                    var newValue = ConvertDep.Words[i];
                    var dblVal = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    stringQue.Push( dblVal.ToString( "0.000" ) );

                    }
                foreach (var strVal in stringQue)
                    {
                    cont4++;
                    saveState4 += "\t" + cont4 + "\t" + strVal + "\t" + "\r\n";
                    }
                Stack<string> stringQue2 = new Stack<string>( );
                for (var i = 923; i < 1105; i += 2)
                    {
                    var newValue = ConvertDep.Words[i];
                    var valor = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    stringQue2.Push( valor.ToString( "0.000" ) );

                    }
                foreach (var strVal in stringQue2)
                    {
                    cont5++;
                    saveState5 += "\t" + cont5 + "\t" + strVal + "\t" + "\r\n";
                    }

                ConvertDep.ConversionResults =
                    start + "\r\n" + saveState + "|VERT|0|181| " + "\r\n" + saveState3 + saveState2 +
                    "|VERT|180|181|\r\n" + saveState4 + saveState5;
                ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Ce4 Conversion Format Exception\n" +
                                        $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                                        $"Please check values in 'Textboxes' are correct\n"
                                        + db.Message );
                }
            }

        public void DownloadCe4Txt( )
            {
            try
                {
                var fileName = string.Empty;
                fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                string commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var ce4Dir = ConvertDep.TargetDirectory + $"Ce4";

                //.txt
                fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                if (!Directory.Exists( ce4Dir )) Directory.CreateDirectory( ce4Dir );

                if (!File.Exists( ce4Dir ))
                    {
                    if (!File.Exists( ce4Dir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{ce4Dir}\\{fileName}", value );
                            tempVar.Close( );
                            }

                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Ce4 .vwa Download Exception\n" +
                                        $"Please Check Directory is valid\n" +
                                        $"Please Ensure You have Write Access"
                                        + db.Message );
                }
            }

        public void DownloadCe4Vwa( )
            {
            try
                {
                var fileName = string.Empty;
                fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                string commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var ce4Dir = ConvertDep.TargetDirectory + $"Ce4";

                //.vwa
                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".vwa";

                if (!Directory.Exists( ce4Dir )) Directory.CreateDirectory( ce4Dir );

                if (!File.Exists( ce4Dir ))
                    {
                    if (!File.Exists( ce4Dir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{ce4Dir}\\{fileName}", value );
                            tempVar.Close( );
                            }

                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Ce4 .txt Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }

            }
        }
    }


