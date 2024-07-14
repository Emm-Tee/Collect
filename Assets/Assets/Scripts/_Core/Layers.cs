using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Layers
{
    public const int Ground = 6;
    public const int Player = 7;
    public const int Collectable = 8;
    public const int Repository = 9;
}

public static class LayerMasks
{
    public const int Player = 1 << Layers.Player;
    public const int Collectable = 1 << Layers.Collectable;
}
