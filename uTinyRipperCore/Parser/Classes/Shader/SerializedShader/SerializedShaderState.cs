using System.Globalization;
using System.IO;

namespace uTinyRipper.Classes.Shaders
{
	public struct SerializedShaderState : IAssetReadable
	{
		/// <summary>
		/// 2017.2 and greater
		/// </summary>
		public static bool HasZClip(Version version) => version.IsGreaterEqual(2017, 2);

		public void Read(AssetReader reader)
		{
			Name = reader.ReadString();
			RtBlend0.Read(reader);
			RtBlend1.Read(reader);
			RtBlend2.Read(reader);
			RtBlend3.Read(reader);
			RtBlend4.Read(reader);
			RtBlend5.Read(reader);
			RtBlend6.Read(reader);
			RtBlend7.Read(reader);
			RtSeparateBlend = reader.ReadBoolean();
			reader.AlignStream();

			if (HasZClip(reader.Version))
			{
				ZClip.Read(reader);
			}
			ZTest.Read(reader);
			ZWrite.Read(reader);
			Culling.Read(reader);
			OffsetFactor.Read(reader);
			OffsetUnits.Read(reader);
			AlphaToMask.Read(reader);
			StencilOp.Read(reader);
			StencilOpFront.Read(reader);
			StencilOpBack.Read(reader);
			StencilReadMask.Read(reader);
			StencilWriteMask.Read(reader);
			StencilRef.Read(reader);
			FogStart.Read(reader);
			FogEnd.Read(reader);
			FogDensity.Read(reader);
			FogColor.Read(reader);

			FogMode = (FogMode)reader.ReadInt32();
			GpuProgramID = reader.ReadInt32();
			Tags.Read(reader);
			LOD = reader.ReadInt32();
			Lighting = reader.ReadBoolean();
			reader.AlignStream();
		}

		public void Export(ShaderWriter writer)
		{
			if (Name != string.Empty)
			{
				writer.WriteLine("Name \"{0}\"", Name);
			}
			if (LOD != 0)
			{
				writer.WriteLine("LOD {0}", LOD);
			}
			Tags.Export(writer);
			
			RtBlend0.Export(writer, RtSeparateBlend ? 0 : -1);
			RtBlend1.Export(writer, 1);
			RtBlend2.Export(writer, 2);
			RtBlend3.Export(writer, 3);
			RtBlend4.Export(writer, 4);
			RtBlend5.Export(writer, 5);
			RtBlend6.Export(writer, 6);
			RtBlend7.Export(writer, 7);

			if (AlphaToMaskValue)
			{
				writer.WriteLine("AlphaToMask On");
			}

			if (!ZClipValue.IsOn())
			{
				writer.WriteLine("ZClip {0}", ZClipValue);
			}
			if (!ZTestValue.IsLEqual() && !ZTestValue.IsNone())
			{
				writer.WriteLine("ZTest {0}", ZTestValue);
			}
			if (!ZWriteValue.IsOn())
			{
				writer.WriteLine("ZWrite {0}", ZWriteValue);
			}
			if (!CullingValue.IsBack())
			{
				writer.WriteLine("Cull {0}", CullingValue);
			}
			if (!OffsetFactor.IsZero || !OffsetUnits.IsZero)
			{
				writer.WriteLine("Offset {0}, {1}", OffsetFactor.Val, OffsetUnits.Val);
			}

			if (!StencilRef.IsZero || !StencilReadMask.IsMax || !StencilWriteMask.IsMax || !StencilOp.IsDefault || !StencilOpFront.IsDefault || !StencilOpBack.IsDefault)
			{
				writer.Write("Stencil ");
				using (writer.IndentBrackets())
				{
					if (!StencilRef.IsZero)
					{
						writer.WriteLine("Ref {0}", StencilRef.Val);
					}
					if (!StencilReadMask.IsMax)
					{
						writer.WriteLine("ReadMask {0}", StencilReadMask.Val);
					}
					if (!StencilWriteMask.IsMax)
					{
						writer.WriteLine("WriteMask {0}", StencilWriteMask.Val);
					}
					if (!StencilOp.IsDefault)
					{
						StencilOp.Export(writer, StencilType.Base);
					}
					if (!StencilOpFront.IsDefault)
					{
						StencilOpFront.Export(writer, StencilType.Front);
					}
					if (!StencilOpBack.IsDefault)
					{
						StencilOpBack.Export(writer, StencilType.Back);
					}
				}
			}
			
			if(!FogMode.IsUnknown() || !FogColor.IsZero || !FogDensity.IsZero || !FogStart.IsZero || !FogEnd.IsZero)
			{
				writer.Write("Fog ");
				using (writer.IndentBrackets())
				{
					if (!FogMode.IsUnknown())
					{
						writer.WriteLine("Mode {0}", FogMode);
					}
					if (!FogColor.IsZero)
					{
						writer.WriteLine("Color ({0},{1},{2},{3})",
							FogColor.X.Val.ToString(CultureInfo.InvariantCulture),
							FogColor.Y.Val.ToString(CultureInfo.InvariantCulture),
							FogColor.Z.Val.ToString(CultureInfo.InvariantCulture),
							FogColor.W.Val.ToString(CultureInfo.InvariantCulture));
					}
					if (!FogDensity.IsZero)
					{
						writer.WriteLine("Density {0}", FogDensity.Val.ToString(CultureInfo.InvariantCulture));
					}
					if (!FogStart.IsZero || !FogEnd.IsZero)
					{
						writer.WriteLine("Range {0}, {1}",
							FogStart.Val.ToString(CultureInfo.InvariantCulture),
							FogEnd.Val.ToString(CultureInfo.InvariantCulture));
					}
				}
			}

			if(Lighting)
			{
				writer.WriteLine("Lighting {0}", LightingValue);
			}
			writer.WriteLine("GpuProgramID {0}", GpuProgramID);
		}

