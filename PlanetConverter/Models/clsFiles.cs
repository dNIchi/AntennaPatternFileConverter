using System.IO;

namespace PlanetConverter.Models
    {


    public class ClsFiles : MainWindow
        {

       // private string _airComConversionResults;
        private string _savePath = $"C:\\Code\\PRJ-2_PlanetConvert\\TestingFolder";
        private string _outputFilePath = $"C:\\Code\\PRJ-2_PlanetConvert\\Converted Downloads\\";

       
        public void BrowseLocalDirectory( )
            {

            #region Changes Per meeting Carlos 4.6.17    
            //todo open directory
            // Create OpenFileDialog  
            var dlg = new Microsoft.Win32.OpenFileDialog( );

            //todo consume all files as List ?? 
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".pln";
            dlg.Filter = "";

            //todo iterate files in collection and apply logic below 
            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog( );

            #endregion
            //todo apply this.Logic ()=> foreach file in collection
            // Get the selected file name and display in a TextBox 
            if (result == true)
                {
                // Open document 
                var pathToFile = dlg.FileName;

                var fileName = Path.GetFileName( pathToFile );

                ResultsLabel.Content = fileName;

                _savePath = $"C:\\Code\\PRJ-2_PlanetConvert\\PlanetTestingFolder\\";
                //fileName
                string pathToCheck = $"{_savePath}{fileName}";
                string tempFileName = string.Empty;

                if (File.Exists( pathToCheck ))
                    {
                    int counter = 2;
                    while (File.Exists( pathToCheck ))
                        {
                        tempFileName = $"{counter}{fileName}";
                        pathToCheck = $"{_savePath}{tempFileName}";
                        counter++;
                        }
                    fileName = tempFileName;
                    ResultsLabel.Content = $"A file with the same name already exists \nYour file was saved as {fileName}";
                    }
                else
                    {
                    ResultsLabel.Content = "Your file was uploaded successfully";
                    }
                try
                    {
                    File.Copy( pathToFile, _savePath += fileName );
                    FileStream fileObj = new FileStream( _savePath, FileMode.Open, FileAccess.Read );
                    StreamReader readerObj = new StreamReader( fileObj );
                    string text = readerObj.ReadToEnd( );
                    readerObj.Close( );
                    string readInfo = text;
                    SaveResults.Text = readInfo;
                    }
                catch (FileNotFoundException db)
                    {
                    ResultsLabel.Content = $"An error occurred with file {fileName}";
                    DebugLabel.Content = db.Message;
                    }

                }

            }


        }
    }
