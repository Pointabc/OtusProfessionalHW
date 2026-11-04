using System.Runtime.InteropServices;

namespace Multithreaded;

/// <summary>
/// From deepseek.
/// </summary>
public class MemoryInfo
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys; // Общий объём физической памяти
        public ulong ullAvailPhys; // Доступная физическая память
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

    public static void PrintMemoryStatus()
    {
        var memStatus = new MEMORYSTATUSEX();
        if (GlobalMemoryStatusEx(memStatus))
        {
            double totalGB = memStatus.ullTotalPhys / (1024.0 * 1024.0 * 1024.0);
            double availGB = memStatus.ullAvailPhys / (1024.0 * 1024.0 * 1024.0);
            Console.WriteLine("--- Информация о памяти ---");
            Console.WriteLine($"Общий объём памяти: {totalGB:F2} GB");
            Console.WriteLine($"Доступная память: {availGB:F2} GB");
        }
        else
        {
            Console.WriteLine("Не удалось получить информацию о памяти.");
        }
    }
}
