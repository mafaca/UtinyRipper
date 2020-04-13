using System.IO;

namespace uTinyRipper
{
	public class CodeWriter : IndentedTextWriter
	{
		public CodeWriter(TextWriter inner, string indent = "\t") : base(inner, indent)
		{
		}
	}
}