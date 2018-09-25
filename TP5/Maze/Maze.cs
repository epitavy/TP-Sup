using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;

namespace Maze
{
	using System.IO;
	internal static class Maze
	{
		/// <summary>
		/// MAZE:
		/// <para/>
		/// This is where you should call your functions to make your program work.
		/// </summary>
		/// <param name="args">unused</param>
		public static void Main(string[] args)
		{
			string inputPath = "../../../tests/map5.maze";// AskMazeFile();
			string outputPath = GetOutputFile(inputPath);
			Console.WriteLine("Chemin d'acces du labyrinthe : {0}\n- Chemin de sortie du labyrinthe resolu : {1}",inputPath, outputPath);
			char[][] maze = ParseFile(inputPath);
			
			//Print2Dtab(maze);
			PrintMaze(maze);
			Thread.Sleep(1000);
			SolveMaze(maze, FindStart(maze));
			//Print2Dtab(maze);
			PrintMaze(maze);
			SaveSolution(maze, outputPath);
		}
		
		
		//Affiche un tableau 2D de char
		public static void Print2Dtab(char[][] grid)
		{
			foreach (var chartab in grid)
			{
				Console.WriteLine();
				foreach (var c in chartab)
					Console.Write(c);
			}
		}
		
		
		public static string AskMazeFile()
		{
			while(true)
			{
				Console.WriteLine("Please enter the path of maze's file.");
				string path = Console.ReadLine();
				
				if (File.Exists(path))
				{
					if (Path.GetExtension(path) == ".maze")
						return path;
					Console.WriteLine("File is not a .maze file.");
				}
					
				else
					Console.WriteLine("File does not exist.");
			}
		}

		public static string GetOutputFile(string fileIn)
		{
			return Path.ChangeExtension(fileIn, ".solved");
		}

		
		public static char[][] ParseFile(string validPath)
		{
			string[] lines = File.ReadAllLines(validPath);
			char[][] maze = new char [lines.Length][];

			for (int i = 0; i < lines.Length; i++)
			{
				maze[i] = new char[lines[i].Length];
				for (int j = 0; j < lines[0].Length; j++)
				{
					maze[i][j] = lines[i][j];
				}
			}
			return maze;
		}

		public static Point FindStart(char[][] grid)
		{
			for (int i = 0; i < grid.Length; i++)
			{
				char[] chartab = grid[i];
				for (var j = 0; j < chartab.Length; j++)
				{
					if (chartab[j] == 'S')
						return new Point(j,i);
				}
			}
			return null;//Could never occure
		}
		
		//Fonction d'appel de l'IA
		public static void SolveMaze(char[][] grid, Point p)
		{
			//On créer le tableau 2D processed rempli de 0
			int[][] processed = new int[grid.Length][];
			for (int i = 0; i < grid.Length; i++)
			{
				processed[i] = new int [grid[i].Length];
				for (int j = 0; j < grid[i].Length; j++)
					processed[i][j] = 0;
			}
			Console.WriteLine(SolveMazeBacktracking(grid, processed, p)
				? "The solve was successfull!"
				: "The maze is unsolvable.");
		}
		
		
		//IA backtracking
		public static bool SolveMazeBacktracking(char[][] grid, int[][] processed, Point p)
		{
			if(p.X >= grid[0].Length || p.Y >= grid.Length || p.X < 0 || p.Y < 0)
				return false;
			char c = grid[p.Y][p.X];

			if (c == 'B' || processed[p.Y][p.X] == 1)
				return false;
			if (c == 'F')
				return true;
			processed[p.Y][p.X] = 1;

			
			if (SolveMazeBacktracking(grid, processed, new Point(p.X, p.Y + 1)))
			{
				grid[p.Y][p.X] = 'P';
				return true;
			}
			if (SolveMazeBacktracking(grid, processed, new Point(p.X + 1, p.Y)))
			{
				grid[p.Y][p.X] = 'P';
				return true;
			}
			if (SolveMazeBacktracking(grid, processed, new Point(p.X, p.Y - 1)))
			{
				grid[p.Y][p.X] = 'P';
				return true;
			}
			if (SolveMazeBacktracking(grid, processed, new Point(p.X - 1, p.Y)))
			{
				grid[p.Y][p.X] = 'P';
				
				return true;
			}
			
			return false;
		}

		public static void SaveSolution(char[][] grid, string fileOut)
		{
			string[] grid_lines = new string[grid.Length];
			for (int i = 0; i < grid.Length; i++)
			{
				foreach (char c in grid[i])
				{
					grid_lines[i] += c;
				}
			}
			
			File.AppendAllLines(fileOut, grid_lines);
		}
		
		/*                                                                
		88888888ba                                                     
		88      "8b                                                    
		88      ,8P                                                    
		88aaaaaa8P'   ,adPPYba,   8b,dPPYba,   88       88  ,adPPYba,  
		88""""""8b,  a8"     "8a  88P'   `"8a  88       88  I8[    ""  
		88      `8b  8b       d8  88       88  88       88   `"Y8ba,   
		88      a8P  "8a,   ,a8"  88       88  "8a,   ,a88  aa    ]8I  
		88888888P"    `"YbbdP"'   88       88   `"YbbdP'Y8  `"YbbdP"' 
		*/
		
		
		public static void PrintMaze(char[][] maze)
		{
			Console.WriteLine();
			foreach (char[] line in maze)
			{
				foreach (char c in line)
				{
					switch (c)
					{
						case 'P':
							Console.BackgroundColor = ConsoleColor.DarkRed;
							break;
						case 'S':
							Console.BackgroundColor = ConsoleColor.Red;
							break;
						case 'O':
							Console.BackgroundColor = ConsoleColor.White;
							break;
						case 'B':
							Console.BackgroundColor = ConsoleColor.DarkBlue;
							break;
						case 'F':
							Console.BackgroundColor = ConsoleColor.Yellow;
							break;
						default:
							Console.BackgroundColor = ConsoleColor.White;
							break;
					}
					Console.Write("  ");
				}
				Console.ResetColor();
				Console.WriteLine();
			}
			Console.ResetColor();
		}

		public static void SolveMazeBFS(char[][] grid, Point start)
		{
			Queue<Point> save = new Queue<Point>();
			save.Enqueue(start);
			bool found = false;

			while (save.Count != 0 && !found)
			{
				Point current = save.Dequeue();

				if (grid[current.Y][current.X] == 'F')
					found = true;
				
				Point east = new Point(current.X + 1, current.Y, current);
				Point south = new Point(current.X , current.Y - 1, current);
				Point west = new Point(current.X - 1, current.Y, current);
				Point north = new Point(current.X, current.Y + 1, current);
				
				if(IsValid(east, grid))
					save.Enqueue(east);
				if(IsValid(north, grid))
					save.Enqueue(north);
				if(IsValid(south, grid))
					save.Enqueue(south);
				if(IsValid(west, grid))
					save.Enqueue(west);
			}
			
			
			
		}

		public static bool IsValid(Point p, char[][] grid)
		{
			return p.X >= 0 && p.Y >= 0 && p.X < grid[0].Length && p.Y < grid.Length && grid[p.Y][p.X] != 'B' && grid[p.Y][p.X] != 'P';
		}
		
	}

	/// <summary>
	///   Class that allows to store 2D coordinates.
	/// </summary>
	internal class Point
	{
		public Point(int x, int y, Point antecedent = null)
		{
			X = x;
			Y = y;
			Last = antecedent;
		}

		public int Y { get; set; }
		public int X { get; set; }
		public Point Last { get; set; }
	}
}