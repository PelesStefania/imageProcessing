using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Threading.Tasks;
using Emgu.CV.Util;

namespace Algorithms.Sections
{
    public class Filters
    {
        #region Median Filtre
        public static Image<Gray, byte> MedianFilter(Image<Gray, byte> inputImage, int radius)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;
            int kernelSize = 2 * radius + 1;
            int windowArea = kernelSize * kernelSize;

            Image<Gray, byte> result = new Image<Gray, byte>(width, height);

            int[][] columnHistograms = ColumnHistograms(inputImage, radius, width, height);

            int[] kernelHistogram = new int[256];

            for (int y = 0; y < height; y++)
            {
                KernelHistogram(kernelHistogram, columnHistograms, radius, width);

                for (int x = 0; x < width; x++)
                {
                    byte median = FindMedian(kernelHistogram, windowArea);
                    result.Data[y, x, 0] = median;

                    if (x - radius >= 0 && x + radius + 1 < width)
                    {
                        SubtractHistogram(kernelHistogram, columnHistograms[x - radius]);
                        AddHistogram(kernelHistogram, columnHistograms[x + radius + 1]);
                    }
                }

            
                if (y - radius >= 0 && y + radius + 1 < height)
                {
                    UpdateColumnHistograms(columnHistograms, inputImage, y, radius, width);
                }
            }

            return result;
        }

        private static int[][] ColumnHistograms(Image<Gray, byte> image, int radius, int width, int height)
        {
            int kernelHeight = 2 * radius + 1;
            int[][] histograms = new int[width][];

            for (int x = 0; x < width; x++)
            {
                histograms[x] = new int[256];
                for (int y = 0; y < Math.Min(height, kernelHeight); y++)
                {
                    byte pixel = image.Data[y, x, 0];
                    histograms[x][pixel]++;
                }
            }

            return histograms;
        }
       
        private static void KernelHistogram(int[] kernelHistogram, int[][] columnHistograms, int radius, int width)
        {
            Array.Clear(kernelHistogram, 0, 256);
            int startX = Math.Max(0, 0);
            int endX = Math.Min(width - 1, 2 * radius);

            for (int x = startX; x <= endX; x++)
            {
                AddHistogram(kernelHistogram, columnHistograms[x]);
            }
        }

        private static void UpdateColumnHistograms(int[][] columnHistograms, Image<Gray, byte> image, int y, int radius, int width)
        {
            for (int x = 0; x < width; x++)
            {
                byte topPixel = image.Data[y - radius, x, 0];
                byte bottomPixel = image.Data[y + radius + 1, x, 0];

                columnHistograms[x][topPixel]--;
                columnHistograms[x][bottomPixel]++;
            }
        }

        private static void AddHistogram(int[] target, int[] source)
        {
            for (int i = 0; i < 256; i++)
            {
                target[i] += source[i];
            }
        }

        private static void SubtractHistogram(int[] target, int[] source)
        {
            for (int i = 0; i < 256; i++)
            {
                target[i] -= source[i];
            }
        }

        private static byte FindMedian(int[] histogram, int totalCount)
        {
            int count = 0;
            int threshold = (totalCount / 2)+1;

            for (int i = 0; i < 256; i++)
            {
                count += histogram[i];
                if (count >= threshold)
                    return (byte)i;
            }

            return 0; 
        }

        public static Image<Bgr, byte> MedianFilter(Image<Bgr, byte> inputImage, int radius)
        {
            
            Image<Gray, byte>[] channels = inputImage.Split();

         
            Image<Gray, byte> blueFiltered = MedianFilter(channels[0], radius);
            Image<Gray, byte> greenFiltered = MedianFilter(channels[1], radius);
            Image<Gray, byte> redFiltered = MedianFilter(channels[2], radius);

            
            Mat merged = new Mat();
            CvInvoke.Merge(new VectorOfMat(blueFiltered.Mat, greenFiltered.Mat, redFiltered.Mat), merged);

           
            return merged.ToImage<Bgr, byte>();
        }


        #endregion
    }
}



