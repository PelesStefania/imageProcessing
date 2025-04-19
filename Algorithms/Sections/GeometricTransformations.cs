using Emgu.CV.Structure;
using Emgu.CV;
using System;
using Emgu.CV.Util;

namespace Algorithms.Sections
{
    public class GeometricTransformations
    {
        #region Apply scaling
        public static Image<Gray, byte> Scaling(Image<Gray, byte> inputImage,double coefficient)
        {
            int newWidth = (int)(inputImage.Width * coefficient);
            int newHeight = (int)(inputImage.Height * coefficient);
            Image<Gray, byte> result = new Image<Gray, byte>(newWidth, newHeight);

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    double srcX = x / coefficient;
                    double srcY = y / coefficient;

                    int x1 = (int)Math.Floor(srcX);
                    int y1 = (int)Math.Floor(srcY);
                    int x2 = Math.Min(x1 + 1, inputImage.Width - 1);
                    int y2 = Math.Min(y1 + 1, inputImage.Height - 1);

                    double dx = srcX - x1;
                    double dy = srcY - y1;

                    double val = (1 - dx) * (1 - dy) * inputImage.Data[y1, x1, 0] +
                                 dx * (1 - dy) * inputImage.Data[y1, x2, 0] +
                                 (1 - dx) * dy * inputImage.Data[y2, x1, 0] +
                                 dx * dy * inputImage.Data[y2, x2, 0];

                    result.Data[y, x, 0] = (byte)Math.Round(val);
                }
            }

            return result;
        }

        public static Image<Bgr, byte> Scaling(Image<Bgr, byte> inputImage,double coefficient)
        {
            Image<Hsv, byte> hsvImage = inputImage.Convert<Hsv, byte>();
            Image<Gray, byte>[] hsvChannels = hsvImage.Split();

            Image<Gray, byte> hScaled = Scaling(hsvChannels[0], coefficient);
            Image<Gray, byte> sScaled = Scaling(hsvChannels[1], coefficient);
            Image<Gray, byte> vScaled = Scaling(hsvChannels[2], coefficient);

            Mat merged = new Mat();
            CvInvoke.Merge(new VectorOfMat(hScaled.Mat, sScaled.Mat, vScaled.Mat), merged);

            Image<Hsv, byte> mergedHsv = merged.ToImage<Hsv, byte>();
            Image<Bgr, byte> result = mergedHsv.Convert<Bgr, byte>();

            return result;

        }
        #endregion
    }
}