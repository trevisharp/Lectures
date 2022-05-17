using System.Diagnostics;

int testes = 100;
Stopwatch sw = new Stopwatch();
Random rand = new Random(DateTime.Now.Millisecond);

List<int[]> vetores = new List<int[]>();

for (int n = 500; n < 20000; n += 500)
{
    for (int k = 0; k < testes; k++)
        vetores.Add(createarray(n));
}

for (int n = 1; n < 20; n++)
{
    sw.Reset();
    sw.Start();
    for (int i = 0; i < testes; i++)
        solution4(vetores[testes * (n - 1) + i]);
    sw.Stop();
    Console.WriteLine($"n = {500 * n}: {sw.ElapsedMilliseconds / 1000.0} s");
}

int solution1(int[] array)
{
    bool ordenado = false;
    while (!ordenado)
    {
        ordenado = true;
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                int temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
                ordenado = false;
            }
        }
    }
    return array[array.Length - 1];
}

int solution2(int[] array)
{
    int max = array[0];
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] > max)
            max = array[i];
    }
    return max;
}

int solution3(int[] array)
{
    int max = array[0];
    for (int i = 1; i < array.Length; i++)
    {
        if (array[i] > max)
            max = array[i];
        else return max;
    }
    return max;
}

int solution4(int[] array)
{
    int pa = 0, pb = -1, pc = array.Length - 1;
    pb = (pa + pc) / 2;
    while (pa != pb && pa != pc)
    {
        if (array[pb] > array[pb + 1])
        {
            pc = pb;
        }
        else
        {
            pa = pb;
        }
        pb = (pa + pc) / 2;
    }
    return array[pa] > array[pc] ? array[pa] : array[pc];
}

int[] createarray(int N)
{
    int[] array = new int[N];
    int maxindex = rand.Next(1, N - 1);
    array[maxindex] = 100;
    for (int i = maxindex + 1; i < N; i++)
        array[i] = array[i - 1] - rand.Next(4) - 1;
    for (int i = maxindex - 1; i >= 0; i--)
        array[i] = array[i + 1] - rand.Next(4) - 1;
    return array;
}