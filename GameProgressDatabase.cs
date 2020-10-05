using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameProgressDatabase
{
    // Saves path
    public static readonly string Saves = "/Saves/";
    // Game progress
    public static readonly string GameProgress = "/save";
    // Saved game format
    public static readonly string DatFormat = ".dat";
    // Heroes limit
    public static readonly int HeroesLimit = 8;

    // Hero name inserted in character creator
    public static string HeroName { get; set; }

    // Save 
    public static Save GameSave;

    // Save structure
    [Serializable]
    public struct Save
    {
        // Class
        public string Name { get; set; }
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
        public float ActionChanceBonus { get; set; }
        public int Defence { get; set; }
        public int ResistMagic { get; set; }
        public float Capacity { get; set; }
        public string LastEnemyType { get; set; }
        public int LastEnemyLvl { get; set; }
        // Characters (simplified people)
        public Character[] Characters { get; set; }
        // Abilities (simplified skills)
        public Ability[] Abilities { get; set; }
        // Inventory spots (simplified inventory slots)
        public Spot[] Inv { get; set; }
        // Potion spots (simplified potion slots)
        public Spot[] Potions { get; set; }
        // Equipment spots (simplified equipment slots)
        internal Spot Head;
        internal Spot Torso;
        internal Spot RightHand;
        internal Spot LeftHand;
        internal Spot Feet;
        // Others
        public int ActiveInvSpots { get; set; }
        public int ActivePotionSpots { get; set; }
        public int Gold { get; set; }
        public int QualityLevel { get; set; }
        public float SoundVolume { get; set; }
        public float MusicVolume { get; set; }
        public float ExpMod { get; set; }
        public float AttackAnimSpeed { get; set; }
        public bool IsHair { get; set; }
        public bool IsOrdinary { get; set; }
        public bool IsElite { get; set; }
        public bool IsLegendary { get; set; }
    }

    // Character structure (simplified person)
    [Serializable]
    public struct Character
    {
        public bool IsVisited { get; set; }
    }

    // Ability structure (simplified skill)
    [Serializable]
    public struct Ability
    {
        public int Level { get; set; }
        public string[] Stats { get; set; }
        public float Effect { get; set; }
        public float EnergyCost { get; set; }
    }

    // Spot structure (simplified slot)
    [Serializable]
    public struct Spot
    {
        public string ItemName;
        public bool IsSpotActive;
    }

    // Copy proper info about hero class to save
    public static void CopyClassToSave(HeroClass heroClass)
    {
        // Copy properties to save
        GameSave.Name = heroClass.Name;
        GameSave.Level = heroClass.Level;
        GameSave.TotalExp = heroClass.TotalExp;
        GameSave.NextLvLExp = heroClass.NextLvLExp;
        GameSave.LvlStart = heroClass.LvlStart;
        GameSave.LvlEnd = heroClass.LvlEnd;
        GameSave.AttributePts = heroClass.AttributePts;
        GameSave.SkillPts = heroClass.SkillPts;
        GameSave.SpentSkillPts = heroClass.SpentSkillPts;
        GameSave.Vitality = heroClass.Vitality;
        GameSave.Wisdom = heroClass.Wisdom;
        GameSave.Strength = heroClass.Strength;
        GameSave.Agility = heroClass.Agility;
        GameSave.MaxHealth = heroClass.MaxHealth;
        GameSave.CurHealth = heroClass.CurHealth;
        GameSave.MaxEnergy = heroClass.MaxEnergy;
        GameSave.CurEnergy = heroClass.CurEnergy;
        GameSave.MaxDamage = heroClass.MaxDamage;
        GameSave.MinDamage = heroClass.MinDamage;
        GameSave.AttackRate = heroClass.AttackRate;
        GameSave.ActionChanceBonus = heroClass.ActionChanceBonus;
        GameSave.Defence = heroClass.Defence;
        GameSave.ResistMagic = heroClass.ResistMagic;
        GameSave.Capacity = heroClass.Capacity;
        GameSave.LastEnemyType = heroClass.LastEnemyType;
        GameSave.LastEnemyLvl = heroClass.LastEnemyLvl;
    }

    // Set hero properties from data file
    public static void ReadClassFromSave(ref HeroClass heroClass)
    {
        // Set properties from file
        heroClass.Name = GameSave.Name;
        heroClass.Level = GameSave.Level;
        heroClass.TotalExp = GameSave.TotalExp;
        heroClass.NextLvLExp = GameSave.NextLvLExp;
        heroClass.LvlStart = GameSave.LvlStart;
        heroClass.LvlEnd = GameSave.LvlEnd;
        heroClass.AttributePts = GameSave.AttributePts;
        heroClass.SkillPts = GameSave.SkillPts;
        heroClass.SpentSkillPts = GameSave.SpentSkillPts;
        heroClass.Vitality = GameSave.Vitality;
        heroClass.Wisdom = GameSave.Wisdom;
        heroClass.Strength = GameSave.Strength;
        heroClass.Agility = GameSave.Agility;
        heroClass.MaxHealth = GameSave.MaxHealth;
        heroClass.CurHealth = GameSave.CurHealth;
        heroClass.MaxEnergy = GameSave.MaxEnergy;
        heroClass.CurEnergy = GameSave.CurEnergy;
        heroClass.MaxDamage = GameSave.MaxDamage;
        heroClass.MinDamage = GameSave.MinDamage;
        heroClass.AttackRate = GameSave.AttackRate;
        heroClass.ActionChanceBonus = GameSave.ActionChanceBonus;
        heroClass.Defence = GameSave.Defence;
        heroClass.ResistMagic = GameSave.ResistMagic;
        heroClass.Capacity = GameSave.Capacity;
        heroClass.LastEnemyType = GameSave.LastEnemyType;
        heroClass.LastEnemyLvl = GameSave.LastEnemyLvl;
    }

    // Copy proper info about people classes to save
    public static void CopyCharactersToSave()
    {
        // Find all characters
        GameObject[] characters = GameObject.FindGameObjectsWithTag(PersonClass.PersonTag);
        // Initialize characters
        GameSave.Characters = new Character[PersonDatabase.People.Length];
        // Search characters
        for (int cnt = 0; cnt < PersonDatabase.People.Length; cnt++)
        {
            // Create new character
            GameSave.Characters[cnt] = new Character
            {
                // Copy properties to save file
                IsVisited = characters[cnt].GetComponent<PersonClass>().IsVisited
            };
        }
    }

    // Set people properties from data file
    public static void ReadCharactersFromSave()
    {
        // Find all characters
        GameObject[] characters = GameObject.FindGameObjectsWithTag(PersonClass.PersonTag);
        // Search characters
        for (int cnt = 0; cnt < PersonDatabase.People.Length; cnt++)
            // Copy property to save
            characters[cnt].GetComponent<PersonClass>().IsVisited = GameSave.Characters[cnt].IsVisited;
    }

    // Copy hero skills to save
    public static void CopySkillsToSave(HeroSkillDatabase.Skill[] skills)
    {
        // Initialize abilities
        GameSave.Abilities = new Ability[skills.Length];
        // Search skills
        for (int cnt = 0; cnt < skills.Length; cnt++)
            // Create new ability
            GameSave.Abilities[cnt] = new Ability
            {
                Level = skills[cnt].Level,
                Stats = skills[cnt].Stats,
                Effect = skills[cnt].Level,
                EnergyCost = skills[cnt].EnergyCost
            };
    }

    // Set hero skills from data file
    public static void ReadSkillsFromSave(ref HeroSkill heroSkill)
    {
        // Search skills
        for (int cnt = 0; cnt < heroSkill.HeroSkills.Length; cnt++)
        {
            // Set proper skill parameters
            heroSkill.HeroSkills[cnt].Level = GameSave.Abilities[cnt].Level;
            heroSkill.HeroSkills[cnt].Stats = GameSave.Abilities[cnt].Stats;
            heroSkill.HeroSkills[cnt].Effect = GameSave.Abilities[cnt].Effect;
            heroSkill.HeroSkills[cnt].EnergyCost = GameSave.Abilities[cnt].EnergyCost;
        }
    }

    // Copy hero inventory slots to save
    public static void CopyInventoryToSave(HeroInventory.Slot[] invSlots)
    {
        // Initialize spots
        GameSave.Inv = new Spot[invSlots.Length];
        // Search inventory slots
        for (int cnt = 0; cnt < invSlots.Length; cnt++)
        {
            // Create new inventory spot
            GameSave.Inv[cnt] = new Spot
            {
                ItemName = invSlots[cnt].ItemName,
                IsSpotActive = invSlots[cnt].IsSlotActive
            };
        }
    }

    // Set hero inventory from data file
    public static void ReadInventoryFromSave(ref HeroInventory heroInventory)
    {
        // Search inventory slots
        for (int cnt = 0; cnt < heroInventory.HeroInv.Length; cnt++)
        {
            // Set proper inventory parameters
            heroInventory.HeroInv[cnt].ItemName = GameSave.Inv[cnt].ItemName;
            heroInventory.HeroInv[cnt].IsSlotActive = GameSave.Inv[cnt].IsSpotActive;
            // Check if slot is active
            if (!heroInventory.HeroInv[cnt].IsSlotActive)
                // Initialize item
                heroInventory.InitSlotItem(heroInventory.HeroInv[cnt]);
        }
    }

    // Copy hero potion slots to save
    public static void CopyPotionsToSave(HeroInventory.Slot[] potionSlots)
    {
        // Initialize spots
        GameSave.Potions = new Spot[potionSlots.Length];
        // Search potion slots
        for (int cnt = 0; cnt < potionSlots.Length; cnt++)
            // Create new potion spot
            GameSave.Potions[cnt] = new Spot
            {
                ItemName = potionSlots[cnt].ItemName,
                IsSpotActive = potionSlots[cnt].IsSlotActive
            };
    }

    // Set hero potions from data file
    public static void ReadPotionsFromSave(ref HeroInventory heroInventory)
    {
        // Search potion slots
        for (int cnt = 0; cnt < heroInventory.HeroPotions.Length; cnt++)
        {
            // Set proper inventory parameters
            heroInventory.HeroPotions[cnt].ItemName = GameSave.Potions[cnt].ItemName;
            heroInventory.HeroPotions[cnt].IsSlotActive = GameSave.Potions[cnt].IsSpotActive;
            // Check if slot is active
            if (!heroInventory.HeroPotions[cnt].IsSlotActive)
                // Initialize item
                heroInventory.InitSlotItem(heroInventory.HeroPotions[cnt]);
        }
    }

    // Copy hero equipment to save
    public static void CopyEquipmentToSave(HeroInventory heroInventory)
    {
        // Head
        GameSave.Head = new Spot
        {
            ItemName = heroInventory.HeadSlot.ItemName,
            IsSpotActive = heroInventory.HeadSlot.IsSlotActive
        };
        // Torso
        GameSave.Torso = new Spot
        {
            ItemName = heroInventory.TorsoSlot.ItemName,
            IsSpotActive = heroInventory.TorsoSlot.IsSlotActive
        };
        // Right hand
        GameSave.RightHand = new Spot
        {
            ItemName = heroInventory.RightHandSlot.ItemName,
            IsSpotActive = heroInventory.RightHandSlot.IsSlotActive
        };
        // Left hand
        GameSave.LeftHand = new Spot
        {
            ItemName = heroInventory.LeftHandSlot.ItemName,
            IsSpotActive = heroInventory.LeftHandSlot.IsSlotActive
        };
        // Feet
        GameSave.Feet = new Spot
        {
            ItemName = heroInventory.FeetSlot.ItemName,
            IsSpotActive = heroInventory.FeetSlot.IsSlotActive
        };
    }

    // Set hero equipment from data file
    public static void ReadEquipmentFromSave(ref HeroInventory heroInventory)
    {
        // Head
        heroInventory.HeadSlot.ItemName = GameSave.Head.ItemName;
        heroInventory.HeadSlot.IsSlotActive = GameSave.Head.IsSpotActive;
        // Check if slot is active
        if (!heroInventory.HeadSlot.IsSlotActive)
            // Initialize item
            heroInventory.InitSlotItem(heroInventory.HeadSlot);
        // Torso
        heroInventory.TorsoSlot.ItemName = GameSave.Torso.ItemName;
        heroInventory.TorsoSlot.IsSlotActive = GameSave.Torso.IsSpotActive;
        // Check if slot is active
        if (!heroInventory.TorsoSlot.IsSlotActive)
            // Initialize item
            heroInventory.InitSlotItem(heroInventory.TorsoSlot);
        // Right hand
        heroInventory.RightHandSlot.ItemName = GameSave.RightHand.ItemName;
        heroInventory.RightHandSlot.IsSlotActive = GameSave.RightHand.IsSpotActive;
        // Check if slot is active
        if (!heroInventory.RightHandSlot.IsSlotActive)
            // Initialize item
            heroInventory.InitSlotItem(heroInventory.RightHandSlot);
        // Left hand
        heroInventory.LeftHandSlot.ItemName = GameSave.LeftHand.ItemName;
        heroInventory.LeftHandSlot.IsSlotActive = GameSave.LeftHand.IsSpotActive;
        // Check if slot is active
        if (!heroInventory.LeftHandSlot.IsSlotActive)
            // Initialize item
            heroInventory.InitSlotItem(heroInventory.LeftHandSlot);
        // Feet
        heroInventory.FeetSlot.ItemName = GameSave.Feet.ItemName;
        heroInventory.FeetSlot.IsSlotActive = GameSave.Feet.IsSpotActive;
        // Check if slot is active
        if (!heroInventory.FeetSlot.IsSlotActive)
            // Initialize item
            heroInventory.InitSlotItem(heroInventory.FeetSlot);
    }

    // Copy other parameters
    public static void CopyOtherParameters(HeroInventory heroInventory, HeroParameter heroParameter,
        GameInterface gameInterface)
    {
        // Copy values
        GameSave.ActiveInvSpots = heroInventory.ActiveInvSlots;
        GameSave.ActivePotionSpots = heroInventory.ActivePotionSlots;
        GameSave.Gold = heroInventory.Gold;
        GameSave.QualityLevel = gameInterface.QualityLevel;
        GameSave.SoundVolume = gameInterface.SoundSliderSld.value;
        GameSave.MusicVolume = gameInterface.MusicSliderSld.value;
        GameSave.ExpMod = heroParameter.ExpMod;
        GameSave.AttackAnimSpeed = heroParameter.AttackAnimSpeed;
        GameSave.IsHair = heroInventory.IsHair;
        GameSave.IsOrdinary = heroInventory.IsOrdinary;
        GameSave.IsElite = heroInventory.IsElite;
        GameSave.IsLegendary = heroInventory.IsLegendary;
    }

    // Set other parameters from file
    public static void ReadOtherParameters(ref HeroInventory heroInventory, ref HeroParameter heroParameter)
    {
        // Set proper values
        heroInventory.ActiveInvSlots = GameSave.ActiveInvSpots;
        heroInventory.ActivePotionSlots = GameSave.ActivePotionSpots;
        heroInventory.Gold = GameSave.Gold;
        heroParameter.ExpMod = GameSave.ExpMod;
        heroParameter.AttackAnimSpeed = GameSave.AttackAnimSpeed;
        heroInventory.IsHair = GameSave.IsHair;
        heroInventory.IsOrdinary = GameSave.IsOrdinary;
        heroInventory.IsElite = GameSave.IsElite;
        heroInventory.IsLegendary = GameSave.IsLegendary;
    }

    // Try keep game progress in file
    public static bool TrySaveGameToFile(string savePath, string heroName)
    {
        // Check operation result
        bool isSucceed;
        // Create new binary formater
        BinaryFormatter binary = new BinaryFormatter();
        // Check if save already exists
        if (File.Exists(savePath + Saves + heroName + GameProgress + DatFormat))
            // Delete save
            File.Delete(savePath + Saves + heroName + GameProgress + DatFormat);
        // Create save file
        FileStream saveFile = File.Create(savePath + Saves + heroName + GameProgress + DatFormat);
        // Try save data
        try
        {
            // Save hero inventory to file
            binary.Serialize(saveFile, GameSave);
            // Operation succeeded
            isSucceed = true;
        }
        catch
        {
            // Operation failed
            isSucceed = false;
        }
        // End action
        finally
        {
            // Close file
            saveFile.Close();
            // Reset game save variable
            GameProgressDatabase.GameSave = new GameProgressDatabase.Save();
        }
        // Return operation result
        return isSucceed;
    }

    // Try load game progress from file
    public static bool TryLoadGameFromFile(string savePath)
    {
        // Check operation result
        bool isSucceed;
        // Create new binary formater
        BinaryFormatter binary = new BinaryFormatter();
        // Check if file exists
        if (!File.Exists(savePath))
            // Operation failed
            return false;
        // Open save file
        FileStream saveFile = File.Open(savePath, FileMode.Open);
        // Initialize variables
        GameSave = new Save();
        GameSave.Characters = new Character[PersonDatabase.People.Length];
        GameSave.Abilities = new Ability[HeroSkillDatabase.PaladinSkills.Length];
        GameSave.Inv = new Spot[HeroInventory.InvSlots];
        GameSave.Potions = new Spot[HeroInventory.PotionSlots];
        GameSave.Head = new Spot();
        GameSave.Torso = new Spot();
        GameSave.RightHand = new Spot();
        GameSave.LeftHand = new Spot();
        GameSave.Feet = new Spot();
        // Try load data
        try
        {
            // Move data from file to variable
            GameSave = (Save)binary.Deserialize(saveFile);
            // Operation succeeded
            isSucceed = true;
        }
        // Catch exception
        catch
        {
            // Operation failed
            isSucceed = false;
        }
        // End action
        finally
        {
            // Close file
            saveFile.Close();
        }
        // Return operation result
        return isSucceed;
    }
}