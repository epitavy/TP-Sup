using System;
using System.Drawing;

namespace TinyPhotoshop
{
    public static class Convolution
    {
      
        public static float[,] Gauss = {
            {1/9f, 2/9f, 1/9f},
            {2/9f, 4/9f, 2/9f},
            {1/9f, 2/9f, 1/9f}
        };
        
        public static float[,] Sharpen = {
	        {0f, -1f, 0f},
	        {-1f, 5f, -1f},
	        {0f, -1, 0f}
        };
        
        public static float[,] Blur = {
	        {1/16f, 1/8f, 1/16f},
	        {1/8f, 1/4f, 1/8f},
	        {1/16f, 1/8f, 1/16f}
        };
        
        public static float[,] EdgeEnhance = {
	        {0f, 0f, 0f},
	        {-1f, 1f, 0f},
	        {0f, 0f, 0f}
        };
        
        public static float[,] EdgeDetect = {
	        {0f, 1f, 0f},
	        {1f, -4f, 1f},
	        {0f, 1, 0f}
        };
        
        public static float[,] Emboss = {
	        {-2f,-1f, 0f},
	        {-1f,1f, 1f},
	        { 0f, 1f, 2f}
        };
        
        private static int Clamp(float c)
        {
	        if (c > 255)
		        return 255;
	        if (c < 0)
		        return 0;
	        return (int) c;
        }

        private static bool IsValid(int x, int y, Size size)
        {
	        return x >= 0 && y >= 0 && x < size.Width && y < size.Height;
        }
        
        public static Image Convolute(Bitmap img, float[,] mask)
        {
	        int height = img.Height;
	        int width = img.Width;
	        Size size = img.Size;
	        int n = mask.GetLength(0);
	        int n2 = n / 2;
	        Bitmap convoluted = new Bitmap(width, height);

	        for (int i = 0; i < width ; i++)
	        {
		        for (int j = 0; j < height; j++)
		        {
			        //Calcul de la valeur du nouveau pixel
			        float convolute_red = 0;
			        float convolute_green = 0;
			        float convolute_blue = 0;
			        for (int k = 0; k < n; k++)
			        {
				        for (int l = 0; l < n; l++)
				        {
					        if (IsValid(i + k - n2, j + l - n2, size))
					        {
						        Color pix = img.GetPixel(i + k - n2, j + l - n2);
						        float mask_factor = mask[l, k];
						        convolute_red += pix.R * mask_factor ;
						        convolute_green += pix.G * mask_factor;
						        convolute_blue += pix.B * mask_factor;
					        }
					        
				        }
			        }
			        convoluted.SetPixel(i,j, Color.FromArgb(Clamp(convolute_red), Clamp(convolute_green), Clamp(convolute_blue)));
			       }
	        }
			
	        return convoluted;
        }
    }
}