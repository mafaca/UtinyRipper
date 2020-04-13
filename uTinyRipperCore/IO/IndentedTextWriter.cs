using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace uTinyRipper
{

	/// <summary>
	/// A TextWriter that automatically indents text
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
				MaybeIndent();
			}

			m_disableIndent++;
			return new DisposableHelper(() => m_disableIndent--);
		}
		
		private void MaybeIndent()
		{
			if (m_indentPending)
			{
				m_indentPending = false;
				if (m_disableIndent == 0)
				{
					for (int i = 0; i < m_indentLevel; i++)
					{
						m_inner.Write(m_indent);
					}
				}
			}
		}

		private async Task MaybeIndentAsync()
		{
			if (m_indentPending)
			{
				m_indentPending = false;
				if (m_disableIndent == 0)
				{
					for (int i = 0; i < m_indentLevel; i++)
					{
						await m_inner.WriteAsync(m_indent);
					}
				}
			}
		}

		public override void Flush()
		{
			m_inner.Flush();
		}

		public override async Task FlushAsync()
		{
			await m_inner.FlushAsync();
		}

		public override void Write(bool value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(char value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(char[] buffer)
		{
			MaybeIndent();
			m_inner.Write(buffer);
		}

		public override void Write(char[] buffer, int index, int count)
		{
			MaybeIndent();
			m_inner.Write(buffer, index, count);
		}

		public override void Write(decimal value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(double value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(int value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(long value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(object value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(float value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(string value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(string format, object arg0)
		{
			MaybeIndent();
			m_inner.Write(format, arg0);
		}

		public override void Write(string format, object arg0, object arg1)
		{
			MaybeIndent();
			m_inner.Write(format, arg0, arg1);
		}

		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			MaybeIndent();
			m_inner.Write(format, arg0, arg1, arg2);
		}

		public override void Write(string format, params object[] arg)
		{
			MaybeIndent();
			m_inner.Write(format, arg);
		}

		public override void Write(uint value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override void Write(ulong value)
		{
			MaybeIndent();
			m_inner.Write(value);
		}

		public override async Task WriteAsync(char value)
		{
			await MaybeIndentAsync();
			await m_inner.WriteAsync(value);
		}

		public override async Task WriteAsync(char[] buffer, int index, int count)
		{
			await MaybeIndentAsync();
			await m_inner.WriteAsync(buffer, index, count);
		}

		public override async Task WriteAsync(string value)
		{
			await MaybeIndentAsync();
			await m_inner.WriteAsync(value);
		}

		public override void WriteLine()
		{
			m_inner.WriteLine();
			m_indentPending = true;
		}

		public override void WriteLine(bool value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(char value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(char[] buffer)
		{
			MaybeIndent();
			m_inner.WriteLine(buffer);
			m_indentPending = true;
		}

		public override void WriteLine(char[] buffer, int index, int count)
		{
			MaybeIndent();
			m_inner.WriteLine(buffer, index, count);
			m_indentPending = true;
		}

		public override void WriteLine(decimal value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(double value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(int value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(long value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(object value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(float value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(string value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(string format, object arg0)
		{
			MaybeIndent();
			m_inner.WriteLine(format, arg0);
			m_indentPending = true;
		}

		public override void WriteLine(string format, object arg0, object arg1)
		{
			MaybeIndent();
			m_inner.WriteLine(format, arg0, arg1);
			m_indentPending = true;
		}

		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			MaybeIndent();
			m_inner.WriteLine(format, arg0, arg1, arg2);
			m_indentPending = true;
		}

		public override void WriteLine(string format, params object[] arg)
		{
			MaybeIndent();
			m_inner.WriteLine(format, arg);
			m_indentPending = true;
		}

		public override void WriteLine(uint value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override void WriteLine(ulong value)
		{
			MaybeIndent();
			m_inner.WriteLine(value);
			m_indentPending = true;
		}

		public override async Task WriteLineAsync()
		{
			await m_inner.WriteLineAsync();
			m_indentPending = true;
		}

		public override async Task WriteLineAsync(char value)
		{
			await MaybeIndentAsync();
			await m_inner.WriteLineAsync(value);
			m_indentPending = true;
		}

		public override async Task WriteLineAsync(char[] buffer, int index, int count)
		{
			await MaybeIndentAsync();
			await m_inner.WriteLineAsync(buffer, index, count);
			m_indentPending = true;
		}

		public override async Task WriteLineAsync(string value)
		{
			await MaybeIndentAsync();
			await base.WriteLineAsync(value);
			m_indentPending = true;
		}

		public void WriteIndentedFull(string value)
		{
			foreach (char c in value)
			{
				WriteCharWithState(c);
			}
		}

		public void WriteCharWithState(char value)
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
					MaybeIndent();
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

