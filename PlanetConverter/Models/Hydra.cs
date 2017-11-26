using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class Hydra
        {
        public string HydraHreturnPattern( int cont, string[] words, int start, int fin )
            {
            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newvalue = ConvertDep.Words[i];
                    var value = (Convert.ToDouble( newvalue )) * -1;
                    saveState += cont + "," + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Hydra Horizontal Return Pattern Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }
            return saveState;
            }
        public string HydraVreturnPattern( int cont, string[] words, int start, int fin )
            {
            var saveState = "";
            var strStack = new Stack<string>( );

            try
                {
                for (var i = start; i < fin; i += 2)
                    {
                    var newValue = ConvertDep.Words[i];
                    var value = (Convert.ToDouble( newValue )) * -1;
                    strStack.Push( value.ToString( "0.0" ) );

                    }
                foreach (var value in strStack)
                    {
                    cont++;
                    saveState += cont + "," + value + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Hydra Vertical Return Pattern Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }

            return saveState;
            }
        public void ConvertToHydraBatch( )
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
                var cont3 = -181;
                var cont4 = 0;
                var saveState1 = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var dateMeasured = Convert.ToDateTime( ConvertDep.Date );

                if (totalWords <= 0)
                    {
                    ConvertDep.ErrorLog.Add( $"Hydra {totalWords} Empty\nCheck file source is correct" );
                    }
                var start = "REVNUM:,TIA/EIA IS-804-0\r\n" +
                            "REVDAT:,20010109\r\n" +
                            "ANTMAN:,Amphenol\r\n" +
                            "MODNUM:," + ConvertDep.Words[1] + "\r\n" +
                            "FILNUM:," + dateMeasured.ToShortDateString( ) + "\r\n" +
                            "DESCR1:," + ConvertDep.Description + "\r\n" +
                            "DTDATA:," + "20030821" + "\r\n" +
                            "LOWFRQ:," + ConvertDep.LowerFrequency + "\r\n" +
                            "HGHFRQ:," + ConvertDep.HighFrequency + "\r\n" +
                            "GUNITS:,DBD/DBR\r\n" +
                            "LWGAIN:," + ConvertDep.MaxGain + "\r\n" +
                            "MDGAIN:," + ConvertDep.MaxGain + "\r\n" +
                            "HGGAIN:," + ConvertDep.MaxGain + "\r\n" +
                            "AZWIDT:," + ConvertDep.Words[7] + "\r\n" +
                            "ELWIDT:," + ConvertDep.Words[9] + "\r\n" +
                            "CONTYP:," + "EDIN" + "\r\n" +
                            "ATVSWR:," + "1.5" + "\r\n" +
                            "ELTILT:,0\r\n" +
                            "MAXPOW:," + ConvertDep.MaxPower + "\r\n" +
                            "ANTLEN:," + ConvertDep.Length + "\r\n" +
                            "ANTWID:," + ConvertDep.AntWidth + "\r\n" +
                            "ANTDEP:," + ConvertDep.Depth + "\r\n" +
                            "FIELD3:,\r\n" +
                            "PATTYP:," + "Typical" + "\r\n" +
                            "NOFREQ:,1\r\n" +
                            "PATFRE:," + ConvertDep.Words[4] + "\r\n" +
                            "NUMCUT:," + "2" + "\r\n" +
                            "PATCUT:,H\r\n" +
                            "POLARI:," + ConvertDep.Polarization + "\r\n" +
                            "NUPOIN:,360\r\n" +
                            "FSTLST:,-180,179\r\n";

                var centerPoint = "PATCUT:,V\r\n" +
                                  "POLARI:," + ConvertDep.Polarization + "\r\n" +
                                  "NUPOIN:,360\r\n" +
                                  "FSTLST:,-180,179\r\n";
                saveState1 = HydraHreturnPattern( cont1, ConvertDep.Words, 381, 739 );
                saveState2 = HydraHreturnPattern( cont2, ConvertDep.Words, 21, 379 );
                saveState3 = HydraVreturnPattern( cont3, ConvertDep.Words, 743, 1105 );
                saveState4 = HydraVreturnPattern( cont4, ConvertDep.Words, 1105, totalWords );
                ConvertDep.ConversionResults = start + saveState1 + saveState2 + centerPoint + saveState3 + saveState4 +
                                          "ENDFIL,EOF";
                ConvertDep.SuccessLog.Add( $"Hydra {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Hydra Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadHydraAdf( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var hydraDir = ConvertDep.TargetDirectory + $"Hydra";

                if (!Directory.Exists( hydraDir )) Directory.CreateDirectory( hydraDir );

                fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".adf";

                using (var tempVar = File.Create( fileName, 1024 ))
                    {
                    File.WriteAllText( $"{hydraDir}\\{fileName}", value );
                    tempVar.Close( );
                    ConvertDep.SuccessLog.Add( $"Hydra {ConvertDep.CurrentFileName} .adf Downloaded Successfully" );
                    }

                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Hydra Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }
            }
        public void DownloadHydraTxt( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;

                string[] separators = { "\r\n" };
                var value = ConvertDep.ConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var hydraDir = ConvertDep.TargetDirectory + $"Hydra";

                if (!Directory.Exists( hydraDir )) Directory.CreateDirectory( hydraDir );

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                using (var tempVar = File.Create( fileName, 1024 ))
                    {
                    File.WriteAllText( $"{hydraDir}\\{fileName}", value );
                    tempVar.Close( );
                    ConvertDep.SuccessLog.Add( $"Hydra {ConvertDep.CurrentFileName} .txt Downloaded Successfully" );
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Hydra Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }
            }
        }
    }
