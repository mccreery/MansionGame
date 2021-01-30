using System;
using UnityEngine;

[Serializable]
public struct FloatGradient
{
    [Serializable]
    public struct Stop
    {
        public float time;
        public float targetValue;
    }

    public float startValue;
    public Stop[] stops;

    public float this[float time]
    {
        get
        {
            if (time < 0)
            {
                return startValue;
            }

            float previousValue = startValue;
            foreach (Stop stop in stops)
            {
                if (time < stop.time)
                {
                    float t = time / stop.time;
                    return Mathf.Lerp(previousValue, stop.targetValue, t);
                }
                time -= stop.time;
                previousValue = stop.targetValue;
            }
            return previousValue;
        }
    }
}
