using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    // Sprites path
    public static readonly string Sprites = "Sprites/";
    // Prefabs path
    public static readonly string Prefabs = "Prefabs/";
    // Material path
    public static readonly string Materials = "Materials/";
    // Item ranks
    public static string Ordinary = "Ordinary Item";
    public static string Elite = "Elite Item";
    public static string Legendary = "Legendary Item";
    // Item types
    public static readonly string Gold = "Gold";
    public static readonly string Potion = "Potion";
    public static readonly string Head = "Headgear";
    public static readonly string Torso = "Armor";
    public static readonly string LeftHand = "Shield";
    public static readonly string RightHand = "Weapon";
    public static readonly string Feet = "Footwear";
    // Bonus stats
    public static readonly string Bonus = " Bonus: ";
    // Hint colors
    public static Color White = new Color(1f, 1f, 1f);
    public static Color Green = new Color(0.24f, 1f, 0.07f);
    public static Color Orange = new Color(1f, 0.6f, 0.01f);

    // Item structure
    public struct Item
    {
        public string Rank { get; set; }
        public string Type { get; set; }
        public string Kind { get; set; }
        public string Desc { get; set; }
        public string[] Bonus { get; set; }
        public string[] Stats { get; set; }
        public float[] Effect { get; set; }
        public float Weight { get; set; }
        public int Value { get; set; }
        public int MinVal { get; set; }
        public int MaxVal { get; set; }
        public Sprite Sprite { get; set; }
        public Vector3 Pos { get; set; }
        public Vector3 Rot { get; set; }
        public Color HintColor { get; set; }
    }

    // Money
    public static readonly Item[] Money = new Item[]
    {
        // Few Gold Coins
        new Item()
        {
            Rank = Ordinary,
            Type = Gold,
            Kind = "Few Gold Coins",
            Desc = null,
            Bonus = null,
            Stats = null,
            Effect = new float[] { 0f },
            Weight = 0f,
            MinVal = 1,
            MaxVal = 10,
            Value = 0,
            Sprite = null,
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Lots Gold Coins
        new Item()
        {
            Rank = Ordinary,
            Type = Gold,
            Kind = "Lots Gold Coins",
            Desc = null,
            Bonus = null,
            Stats = null,
            Effect = new float[] { 0f },
            Weight = 0f,
            MinVal = 10,
            MaxVal = 50,
            Value = 0,
            Sprite = null,
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Plenty Gold Coins
        new Item()
        {
            Rank = Elite,
            Type = Gold,
            Kind = "Plenty Gold Coins",
            Desc = null,
            Bonus = null,
            Stats = null,
            Effect = new float[] { 50f, 100f },
            Weight = 0f,
            MinVal = 50,
            MaxVal = 100,
            Value = 0,
            Sprite = null,
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Tons Gold Coins
        new Item()
        {
            Rank = Legendary,
            Type = Gold,
            Kind = "Tons Gold Coins",
            Desc = null,
            Bonus = null,
            Stats = null,
            Effect = new float[] { 100f, 250f },
            Weight = 0f,
            MinVal = 100,
            MaxVal = 250,
            Value = 0,
            Sprite = null,
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        }
    };

    // Ordinary items
    public static readonly Item[] OrdinaryItems = new Item[]
    {
        // Small Healing Potion
        new Item()
        {
            Rank = Ordinary,
            Type = Potion,
            Kind = "Small Healing Potion",
            Desc = "Replenishes a hero life",
            Bonus = new string[] { HeroClass.CurHealthId + Bonus },
            Stats = new string[] { HeroClass.CurHealthId },
            Effect = new float[] { 30f },
            Weight = 0.4f,
            MinVal = 30,
            MaxVal = 30,
            Value = 30,
            Sprite = Resources.Load<Sprite>(Sprites + "SmallHealingPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Small Energy Potion
        new Item()
        {
            Rank = Ordinary,
            Type = Potion,
            Kind = "Small Energy Potion",
            Desc = "Replenishes a hero energy",
            Bonus = new string[] { HeroClass.CurEnergyId + Bonus },
            Stats = new string[] { HeroClass.CurEnergyId },
            Effect = new float[] { 30f },
            Weight = 0.4f,
            MinVal = 30,
            MaxVal = 30,
            Value = 30,
            Sprite = Resources.Load<Sprite>(Sprites + "SmallEnergyPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Chain Mail Cap
        new Item()
        {
            Rank = Ordinary,
            Type = Head,
            Kind = "Chain Mail Cap",
            Desc = "Increases hero health",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId },
            Effect = new float[] { 2f },
            Weight = 7f,
            MinVal = 60,
            MaxVal = 60,
            Value = 60,
            Sprite = Resources.Load<Sprite>(Sprites + "ChainMailCap"),
            Pos = new Vector3(0f, 0.002f, 0.003f),
            Rot = Vector3.zero,
            HintColor = White
        },
        // Bascinet
        new Item()
        {
            Rank = Ordinary,
            Type = Head,
            Kind = "Bascinet",
            Desc = "Increases hero health",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId },
            Effect = new float[] { 4f },
            Weight = 10f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "Bascinet"),
            Pos = new Vector3(0f, 0.022f, 0f),
            Rot = Vector3.zero,
            HintColor = White
        },
        // Light Leather Armor
        new Item()
        {
            Rank = Ordinary,
            Type = Torso,
            Kind = "Light Leather Armor",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 2f, 2f },
            Weight = 18f,
            MinVal = 150,
            MaxVal = 150,
            Value = 150,
            Sprite = Resources.Load<Sprite>(Sprites + "LightLeatherArmor"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Banded Armor
        new Item()
        {
            Rank = Ordinary,
            Type = Torso,
            Kind = "Banded Armor",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 4f, 4f },
            Weight = 20f,
            MinVal = 300,
            MaxVal = 300,
            Value = 300,
            Sprite = Resources.Load<Sprite>(Sprites + "BandedArmor"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Short Sword
        new Item()
        {
            Rank = Ordinary,
            Type = RightHand,
            Kind = "Short Sword",
            Desc = "Increases hero damage",
            Bonus = new string[] { HeroClass.MaxDamageId + Bonus },
            Stats = new string[] { HeroClass.MaxDamageId },
            Effect = new float[] { 2f },
            Weight = 5.5f,
            MinVal = 60,
            MaxVal = 60,
            Value = 60,
            Sprite = Resources.Load<Sprite>(Sprites + "ShortSword"),
            Pos = new Vector3(-0.001f, 0.057f, 0.0323f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = White
        },
        // Steel Sword
        new Item()
        {
            Rank = Ordinary,
            Type = RightHand,
            Kind = "Steel Sword",
            Desc = "Increases hero damage",
            Bonus = new string[] { HeroClass.MaxDamageId + Bonus },
            Stats = new string[] { HeroClass.MaxDamageId },
            Effect = new float[] { 4f },
            Weight = 6f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "SteelSword"),
            Pos = new Vector3(-0.0064f, 0.0571f, 0.0266f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = White
        },
        // Wooden Shield
        new Item()
        {
            Rank = Ordinary,
            Type = LeftHand,
            Kind = "Wooden Shield",
            Desc = "Increases hero defence",
            Bonus = new string[] { HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.DefenceId },
            Effect = new float[] { 2f },
            Weight = 10.5f,
            MinVal = 60,
            MaxVal = 60,
            Value = 60,
            Sprite = Resources.Load<Sprite>(Sprites + "WoodenShield"),
            Pos = new Vector3(0.004f, -0.03f, -0.05f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = White
        },
        // Heater Shield
        new Item()
        {
            Rank = Ordinary,
            Type = LeftHand,
            Kind = "Heater Shield",
            Desc = "Increases hero defence",
            Bonus = new string[] { HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.DefenceId },
            Effect = new float[] { 4f },
            Weight = 12.5f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "HeaterShield"),
            Pos = new Vector3(-0.066f, -0.044f, -0.061f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = White
        },
        // Heavy Boots
        new Item()
        {
            Rank = Ordinary,
            Type = Feet,
            Kind = "Heavy Boots",
            Desc = "Increases hero health",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId },
            Effect = new float[] { 2f },
            Weight = 8f,
            MinVal = 60,
            MaxVal = 60,
            Value = 60,
            Sprite = Resources.Load<Sprite>(Sprites + "HeavyBoots"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        },
        // Leather Boots
        new Item()
        {
            Rank = Ordinary,
            Type = Feet,
            Kind = "Leather Boots",
            Desc = "Increases hero health",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId },
            Effect = new float[] { 4f },
            Weight = 9f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "LeatherBoots"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = White
        }
    };

    // Elite items
    public static readonly Item[] EliteItems = new Item[]
    {
        // Healing Potion
        new Item()
        {
            Rank = Elite,
            Type = Potion,
            Kind = "Healing Potion",
            Desc = "Replenishes a hero life",
            Bonus = new string[] { HeroClass.CurHealthId + Bonus },
            Stats = new string[] { HeroClass.CurHealthId },
            Effect = new float[] { 50f },
            Weight = 0.7f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "HealingPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Energy Potion
        new Item()
        {
            Rank = Elite,
            Type = Potion,
            Kind = "Energy Potion",
            Desc = "Replenishes a hero energy",
            Bonus = new string[] { HeroClass.CurEnergyId + Bonus },
            Stats = new string[] { HeroClass.CurEnergyId },
            Effect = new float[] { 50f },
            Weight = 0.7f,
            MinVal = 120,
            MaxVal = 120,
            Value = 120,
            Sprite = Resources.Load<Sprite>(Sprites + "EnergyPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Small Rejuvenation Potion
        new Item()
        {
            Rank = Elite,
            Type = Potion,
            Kind = "Small Rejuvenation Potion",
            Desc = "Replenishes a hero life and energy",
            Bonus = new string[] { HeroClass.CurHealthId + Bonus, HeroClass.CurEnergyId + Bonus },
            Stats = new string[] { HeroClass.CurHealthId, HeroClass.CurEnergyId },
            Effect = new float[] { 40f, 40f },
            Weight = 0.4f,
            MinVal = 210,
            MaxVal = 210,
            Value = 210,
            Sprite = Resources.Load<Sprite>(Sprites + "SmallRejuvenationPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Nordic Helm
        new Item()
        {
            Rank = Elite,
            Type = Head,
            Kind = "Nordic Helm",
            Desc = "Increases hero health and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.ResistMagicId },
            Effect = new float[] { 8f, 8f },
            Weight = 12f,
            MinVal = 540,
            MaxVal = 540,
            Value = 540,
            Sprite = Resources.Load<Sprite>(Sprites + "NordicHelm"),
            Pos = new Vector3(0f, 0.0501f, 0.015f),
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Great Helmet
        new Item()
        {
            Rank = Elite,
            Type = Head,
            Kind = "Great Helmet",
            Desc = "Increases hero health and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.ResistMagicId },
            Effect = new float[] { 10f, 10f },
            Weight = 13f,
            MinVal = 660,
            MaxVal = 660,
            Value = 660,
            Sprite = Resources.Load<Sprite>(Sprites + "GreatHelmet"),
            Pos = new Vector3(0f, 0.02f, 0.047f),
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Heavy Leather Armor
        new Item()
        {
            Rank = Elite,
            Type = Torso,
            Kind = "Heavy Leather Armor",
            Desc = "Increases hero health, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 8f, 8f, 8f },
            Weight = 27f,
            MinVal = 930,
            MaxVal = 930,
            Value = 930,
            Sprite = Resources.Load<Sprite>(Sprites + "HeavyLeatherArmor"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Mail With Tabard
        new Item()
        {
            Rank = Elite,
            Type = Torso,
            Kind = "Mail With Tabard",
            Desc = "Increases hero health, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 10f, 10f, 10f },
            Weight = 30f,
            MinVal = 1200,
            MaxVal = 1200,
            Value = 1200,
            Sprite = Resources.Load<Sprite>(Sprites + "MailWithTabard"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Nordic Sword
        new Item()
        {
            Rank = Elite,
            Type = RightHand,
            Kind = "Nordic Sword",
            Desc = "Increases hero damage and attack rate",
            Bonus = new string[] { HeroClass.MaxDamageId + Bonus, HeroClass.AttackRateId + Bonus },
            Stats = new string[] { HeroClass.MaxDamageId, HeroClass.AttackRateId },
            Effect = new float[] { 8f, 0.008f },
            Weight = 6.5f,
            MinVal = 540,
            MaxVal = 540,
            Value = 540,
            Sprite = Resources.Load<Sprite>(Sprites + "NordicSword"),
            Pos = new Vector3(0.001f, 0.057f, 0.0321f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = Green
        },
        // Long Sword
        new Item()
        {
            Rank = Elite,
            Type = RightHand,
            Kind = "Long Sword",
            Desc = "Increases hero damage and attack rate",
            Bonus = new string[] { HeroClass.MaxDamageId + Bonus, HeroClass.AttackRateId + Bonus },
            Stats = new string[] { HeroClass.MaxDamageId, HeroClass.AttackRateId },
            Effect = new float[] { 10f, 0.01f },
            Weight = 7f,
            MinVal = 660,
            MaxVal = 660,
            Value = 660,
            Sprite = Resources.Load<Sprite>(Sprites + "LongSword"),
            Pos = new Vector3(-0.0064f, 0.0571f, 0.0266f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = Green
        },
        // Ornate Shield
        new Item()
        {
            Rank = Elite,
            Type = LeftHand,
            Kind = "Ornate Shield",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 8f, 8f },
            Weight = 14.5f,
            MinVal = 540,
            MaxVal = 540,
            Value = 540,
            Sprite = Resources.Load<Sprite>(Sprites + "OrnateShield"),
            Pos = new Vector3(0.004f, -0.03f, -0.05f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = Green
        },
        // Knight Shield
        new Item()
        {
            Rank = Elite,
            Type = LeftHand,
            Kind = "Knight Shield",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 10f, 10f },
            Weight = 15f,
            MinVal = 660,
            MaxVal = 660,
            Value = 660,
            Sprite = Resources.Load<Sprite>(Sprites + "KnightShield"),
            Pos = new Vector3(-0.057f, -0.03f, -0.05f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = Green
        },
        // Mail Boots
        new Item()
        {
            Rank = Elite,
            Type = Feet,
            Kind = "Mail Boots",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 8f, 8f },
            Weight = 11.5f,
            MinVal = 540,
            MaxVal = 540,
            Value = 540,
            Sprite = Resources.Load<Sprite>(Sprites + "MailBoots"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        },
        // Splinted Greaves
        new Item()
        {
            Rank = Elite,
            Type = Feet,
            Kind = "Splinted Greaves",
            Desc = "Increases hero health and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId },
            Effect = new float[] { 10f, 10f },
            Weight = 12.5f,
            MinVal = 660,
            MaxVal = 660,
            Value = 660,
            Sprite = Resources.Load<Sprite>(Sprites + "SplintedGreaves"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Green
        }
    };

    // Legendary items
    public static readonly Item[] LegendaryItems = new Item[]
    {
        // Rejuvenation Potion
        new Item()
        {
            Rank = Legendary,
            Type = Potion,
            Kind = "Rejuvenation Potion",
            Desc = "Replenishes a hero life and energy",
            Bonus = new string[] { HeroClass.CurHealthId + Bonus, HeroClass.CurEnergyId + Bonus },
            Stats = new string[] { HeroClass.CurHealthId, HeroClass.CurEnergyId },
            Effect = new float[] { 60f, 60f },
            Weight = 0.7f,
            MinVal = 510,
            MaxVal = 510,
            Value = 510,
            Sprite = Resources.Load<Sprite>(Sprites + "RejuvenationPotion"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Sallet
        new Item()
        {
            Rank = Legendary,
            Type = Head,
            Kind = "Sallet",
            Desc = "Increases hero energy, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxEnergyId + Bonus, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus},
            Stats = new string[] { HeroClass.MaxEnergyId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 18f, 18f, 18f },
            Weight = 14.5f,
            MinVal = 1620,
            MaxVal = 1620,
            Value = 1620,
            Sprite = Resources.Load<Sprite>(Sprites + "Sallet"),
            Pos = new Vector3(0f, 0.023f, 0.0194f),
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Paladin Helm
        new Item()
        {
            Rank = Legendary,
            Type = Head,
            Kind = "Paladin Helm",
            Desc = "Increases hero energy, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxEnergyId + Bonus, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus},
            Stats = new string[] { HeroClass.MaxEnergyId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 22f, 22f, 22f },
            Weight = 15f,
            MinVal = 1950,
            MaxVal = 1950,
            Value = 1950,
            Sprite = Resources.Load<Sprite>(Sprites + "PaladinHelm"),
            Pos = new Vector3(0f, 0.02f, 0.04f),
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Brass Armor
        new Item()
        {
            Rank = Legendary,
            Type = Torso,
            Kind = "Brass Armor",
            Desc = "Increases hero health, energy, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.MaxEnergyId + Bonus,
                HeroClass.DefenceId + Bonus, HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.MaxEnergyId,
                HeroClass.DefenceId, HeroClass.ResistMagicId},
            Effect = new float[] { 18f, 18f, 18f, 18f },
            Weight = 37f,
            MinVal = 2880,
            MaxVal = 2880,
            Value = 2880,
            Sprite = Resources.Load<Sprite>(Sprites + "BrassArmor"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Paladin Armor
        new Item()
        {
            Rank = Legendary,
            Type = Torso,
            Kind = "Paladin Armor",
            Desc = "Increases hero health, energy, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.MaxEnergyId + Bonus,
                HeroClass.DefenceId + Bonus, HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.MaxEnergyId,
                HeroClass.DefenceId, HeroClass.ResistMagicId},
            Effect = new float[] { 20f, 20f, 20f, 20f },
            Weight = 40f,
            MinVal = 3300,
            MaxVal = 3300,
            Value = 3300,
            Sprite = Resources.Load<Sprite>(Sprites + "PaladinArmor"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Obsidian Sword
        new Item()
        {
            Rank = Legendary,
            Type = RightHand,
            Kind = "Obsidian Sword",
            Desc = "Increases hero damage and attack rate",
            Bonus = new string[] { HeroClass.MinDamageId + Bonus, HeroClass.MaxDamageId + Bonus,
                HeroClass.AttackRateId + Bonus },
            Stats = new string[] { HeroClass.MinDamageId, HeroClass.MaxDamageId, HeroClass.AttackRateId },
            Effect = new float[] { 18f, 24f, 0.018f },
            Weight = 7.5f,
            MinVal = 1620,
            MaxVal = 1620,
            Value = 1620,
            Sprite = Resources.Load<Sprite>(Sprites + "ObsidianSword"),
            Pos = new Vector3(0.011f, 0.061f, 0.033f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = Orange
        },
        // Paladin Sword
        new Item()
        {
            Rank = Legendary,
            Type = RightHand,
            Kind = "Paladin Sword",
            Desc = "Increases hero damage and attack rate",
            Bonus = new string[] { HeroClass.MinDamageId + Bonus, HeroClass.MaxDamageId + Bonus,
                HeroClass.AttackRateId + Bonus },
            Stats = new string[] { HeroClass.MinDamageId, HeroClass.MaxDamageId, HeroClass.AttackRateId },
            Effect = new float[] { 22f, 28f, 0.022f },
            Weight = 8f,
            MinVal = 1950,
            MaxVal = 1950,
            Value = 1950,
            Sprite = Resources.Load<Sprite>(Sprites + "PaladinSword"),
            Pos = new Vector3(0.04f, 0.061f, 0.033f),
            Rot = new Vector3(0f, -90f, 90f),
            HintColor = Orange
        },
        // Ancient Shield
        new Item()
        {
            Rank = Legendary,
            Type = LeftHand,
            Kind = "Ancient Shield",
            Desc = "Increases hero health, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 18f, 18f, 18f },
            Weight = 15.5f,
            MinVal = 1620,
            MaxVal = 1620,
            Value = 1620,
            Sprite = Resources.Load<Sprite>(Sprites + "AncientShield"),
            Pos = new Vector3(-0.112f, -0.065f, -0.063f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = Orange
        },
        // Paladin Shield
        new Item()
        {
            Rank = Legendary,
            Type = LeftHand,
            Kind = "Paladin Shield",
            Desc = "Increases hero health, defence and resist magic",
            Bonus = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId + Bonus,
                HeroClass.ResistMagicId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.DefenceId, HeroClass.ResistMagicId },
            Effect = new float[] { 22f, 22f, 22f },
            Weight = 16f,
            MinVal = 1950,
            MaxVal = 1950,
            Value = 1950,
            Sprite = Resources.Load<Sprite>(Sprites + "PaladinShield"),
            Pos = new Vector3(-0.063f, -0.058f, -0.052f),
            Rot = new Vector3(0f, 90f, -90f),
            HintColor = Orange
        },
        // Brass Legs
        new Item()
        {
            Rank = Legendary,
            Type = Feet,
            Kind = "Brass Legs",
            Desc = "Increases hero health, energy and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.MaxEnergyId + Bonus,
                HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.MaxEnergyId, HeroClass.DefenceId },
            Effect = new float[] { 18f, 18f, 18f },
            Weight = 16.5f,
            MinVal = 1620,
            MaxVal = 1620,
            Value = 1620,
            Sprite = Resources.Load<Sprite>(Sprites + "BrassLegs"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        },
        // Paladin Greaves
        new Item()
        {
            Rank = Legendary,
            Type = Feet,
            Kind = "Paladin Greaves",
            Desc = "Increases hero health, energy and defence",
            Bonus = new string[] { HeroClass.MaxHealthId + Bonus, HeroClass.MaxEnergyId + Bonus,
                HeroClass.DefenceId + Bonus },
            Stats = new string[] { HeroClass.MaxHealthId, HeroClass.MaxEnergyId, HeroClass.DefenceId },
            Effect = new float[] { 22f, 22f, 22f },
            Weight = 17f,
            MinVal = 1950,
            MaxVal = 1950,
            Value = 1950,
            Sprite = Resources.Load<Sprite>(Sprites + "PaladinGreaves"),
            Pos = Vector3.zero,
            Rot = Vector3.zero,
            HintColor = Orange
        }
    };

    // Get proper indexes by gold rank
    public static int[] GetMoneyIndex(string itemRank)
    {
        // Prepare list
        List<int> indexList = new List<int>();
        // Search proper item
        for (int cnt = 0; cnt < Money.Length; cnt++)
            // Check item rank
            if (Money[cnt].Rank.Equals(itemRank))
                // Add index to list
                indexList.Add(cnt);
        // Convert list to array
        int[] indexArr = indexList.ToArray();
        // Return indexes
        return indexArr;
    }
}