using Multithreaded;
using System.Diagnostics;
using static System.Console;

// Создаем переменную для хранения результата.
int sumResult = 0;
object locker = new object();
// Сгенерировать массивы 100000, 1000000 и 10000000
WriteLine(new string('*', 50));

#region Вычисление суммы массива целых чисел (синхронно).

WriteLine("Вычисление суммы массива целых чисел (синхронно)...");
var ar = GenerateIntArray(10000000);
Stopwatch sw = Stopwatch.StartNew();
var sum = GetIntArraySumSync(ar);
sw.Stop();
WriteLine($"Сумма массива (вычисление синхронное): {sum}.");
WriteLine($"Время синхронного вычисления суммы: {sw.ElapsedMilliseconds} милисекунд.");

#endregion

#region Вычисление суммы массива целых чисел (асинхронно - Thread) 

//***************************************************************************************
WriteLine();
WriteLine(new string('*', 50));
WriteLine("Вычисление суммы массива целых чисел (асинхронно)...");
var exceptions = new List<Exception>();
var threads = new List<Thread>(); // Сохранить threads для ожидания зввершения.
int quantityThreads = GetNumberThreads();
if (quantityThreads <= 0) quantityThreads = 1;
if (quantityThreads > ar.Length) quantityThreads = Math.Max(1, ar.Length);

sw.Restart();
// Создать quantityThreads потоков для вычисления суммы.
var r = ar.Length % quantityThreads;
int parts = ar.Length / quantityThreads;

int currentStart = 0;
for (int i = 0; i < quantityThreads; i++)
{
    // Первые r потоков получают на 1 элемент больше
    int length = parts + (i < r ? 1 : 0);
    if (length == 0)
        continue;

    int startIndex = currentStart;
    int endIndex = currentStart + length; // ЭКСКЛЮЗИВНЫЙ конец — как ждёт метод
    currentStart = endIndex;

    Thread thread = new Thread(() =>
    {
        int sum = GetIntArraySumThread(ar, startIndex, endIndex);
        lock (locker)
        {
            sumResult += sum;
        }
    });

    threads.Add(thread);
    thread.Start();
}

foreach (var thread in threads)
    thread.Join(); // Ждем завершения потока.

if (exceptions.Count > 0)
    throw new AggregateException("Ошибка при вычислении суммы", exceptions);
sw.Stop();
WriteLine($"Сумма элементов (Thread) = {sumResult}");
WriteLine($"Время aсинхронного вычисления суммы: {sw.ElapsedMilliseconds} милисекунд.");

#endregion

#region Вычисление суммы массива целых чисел (асинхронно - PLINQ).

WriteLine(new string('*', 50));
WriteLine("Вычисление суммы массива целых чисел (асинхронно - PLINQ)...");

sw.Restart();
// Включаем параллельный режим и напрямую вычисляем сумму
int sumPLINQ = ar.AsParallel().Sum();
sw.Stop();
WriteLine($"Сумма элементов (PLINQ) = {sumPLINQ}");
WriteLine($"Время aсинхронного вычисления суммы (PLINQ): {sw.ElapsedMilliseconds} милисекунд.");

#endregion

#region Вывод информации об окружении

WriteLine(new string('*', 50));
// Вывод информации об окружении
var os = Environment.OSVersion;
WriteLine("=== Информация об окружении ===");
WriteLine($"ОС: {os.VersionString} ({os.Platform})");
WriteLine($"Логических процессоров: {Environment.ProcessorCount}");
MemoryInfo.PrintMemoryStatus();

#endregion

WriteLine("Done.");

#region Методы

// Вычислить сумму всех эл-тов массива типа int синхронно.
static int GetIntArraySumSync(int[] ar)
{
    int sum = 0;
    foreach (int i in ar)
        sum += i;

    return sum;
}

static int GetIntArraySumThread(int[] ar, int startIndex, int endIndex)
{
    int sum = 0;
    for (int i = startIndex; i < endIndex; i++)
        sum += ar[i];

    return sum;
}

/// <summary>
/// Сгенерировать массив целых чисел.
/// </summary>
/// <param name="quantityElements">Количество элементов массива.</param>
/// <returns>Массив целых чисел.</returns>
static int[] GenerateIntArray(int quantityElements)
{
    if (quantityElements < 1)
        return Array.Empty<int>();
    ;
    int[] intArray = new int[quantityElements];
    for (int i = 0; i < quantityElements; ++i)
        intArray[i] = new Random().Next(1, 10);

    return intArray;
}

/// <summary>
/// Получить количество потоков для вычисления суммы масстива.
/// </summary>
/// <returns>Необходимое кол-во потоков.</returns>
int GetNumberThreads()
{
    var quantityThreads = 0;
    while (true)
    {
        Write("Введите кол-во потоков для вычисления суммы массива (2-10): ");
        var input = ReadLine();
        if (int.TryParse(input, out quantityThreads))
        {
            if (quantityThreads >= 2 && quantityThreads <= 10)
                break;
        }
    }

    return quantityThreads;
}

#endregion