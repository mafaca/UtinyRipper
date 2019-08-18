using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joint
{
	public struct JointLimits : IAssetReadable, IYAMLExportable
	{

		public void Read(AssetReader reader)
		{
			Min = reader.ReadSingle();
			Max = reader.ReadSingle();
			Bounciness = reader.ReadSingle();
			BounceMinVelocity = reader.ReadSingle();
			ContactDistance = reader.ReadSingle();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("min", Min);
			node.Add("max", Max);
			node.Add("bounciness", Bounciness);
			node.Add("bounceMinVelocity", BounceMinVelocity);
			node.Add("contactDistance", ContactDistance);
			return node;
		}

		public float Min { get; private set; }
		public float Max { get; private set; }
		public float Bounciness { get; private set; }
		public float BounceMinVelocity { get; private set; }
		public float ContactDistance { get; private set; }
	}
}
