using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using NsExcel = Microsoft.Office.Interop.Excel;
using Path = System.IO.Path;
using PlanetConverter.Models;
using log4net;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace PlanetConverter
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        public bool IsAtollExNew;
        public NsExcel.Worksheet EXlBkOpen;
        public NsExcel.Workbook WorkBook;
        public List<AtollPillaLst> Datos = new List<AtollPillaLst>( );
        public int RowCt = 2;
        public int DatOsCt;
        private AirCom AirC = new AirCom( );
        private Atoll Atoll;
        private Ce4 C4 = new Ce4( );
        private CelCad CelC = new CelCad( );
        private CelPlan CelP = new CelPlan( );
        private GeoPlan GeoP = new GeoPlan( );
        private Granet GraN = new Granet( );
        private Hodiax HodX = new Hodiax( );
        private Hydra HyD = new Hydra( );
        private LccNet LccN = new LccNet( );
        private NetPlan NetP = new NetPlan( );
        private Odyssey Ody = new Odyssey( );
        private PathLoss PathL = new PathLoss( );
        private Wizard Wiz = new Wizard( );

        public MainWindow( )
            {
            InitializeComponent( );
            ConvertDep.ErrorLog = new List<string>( );
            ConvertDep.SuccessLog = new List<string>( );

            Atoll = new Atoll
                {
                IsAtollExNew = true,
                Datos = new List<AtollPillaLst>( ),
                DatOsCt = 0,
                RowCt = 2,
                };
            CleanIngestDirectory( );


            }

        #region Testing Methods
        private void PopulateButton_Click( object sender, RoutedEventArgs e )
            {
            AtvswrTextBox.Text = $"2.0";
            AzimuthDisplayTextBox.Text = $"360";
            BeamwidthTextBox.Text = $"19";
            CommentsTextBox.Text = $"...Some droll commentary";
            DateTextBox.Text = $"4/18/2017";
            DateMesuredTextBox.Text = $"1/1/2016";
            DepthTextBox.Text = $"180";
            DescriptionTextBox.Text = $"Panel";
            DimensionsTextBox.Text = $"305";
            FamilyTextBox.Text = $"TWIN654";
            FccIdTextBox.Text = $"2AJQZ-AVS-X2015 ";
            GainTextBox.Text = $"15.8";
            HeightTextBox.Text = $"1298";
            ConvertDep.HighFrequency = $"2400";
            LengthTextBox.Text = $"2.580";
            LobeTiltTextBox.Text = $"0";
            LowFrequencyTextBox.Text = $"696";
            ConvertDep.LowerFrequency = $"696";
            MaxFrequencyTextBox.Text = $"2400";
            MaxGainTextBox.Text = $"10.8";
            ConvertDep.MaxPower = $"240";
            MeasFrequencyTextBox.Text = $"824";
            MinFrequencyTextBox.Text = $"696";
            PolarizationTextBox.Text = $"+45";
            ConvertDep.Size = $"3";
            TimeTextBox.Text = $"3:33am";
            ConvertDep.UpperFrequency = $"2400";
            WidthTextBox.Text = $"305";
            WeightTextBox.Text = $"305";
            WindAreaTextBox.Text = $".7";
            }
        private void InitializeConversionDependencies( )
            {
            ConvertDep.AntHeight = HeightTextBox.Text;
            ConvertDep.AntWidth = WidthTextBox.Text;
            ConvertDep.Atvswr = AtvswrTextBox.Text;
            ConvertDep.AzimuthDisplay = AzimuthDisplayTextBox.Text;
            ConvertDep.Beamwidth = BeamwidthTextBox.Text;
            ConvertDep.Comments = CommentsTextBox.Text;
            ConvertDep.Date = DateTextBox.Text;
            ConvertDep.DateMeasured = DateTextBox.Text;
            ConvertDep.Depth = DepthTextBox.Text;
            ConvertDep.Description = DescriptionTextBox.Text;
            ConvertDep.Dimensions = DimensionsTextBox.Text;
            ConvertDep.ElectricalTilt = ElectricalTiltTextBox.Text;
            ConvertDep.Family = FamilyTextBox.Text;
            ConvertDep.FccId = FccIdTextBox.Text;
            ConvertDep.Gain = GainTextBox.Text;
            ConvertDep.HighFrequency = HighFrequencyTextBox.Text;
            ConvertDep.Height = HeightTextBox.Text;
            ConvertDep.Length = LengthTextBox.Text;
            ConvertDep.LobeTilt = LobeTiltTextBox.Text;
            ConvertDep.LowFrequency = LowFrequencyTextBox.Text;
            ConvertDep.LowerFrequency = LowerFrequencyTextBox.Text;
            ConvertDep.MaxFrequency = MaxFrequencyTextBox.Text;
            ConvertDep.MaxGain = MaxGainTextBox.Text;
            ConvertDep.MaxPower = MaxPowerTextBox.Text;
            ConvertDep.MeasFrequency = MeasFrequencyTextBox.Text;
            ConvertDep.MfrId = MfrIdTextBox.Text;
            ConvertDep.MinGain = MinGainTextBox.Text;
            ConvertDep.MinFrequency = MinFrequencyTextBox.Text;
            ConvertDep.PatternElectricalTilt = PatternElectricalTiltTextBox.Text;
            ConvertDep.Polarization = PolarizationTextBox.Text;
            ConvertDep.Size = SizeTextBox.Text;
            ConvertDep.Time = TimeTextBox.Text;
            ConvertDep.UpperFrequency = UpperFrequencyTextBox.Text;
            ConvertDep.Weight = WeightTextBox.Text;
            ConvertDep.Width = WidthTextBox.Text;
            ConvertDep.WindArea = WindAreaTextBox.Text;
            }
        #endregion

        #region Conversion / Download Methods
        private void Src_Dir_Button_Click( object sender, RoutedEventArgs e )
            {
            try
                {
                using (var fbd = new FolderBrowserDialog( ))
                    {
                    var dirIsValid = fbd.ShowDialog( );
                    if (dirIsValid != System.Windows.Forms.DialogResult.OK ||
                        string.IsNullOrWhiteSpace( fbd.SelectedPath )) return;
                    ConvertDep.SourcePlanetDirectory = fbd.SelectedPath;
                    }
                }
            catch (DirectoryNotFoundException db)
                {
                ConvertDep.ErrorLog.Add( $"Source directory not valid\n" +
                              $"Please select a valid directory\n"
                              + db.Message );
                }
            }
        private void Target_Dir_Button_Click( object sender, RoutedEventArgs e )
            {
            try
                {
                using (var fbd = new FolderBrowserDialog( ))
                    {
                    var targetIsValid = fbd.ShowDialog( );
                    if (targetIsValid != System.Windows.Forms.DialogResult.OK ||
                        string.IsNullOrWhiteSpace( fbd.SelectedPath )) return;
                    ConvertDep.TargetDirectory = fbd.SelectedPath + $"\\";
                    }
                }
            catch (DirectoryNotFoundException db)
                {

                ErrorResults.Text = $"Target directory not valid\n" +
                              $"Please select a valid output directory" + db.Message;
                }
            }
        private void Convert_Download_Button_Click( object sender, RoutedEventArgs e )
            {
            InitializeConversionDependencies( );
            GetAllFilesInDirectory( ConvertDep.SourcePlanetDirectory );
            }
        private void GetAllFilesInDirectory( string filePath )
            {
            #region Ingest

            ConvertDep.FileEntries = Directory.GetFiles( filePath );
            var location = Assembly.GetExecutingAssembly( ).Location;
            if (location != null)
                ConvertDep.IngestDirectory = Path.Combine(
                    Path.GetDirectoryName( location ), @"Ingest\" );
            foreach (var fileLocation in ConvertDep.FileEntries)
                {
                ConvertDep.CurrentFileName = "";
                ConvertDep.CurrentIngestedtFile = string.Empty;
                ConvertDep.Words = new[] { string.Empty };
                ConvertDep.CurrentFileName = Path.GetFileName( fileLocation );

                //substring
                if (ConvertDep.CurrentFileName != null)
                    {
                    var trimName = ConvertDep.CurrentFileName.Split( '.' )[0];

                    var name = Regex.Split( trimName, "-" );
                    //Extract Data From FileName
                    if (name.Length == 5)
                        {
                        //method #1
                        ConvertDep.Polarization = ConvertDep.CurrentFileName.Split( '(', ')' )[1];
                        var tiltVal = name[1];
                        ConvertDep.TiltValue = tiltVal.Split( 'T', '-' )[1];
                        }

                    //fileName
                    var pathToCheck = $"{ConvertDep.IngestDirectory}{ConvertDep.CurrentFileName}";

                    if (File.Exists( pathToCheck ))
                        {
                        var fileObj = new FileStream( pathToCheck, FileMode.Open, FileAccess.Read );
                        var readerObj = new StreamReader( fileObj );
                        var text = readerObj.ReadToEnd( );
                        readerObj.Close( );

                        ConvertDep.CurrentIngestedtFile = text;
                        }
                    try
                        {
                        if (!File.Exists( pathToCheck ))
                            {
                            File.Copy( fileLocation, ConvertDep.IngestDirectory + ConvertDep.CurrentFileName );
                            var fileObj = new FileStream( ConvertDep.IngestDirectory + ConvertDep.CurrentFileName,
                                FileMode.Open, FileAccess.Read );

                            var readerObj = new StreamReader( fileObj );
                            var text = readerObj.ReadToEnd( );
                            readerObj.Close( );
                            fileObj.Close( );

                            ConvertDep.CurrentIngestedtFile = text;
                            ConvertDep.FileCount++;
                            }
                        }
                    catch (FileNotFoundException db)
                        {
                        ConvertDep.ErrorLog.Add( $"Encountered an error\n" +
                                                $"Please check {ConvertDep.CurrentFileName} is valid\n" +
                                                db.Message );
                        }
                    }

                #endregion

                #region Convert / Download

                try
                    {
                    if (AircomCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        AirC.ConvertToAircomBatch( );
                        AirC.DownloadAircom( );
                        }
                    if (AtollCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        Atoll.ConvertToAtollBatch( );
                        }
                    if (Ce4CheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        C4.ConvertToCe4Batch( );
                        if (Ce4TxtRadioButton.IsChecked.GetValueOrDefault( )) C4.DownloadCe4Txt( );
                        if (Ce4VwaRadioButton.IsChecked.GetValueOrDefault( )) C4.DownloadCe4Vwa( );
                        }
                    if (CelCadCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        CelC.ConvertToCelCadBatchDownload( );
                        }
                    if (CelPlanCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        CelP.ConvertToCelPlanBatch( );
                        CelP.DownloadCelPlanBatch( );
                        }

                    if (GeoplanCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        GeoP.ConvertToGeoPlanBatch( );
                        if (GeoPlanVwaRadioButton.IsChecked.GetValueOrDefault( )) GeoP.DownloadGeoPlanVwa( );
                        if (GeoPlanTxtRadioButton.IsChecked.GetValueOrDefault( )) GeoP.DownloadGeoPlanTxt( );
                        }
                    if (GranetCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        GraN.ConvertToGranetBatch( );
                        if (GranetPatRadioButton.IsChecked.GetValueOrDefault( )) GraN.DownloadGranetPat( );
                        if (GranetTxtRadioButton.IsChecked.GetValueOrDefault( )) GraN.DownloadGranetTxt( );
                        }
                    if (HodiaxCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        HodX.ConvertToHodiaxBatch( );

                        if (HodiaxHodRadioButton.IsChecked.GetValueOrDefault( ))
                            {
                            HodX.DownloadHodiaxHorizontalBatchHod( );
                            HodX.DownloadHodiaxVerticalBatchHod( );
                            }

                        if (HodiaxTxtRadioButton.IsChecked.GetValueOrDefault( ))
                            {
                            HodX.DownloadHodiaxHorizontalBatchTxt( );
                            HodX.DownloadHodiaxVerticalBatchTxt( );
                            }
                        }
                    if (HydraCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        HyD.ConvertToHydraBatch( );
                        if (HydraAdfRadioButton.IsChecked.GetValueOrDefault( )) HyD.DownloadHydraAdf( );
                        if (HydraTxtRadioButton.IsChecked.GetValueOrDefault( )) HyD.DownloadHydraTxt( );
                        }
                    if (LccNetCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        LccN.ConvertToLccBatchTest( );
                        if (LccAntRadioButton.IsChecked.GetValueOrDefault( )) LccN.DownloadLccBatchAnt( );
                        if (LccTxtRadioButton.IsChecked.GetValueOrDefault( )) LccN.DownloadLccBatchTxt( );
                        }
                    if (NetplanCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        NetP.ConvertToNetPlanBatch( );
                        NetP.DownloadNetPlanBatch( );
                        }
                    if (OdesseyCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        Ody.ConvertToOdysseyBatch( );
                        Ody.DownloadOdysseyBatch( );
                        }
                    if (PathLossCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        PathL.ConvertToPathLossBatch( );
                        if (PathLossTxtRadioButton.IsChecked.GetValueOrDefault( )) PathL.DownloadPathLossBatchTxt( );
                        if (PathLossAdfRadioButton.IsChecked.GetValueOrDefault( )) PathL.DownloadPathLossBatchAdf( );
                        }
                    if (WizardCheckBox.IsChecked.GetValueOrDefault( ))
                        {
                        Wiz.ConvertToWizardBatch( );
                        if (WizardApfRadioButton.IsChecked.GetValueOrDefault( )) Wiz.DownloadWizardBatchApf( );
                        if (WizardTxtRadioButton.IsChecked.GetValueOrDefault( )) Wiz.DownLoadWizardBatchTxt( );
                        }

                    #endregion
                    }
                catch (InvalidOperationException db)
                    {
                    ConvertDep.ErrorLog.Add( $"Error encountered with File Ingest || download\n" +
                                            $"Please try again || restart the application\n" +
                                            $"Validate working directories are clean"
                                            + db.Message );
                    }
                }
            var s = ConvertDep.SuccessLog.Aggregate( string.Empty, ( current, entry ) => current + (entry + "\r\n") );
            var e = ConvertDep.ErrorLog.Aggregate( string.Empty, ( current, entry ) => current + (entry + "\r\n") );

            SuccessResults.Text = s += $"\nTotal Files Ingested : {ConvertDep.FileCount}" +
                                       $"\nTotal Files Affected: {ConvertDep.SuccessLog.Count}";

            ErrorResults.Text = e;
            }
        #endregion

        #region View Helper Methods
        private void SelectAllFormatsCheckBox_OnChecked( object sender, RoutedEventArgs e )
            {

            AircomCheckBox.IsChecked = true;
            AtollCheckBox.IsChecked = true;
            Ce4CheckBox.IsChecked = true;
            CelCadCheckBox.IsChecked = true;
            CelPlanCheckBox.IsChecked = true;
            GeoplanCheckBox.IsChecked = true;
            GranetCheckBox.IsChecked = true;
            HodiaxCheckBox.IsChecked = true;
            HydraCheckBox.IsChecked = true;
            LccNetCheckBox.IsChecked = true;
            NetplanCheckBox.IsChecked = true;
            OdesseyCheckBox.IsChecked = true;
            PathLossCheckBox.IsChecked = true;
            WizardCheckBox.IsChecked = true;

            Ce4VwaRadioButton.IsChecked = true;
            GeoPlanVwaRadioButton.IsChecked = true;
            GranetPatRadioButton.IsChecked = true;
            HydraAdfRadioButton.IsChecked = true;
            HodiaxHodRadioButton.IsChecked = true;
            LccAntRadioButton.IsChecked = true;
            PathLossAdfRadioButton.IsChecked = true;
            WizardApfRadioButton.IsChecked = true;

            }
        private void SelectAllFormatsCheckBox_OnUnchecked( object sender, RoutedEventArgs e )
            {
            AircomCheckBox.IsChecked = false;
            AtollCheckBox.IsChecked = false;
            Ce4CheckBox.IsChecked = false;
            CelCadCheckBox.IsChecked = false;
            CelPlanCheckBox.IsChecked = false;
            GeoplanCheckBox.IsChecked = false;
            GranetCheckBox.IsChecked = false;
            HodiaxCheckBox.IsChecked = false;
            HydraCheckBox.IsChecked = false;
            LccNetCheckBox.IsChecked = false;
            NetplanCheckBox.IsChecked = false;
            OdesseyCheckBox.IsChecked = false;
            PathLossCheckBox.IsChecked = false;
            WizardCheckBox.IsChecked = false;

            Ce4TxtRadioButton.IsChecked = true;
            GeoPlanTxtRadioButton.IsChecked = true;
            GranetTxtRadioButton.IsChecked = true;
            HydraTxtRadioButton.IsChecked = true;
            HodiaxTxtRadioButton.IsChecked = true;
            LccTxtRadioButton.IsChecked = true;
            PathLossTxtRadioButton.IsChecked = true;
            WizardTxtRadioButton.IsChecked = true;

            }
        private void CleanIngestDirectory( )
            {
            var location = Assembly.GetExecutingAssembly( ).Location;
            ConvertDep.IngestDirectory = Path.Combine(
                Path.GetDirectoryName( location ), @"Ingest\" );
            foreach (var file in Directory.GetFiles( ConvertDep.IngestDirectory ))
                {
                File.Delete( file );
                }
            }
        private void Reset_Values_Button_Click( object sender, RoutedEventArgs e )
            {
            IsAtollExNew = true;
            Datos = new List<AtollPillaLst>( );
            RowCt = 2;
            DatOsCt = 0;
            AirC = new AirCom( );
            Atoll = new Atoll
            {
                IsAtollExNew = true,
                Datos = new List<AtollPillaLst>( ),
                DatOsCt = 0,
                RowCt = 2,
            };
            C4 = new Ce4( );
            CelC = new CelCad( );
            CelP = new CelPlan( );
            GeoP = new GeoPlan( );
            GraN = new Granet( );
            HodX = new Hodiax( );
            HyD = new Hydra( );
            LccN = new LccNet( );
            NetP = new NetPlan( );
            Ody = new Odyssey( );
            PathL = new PathLoss( );
            Wiz = new Wizard( );

            ConvertDep.ErrorLog = new List<string>( );
            ConvertDep.SuccessLog = new List<string>( );
            CleanIngestDirectory( );
            }
        #endregion
        }
    }