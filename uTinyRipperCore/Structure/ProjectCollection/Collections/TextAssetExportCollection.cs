using System.IO;
using uTinyRipper.Classes;
using uTinyRipper.Converters;

namespace uTinyRipper.Project
{
	public sealed class TextAssetExportCollection : AssetExportCollection
	{

		public static bool KeepExistingExtension { get; set; } = true;

		public TextAssetExportCollection(IAssetExporter assetExporter, TextAsset asset) :
			base(assetExporter, asset)
		{
		}

		protected override string GetExportExtension(Object asset)
		{
			//if the asset already has an extension then don't add the default extension
			return KeepExistingExtension && asset is TextAsset textAsset && !string.IsNullOrEmpty(Path.GetExtension(textAsset.Name)) ? string.Empty : "bytes";
		}
	}
}
