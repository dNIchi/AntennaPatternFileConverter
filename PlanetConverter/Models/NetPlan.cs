using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace PlanetConverter.Models
    {
    public class NetPlan
        {
        public string NetPlanHreturnPattern( string[] words, int start, int fin )
            {

            var saveState = "";
            var strStack = new Stack<string>( );
            var aryLst = new ArrayList( );

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
                    saveState += value + "\t";
                    aryLst.Add( saveState );
                    if (aryLst.Count == 10)
                        {
                        saveState += "\r\n";
                        aryLst.Clear( );
                        }
                    }

                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"NetPlan Horizontal Return Pattern Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }
            return saveState;
            }
        public string NetPlanVreturnPattern( string[] words, int start, int fin )
            {

            var saveState = "";
            var strStack = new Stack<string>( );
            var aryLst = new ArrayList( );
            aryLst.Add( "" );
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
                    saveState += value + "\t";
                    aryLst.Add( saveState );
                    if (aryLst.Count == 10)
                        {
                        saveState += "\r\n";
                        aryLst.Clear( );
                        }
                    }
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"NetPlan Vertical Return Pattern Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            return saveState;
            }
        public void ConvertToNetPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = ConvertDep.Words.Length;
                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";

                if (totalWords <= 0) ConvertDep.ErrorLog.Add( $"NetPlan {totalWords} Empty\nCheck file source is correct" );

                var start = "Name = " + ConvertDep.Words[1] + "\r\n" +
                            "Number of Points = 360\r\n" +
                            "Start Ang.= 0\r\n" +
                            "Range = " + ConvertDep.Words[7] + "\r\n" +
                            "Increment = 1\r\n" +
                            "Orientation = Horizontal";
                var start2 = "Name = " + ConvertDep.Words[1] + "\r\n" +
                             "Number of Points = 360\r\n" +
                             "Start Ang. = -180\r\n" +
                             "Range = " + ConvertDep.Words[9] + "\r\n" +
                             "Increment = 1\r\n" +
                             "Orientation = Vertical";
                var aryLst = new ArrayList( );
                var strStack = new Stack<string>( );

                for (var i = 23; i <= 739; i += 2)
                    {
                    var newvalue = ConvertDep.Words[i];
                    strStack.Push( (Convert.ToDouble( newvalue ) * -1).ToString( "0.0" ) );
                    }
                for (var i = 21; i <= 21; i += 2)
                    {
                    var newvalue = ConvertDep.Words[i];
                    strStack.Push( (Convert.ToDouble( newvalue ) * -1).ToString( "0.0" ) );
                    }
                foreach (var strVal in strStack)
                    {
                    saveState += strVal + "\t";
                    aryLst.Add( saveState );
                    if (aryLst.Count == 10)
                        {
                        saveState += "\r\n";
                        aryLst.Clear( );
                        }
                    }
                for (var i = 743; i <= totalWords - 2; i += 2)
                    {
                    var newValue = ConvertDep.Words[i];
                    saveState2 += (Convert.ToDouble( newValue ) * -1).ToString( "0.0" ) + "\t";
                    aryLst.Add( saveState );
                    if (aryLst.Count == 10)
                        {
                        saveState2 += "\r\n";
                        aryLst.Clear( );
                        }
                    }
                saveState2 = NetPlanHreturnPattern( ConvertDep.Words, 743, 1105 );
                saveState3 = NetPlanVreturnPattern( ConvertDep.Words, 1105, totalWords );
                ConvertDep.CurrentVertConversionResults = start2 + "\r\n" + saveState2 + saveState3;
                ConvertDep.CurrentVertConversionResults = start + "\r\n" + saveState;
                ConvertDep.SuccessLog.Add( $"NetPlan {ConvertDep.CurrentFileName} Converted Successfully" );
                }
            catch (FormatException db)
                {
                ConvertDep.ErrorLog.Add( $"NetPlan Conversion Format Exception\n" +
                          $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                          $"Please check values in 'Textboxes' are correct\n"
                          + db.Message );
                }
            }
        public void DownloadNetPlanBatch( )
            {
            try
                {
                var fileName = ConvertDep.CurrentFileName;
                var hFileName = "";
                var vFileName = "";

                var hValue = ConvertDep.CurrentHorzConversionResults;
                var vValue = ConvertDep.CurrentVertConversionResults;

                string[] hSeparators = { "\r\n" };
                string[] vSeparators = { "\r\n" };

                var vCommavalue = vValue.Replace( "\t", "\r\n" );
                var hCommavalue = hValue.Replace( "\t", "\r\n" );

                string[] vWords = vCommavalue.Split( vSeparators, StringSplitOptions.RemoveEmptyEntries );
                string[] hWords = hCommavalue.Split( hSeparators, StringSplitOptions.RemoveEmptyEntries );

                var netPlanHdir = ConvertDep.TargetDirectory + $"NetPlan_Horizontal";
                var netPlanVdir = ConvertDep.TargetDirectory + $"NetPlan_Vertical";

                if (!Directory.Exists( netPlanHdir )) Directory.CreateDirectory( netPlanHdir );


                if (!Directory.Exists( netPlanVdir )) Directory.CreateDirectory( netPlanVdir );

                hFileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".h";

                if (!File.Exists( netPlanHdir + hFileName ))
                    {
                    using (var tempVar = File.Create( hFileName, 1024 ))
                        {
                        File.WriteAllText( $"{netPlanHdir}\\{hFileName}", hValue );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"NetPlan {ConvertDep.CurrentFileName}  .h Converted Successfully" );
                        }
                    }

                vFileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".v";

                if (!File.Exists( netPlanVdir + vFileName ))
                    {
                    using (var tempVar = File.Create( vFileName, 1024 ))
                        {
                        File.WriteAllText( $"{netPlanVdir}\\{vFileName}", vValue );
                        tempVar.Close( );
                        ConvertDep.SuccessLog.Add( $"NetPlan {ConvertDep.CurrentFileName} .v Converted Successfully" );
                        }
                    }
                }
            catch (Exception db)
                {
                ConvertDep.ErrorLog.Add( $"NetPlan Download Exception\n" +
                             $"Please Check Directory is valid\n" +
                             $"Please Ensure You have Write Access"
                             + db.Message );
                }
            }
        }
    }
