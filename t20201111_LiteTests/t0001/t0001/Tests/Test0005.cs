using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0005
	{
		public void Test01()
		{
			int[] arr1 = new int[] { 123, 456, 789, 1000 };
			List<int> list1 = new List<int>(arr1.ToArray());
			string[] arr2 = new string[] { "ABC", "DEF", "GHI", "JKL" };
			List<string> list2 = new List<string>(arr2.ToArray());

			ModifyIList(arr1);
			ModifyIList(list1);
			ModifyIList(arr2);
			ModifyIList(list2);

			PrintIList(arr1);
			PrintIList(list1);
			PrintIList(arr2);
			PrintIList(list2);
			//
			// output ...
			//
			// 1000, 456, 789, 123
			// 1000, 456, 789, 123
			// JKL, DEF, GHI, ABC
			// JKL, DEF, GHI, ABC

			{
				IList<int> arr1b = ReverseIList(arr1);
				IList<int> list1b = ReverseIList(list1);
				IList<string> arr2b = ReverseIList(arr2);
				IList<string> list2b = ReverseIList(list2);

				PrintIList(arr1);
				PrintIList(list1);
				PrintIList(arr2);
				PrintIList(list2);
				//
				// output ...
				//
				// 1000, 456, 789, 123
				// 1000, 456, 789, 123
				// JKL, DEF, GHI, ABC
				// JKL, DEF, GHI, ABC

				PrintIList(arr1b);
				PrintIList(list1b);
				PrintIList(arr2b);
				PrintIList(list2b);
				//
				// output ...
				//
				// 123, 789, 456, 1000
				// 123, 789, 456, 1000
				// ABC, GHI, DEF, JKL
				// ABC, GHI, DEF, JKL
			}

			Swap(arr1, 1, 2);
			Swap(list1, 1, 2);
			Swap(arr2, 1, 2);
			Swap(list2, 1, 2);

			PrintIList(arr1);
			PrintIList(list1);
			PrintIList(arr2);
			PrintIList(list2);
			//
			// output ...
			//
			// 1000, 789, 456, 123
			// 1000, 789, 456, 123
			// JKL, GHI, DEF, ABC
			// JKL, GHI, DEF, ABC
		}

		private void ModifyIList<T>(IList<T> list)
		{
			//list.Clear(); // 配列は例外
			//list.Add(XXX); // 配列は例外

			// 最初と最後の要素を入れ替える。
			//
			T tmp = list[0];
			list[0] = list[list.Count - 1];
			list[list.Count - 1] = tmp;
		}

		private void PrintIList<T>(IList<T> list)
		{
			Console.WriteLine(string.Join(", ", list));
		}

		private IList<T> ReverseIList<T>(IList<T> list)
		{
			return list.Reverse().ToArray();
		}

		public static void Swap<T>(IList<T> list, int a, int b)
		{
			T tmp = list[a];
			list[a] = list[b];
			list[b] = tmp;
		}
	}
}
