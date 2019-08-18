using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct JointMotor : IAssetReadable, IYAMLExportable
	{

		public void Read(AssetReader reader)
		{
			TargetVelocity = reader.ReadSingle();
			Force = reader.ReadSingle();
			FreeSpin = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("targetVelocity", TargetVelocity);
			node.Add("force", Force);
			node.Add("freeSpin", FreeSpin);
			return node;
		}

		public float TargetVelocity { get; private set; }
		public float Force { get; private set; }
		public float FreeSpin { get; private set; }
	}
}
