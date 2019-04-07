using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace uTinyRipper
{
	public static class DirectoryUtils
	{
        private static readonly Dictionary<string, int> _fileIndexCache = new Dictionary<string, int>();

		public static bool Exists(string path)
		{
			return Directory.Exists(ToLongPath(path));
		}

		public static DirectoryInfo CreateDirectory(string path)
		{
			return Directory.CreateDirectory(ToLongPath(path));
		}
		
		public static void CreateVirtualDirectory(string path)
		{
#if !VIRTUAL
			CreateDirectory(path);
#endif
		}

		public static void Delete(string path)
		{
			Directory.Delete(ToLongPath(path, true));
		}

		public static void Delete(string path, bool recursive)
		{
			Directory.Delete(ToLongPath(path, true), recursive);
		}

		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(ToLongPath(path));
		}

		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(ToLongPath(path), searchPattern);
		}

		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOptions)
		{
			return Directory.GetFiles(ToLongPath(path), searchPattern, searchOptions);
		}

		public static DirectoryInfo GetParent(string path)
		{
			return Directory.GetParent(ToLongPath(path));
		}

		public static string ToLongPath(string path)
		{
			return ToLongPath(path, false);
		}

		private static string ToLongPath(string path, bool force)
		{
			if (path.StartsWith(LongPathPrefix, StringComparison.Ordinal))
			{
				return path;
			}

			string fullPath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path);
			if (force || fullPath.Length >= MaxDirectoryLength)
			{
				return $"{LongPathPrefix}{fullPath}";
			}
			return path;
		}

        public static string GetMaxIndexName(string dirPath, string fileName)
        {
            if (!Directory.Exists(dirPath))
            {
                return fileName;
            }

            if (fileName.Length > 245)
            {
                fileName = fileName.Substring(0, 245);
            }

            var longDirPath = ToLongPath(dirPath);
            var filePathBase = Path.Combine(longDirPath, fileName);
            if (!_fileIndexCache.TryGetValue(filePathBase, out int counter))
            {
                counter = 0;
            }

            var resultFileName = fileName;
            while (Directory.EnumerateFiles(longDirPath, resultFileName + ".*", SearchOption.TopDirectoryOnly).Any())
            {
                resultFileName = fileName + "_" + counter++;
            }

            _fileIndexCache[filePathBase] = counter;
            return resultFileName;
        }

        public const string LongPathPrefix = @"\\?\";
		public const int MaxDirectoryLength = 248;
	}
}
