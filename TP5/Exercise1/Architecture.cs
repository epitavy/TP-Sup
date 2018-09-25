using System.IO;

namespace Exercise1
{
	internal static class Architecture
	{
		/// <summary>
		///   EXERCISE 1.5:
		///   <para />
		///   This function creates a folder architecture as described in the subject.
		///   When testing, take care not to put an important folder in path.
		/// </summary>
		/// <param name="path">the path to the first folder you will create</param>
		public static void Architect(string path)
		{
			if(File.Exists(path))
				File.Delete(path);
			if(Directory.Exists(path))
				Directory.Delete(path, true);
			Directory.CreateDirectory(path);
			File.WriteAllText(Path.Combine(path,"AUTHORS"), "* eliaz.pitavy\n");
			File.WriteAllText(Path.Combine(path,"README"), "Everything in programming is magic... except for the programmer\n");
			Directory.CreateDirectory(Path.Combine(path,"TP5"));
			File.Create(Path.Combine(path,"TP5","useless.txt"));
		}
	}
}