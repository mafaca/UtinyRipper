using System.Collections.Generic;
using System.IO;

namespace uTinyRipper.Converters.Script
{
	// TODO: add constructor support
	public abstract class ScriptExportMethod
	{
		public abstract void Init(IScriptExportManager manager);

		public void Export(CodeWriter writer)
		{
			string returnTypeName = ReturnType.GetTypeNestedName(DeclaringType);
			writer.Write("{0} override {1} {2}(", Keyword, returnTypeName, Name);
			for (int i = 0; i < Parameters.Count; i++)
			{
				ScriptExportParameter parameter = Parameters[i];
				parameter.Export(writer);
				if (i < Parameters.Count - 1)
				{
					writer.Write(", ");
				}
			}
			writer.WriteLine(")");
			using (writer.IndentBrackets())
			{
				foreach (ScriptExportParameter parameter in Parameters)
				{
					if (parameter.ByRef == ScriptExportParameter.ByRefType.Out)
					{
						writer.WriteLine("{0} = default({1});", parameter.Name, parameter.Type.GetTypeNestedName(DeclaringType));
					}
				}
				if (ReturnType.TypeName != MonoUtils.CVoidName)
				{
					writer.WriteLine("return default({0});", returnTypeName);
				}
			}
		}

		public void GetUsedNamespaces(ICollection<string> namespaces)
		{
			ReturnType.GetTypeNamespaces(namespaces);
			foreach (ScriptExportParameter parameter in Parameters)
			{
				parameter.GetUsedNamespaces(namespaces);
			}
		}

		public override string ToString()
		{
			if (Name == null)
			{
				return base.ToString();
			}
			else
			{
				return Name;
			}
		}

		public abstract string Name { get; }

		public abstract ScriptExportType DeclaringType { get; }
		public abstract ScriptExportType ReturnType { get; }
		public abstract IReadOnlyList<ScriptExportParameter> Parameters { get; }

		protected abstract string Keyword { get; }

		protected const string PublicKeyWord = "public";
		protected const string InternalKeyWord = "internal";
		protected const string ProtectedKeyWord = "protected";
		protected const string PrivateKeyWord = "private";
	}
}
