namespace ReadFilesAsync;

internal class Automat
{
    int[,] delta;
    int length;
    string pattern;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pattern"></param>
    public Automat(string pattern)
    {
        this.pattern = pattern;
        length = pattern.Length;
        CreateDelta();
    }

    private void CreateDelta()
    {
        delta = new int[length + 1, 128];

        for (int q = 0; q <= length; q++)
        {
            for (char c = ' '; c < '~'; c++)
            {
                if (c == 114)
                {
                    int a = 15;
                }

                int k = q + 1;
                if (q == length)
                    k--;

                string line = pattern.Left(q) + c;

                while (pattern.Left(k) != line.Right(k))
                    k--;

                delta[q, c] = k;
            }
        }
    }

    public int search(string text)
    {
        int count = 0;
        int q = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] < 32 || text[i] > 126)
                q = 0;
            else
            {
                q = delta[q, text[i]];
                if (q == length)
                    count++;
            }
        }

        return count;
    }
}
