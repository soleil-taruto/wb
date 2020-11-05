using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(@"C:\Dev\wb\t20201024\Claes20200001\Claes20200001\Program.cs", Encoding.UTF8));

			Console.WriteLine(tree.GetText());

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		public void Test02()
		{
			SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(@"C:\Dev\wb\t20201024\Claes20200001\Claes20200001\Program.cs", Encoding.UTF8));

			Test02a(tree.GetRoot());

			foreach (SyntaxNode node in tree.GetRoot().ChildNodes())
			{
				Console.WriteLine("node: " + node.GetText());
			}
			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		private void Test02a(SyntaxNode root)
		{
			Console.WriteLine("NODE >");

			if (root.ChildNodes().Count() == 0)
			{
				Console.WriteLine(root.GetText());
			}
			else
			{
				foreach (SyntaxNode node in root.ChildNodes())
				{
					Test02a(node);
				}
			}
			Console.WriteLine("< NODE");
		}
	}
}
