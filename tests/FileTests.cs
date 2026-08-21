using System;
using System.Console;

class FileTests
{
    public static void TestSum()
    {
        int result = Sum(2, 3);
        if (result == 5)
            WriteLine("Test passed");
        else
            WriteLine("Test failed");
    }

    public static void Main()
    {
        TestSum();
    }

    static int Sum(int a, int b)
    {
        return a + b;
    }
}