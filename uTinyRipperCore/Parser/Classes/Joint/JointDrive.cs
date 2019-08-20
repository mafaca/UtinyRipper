using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct JointDrive : IAssetReadable, IYAMLExportable
	{

		private static int GetSerializedVersion(Version version)
		{
			// TODO:
			return 2;
		}

		public void Read(AssetReader reader)
		{
			PositionSpring = reader.ReadSingle();
			PositionDamper = reader.ReadSingle();
			MaximumForce = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.AddSerializedVersion(GetSerializedVersion(container.ExportVersion));
			node.Add("positionSpring", PositionSpring);
			node.Add("positionDamper", PositionDamper);
			node.Add("maximumForce", MaximumForce);
			return node;
		}

		public float PositionSpring { get; private set; }
		public float PositionDamper { get; private set; }
		public float MaximumForce { get; private set; }
	}
}
