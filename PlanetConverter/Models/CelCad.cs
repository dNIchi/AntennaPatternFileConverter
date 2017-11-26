using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace PlanetConverter.Models
    {
    public class CelCad
        {
        public void ConvertToCelCadBatchDownload( )
            {
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var cont1 = -1;
                var cont2 = 181;
                var cont3 = 1;

                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";
                    if (totalWords <= 0)
                    {
                        ConvertDep.ErrorLog.Add( $"CelCad {totalWords} Empty\nCheck file source is correct" );
                    }
                var start = "LCC\r\n" +
                            ConvertDep.Date + "\r\n" +
                            ConvertDep.Time + "\r\n" + 
                            ConvertDep.Words[3] +
                            " 815-399-0001\r\n" +
                            ConvertDep.Words[1] + "\r\n";

                saveState = CelCadReturnPattern( cont1, ConvertDep.Words, 21, 739, true, -1, -1 );
                saveState2 = CelCadReturnPattern( cont3, ConvertDep.Words, 743, 1101, false, 1, 1 );
                saveState3 = CelCadReturnPattern( cont2, ConvertDep.Words, 1103, totalWords, false, 1, 1 );

                ConvertDep.SuccessLog.Add( $"CelCad {ConvertDep.Words[1]} Converted Successfully" );
                ConvertDep.ConversionResults =
                start + ConvertDep.Words[7] + "\r\n" + "H\r\n0.00\r\n" + saveState + "*";

                ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );
                var celCadVertFileName = "";
                var celCadHorzFileName = celCadVertFileName = ConvertDep.Words[1];

                var celCadHorizontalDir = ConvertDep.TargetDirectory + "CelCad_Horizontal";

                if (!Directory.Exists( celCadHorizontalDir ))
                    Directory.CreateDirectory( celCadHorizontalDir );

                if (Directory.Exists( celCadHorizontalDir ))
                    {
                    if (!File.Exists( celCadHorizontalDir + "\\" + celCadHorzFileName ))
                        {
                        using (var tempCreate = File.Create( celCadHorzFileName, 1024 ))
                            {
                            File.WriteAllText( $"{celCadHorizontalDir}\\{celCadHorzFileName}",
                            ConvertDep.ConversionResults );
                            tempCreate.Close( );
                                ConvertDep.SuccessLog.Add( $"CelCad Horz {ConvertDep.Words[1]} Downloaded Successfully" );
                            }
                        }
                    }

                ConvertDep.ConversionResults = start + ConvertDep.Words[9] + "\r\n" + "V\r\n0.00\r\n" + saveState3 +  saveState2 + "*";
                ConvertDep.SuccessLog.Add( $"{ConvertDep.Words[1]} Converted Successfully" );

                var celCadVerticalDir = ConvertDep.TargetDirectory + "CelCad_Vertical";

                if (!Directory.Exists( celCadVerticalDir ))
                    Directory.CreateDirectory( celCadVerticalDir );

                if (Directory.Exists( celCadVerticalDir ))
                    {
                    if (!File.Exists( celCadVerticalDir + "\\" + celCadVertFileName ))
                        {
                        using (var tempCreate = File.Create( celCadVertFileName, 1024 ))
                            {
                            File.WriteAllText( $"{celCadVerticalDir}\\{celCadVertFileName}",
                                ConvertDep.ConversionResults );
                            tempCreate.Close( );
                                ConvertDep.SuccessLog.Add( $" CelCad Vertical {ConvertDep.Words[1]} Downloaded Successfully" );
                            }
                        }
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"CellCad Conversion Format || Download Exception\n" +
                         $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                         $"Please check values in 'Textboxes' are correct\n"
                         + db.Message );
                }
            }
        public string CelCadReturnPattern( int cont, string[] words, int start, int fin, bool increment, int negpos, int negpos2 )
            {
            var celCadReturnSave = string.Empty;
            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    if (increment)
                        {
                        cont++;
                        }
                    else
                        {
                        cont--;
                        }
                    var newvalue = words[i];
                    var maxGainParsed = (Convert.ToDouble( ConvertDep.MaxGain ) - (Convert.ToDouble( newvalue ))) * negpos;

                    celCadReturnSave += cont + "\t" +
                                                     (Convert.ToDouble( maxGainParsed ) * negpos2).ToString( "0.0" ) +
                                                     "\r\n";
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"CellCad Return Pattern Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }
            return celCadReturnSave;
            }
        }
    }
