using System.Diagnostics;
while (true)
{
    try
    {
        Console.Write("Hi there please enter the amount of dimensions and queens(zero to quit): ");

        int n = int.Parse(Console.ReadLine());
        if (n == 0)
            break;
        Stopwatch calculationWatch = Stopwatch.StartNew();

        List<char[,]> Maps = NQueenSolvation.SolveNQueen(n);
        calculationWatch.Stop();
        Stopwatch printWatch = Stopwatch.StartNew();
        foreach (var Map in Maps)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(Map[i, j] + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------------------");
        }
        printWatch.Stop();
        Console.WriteLine($"All posible ways: {Maps.Count}");
        Console.WriteLine($"Calculation Time: {calculationWatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Print Time: {printWatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Hole Time: {printWatch.ElapsedMilliseconds + calculationWatch.ElapsedMilliseconds}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message.ToString());
    }
}