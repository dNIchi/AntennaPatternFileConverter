using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
    {
    public class Hodiax
        {
        public void ConvertToHodiaxBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var cont1 = -1;
                var totalWords = ConvertDep.Words.Length;
                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                    if (totalWords <= 0)
                    {
                        ConvertDep.ErrorLog.Add( $"Hodiax {totalWords} Empty\nCheck file source is correct" );
                    }
                var start = "ANTENNA-FILE \r\n" +
                            "IA" + "\t" + "Hodiax v2.0 \r\n" +
                            "IB" + "\t" + "ANTENNA-FILE \r\n" +
                            "HA" + "\t" + ConvertDep.Words[1] + "\r\n" +
                            "HB \r\n" +
                            "HC \r\n" +
                            "HD \r\n" +
                            "HE \r\n" +
                            "HF \r\n" +
                            "HG \r\n" +
                            "HH \r\n" +
                            "HM \r\n" +
                            "HI  R         0 \r\n" +
                            "HJ     0       0 ";
                var final = "HL     0       0 \r\n" +
                            "ZZ";
                for (var i = 21; i <= 739; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    saveState += "HK" + "\t" + "H" + "\t" + cont1 + "\t" +
                                 (Convert.ToDouble( newValue ) * -1) + "\r\n";
                    }
                cont1 = -1;
                for (var i = 21; i <= 739; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    saveState2 += "HK\tV\t" + cont1 + "\t" +
                                  (Convert.ToDouble( newValue ) * -1) + "\r\n";
                    }
                cont1 = -1;
                for (var i = 743; i <= totalWords; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    saveState3 += "HK\t H\t" + cont1 + "\t" +
                                  (Convert.ToDouble( newValue ) * -1) + "\r\n";
                    }
                cont1 = -1;
                for (var i = 743; i <= totalWords; i += 2)
                    {
                    cont1++;
                    var newValue = ConvertDep.Words[i];
                    saveState4 += "HK\t V\t" + cont1 + "\t" +
                                  (Convert.ToDouble( newValue ) * -1) + "\r\n";
                    }
                ConvertDep.CurrentHorzConversionResults = start + "\r\n" + saveState3 + saveState4 + final;
                ConvertDep.CurrentVertConversionResults = start + "\r\n" + saveState + saveState2 + final;
                ConvertDep.SuccessLog.Add( $"Hodiax {ConvertDep.Words[1]} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Hodiax Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadHodiaxHorizontalBatchHod()
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;
                var h = $"H-";
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentHorzConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var hodiaxHorizDir = ConvertDep.TargetDirectory + $"Hodiax_Horizontal";

                if (!Directory.Exists( hodiaxHorizDir )) Directory.CreateDirectory( hodiaxHorizDir );

                fileName = fileName.Substring( 0, fileName.LastIndexOf(
                               ".", StringComparison.Ordinal ) ) + ".hod";

                if (!File.Exists( $"{hodiaxHorizDir}\\{h}{fileName}" ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{hodiaxHorizDir}\\{h}{fileName}", value );
                        tempVar.Close( );
                            ConvertDep.SuccessLog.Add( $"Hodiax {ConvertDep.Words[1]} Horz .hod Converted Successfully" );
                        }
                    }

                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"Hodiax Horizontal Return Pattern Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadHodiaxHorizontalBatchTxt()
            {
                try
                {
                    var fileName = ConvertDep.CurrentFileName;
                    var h = $"H-";
                    string[] separators = { "\r\n" };
                    var value = ConvertDep.CurrentHorzConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var hodiaxHorizDir = ConvertDep.TargetDirectory + $"Hodiax_Horizontal";

                    if (!Directory.Exists( hodiaxHorizDir )) Directory.CreateDirectory( hodiaxHorizDir );


                    fileName = fileName.Substring( 0, fileName.LastIndexOf(
                                   ".", StringComparison.Ordinal ) ) + ".txt";

                    if (!File.Exists( $"{hodiaxHorizDir}\\{h}{fileName}" ))
                    {
                        using (var tempVar = File.Create( fileName, 1024 ))
                        {
                            File.WriteAllText( $"{hodiaxHorizDir}\\{h}{fileName}", value );
                            tempVar.Close( );
                            ConvertDep.SuccessLog.Add( $"Hodiax {ConvertDep.Words[1]} Horz .txt Converted Successfully" );
                        }
                    }
                   }
                catch (FormatException db)
                {
                    ConvertDep.ErrorLog.Add( $"Hodiax Horizontal Return Pattern Format Exception\n" +
                                             $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                                             $"Please check values in 'Textboxes' are correct\n"
                                             + db.Message );
                }
            }

        public void DownloadHodiaxVerticalBatchHod()
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;
                var e = $"E-";
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentVertConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                    ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var hodiaxVertDir = ConvertDep.TargetDirectory + $"Hodiax_Vertical";
                if (!Directory.Exists( hodiaxVertDir )) Directory.CreateDirectory( hodiaxVertDir );

                fileName = fileName.Substring( 0, fileName.LastIndexOf(
                               ".", StringComparison.Ordinal ) ) + ".txt";

                if (!File.Exists( $"{hodiaxVertDir}\\{e}{fileName}" ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{hodiaxVertDir}\\{e}{fileName}", value );
                        tempVar.Close( );
                        }
                    }
                fileName = fileName.Substring( 0, fileName.LastIndexOf(
                               ".", StringComparison.Ordinal ) ) + ".hod";

                if (!File.Exists( $"{hodiaxVertDir}\\{e}{fileName}" ))
                    {
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{hodiaxVertDir}\\{e}{fileName}", value );
                        tempVar.Close( );
                            ConvertDep.SuccessLog.Add( $"Hodiax {ConvertDep.Words[1]} Vert .hod Converted Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"Hodiax Download Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadHodiaxVerticalBatchTxt()
        {
            try
            {
                var fileName = ConvertDep.CurrentFileName;
                var e = $"E-";
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentVertConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var hodiaxVertDir = ConvertDep.TargetDirectory + $"Hodiax_Vertical";
                if (!Directory.Exists( hodiaxVertDir )) Directory.CreateDirectory( hodiaxVertDir );

                fileName = fileName.Substring( 0, fileName.LastIndexOf(
                               ".", StringComparison.Ordinal ) ) + ".txt";

                if (!File.Exists( $"{hodiaxVertDir}\\{e}{fileName}" ))
                {
                    using (var tempVar = File.Create( fileName, 1024 ))
                    {
                        File.WriteAllText( $"{hodiaxVertDir}\\{e}{fileName}", value );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"Hodiax {ConvertDep.Words[1]} Vert .txt Converted Successfully" );
                        }
                }
            }
            catch (Exception db)
            {
                ConvertDep.ErrorLog.Add( $"Hodiax Download Exception\n" +
                                         $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                                         $"Please check values in 'Textboxes' are correct\n"
                                         + db.Message );
            }
        }
        }
    }
