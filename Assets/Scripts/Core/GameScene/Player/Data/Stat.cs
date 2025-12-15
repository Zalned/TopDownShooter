using System;

public enum StatType {
    Additive, Percent
}

public struct Stat {
    public float Base;
    public float Additive;
    public float Percent;

    public float Value => (Base + Additive) * (1f + Percent);
}