using uTinyRipper.Converters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joints
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
			node.Add("spring", Spring);
			node.Add("damper", Damper);
			return node;
		}

		public float Spring { get; private set; }
		public float Damper { get; private set; }
	}
}
