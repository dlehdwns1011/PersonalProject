using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Program {
    private const string DllName = "C++DLL"; // dll 이름

    [DllImport(DllName)]
    public static extern int Add(int a, int b);
    [DllImport(DllName)]
    public static extern int Minus(int a, int b);

    [DllImport(DllName)]
    public static extern bool IsEvenNumber(int number);

    [DllImport(DllName)]
    public static extern void WriteMessage(string message);

    static void Main(string[] args) {
        Console.WriteLine("Add Result : " + Add(1, 2).ToString());

        Console.WriteLine("Minus Result : " + Add(2, 1).ToString());

        Console.WriteLine("IsEvenNumber Result : " + IsEvenNumber(25182).ToString());

        Console.Write("WriteMessage Result : ");
        WriteMessage("Hello!");
    }
}

