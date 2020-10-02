using uTinyRipper.Converters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes.Joints
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
			node.Add("limit", Limit);
			node.Add("bounciness", Bounciness);
			node.Add("contactDistance", ContactDistance);
			return node;
		}

		public float Limit { get; private set; }
		public float Bounciness { get; private set; }
		public float ContactDistance { get; private set; }
	}
}
