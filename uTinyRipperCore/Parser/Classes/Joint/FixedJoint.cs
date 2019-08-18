using System.Collections.Generic;
using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;
using uTinyRipper.SerializedFiles;
using uTinyRipper.Classes.Joint;

namespace uTinyRipper.Classes
{
	public sealed class FixedJoint : Component
	{
		public FixedJoint(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
			ConnectedBody.Read(reader);

			BreakForce = reader.ReadSingle();
			BreakTorque = reader.ReadSingle();
			EnableCollision = reader.ReadBoolean();
			EnablePreprocessing = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			MassScale = reader.ReadSingle();
			ConnectedMassScale = reader.ReadSingle();
		}

		public override IEnumerable<Object> FetchDependencies(ISerializedFile file, bool isLog = false)
		{
			foreach (Object asset in base.FetchDependencies(file, isLog))
			{
				yield return asset;
			}

			yield return ConnectedBody.FetchDependency(file, isLog, ToLogString, ConnectedBodyName);
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.Add(ConnectedBodyName, ConnectedBody.ExportYAML(container));

			node.Add(BreakForceName, BreakForce);
			node.Add(BreakTorqueName, BreakTorque);
			node.Add(EnableCollisionName, EnableCollision);
			node.Add(EnablePreprocessingName, EnablePreprocessing);
			node.Add(MassScaleName, MassScale);
			node.Add(ConnectedMassScaleName, ConnectedMassScale);
			return node;
		}
		public const string ConnectedBodyName = "m_ConnectedBodyName";

		public const string BreakForceName = "m_BreakForceName";
		public const string BreakTorqueName = "m_BreakTorqueName";
		public const string EnableCollisionName = "m_EnableCollisionName";
		public const string EnablePreprocessingName = "m_EnablePreprocessingName";
		public const string MassScaleName = "m_MassScaleName";
		public const string ConnectedMassScaleName = "m_ConnectedMassScaleName";

		public PPtr<Rigidbody> ConnectedBody;

		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }
	}
}
