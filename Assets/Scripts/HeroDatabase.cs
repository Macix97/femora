/// <summary>
/// Stores information about individual heroes and their parameters.
/// </summary>
public static class HeroDatabase
{
    // Paladin
    public static readonly string Paladin = "Paladin";

    // Hero structure
    public struct Hero
    {
        public string Type { get; set; }
        public string Sex { get; set; }
        public string Name { get; set; }
        public bool IsTalking { get; set; }
        public int Level { get; set; }
        public long TotalExp { get; set; }
        public long NextLvLExp { get; set; }
        public long LvlStart { get; set; }
        public long LvlEnd { get; set; }
        public int AttributePts { get; set; }
        public int SkillPts { get; set; }
        public int SpentSkillPts { get; set; }
        public int Vitality { get; set; }
        public int Wisdom { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int MaxHealth { get; set; }
        public int CurHealth { get; set; }
        public int MaxEnergy { get; set; }
        public int CurEnergy { get; set; }
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public float AttackRate { get; set; }
        public float SkillRate { get; set; }
        public float ActionChanceBonus { get; set; }
        public int StartAttackChance { get; set; }
        public int AttackChance { get; set; }
        public int StartDodgeChance { get; set; }
        public int DodgeChance { get; set; }
        public float AttackDist { get; set; }
        public float InteractDist { get; set; }
        public int Defence { get; set; }
        public int ResistMagic { get; set; }
        public float Capacity { get; set; }
        public string LastEnemyType { get; set; }
        public int LastEnemyLvl { get; set; }
        public float WalkSpeed { get; set; }
        public float RunSpeed { get; set; }
        public string CurLocation { get; set; }
    }

    // Heroes
    public static readonly Hero[] Heroes = new Hero[]
    {
        // Paladin
        new Hero
        {
            Type = Paladin,
            Sex = "Male",
            Name = "None",
            IsTalking = false,
            Level = 1,
            TotalExp = 0,
            NextLvLExp = 100,
            LvlStart = 0,
            LvlEnd = 100,
            AttributePts = 0,
            SkillPts = 0,
            SpentSkillPts = 0,
            Vitality = 30,
            Wisdom = 10,
            Strength = 35,
            Agility = 25,
            MaxHealth = 60,
            CurHealth = 60,
            MaxEnergy = 25,
            CurEnergy = 25,
            MinDamage = 7,
            MaxDamage = 15,
            AttackRate = 1f,
            SkillRate = 3f,
            ActionChanceBonus = 0,
            StartAttackChance = 75,
            AttackChance = 75 - EnemyDatabase.Enemies[0].Level,
            StartDodgeChance = 25,
            DodgeChance = 25 - EnemyDatabase.Enemies[0].Level,
            AttackDist = 1.5f,
            InteractDist = 1.2f,
            Defence = 3,
            ResistMagic = 1,
            Capacity = 70,
            LastEnemyType = EnemyDatabase.Enemies[0].Type,
            LastEnemyLvl = EnemyDatabase.Enemies[0].Level,
            WalkSpeed = 3.5f,
            RunSpeed = 7f,
            CurLocation = "Refugee Camp"
        }
    };
}