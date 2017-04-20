using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
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
        #region --global
        private string _currentIngestedPlanetFileName;
        private string _currentIngestedPlanetFile;
        private string _appSrcPlnDir;
        private string _ingestDir = $"C:\\Code\\PRJ-2_PlanetConvert\\PlanetFilesIngestDir\\";
        private string _appTargetDir;
        private string[] _fileEntries;
        private string[] _words;

        #region T/F
        private bool _isAtollExNew = true;
        private bool _aircomChecked,

                      _atollChecked,

                        _ce4Checked,
                     _ce4VwaChecked,
                     _ce4TxtChecked,

                     _celCadChecked,

                    _celPlanChecked,

                    _geoPlanChecked,
                 _geoPlanVwaChecked,
                 _geoPlanTxtChecked,

                     _granetChecked,
                  _granetPatChecked,
                  _granetTxtChecked,

                     _hodiaxChecked,
                  _hodiaxHodChecked,
                  _hodiaxTxtChecked,

                      _hydraChecked,
                   _hydraAdfChecked,
                   _hydraTxtChecked,

                     _lccNetChecked,
                     _lccAntChecked,
                     _lccTxtChecked,

                    _netPlanChecked,

                    _odysseyChecked,
                   _pathLossChecked,
                _pathLossAdfChecked,
                _pathLossTxtChecked,

                     _wizardChecked,
                 _wizardApfChecked,
                 _wizardTxtChecked = false;

        #endregion

        #region All Fields
        private string _antWidth;
        private string _atvswr;
        private string _azimuthDisplay;
        private string _beamwidth;
        private string _comments;
        private string _date;
        private string _dateMesured;
        private string _depth;
        private string _description;
        private string _dimensions;
        private string _electricalTilt;
        private string _family;
        private string _fccId;
        private string _gain;
        private string _antHeight;
        private string _highFrequency;
        private string _length;
        private string _lobeTilt;
        private string _lowFrequency;
        private string _lowerFrequency;
        private string _maxFrequency;
        private string _maxGain;
        private string _maxPower;
        private string _measFrequency;
        private string _mfrId;
        private string _minGain;
        private string _minFrequency;
        private string _polarization;
        private string _size;
        private string _time;
        private string _upperFrequency;
        private string _weight;
        private string _windArea;

        #endregion

        #region File Name / Substring 



        private string[] _name;
        private string _polRes;
        private string _tiltValue;
        private string _trimmedFileName;
        private string _trimmedTiltVal;
        #endregion

        #region  Conversion Results
        private string _airComConversionResults;
        private string _atollConversionResults;
        private string _ce4ConversionResults;
        private string _celCadHorizontalConversionResults;
        private string _celCadVerticalConversionResults;
        private string _celCadReturnPatternSaveState;
        private string _celPlanConversionResults;
        private string _geoPlanConversionResults;
        private string _granetConversionResults;
        private string _hodiaxHorizontalConversionResults;
        private string _hodiaxVerticalConversionResults;
        private string _hydraConversionResults;
        private string _lccConversionResults;
        private string _netPlanHorizontalConversionResults;
        private string _netPlanVerticalConversionResults;
        private string _odysseyConversionResults;
        private string _pathLossConversionResults;
        private string _wizardConversionResults;
        #endregion


        #endregion

        #region ATOLL Var
        private NsExcel.Worksheet _eXlBkOpen;
        private NsExcel.Workbook _workBook;
        private List<AtollPillaLst> _datos = new List<AtollPillaLst>( );
        private int _rowCt = 2;
        private int _datOsCt;
        #endregion

        public MainWindow( )
            {
            InitializeComponent( );
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
            _electricalTilt = _tiltValue;
            FamilyTextBox.Text = $"TWIN654";
            FccIdTextBox.Text = $"2AJQZ-AVS-X2015 ";
            GainTextBox.Text = $"15.8";
            HeightTextBox.Text = $"1298";
            _highFrequency = $"2400";
            LengthTextBox.Text = $"2.580";
            LobeTiltTextBox.Text = $"0";
            LowFrequencyTextBox.Text = $"696";
            _lowerFrequency = $"696";
            MaxFrequencyTextBox.Text = $"2400";
            MaxGainTextBox.Text = $"10.8";
            _maxPower = $"240";
            MeasFrequencyTextBox.Text = $"824";
            MinFrequencyTextBox.Text = $"696";
            PolarizationTextBox.Text = $"+45";
            _size = $"3";
            TimeTextBox.Text = $"3:33am";
            _upperFrequency = $"2400";
            WidthTextBox.Text = $"305";
            WeightTextBox.Text = $"305";
            WindAreaTextBox.Text = $".7";
            }
        private void MinimizeGuiCalls( )
            {
            _atvswr = AtvswrTextBox.Text;
            _azimuthDisplay = AzimuthDisplayTextBox.Text;
            _beamwidth = BeamwidthTextBox.Text;
            _comments = CommentsTextBox.Text;
            _date = DateTextBox.Text;
            _dateMesured = DateTextBox.Text;
            _depth = DepthTextBox.Text;
            _description = DescriptionTextBox.Text;
            _dimensions = DimensionsTextBox.Text;
            _electricalTilt = _tiltValue;
            _family = FamilyTextBox.Text;
            _fccId = FccIdTextBox.Text;
            _gain = GainTextBox.Text;
            _antHeight = HeightTextBox.Text;
            _highFrequency = HighFrequencyTextBox.Text;
            _length = LengthTextBox.Text;
            _lobeTilt = LobeTiltTextBox.Text;
            _lowFrequency = LowFrequencyTextBox.Text;
            _lowerFrequency = LowerFrequencyTextBox.Text;
            _maxFrequency = MaxFrequencyTextBox.Text;
            _maxGain = MaxGainTextBox.Text;
            _maxPower = MaxPowerTextBox.Text;
            _measFrequency = MeasFrequencyTextBox.Text;
            _mfrId = MfrIdTextBox.Text;
            _minGain = MinGainTextBox.Text;
            _minFrequency = MinFrequencyTextBox.Text;
            _polarization = PolarizationTextBox.Text;
            _size = SizeTextBox.Text;
            _time = TimeTextBox.Text;
            _upperFrequency = UpperFrequencyTextBox.Text;
            _antWidth = WidthTextBox.Text;
            _weight = WeightTextBox.Text;
            _windArea = WindAreaTextBox.Text;


            _aircomChecked = AircomCheckBox.IsChecked.GetValueOrDefault( );

            _atollChecked = AtollCheckBox.IsChecked.GetValueOrDefault( );

            _ce4Checked = Ce4CheckBox.IsChecked.GetValueOrDefault( );
            _ce4VwaChecked = Ce4VwaRadioButton.IsChecked.GetValueOrDefault( );
            _ce4TxtChecked = Ce4TxtRadioButton.IsChecked.GetValueOrDefault( );

            _celCadChecked = CelCadCheckBox.IsChecked.GetValueOrDefault( );
            _celPlanChecked = CelPlanCheckBox.IsChecked.GetValueOrDefault( );

            _geoPlanChecked = GeoplanCheckBox.IsChecked.GetValueOrDefault( );
            _geoPlanVwaChecked = GeoPlanVwaRadioButton.IsChecked.GetValueOrDefault( );
            _geoPlanTxtChecked = GeoPlanTxtRadioButton.IsChecked.GetValueOrDefault( );

            _granetChecked = GranetCheckBox.IsChecked.GetValueOrDefault( );
            _granetPatChecked = GranetPatRadioButton.IsChecked.GetValueOrDefault( );
            _granetTxtChecked = GranetTxtRadioButton.IsChecked.GetValueOrDefault( );

            _hodiaxChecked = HodiaxCheckBox.IsChecked.GetValueOrDefault( );
            _hodiaxHodChecked = HodiaxHodRadioButton.IsChecked.GetValueOrDefault( );
            _hodiaxTxtChecked = HodiaxTxtRadioButton.IsChecked.GetValueOrDefault( );

            _hydraChecked = HydraCheckBox.IsChecked.GetValueOrDefault( );
            _hydraAdfChecked = HydraAdfRadioButton.IsChecked.GetValueOrDefault( );
            _hydraTxtChecked = HydraTxtRadioButton.IsChecked.GetValueOrDefault( );

            _lccNetChecked = LccNetCheckBox.IsChecked.GetValueOrDefault( );
            _lccAntChecked = LccAntRadioButton.IsChecked.GetValueOrDefault( );
            _lccTxtChecked = LccTxtRadioButton.IsChecked.GetValueOrDefault( );

            _netPlanChecked = NetplanCheckBox.IsChecked.GetValueOrDefault( );

            _odysseyChecked = OdesseyCheckBox.IsChecked.GetValueOrDefault( );

            _pathLossChecked = PathLossCheckBox.IsChecked.GetValueOrDefault( );
            _pathLossAdfChecked = PathLossAdfRadioButton.IsChecked.GetValueOrDefault( );
            _pathLossTxtChecked = PathLossTxtRadioButton.IsChecked.GetValueOrDefault( );

            _wizardChecked = WizardCheckBox.IsChecked.GetValueOrDefault( );
            _wizardApfChecked = WizardApfRadioButton.IsChecked.GetValueOrDefault( );
            _wizardTxtChecked = WizardTxtRadioButton.IsChecked.GetValueOrDefault( );
            }
        #endregion

        #region Batch Operations 

        private void Single_File_Button_Click( object sender, RoutedEventArgs e )
            {
            #region single file

            ResultsLabel.Content = string.Empty;
            SaveResults.Text = string.Empty;

            var dlg = new Microsoft.Win32.OpenFileDialog( );

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".pln";
            dlg.Filter = "";

            bool? result = dlg.ShowDialog( );

            // Get the selected file name and display in a TextBox 
            if (result == true)
                {
                // Open document 
                var pathToFile = dlg.FileName;

                var fileName = string.Empty;
                _currentIngestedPlanetFileName = fileName = Path.GetFileName( pathToFile );

                #region File Name Disection
                //method #1
                _polRes = fileName.Split( '(', ')' )[1];

                //substring
                var trimmedFileName = fileName.Split( '(' )[0];
                string[] lines = Regex.Split( trimmedFileName, "-" );

                if (lines.Length == 5) _tiltValue = lines[1];


                var regEx = new Regex( $"T" + "[0-9]" + "[0-9]" );
                var tiltResult = regEx.Matches( lines[1] );
                if (tiltResult.Count == 1)
                    {
                    //todo possibly parse and use tile as counter / validation
                    //todo can be done with string Match though...
                    }
                #endregion

                //fileName
                var pathToCheck = $"{_ingestDir}{fileName}";


                if (File.Exists( pathToCheck ))
                    {
                    FileStream fileObj = new FileStream( pathToCheck, FileMode.Open, FileAccess.Read );
                    StreamReader readerObj = new StreamReader( fileObj );
                    var text = readerObj.ReadToEnd( );
                    readerObj.Close( );

                    _currentIngestedPlanetFile = text;
                    }

                try
                    {
                    File.Copy( pathToFile, _ingestDir += fileName );
                    FileStream fileObj = new FileStream( _ingestDir, FileMode.Open, FileAccess.Read );
                    StreamReader readerObj = new StreamReader( fileObj );
                    string text = readerObj.ReadToEnd( );
                    readerObj.Close( );
                    string readInfo = text;

                    _currentIngestedPlanetFile = readInfo;

                    }
                catch (FileNotFoundException db)
                    {
                    ResultsLabel.Content = $"An error occurred with file {fileName}";
                    DebugLabel.Content = db.Message;
                    }
                }

            #endregion
            }

        private void Src_Dir_Button_Click( object sender, RoutedEventArgs e )
            {
            try
                {
                using (var fbd = new FolderBrowserDialog( ))
                    {
                    MinimizeGuiCalls( );
                    var dirIsValid = fbd.ShowDialog( );
                    if (dirIsValid == System.Windows.Forms.DialogResult.OK &&
                        !string.IsNullOrWhiteSpace( fbd.SelectedPath ))
                        {
                        _appSrcPlnDir = fbd.SelectedPath;
                        }
                    }
                }
            catch (DirectoryNotFoundException db)
                {
                SaveResults.Text = $"Source directory not valid\n" +
                                   $"Please select a valid directory\n"
                                   + db.Message;
                }
            }
        private void Target_Dir_Button_Click( object sender, RoutedEventArgs e )
            {
            try
                {
                using (var fbd = new FolderBrowserDialog( ))
                    {
                    MinimizeGuiCalls( );
                    var targetIsValid = fbd.ShowDialog( );
                    if (targetIsValid == System.Windows.Forms.DialogResult.OK &&
                        !string.IsNullOrWhiteSpace( fbd.SelectedPath ))
                        {
                        _appTargetDir = fbd.SelectedPath + $"\\";

                        GetAllFilesInDirectory( _appSrcPlnDir );
                        }
                    }
                }
            catch (DirectoryNotFoundException db)
                {

                SaveResults.Text = $"Target directory not valid\n" +
                                   $"Please select a valid output directory" + db.Message;
                }
            }
        
        private void GetAllFilesInDirectory( string filePath )
            {

            _fileEntries = Directory.GetFiles( filePath );

            MinimizeGuiCalls( );

            foreach (var fileLocation in _fileEntries)
                {
                _currentIngestedPlanetFileName = string.Empty;
                _words = new[] { string.Empty };
                var fileName = _currentIngestedPlanetFileName = Path.GetFileName( fileLocation );

                //substring
                if (fileName != null)
                    {
                    _trimmedFileName = fileName.Split( '.' )[0];
                    _name = Regex.Split( _trimmedFileName, "-" );

                    if (_name.Length == 5)
                        {
                        //method #1
                        _polarization = fileName.Split( '(', ')' )[1];


                        _tiltValue = _name[1];
                        _trimmedTiltVal = _tiltValue.Split( 'T', '-' )[1];

                        }

                    //fileName
                    var pathToCheck = $"{_ingestDir}{fileName}";

                    if (File.Exists( pathToCheck ))
                        {
                        var fileObj = new FileStream( pathToCheck, FileMode.Open, FileAccess.Read );
                        var readerObj = new StreamReader( fileObj );
                        var text = readerObj.ReadToEnd( );
                        readerObj.Close( );

                        _currentIngestedPlanetFile = text;
                        }
                    try
                        {
                        if (!File.Exists( pathToCheck ))
                            {
                            File.Copy( fileLocation, _ingestDir + fileName );
                            var fileObj = new FileStream( _ingestDir + fileName, FileMode.Open,
                                FileAccess.Read );
                            var readerObj = new StreamReader( fileObj );
                            var text = readerObj.ReadToEnd( );
                            readerObj.Close( );
                            fileObj.Close( );

                            _currentIngestedPlanetFile = text;
                            }
                        }
                    catch (FileNotFoundException db)
                        {
                        SaveResults.Text = $"GetAllFiles encountered an error\n" +
                                           $"Please check the following\n" +
                                           $"Source directory is valid\n" +
                                           $"Target directory is valid\n" +
                                           $".pln files are valid format\n" + db.Message;
                        }
                    }
                try
                    {
                    if (_aircomChecked)
                        {
                        ConvertToAircomBatch( _tiltValue, _polarization );
                        DownloadAircom( );
                        }
                    if (_atollChecked)
                        {
                        ConvertToAtollBatch( );
                        }
                    if (_ce4Checked)
                        {
                        ConvertToCe4Batch( _polarization );
                        DownloadCe4Batch( );
                        }
                    if (_celCadChecked)
                        {
                        ConvertToCelCadBatchDownload( );
                        }
                    if (_celPlanChecked)
                        {
                        ConvertToCelPlanBatch( );
                        DownloadCelPlanBatch( );
                        }

                    if (_geoPlanChecked)
                        {
                        ConvertToGeoPlanBatch( );
                        DownloadGeoPlanBatch( );
                        }
                    if (_granetChecked)
                        {
                        ConvertToGranetBatch( );
                        DownloadGranetBatch( );
                        }
                    if (_hodiaxChecked)
                        {
                        ConvertToHodiaxBatch( );
                        DownloadHodiaxHorizontalBatch( );
                        DownloadHodiaxVerticalBatch( );
                        }
                    if (_hydraChecked)
                        {
                        ConvertToHydraBatch( );
                        DownloadHydra( );
                        }
                    if (_lccNetChecked)
                        {
                        ConvertToLccBatchTest( );
                        DownloadLccBatchTest( );
                        }
                    if (_netPlanChecked)
                        {
                        ConvertToNetPlanBatch( );
                        DownloadNetPlanBatch( );
                        }
                    if (_odysseyChecked)
                        {
                        ConvertToOdysseyBatch( );
                        DownloadOdysseyBatch( );
                        }
                    if (_pathLossChecked)
                        {
                        ConvertToPathLossBatch( );
                        DownloadPathLossBatch( );
                        }
                    if (_wizardChecked)
                        {
                        ConvertToWizardBatch( );
                        DownLoadWizardBatch( );
                        }
                    }
                catch (InvalidOperationException db)
                    {
                    SaveResults.Text = $"Error encountered with File Ingest || download\n" +
                                       $"Please try again || restart the application\n" +
                                       $"Please validate working directories are clean"
                                       + db.Message;
                    }
                }
            }
        
        private void ConvertToAircomBatch( string tiltValue, string polarization )
            {
            if (_aircomChecked)
                {
                try
                    {
                    string[] seperators = { "\r\n" };
                    var valOne = _currentIngestedPlanetFile;
                    var commaValue = valOne.Replace( "\t", "\r\n" );
                    _words = commaValue.Split( seperators, StringSplitOptions.RemoveEmptyEntries );

                    var totalWords = _words.Length;
                    var countOne = -1;
                    var countTwo = -1;

                    var saveStateOne = string.Empty;
                    var saveStateTwo = string.Empty;

                    if (totalWords <= 0)
                        {
                        SaveResults.Text = $"Aircom {totalWords} Empty\nCheck file source is correct";
                        }
                    var start = $"NAME\t" + _words[1] + "\r\n" +
                                "MAKE\t" + _words[3] + "\tAmphenol\r\n" +
                                "FREQUENCY\t" + _words[5] + "\r\n" +
                                "H_WIDTH " + _words[7] + "\r\n" +
                                "H_WIDTH " + _words[9] + "\r\n" +
                                "FRONT_TO_BACK " + _words[11] + "\r\n" +
                                "POLARIZATION\t" + polarization + "\r\n" +
                                "GAIN\t" + _maxGain + " dBi\r\n" +
                                "TILT\t" + tiltValue + "\tELECTRICAL\r\n" +
                                "COMMENTS\t" + _comments + "\r\n" +
                                "HORIZONTAL\t360";

                    for (int i = 21; i <= 740; i += 2)
                        {
                        countOne++;
                        var valTwo = _words[i];
                        var dblVal1 = Convert.ToDouble( valTwo );
                        saveStateOne += countOne.ToString( ) + "\t" + dblVal1.ToString( "0.0" ) + "\r\n";
                        }
                    for (var j = 743; j < totalWords; j += 2)
                        {
                        countTwo++;
                        var valThree = _words[j];
                        var dblVal2 = Convert.ToDouble( valThree );
                        saveStateTwo += countTwo.ToString( ) + "\t" + dblVal2.ToString( "0.0" ) + "\r\n";
                        }

                    _airComConversionResults = start + "\r\n" + saveStateOne + "VERTICAL\t360\r\n" + saveStateTwo;
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"Aircom Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private void DownloadAircom( )
            {
            try
                {
                if (_aircomChecked)
                    {
                    string[] seperators = { $"\r\n" };
                    var value = _airComConversionResults;
                    var commaValue = value.Replace( "\t", "\r\n" );
                    _words = commaValue.Split( seperators, StringSplitOptions.RemoveEmptyEntries );

                    var fileName = string.Empty;

                    fileName = _currentIngestedPlanetFileName;

                    fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                    var aircomDir = _appTargetDir + "Aircom";
                    if (!Directory.Exists( aircomDir ))

                        Directory.CreateDirectory( aircomDir );

                    if (!File.Exists( aircomDir + "\\" + fileName ))
                        {
                        using (var tempVar = File.Create( _words[1], 1024 ))
                            {
                            File.WriteAllText( $"{aircomDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
                }
            catch (Exception db)
                {
                SaveResults.Text = $"Aircom Download Encountered an error\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToCe4Batch( string polarization )
            {
            if (_ce4Checked)
                {
                try
                    {
                    string[] separators = { "\r\n" };

                    var value = _currentIngestedPlanetFile;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var totalWords = _words.Length;
                    var cont1 = -1;
                    var cont3 = -91;
                    var cont2 = -1;
                    var cont4 = -91;
                    var cont5 = -1;

                    var saveState = "";
                    var saveState2 = "";
                    var saveState3 = "";
                    var saveState4 = "";
                    var saveState5 = "";

                    if (totalWords <= 0)
                        {
                        SaveResults.Text = $"Ce4 {totalWords} Empty\nCheck file source is correct";
                        }

                    var dateMeasured = Convert.ToDateTime( _dateMesured );
                    var start = "|MANUF|Amphenol|\r\n" +
                                "|MODEL|" + _words[1] + "|\r\n" +
                                "|FILE|" + _words[1] + "|\r\n" +
                                "|DESCR|" + _description + "|\r\n" +
                                "|FCC ID|\r\n" +
                                "|REVERSE ID|\r\n" +
                                "|DATE|" + dateMeasured.ToShortDateString( ) + "|\r\n" +
                                "|MANUF ID|Amphenol|\r\n" +
                                "|FREQ|" + _minFrequency + "-" + _maxFrequency +
                                " MHz|\r\n" +
                                "|DBD/DBI Flag|dBd|\r\n" +
                                "|POLARIZATION|" + polarization + "|\r\n" +
                                "|HORIZ BEAM WIDTH|" + _words[7] + "|\r\n" +
                                "|VERT BEAM WIDTH|" + _words[9] + "|\r\n" +
                                "|HORIZ OFFSET|0|\r\n" +
                                "|HORIZ|0|360|";
                    //Horizontal
                    for (var i = 21; i <= 739; i += 2)
                        {
                        cont1++;
                        var newValue = _words[i];
                        var dblVal = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue );
                        saveState += "\t" + cont1.ToString( ) + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";
                        }

                    //Verticals 1
                    for (var i = 1283; i <= totalWords; i += 2)
                        {
                        cont3++;
                        var newValue = _words[i];
                        var dblVal = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                        saveState3 += "\t" + cont3 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                        }
                    for (var i = 743; i <= 923; i += 2)
                        {
                        cont2++;
                        var newValue = _words[i];
                        var dblVal = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                        saveState2 += "\t" + cont2 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                        }
                    //Verticals 2
                    Stack<string> stringQue = new Stack<string>( );
                    for (var i = 1105; i <= 1283; i += 2)
                        {
                        var newValue = _words[i];
                        var dblVal = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                        stringQue.Push( dblVal.ToString( "0.000" ) );

                        }
                    foreach (var strVal in stringQue)
                        {
                        cont4++;
                        saveState4 += "\t" + cont4 + "\t" + strVal + "\t" + "\r\n";
                        }
                    Stack<string> stringQue2 = new Stack<string>( );
                    for (var i = 923; i < 1105; i += 2)
                        {
                        var newValue = _words[i];
                        var valor = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                        stringQue2.Push( valor.ToString( "0.000" ) );

                        }
                    foreach (var strVal in stringQue2)
                        {
                        cont5++;
                        saveState5 += "\t" + cont5 + "\t" + strVal + "\t" + "\r\n";
                        }

                    _ce4ConversionResults =
                        start + "\r\n" + saveState + "|VERT|0|181| " + "\r\n" + saveState3 + saveState2 +
                        "|VERT|180|181|\r\n" + saveState4 + saveState5;
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"Ce4 Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private void DownloadCe4Batch( )
            {
            try
                {

                if (_ce4Checked)
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;

                    string[] separators = { "\r\n" };
                    string value = _ce4ConversionResults;
                    string commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var Ce4Dir = _appTargetDir + $"Ce4";

                    //.VWA
                    if (_ce4VwaChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".vwa";

                        if (!Directory.Exists( Ce4Dir )) Directory.CreateDirectory( Ce4Dir );

                        if (!File.Exists( Ce4Dir + fileName ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{Ce4Dir}\\{fileName}", value );
                                tempVar.Close( );
                                }

                            }
                        }

                    //.txt
                    if (_ce4TxtChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".txt";

                        if (!Directory.Exists( Ce4Dir )) Directory.CreateDirectory( Ce4Dir );

                        if (!File.Exists( Ce4Dir ))
                            {
                            if (!File.Exists( Ce4Dir + fileName ))
                                {
                                using (var tempVar = File.Create( fileName, 1024 ))
                                    {
                                    File.WriteAllText( $"{Ce4Dir}\\{fileName}", value );
                                    tempVar.Close( );
                                    }

                                }
                            }
                        }
                    }
                }

            catch (Exception db)
                {
                SaveResults.Text = $"Ce4 Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToCelCadBatchDownload( )
            {
            if (_celCadChecked)
                {
                try
                    {
                    string[] separators = { "\r\n" };
                    var value = _currentIngestedPlanetFile;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var totalWords = _words.Length;
                    var cont1 = -1;
                    var cont2 = 181;
                    var cont3 = 1;

                    var saveState = "";
                    var saveState2 = "";
                    var saveState3 = "";

                    var start = "LCC\r\n" +
                                _date + "\r\n" +
                                _time + "\r\n" +
                                "Amphenol 815-399-0001\r\n" +
                                _words[1] + "\r\n";

                    saveState = CelCadReturnPattern( cont1, _words, 21, 739, true, -1, -1 );
                    saveState2 = CelCadReturnPattern( cont3, _words, 743, 1101, false, 1, 1 );
                    saveState3 = CelCadReturnPattern( cont2, _words, 1103, totalWords, false, 1, 1 );

                    _celCadHorizontalConversionResults =
                        start + _words[7] + "\r\n" + "H\r\n0.00\r\n" + saveState + "*";

                    var celCadVertFileName = "";
                    var celCadHorzFileName = celCadVertFileName = _words[1];

                    var celCadHorizontalDir = _appTargetDir + "CelCad_Horizontal";

                    if (!Directory.Exists( celCadHorizontalDir ))
                        Directory.CreateDirectory( celCadHorizontalDir );

                    if (Directory.Exists( celCadHorizontalDir ))
                        {
                        if (!File.Exists( celCadHorizontalDir + "\\" + celCadHorzFileName ))
                            {
                            using (var tempCreate = File.Create( celCadHorzFileName, 1024 ))
                                {
                                File.WriteAllText( $"{celCadHorizontalDir}\\{celCadHorzFileName}",
                                    _celCadHorizontalConversionResults );
                                tempCreate.Close( );
                                }
                            }
                        }

                    _celCadVerticalConversionResults = start + _words[9] + "\r\n" + "V\r\n0.00\r\n" + saveState3 +
                                                       saveState2 + "*";

                    var celCadVerticalDir = _appTargetDir + "CelCad_Vertical";

                    if (!Directory.Exists( celCadVerticalDir ))
                        Directory.CreateDirectory( celCadVerticalDir );

                    if (Directory.Exists( celCadVerticalDir ))
                        {
                        if (!File.Exists( celCadVerticalDir + "\\" + celCadVertFileName ))
                            {
                            using (var tempCreate = File.Create( celCadVertFileName, 1024 ))
                                {
                                File.WriteAllText( $"{celCadVerticalDir}\\{celCadVertFileName}",
                                    _celCadVerticalConversionResults );
                                tempCreate.Close( );
                                }
                            }
                        }
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"CellCad Conversion Format || Download Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private string CelCadReturnPattern( int cont, string[] words, int start, int fin, bool increment, int negpos, int negpos2 )
            {

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
                    var maxGainParsed = (Convert.ToDouble( _maxGain ) - (Convert.ToDouble( newvalue ))) * negpos;
                    _celCadReturnPatternSaveState += cont + "\t" +
                                                     (Convert.ToDouble( maxGainParsed ) * negpos2).ToString( "0.0" ) +
                                                     "\r\n";
                    }
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"CellCad Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return _celCadReturnPatternSaveState;
            }

        private void ConvertToCelPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -1;
                var cont2 = -1;
                var saveState = "";
                var saveState2 = "";

                var start = "[CelPlan DT ANT]\r\n" +
                            "Mod:\t" + _words[1] + "\r\n" +
                            "Man:\tAmphenol\r\n" +
                            "Dig:\t" + " " + "\r\n" +
                            "Dsc:\t" + _description + "\r\n" +
                            "Ngn:\t" + _maxGain + " dBd\r\n" +
                            "Hbw:\t" + _words[7] + "°\r\n" +
                            "Vbw:\t" + _words[9] + "°\r\n" +
                            "Mnf:\t" + _minFrequency + " MHz\r\n" +
                            "Mxf:\t" + _maxFrequency + " MHz\r\n" +
                            "Sze:\t" + _size + " m\r\n" +
                            "Inc:\t1°\r\n" +
                            "Han:\tHgn";

                for (var i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    var newValue = _words[i];
                    var valor = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                    saveState += cont1.ToString( ) + "\t" + valor.ToString( "0.0" ) + "\r\n";
                    }
                for (var i = 743; i < totalWords; i += 2)
                    {
                    cont2++;
                    var newValue = _words[i];
                    var dblVal = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue ));
                    saveState2 += cont2.ToString( ) + "\t" + dblVal.ToString( "0.0" ) + "\r\n";
                    }
                _celPlanConversionResults = start + "\r\n" + saveState + "Van\tVgn\r\n" + saveState2;
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"CelPlan Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        private void DownloadCelPlanBatch( )
            {
            try
                {
                if (_celPlanChecked)
                    {
                    string[] separators = { "\r\n" };
                    string value = _celPlanConversionResults;
                    string commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var fileName = string.Empty;

                    fileName = _currentIngestedPlanetFileName;

                    fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".txt";

                    var celPlanDir = _appTargetDir + "CelPlan";
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
                }
            catch (Exception db)
                {
                SaveResults.Text = $"CelPlan Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToGeoPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -1;
                var cont2 = 0;
                var cont3 = -180;

                var saveState = "";
                var saveState2 = "";
                var dateMeasured = Convert.ToDateTime( _date );

                var start = "VERIZON WIRELESS RFTOOLS ANTENNA" + "\r\n" +
                            "V5 \r\n" +
                            "model_name:" + "\t" + _words[1] + "\r\n" +
                            "manufacturer:" + "\t" + _words[3] + "\r\n" +
                            "description:" + "\t" + _description + "\r\n" +
                            "antenna_type:" + "\t" + _family + "\r\n" +
                            "polarization:" + "\t" + _polarization + "\r\n" +
                            "azimuth_display_offset_deg:" + "\t" + _azimuthDisplay + "\r\n" +
                            "date_measured:" + "\t" + dateMeasured.ToString( "dd-MMM-yy" ) + "\r\n" +
                            "freq_measured_mhz:" + "\t" + _words[5] + "\r\n" +
                            "lower_freq_mhz:" + "\t" + _lowerFrequency + "\r\n" +
                            "upper_freq_mhz:" + "\t" + _upperFrequency + "\r\n" +
                            "electrical_tilt:" + "\t" + _tiltValue + "\r\n" +
                            "height_m:" + "\t" + _antHeight + "\r\n" +
                            "width_m:" + "\t" + _antWidth + "\r\n" +
                            "depth_m:" + "\t" + _depth + "\r\n" +
                            "weight_kg:" + "\t" + _weight + "\r\n" +
                            "HORIZONTAL_GAINS";

                for (var i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    var newValue = _words[i];
                    var strValue = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue );
                    saveState += cont1.ToString( "0.0" ) + "\t" + strValue.ToString( "0.0" ) + "\r\n";
                    }
                Stack<string> strStack = new Stack<string>( );
                for (var i = 743; i < 1103; i += 2)
                    {
                    var newValue = _words[i];
                    var strValue = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue );
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
                    string newvalue = _words[i];
                    double valor = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue );
                    strStack2.Push( valor.ToString( "0.0" ) );

                    }
                foreach (string valor in strStack2)
                    {
                    cont2++;
                    saveState3 += cont2.ToString( "0.0" ) + "\t" + valor + "\r\n";
                    }
                _geoPlanConversionResults = start + "\r\n" + saveState + "END" + "\r\n" + "VERTICAL_GAINS" +
                                            "\r\n" + saveState2 + saveState3 + "END";
                //txtGuardar.Text = guardar3;
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"GeoPlan Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        private void DownloadGeoPlanBatch( )
            {
            try
                {
                if (_geoPlanChecked)
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;


                    string[] separators = { "\r\n" };
                    var value = _geoPlanConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );


                    var geoPlanDir = _appTargetDir + $"GeoPlan";

                    //.VWA
                    if (_geoPlanVwaChecked)
                        {
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

                    //.txt
                    if (_geoPlanTxtChecked)
                        {
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
                    }
                }
            catch (Exception db)
                {
                SaveResults.Text = $"GepPlan Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToGranetBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -181;
                var cont2 = -1;
                var cont3 = 181;
                var cont4 = 1;
                var saveState1 = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var dateMeasured = Convert.ToDateTime( _date );

                var start = "model_number\t" + _words[1] + "\r\n" +
                            "manufacturer\tAmphenol\r\n" +
                            "gain\t" + _maxGain + " dBd\r\n" +
                            "h_beamwidth\t" + _words[7] + " degrees\r\n" +
                            "v_beamwidth\t" + _words[9] + " degrees\r\n" +
                            "front_to_back\t" + _words[11] + " dB\r\n" +
                            "length\t" + _length + " meters\r\n" +
                            "lobe_tilt\t" + _lobeTilt + " degrees\r\n" +
                            "wind_area\t" + _windArea + " square meters\r\n" +
                            "source\t0\r\n" +
                            "date\t" + dateMeasured.ToShortDateString( ) + "\r\n" +
                            "meas-freq\t" + _measFrequency + " MHz\r\n" +
                            "description\t" + _description + "\r\n" +
                            "polarization\t" + _polarization + "\r\n" +
                            "Sectored\r\n\r\n" +

                            "horizontal\r\n" +
                            "unequal unsymmetrical\r\n";
                saveState1 = GranetHreturnPattern( cont1, _words, 381, 739 );
                saveState2 = GranetHreturnPattern( cont2, _words, 21, 379 );
                saveState3 = GranetVreturnPattern( cont3, _words, 1103, totalWords - 1 );
                saveState4 = GranetVreturnPattern( cont4, _words, 743, 1101 );
                _granetConversionResults = start + saveState1 + saveState2 +
                                           "\r\nvertical\r\nunequal unsymmetrical\r\n" + saveState3 + saveState4;
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Granet Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        private string GranetHreturnPattern( int cont, string[] words, int startIndx, int fin )
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
                SaveResults.Text = $"Gran Horizontal Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return saveState;
            }
        private string GranetVreturnPattern( int cont, string[] words, int startIndx, int fin )
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
                SaveResults.Text = $"Gran Vertical Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return saveState;
            }
        private void DownloadGranetBatch( )
            {
            try
                {
                if (_granetChecked)
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;

                    string[] separators = { "\r\n" };
                    var value = _granetConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var granetDir = _appTargetDir + $"Granet";

                    //.VWA
                    if (_granetPatChecked)
                        {
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

                    //.txt
                    if (_granetTxtChecked)
                        {
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
                    }

                }
            catch (Exception db)
                {
                SaveResults.Text = $"Granet Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToHodiaxBatch( )
            {
            if (_hodiaxChecked)
                {
                try
                    {
                    string[] separators = { "\r\n" };
                    var value = _currentIngestedPlanetFile;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var cont1 = -1;
                    var totalWords = _words.Length;
                    var saveState = "";
                    var saveState2 = "";
                    var saveState3 = "";
                    var saveState4 = "";

                    var start = "ANTENNA-FILE \r\n" +
                                "IA" + "\t" + "Hodiax v2.0 \r\n" +
                                "IB" + "\t" + "ANTENNA-FILE \r\n" +
                                "HA" + "\t" + _words[1] + "\r\n" +
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
                        var newValue = _words[i];
                        saveState += "HK" + "\t" + "H" + "\t" + cont1.ToString( ) + "\t" +
                                     (Convert.ToDouble( newValue ) * -1).ToString( ) + "\r\n";
                        }
                    cont1 = -1;
                    for (var i = 21; i <= 739; i += 2)
                        {
                        cont1++;
                        var newValue = _words[i];
                        saveState2 += "HK\tV\t" + cont1.ToString( ) + "\t" +
                                      (Convert.ToDouble( newValue ) * -1).ToString( ) + "\r\n";
                        }
                    cont1 = -1;
                    for (var i = 743; i <= totalWords; i += 2)
                        {
                        cont1++;
                        var newValue = _words[i];
                        saveState3 += "HK\t H\t" + cont1.ToString( ) + "\t" +
                                      (Convert.ToDouble( newValue ) * -1).ToString( ) + "\r\n";
                        }
                    cont1 = -1;
                    for (var i = 743; i <= totalWords; i += 2)
                        {
                        cont1++;
                        var newValue = _words[i];
                        saveState4 += "HK\t V\t" + cont1.ToString( ) + "\t" +
                                      (Convert.ToDouble( newValue ) * -1).ToString( ) + "\r\n";
                        }


                    _hodiaxHorizontalConversionResults = start + "\r\n" + saveState3 + saveState4 + final;
                    _hodiaxVerticalConversionResults = start + "\r\n" + saveState + saveState2 + final;
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"Hodiax Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private void DownloadHodiaxHorizontalBatch( )
            {
            if (_hodiaxChecked)
                {
                try
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;
                    var h = $"H-";
                    string[] separators = { "\r\n" };
                    var value = _hodiaxHorizontalConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var hodiaxHorizDir = _appTargetDir + $"Hodiax_Horizontal";


                    if (!Directory.Exists( hodiaxHorizDir )) Directory.CreateDirectory( hodiaxHorizDir );

                    if (_hodiaxTxtChecked)
                        {
                        fileName = fileName.Substring( 0, fileName.LastIndexOf(
                                       ".", StringComparison.Ordinal ) ) + ".txt";

                        if (!File.Exists( $"{hodiaxHorizDir}\\{h}{fileName}" ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{hodiaxHorizDir}\\{h}{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }
                    if (_hodiaxHodChecked)
                        {
                        fileName = fileName.Substring( 0, fileName.LastIndexOf(
                                       ".", StringComparison.Ordinal ) ) + ".hod";

                        if (!File.Exists( $"{hodiaxHorizDir}\\{h}{fileName}" ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{hodiaxHorizDir}\\{h}{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"Hodiax Horizontal Return Pattern Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private void DownloadHodiaxVerticalBatch( )
            {
            if (_hodiaxChecked)
                {
                try
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;
                    var e = $"E-";
                    string[] separators = { "\r\n" };
                    var value = _hodiaxVerticalConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var hodiaxVertDir = _appTargetDir + $"Hodiax_Vertical";
                    if (!Directory.Exists( hodiaxVertDir )) Directory.CreateDirectory( hodiaxVertDir );

                    if (_hodiaxTxtChecked)
                        {
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
                        }

                    if (_hodiaxHodChecked)
                        {
                        fileName = fileName.Substring( 0, fileName.LastIndexOf(
                                       ".", StringComparison.Ordinal ) ) + ".hod";

                        if (!File.Exists( $"{hodiaxVertDir}\\{e}{fileName}" ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{hodiaxVertDir}\\{e}{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }
                    }
                catch (Exception db)
                    {
                    SaveResults.Text = $"Hodiax Download Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }

        public string HydraHreturnPattern( int cont, string[] words, int start, int fin )
            {

            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newvalue = words[i];
                    var value = (Convert.ToDouble( newvalue )) * -1;
                    saveState += cont.ToString( ) + "," + value.ToString( "0.0" ) + "\r\n";
                    }

                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Hydra Horizontal Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
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
                    var newValue = words[i];
                    var value = (Convert.ToDouble( newValue )) * -1;
                    strStack.Push( value.ToString( "0.0" ) );

                    }
                foreach (var value in strStack)
                    {
                    cont++;
                    saveState += cont.ToString( ) + "," + value + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Hydra Vertical Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }

            return saveState;
            }
        protected void ConvertToHydraBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -181;
                var cont2 = -1;
                var cont3 = -181;
                var cont4 = 0;
                var saveState1 = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var dateMeasured = Convert.ToDateTime( _date );

                var start = "REVNUM:,TIA/EIA IS-804-0\r\n" +
                            "REVDAT:,20010109\r\n" +
                            "ANTMAN:,Amphenol\r\n" +
                            "MODNUM:," + _words[1] + "\r\n" +
                            "FILNUM:," + dateMeasured.ToShortDateString( ) + "\r\n" +
                            "DESCR1:," + _description + "\r\n" +
                            "DTDATA:," + "20030821" + "\r\n" +
                            "LOWFRQ:," + _lowerFrequency + "\r\n" +
                            "HGHFRQ:," + _highFrequency + "\r\n" +
                            "GUNITS:,DBD/DBR\r\n" +
                            "LWGAIN:," + _maxGain + "\r\n" +
                            "MDGAIN:," + _maxGain + "\r\n" +
                            "HGGAIN:," + _maxGain + "\r\n" +
                            "AZWIDT:," + _words[7] + "\r\n" +
                            "ELWIDT:," + _words[9] + "\r\n" +
                            "CONTYP:," + "EDIN" + "\r\n" +
                            "ATVSWR:," + "1.5" + "\r\n" +
                            "ELTILT:,0\r\n" +
                            "MAXPOW:," + _maxPower + "\r\n" +
                            "ANTLEN:," + _length + "\r\n" +
                            "ANTWID:," + _antWidth + "\r\n" +
                            "ANTDEP:," + _depth + "\r\n" +
                            "FIELD3:,\r\n" +
                            "PATTYP:," + "Typical" + "\r\n" +
                            "NOFREQ:,1\r\n" +
                            "PATFRE:," + _words[4] + "\r\n" +
                            "NUMCUT:," + "2" + "\r\n" +
                            "PATCUT:,H\r\n" +
                            "POLARI:," + _polarization + "\r\n" +
                            "NUPOIN:,360\r\n" +
                            "FSTLST:,-180,179\r\n";

                var centerPoint = "PATCUT:,V\r\n" +
                                  "POLARI:," + _polarization + "\r\n" +
                                  "NUPOIN:,360\r\n" +
                                  "FSTLST:,-180,179\r\n";
                saveState1 = HydraHreturnPattern( cont1, _words, 381, 739 );
                saveState2 = HydraHreturnPattern( cont2, _words, 21, 379 );
                saveState3 = HydraVreturnPattern( cont3, _words, 743, 1105 );
                saveState4 = HydraVreturnPattern( cont4, _words, 1105, totalWords );
                _hydraConversionResults = start + saveState1 + saveState2 + centerPoint + saveState3 + saveState4 +
                                          "ENDFIL,EOF";
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Hydra Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        protected void DownloadHydra( )
            {
            if (_hydraChecked)
                {
                try
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;

                    string[] separators = { "\r\n" };
                    var value = _hydraConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var hydraDir = _appTargetDir + $"Hydra";

                    if (!Directory.Exists( hydraDir )) Directory.CreateDirectory( hydraDir );

                    if (_hydraTxtChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".txt";

                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{hydraDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    if (_hydraAdfChecked)
                        {
                        fileName =
                               fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                               + ".adf";

                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{hydraDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
                catch (Exception db)
                    {
                    SaveResults.Text = $"Hydra Download Exception\n" +
                                       $"Please Check Directory is valid\n" +
                                       $"Please Ensure You have Write Access"
                                       + db.Message;
                    }
                }


            }

        public string LccHreturnPattern( int cont, string[] words, int start, int fin )
            {
            var saveState = "";
            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newValue = words[i];
                    var value = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue );
                    saveState += "AE\tH\t" + cont.ToString( "0.0" ) + "\t" + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"LCC Horizontal Return Pattern Format Exception\n" +
                                         $"Please check .pln is valid\n" +
                                         $"Please check values in 'Textboxes' are correct\n"
                                         + db.Message;
                }
            return saveState;
            }
        public string LccVreturnPattern( int cont, string[] words, int start, int fin )
            {

            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont--;
                    var newValue = words[i];
                    var value = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newValue );
                    saveState += "AE\tV\t" + cont.ToString( "0.0" ) + "\t" + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"LCC Vertical Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return saveState;
            }
        protected void ConvertToLccBatchTest( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -1;
                // var cont2 = -1;
                var cont3 = 181;
                var cont4 = 1;
                var saveState1 = "";
                var saveState3 = "";
                var saveState4 = "";
                var start = "AA  " + _words[1] + "\r\n" +
                            "AB\r\n" +
                            "AC  S  " + _words[7] + " " + _maxGain + " 0\r\n" +
                            "AD  " + _length + " 0 Amphenol " + _words[1] + "\r\n";
                var fin = "AF  " + _minFrequency + "-" + _minFrequency + " MHz\r\n" +
                            "AG  50 Ohms\r\n" +
                            "AH  <=1.5:1\r\n" +
                            "AI  0\r\n" +
                            "AJ  19\r\n" +
                            "AK  500 W\r\n" +
                            "AL  NE or EDIN\r\n" +
                            "AM  " + _words[11] + "\r\n" +
                            "AN  29.1 lbs\r\n" +
                            "AO\r\n" +
                            "AP\r\n" +
                            "AQ\r\n" +
                            "AR";
                saveState1 = LccHreturnPattern( cont1, _words, 21, 739 );
                saveState3 = LccVreturnPattern( cont3, _words, 1103, totalWords - 1 );
                saveState4 = LccVreturnPattern( cont4, _words, 743, 1101 );
                _lccConversionResults = start + saveState1 + saveState3 + saveState4 + fin;
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"LCC Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        protected void DownloadLccBatchTest( )
            {
            if (_lccNetChecked)
                {
                try
                    {
                    var fileName = string.Empty;
                    fileName = _currentIngestedPlanetFileName;

                    string[] separators = { "\r\n" };
                    var value = _lccConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var lccNetDir = _appTargetDir + $"Lcc_Net";
                    if (!Directory.Exists( lccNetDir )) Directory.CreateDirectory( lccNetDir );

                    //.ant
                    if (_lccAntChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".ant";

                        if (!File.Exists( lccNetDir + fileName ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{lccNetDir}\\{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }

                    //.txt
                    if (_lccTxtChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".txt";

                        if (!File.Exists( lccNetDir ))
                            {
                            if (!File.Exists( lccNetDir + fileName ))
                                {
                                using (var tempVar = File.Create( fileName, 1024 ))
                                    {
                                    File.WriteAllText( $"{lccNetDir}\\{fileName}", value );
                                    tempVar.Close( );
                                    }
                                }
                            }
                        }
                    }
                catch (Exception db)
                    {
                    SaveResults.Text = $"LCC Download Exception\n" +
                                       $"Please Check Directory is valid\n" +
                                       $"Please Ensure You have Write Access"
                                       + db.Message;
                    }
                }
            }

        private string NetPlanHreturnPattern( string[] words, int start, int fin )
            {

            var saveState = "";
            var strStack = new Stack<string>( );
            var aryLst = new ArrayList( );

            try
                {
                for (var i = start; i < fin; i += 2)
                    {
                    var newValue = words[i];
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
                SaveResults.Text = $"NetPlan Horizontal Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return saveState;
            }
        private string NetPlanVreturnPattern( string[] words, int start, int fin )
            {

            var saveState = "";
            var strStack = new Stack<string>( );
            var aryLst = new ArrayList( );
            aryLst.Add( "" );
            try
                {
                for (var i = start; i < fin; i += 2)
                    {
                    var newValue = words[i];
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
                SaveResults.Text = $"NetPlan Vertical Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            return saveState;
            }
        private void ConvertToNetPlanBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };
                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                // var saveState1 = "";
                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";

                var start = "Name = " + _words[1] + "\r\n" +
                            "Number of Points = 360\r\n" +
                            "Start Ang.= 0\r\n" +
                            "Range = " + _words[7] + "\r\n" +
                            "Increment = 1\r\n" +
                            "Orientation = Horizontal";
                var start2 = "Name = " + _words[1] + "\r\n" +
                             "Number of Points = 360\r\n" +
                             "Start Ang. = -180\r\n" +
                             "Range = " + _words[9] + "\r\n" +
                             "Increment = 1\r\n" +
                             "Orientation = Vertical";
                var aryLst = new ArrayList( );
                var strStack = new Stack<string>( );

                for (var i = 23; i <= 739; i += 2)
                    {
                    var newvalue = _words[i];
                    strStack.Push( (Convert.ToDouble( newvalue ) * -1).ToString( "0.0" ) );
                    }
                for (var i = 21; i <= 21; i += 2)
                    {
                    var newvalue = _words[i];
                    strStack.Push( (Convert.ToDouble( newvalue ) * -1).ToString( "0.0" ) );
                    //aryLst.Add(saveState);
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
                    var newValue = _words[i];
                    saveState2 += (Convert.ToDouble( newValue ) * -1).ToString( "0.0" ) + "\t";
                    aryLst.Add( saveState );
                    if (aryLst.Count == 10)
                        {
                        saveState2 += "\r\n";
                        aryLst.Clear( );
                        }
                    }
                saveState2 = NetPlanHreturnPattern( _words, 743, 1105 );
                saveState3 = NetPlanVreturnPattern( _words, 1105, totalWords );
                _netPlanVerticalConversionResults = start2 + "\r\n" + saveState2 + saveState3;
                _netPlanHorizontalConversionResults = start + "\r\n" + saveState;
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"NetPlan Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        private void DownloadNetPlanBatch( )
            {
            if (_netPlanChecked)
                {
                var fileName = string.Empty;
                try
                    {
                    fileName = _currentIngestedPlanetFileName;
                    var hFileName = "";
                    var vFileName = "";

                    var hValue = _netPlanHorizontalConversionResults;
                    var vValue = _netPlanVerticalConversionResults;

                    string[] hSeparators = { "\r\n" };
                    string[] vSeparators = { "\r\n" };

                    var vCommavalue = vValue.Replace( "\t", "\r\n" );
                    var hCommavalue = hValue.Replace( "\t", "\r\n" );

                    string[] vWords = vCommavalue.Split( vSeparators, StringSplitOptions.RemoveEmptyEntries );
                    string[] hWords = hCommavalue.Split( hSeparators, StringSplitOptions.RemoveEmptyEntries );

                    var netPlanHdir = _appTargetDir + $"NetPlan_Horizontal";
                    var netPlanVdir = _appTargetDir + $"NetPlan_Vertical";

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
                            }
                        }
                    }
                catch (Exception db)
                    {
                    SaveResults.Text = $"NetPlan Download Exception\n" +
                                       $"Please Check Directory is valid\n" +
                                       $"Please Ensure You have Write Access"
                                       + db.Message;
                    }
                }
            }

        private void ConvertToOdysseyBatch( )
            {
            if (_odysseyChecked)
                {
                try
                    {
                    string[] separators = { "\r\n" };
                    var value = _currentIngestedPlanetFile;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var cont1 = -1;
                    var cont2 = -1;
                    var totalWords = _words.Length;
                    var saveState = "";
                    var saveState2 = "";
                    var start = "NAME\t" + _words[1] + "\r\n" +
                                "FREQUENCY\t" + _words[5] + "\r\n" +
                                "BEAM_WIDTH\t" + _words[7] + "\r\n" +
                                "GAIN\t" + _gain + "\r\n" +
                                "TILT\t" + _words[15] + "\r\n" +
                                "CLASS\t" + _family + "\r\n" +
                                "FREQUENCY_BAND" + "\t" + _lowerFrequency + " - " + _highFrequency + "\r\n" +
                                "ELECTRICAL_TILT\t" + _electricalTilt + "\r\n" +
                                "HORIZONTAL	360";

                    for (var i = 21; i <= 740; i += 2)
                        {
                        cont1++;
                        var newValue = _words[i];
                        saveState += cont1.ToString( ) + "\t" + (Convert.ToDouble( newValue )).ToString( "0.0" ) + "\r\n";
                        }
                    for (var i = 743; i <= totalWords - 1; i += 2)
                        {
                        cont2++;
                        var newvalue = _words[i];
                        saveState2 += cont2.ToString( ) + "\t" + (Convert.ToDouble( newvalue )).ToString( "0.0" ) + "\r\n";
                        }

                    _odysseyConversionResults = start + "\r\n" + saveState + "VERTICAL\t360\r\n" + saveState2;

                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"Odyssey Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        private void DownloadOdysseyBatch( )
            {
            var fileName = _currentIngestedPlanetFileName;

            try
                {
                string[] separators = { "\r\n" };
                var value = _odysseyConversionResults;
                var commaValue = value.Replace( "\t", "\r\n" );
                string[] words = commaValue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var odysseyDir = _appTargetDir + $"Odyssey";

                if (!Directory.Exists( odysseyDir )) Directory.CreateDirectory( odysseyDir );
                fileName =
                    fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                    + ".txt";

                if (!File.Exists( odysseyDir + fileName ))
                    using (var tempVar = File.Create( fileName, 1024 ))
                        {
                        File.WriteAllText( $"{odysseyDir}\\{fileName}", value );
                        tempVar.Close( );
                        }
                }
            catch (Exception db)
                {
                SaveResults.Text = $"Odyssey Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        public string PathLossReturnPattern( int cont, string[] words, int start, int fin )
            {

            var saveState = "";

            try
                {
                for (var i = start; i <= fin; i += 2)
                    {
                    cont++;
                    var newValue = words[i];
                    var value = (Convert.ToDouble( newValue )) * -1;
                    saveState += cont.ToString( "0.0" ) + "," + value.ToString( "0.0" ) + "\r\n";
                    }
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"PathLoss Return Pattern Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }

            return saveState;
            }
        protected void ConvertToPathLossBatch( )
            {
            if (_pathLossChecked)
                {
                try
                    {
                    string[] separators = { "\r\n" };

                    var value = _currentIngestedPlanetFile;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                    var totalWords = _words.Length;
                    var cont1 = -181;
                    var cont2 = -1;
                    var cont3 = -181;
                    var cont4 = -1;
                    var saveState1 = "";
                    var saveState2 = "";
                    var saveState3 = "";
                    var saveState4 = "";
                    var dateMeasured = Convert.ToDateTime( _date );

                    var start = "REVNUM:,NSMA WG16.99.050\r\n" +
                                "REVDAT:,19980706\r\n" +
                                "COMNT1:,\r\n" +
                                "COMNT2:,\r\n" +
                                "ANTMAN:,Amphenol\r\n" +
                                "MODNUM:," + _words[1] + "\r\n" +
                                "DESCR1:," + _description + "\r\n" +
                                "DTDATA:," + dateMeasured.ToString( "MM/dd/yyyy" ) + "\r\n" +
                                "LOWFRQ:," + _lowFrequency + "\r\n" +
                                "HGHFRQ:," + _highFrequency + "\r\n" +
                                "GUNITS:,DBI/DBR\r\n" +
                                "LWGAIN:," + _maxGain + "\r\n" +
                                "MDGAIN:," + _maxGain + "\r\n" +
                                "HGGAIN:," + _maxGain + "\r\n" +
                                "AZWIDT:," + _words[7] + "\r\n" +
                                "ELWIDT:," + _words[9] + "\r\n" +
                                "CONTYP:,EDIN\r\n" +
                                "ATVSWR:" + _atvswr + "\r\n" +
                                "FRTOBA:,32.0\r\n" +
                                "ELTILT:,0\r\n" +
                                "RADCTR:,\r\n" +
                                "POTOPO:,\r\n" +
                                "MAXPOW:," + _maxPower + "\r\n" +
                                "ANTLEN:," + _length + "\r\n" +
                                "ANTWID:," + _antWidth + "\r\n" +
                                "ANTDEP:," + _depth + "\r\n" +
                                "ANTWGT:," + _antWidth + "\r\n" +
                                "FIELD1:,\r\n" +
                                "FIELD2:,\r\n" +
                                "FIELD3:,\r\n" +
                                "FIELD4:,\r\n" +
                                "FIELD5:,\r\n" +
                                "PATTYP:,Typical\r\n" +
                                "NOFREQ:,1\r\n" +
                                "PATFRE:," + _words[4] + "\r\n" +
                                "NUMCUT:,2\r\n" +
                                "PATCUT:,AZ\r\n" +
                                "POLARI:,SLR-SLL\r\n" +
                                "NUPOIN:,361\r\n" +
                                "FSTLST:,-180,180\r\n";
                    var centerPoint = "PATCUT:,EL\r\n" +
                                 "POLARI:,SLR-SLL\r\n" +
                                 "NUPOIN:,361\r\n" +
                                 "FSTLST:,-180,180\r\n";
                    saveState1 = PathLossReturnPattern( cont1, _words, 381, 739 );
                    saveState2 = PathLossReturnPattern( cont2, _words, 21, 381 );
                    saveState3 = PathLossReturnPattern( cont3, _words, 1103, totalWords );   //743-1105
                    saveState4 = PathLossReturnPattern( cont4, _words, 743, 1103 );    //1105-ta
                    _pathLossConversionResults = start + saveState1 + saveState2 + centerPoint + saveState3 + saveState4 + "ENDFIL,EOF";

                    // _pathLossTestVarSaveSate = saveState4; //CenterPoint V
                    }
                catch (FormatException db)
                    {
                    SaveResults.Text = $"PathLoss Conversion Format Exception\n" +
                                       $"Please check .pln is valid\n" +
                                       $"Please check values in 'Textboxes' are correct\n"
                                       + db.Message;
                    }
                }
            }
        protected void DownloadPathLossBatch( )
            {

            var fileName = _currentIngestedPlanetFileName;

            try
                {
                string[] separators = { "\r\n" };
                var value = _pathLossConversionResults;
                var commavalue = value.Replace( "\t", "\r\n" );
                string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                var pathLossDir = _appTargetDir + $"PathLoss";

                if (!Directory.Exists( pathLossDir )) Directory.CreateDirectory( pathLossDir );
                if (_pathLossTxtChecked)
                    {
                    fileName =
                           fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                           + ".txt";

                    if (!File.Exists( pathLossDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{pathLossDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
                if (_pathLossAdfChecked)
                    {
                    fileName =
                        fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                        + ".adf";

                    if (!File.Exists( pathLossDir + fileName ))
                        {
                        using (var tempVar = File.Create( fileName, 1024 ))
                            {
                            File.WriteAllText( $"{pathLossDir}\\{fileName}", value );
                            tempVar.Close( );
                            }
                        }
                    }
                }
            catch (Exception db)
                {
                SaveResults.Text = $"PathLoss Download Exception\n" +
                                   $"Please Check Directory is valid\n" +
                                   $"Please Ensure You have Write Access"
                                   + db.Message;
                }
            }

        private void ConvertToWizardBatch( )
            {
            try
                {
                string[] separators = { "\r\n" };

                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\r\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var totalWords = _words.Length;
                var cont1 = -1;
                var cont3 = -91;
                var cont2 = -1;
                var cont4 = -91;
                var cont5 = -1;

                var saveState = "";
                var saveState2 = "";
                var saveState3 = "";
                var saveState4 = "";
                var saveState5 = "";
                var start = "A|TECC|ANTESD|01.00|930501|APF|01|Amphenol_Amphenol| \r\n" +
                            "|MFR|" + _words[3] + "|" + "\r\n" +
                            "|MODEL|" + _words[1] + "|\r\n" +
                            "|FILE|" + _words[1] + ".apf|\r\n" +
                            "|DESC|" + _description + "|\r\n" +
                            "|FCC ID|" + _fccId + "|\r\n" +
                            "|LENGTH|" + _length + "|\r\n" +
                            "|DATE|" + _date + "|\r\n" +
                            "|MFR ID|" + _mfrId + "|\r\n" +
                            "|FREQ|" + _words[5] + " MHz|\r\n" +
                            "|POLARIZATION|" + _polarization + "|\r\n" +
                            "|Hbeam|" + _words[7] + "|\r\n" +
                            "|Vbeam|" + _words[9] + "|\r\n" +
                            "|MaxGain|" + _maxGain + " |\r\n" +
                            "|MinGain|" + _minGain + "|\r\n" +
                            "|HORIZ|0|360|";
                //Horizontal
                for (var i = 21; i <= 739; i += 2)
                    {
                    cont1++;
                    var newvalue = _words[i];
                    var dblVal = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue );
                    saveState += "\t" + cont1.ToString( ) + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";
                    }

                //Verticals 1
                for (var i = 1283; i <= totalWords; i += 2)
                    {
                    cont3++;
                    var newvalue = _words[i];
                    var dblVal = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue );
                    saveState3 += "\t" + cont3 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                for (var i = 743; i <= 923; i += 2)
                    {
                    cont2++;
                    var newvalue = _words[i];
                    var dblVal = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue );
                    saveState2 += "\t" + cont2 + "\t" + dblVal.ToString( "0.000" ) + "\t" + "\r\n";

                    }
                //Verticals 2
                Stack<string> strStack = new Stack<string>( );
                for (var i = 1105; i <= 1283; i += 2)
                    {
                    var newvalue = _words[i];
                    var dblVal = Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue );
                    strStack.Push( dblVal.ToString( "0.000" ) );

                    }
                foreach (var strValue in strStack)
                    {
                    cont4++;
                    saveState4 += "\t" + cont4 + "\t" + strValue + "\t" + "\r\n";
                    }
                Stack<string> strStack2 = new Stack<string>( );
                for (var i = 923; i < 1105; i += 2)
                    {
                    var newvalue = _words[i];
                    var dblVal = (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue ));
                    strStack2.Push( dblVal.ToString( "0.000" ) );

                    }
                foreach (var strValue in strStack2)
                    {
                    cont5++;
                    saveState5 += "\t" + cont5 + "\t" + strValue + "\t" + "\r\n";
                    }

                _wizardConversionResults = start + "\r\n" + saveState + "|VERT|0|181| " + "\r\n" + saveState3 + saveState2 + "|VERT|180|181|\r\n" + saveState4 + saveState5;
                //txtGuardar.Text = saveState3 ;
                _wizardConversionResults = _wizardConversionResults.Replace( "\t", "|" );
                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Wizard Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        private void DownLoadWizardBatch( )
            {
            if (_wizardChecked)
                {
                var fileName = _currentIngestedPlanetFileName;
                try
                    {
                    string[] separators = { "\r\n" };
                    var value = _wizardConversionResults;
                    var commavalue = value.Replace( "\t", "\r\n" );
                    string[] words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                    var wizardDir = _appTargetDir + $"Wizard";

                    if (!Directory.Exists( wizardDir )) Directory.CreateDirectory( wizardDir );

                    if (_wizardApfChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".apf";
                        if (!File.Exists( wizardDir + fileName ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{wizardDir}\\{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }

                    if (_wizardTxtChecked)
                        {
                        fileName =
                            fileName.Substring( 0, fileName.LastIndexOf( ".", StringComparison.Ordinal ) )
                            + ".txt";

                        if (!File.Exists( wizardDir + fileName ))
                            {
                            using (var tempVar = File.Create( fileName, 1024 ))
                                {
                                File.WriteAllText( $"{wizardDir}\\{fileName}", value );
                                tempVar.Close( );
                                }
                            }
                        }
                    }
                catch (Exception db)
                    {
                    SaveResults.Text = $"Wizard Download Exception\n" +
                                       $"Please Check Directory is valid\n" +
                                       $"Please Ensure You have Write Access"
                                       + db.Message;
                    }
                }
            }

        private void ConvertToAtollBatch( )
            {
            try
                {
                string[] separators = { "\n" };
                var value = _currentIngestedPlanetFile;
                var commavalue = value.Replace( "\t", "\n" );
                _words = commavalue.Split( separators, StringSplitOptions.RemoveEmptyEntries );
                var cont1 = -1;
                // var count = 0;
                var count3 = 0.0;
                var saveState = "";


                for (int i = 21; i <= 740; i += 2)
                    {
                    cont1++;
                    string newvalue = _words[i];
                    double valor = Math.Round( (Convert.ToDouble( _maxGain ) - Convert.ToDouble( newvalue )), 2 );
                    count3 += 0.1;
                    _atollConversionResults = saveState += cont1.ToString( ) + " " + Math.Round( count3, 2 ) + " ";
                    }

                var dateMeasured = Convert.ToDateTime( _date );
                int k;
                int.TryParse( _trimmedTiltVal, out k );
                var d = new AtollPillaLst( )
                    {
                    Name = _words[1],
                    Name2 = _words[1],
                    Gain = _maxGain,
                    Manuf = _words[3],
                    Comm = _comments,
                    Patt = "2 0 0 360 " + saveState,
                    PET = k.ToString( ),//<-------
                    Beam = _beamwidth,//<-------
                    Fmin = _minFrequency,//<-------
                    Fmax = _maxFrequency,//<-------
                    Freq = _words[5], //Frequency
                    VWidth = _words[9],
                    FTB = _words[11],
                    Tilt = _trimmedTiltVal,
                    Hwidth = _words[7],
                    Fam = _family,//<-------
                    Dim = _dimensions,//<-------
                    Weight = _weight,//<-------
                    PPD = dateMeasured.ToString( "yyyy_mm_dd" )//<-------

                    };
                _datos.Add( d );
                //var rowCt = 2; // todo var _fileEntries[] 
                NsExcel.ApplicationClass toExcelApp = new NsExcel.ApplicationClass( );


                if (_isAtollExNew || _datOsCt == _fileEntries.Length)
                    {
                    if (_isAtollExNew)
                        {
                        toExcelApp.Visible = true;
                        var workBookPath = $"C:\\Users\\mmeza\\Desktop\\AtollTest.xlsx";
                        _workBook = toExcelApp.Workbooks.Open( workBookPath, 0, false, 5, "", "", false,
                           NsExcel.XlPlatform.xlWindows, "", true, false, 0, true, false, false );
                        var sheetOnOpen = (NsExcel.Worksheet)_workBook.Sheets[1];
                        _eXlBkOpen = sheetOnOpen;
                        }

                    #region Set Column Names
                    if (_rowCt <= 2)
                        {
                        _eXlBkOpen.Cells[1, 1] = $"Name";
                        _eXlBkOpen.Cells[1, 2] = $"Model";
                        _eXlBkOpen.Cells[1, 3] = $"Gain (dbi)";
                        _eXlBkOpen.Cells[1, 4] = $"Manufacturer";
                        _eXlBkOpen.Cells[1, 5] = $"Comments";
                        _eXlBkOpen.Cells[1, 6] = $"Pattern";
                        _eXlBkOpen.Cells[1, 7] = $"Pattern Electrical Tilt(?) ";
                        _eXlBkOpen.Cells[1, 8] = $"BeamWidth";
                        _eXlBkOpen.Cells[1, 9] = $"FMin";
                        _eXlBkOpen.Cells[1, 10] = $"FMax";
                        _eXlBkOpen.Cells[1, 11] = $"Frequency";
                        _eXlBkOpen.Cells[1, 12] = $"VWidth";
                        _eXlBkOpen.Cells[1, 13] = $"Front To Back";
                        _eXlBkOpen.Cells[1, 14] = $"Tilt";
                        _eXlBkOpen.Cells[1, 15] = $"H Width";
                        _eXlBkOpen.Cells[1, 16] = $"Family";
                        _eXlBkOpen.Cells[1, 17] = $"Dimensions HxWxD (inches)";
                        _eXlBkOpen.Cells[1, 18] = $"Weight (lbs)";
                        _eXlBkOpen.Cells[1, 19] = $"Pattern Posting Date";
                        _isAtollExNew = false;
                        }
                    #endregion
                    }
                #region Insert current datOs to current exRow

                if (_datOsCt <= _fileEntries.Length)
                    {
                    _eXlBkOpen.Cells[_rowCt, 1] = _datos[_datOsCt].Name;
                    _eXlBkOpen.Cells[_rowCt, 2] = _datos[_datOsCt].Name2;
                    _eXlBkOpen.Cells[_rowCt, 3] = _datos[_datOsCt].Gain;
                    _eXlBkOpen.Cells[_rowCt, 4] = _datos[_datOsCt].Manuf;
                    _eXlBkOpen.Cells[_rowCt, 5] = _datos[_datOsCt].Comm;
                    _eXlBkOpen.Cells[_rowCt, 6] = _datos[_datOsCt].Patt;
                    _eXlBkOpen.Cells[_rowCt, 7] = _datos[_datOsCt].PET;
                    _eXlBkOpen.Cells[_rowCt, 8] = _datos[_datOsCt].Beam;
                    _eXlBkOpen.Cells[_rowCt, 9] = _datos[_datOsCt].Fmin;
                    _eXlBkOpen.Cells[_rowCt, 10] = _datos[_datOsCt].Fmax;
                    _eXlBkOpen.Cells[_rowCt, 11] = _datos[_datOsCt].Freq;
                    _eXlBkOpen.Cells[_rowCt, 12] = _datos[_datOsCt].VWidth;
                    _eXlBkOpen.Cells[_rowCt, 13] = _datos[_datOsCt].FTB;
                    _eXlBkOpen.Cells[_rowCt, 14] = _datos[_datOsCt].Tilt;
                    _eXlBkOpen.Cells[_rowCt, 15] = _datos[_datOsCt].Hwidth;
                    _eXlBkOpen.Cells[_rowCt, 16] = _datos[_datOsCt].Fam;
                    _eXlBkOpen.Cells[_rowCt, 17] = _datos[_datOsCt].Dim;
                    _eXlBkOpen.Cells[_rowCt, 18] = _datos[_datOsCt].Weight;
                    _eXlBkOpen.Cells[_rowCt, 19] = _datos[_datOsCt].PPD;
                    _rowCt++;
                    ++_datOsCt;
                    if (_datOsCt == _fileEntries.Length)
                        {
                        var sT = DateTime.Now.ToShortDateString( );
                        var time = sT.Replace( '/', '_' );
                        var savePath = $"{_appTargetDir}Atoll_{FamilyTextBox.Text}_{time}.xlsx";
                        _workBook.SaveAs( savePath );
                        toExcelApp.Workbooks.Close( );
                        toExcelApp.Quit( );
                        }
                    }

                #endregion

                }
            catch (FormatException db)
                {
                SaveResults.Text = $"Atoll Conversion Format Exception\n" +
                                   $"Please check .pln is valid\n" +
                                   $"Please check values in 'Textboxes' are correct\n"
                                   + db.Message;
                }
            }
        #endregion
        
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
        }
    }