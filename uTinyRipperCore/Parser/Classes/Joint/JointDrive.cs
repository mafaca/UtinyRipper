using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct JointDrive : IAssetReadable, IYAMLExportable
	{

		public void Read(AssetReader reader)
		{
			PositionSpring = reader.ReadSingle();
			PositionDamper = reader.ReadSingle();
			MaximumForce = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("m_Limit", PositionSpring);
			node.Add("m_Bounciness", PositionDamper);
			node.Add("m_ContactDistance", MaximumForce);
			return node;
		}

		public float PositionSpring { get; private set; }
		public float PositionDamper { get; private set; }
		public float MaximumForce { get; private set; }
	}
}
