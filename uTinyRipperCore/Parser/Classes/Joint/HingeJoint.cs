using System.Collections.Generic;
//using uTinyRipper.AssetExporters;
using uTinyRipper.Classes;
using uTinyRipper.Converters;
using uTinyRipper.BundleFiles;
using uTinyRipper.YAML;
using uTinyRipper.SerializedFiles;
using uTinyRipper.Classes.Joints;
using uTinyRipper.Classes.PhysicMaterials;

namespace uTinyRipper.Classes
{
	public sealed class HingeJoint : Component
	{
		public HingeJoint(AssetInfo assetInfo) :
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
			Axis = reader.ReadAsset<Vector3f>();
			AutoConfigureConnectedAnchor = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			ConnectedAnchor = reader.ReadAsset<Vector3f>();

			UseSpring = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			Spring = reader.ReadAsset<JointSpring>();
			UseMotor = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			Motor = reader.ReadAsset<JointMotor>();
			UseLimits = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			Limits = reader.ReadAsset<JointLimits>();

			BreakForce = reader.ReadSingle();
			BreakTorque = reader.ReadSingle();
			EnableCollision = reader.ReadBoolean();
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
			node.Add(AxisName, Axis.ExportYAML(container));
			node.Add(AutoConfigureConnectedAnchorName, AutoConfigureConnectedAnchor);
			node.Add(ConnectedAnchorName, ConnectedAnchor.ExportYAML(container));

			node.Add(UseSpringName, UseSpring);
			node.Add(SpringName, Spring.ExportYAML(container));
			node.Add(UseMotorName, UseMotor);
			node.Add(MotorName, Motor.ExportYAML(container));
			node.Add(UseLimitsName, UseLimits);
			node.Add(LimitsName, Limits.ExportYAML(container));

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
		public const string AxisName = "m_Axis";
		public const string AutoConfigureConnectedAnchorName = "m_AutoConfigureConnectedAnchor";
		public const string ConnectedAnchorName = "m_ConnectedAnchor";
		public const string BreakForceName = "m_BreakForce";
		public const string BreakTorqueName = "m_BreakTorque";
		public const string EnableCollisionName = "m_EnableCollision";
		public const string EnablePreprocessingName = "m_EnablePreprocessing";
		public const string MassScaleName = "m_MassScale";
		public const string ConnectedMassScaleName = "m_ConnectedMassScale";

		public const string UseSpringName = "m_UseSpring";
		public const string SpringName = "m_Spring";
		public const string UseMotorName = "m_UseMotor";
		public const string MotorName = "m_Motor";
		public const string UseLimitsName = "m_UseLimits";
		public const string LimitsName = "m_Limits";

		public PPtr<Rigidbody> ConnectedBody { get; private set; }
		public Vector3f Anchor { get; private set; }
		public Vector3f Axis { get; private set; }
		public bool AutoConfigureConnectedAnchor { get; private set; }
		public Vector3f ConnectedAnchor { get; private set; }
		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }

		public bool UseSpring { get; private set; }
		public JointSpring Spring { get; private set; }
		public bool UseMotor { get; private set; }
		public JointMotor Motor { get; private set; }
		public bool UseLimits { get; private set; }
		public JointLimits Limits { get; private set; }
		
	}
}
