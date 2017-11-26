using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetConverter.Models
{
    public static class ConvertDep
    {
        public static bool IsCheckBoxChecked { get; set; }
        public static bool IsFileTypeSelected { get; set; }
        public static string CurrentFileName { get; set; }
        public static string CurrentIngestedtFile { get; set; }
        public static string SourcePlanetDirectory { get; set; }
        public static string IngestDirectory { get;  set; }
        public static string TargetDirectory { get; set; }
        public static string[] FileEntries { get; set; }
        public static string[] Words { get; set; }
        public static string ConversionResults { get; set; }
        public static string CurrentHorzConversionResults { get; set; }
        public static string CurrentVertConversionResults { get; set; }
        public static List<string> ErrorLog { get; set; }
        public static List<string> SuccessLog { get; set; }
        public static int FileCount { get; set; }



        public static string AntHeight { get; set; }
        public static string AntWidth { get; set; }
        public static string Atvswr { get; set; }
        public static string AzimuthDisplay { get; set; }
        public static string Beamwidth { get; set; }
        public static string Comments { get; set; }
        public static string Date { get; set; }
        public static string DateMeasured { get; set; }
        public static string Depth { get; set; }
        public static string Description { get; set; }
        public static string Dimensions { get; set; }
        public static string ElectricalTilt { get; set; }
        public static string Family { get; set; }
        public static string FccId { get; set; }
        public static string FrontToBack { get; set; }
        public static string Gain { get; set; }
        public static string Height { get; set; }
        public static string HighFrequency { get; set; }
        public static string HorizontalBeamWidth { get; set; }
        public static string VerticalBeamWidth { get; set; }
        public static string Length { get; set; }
        public static string LobeTilt { get; set; }
        public static string LowFrequency { get; set; }
        public static string LowerFrequency { get; set; }
        public static string MaxFrequency { get; set; }
        public static string MaxGain { get; set; }
        public static string MaxPower { get; set; }
        public static string MeasFrequency { get; set; }
        public static string MfrId { get; set; }
        public static string MinGain { get; set; }
        public static string MinFrequency { get; set; }
        public static string Model { get; set; }
        public static string PatternElectricalTilt { get; set; }
        public static string Polarization { get; set; }
        public static string Size { get; set; }
        public static string Time { get; set; }
        public static string UpperFrequency { get; set; }
        public static string Width { get; set; }
        public static string Weight { get; set; }
        public static string WindArea { get; set; }
        public static string TiltValue { get; set; }

    }
}
