using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TinyPhotoshop
{
    public static class Auto
    {

        public static Dictionary<char, int[]> GetHistogram(Bitmap img)
        {
            Dictionary<char, int[]> hist = new Dictionary<char, int[]> 
                                           { { 'R', new int[256] },
                                             { 'G', new int[256] },
                                             { 'B', new int[256] } };
	        
	        int height = img.Height;
	        int width = img.Width;

	        for (int i = 0; i < width; i++)
	        {
		        for (int j = 0; j < height; j++)
		        {
			        Color components = img.GetPixel(i, j);
			        hist['R'][components.R]++;
			        hist['G'][components.G]++;
			        hist['B'][components.B]++;
		        }
	        }

	        return hist;
        }

		public static int FindLow(int[] hist)
		{
			for (int i = 0; i < hist.Length; i++)
			{
				if (hist[i] != 0)
					return i;
			}
			return 0;
		}

		public static int FindHigh(int[] hist)
        {
	        for (int i = hist.Length - 1; i >= 0; i--)
	        {
		        if (hist[i] != 0)
			        return i;
	        }
	        return 255;
		}

		public static Dictionary<char, int>
        FindBound(Dictionary<char, int[]> hist, Func<int[], int> f)
        {
			Dictionary<char, int> bound = new Dictionary<char, int>();

	        foreach (char key in hist.Keys)
		        bound.Add(key, f(hist[key]));
	        return bound;
        }


		public static int[] ComputeLUT(int low, int high)
        {
			int[] LUT = new int[256];

	        for (int i = 0; i < 256; i++)
	        {
		        if (i < low)
			        LUT[i] = 0;
		        else if (i > high)
			        LUT[i] = 255;
		        else
			        LUT[i] = 255 * (i - low) / (high - low);
	        }

	        return LUT;
        }

		public static Dictionary<char, int[]> GetLUT(Bitmap img)
        {
			Dictionary<char, int[]> LUT = new Dictionary<char, int[]>();

			Dictionary<char, int[]> hist = GetHistogram(img);

	        Dictionary<char, int> low_bounds = FindBound(hist, FindLow);
	        Dictionary<char, int> high_bounds = FindBound(hist, FindHigh);

	        //On est sur que low_bounds et high_bounds ont les memes cles
	        foreach (char key in low_bounds.Keys)
		        LUT.Add(key, ComputeLUT(low_bounds[key], high_bounds[key]));
	        
			return LUT;
		}

        public static Image ConstrastStretch(Bitmap img)
        {
			Dictionary<char, int[]> LUT = GetLUT(img);

	        int height = img.Height;
	        int width = img.Width;

	        for (int i = 0; i < width; i++)
	        {
		        for (int j = 0; j < height; j++)
		        {
			        Color components = img.GetPixel(i, j);
			        int red = LUT['R'][components.R];
			        int green = LUT['G'][components.G];
			        int blue = LUT['B'][components.B];
			        img.SetPixel(i,j, Color.FromArgb(red,green,blue));
		        }
	        }

			return img;
		}

    }
}
