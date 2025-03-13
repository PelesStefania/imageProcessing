using Emgu.CV;
using Emgu.CV.Structure;

using Microsoft.Win32;
using System.Windows;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

using Framework.View;
using static Framework.Utilities.DataProvider;
using static Framework.Utilities.DrawingHelper;
using static Framework.Utilities.FileHelper;
using static Framework.Converters.ImageConverter;

using Algorithms.Sections;
using Algorithms.Tools;
using Algorithms.Utilities;
using Framework.Utilities;
using System;
using MatFileHandler;
using System.Linq;



namespace Framework.ViewModel
{
    public class MenuCommands : BaseVM
    {
        private readonly MainVM _mainVM;

        public MenuCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }

        private ImageSource InitialImage
        {
            get => _mainVM.InitialImage;
            set => _mainVM.InitialImage = value;
        }

        private ImageSource ProcessedImage
        {
            get => _mainVM.ProcessedImage;
            set => _mainVM.ProcessedImage = value;
        }

        private double ScaleValue
        {
            get => _mainVM.ScaleValue;
            set => _mainVM.ScaleValue = value;
        }

        #region File

        #region Load grayscale image
        private RelayCommand _loadGrayscaleImageCommand;
        public RelayCommand LoadGrayscaleImageCommand
        {
            get
            {
                if (_loadGrayscaleImageCommand == null)
                    _loadGrayscaleImageCommand = new RelayCommand(LoadGrayscaleImage);
                return _loadGrayscaleImageCommand;
            }
        }

