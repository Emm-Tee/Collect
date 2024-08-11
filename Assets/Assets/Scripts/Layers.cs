namespace Collect.Core
{
    public static class Layers
    {
        public const int Ground = 6;
        public const int Wall = 7;
        public const int Collectable = 8;
        public const int Repository = 9;
        public const int Player = 10;
    }

    public static class LayerMasks
    {
        public const int Player = 1 << Layers.Player;
        public const int Collectable = 1 << Layers.Collectable;
        public const int Environment = (1 << Layers.Ground) | (1 << Layers.Collectable) | (1 << Layers.Repository) | (1 << Layers.Wall);
    }
}
