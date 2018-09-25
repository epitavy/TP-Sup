using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using OpenGL;

namespace Genetics
{
    public class Matrix
    {
        #region Attributes

        private int Height;
        private int Width;
        private const float mutation = 0.5f;
        public float[,] Tab;
        public float[] Bias;
        private static readonly Random Rdn = new Random();

        #endregion

        #region Constructors

        public Matrix(int height, int width, bool init = false)
        {
            Height = height;
            Width = width;
            
            Tab = new float[Height, Width];
            if (init)
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                        Tab[i, j] = (float) Rdn.NextDouble();
                }
            }

            Bias = new float[Width];
            for (int i = 0; i < Width; i++)
                Bias[i] = (float) (Rdn.NextDouble() / 2) - 1;
        }

        public Matrix(List<float> tab)
        {
            Height = 1;
            Width = tab.Count;
            Tab = new float[Height, Width];
            for (int j = 0; j < Width; j++)
            {
                Tab[0, j] = tab[j];
            }
        }

        #endregion

        public void MakeCopyFrom(Matrix copy)
        {
            Height = copy.Height;
            Width = copy.Width;
            Tab = new float[Height, Width];
            
            for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
                Tab[i, j] = copy.Tab[i, j];
            
            Bias = new float[Width];
            for (int i = 0; i < Width; i++)
                Bias[i] = copy.Bias[i];
        }


        public void ApplyMutation()
        {
            //Matrice mutation
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i, j] += (float) (Rdn.NextDouble() / (1 / (2* mutation)) - mutation);
                    Tab[i,j] = Sigmoid(Tab[i, j]);
                }
            }
            
                    
            
            
            //Bias mutation
            for (int i = 0; i < Width; i++)
            {
                Bias[i] += (float) (Rdn.NextDouble() / (1 / (2* mutation)) - mutation);
            }
            
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Width != b.Width)
                throw new ArgumentOutOfRangeException("Addition : matrix width are different");
            Matrix m = new Matrix(a.Height, a.Width);
            
            for (int i = 0; i < a.Height; i++)
            for (int j = 0; j < a.Width; j++)
                m.Tab[i, j] = a.Tab[i, j] + b.Tab[i, j];
            
            for (int i = 0; i < a.Width; i++)
                m.Bias[i] = a.Bias[i] + b.Bias[i];

            return m;
        }

        public  static float Sigmoid(float x)
        {
            return (float) (1 / (1 + Math.Exp(-x)));
        }

        /// <summary>
        /// This is not only a multiplication ! We also normalize !
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Normalized multiplication</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            int widthA = a.Width;
            if (widthA != b.Height)
                throw new ArgumentOutOfRangeException("operator < * > : matrix width are different");

            float[,] tabA = a.Tab;
            float[,] tabB = b.Tab;
            
            Matrix C = new Matrix(a.Height, b.Width);

            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    float s = 0;
                    for (int k = 0; k < widthA; k++)
                        s = s + tabA[i, k] * tabB[k, j];

                    C.Tab[i, j] = Sigmoid(s / b.Width + b.Bias[j]);
                }
            }

            return C;
        }


        public void Print()
        { 
            throw new NotImplementedException();
        }
    }
}