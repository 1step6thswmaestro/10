using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
    
    public static void ShuffleList<T>(IList<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            int k = (Random.Range(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Color ComplementaryColor (Color color)
    {
        Color complementary = new Color();

        complementary.a = 1;
        complementary.r = 1 - color.r;
        complementary.g = 1 - color.g;
        complementary.b = 1 - color.b;
        // complementary.r = color.r;
        // complementary.g = color.g;
        // complementary.b = color.b;

        return complementary;
    }

    public static bool ColorCompareRGB (Color color1, Color color2)
    {
        return color1.r == color2.r && color1.g == color2.g && color1.b == color2.b;
    }
}
