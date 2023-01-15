using System;

namespace LabGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            DynamicArray<int> Arr1 = new DynamicArray<int>();
            string[] input = { "hot", "cold" };
            DynamicArray<string> Arr2 = new DynamicArray<string>(input);
            Arr1.Notify += Arr1.DisplayMessage;
            Arr2.Notify += Arr2.DisplayMessage;
            Console.WriteLine(Arr1.IsEmpty ? "Arr1 is empty" : "Arr1 isn't empty");
            Console.WriteLine(Arr2.ToString());
            Arr1.Add(1);
            Arr1.Add(2);
            Arr1.Add(3);
            Arr1.Add(4);
            Console.WriteLine(Arr1.ToString());
            Arr1.Insert(2, 0);
            Console.WriteLine(Arr1.ToString());
            Arr1.Remove(1);
            Arr1.RemoveAt(1);
            Console.WriteLine(Arr1.ToString());
            Console.WriteLine($"Arr1 has {Arr1.Count} elem, Arr2 has {Arr2.Count} elem");
            Arr2.Clear();
            Console.WriteLine(Arr2.ToString());
            Console.WriteLine($"Index of 2 in Arr1 = {Arr1.IndexOf(2)}");
            Arr2.Add("red");
            Arr2.Add("green");
            Console.WriteLine(Arr2.Contains("hot") ? "Arr2 contains hot" : "Arr2 not contains hot");
            Console.WriteLine(Arr2.ToString());
            int[] array = new int[Arr1.Count];
            Arr1.CopyTo(array, 0);
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(array.GetType());
            Arr1[0] = 123;
            for(int i = 0; i < Arr1.Count; i++)
            {
                Console.Write(Arr1[i] + " ");
            }
            Console.WriteLine(Arr1.GetType());
            foreach (int i in Arr1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            
        }
    }
}
