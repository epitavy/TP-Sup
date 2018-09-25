using System;
using System.Drawing;

namespace TinyPhotoshop
{
    public static class Basics
    {
        public static Color Grey(Color color)
        {
            int grey_value = (color.R + color.G + color.B)/3;
            return Color.FromArgb(grey_value, grey_value, grey_value);
        }

        public static Color Binarize(Color color)
        {
            if((color.R + color.B + color.G)/382 < 1)
                return Color.Black;
            return Color.White;
        }
        
        public static Color BinarizeColor(Color color)
        {
            int bin_R = color.R < 128 ? 0 : 255;
            int bin_G = color.G < 128 ? 0 : 255;
            int bin_B = color.B < 128 ? 0 : 255;
            return Color.FromArgb(bin_R, bin_G, bin_B);
        }
        
        public static Color Negative(Color color)
        {
            return Color.FromArgb(Math.Abs(255 - color.R), Math.Abs(255 - color.G), Math.Abs(255 - color.B));
        }
        
        public static Color Tinter(Color color, Color tint, int factor)
        {
	        
            int bin_R = (color.R * (100 - factor) + tint.R * factor)/100;
            int bin_G = (color.G * (100 - factor) + tint.G * factor)/100;
            int bin_B = (color.B * (100 - factor) + tint.B * factor)/100;
	        
            return Color.FromArgb(bin_R, bin_G, bin_B);
        }
			
        public static Image Apply(Bitmap img, Func<Color, Color> func)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                    img.SetPixel(i, j, func(img.GetPixel(i,j)));
            }
            return img;
        }
    }
}