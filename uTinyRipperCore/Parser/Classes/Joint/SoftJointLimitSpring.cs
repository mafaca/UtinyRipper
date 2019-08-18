using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct SoftJointLimitSpring : IAssetReadable, IYAMLExportable
	{

		public void Read(AssetReader reader)
		{
			Spring = reader.ReadSingle();
			Damper = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("m_Spring", Spring);
			node.Add("m_Damper", Damper);
			return node;
		}

		public float Spring { get; private set; }
		public float Damper { get; private set; }
	}
}
