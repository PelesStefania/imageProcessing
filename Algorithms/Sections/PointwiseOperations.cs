using Emgu.CV.Structure;
using Emgu.CV;
using System;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {
        #region ProcessMatImmage

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
        #endregion

        #region Extract band from Mat Immage

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

        #region Apply gamma operator 

        public static Image<Bgr, byte> ApplyGammaToColorImage(Image<Bgr, byte> inputImage, double gamma)
        {
            Image<Bgr, byte> outputImage = inputImage.CopyBlank();

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    var pixel = inputImage[y, x];

                    byte B = (byte)(255 * Math.Pow(pixel.Blue / 255.0, gamma));
                    byte G = (byte)(255 * Math.Pow(pixel.Green / 255.0, gamma));
                    byte R = (byte)(255 * Math.Pow(pixel.Red / 255.0, gamma));

                    outputImage[y, x] = new Bgr(B, G, R);
                }
            }

            return outputImage;
        }


        #endregion


        #region Apply Otsu

        public static Image<Gray, byte> ApplyOtsuThreshold(Image<Gray, byte> inputImage)
        {
            int[] histogram = new int[256];
            int totalPixels = inputImage.Width * inputImage.Height;

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    int intensity = inputImage.Data[y, x, 0];
                    histogram[intensity]++;
                }
            }

            int threshold = ComputeOtsuThreshold(histogram, totalPixels);

            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = (inputImage.Data[y, x, 0] >= threshold) ? (byte)255 : (byte)0;
                }
            }

            return result;
        }

        #endregion

        #region Compute Otsu Treshold

        public static int ComputeOtsuThreshold(int[] histogram, int totalPixels)
        {
            float sum = 0;
            for (int i = 0; i < 256; i++)
                sum += i * histogram[i];

            float sumB = 0, wB = 0, wF = 0;
            float maxVariance = 0;
            int threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histogram[t];
                if (wB == 0) continue;

                wF = totalPixels - wB;
                if (wF == 0) break;

                sumB += t * histogram[t];
                float meanB = sumB / wB;
                float meanF = (sum - sumB) / wF;

                float varianceBetween = wB * wF * (meanB - meanF) * (meanB - meanF);

                if (varianceBetween > maxVariance)
                {
                    maxVariance = varianceBetween;
                    threshold = t;
                }
            }

            return threshold;
        }

        #endregion
    }
}