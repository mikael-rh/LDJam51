using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class XRangeAttribute : PropertyAttribute
{
    public readonly float min, max;
    public XRangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
    public XRangeAttribute(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}