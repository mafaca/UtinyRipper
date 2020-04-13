using System.IO;

namespace uTinyRipper.Classes.Shaders
{
	public struct SerializedProperties : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			Props = reader.ReadAssetArray<SerializedProperty>();
		}

		public void Export(ShaderWriter writer)
		{
			writer.Write("Properties ");
			using (writer.IndentBrackets())
			{
				foreach (SerializedProperty prop in Props)
				{
					prop.Export(writer);
				}
			}
		}

		public SerializedProperty[] Props { get; set; }
	}
}
