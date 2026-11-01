using ReadFilesAsync;
using System.Diagnostics;
using static System.Console;

string pathFiles = @"..\..\..\TestData\";
string path1 = @"..\..\..\TestData\test1.txt";
string path2 = @"..\..\..\TestData\test2.txt";
string path3 = @"..\..\..\TestData\War and Peace.txt";
string pattern = " ";

WriteLine(new string('*', 50));
WriteLine("Синхронный поиск. Чтение текстовых файлов в папке.");
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
int cnt = ReadFiles(pathFiles, pattern);
WriteLine("KMP Done in " + stopwatch.ElapsedMilliseconds + " Найдено " + cnt + " вхождений");
WriteLine(new string('*', 50));

stopwatch.Restart();
WriteLine("Прочитать 3 файла параллельно и вычислить количество пробелов в них (через Task).");
var task1 = ReadFileUsingTask(path1, pattern);
var task2 = ReadFileUsingTask(path2, pattern);
var task3 = ReadFileUsingTask(path3, pattern);

int[] results = await Task.WhenAll(task1, task2, task3);
int total = results.Sum();

WriteLine("KMP Done in " + stopwatch.ElapsedMilliseconds + " Найдено " + total + " вхождений");
WriteLine(new string('*', 50));
stopwatch.Restart();
total = await ReadFromFolder(pathFiles, pattern);
WriteLine("KMP Done in " + stopwatch.ElapsedMilliseconds + " Найдено " + total + " вхождений");

/// <summary>
/// Вывести количество пробелов в текстовых файлах (синхронно).
/// </summary>
/// <param name="path">Путь к тектовыми фалам.</param>
static int ReadFiles(string path, string pattern)
{
    var totalSpases = 0;
    var txtFiles = Directory.GetFiles(path, "*.txt");
    foreach (var txtFile in txtFiles)
    {
        string text = File.ReadAllText(txtFile);
        KMP kmp = new KMP();
        kmp.CreatePi(pattern);
        int cnt = kmp.Search(text);
        totalSpases += cnt;
    }

    return totalSpases;
}

/// <summary>
/// Получить количество пробелов в текстовом файле используя Task.
/// </summary>
/// <param name="path">Путь к тектовыми фалам.</param>
async Task<int> ReadFileUsingTask(string filePath, string pattern)
{
    //string text = await File.ReadAllTextAsync(filePath);
    return await Task.Run(() =>
    {
        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}, файл {Path.GetFileName(filePath)}");
        string text = File.ReadAllText(filePath);
        var kmp = new KMP();
        kmp.CreatePi(pattern);
        return kmp.Search(text);
    });
}

/// <summary>
/// Получить количество пробелов в текстовых файлах используя Task.
/// </summary>
/// <param name="path">Путь к тектовыми фалам.</param>
async Task<int> ReadFromFolder(string path, string pattern)
{
    var files = Directory.GetFiles(path, "*.txt");
    var tasks = new List<Task<int>>();
    foreach(var file in files)
        tasks.Add(ReadFileUsingTask(file, pattern));

    int[] results = await Task.WhenAll(tasks);
    return results.Sum();
}
