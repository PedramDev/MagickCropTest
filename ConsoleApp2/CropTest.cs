﻿using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class CropTest
    {

        private class MagicStruct
        {
            public MagicStruct(FileInfo file,
                               string filePath,
                               int? quality,
                               int? width,
                               int? height,
                               int? cropX = null,
                               int? cropY = null,
                               int? cropWidth = null,
                               int? cropHeight = null)
            {
                File = file;
                FilePath = filePath;
                Quality = quality;
                Width = width;
                Height = height;
                CropDetailX = cropX;
                CropDetailY = cropY;
                CropDetailHeight = cropHeight;
                CropDetailWidth = cropWidth;
            }

            public FileInfo File { get; }
            public string FilePath { get; }
            public int? Quality { get; }
            public int? Width { get; }
            public int? Height { get; }


            public int? CropDetailX { get; }
            public int? CropDetailY { get; }
            public int? CropDetailHeight { get; }
            public int? CropDetailWidth { get; }
        }




        private static string Src = "";
        private static string DestNoExt = "";
        private static int Numb = 0;
        private static string Ext = "";

        private static string Bg = "";

        public static void Build()
        {
            var baseDir = @"C:\Users\Pedram\Desktop\magick\src\";
            var foolThere = @"1\";
            var filenameL = "cropped-";
            Ext = ".png";
            Src = baseDir + foolThere + "picture.jpg";// filenameL + Numb + filenameR;
            Bg = baseDir + foolThere + "bg.png";

            DestNoExt = baseDir + foolThere + filenameL + Numb + "-dest-";
        }


        public static void Test4()
        {
            byte red = 0;
            byte green = 0;
            byte blue = 0;
            byte alpha = 0;



            var t = 4;

            //int CropDetailX = 889;
            //int CropDetailY = -58;
            //int CropDetailWidth = 1259;
            //int CropDetailHeight = 256;


            int CropDetailX = -58;
            int CropDetailY = -58;
            int CropDetailWidth = 1367;
            int CropDetailHeight = 256;

            int cx = CropDetailX;
            int cy = CropDetailY;
            if (CropDetailX < 0)
            {
                cx = CropDetailX * -1;
            }

            if (CropDetailY < 0)
            {
                cy = CropDetailY * -1;
            }


            var second = new MagickImage(Src);
            var sh = second.Height;
            var sw = second.Width;

            var bw = sw + cx;
            var bh = sh + cy;

            // Add the first image
            var bgImage = new MagickImage(MagickColor.FromRgba(red, green, blue, alpha), bw, bh);

            int appendMainImage_YPosition;
            if (CropDetailY < 0)
            {
                appendMainImage_YPosition = cy;
            }
            else
            {
                appendMainImage_YPosition = 0;
            }


            int appendMainImage_XPosition;
            if (CropDetailX < 0)
            {
                appendMainImage_XPosition = cx;
            }
            else
            {
                appendMainImage_XPosition = 0;
            }

            bgImage.Composite(second, appendMainImage_XPosition, appendMainImage_YPosition, CompositeOperator.Over);



            var newXPosition = CropDetailX;
            var newYPosition = CropDetailY;

            if (CropDetailY < 0)
            {
                newYPosition = 0;
            }
            if (CropDetailX < 0)
            {
                newXPosition = 0;
            }

            // Save the result
            MagickGeometry geometry = new(newXPosition, newYPosition, CropDetailWidth, CropDetailHeight);
            bgImage.Crop(geometry);
            bgImage.RePage();
            bgImage.Write(DestNoExt + t + Ext);

        }

        public static void Test3(int t)
        {
            using (var images = new MagickImageCollection())
            {
                int x = 889;
                int y = -58;
                int width = 1259;
                int height = 256;

                int positiveX = x;
                int positiveH = y;
                if (x < 0)
                {
                    positiveX = x * -1;
                }

                if (y < 0)
                {
                    positiveH = y * -1;
                }


                var second = new MagickImage(Src);
                var baseHeight = second.Height;
                var baseWidth = second.Width;

                var totalWidth = baseWidth + positiveX;
                var totalHeight = baseHeight + positiveH;

                // Add the first image
                var first = new MagickImage(MagickColor.FromRgba(0, 0, 0, 0), totalWidth, totalHeight);
                images.Add(first);


                // Add the second image
                images.Add(second);

                // Create a mosaic from both images
                using (var result = images.Mosaic())
                {
                    result.Write(DestNoExt + t + "zert" + Ext);

                    var newXPosition = x;
                    var newYPosition = y;

                    if (y < 0)
                    {
                        newYPosition = 0;
                    }
                    if (x < 0)
                    {
                        newXPosition = 0;
                    }

                    // Save the result
                    MagickGeometry geometry = new(newXPosition, newYPosition, width, height);
                    result.Crop(geometry);
                    result.RePage();
                    result.Write(DestNoExt + t + Ext);
                }
            }
        }

        public static void Test2(int t)
        {
            using var image = new MagickImage(Src);
            image.Transparent(MagickColor.FromRgb(0, 0, 0));
            image.FilterType = FilterType.Quadratic;
            MagickGeometry geometry = new(889, -57, 1280, 256);
            image.Crop(geometry);
            image.RePage();
            image.Write(DestNoExt + t + Ext);
        }

        public static void Test1(int t)
        {
            using var image = new MagickImage(Src);
            MagickGeometry geometry = new(889, -57, 1280, 256);
            image.Crop(geometry);
            image.Write(DestNoExt + t + Ext);
        }
    }
}
