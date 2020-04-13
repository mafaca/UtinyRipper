using System;
using System.IO;
using System.Text;

namespace uTinyRipper
{

	/// <summary>
	/// A TextWriter that automatically indents text
	/// Additionally it will unify <c>\r</c> , <c>\n</c> and <see cref="TextWriter.NewLine"/>
	/// </summary>
	public class IndentedTextWriter : TextWriter
	{
		public IndentedTextWriter(TextWriter inner, string indent = "\t")
		{
			m_inner = inner ?? throw new ArgumentNullException(nameof(inner));
			m_indent = indent ?? throw new ArgumentNullException(nameof(indent));
			base.NewLine = m_inner.NewLine;
		}

		public IDisposable Indent()
		{
			m_indentLevel++;
			return new DisposableHelper(() => m_indentLevel--);
		}
		
		public IDisposable DisableIndent(bool indentCurrent = false)
		{
			if (indentCurrent)
			{
				MaybeWriteIndent();
			}

			m_disableIndent++;
			return new DisposableHelper(() => m_disableIndent--);
		}

		public void MaybeWriteIndent()
		{
			if (m_indentPending)
			{
				m_indentPending = false;

				if (m_disableIndent == 0)
				{
					for (int i = 0; i < m_indentLevel; i++)
					{
						Write(m_indent);
					}
				}
			}
		}

		public override void WriteLine()
		{
			m_inner.WriteLine();
			m_indentPending = true;
		}

		public override void Write(char value)
		{
			if (CoreNewLine[m_newLineState] == value)
			{
				m_newLineState++;
			}
			else if (m_newLineState != 0)
			{
				if (CoreNewLine[m_newLineState - 1] == '\r' || CoreNewLine[m_newLineState - 1] == '\n')
				{
					m_inner.Write(CoreNewLine, 0, m_newLineState - 1);
					WriteLine();
				}
				else
				{
					m_inner.Write(CoreNewLine, 0, m_newLineState);
				}

				m_newLineState = 0;
			}

			if (m_newLineState == CoreNewLine.Length)
			{
				WriteLine();
				m_newLineState = 0;
			}
			else if (m_newLineState == 0) 
			{
				if (value == '\r' || value == '\n')
				{
					WriteLine();
				}
				else
				{
					MaybeWriteIndent();
					m_inner.Write(value);
				}
			}
		}
		
		public override Encoding Encoding => m_inner.Encoding;

		public override IFormatProvider FormatProvider => m_inner.FormatProvider;
		
		protected override void Dispose(bool disposing)
		{
			m_inner.Dispose();
			base.Dispose(disposing);
		}

		private readonly TextWriter m_inner;
		private readonly string m_indent;
		private int m_disableIndent;
		private int m_indentLevel;
		private int m_newLineState;
		private bool m_indentPending;
	}
}

