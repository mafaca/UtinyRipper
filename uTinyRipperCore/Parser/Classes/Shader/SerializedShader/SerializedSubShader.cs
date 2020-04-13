namespace uTinyRipper.Classes.Shaders
{
	public struct SerializedSubShader : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			Passes = reader.ReadAssetArray<SerializedPass>();
			Tags.Read(reader);
			LOD = reader.ReadInt32();
		}

		public void Export(ShaderWriter writer)
		{
			writer.Write("SubShader ");
			using (writer.IndentBrackets())
			{
				if (LOD != 0)
				{
					writer.WriteLine("LOD {0}", LOD);
				}
				Tags.Export(writer);
				for (int i = 0; i < Passes.Length; i++)
				{
					Passes[i].Export(writer);
				}
			}
		}

		public SerializedPass[] Passes { get; set; }
		public int LOD { get; set; }

		public SerializedTagMap Tags;
	}
}
