using System;
using System.Collections.Generic;

public class AlphanumericComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        int xNum, yNum;

        bool isXNumeric = int.TryParse(x, out xNum);
        bool isYNumeric = int.TryParse(y, out yNum);

        // Both are numbers
        if (isXNumeric && isYNumeric)
        {
            return xNum.CompareTo(yNum);
        }

        // Only x is a number
        if (isXNumeric)
        {
            return -1;
        }

        // Only y is a number
        if (isYNumeric)
        {
            return 1;
        }

        // Both are strings, extract numeric parts
        int xIndex = FindFirstDigit(x);
        int yIndex = FindFirstDigit(y);

        string xPrefix = x.Substring(0, xIndex);
        string yPrefix = y.Substring(0, yIndex);

        int comparison = xPrefix.CompareTo(yPrefix);
        if (comparison != 0)
        {
            return comparison;
        }

        xNum = int.Parse(x.Substring(xIndex));
        yNum = int.Parse(y.Substring(yIndex));

        return xNum.CompareTo(yNum);
    }

    private int FindFirstDigit(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsDigit(s[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
