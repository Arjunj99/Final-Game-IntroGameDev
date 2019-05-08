using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class God {
    public static GSM GSM;
    public static AM AM;
    public static SM SM;
    public static WSM WSM;
    public static LibraryManager Library;
    public static Controller C;
    public static int Round = 0;
    public static int MonsterCount = 0;
    public static int GemCount = 0;
    public static bool MonsterSwitch = false;

    public static float Ease(float t, bool inout) {
        if (inout)
            return t < .5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
        return (--t) * t * t + 1;
    }
}