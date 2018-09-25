using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise1
{
	internal static class CopyFile
	{
		/// <summary>
		///   EXERCISE 1.3:
		///   <para />
		///   Read the source file and put its content into destination file
		/// </summary>
		/// <param name="source">the path to the source file</param>
		/// <param name="destination">the path to the destination file</param>
		public static void CopyAllFile(string source, string destination)
		{
			if(File.Exists(source))
				File.Copy(source, destination, true);
			else
				Console.WriteLine("Could not copy file : File at "+ source +" does not exist");
		}

		/// <summary>
		///   EXERCISE 1.4:
		///   <para />
		///   Read the source file and put half of its content into destination file
		/// </summary>
		/// <param name="source">the path to the source file</param>
		/// <param name="destination">the path to the destination file</param>
		public static void CopyHalfFile(string source, string destination)
		{
			if (File.Exists(source))
			{
				string[] contents = File.ReadAllLines(source);
				for(int i = 0; i<contents.Length/2; i++)
					File.AppendAllText(destination, contents[i]+"\n");
			}
			else
				Console.WriteLine("Could not copy file : File at "+ source +" does not exist");
		}
	}
}