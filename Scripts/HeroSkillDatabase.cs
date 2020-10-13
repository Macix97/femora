using UnityEngine;

public static class HeroSkillDatabase
{
    // Skill types
    public static readonly string Attack = "Physical Attack";
    public static readonly string Passive = "Passive Skill";
    public static readonly string Active = "Active Skill";
    public static readonly string Regenerative = "Regenerative Skill";
    public static readonly string Releasable = "Releasable Skill";
    // Skill stats
    public static readonly string Health = "Health";
    public static readonly string Damage = "Damage";
    public static readonly string AttackRate = "Attack Rate";
    public static readonly string Defence = "Defence";
    // Skill results
    public static readonly string AttackMelee = "Skill Attack Melee";
    public static readonly string Support = "Skill Support";
    public static readonly string None = "None";
    // Attack melee aura position
    public static readonly Vector3 AttackMeleePos = new Vector3(-0.0064f, 0.0571f, 0.0266f);
    // Attack melee aura rotation
    public static readonly Vector3 AttackMeleeRot = new Vector3(0f, -90f, 90f);
    // Skill colors
    public static readonly Color White = new Color(1f, 1f, 1f);
    public static readonly Color Blue = new Color(0.51f, 0.58f, 1f);
    public static readonly Color Yellow = new Color(0.74f, 0.62f, 0.15f);
    public static readonly Color Purple = new Color(0.76f, 0.29f, 1f);


    // Skill structure
    public struct Skill
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public int Level { get; set; }
        public string[] Stats { get; set; }
        public float Effect { get; set; }
        public float EnergyCost { get; set; }
        public float Rate { get; set; }
        public string Result { get; set; }
        public int ReqLvl { get; set; }
        public string ReqSkill { get; set; }
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }
    }

    // Normal attack
    public static readonly Skill NormalAttack = new Skill
    {
        // Normal attack
        Id = 0,
        Kind = "Normal Attack",
        Type = Attack,
        Desc = "The basic attack of the hero that deals physical damage",
        Level = 0,
        Stats = new string[] { None },
        Effect = 0f,
        EnergyCost = 0f,
        Rate = 0f,
        Result = None,
        ReqLvl = 0,
        ReqSkill = None,
        Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageNormalAttack"),
        Color = White
    };

    // Paladin skills
    public static readonly Skill[] PaladinSkills = new Skill[]
    {
        // Bless
        new Skill
        {
            Id = 1,
            Kind = "Bless",
            Type = Passive,
            Desc = "Increases the paladin health by 2 per level",
            Level = 0,
            Stats = new string[] { Health },
            Effect = 2f,
            EnergyCost = 0f,
            Rate = 0f,
            Result = None,
            ReqLvl = 0,
            ReqSkill = None,
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageBless"),
            Color = White
        },
        // Sacred Might
        new Skill
        {
            Id = 2,
            Kind = "Sacred Might",
            Type = Passive,
            Desc = "Increases the paladin damage by 1 per level",
            Level = 0,
            Stats = new string[] { Damage },
            Effect = 1f,
            EnergyCost = 0f,
            Rate = 0f,
            Result = None,
            ReqLvl = 0,
            ReqSkill = None,
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageSacredMight"),
            Color = White
        },
        // Prayer
        new Skill
        {
            Id = 3,
            Kind = "Prayer",
            Type = Regenerative,
            Desc = "Aura that heals paladin once every second",
            Level = 0,
            Stats = new string[] { Health },
            Effect = 1.5f,
            EnergyCost = 0.5f,
            Rate = 1f,
            Result = None,
            ReqLvl = 10,
            ReqSkill = "Bless",
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImagePrayer"),
            Color = Purple
        },
        // Holy Zeal
        new Skill
        {
            Id = 4,
            Kind = "Holy Zeal",
            Type = Releasable,
            Desc = "Increases paladin damage during an attack",
            Level = 0,
            Stats = new string[] { Damage },
            Effect = 2f,
            EnergyCost = 0.5f,
            Rate = 0f,
            Result = AttackMelee,
            ReqLvl = 10,
            ReqSkill = "Sacred Might",
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageHolyZeal"),
            Color = Blue
        },
        // Sanctuary
        new Skill
        {
            Id = 5,
            Kind = "Sanctuary",
            Type = Releasable,
            Desc = "Increases paladin defence and health when is active",
            Level = 0,
            Stats = new string[] { Defence, Health },
            Effect = 3f,
            EnergyCost = 1.5f,
            Rate = 30f,
            Result = Support,
            ReqLvl = 20,
            ReqSkill = "Prayer",
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageSanctuary"),
            Color = Blue
        },
        // Divine Anger
        new Skill
        {
            Id = 6,
            Kind = "Divine Anger",
            Type = Active,
            Desc = "Aura that increases paladin damage and attack rate",
            Level = 0,
            Stats = new string[] { Damage, AttackRate },
            Effect = 3f,
            EnergyCost = 0f,
            Rate = 0f,
            Result = None,
            ReqLvl = 20,
            ReqSkill = "Holy Zeal",
            Sprite = Resources.Load<Sprite>(ItemDatabase.Sprites + "ImageDivineAnger"),
            Color = Yellow
        },
    };
}