        private void LoadGrayscaleImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a grayscale picture");
            if (fileName != null)
            {
                GrayInitialImage = new Image<Gray, byte>(fileName);
                InitialImage = Convert(GrayInitialImage);
            }
        }
        #endregion



        #region Load color image
        private ICommand _loadColorImageCommand;
        public ICommand LoadColorImageCommand
        {
            get
            {
                if (_loadColorImageCommand == null)
                    _loadColorImageCommand = new RelayCommand(LoadColorImage);
                return _loadColorImageCommand;
            }
        }

        private void LoadColorImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a color picture");
            if (fileName != null)
            {
                ColorInitialImage = new Image<Bgr, byte>(fileName);
                InitialImage = Convert(ColorInitialImage);
            }
        }
        #endregion

        #region Save processed image
        private ICommand _saveProcessedImageCommand;
        public ICommand SaveProcessedImageCommand
        {
            get
            {
                if (_saveProcessedImageCommand == null)
                    _saveProcessedImageCommand = new RelayCommand(SaveProcessedImage);
                return _saveProcessedImageCommand;
            }
        }

        private void SaveProcessedImage(object parameter)
        {
            if (GrayProcessedImage == null && ColorProcessedImage == null)
            {
                MessageBox.Show("If you want to save your processed image, " +
                    "please load and process an image first!");
                return;
            }

            string imagePath = SaveFileDialog("image.jpg");
            if (imagePath != null)
            {
                GrayProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                ColorProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                Process.Start(imagePath);
            }
        }
        #endregion

        #region Exit
        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new RelayCommand(Exit);
                return _exitCommand;
            }
        }

        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        #region Edit

        #region Remove drawn shapes from initial canvas
        private ICommand _removeInitialDrawnShapesCommand;
        public ICommand RemoveInitialDrawnShapesCommand
        {
            get
            {
                if (_removeInitialDrawnShapesCommand == null)
                    _removeInitialDrawnShapesCommand = new RelayCommand(RemoveInitialDrawnShapes);
                return _removeInitialDrawnShapesCommand;
            }
        }

        private void RemoveInitialDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from processed canvas
        private ICommand _removeProcessedDrawnShapesCommand;
        public ICommand RemoveProcessedDrawnShapesCommand
        {
            get
            {
                if (_removeProcessedDrawnShapesCommand == null)
                    _removeProcessedDrawnShapesCommand = new RelayCommand(RemoveProcessedDrawnShapes);
                return _removeProcessedDrawnShapesCommand;
            }
        }

        private void RemoveProcessedDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from both canvases
        private ICommand _removeDrawnShapesCommand;
        public ICommand RemoveDrawnShapesCommand
        {
            get
            {
                if (_removeDrawnShapesCommand == null)
                    _removeDrawnShapesCommand = new RelayCommand(RemoveDrawnShapes);
                return _removeDrawnShapesCommand;
            }
        }

        private void RemoveDrawnShapes(object parameter)
        {
            var canvases = (object[])parameter;
            RemoveUiElements(canvases[0] as Canvas);
            RemoveUiElements(canvases[1] as Canvas);
        }
        #endregion

        #region Clear initial canvas
        private ICommand _clearInitialCanvasCommand;
        public ICommand ClearInitialCanvasCommand
        {
            get
            {
                if (_clearInitialCanvasCommand == null)
                    _clearInitialCanvasCommand = new RelayCommand(ClearInitialCanvas);
                return _clearInitialCanvasCommand;
            }
        }

        private void ClearInitialCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayInitialImage = null;
            ColorInitialImage = null;
            InitialImage = null;
        }
        #endregion

        #region Clear processed canvas
        private ICommand _clearProcessedCanvasCommand;
        public ICommand ClearProcessedCanvasCommand
        {
            get
            {
                if (_clearProcessedCanvasCommand == null)
                    _clearProcessedCanvasCommand = new RelayCommand(ClearProcessedCanvas);
                return _clearProcessedCanvasCommand;
            }
        }

        private void ClearProcessedCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayProcessedImage = null;
            ColorProcessedImage = null;
            ProcessedImage = null;
        }
        #endregion

        #region Closing all open windows and clear both canvases
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                    _clearCommand = new RelayCommand(Clear);
                return _clearCommand;
            }
        }

        private void Clear(object parameter)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }

            ScaleValue = 1;

            var canvases = (object[])parameter;
            ClearInitialCanvas(canvases[0] as Canvas);
            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion

        #endregion

        #region Tools

        #region Magnifier
        private ICommand _magnifierCommand;
        public ICommand MagnifierCommand
        {
            get
            {
                if (_magnifierCommand == null)
                    _magnifierCommand = new RelayCommand(Magnifier);
                return _magnifierCommand;
            }
        }

        private void Magnifier(object parameter)
        {
            if (MagnifierOn == true) return;
            if (MouseClickCollection.Count == 0)
            {
                MessageBox.Show("Please select an area first!");
                return;
            }

            MagnifierWindow magnifierWindow = new MagnifierWindow();
            magnifierWindow.Show();
        }
        #endregion

        #region Visualize color levels

        #region Row color levels
        private ICommand _rowColorLevelsCommand;
        public ICommand RowColorLevelsCommand
        {
            get
            {
                if (_rowColorLevelsCommand == null)
                    _rowColorLevelsCommand = new RelayCommand(RowColorLevels);
                return _rowColorLevelsCommand;
            }
        }

        private void RowColorLevels(object parameter)
        {
            if (RowColorLevelsOn == true) return;
            if (MouseClickCollection.Count == 0)
            {
                MessageBox.Show("Please select an area first!");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Row);
            window.Show();
        }
        #endregion

        #region Column color levels
        private ICommand _columnColorLevelsCommand;
        public ICommand ColumnColorLevelsCommand
        {
            get
            {
                if (_columnColorLevelsCommand == null)
                    _columnColorLevelsCommand = new RelayCommand(ColumnColorLevels);
                return _columnColorLevelsCommand;
            }
        }

        private void ColumnColorLevels(object parameter)
        {
            if (ColumnColorLevelsOn == true) return;
            if (MouseClickCollection.Count == 0)
            {
                MessageBox.Show("Please select an area first!");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Column);
            window.Show();
        }
        #endregion

        #endregion

        #region Visualize image histogram

        #region Initial image histogram
        private ICommand _histogramInitialImageCommand;
        public ICommand HistogramInitialImageCommand
        {
            get
            {
                if (_histogramInitialImageCommand == null)
                    _histogramInitialImageCommand = new RelayCommand(HistogramInitialImage);
                return _histogramInitialImageCommand;
            }
        }

        private void HistogramInitialImage(object parameter)
        {
            if (InitialHistogramOn == true) return;
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }

            HistogramWindow window = null;

            if (ColorInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialColor);
            }
            else if (GrayInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialGray);
            }

            window.Show();
        }
        #endregion

        #region Processed image histogram
        private ICommand _histogramProcessedImageCommand;
        public ICommand HistogramProcessedImageCommand
        {
            get
            {
                if (_histogramProcessedImageCommand == null)
                    _histogramProcessedImageCommand = new RelayCommand(HistogramProcessedImage);
                return _histogramProcessedImageCommand;
            }
        }

        private void HistogramProcessedImage(object parameter)
        {
            if (ProcessedHistogramOn == true) return;
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image first!");
                return;
            }

            HistogramWindow window = null;

            if (ColorProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedColor);
            }
            else if (GrayProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedGray);
            }

            window.Show();
        }
        #endregion

        #endregion

        #region Copy image
        private ICommand _copyImageCommand;
        public ICommand CopyImageCommand
        {
            get
            {
                if (_copyImageCommand == null)
                    _copyImageCommand = new RelayCommand(CopyImage);
                return _copyImageCommand;
            }
        }

        private void CopyImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Copy(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
            else if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Copy(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
        }
        #endregion

        #region Invert image
        private ICommand _invertImageCommand;
        public ICommand InvertImageCommand
        {
            get
            {
                if (_invertImageCommand == null)
                    _invertImageCommand = new RelayCommand(InvertImage);
                return _invertImageCommand;
            }
        }

        private void InvertImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }

            ClearProcessedCanvas(parameter as Canvas);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Invert(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Invert(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #region Convert color image to grayscale image
        private ICommand _convertImageToGrayscaleCommand;
        public ICommand ConvertImageToGrayscaleCommand
        {
            get
            {
                if (_convertImageToGrayscaleCommand == null)
                    _convertImageToGrayscaleCommand = new RelayCommand(ConvertImageToGrayscale);
                return _convertImageToGrayscaleCommand;
            }
        }

        private void ConvertImageToGrayscale(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                GrayProcessedImage = Tools.Convert(ColorInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else
            {
                MessageBox.Show("It is possible to convert only color images!");
            }
        }
        #endregion

        #region Binary
        private ICommand _binarizeImageCommand;
        public ICommand BinarizeImageCommand
        {
            get
            {
                if (_binarizeImageCommand == null)
                    _binarizeImageCommand = new RelayCommand(BinarizeImage);
                return _binarizeImageCommand;
            }

        }
        private void BinarizeImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }
            ClearProcessedCanvas(parameter);
            List<string> labels = new List<string> {
                "Enter first value(0-255): ",
                "Enter second value(0-255): "
            };
            DialogWindow window = new DialogWindow(_mainVM, labels);
            window.ShowDialog();
            List<double> values = window.GetValues();
            if (values.Count < 2)
            {
                MessageBox.Show("Invalid input values!");
                return;
            }
            double treshold1 = values[0];
            double treshold2 = values[1];

            if (treshold1 < 0 || treshold1 > 255 )
            {
                MessageBox.Show("The first value does not match!");
                return;
            }
            if (treshold2 < 0 || treshold2 > 255)
            {
                MessageBox.Show("The second value does not match!");
                return;
            }
            byte t1=(byte)treshold1;
            byte t2=(byte)treshold2;

                if (GrayInitialImage != null)
                {
                    GrayProcessedImage = Tools.Binary(GrayInitialImage, t1,t2);
                    ProcessedImage = Convert(GrayProcessedImage);
                }
                else
                {
                    MessageBox.Show("Only gray images can be modified!");
                    return;
                }
            


        }
        #endregion

        #region Crop Image
        private ICommand _cropImageCommand;
        public ICommand CropImageCommand
        {
            get
            {
                if (_cropImageCommand == null)
                    _cropImageCommand = new RelayCommand(CropImage);
                return _cropImageCommand;
            }
        }
        private void CropImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }
            var count = MouseClickCollection.Count;

            if (count < 2)
            {
                MessageBox.Show("Select 2 points!");
                return;
            }
            
            Point p1 = MouseClickCollection[MouseClickCollection.Count - 2];
            Point p2 = MouseClickCollection[MouseClickCollection.Count-1];

            var canvases = (object[])parameter;
            var initialCanvas = canvases[0] as Canvas;
            var procesedCanvas = canvases[1] as Canvas;
            ClearProcessedCanvas(procesedCanvas);
            
            int x1 = (int)Math.Min(p1.X, p2.X);
            int y1 = (int)Math.Min(p1.Y, p2.Y);
            int x2 = (int)Math.Max(p1.X, p2.X);
            int y2 = (int)Math.Max(p1.Y, p2.Y);
            p1 = new Point(x1, y1);
            p2 = new Point(x2, y2);
            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Crop(GrayInitialImage, x1, y1, x2, y2);
                ProcessedImage = Convert(GrayProcessedImage);
                if (initialCanvas != null && procesedCanvas != null)
                {
                        DrawRectangle(initialCanvas, p1, p2, 2, Brushes.Red, ScaleValue);
                }
                ComputeStatisticsAndShowMessage(GrayProcessedImage);


            }
            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Crop(ColorInitialImage, x1, y1, x2, y2);
                ProcessedImage = Convert(ColorProcessedImage);
                if (initialCanvas != null && procesedCanvas != null)
                {
                        DrawRectangle(initialCanvas, p1, p2, 2, Brushes.Red, ScaleValue);
                }
                ComputeStatisticsAndShowMessage(ColorProcessedImage);


            }
            RemoveInitialDrawnShapes(initialCanvas);

        }
        private void ComputeStatisticsAndShowMessage(Image<Gray, byte> croppedImage)
        {
            if (croppedImage == null)
            {
                MessageBox.Show("No cropped image available!");
                return;
            }

            int width = croppedImage.Width;
            int height = croppedImage.Height;
            int totalPixels = width * height;

            if (totalPixels == 0)
            {
                MessageBox.Show("Invalid cropped image dimensions!");
                return;
            }

            double sum = 0;
            double sumOfSquares = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte pixelValue = croppedImage.Data[y, x, 0];
                    sum += pixelValue;
                    sumOfSquares += pixelValue * pixelValue;
                }
            }

            double mean = sum / totalPixels;

            double variance = (sumOfSquares / totalPixels) - (mean * mean);
            double stdDeviation = Math.Sqrt(variance);

            MessageBox.Show($"Media pixelilor: {mean:F2}\nAbaterea medie pătratică: {stdDeviation:F2}", "Statisticile imaginii");
        }

        private void ComputeStatisticsAndShowMessage(Image<Bgr, byte> croppedImage)
        {
            if (croppedImage == null)
            {
                MessageBox.Show("No cropped image available!");
                return;
            }

            int width = croppedImage.Width;
            int height = croppedImage.Height;
            int totalPixels = width * height;

            if (totalPixels == 0)
            {
                MessageBox.Show("Invalid cropped image dimensions!");
                return;
            }

            double[] sum = new double[3]; 
            double[] sumOfSquares = new double[3]; 

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int c = 0; c < 3; c++) 
                    {
                        byte pixelValue = croppedImage.Data[y, x, c];
                        sum[c] += pixelValue;
                        sumOfSquares[c] += pixelValue * pixelValue;
                    }
                }
            }

            double[] mean = new double[3];
            double[] variance = new double[3];
            double[] stdDeviation = new double[3];

            for (int c = 0; c < 3; c++)
            {
                mean[c] = sum[c] / totalPixels;
                variance[c] = (sumOfSquares[c] / totalPixels) - (mean[c] * mean[c]);
                stdDeviation[c] = Math.Sqrt(variance[c]);
            }

            MessageBox.Show($"Media pixelilor:\nBlue: {mean[0]:F2}, Green: {mean[1]:F2}, Red: {mean[2]:F2}\n" +
                            $"Abaterea standard:\nBlue: {stdDeviation[0]:F2}, Green: {stdDeviation[1]:F2}, Red: {stdDeviation[2]:F2}",
                            "Statisticile imaginii");
        }


        #endregion
        #region Mirror Image
        private ICommand _mirrorImageCommand;
        public ICommand MirrorImageCommand{
            get {
                if (_mirrorImageCommand == null)
                    _mirrorImageCommand = new RelayCommand(MirrorImage);

                return _mirrorImageCommand;
            }
        }
        private void MirrorImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Mirror(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Mirror(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #region Clockwise Image
        private ICommand _clockwiseImageCommand;
        public ICommand ClockwiseImageCommand
        {
           get{ 
                if (_clockwiseImageCommand == null)
                    _clockwiseImageCommand = new RelayCommand(ClockwiseImage);
                return _clockwiseImageCommand;
            }
        }

        private void ClockwiseImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }
            ClearProcessedCanvas(parameter);
            if (GrayInitialImage != null) {
                GrayProcessedImage= Tools.Clockwise(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            if (ColorInitialImage != null)
            {

                ColorProcessedImage = Tools.Clockwise(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }




        #endregion
        #region Anti Clockwise Image
        private ICommand _antiClockwiseImageCommand;
        public ICommand AntiClockwiseImageCommand
        {
            get
            {
                if (_antiClockwiseImageCommand == null)
                    _antiClockwiseImageCommand = new RelayCommand(AntiClockwiseImage);
                return _antiClockwiseImageCommand;
            }
        }

        private void AntiClockwiseImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }
            ClearProcessedCanvas(parameter);
            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.AntiClockwise(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            if (ColorInitialImage != null)
            {

                ColorProcessedImage = Tools.AntiClockwise(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }




        #endregion

        #endregion

        #region Pointwise operations
        #endregion

        #region Thresholding
        #endregion

        #region Filters
        #endregion

        #region Morphological operations
        #endregion

        #region Geometric transformations
        #endregion

        #region Segmentation
        #endregion

        #region Use processed image as initial image
        private ICommand _useProcessedImageAsInitialImageCommand;
        public ICommand UseProcessedImageAsInitialImageCommand
        {
            get
            {
                if (_useProcessedImageAsInitialImageCommand == null)
                    _useProcessedImageAsInitialImageCommand = new RelayCommand(UseProcessedImageAsInitialImage);
                return _useProcessedImageAsInitialImageCommand;
            }
        }

        private void UseProcessedImageAsInitialImage(object parameter)
        {
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image first!");
                return;
            }

            var canvases = (object[])parameter;

            ClearInitialCanvas(canvases[0] as Canvas);

            if (GrayProcessedImage != null)
            {
                GrayInitialImage = GrayProcessedImage;
                InitialImage = Convert(GrayInitialImage);
            }
            else if (ColorProcessedImage != null)
            {
                ColorInitialImage = ColorProcessedImage;
                InitialImage = Convert(ColorInitialImage);
            }

            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion

        #region Load Multispectral Image

        private RelayCommand _loadMultispectralImageCommand;
        public RelayCommand LoadMultispectralImageCommand
        {
            get
            {
                if (_loadMultispectralImageCommand == null)
                    _loadMultispectralImageCommand = new RelayCommand(LoadMultispectralImage);
                return _loadMultispectralImageCommand;
            }
        }
        private void LoadMultispectralImage(object parameter)
        {
            Clear(parameter);
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a multispectral image",
                Filter = "MAT Files|*.mat"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                IMatFile matFile;
                using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
                {
                    var reader = new MatFileReader(fileStream);
                    matFile = reader.Read();
                }

                IVariable imageVariable = matFile["paviaU"];
                IArray imageData = imageVariable.Value;
                int height = imageData.Dimensions[0];
                int width = imageData.Dimensions[1];
                int bands = imageData.Dimensions[2];

                double[] rawPixelValues = imageData.ConvertToDoubleArray();

                double minVal = rawPixelValues.Min();
                double maxVal = rawPixelValues.Max();
                double scale = 255.0 / (maxVal - minVal);
                byte[] normalizedPixels = rawPixelValues.Select(v => (byte)((v - minVal) * scale)).ToArray();


                int bBand, gBand, rBand;

                List<string> labels = new List<string> {
                        "Enter blue band: ",
                        "Enter green band: ",
                        "Enter red band: ",
                    };
                DialogWindow window = new DialogWindow(_mainVM, labels);
                window.ShowDialog();
                List<double> values = window.GetValues();
                if (values.Count < 3)
                {
                    MessageBox.Show("Invalid input values!");
                    return;
                }
                bBand = 13;//(int)values[0];
                gBand = 38;//(int)values[1];
                rBand = 63;//(int)values[2];

                if (bBand < 0 || bBand >=bands)
                {
                    MessageBox.Show("The first value does not match!");
                    return;
                }
                if (gBand < 0 || gBand >=bands)
                {
                    MessageBox.Show("The second value does not match!");
                    return;
                }
                if (rBand < 0 || rBand >=bands)
                {
                    MessageBox.Show("The third value does not match!");
                    return;
                }


                Image<Bgr, byte> multispectralImage = ProcessMatImage(normalizedPixels, width, height, bBand, gBand, rBand);
                ColorInitialImage = multispectralImage;
                InitialImage = Convert(ColorInitialImage);
               
            }

        }

        public static Image<Bgr, byte> ProcessMatImage(byte[] pixelValues, int width, int height, int bandB, int bandG, int bandR)
        {
            var blueBand = ExtractBand(pixelValues, width, height, bandB);
            var greenBand = ExtractBand(pixelValues, width, height, bandG);
            var redBand = ExtractBand(pixelValues, width, height, bandR);

            Image<Bgr, byte> multispectralImage = new Image<Bgr, byte>(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    multispectralImage.Data[y, x, 0] = blueBand.Data[y, x, 0];
                    multispectralImage.Data[y, x, 1] = greenBand.Data[y, x, 0];
                    multispectralImage.Data[y, x, 2] = redBand.Data[y, x, 0];
                }
            }

            return multispectralImage;
        }

        public static Image<Gray, byte> ExtractBand(byte[] pixelValues, int width, int height, int bandIndex)
        {
            Image<Gray, byte> bandImage = new Image<Gray, byte>(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y + x * height + bandIndex * width * height;
                    bandImage.Data[y, x, 0] = pixelValues[index];
                }
            }

            return bandImage;
        }
      
        #endregion



        #region Apply Gamma

        private RelayCommand _applyGammaCommand;
        public RelayCommand ApplyGammaCommand
        {
            get
            {
                if (_applyGammaCommand == null)
                    _applyGammaCommand = new RelayCommand(ApplyGammaOperator);
                return _applyGammaCommand;
            }
        }
        private void ApplyGammaOperator(object parameter)
        {

            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image!");
                return;
            }
            ClearProcessedCanvas(parameter);
            double gamma;
            List<string> labels = new List<string> {
                        "Enter gamma: ",
                    };
            DialogWindow window = new DialogWindow(_mainVM, labels);
            window.ShowDialog();
            List<double> values = window.GetValues();
            if (values.Count < 1)
            {
                MessageBox.Show("Invalid input values!");
                return;
            }
            gamma = values[0];
            if (ColorInitialImage != null)
            {

                Image<Bgr, byte> outputImage = ColorInitialImage.CopyBlank();

                for (int y = 0; y < ColorInitialImage.Height; y++)
                {
                    for (int x = 0; x < ColorInitialImage.Width; x++)
                    {
                        var pixel = ColorInitialImage[y, x];

                        byte B = (byte)(255 * Math.Pow(pixel.Blue / 255.0, gamma));
                        byte G = (byte)(255 * Math.Pow(pixel.Green / 255.0, gamma));
                        byte R = (byte)(255 * Math.Pow(pixel.Red / 255.0, gamma));

                        outputImage[y, x] = new Bgr(B, G, R);
                    }
                }

                ColorProcessedImage = outputImage;
                ProcessedImage = Convert(ColorProcessedImage);
            }
            else
            {
                MessageBox.Show("Only color images can be modified!");
                return;
            }

            

        }

        #endregion


    }
}