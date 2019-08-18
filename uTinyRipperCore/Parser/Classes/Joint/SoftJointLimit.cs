using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct SoftJointLimit : IAssetReadable, IYAMLExportable
	{

		public void Read(AssetReader reader)
		{
			Limit = reader.ReadSingle();
			Bounciness = reader.ReadSingle();
			ContactDistance = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("m_Limit", Limit);
			node.Add("m_Bounciness", Bounciness);
			node.Add("m_ContactDistance", ContactDistance);
			return node;
		}

		public float Limit { get; private set; }
		public float Bounciness { get; private set; }
		public float ContactDistance { get; private set; }
	}
}
