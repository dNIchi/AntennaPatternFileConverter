using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class Granet
        {
        public string GranetHreturnPattern( int cont, string[] words, int startIndx, int fin )
            {
            var saveState = "";
            try
                {
                for (var i = startIndx; i <= fin; i += 2)
                    {
                    cont++;
                    var newvalue = words[i];
                    var value = (Convert.ToDouble( newvalue ) * -1);
                    saveState += cont.ToString( ) + "\t" + value.ToString( "0.000" ) + "\r\n";
                    }
                //return saveState;
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Gran Horizontal Return Pattern Format Exception\n" +
                                        $"Please check .pln is valid\n" +
                                        $"Please check values in 'Textboxes' are correct\n"
                                        + db.Message );
                }
            return saveState;
            }
        public string GranetVreturnPattern( int cont, string[] words, int startIndx, int fin )
            {

            var saveState = "";
            try
                {
                for (var i = startIndx; i <= fin; i += 2)
                    {
                    cont--;
                    var newValue = words[i];
                    var value = (Convert.ToDouble( newValue )) * -1;
                    saveState += cont.ToString( ) + "\t" + value.ToString( "0.000" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Gran Vertical Return Pattern Format Exception\n" +
                                        $"Please check .pln is valid\n" +
                                        $"Please check values in 'Textboxes' are correct\n"
                                        + db.Message );
                }
            return saveState;
            }
        public void ConvertToGranetBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -181;
                var cont2 = -1;
                var cont3 = 181;
                var cont4 = 1;
                var saveState1 = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var dateMeasured = Convert.ToDateTime( ConvertDep.Date );
                if (totalWords <= 0)
                    {
                    ConvertDep.ErrorLog.Add( $"Granet {totalWords} Empty\nCheck file source is correct" );
                    }

                var start = "model_number\t" + ConvertDep.Words[1] + "\r\n" +
                            "manufacturer\tConvertDep.Words[3]\r\n" +
                            "gain\t" + ConvertDep.MaxGain + " dBd\r\n" +
                            "h_beamwidth\t" + ConvertDep.Words[7] + " degrees\r\n" +
                            "v_beamwidth\t" + ConvertDep.Words[9] + " degrees\r\n" +
                            "front_to_back\t" + ConvertDep.Words[11] + " dB\r\n" +
                            "length\t" + ConvertDep.Length + " meters\r\n" +
                            "lobe_tilt\t" + ConvertDep.LobeTilt + " degrees\r\n" +
                            "wind_area\t" + ConvertDep.WindArea + " square meters\r\n" +
                            "source\t0\r\n" +
                            "date\t" + dateMeasured.ToShortDateString( ) + "\r\n" +
                            "meas-freq\t" + ConvertDep.MeasFrequency + " MHz\r\n" +
                            "description\t" + ConvertDep.Description + "\r\n" +
                            "polarization\t" + ConvertDep.Polarization + "\r\n" +
                            "Sectored\r\n\r\n" +

                            "horizontal\r\n" +
                            "unequal unsymmetrical\r\n";
                saveState1 = GranetHreturnPattern( cont1, ConvertDep.Words, 381, 739 );
                saveState2 = GranetHreturnPattern( cont2, ConvertDep.Words, 21, 379 );
                saveState3 = GranetVreturnPattern( cont3, ConvertDep.Words, 1103, totalWords - 1 );
                saveState4 = GranetVreturnPattern( cont4, ConvertDep.Words, 743, 1101 );
                ConvertDep.ConversionResults = start + saveState1 + saveState2 +
                                       "\r\nvertical\r\nunequal unsymmetrical\r\n" + saveState3 + saveState4;
                ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Granet Conversion Format Exception\n" +
                                     $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                                     $"Please check values in 'Textboxes' are correct\n"
                                     + db.Message );
                }
            }
        public void DownloadGranetPat( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var granetDir = ConvertDep.TargetDirectory + $"Granet";

                //.pat

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".pat";

                if (!Directory.Exists( granetDir )) Directory.CreateDirectory( granetDir );

                if (!File.Exists( granetDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{granetDir}\\{fileName}", value );
                        tempVar.Close( );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Granet Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }
            }
        public void DownloadGranetTxt( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var granetDir = ConvertDep.TargetDirectory + $"Granet";

                //.txt
                fileName =
                      fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                      + ".txt";

                if (!Directory.Exists( granetDir )) Directory.CreateDirectory( granetDir );

                if (!File.Exists( granetDir ))
                    {
                    if (!File.Exists( granetDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{granetDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Granet Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }
            }
        }
    }
    
