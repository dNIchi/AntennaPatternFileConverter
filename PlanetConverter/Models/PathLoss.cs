using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class PathLoss
        {
        public string PathLossReturnPattern( int cont, string[] words, int start, int fin )
            {

            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newValue = ConvertDep.Words[i];
                    var value = (Convert.ToDouble( newValue )) * -1;
                    saveState += cont.ToString( "0.0" ) + "," + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"PathLoss Return Pattern Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }

            return saveState;
            }
        public void ConvertToPathLossBatch( )
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
                var cont4 = -1;
                var saveState1 = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var dateMeasured = Convert.ToDateTime( ConvertDep.Date );

                if (totalWords <= 0) ConvertDep.ErrorLog.Add( $"PathLoss {totalWords} Empty\nCheck file source is correct" );
                var start = "REVNUM:,NSMA WG16.99.050\r\n" +
                                "REVDAT:,19980706\r\n" +
                                "COMNT1:,\r\n" +
                                "COMNT2:,\r\n" +
                                "ANTMAN:,Amphenol\r\n" +
                                "MODNUM:," + ConvertDep.Words[1] + "\r\n" +
                                "DESCR1:," + ConvertDep.Description + "\r\n" +
                                "DTDATA:," + dateMeasured.ToString( "MM/dd/yyyy" ) + "\r\n" +
                                "LOWFRQ:," + ConvertDep.LowFrequency + "\r\n" +
                                "HGHFRQ:," + ConvertDep.HighFrequency + "\r\n" +
                                "GUNITS:,DBI/DBR\r\n" +
                                "LWGAIN:," + ConvertDep.MaxGain + "\r\n" +
                                "MDGAIN:," + ConvertDep.MaxGain + "\r\n" +
                                "HGGAIN:," + ConvertDep.MaxGain + "\r\n" +
                                "AZWIDT:," + ConvertDep.Words[7] + "\r\n" +
                                "ELWIDT:," + ConvertDep.Words[9] + "\r\n" +
                                "CONTYP:,EDIN\r\n" +
                                "ATVSWR:" + ConvertDep.Atvswr + "\r\n" +
                                "FRTOBA:,32.0\r\n" +
                                "ELTILT:,0\r\n" +
                                "RADCTR:,\r\n" +
                                "POTOPO:,\r\n" +
                                "MAXPOW:," + ConvertDep.MaxPower + "\r\n" +
                                "ANTLEN:," + ConvertDep.Length + "\r\n" +
                                "ANTWID:," + ConvertDep.AntWidth + "\r\n" +
                                "ANTDEP:," + ConvertDep.Depth + "\r\n" +
                                "ANTWGT:," + ConvertDep.Width + "\r\n" +
                                "FIELD1:,\r\n" +
                                "FIELD2:,\r\n" +
                                "FIELD3:,\r\n" +
                                "FIELD4:,\r\n" +
                                "FIELD5:,\r\n" +
                                "PATTYP:,Typical\r\n" +
                                "NOFREQ:,1\r\n" +
                                "PATFRE:," + ConvertDep.Words[4] + "\r\n" +
                                "NUMCUT:,2\r\n" +
                                "PATCUT:,AZ\r\n" +
                                "POLARI:,SLR-SLL\r\n" +
                                "NUPOIN:,361\r\n" +
                                "FSTLST:,-180,180\r\n";
                var centerPoint = "PATCUT:,EL\r\n" +
                                  "POLARI:,SLR-SLL\r\n" +
                                  "NUPOIN:,361\r\n" +
                                  "FSTLST:,-180,180\r\n";
                saveState1 = PathLossReturnPattern( cont1, ConvertDep.Words, 381, 739 );
                saveState2 = PathLossReturnPattern( cont2, ConvertDep.Words, 21, 381 );
                saveState3 = PathLossReturnPattern( cont3, ConvertDep.Words, 1103, totalWords );   //743-1105
                saveState4 = PathLossReturnPattern( cont4, ConvertDep.Words, 743, 1103 );    //1105-ta
                ConvertDep.ConversionResults = start + saveState1 + saveState2 + centerPoint + saveState3 + saveState4 + "ENDFIL,EOF";

                ConvertDep.SuccessLog.Add( $"PathLoss {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"PathLoss Conversion Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }

            }
        public void DownloadPathLossBatchTxt( )
            {

            var fileName = ConvertDep.CurrentFileName;

            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var pathLossDir = ConvertDep.TargetDirectory + $"PathLoss";

                if (!Directory.Exists( pathLossDir )) Directory.CreateDirectory( pathLossDir );

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( pathLossDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{pathLossDir}\\{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"PathLoss {ConvertDep.CurrentFileName} .txt Converted Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"PathLoss Download Exception\n" +
                              $"Please Check Directory is valid\n" +
                              $"Please Ensure You have Write Access"
                              + db.Message );
                }
            }
        public void DownloadPathLossBatchAdf( )
            {

            var fileName = ConvertDep.CurrentFileName;

            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var pathLossDir = ConvertDep.TargetDirectory + $"PathLoss";

                if (!Directory.Exists( pathLossDir )) Directory.CreateDirectory( pathLossDir );

                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( pathLossDir + fileName ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{pathLossDir}\\{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"PathLoss {ConvertDep.CurrentFileName} .txt Converted Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"PathLoss Download Exception\n" +
                                         $"Please Check Directory is valid\n" +
                                         $"Please Ensure You have Write Access"
                                         + db.Message );
                }
            }
        }
    }
