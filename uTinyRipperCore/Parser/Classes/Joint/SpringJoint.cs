using System.Collections.Generic;
using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;
using uTinyRipper.SerializedFiles;
using uTinyRipper.Classes.Joint;

namespace uTinyRipper.Classes
{
	public sealed class SpringJoint : Component
	{
		public SpringJoint(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		/// <summary>
		/// 2017.0.0 and greater
		/// </summary>
		private static bool IsReadMassScale(Version version)
		{
			return version.IsGreaterEqual(2017, 0);
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
			ConnectedBody.Read(reader);
			Anchor = reader.ReadAsset<Vector3f>();
			AutoConfigureConnectedAnchor = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			ConnectedAnchor = reader.ReadAsset<Vector3f>();

			Spring = reader.ReadSingle();
			Damper = reader.ReadSingle();
			MinDistance = reader.ReadSingle();
			MaxDistance = reader.ReadSingle();
			Tolerance = reader.ReadSingle();

			BreakForce = reader.ReadSingle();
			BreakTorque = reader.ReadSingle();
			EnableCollision = reader.ReadBoolean();
			EnablePreprocessing = reader.ReadBoolean();
			if (IsReadMassScale(reader.Version))
			{
				reader.AlignStream(AlignType.Align4);
				MassScale = reader.ReadSingle();
				ConnectedMassScale = reader.ReadSingle();
			}
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
			node.Add(AnchorName, Anchor.ExportYAML(container));
			node.Add(AutoConfigureConnectedAnchorName, AutoConfigureConnectedAnchor);
			node.Add(ConnectedAnchorName, ConnectedAnchor.ExportYAML(container));
			node.AddSerializedVersion(2);

			node.Add(SpringName, Spring);
			node.Add(DamperName, Damper);
			node.Add(MinDistanceName, MinDistance);
			node.Add(MaxDistanceName, MaxDistance);
			node.Add(ToleranceName, Tolerance);

			node.Add(BreakForceName, BreakForce);
			node.Add(BreakTorqueName, BreakTorque);
			node.Add(EnableCollisionName, EnableCollision);
			node.Add(EnablePreprocessingName, EnablePreprocessing);
			node.Add(MassScaleName, MassScale);
			node.Add(ConnectedMassScaleName, ConnectedMassScale);
			return node;
		}
		public const string ConnectedBodyName = "m_ConnectedBody";
		public const string AnchorName = "m_Anchor";
		public const string AutoConfigureConnectedAnchorName = "m_AutoConfigureConnectedAnchor";
		public const string ConnectedAnchorName = "m_ConnectedAnchor";
		public const string BreakForceName = "m_BreakForce";
		public const string BreakTorqueName = "m_BreakTorque";
		public const string EnableCollisionName = "m_EnableCollision";
		public const string EnablePreprocessingName = "m_EnablePreprocessing";
		public const string MassScaleName = "m_MassScale";
		public const string ConnectedMassScaleName = "m_ConnectedMassScale";

		public const string SpringName = "m_Spring";
		public const string DamperName = "m_Damper";
		public const string MinDistanceName = "m_MinDistance";
		public const string MaxDistanceName = "m_MaxDistance";
		public const string ToleranceName = "m_Tolerance";


		public PPtr<Rigidbody> ConnectedBody { get; private set; }
		public Vector3f Anchor { get; private set; }
		public bool AutoConfigureConnectedAnchor { get; private set; }
		public Vector3f ConnectedAnchor { get; private set; }
		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }

		public float Spring { get; private set; }
		public float Damper { get; private set; }
		public float MinDistance { get; private set; }
		public float MaxDistance { get; private set; }
		public float Tolerance { get; private set; }
	}
}
