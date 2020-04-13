using System.IO;
using uTinyRipper.Classes.Shaders;

namespace uTinyRipper.Converters.Shaders
{
	public class ShaderTextExporter
	{
		public virtual void Export(ShaderWriter writer, ref ShaderSubProgram subProgram)
		{
			byte[] exportData = subProgram.ProgramData;
			using (MemoryStream memStream = new MemoryStream(exportData))
			{
				using (StreamReader reader = new StreamReader(memStream))
				{
					writer.WriteIndentedFull(reader.ReadToEnd().Trim());
				}
			}
		}
	}
}
