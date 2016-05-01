using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Sudoku_grabber
{
    public class SudokuDetector
    {
        Image<Gray, byte> originalImage;

        public void SetGrayImage(Image<Gray, byte> image)
        {
            originalImage = image.Clone();
        }


        private void FindOuterGridByFloorFill(Mat image)
        {
            Point maxPt = new Point();
            List<Point> points = new List<Point>();
            Rectangle rect = new Rectangle();
            { // find the maximum connected component
                int maxArea = 0;
                for (int y = 0; y < image.Size.Height; y++)
                {
                    for (int x = 0; x < image.Size.Width; x++)
                    {
                        byte pixel = Marshal.ReadByte(image.DataPointer + y * image.Size.Width + x);
                        if (pixel > 0)
                        {
                            points.Add(new Point(x, y));
                            int i = CvInvoke.FloodFill(image, new Mat(), new Point(x, y), new MCvScalar(128), out rect, new MCvScalar(), new MCvScalar());
                            if (i > maxArea && Math.Abs(rect.Width - rect.Height) / Math.Max(rect.Width, rect.Height) < 0.3)
                            {
                                maxArea = i;
                                maxPt = new Point(x, y);
                            }
                        }
                    }
                }
                CvInvoke.FloodFill(image, new Mat(), maxPt, new MCvScalar(255), out rect, new MCvScalar(), new MCvScalar());

                foreach (Point p in points)
                {
                    byte pixel = Marshal.ReadByte(image.DataPointer + p.Y * image.Size.Width + p.X);
                    if (pixel == 128)
                    {
                        int i = CvInvoke.FloodFill(image, new Mat(), p, new MCvScalar(0), out rect, new MCvScalar(), new MCvScalar());
                    }
                }
            }
        }

        public void GridDetection()
        {
            // convert to gray-scaler image
            Mat image = originalImage.Mat.Clone();

            // blur the image
            CvInvoke.GaussianBlur(image, image, new Size(11, 11), 0);

            // threshold the image
            CvInvoke.AdaptiveThreshold(image, image, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, 5, 2);
            CvInvoke.BitwiseNot(image, image);
            Mat kernel = new Mat(new Size(3, 3), DepthType.Cv8U, 1);
            Marshal.Copy(new byte[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 }, 0, kernel.DataPointer, 9);
            CvInvoke.Dilate(image, image, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            FindOuterGridByFloorFill(image);
            CvInvoke.Erode(image, image, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            ImageShowCase.ShowImage(image, "biggest blob");
            VectorOfPointF lines = new VectorOfPointF();
            CvInvoke.HoughLines(image, lines, 1, Math.PI / 180, 200);


            // merging lines
            PointF[] linesArray = lines.ToArray();
            //MergeLines(linesArray, image);
            lines = RemoveUnusedLine(linesArray);

            Mat harrisResponse = new Mat(image.Size, DepthType.Cv8U, 1);
            CvInvoke.CornerHarris(image, harrisResponse, 5);

            DrawLines(lines.ToArray(), image);
            ImageShowCase.ShowImage(image, "corners");
        }

        private VectorOfPointF RemoveUnusedLine(PointF[] linesArray)
        {
            VectorOfPointF lines = new VectorOfPointF();
            for (int i = 0; i < linesArray.Length; i++)
            {
                if (linesArray[i].X != 0 || linesArray[i].Y != -100)
                    lines.Push(new PointF[] { linesArray[i] });
            }
            return lines;
        }

        private void DrawLines(PointF[] lines, Mat image)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Y != 0)
                {
                    double m = -1 / Math.Tan(line.Y);

                    double c = line.X / Math.Sin(line.Y);

                    CvInvoke.Line(image, new Point(0, (int)c), new Point(image.Size.Width, (int)(m * image.Size.Width + c)), new MCvScalar(128));
                }
                else
                {
                    CvInvoke.Line(image, new Point((int)line.X, 0), new Point((int)line.X, image.Size.Height), new MCvScalar(255));
                }
            }
        }

        private void MergeLines(PointF[] lines, Mat image)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].X == 0 && lines[i].Y == -100)
                    continue;
                float p1 = lines[i].X;
                float theta1 = lines[i].Y;

                PointF pt1current = new PointF(), pt2current = new PointF();
                if (theta1 > Math.PI * 45 / 180 && theta1 < Math.PI * 135 / 180)
                {
                    pt1current.X = 0;

                    pt1current.Y = (float)(p1 / Math.Sin(theta1));

                    pt2current.X = image.Size.Width;
                    pt2current.Y = (float)(-pt2current.X / Math.Tan(theta1) + p1 / Math.Sin(theta1));
                }
                else
                {
                    pt1current.Y = 0;

                    pt1current.X = (float)(p1 / Math.Cos(theta1));

                    pt2current.Y = image.Size.Height;
                    pt2current.X = (float)(-pt2current.Y / Math.Tan(theta1) + p1 / Math.Cos(theta1));

                }

                for (int j = 0; j < lines.Length; j++)
                {
                    if (i == j)
                        continue;
                    if (Math.Abs(lines[j].X - lines[i].X) < 20 && Math.Abs(lines[j].Y - lines[i].Y) < Math.PI * 10 / 180)
                    {
                        float p = (float)lines[j].X;
                        float theta = (float)lines[j].Y;
                        PointF pt1 = new PointF(), pt2 = new PointF();
                        if (lines[j].Y > Math.PI * 45 / 180 && lines[j].Y < Math.PI * 135 / 180)
                        {
                            pt1.X = 0;
                            pt1.Y = (float)(p / Math.Sin(theta));
                            pt2.X = image.Size.Width;
                            pt2.Y = (float)(-pt2.X / Math.Tan(theta) + p / Math.Sin(theta));
                        }
                        else
                        {
                            pt1.Y = 0;
                            pt1.X = (float)(p / Math.Cos(theta));
                            pt2.Y = image.Size.Height;
                            pt2.X = (float)(-pt2.Y / Math.Tan(theta) + p / Math.Cos(theta));
                        }

                        if (((double)(pt1.X - pt1current.X) * (pt1.X - pt1current.X) + (pt1.Y - pt1current.Y) * (pt1.Y - pt1current.Y) < 128 * 128)
                            && ((double)(pt2.X - pt2current.X) * (pt2.X - pt2current.X) + (pt2.Y - pt2current.Y) * (pt2.Y - pt2current.Y) < 128 * 128))
                        {
                            // Merge the two
                            lines[i].X = (lines[i].X + lines[j].X) / 2;
                            lines[i].Y = (lines[i].Y + lines[j].Y) / 2;
                            lines[j].X = 0;
                            lines[j].Y = -100;
                        }
                    }
                }

            }
        }
    }
}
