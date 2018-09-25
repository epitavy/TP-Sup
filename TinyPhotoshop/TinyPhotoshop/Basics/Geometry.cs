using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net.Mime;
using System.Security.Policy;


namespace TinyPhotoshop
{
    public static class Geometry
    {
        public static Image Resize(Bitmap img, int x, int y)
        {
            int height = img.Height;
            int width = img.Width;
            Bitmap resized = new Bitmap(x, y);

            

            return resized;
        }

        public static Image Shift(Bitmap img, int x, int y)
        {
            Bitmap shifted = new Bitmap(img);
            int height = img.Height;
            int width = img.Width;

            x %= width;
            y %= height;
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    
                    shifted.SetPixel((i + x + width) % width, (j + y + height) % height, img.GetPixel(i, j));
            }

            return shifted;
        }

        public static Image SymmetryHorizontal(Bitmap img)
        {
            Bitmap horizontal_sym = new Bitmap(img);
            int height = img.Height;
            int width = img.Width;
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    horizontal_sym.SetPixel(i, height - j - 1, img.GetPixel(i, j));
                }
            }
            return horizontal_sym;
        }

        public static Image SymmetryVertical(Bitmap img)
        {
            Bitmap vertical_sym = new Bitmap(img);
            int height = img.Height;
            int width = img.Width;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    vertical_sym.SetPixel(width - i - 1, j, img.GetPixel(i, j));
                }
            }

            return vertical_sym;
        }

        public static Image SymmetryPoint(Bitmap img, int x, int y)
        {
            return SymmetryHorizontal((Bitmap)SymmetryVertical((Bitmap)Shift(img, x, y)));
        }

        public static Image RotationLeft(Bitmap img)
        {
            int height = img.Height;
            int width = img.Width;
            Bitmap rotation_left = new Bitmap(height, width);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    rotation_left.SetPixel(j, width - i - 1, img.GetPixel(i, j));
                }
            }

            return rotation_left;
        }

        public static Image RotationRight(Bitmap img)
        {
            int height = img.Height;
            int width = img.Width;
            Bitmap rotation_right = new Bitmap(height, width);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    rotation_right.SetPixel(height - j - 1, i, img.GetPixel(i, j));
                }
            }

            return rotation_right;
        }
    }
}