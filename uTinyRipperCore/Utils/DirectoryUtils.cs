using System;
using System.IO;
using System.Runtime.InteropServices;

namespace uTinyRipper
{
	public static class DirectoryUtils
	{
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

		public static string ToLongPath(string path, bool force)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				if (RunetimeUtils.IsRunningOnNetCore)
				{
					return path;
				}
				if (path.StartsWith(LongPathPrefix, StringComparison.Ordinal))
				{
					return path;
				}

				string fullPath = FileUtils.GetFullPath(path);
				if (force || fullPath.Length >= MaxDirectoryLength)
				{
					return $"{LongPathPrefix}{fullPath}";
				}
			}
			return path;
		}
		
		public static bool IsPathRooted(string path, out int prefixLength)
		{
			if (path.StartsWith(LongPathPrefix, StringComparison.Ordinal))
			{
				// TODO: prefix lengths for things other than drives
				prefixLength = 7;
				return true;
			}

			if (path.StartsWith("/", StringComparison.Ordinal))
			{
				prefixLength = 1;
				return true;
			}

			if (path.Length > 3 && char.IsLetter(path[0]) && path[1] == ':' && path[2] == '\\')
			{
				prefixLength = 3;
				return true;
			}

			prefixLength = 0;
			return false;
		}

		public const string LongPathPrefix = @"\\?\";
		public const int MaxDirectoryLength = 248;
	}
}
