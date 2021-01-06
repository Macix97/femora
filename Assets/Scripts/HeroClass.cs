using UnityEngine;

/// <summary>
/// Describes the basic parameters of the hero.
/// </summary>
public class HeroClass : MonoBehaviour
{
    // Hero tag
    public static readonly string HeroTag = "Hero";
    // Attributes IDs
    public static readonly string VitalityId = "Vitality";
    public static readonly string WisdomId = "Wisdom";
    public static readonly string StrengthId = "Strength";
    public static readonly string AgilityId = "Agility";
    // Statistics IDs
    public static readonly string MaxHealthId = "Maximal Health";
    public static readonly string CurHealthId = "Current Health";
    public static readonly string MaxEnergyId = "Maximal Energy";
    public static readonly string CurEnergyId = "Current Energy";
    public static readonly string MinDamageId = "Minimal Damage";
    public static readonly string MaxDamageId = "Maximal Damage";
    public static readonly string AttackRateId = "Attack Rate";
    public static readonly string AttackDistId = "Attack Distance";
    public static readonly string DefenceId = "Defence";
    public static readonly string ResistMagicId = "Resist Magic";
    public static readonly string CapacityId = "Capacity";
    public static readonly int MaxActionChance = 90;
    public static readonly int MinActionChance = 10;
    // Modifiers
    public static readonly int HealthMod = 2;
    public static readonly int EnergyMod = 2;
    public static readonly int MaxDamageMod = 1;
    public static readonly int MinDamageMod = 1;
    public static readonly int CapacityMod = 2;
    public static readonly float AttackRateMod = 0.001f;
    public static readonly float ActionChanceMod = 0.5f;
    // Motions
    public static readonly string RunMotion = "isRunning";
    public static readonly string WalkMotion = "isWalking";
    public static readonly string AttackMotion = "isAttacking";
    public static readonly string SkillMotion = "isCasting";
    public static readonly string DeathMotion = "death";
    public static readonly string RiseMotion = "rise";
    public static readonly string AttackRateFloat = "attackRate";

    // Hero properties
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
    public Vector3 StartPos { get; set; }
    public Vector3 StartRot { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    public void Init()
    {
        // Search proper hero
        for (int cnt = 0; cnt < HeroDatabase.Heroes.Length; cnt++)
            // Check hero name
            if (name.Equals(HeroDatabase.Heroes[cnt].Type))
            {
                // Initialize hero
                InitHero(HeroDatabase.Heroes[cnt]);
                // Break action
                return;
            }
    }

    /// <summary>
    /// Initiates hero parameters according to appropriate criteria.
    /// </summary>
    /// <param name="hero">A Type of hero from database.</param>
    public void InitHero(HeroDatabase.Hero hero)
    {
        Type = hero.Type;
        Sex = hero.Sex;
        Name = hero.Name;
        IsTalking = hero.IsTalking;
        Level = hero.Level;
        TotalExp = hero.TotalExp;
        NextLvLExp = hero.NextLvLExp;
        LvlStart = hero.LvlStart;
        LvlEnd = hero.LvlEnd;
        AttributePts = hero.AttributePts;
        SkillPts = hero.SkillPts;
        SpentSkillPts = hero.SpentSkillPts;
        Vitality = hero.Vitality;
        Wisdom = hero.Wisdom;
        Strength = hero.Strength;
        Agility = hero.Agility;
        MaxHealth = hero.MaxHealth;
        CurHealth = hero.CurHealth;
        MaxEnergy = hero.MaxEnergy;
        CurEnergy = hero.CurEnergy;
        MinDamage = hero.MinDamage;
        MaxDamage = hero.MaxDamage;
        AttackRate = hero.AttackRate;
        SkillRate = hero.SkillRate;
        ActionChanceBonus = hero.ActionChanceBonus;
        StartAttackChance = hero.StartAttackChance;
        AttackChance = hero.AttackChance;
        StartDodgeChance = hero.StartDodgeChance;
        DodgeChance = hero.DodgeChance;
        AttackDist = hero.AttackDist;
        InteractDist = hero.InteractDist;
        Defence = hero.Defence;
        ResistMagic = hero.ResistMagic;
        Capacity = hero.Capacity;
        LastEnemyType = hero.LastEnemyType;
        LastEnemyLvl = hero.LastEnemyLvl;
        WalkSpeed = hero.WalkSpeed;
        RunSpeed = hero.RunSpeed;
        CurLocation = hero.CurLocation;
        StartPos = transform.position;
        StartRot = transform.eulerAngles;
    }
}