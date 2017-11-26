using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.ExtendedProperties;
//using static PlanetConverter.MainWindow;

namespace PlanetConverter.Models
    {
    public class AirCom 
        {
        public void ConvertToAircomBatch()
            {
             try
                    {
                    string[] seperators = { "\r\n" };
                     var valOne = ConvertDep.CurrentIngestedtFile;
                     var commaValue = valOne.Replace( "\t", "\r\n" );
                      ConvertDep.Words = commaValue.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                    var totalWords = ConvertDep.Words.Length;
                    var countOne = -1;
                    var countTwo = -1;

                    var saveStateOne = string.Empty;
                    var saveStateTwo = string.Empty;

                    if (totalWords <= 0)
                        {
                        ConvertDep.ErrorLog.Add( $"Aircom {totalWords} Empty\nCheck file source is correct"); 
                        }
                    var start = $"NAME\t" + ConvertDep.Words[1] + "\r\n" +
                                "MAKE\t" + ConvertDep.Words[1] + "\t" + ConvertDep.Words[3] +"\r\n" +
                                "FREQUENCY\t" + ConvertDep.Words[5] + "\r\n" +
                                "H_WIDTH " + ConvertDep.Words[7] + "\r\n" +
                                "H_WIDTH " + ConvertDep.Words[9] + "\r\n" +
                                "FRONT_TO_BACK " + ConvertDep.Words[11] + "\r\n" +
                                "POLARIZATION\t" + ConvertDep.Polarization + "\r\n" +
                                "GAIN\t" + ConvertDep.MaxGain + " dBi\r\n" +
                                "TILT\t" +ConvertDep.TiltValue + "\tELECTRICAL\r\n" +
                                "COMMENTS\t" + ConvertDep.Comments + "\r\n" +
                                "HORIZONTAL\t360";

                    for (int i = 21; i <= 740; i += 2)
                        {
                        countOne++;
                        var valTwo = ConvertDep.Words[i];
                        var dblVal1 = Convert.ToDouble( valTwo );
                        saveStateOne += countOne.ToString( ) + "\t" + dblVal1.ToString( "0.0" ) + "\r\n";
                        }
                    for (var j = 743; j < totalWords; j += 2)
                        {
                        countTwo++;
                        var valThree = ConvertDep.Words[j];
                        var dblVal2 = Convert.ToDouble( valThree );
                        saveStateTwo += countTwo.ToString( ) + "\t" + dblVal2.ToString( "0.0" ) + "\r\n";
                        }

                    ConvertDep.ConversionResults = start + "\r\n" + saveStateOne + "VERTICAL\t360\r\n" + saveStateTwo;
                    }
                catch (FormatException db)
                    {
                    ConvertDep.ErrorLog.Add( $"Aircom Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message);
                    }
                ConvertDep.SuccessLog.Add($"{ConvertDep.Words[1]} Converted Successfully");
            }
        public void DownloadAircom( )
            {
            try
                {
                string[] seperators = { $"\r\n" };
                    var value = ConvertDep.ConversionResults;
                    var commaValue = value.Replace( "\t", "\r\n" );
                    ConvertDep.Words = commaValue.Split( seperators, StringSplitOptions.RemoveEmptyEntries );

                    var fileName = string.Empty;

                    fileName = ConvertDep.CurrentFileName;

                    fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                    var aircomDir = ConvertDep.TargetDirectory + "Aircom";
                    if (!Directory.Exists( aircomDir ))

                        Directory.CreateDirectory( aircomDir );

                    if (!File.Exists( aircomDir + "\\" + fileName ))
                        {
                        using (var tempVar = File.Create(ConvertDep.Words[1], 1024 ))
                            {
                            File.WriteAllText( $"{aircomDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
            catch (Exception db)
                {
               ConvertDep.ErrorLog.Add($"Aircom Download Encountered an error\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message);
                }
            }
        }
    }