using System.Collections.Generic;
using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;
using uTinyRipper.SerializedFiles;
using uTinyRipper.Classes.Joint;
using uTinyRipper.Parser.Classes.Joint;

namespace uTinyRipper.Classes
{
	public sealed class ConfigurableJoint : Component
	{
		public ConfigurableJoint(AssetInfo assetInfo) :
			base(assetInfo)
		{
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

			SecondaryAxis = reader.ReadAsset<Vector3f>();
			XMotion = (ConfigurableJointMotion)reader.ReadInt32();
			YMotion = (ConfigurableJointMotion)reader.ReadInt32();
			ZMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularXMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularYMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularZMotion = (ConfigurableJointMotion)reader.ReadInt32();
			LinearLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			LinearLimit = reader.ReadAsset<SoftJointLimit>();
			AngularXLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			LowAngularXLimit = reader.ReadAsset<SoftJointLimit>();
			HighAngularXLimit = reader.ReadAsset<SoftJointLimit>();
			AngularYZLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			AngularYLimit = reader.ReadAsset<SoftJointLimit>();
			AngularZLimit = reader.ReadAsset<SoftJointLimit>();
			TargetPosition = reader.ReadAsset<Vector3f>();
			TargetVelocity = reader.ReadAsset<Vector3f>();
			XDrive = reader.ReadAsset<JointDrive>();
			YDrive = reader.ReadAsset<JointDrive>();
			ZDrive = reader.ReadAsset<JointDrive>();
			TargetRotation = reader.ReadAsset<Quaternionf>();
			TargetAngularVelocity = reader.ReadAsset<Vector3f>();
			RotationDriveMode = (RotationDriveMode)reader.ReadInt32();
			AngularXDrive = reader.ReadAsset<JointDrive>();
			AngularYZDrive = reader.ReadAsset<JointDrive>();
			SlerpDrive = reader.ReadAsset<JointDrive>();
			ProjectionMode = (JointProjectionMode)reader.ReadInt32();
			ProjectionDistance = reader.ReadSingle();
			ProjectionAngle = reader.ReadSingle();
			ConfiguredInWorldSpace = reader.ReadBoolean();
			SwapBodies = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
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
			node.Add(AnchorName, Anchor.ExportYAML(container));
			node.Add(AutoConfigureConnectedAnchorName, AutoConfigureConnectedAnchor);
			node.Add(ConnectedAnchorName, ConnectedAnchor.ExportYAML(container));

			node.Add(SecondaryAxisName, SecondaryAxis.ExportYAML(container));
			node.Add(XMotionName, (int)XMotion);
			node.Add(YMotionName, (int)YMotion);
			node.Add(ZMotionName, (int)ZMotion);
			node.Add(AngularXMotionName, (int)AngularXMotion);
			node.Add(AngularYMotionName, (int)AngularYMotion);
			node.Add(AngularZMotionName, (int)AngularZMotion);
			node.Add(LinearLimitSpringName, LinearLimitSpring.ExportYAML(container));
			node.Add(LinearLimitName, LinearLimit.ExportYAML(container));
			node.Add(AngularXLimitSpringName, AngularXLimitSpring.ExportYAML(container));
			node.Add(LowAngularXLimitName, LowAngularXLimit.ExportYAML(container));
			node.Add(HighAngularXLimitName, HighAngularXLimit.ExportYAML(container));
			node.Add(AngularYZLimitSpringName, AngularYZLimitSpring.ExportYAML(container));
			node.Add(AngularYLimitName, AngularYLimit.ExportYAML(container));
			node.Add(AngularZLimitName, AngularZLimit.ExportYAML(container));
			node.Add(TargetPositionName, TargetPosition.ExportYAML(container));
			node.Add(TargetVelocityName, TargetVelocity.ExportYAML(container));
			node.Add(XDriveName, XDrive.ExportYAML(container));
			node.Add(YDriveName, YDrive.ExportYAML(container));
			node.Add(ZDriveName, ZDrive.ExportYAML(container));
			node.Add(TargetRotationName, TargetRotation.ExportYAML(container));
			node.Add(TargetAngularVelocityName, TargetAngularVelocity.ExportYAML(container));
			node.Add(RotationDriveModeName, (int)RotationDriveMode);
			node.Add(AngularXDriveName, AngularXDrive.ExportYAML(container));
			node.Add(AngularYZDriveName, AngularYZDrive.ExportYAML(container));
			node.Add(SlerpDriveName, SlerpDrive.ExportYAML(container));
			node.Add(ProjectionModeName, (int)ProjectionMode);
			node.Add(ProjectionDistanceName, ProjectionDistance);
			node.Add(ProjectionAngleName, ProjectionAngle);
			node.Add(ConfiguredInWorldSpaceName, ConfiguredInWorldSpace);
			node.Add(SwapBodiesName, SwapBodies);

			node.Add(BreakForceName, BreakForce);
			node.Add(BreakTorqueName, BreakTorque);
			node.Add(EnableCollisionName, EnableCollision);
			node.Add(EnablePreprocessingName, EnablePreprocessing);
			node.Add(MassScaleName, MassScale);
			node.Add(ConnectedMassScaleName, ConnectedMassScale);
			return node;
		}
		public const string ConnectedBodyName = "m_ConnectedBodyName";
		public const string AnchorName = "m_AnchorName";
		public const string AutoConfigureConnectedAnchorName = "m_AutoConfigureConnectedAnchorName";
		public const string ConnectedAnchorName = "m_ConnectedAnchorName";
		public const string BreakForceName = "m_BreakForceName";
		public const string BreakTorqueName = "m_BreakTorqueName";
		public const string EnableCollisionName = "m_EnableCollisionName";
		public const string EnablePreprocessingName = "m_EnablePreprocessingName";
		public const string MassScaleName = "m_MassScaleName";
		public const string ConnectedMassScaleName = "m_ConnectedMassScaleName";