		public string Name { get; set; }
		public bool RtSeparateBlend { get; set; }
		public FogMode FogMode { get; set; }
		public int GpuProgramID { get; set; }
		public int LOD { get; set; }
		public bool Lighting { get; set; }

		private ZClip ZClipValue => (ZClip)ZClip.Val;
		private ZTest ZTestValue => (ZTest)ZTest.Val;
		private ZWrite ZWriteValue => (ZWrite)ZWrite.Val;
		private Cull CullingValue => (Cull)Culling.Val;
		private bool AlphaToMaskValue => AlphaToMask.Val > 0;
		private string LightingValue => Lighting ? "On" : "Off";

		public SerializedShaderRTBlendState RtBlend0;
		public SerializedShaderRTBlendState RtBlend1;
		public SerializedShaderRTBlendState RtBlend2;
		public SerializedShaderRTBlendState RtBlend3;
		public SerializedShaderRTBlendState RtBlend4;
		public SerializedShaderRTBlendState RtBlend5;
		public SerializedShaderRTBlendState RtBlend6;
		public SerializedShaderRTBlendState RtBlend7;
		public SerializedShaderFloatValue ZClip;
		public SerializedShaderFloatValue ZTest;
		public SerializedShaderFloatValue ZWrite;
		public SerializedShaderFloatValue Culling;
		public SerializedShaderFloatValue OffsetFactor;
		public SerializedShaderFloatValue OffsetUnits;
		public SerializedShaderFloatValue AlphaToMask;
		public SerializedStencilOp StencilOp;
		public SerializedStencilOp StencilOpFront;
		public SerializedStencilOp StencilOpBack;
		public SerializedShaderFloatValue StencilReadMask;
		public SerializedShaderFloatValue StencilWriteMask;
		public SerializedShaderFloatValue StencilRef;
		public SerializedShaderFloatValue FogStart;
		public SerializedShaderFloatValue FogEnd;
		public SerializedShaderFloatValue FogDensity;
		public SerializedShaderVectorValue FogColor;
		public SerializedTagMap Tags;
	}
}
