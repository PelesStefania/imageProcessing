using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace Algorithms.Tools
{
    public class Tools
    {
        #region Copy
        public static Image<Gray, byte> Copy(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Clone();
            return result;
        }

        public static Image<Bgr, byte> Copy(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = inputImage.Clone();
            return result;
        }
        #endregion

        #region Invert
        public static Image<Gray, byte> Invert(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> Invert(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                    result.Data[y, x, 1] = (byte)(255 - inputImage.Data[y, x, 1]);
                    result.Data[y, x, 2] = (byte)(255 - inputImage.Data[y, x, 2]);
                }
            }
            return result;
        }
        #endregion

        #region Convert color image to grayscale image
        public static Image<Gray, byte> Convert(Image<Bgr, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Convert<Gray, byte>();
            return result;
        }
        #endregion

        #region Binary
        public static Image<Gray, byte> Binary(Image<Gray, byte> inputImage, byte treshold1, byte treshold2)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            if (treshold1 > treshold2)
            {
                byte temp = treshold1;
                treshold1 = treshold2;
                treshold2 = temp;
            }
            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    byte pixelValue = inputImage.Data[y, x, 0];
                    if (pixelValue < treshold1)
                        result.Data[y, x, 0] = 0; 
                    else if (pixelValue >= treshold1 && pixelValue <= treshold2)
                        result.Data[y, x, 0] = 128;
                    else
                        result.Data[y, x, 0] = 255;

                }
            }
            return result;
        }
        #endregion

        #region Crop Image
        public static Image<Gray, byte> Crop(Image<Gray, byte> inputImage, int x1,int y1,int x2,int y2)
        {
            
            
            int width = (x2 - x1);
            int height = (y2 - y1);
            Image<Gray,byte> result=new Image<Gray,byte>(width, height);

            for (int y = 0; y < height;y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Data[y, x, 0]=inputImage.Data[y1+y, x1+x, 0];
                }
            }
                    return result;
        }
        public static Image<Bgr, byte> Crop(Image<Bgr, byte> inputImage, int x1, int y1, int x2, int y2)
        {
            int width = x2 - x1;
            int height = y2 - y1;
            Image<Bgr, byte> result = new Image<Bgr, byte>(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[y1 + y, x1 + x, 0];
                    result.Data[y, x, 1] = inputImage.Data[y1 + y, x1 + x, 1];
                    result.Data[y, x, 2] = inputImage.Data[y1 + y, x1 + x, 2];
                }
            }
            return result;

        }


        #endregion

        #region Mirror Immage
        public static Image<Gray, byte> Mirror(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    int mirroredX = inputImage.Width - x - 1; 
                    result.Data[y, mirroredX, 0] = inputImage.Data[y, x, 0];

                }
            }
            return result;
        }

        public static Image<Bgr, byte> Mirror(Image<Bgr, byte> inputImage)
        {

            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    int mirroredX = inputImage.Width - x - 1;
                    result.Data[y, mirroredX, 0] = inputImage.Data[y, x, 0];
                    result.Data[y, mirroredX, 1] = inputImage.Data[y, x, 1]; 
                    result.Data[y, mirroredX, 2] = inputImage.Data[y, x, 2];

                }
            }
            return result;
        }
        #endregion

        #region Clockwise Image
        public static Image<Gray, byte> Clockwise(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Height, inputImage.Width);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[x, inputImage.Height - 1 - y, 0] = inputImage.Data[y, x, 0];
                }
            }

            return result;
        }
        public static Image<Bgr, byte> Clockwise(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Height, inputImage.Width);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[x, inputImage.Height - 1 - y, 0] = inputImage.Data[y, x, 0];
                    result.Data[x, inputImage.Height - 1 - y, 1] = inputImage.Data[y, x, 1];
                    result.Data[x, inputImage.Height - 1 - y, 2] = inputImage.Data[y, x, 2];
                }
            }

            return result;
        }
        #endregion

        #region Clockwise Image
        public static Image<Gray, byte> AntiClockwise(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Height, inputImage.Width);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[inputImage.Width - 1 - x, y, 0] = inputImage.Data[y, x, 0];
                }
            }

            return result;
        }
        public static Image<Bgr, byte> AntiClockwise(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Height, inputImage.Width);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[inputImage.Width - 1 - x, y, 0] = inputImage.Data[y, x, 0];
                    result.Data[inputImage.Width - 1 - x, y, 1] = inputImage.Data[y, x, 1];
                    result.Data[inputImage.Width - 1 - x, y, 2] = inputImage.Data[y, x, 2];
                }
            }

            return result;
        }
        #endregion
    }
}