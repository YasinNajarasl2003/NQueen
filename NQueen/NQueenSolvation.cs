using System.Diagnostics;
using System.Runtime.CompilerServices;

public class NQueenSolvation
{
    public static List<char[,]> SolveNQueen(int n)
    {
        List<char[,]> Maps = new List<char[,]>();
        int[] origin = new int[n];
        for (int i = 0; i < n; i++)
        {
            origin[i] = i;
        }
        // Parallel foreach to speed up the calculations
        Parallel.ForEach(GetPermutations(origin, n), new ParallelOptions { MaxDegreeOfParallelism = 10}, permutation =>
        {
            int[,] map = new int[n, n];
            map = GetMap(permutation.ToArray(), n);
            if (IsSafe(map, n))
            {
                Maps.Add(Translator(map, n));
            }
        });

        //Normal foreach optional to uses
        /*
        foreach (IEnumerable<int> permutation in GetPermutations(origin, n))
        {
            int[,] map = new int[n, n];
            map = GetMap(permutation.ToArray(), n);
            if (IsSafe(map, n))
            {
                Maps.Add(Translator(map, n));
            }
        }
        */

        return Maps;
    }
    private static char[,] Translator(int[,] input, int n)
    {
        char[,] result = new char[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (input[i, j] == 1)
                    result[i, j] = '#';
                else
                    result[i, j] = '*';
            }
        }
        return result;
    }
    private static int[,] GetMap(int[] permutation, int n)
    {
        int[,] map = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                map[i, j] = 0;
            }
        }
        for (int i = 0; i < n; i++)
        {
            map[i, permutation[i]] = 1;
        }
        return map;
    }
    private static bool IsSafe(int[,] Map, int n)
    {
        int rowQueensCount = 0;
        int columnQueensCount = 0;
        int acrossQueensCount = 0;

        // null check
        if (Map == null)
            return false;

        // Row repetition check
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (Map[i, j] == 1)
                    rowQueensCount++;
            }
            if (rowQueensCount > 1)
                return false;
            else
                rowQueensCount = 0;
        }

        // Column repetition check
        for (int j = 0; j < n; j++)
        {
            for (int i = 0; i < n; i++)
            {
                if (Map[i, j] == 1)
                    columnQueensCount++;
            }
            if (columnQueensCount > 1)
                return false;
            else
                columnQueensCount = 0;
        }

        // A cross repetition check

        // Main diameter check
        for (int k = 0; k < n; k++)
        {
            for (int i = k, j = 0; i < n & j < n; i++, j++)
            {
                if (Map[i, j] == 1)
                    acrossQueensCount++;
            }
            if (acrossQueensCount > 1)
                return false;
            else
                acrossQueensCount = 0;
            for (int i = 0, j = k; i < n & j < n; i++, j++)
            {
                if (Map[i, j] == 1)
                    acrossQueensCount++;
            }
            if (acrossQueensCount > 1)
                return false;
            else
                acrossQueensCount = 0;
        }

        // Subdiameter check
        for (int k = 0; k < n; k++)
        {
            for (int p = 0; p < n; p++)
            {
                for (int i = k, j = p; i >= 0 & j < n; i--, j++)
                {
                    if (Map[i, j] == 1)
                        acrossQueensCount++;
                }
                if (acrossQueensCount > 1)
                    return false;
                else
                    acrossQueensCount = 0;
            }
        }

        return true;
    }
    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int n)
    {
        if (n == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, n - 1)
            //.Where(t => t.ElementAt(0).Equals(list.ElementAt(0)))
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}