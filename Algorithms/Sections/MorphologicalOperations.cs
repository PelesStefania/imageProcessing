using Emgu.CV.Structure;
using Emgu.CV;

namespace Algorithms.Sections
{
    public class MorphologicalOperations
    {
        #region Closing
        #region Dilation

        public static Image<Gray, byte> Dilation(Image<Gray, byte> input, int kernelSize)
        {
            int radius = kernelSize / 2;
            int width = input.Width;
            int height = input.Height;

            Image<Gray, byte> output = new Image<Gray, byte>(width, height);

            byte[,,] inputData = input.Data;
            byte[,,] outputData = output.Data;

            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        byte max = 0;

            //        for (int dy = -radius; dy <= radius; dy++)
            //        {
            //            for (int dx = -radius; dx <= radius; dx++)
            //            {
            //                int neighbourY = y + dy;
            //                int neighbourX = x + dx;

            //                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
            //                {
            //                    byte val = inputData[neighbourY, neighbourX, 0];
            //                    if (val > max)
            //                        max = val;
            //                }
            //            }
            //        }

            //        outputData[y, x, 0] = max;
            //    }
            //}
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool found = false;

                    for (int dy = -radius; dy <= radius; dy++)
                    {
                        for (int dx = -radius; dx <= radius; dx++)
                        {
                            int neighbourY = y + dy;
                            int neighbourX = x + dx;

                            if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                            {
                                byte val = inputData[neighbourY, neighbourX, 0];
                                if (val == 255) {
                                    found = true;
                                }

                            }
                        }
                    }
                    if (found == true)
                        outputData[y, x, 0] = 255; 
                    else
                        outputData[y, x, 0] = 0;

                }
            }

            return output;
        }

        #endregion

        #region Erosion

        public static Image<Gray, byte> Erosion(Image<Gray, byte> input, int kernelSize)
        {
            int radius = kernelSize / 2;
            int width = input.Width;
            int height = input.Height;

            Image<Gray, byte> output = new Image<Gray, byte>(width, height);

            byte[,,] inputData = input.Data;
            byte[,,] outputData = output.Data;

            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        byte min = 255;

            //        for (int dy = -radius; dy <= radius; dy++)
            //        {
            //            for (int dx = -radius; dx <= radius; dx++)
            //            {
            //                int neighbourY = y + dy;
            //                int neighbourX = x + dx;

            //                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
            //                {
            //                    byte val = inputData[neighbourY, neighbourX, 0];
            //                    if (val < min)
            //                        min = val;
            //                }
            //            }
            //        }

            //        outputData[y, x, 0] = min;
            //    }
            //}

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool found = false;

                    for (int dy = -radius; dy <= radius; dy++)
                    {
                        for (int dx = -radius; dx <= radius; dx++)
                        {
                            int neighbourY = y + dy;
                            int neighbourX = x + dx;

                            if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                            {
                                byte val = inputData[neighbourY, neighbourX, 0];
                                if (val == 0)
                                {
                                    found = true;
                                }

                            }
                        }
                    }
                    if (found == true)
                        outputData[y, x, 0] = 0;
                    else
                        outputData[y, x, 0] = 255;

                }
            }

            return output;
        }

        #endregion
        #endregion
    }
}
