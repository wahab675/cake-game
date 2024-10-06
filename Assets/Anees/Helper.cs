using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static string FormatNumber(uint value)
    {
        const int shortScale = 1000;

        if(value < shortScale)
        {
            return value.ToString();
        }

        string[] suffixes = { "K", "M", "B", "T", "Q" }; // Add more as needed

        int logValue = (int)Mathf.Log10(value);

        int index = logValue / 3; // Determine the appropriate suffix index

        // Ensure the index is within the bounds of the suffix array
        index = Mathf.Clamp(index, 0, suffixes.Length - 1);

        // Calculate the scaled value with a single decimal place
        float scaledValue = value / Mathf.Pow(10, index * 3);

        // Use string.Format to format the string without decimal places if they are zero
        string formattedValue = scaledValue.ToString("#.#") + suffixes[index-1];

        return formattedValue;
    }


}
