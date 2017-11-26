using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class GeoPlan
        {
        public void ConvertToGeoPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -1;
                var cont2 = 0;
                var cont3 = -180;

                var saveState = "";
                var saveState2 = "";
                var dateMeasured = Convert.ToDateTime( ConvertDep.Date );
                    if (totalWords <= 0)
                    {
                        ConvertDep.ErrorLog.Add( $"GeoPlan {totalWords} Empty\nCheck file source is correct" );
                    }

                var start = "VERIZON WIRELESS RFTOOLS ANTENNA" + "\r\n" +
                            "V5 \r\n" +
                            "model_name:" + "\t" + ConvertDep.Words[1] + "\r\n" +
                            "manufacturer:" + "\t" + ConvertDep.Words[3] + "\r\n" +
                            "description:" + "\t" + ConvertDep.Description + "\r\n" +
                            "antenna_type:" + "\t" + ConvertDep.Family + "\r\n" +
                            "polarization:" + "\t" + ConvertDep.Polarization + "\r\n" +
                            "azimuth_display_offset_deg:" + "\t" + ConvertDep.AzimuthDisplay + "\r\n" +
                            "date_measured:" + "\t" + dateMeasured.ToString( "dd-MMM-yy" ) + "\r\n" +
                            "freq_measured_mhz:" + "\t" + ConvertDep.Words[5] + "\r\n" +
                            "lower_freq_mhz:" + "\t" + ConvertDep.LowerFrequency + "\r\n" +
                            "upper_freq_mhz:" + "\t" + ConvertDep.UpperFrequency + "\r\n" +
                            "electrical_tilt:" + "\t" + ConvertDep.TiltValue + "\r\n" +
                            "height_m:" + "\t" + ConvertDep.AntHeight + "\r\n" +
                            "width_m:" + "\t" + ConvertDep.AntWidth + "\r\n" +
                            "depth_m:" + "\t" + ConvertDep.Depth + "\r\n" +
                            "weight_kg:" + "\t" + ConvertDep.Weight + "\r\n" +
                            "HORIZONTAL_GAINS";

                for (var i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    var strValue = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue );
                    saveState += cont1.ToString( "0.0" ) + "\t" + strValue.ToString( "0.0" ) + "\r\n";
                    }
                Stack<string> strStack = new Stack<string>( );
                for (var i = 743; i < 1103; i += 2)
                    {
                    var newValue = ConvertDep.Words[i];
                    var strValue = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newValue );
                    strStack.Push( strValue.ToString( "0.0" ) );

                    }
                var saveState3 = "";
                foreach (string strValue in strStack)
                    {
                    cont3++;
                    saveState2 += cont3.ToString( "0.0" ) + "\t" + strValue + "\r\n";
                    }

                Stack<string> strStack2 = new Stack<string>( );
                for (var i = 1103; i <= totalWords - 1; i += 2)
                    {
                    string newvalue = ConvertDep.Words[i];
                    double valor = Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue );
                    strStack2.Push( valor.ToString( "0.0" ) );

                    }
                foreach (string valor in strStack2)
                    {
                    cont2++;
                    saveState3 += cont2.ToString( "0.0" ) + "\t" + valor + "\r\n";
                    }
                ConvertDep.ConversionResults = start + "\r\n" + saveState + "END" + "\r\n" + "VERTICAL_GAINS" +
                                        "\r\n" + saveState2 + saveState3 + "END";
                    ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"GeoPlan Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadGeoPlanVwa( )
            {
            try
                {
                 var fileName = ConvertDep.CurrentFileName;
                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var geoPlanDir = ConvertDep.TargetDirectory + $"GeoPlan";

                //.VWA
                fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".vwa";

                    if (!Directory.Exists( geoPlanDir )) Directory.CreateDirectory( geoPlanDir );

                    if (!File.Exists( geoPlanDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{geoPlanDir}\\{fileName}", value );
                            tempVar.Close( );
                            }

                        }
                    }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"GepPlan Download Exception\n" +
                          $"Please Check Directory is valid\n" +
                          $"Please Ensure You have Write Access"
                          + db.Message );
                }
            }
        public void DownloadGeoPlanTxt( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );


                var geoPlanDir = ConvertDep.TargetDirectory + $"GeoPlan";

                //.txt
                fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                if (!Directory.Exists( geoPlanDir )) Directory.CreateDirectory( geoPlanDir );

                if (!File.Exists( geoPlanDir ))
                    {
                    if (!File.Exists( geoPlanDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{geoPlanDir}\\{fileName}", value );
                            tempVar.Close( );
                            }

                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"GepPlan Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }
            }
        }
    }
