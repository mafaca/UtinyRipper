﻿using System.Collections.Generic;
using uTinyRipper.AssetExporters;
using uTinyRipper.YAML;
using uTinyRipper.SerializedFiles;
using uTinyRipper.Classes.Joint;

namespace uTinyRipper.Classes
{
	public sealed class CharacterJoint : Component
	{
		public CharacterJoint(AssetInfo assetInfo) :
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
			SwingAxis = reader.ReadAsset<Vector3f>();
			TwistLimitSpring = reader.ReadAsset< SoftJointLimitSpring>();
			LowTwistLimit = reader.ReadAsset<SoftJointLimit>();
			HighTwistLimit = reader.ReadAsset< SoftJointLimit>();
			SwingLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			Swing1Limit = reader.ReadAsset<SoftJointLimit>();
			Swing2Limit = reader.ReadAsset< SoftJointLimit>();
			EnableProjection = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
			ProjectionDistance = reader.ReadSingle();
			ProjectionAngle = reader.ReadSingle();
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
			node.Add(SwingAxisName, SwingAxis.ExportYAML(container));
			node.Add(TwistLimitSpringName, TwistLimitSpring.ExportYAML(container));
			node.Add(LowTwistLimitName, LowTwistLimit.ExportYAML(container));
			node.Add(HighTwistLimitName, HighTwistLimit.ExportYAML(container));
			node.Add(Swing1LimitName, Swing1Limit.ExportYAML(container));
			node.Add(Swing2LimitName, Swing2Limit.ExportYAML(container));
			node.Add(ProjectionDistanceName, ProjectionDistance);
			node.Add(ProjectionAngleName, ProjectionAngle);
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

		public const string SwingAxisName = "m_SwingAxisName";
		public const string TwistLimitSpringName = "m_TwistLimitSpringName";
		public const string LowTwistLimitName = "m_LowTwistLimitName";
		public const string HighTwistLimitName = "m_HighTwistLimitName";
		public const string Swing1LimitName = "m_Swing1LimitName";
		public const string Swing2LimitName = "m_Swing2LimitName";
		public const string EnableProjectionName = "m_EnableProjectionName";
		public const string ProjectionDistanceName = "m_ProjectionDistanceName";
		public const string ProjectionAngleName = "m_ProjectionAngleName";

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

		public Vector3f SwingAxis { get; private set; }
		public SoftJointLimitSpring TwistLimitSpring { get; private set; }
		public SoftJointLimit LowTwistLimit { get; private set; }
		public SoftJointLimit HighTwistLimit { get; private set; }
		public SoftJointLimitSpring SwingLimitSpring { get; private set; }
		public SoftJointLimit Swing1Limit { get; private set; }
		public SoftJointLimit Swing2Limit { get; private set; }
		public bool EnableProjection { get; private set; }
		public float ProjectionDistance { get; private set; }
		public float ProjectionAngle { get; private set; }


	}
}