using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace PlanetConverter.Models
    {
    public class CelPlan
        {
        public void ConvertToCelPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                    ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -1;
                var cont2 = -1;
                var saveState = "";
                var saveState2 = "";
                    if (totalWords <= 0)
                    {
                        ConvertDep.ErrorLog.Add( $"CelPlan {totalWords} Empty\nCheck file source is correct" );
                    }

                var start = "[CelPlan DT ANT]\r\n" +
                            "Mod:\t" + ConvertDep.Words[1] + "\r\n" +
                            "Man:\tAmphenol\r\n" +
                            "Dig:\t" + " " + "\r\n" +
                            "Dsc:\t" + ConvertDep.Description + "\r\n" +
                            "Ngn:\t" + ConvertDep.MaxGain + " dBd\r\n" +
                            "Hbw:\t" + ConvertDep.Words[7] + "°\r\n" +
                            "Vbw:\t" + ConvertDep.Words[9] + "°\r\n" +
                            "Mnf:\t" + ConvertDep.MinFrequency + " MHz\r\n" +
                            "Mxf:\t" + ConvertDep.MaxFrequency + " MHz\r\n" +
                            "Sze:\t" + ConvertDep.Size + " m\r\n" +
                            "Inc:\t1°\r\n" +
                            "Han:\tHgn";

                for (var i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    var valor = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    saveState += cont1 + "\t" + valor.ToString( "0.0" ) + "\r\n";
                    }
                for (var i = 743; i < totalWords; i += 2)
                    {
                    cont2++;
                    var newValue = ConvertDep.Words[i];
                    var dblVal = (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue ));
                    saveState2 += cont2.ToString( ) + "\t" + dblVal.ToString( "0.0" ) + "\r\n";
                    }
                    ConvertDep.ConversionResults = start + "\r\n" + saveState + "Van\tVgn\r\n" + saveState2;
                    ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );
                }
            catch (FormatException db)
                {
                    ConvertDep.ErrorLog.Add( $"CelPlan Conversion Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }
            }
        public void DownloadCelPlanBatch( )
            {
            try
                {
                
                    string[] separators = { "\r\n" };
                    string value = ConvertDep.ConversionResults;
                    string commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var fileName = string.Empty;

                    fileName = ConvertDep.CurrentFileName;

                    fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                    var celPlanDir = ConvertDep.TargetDirectory + "CelPlan";
                    if (!Directory.Exists( celPlanDir ))

                        Directory.CreateDirectory( celPlanDir );

                    if (!File.Exists( celPlanDir + "\\" + fileName ))
                        {
                        using (var tempFile = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{celPlanDir}\\{fileName}", value );
                            tempFile.Close( );
                            }
                        }
                    
                }
            catch (Exception db)
                {
                    ConvertDep.ErrorLog.Add( $"CelPlan Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }
            }
        }
    }
