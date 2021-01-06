using UnityEngine;

/// <summary>
/// Stores information about individual enemies and their parameters.
/// </summary>
public static class EnemyDatabase
{
    // Enemy attack type
    public static readonly string PhysicalAttack = "Physical Attack";
    public static readonly string MagicalAttack = "Magical Attack";
    // Zombie
    public static readonly string Zombie = "Zombie";
    // Skeleton
    public static readonly string Skeleton = "Skeleton";
    // Demon Skeleton
    public static readonly string DemonSkeleton = "Demon Skeleton";
    // Rotfiend
    public static readonly string Rotfiend = "Rotfiend";
    // Pit Fiend
    public static readonly string PitFiend = "Pit Fiend";
    // Efreeti
    public static readonly string Efreeti = "Efreeti";
    // Hell Knight
    public static readonly string HellKnight = "Hell Knight";

    // Enemy structure
    public struct Enemy
    {
        public string Nature { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int MaxHealth { get; set; }
        public int CurHealth { get; set; }
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public float AttackRate { get; set; }
        public float DetectRay { get; set; }
        public float AttackRay { get; set; }
        public string AttackType { get; set; }
        public int MinItemAmt { get; set; }
        public int MaxItemAmt { get; set; }
        public ItemDatabase.Item[][] ItemPool { get; set; }
        public int[] ItemChancePercent { get; set; }
        public float DeathTime { get; set; }
        public float SpawnTime { get; set; }
        public Vector3 RespawnPosition { get; set; }
    }

    // Enemies
    public static readonly Enemy[] Enemies = new Enemy[]
    {
        // Zombie
        new Enemy
        {
            Nature = "Undead",
            Type = Zombie,
            Level = 1,
            Experience = 100,
            MaxHealth = 50,
            CurHealth = 50,
            MaxDamage = 20,
            MinDamage = 10,
            AttackRate = 2f,
            DetectRay = 8.5f,
            AttackRay = 1.5f,
            AttackType = PhysicalAttack,
            MinItemAmt = 0,
            MaxItemAmt = 3,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems },
            ItemChancePercent = new int[] { 50 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Skeleton
        new Enemy
        {
            Nature = "Undead",
            Type = Skeleton,
            Level = 5,
            Experience = 500,
            MaxHealth = 90,
            CurHealth = 90,
            MaxDamage = 35,
            MinDamage = 20,
            AttackRate = 1.5f,
            DetectRay = 11f,
            AttackRay = 2f,
            AttackType = PhysicalAttack,
            MinItemAmt = 1,
            MaxItemAmt = 4,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems, ItemDatabase.EliteItems },
            ItemChancePercent = new int[] { 70, 10 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Demon Skeleton
        new Enemy
        {
            Nature = "Demon",
            Type = DemonSkeleton,
            Level = 10,
            Experience = 1000,
            MaxHealth = 130,
            CurHealth = 130,
            MaxDamage = 50,
            MinDamage = 25,
            AttackRate = 1.2f,
            DetectRay = 12f,
            AttackRay = 2f,
            AttackType = PhysicalAttack,
            MinItemAmt = 1,
            MaxItemAmt = 4,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.OrdinaryItems, ItemDatabase.EliteItems,
                ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[] { 90, 25, 5 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Rotfiend
        new Enemy
        {
            Nature = "Undead",
            Type = Rotfiend,
            Level = 15,
            Experience = 1500,
            MaxHealth = 200,
            CurHealth = 200,
            MaxDamage = 65,
            MinDamage = 35,
            AttackRate = 1.1f,
            DetectRay = 13f,
            AttackRay = 1.8f,
            AttackType = PhysicalAttack,
            MinItemAmt = 2,
            MaxItemAmt = 5,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.EliteItems, ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[] { 40, 15 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Pit Fiend
        new Enemy
        {
            Nature = "Demon",
            Type = PitFiend,
            Level = 20,
            Experience = 2000,
            MaxHealth = 270,
            CurHealth = 270,
            MaxDamage = 80,
            MinDamage = 50,
            AttackRate = 1.7f,
            DetectRay = 11f,
            AttackRay = 1.6f,
            AttackType = PhysicalAttack,
            MinItemAmt = 2,
            MaxItemAmt = 5,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.EliteItems, ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[] { 65, 30 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Efreeti
        new Enemy
        {
            Nature = "Demon",
            Type = Efreeti,
            Level = 25,
            Experience = 2500,
            MaxHealth = 280,
            CurHealth = 280,
            MaxDamage = 120,
            MinDamage = 80,
            AttackRate = 1.2f,
            DetectRay = 12.5f,
            AttackRay = 1.4f,
            AttackType = MagicalAttack,
            MinItemAmt = 2,
            MaxItemAmt = 6,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.EliteItems, ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[] { 80, 35 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        },
        // Hell Knight
        new Enemy
        {
            Nature = "Demon",
            Type = HellKnight,
            Level = 30,
            Experience = 3000,
            MaxHealth = 350,
            CurHealth = 350,
            MaxDamage = 150,
            MinDamage = 100,
            AttackRate = 1f,
            DetectRay = 13f,
            AttackRay = 2f,
            AttackType = MagicalAttack,
            MinItemAmt = 3,
            MaxItemAmt = 8,
            ItemPool = new ItemDatabase.Item[][] { ItemDatabase.LegendaryItems },
            ItemChancePercent = new int[] { 50 },
            DeathTime = 0f,
            SpawnTime = 0f,
            RespawnPosition = Vector3.zero
        }
    };
}