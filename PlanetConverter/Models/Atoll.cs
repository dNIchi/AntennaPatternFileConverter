using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using NsExcel = Microsoft.Office.Interop.Excel;
using Path = System.IO.Path;


namespace PlanetConverter.Models
    {
   
    public class Atoll
        {
            public bool IsAtollExNew;
            public NsExcel.Worksheet EXlBkOpen;
            public NsExcel.Workbook WorkBook;
            public List<AtollPillaLst> Datos;
            public int RowCt = 2;
            public int DatOsCt;

        public void ConvertToAtollBatch( )
            {
            try
                {
                string[] separators = { "\n" };
                var value = ConvertDep.CurrentIngestedtFile;
                var commavalue = value.Replace( "\t", "\n" );
                    ConvertDep.Words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var cont1 = -1;
                // var count = 0;
                var count3 = 0.0;
                var saveState = "";


                for (int i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    string newvalue = ConvertDep.Words[i];
                    double valor = Math.Round( (Convert.ToDouble( ConvertDep.MaxGain ) - Convert.ToDouble( newvalue )), 2 );
                    count3 += 0.1;
                        ConvertDep.ConversionResults = string.Empty;
                        ConvertDep.ConversionResults = saveState += cont1.ToString( ) + " " + Math.Round( count3, 2 ) + " ";
                    }

                var dateMeasured = Convert.ToDateTime( ConvertDep.Date );
                int k;
                int.TryParse( ConvertDep.TiltValue, out k );
                var d = new AtollPillaLst( )
                    {
                    Name = ConvertDep.Words[1],
                    Name2 = ConvertDep.Words[1],
                    Gain = ConvertDep.MaxGain,
                    Manuf = ConvertDep.Words[3],
                    Comm = ConvertDep.Comments,
                    Patt = "2 0 0 360 " + saveState,
                    PET = k.ToString( ),//<-------
                    Beam = ConvertDep.Beamwidth,//<-------
                    Fmin = ConvertDep.MinFrequency,//<-------
                     Fmax = ConvertDep.MaxFrequency,//<-------
                    Freq = ConvertDep.Words[5], //Frequency
                    VWidth = ConvertDep.Words[9],
                    FTB = ConvertDep.Words[11],
                    Tilt = ConvertDep.TiltValue,
                    Hwidth = ConvertDep.Words[7],
                    Fam = ConvertDep.Family,//<-------
                    Dim = ConvertDep.Dimensions,//<-------
                    Weight = ConvertDep.Weight,//<-------
                    PPD = dateMeasured.ToString( "yyyy_mm_dd" )//<-------

                    };
                Datos.Add( d );
                //var rowCt = 2; // todo var _fileEntries[] 
                NsExcel.ApplicationClass excelApp = new NsExcel.ApplicationClass( );


                if (IsAtollExNew || DatOsCt == ConvertDep.FileEntries.Length)
                    {
                    if (IsAtollExNew)
                        {
                        excelApp.Visible = true; IsAtollExNew = false;
                        var workBookPath = Path.Combine(Environment.CurrentDirectory,@"Assets\Atoll.xlsx");
                        WorkBook = excelApp.Workbooks.Open( workBookPath, 0, false, 5, "", "", false,
                        NsExcel.XlPlatform.xlWindows, "", true, false, 0, true, false, false );
                        var sheetOnOpen = (NsExcel.Worksheet)WorkBook.Sheets[1];
                        EXlBkOpen = sheetOnOpen;
                           
                        }
                    #region Hydrate Excel 
                    if (RowCt <= 2)
                        {
                        EXlBkOpen.Cells[1, 1] = $"Name";
                        EXlBkOpen.Cells[1, 2] = $"Model";
                        EXlBkOpen.Cells[1, 3] = $"Gain (dbi)";
                        EXlBkOpen.Cells[1, 4] = $"Manufacturer";
                        EXlBkOpen.Cells[1, 5] = $"Comments";
                        EXlBkOpen.Cells[1, 6] = $"Pattern";
                        EXlBkOpen.Cells[1, 7] = $"Pattern Electrical Tilt(?) ";
                        EXlBkOpen.Cells[1, 8] = $"BeamWidth";
                        EXlBkOpen.Cells[1, 9] = $"FMin";
                        EXlBkOpen.Cells[1, 10] = $"FMax";
                        EXlBkOpen.Cells[1, 11] = $"Frequency";
                        EXlBkOpen.Cells[1, 12] = $"VWidth";
                        EXlBkOpen.Cells[1, 13] = $"Front To Back";
                        EXlBkOpen.Cells[1, 14] = $"Tilt";
                        EXlBkOpen.Cells[1, 15] = $"H Width";
                        EXlBkOpen.Cells[1, 16] = $"Family";
                        EXlBkOpen.Cells[1, 17] = $"Dimensions HxWxD (inches)";
                        EXlBkOpen.Cells[1, 18] = $"Weight (lbs)";
                        EXlBkOpen.Cells[1, 19] = $"Pattern Posting Date";
                        IsAtollExNew = false;
                        }
                
                    }


                if (DatOsCt <= ConvertDep.FileEntries.Length)
                    {
                    EXlBkOpen.Cells[RowCt, 1] = Datos[DatOsCt].Name;
                    EXlBkOpen.Cells[RowCt, 2] = Datos[DatOsCt].Name2;
                    EXlBkOpen.Cells[RowCt, 3] = Datos[DatOsCt].Gain;
                    EXlBkOpen.Cells[RowCt, 4] = Datos[DatOsCt].Manuf;
                    EXlBkOpen.Cells[RowCt, 5] = Datos[DatOsCt].Comm;
                    EXlBkOpen.Cells[RowCt, 6] = Datos[DatOsCt].Patt;
                    EXlBkOpen.Cells[RowCt, 7] = Datos[DatOsCt].PET;
                    EXlBkOpen.Cells[RowCt, 8] = Datos[DatOsCt].Beam;
                    EXlBkOpen.Cells[RowCt, 9] = Datos[DatOsCt].Fmin;
                    EXlBkOpen.Cells[RowCt, 10] = Datos[DatOsCt].Fmax;
                    EXlBkOpen.Cells[RowCt, 11] = Datos[DatOsCt].Freq;
                    EXlBkOpen.Cells[RowCt, 12] = Datos[DatOsCt].VWidth;
                    EXlBkOpen.Cells[RowCt, 13] = Datos[DatOsCt].FTB;
                    EXlBkOpen.Cells[RowCt, 14] = Datos[DatOsCt].Tilt;
                    EXlBkOpen.Cells[RowCt, 15] = Datos[DatOsCt].Hwidth;
                    EXlBkOpen.Cells[RowCt, 16] = Datos[DatOsCt].Fam;
                    EXlBkOpen.Cells[RowCt, 17] = Datos[DatOsCt].Dim;
                    EXlBkOpen.Cells[RowCt, 18] = Datos[DatOsCt].Weight;
                    EXlBkOpen.Cells[RowCt, 19] = Datos[DatOsCt].PPD;
                    RowCt++;
                    ++DatOsCt;
                    ConvertDep.SuccessLog.Add($"{ConvertDep.CurrentFileName} added on Row{DatOsCt}");
                    if (DatOsCt == ConvertDep.FileEntries.Length)
                        {
                        var sT = DateTime.Now.ToShortDateString( );
                        var time = sT.Replace( '/', '_' );
                        var savePath = $"{ConvertDep.TargetDirectory}Atoll_{ConvertDep.Family}_{time}.xlsx";
                        WorkBook.SaveAs( savePath );
                        excelApp.Workbooks.Close( );
                        excelApp.Quit( );
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                        }
                    }

                #endregion
                }
            catch (FormatException db)
                {
                    ConvertDep.ErrorLog.Add( $"Atoll Conversion Format Exception\n" +
                              $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                              $"Please check values in 'Textboxes' are correct\n"
                              + db.Message );
                }
            }
        }
    }
