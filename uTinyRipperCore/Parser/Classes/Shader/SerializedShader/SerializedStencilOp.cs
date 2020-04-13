using System.IO;

namespace uTinyRipper.Classes.Shaders
{
	public struct SerializedStencilOp : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			Pass.Read(reader);
			Fail.Read(reader);
			ZFail.Read(reader);
			Comp.Read(reader);
		}

		public void Export(ShaderWriter writer, StencilType type)
		{
			writer.WriteLine("Comp{0} {1}", type.ToSuffixString(), CompValue);
			writer.WriteLine("Pass{0} {1}", type.ToSuffixString(), PassValue);
			writer.WriteLine("Fail{0} {1}", type.ToSuffixString(), FailValue);
			writer.WriteLine("ZFail{0} {1}", type.ToSuffixString(), ZFailValue);
		}

		public bool IsDefault => PassValue.IsKeep() && FailValue.IsKeep() && ZFailValue.IsKeep() && CompValue.IsAlways();

		public SerializedShaderFloatValue Pass;
		public SerializedShaderFloatValue Fail;
		public SerializedShaderFloatValue ZFail;
		public SerializedShaderFloatValue Comp;

		private StencilOp PassValue => (StencilOp)Pass.Val;
		private StencilOp FailValue => (StencilOp)Fail.Val;
		private StencilOp ZFailValue => (StencilOp)ZFail.Val;
		private StencilComp CompValue => (StencilComp)Comp.Val;
	}
}
