namespace WebFileManagement.Service.ExtraFolder;

public delegate int MyConculator(int x, int y, int z, int f, int g);
public class WorkingWithDelegate
{

   

    public int CounterTxt(string folderPath)
    {
        var list = Directory.GetFileSystemEntries(folderPath);

        var count = 0;
        foreach (var entry in list)
        {
            if(Path.GetExtension(entry) == ".txt")
            {
                count++;
            }
        }
        return count;
    }


    public static int MyFunc1(int a, int b, int c, int d, int f) => a + b + c + d + f;
    public static int MyFunc2(int a, int b, int c, int d, int f) => a * b * c * d * f;
    public static int MyFunc3(int a, int b, int c, int d, int f) => a - b - c - d - f;
    public static int MyFunc4(int a, int b, int c, int d, int f) => a + b - c + d - f;
    public static int MyFunc5(int a, int b, int c, int d, int f) => a + b * c + d * f;

};


