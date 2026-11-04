namespace ReadFilesAsync;

// Алгоритм Ктута-Морриса-Прата. Взял из лекций по алгоритмам.
public class KMP
{
    int[] pi;
    string pattern;
    public void CreatePi(string pattern)
    {
        this.pattern = pattern;
        pi = new int[pattern.Length + 1];
        pi[0] = 0;
        pi[1] = 0;

        for (int q = 1; q < pattern.Length; q++)
        {
            int len = pi[q];

            while (len > 0 && pattern[len] != pattern[q])
                len = pi[len];

            if (pattern[len] == pattern[q])
                len++;

            pi[q + 1] = len;
        }
    }

    public int Search(string text)
    {
        int q = 0;
        int count = 0;

        for (int i = 0; i < text.Length; i++)
        {
            while (q > 0 && pattern[q] != text[i])
                q = pi[q];

            if (pattern[q] == text[i])
                q++;

            if (q == pattern.Length)
            {
                count++;
                q = pi[q];
            }
        }

        return count;
    }
}
