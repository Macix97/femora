using UnityEngine;

public static class ContainerDatabase
{
    // Item ranks
    public static readonly string OrdinaryRank = "Ordinary";
    public static readonly string EliteRank = "Elite";
    public static readonly string LegendaryRank = "Legendary";
    // Item spread after loot (in angles)
    public static readonly int SpreadAngle = 45;

    public struct Container
    {
        public string Kind { get; set; }
        public int MinItemAmt { get; set; }
        public int MaxItemAmt { get; set; }
        public ItemDatabase.Item[][] ItemPool { get; set; }
        public int[] ItemChancePercent { get; set; }
    }

    // Containers
    public static readonly Container[] Containers = new Container[]
    {
        // Loose rock
        new Container()
        {
            Kind = "Loose Rock",
            MinItemAmt = 0,
            MaxItemAmt = 3,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems, ItemDatabase.EliteItems,
                ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[]  { 30, 10, 1 }
        },
        // Chest
        new Container()
        {
            Kind = "Chest",
            MinItemAmt = 1,
            MaxItemAmt = 4,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems, ItemDatabase.EliteItems,
                ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[]  { 60, 20, 5 }
        },
        // Remains
        new Container()
        {
            Kind = "Remains",
            MinItemAmt = 2,
            MaxItemAmt = 6,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems, ItemDatabase.EliteItems,
                ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[]  { 80, 35, 15 }
        }
    };
}