		public const string SecondaryAxisName = "m_SecondaryAxisName";
		public const string XMotionName = "m_XMotionName";
		public const string YMotionName = "m_YMotionName";
		public const string ZMotionName = "m_ZMotionName";
		public const string AngularXMotionName = "m_AngularXMotionName";
		public const string AngularYMotionName = "m_AngularYMotionName";
		public const string AngularZMotionName = "m_AngularZMotionName";
		public const string LinearLimitSpringName = "m_LinearLimitSpringName";
		public const string LinearLimitName = "m_LinearLimitName";
		public const string AngularXLimitSpringName = "m_AngularXLimitSpringName";
		public const string LowAngularXLimitName = "m_LowAngularXLimitName";
		public const string HighAngularXLimitName = "m_HighAngularXLimitName";
		public const string AngularYZLimitSpringName = "m_AngularYZLimitSpringName";
		public const string AngularYLimitName = "m_AngularYLimitName";
		public const string AngularZLimitName = "m_AngularZLimitName";
		public const string TargetPositionName = "m_TargetPositionName";
		public const string TargetVelocityName = "m_TargetVelocityName";
		public const string XDriveName = "m_XDriveName";
		public const string YDriveName = "m_YDriveName";
		public const string ZDriveName = "m_ZDriveName";
		public const string TargetRotationName = "m_TargetRotationName";
		public const string TargetAngularVelocityName = "m_TargetAngularVelocityName";
		public const string RotationDriveModeName = "m_RotationDriveModeName";
		public const string AngularXDriveName = "m_AngularXDriveName";
		public const string AngularYZDriveName = "m_AngularYZDriveName";
		public const string SlerpDriveName = "m_SlerpDriveName";
		public const string ProjectionModeName = "m_ProjectionModeName";
		public const string ProjectionDistanceName = "m_ProjectionDistanceName";
		public const string ProjectionAngleName = "m_ProjectionAngleName";
		public const string ConfiguredInWorldSpaceName = "m_ConfiguredInWorldSpaceName";
		public const string SwapBodiesName = "m_SwapBodiesName";

		public PPtr<Rigidbody> ConnectedBody;
		public Vector3f Anchor;
		public Vector3f Axis;
		public bool AutoConfigureConnectedAnchor;
		public Vector3f ConnectedAnchor;
		public float BreakForce;
		public float BreakTorque;
		public bool EnableCollision;
		public bool EnablePreprocessing;
		public float MassScale;
		public float ConnectedMassScale;

		public Vector3f SecondaryAxis { get; private set; }
		public ConfigurableJointMotion XMotion { get; private set; }
		public ConfigurableJointMotion YMotion { get; private set; }
		public ConfigurableJointMotion ZMotion { get; private set; }
		public ConfigurableJointMotion AngularXMotion { get; private set; }
		public ConfigurableJointMotion AngularYMotion { get; private set; }
		public ConfigurableJointMotion AngularZMotion { get; private set; }
		public SoftJointLimitSpring LinearLimitSpring { get; private set; }
		public SoftJointLimit LinearLimit { get; private set; }
		public SoftJointLimitSpring AngularXLimitSpring { get; private set; }
		public SoftJointLimit LowAngularXLimit { get; private set; }
		public SoftJointLimit HighAngularXLimit { get; private set; }
		public SoftJointLimitSpring AngularYZLimitSpring { get; private set; }
		public SoftJointLimit AngularYLimit { get; private set; }
		public SoftJointLimit AngularZLimit { get; private set; }
		public Vector3f TargetPosition { get; private set; }
		public Vector3f TargetVelocity { get; private set; }
		public JointDrive XDrive { get; private set; }
		public JointDrive YDrive { get; private set; }
		public JointDrive ZDrive { get; private set; }
		public Quaternionf TargetRotation { get; private set; }
		public Vector3f TargetAngularVelocity { get; private set; }
		public RotationDriveMode RotationDriveMode { get; private set; }
		public JointDrive AngularXDrive { get; private set; }
		public JointDrive AngularYZDrive { get; private set; }
		public JointDrive SlerpDrive { get; private set; }
		public JointProjectionMode ProjectionMode { get; private set; }
		public float ProjectionDistance { get; private set; }
		public float ProjectionAngle { get; private set; }
		public bool ConfiguredInWorldSpace { get; private set; }
		public bool SwapBodies { get; private set; }
	}
}
