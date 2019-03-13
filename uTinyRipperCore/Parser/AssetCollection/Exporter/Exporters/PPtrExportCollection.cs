using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uTinyRipper;
using uTinyRipper.AssetExporters;
using uTinyRipper.AssetExporters.Classes;
using uTinyRipper.Classes;
using uTinyRipper.SerializedFiles;
using Object = uTinyRipper.Classes.Object;

namespace uTinyRipper.AssetExporters
{
    class PPtrExportCollection : AssetExportCollection
    {
        private ProjectExporter m_ProjectExporter;
        public PPtrExportCollection(ProjectExporter projectExporter, IAssetExporter assetExporter, Object asset) : base(assetExporter, asset)
        {
            m_ProjectExporter = projectExporter;
        }
        public override bool Export(ProjectAssetContainer container, string dirPath)
        {
            return false;
        }
        public override ExportPointer CreateExportPointer(Object asset, bool isLocal)
        {
            long exportID = GetExportID(asset);
            return isLocal ?
                new ExportPointer(exportID) :
                new ExportPointer(exportID, Asset.GUID, m_ProjectExporter.ToExportType(Asset.ClassID));
        }
    }
}
