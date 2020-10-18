using UnityEngine;

public static class SoundDatabase
{
    // Sounds path
    public static readonly string Sounds = "Sounds/";
    // Combat
    public static readonly string Attack = "Attack";
    public static readonly string Hit = "Hit";
    public static readonly string Grunt0 = "Grunt0";
    public static readonly string Death = "Death";
    // Speech
    public static readonly string Impossible = "Impossible";
    public static readonly string ICantCarryAnymore = "ICantCarryAnymore";
    // Containers
    public static readonly string LooseRock = "LooseRock";
    public static readonly string Chest = "Chest";
    public static readonly string Remains = "Remains";
    // Items
    public static readonly string Click = "Click";
    public static readonly string PickUp = "PickUp";
    public static readonly string Gulp = "Gulp";
    public static readonly string Gold = "Gold";
    public static readonly string Swoosh = "Swoosh";
    public static readonly string Potion = "Potion";
    public static readonly string Headgear = "Headgear";
    public static readonly string Armor = "Armor";
    public static readonly string Shield = "Shield";
    public static readonly string Weapon = "Weapon";
    public static readonly string Footwear = "Footwear";
    public static readonly string Level = "Level";
    // Skills
    public static readonly string Prayer = "Prayer";
    public static readonly string HolyZeal = "HolyZeal";
    public static readonly string Sanctuary = "Sanctuary";
    public static readonly string DivineAnger = "DivineAnger";

    // Sound structure
    public struct Sound
    {
        public string Name { get; set; }
        public AudioClip Audio { get; set; }
    }

    // Container sounds
    public static readonly Sound[] ContainerSounds = new Sound[]
    {
        // Loose Rock
        new Sound
        {
            Name = LooseRock,
            Audio = Resources.Load<AudioClip>(Sounds + LooseRock)
        },
        // Chest
        new Sound
        {
            Name = Chest,
            Audio = Resources.Load<AudioClip>(Sounds + Chest)
        },
        // Remains
        new Sound
        {
            Name = Remains,
            Audio = Resources.Load<AudioClip>(Sounds + Remains)
        }
    };

    // Item sounds
    public static readonly Sound[] ItemSounds = new Sound[]
    {
        // Headgear
        new Sound
        {
            Name = Headgear,
            Audio = Resources.Load<AudioClip>(Sounds + Headgear)
        },
        // Armor
        new Sound
        {
            Name = Armor,
            Audio = Resources.Load<AudioClip>(Sounds + Armor)
        },
        // Shield
        new Sound
        {
            Name = Shield,
            Audio = Resources.Load<AudioClip>(Sounds + Shield)
        },
        // Weapon
        new Sound
        {
            Name = Weapon,
            Audio = Resources.Load<AudioClip>(Sounds + Weapon)
        },
        // Footwear
        new Sound
        {
            Name = Footwear,
            Audio = Resources.Load<AudioClip>(Sounds + Footwear)
        },
        // Potion
        new Sound
        {
            Name = Potion,
            Audio = Resources.Load<AudioClip>(Sounds + Potion)
        },
        // Gulp
        new Sound
        {
            Name = Gulp,
            Audio = Resources.Load<AudioClip>(Sounds + Gulp)
        },
        // Click
        new Sound
        {
            Name = Click,
            Audio = Resources.Load<AudioClip>(Sounds + Click)
        },
        // Pick up
        new Sound
        {
            Name = PickUp,
            Audio = Resources.Load<AudioClip>(Sounds + PickUp)
        },
        // Gold
        new Sound
        {
            Name = Gold,
            Audio = Resources.Load<AudioClip>(Sounds + Gold)
        },
        // Swoosh
        new Sound
        {
            Name = Swoosh,
            Audio = Resources.Load<AudioClip>(Sounds + Swoosh)
        },
        // Level
        new Sound
        {
            Name = Level,
            Audio = Resources.Load<AudioClip>(Sounds + Level)
        }
    };

    // Paladin sounds
    public static readonly Sound[] PaladinSounds = new Sound[]
    {
        // Prayer
        new Sound
        {
            Name = Prayer,
            Audio = Resources.Load<AudioClip>(Sounds + Prayer)
        },
        // Holy Zeal
        new Sound
        {
            Name = HolyZeal,
            Audio = Resources.Load<AudioClip>(Sounds + HolyZeal)
        },
        // Sanctuary
        new Sound
        {
            Name = Sanctuary,
            Audio = Resources.Load<AudioClip>(Sounds + Sanctuary)
        },
        // Divine Anger
        new Sound
        {
            Name = DivineAnger,
            Audio = Resources.Load<AudioClip>(Sounds + DivineAnger)
        },
        // Impossible
        new Sound
        {
            Name = Impossible,
            Audio = Resources.Load<AudioClip>(Sounds + Impossible)
        },
        // I can't carry anymore
        new Sound
        {
            Name = ICantCarryAnymore,
            Audio = Resources.Load<AudioClip>(Sounds + ICantCarryAnymore)
        },
        // Attack
        new Sound
        {
            Name = Attack,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Attack)
        },
        // Hit
        new Sound
        {
            Name = Hit,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Hit)
        },
        // Grunt 0
        new Sound
        {
            Name = Grunt0 + "0",
            Audio = Resources.Load<AudioClip>(Sounds + "Male" + Grunt0 + "0")
        },
        // Grunt 1
        new Sound
        {
            Name = Grunt0 + "1",
            Audio = Resources.Load<AudioClip>(Sounds + "Male" + Grunt0 + "1")
        },
        // Grunt 2
        new Sound
        {
            Name = Grunt0 + "2",
            Audio = Resources.Load<AudioClip>(Sounds + "Male" + Grunt0 + "2")
        },
        // Death
        new Sound
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Sounds + "Male" + Death)
        }
    };

    // Undead sounds
    public static readonly Sound[] UndeadSounds = new Sound[]
    {
        // Attack
        new Sound
        {
            Name = Attack,
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Attack)
        },
        // Hit
        new Sound
        {
            Name = Hit,
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Hit)
        },
        // Grunt 0
        new Sound
        {
            Name = Grunt0 + "0",
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Grunt0 + "0")
        },
        // Grunt 1
        new Sound
        {
            Name = Grunt0 + "1",
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Grunt0 + "1")
        },
        // Grunt 2
        new Sound
        {
            Name = Grunt0 + "2",
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Grunt0 + "2")
        },
        // Death
        new Sound
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Sounds + "Undead" + Death)
        }
    };

    // Skeleton sounds
    public static readonly Sound[] SkeletonSounds = new Sound[]
    {
        // Attack
        new Sound
        {
            Name = Attack,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Attack)
        },
        // Hit
        new Sound
        {
            Name = Hit,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Hit)
        },
        // Grunt 0
        new Sound
        {
            Name = Grunt0 + "0",
            Audio = Resources.Load<AudioClip>(Sounds + "Skeleton" + Grunt0 + "0")
        },
        // Grunt 1
        new Sound
        {
            Name = Grunt0 + "1",
            Audio = Resources.Load<AudioClip>(Sounds + "Skeleton" + Grunt0 + "1")
        },
        // Grunt 2
        new Sound
        {
            Name = Grunt0 + "2",
            Audio = Resources.Load<AudioClip>(Sounds + "Skeleton" + Grunt0 + "2")
        },
        // Death
        new Sound
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Sounds + "Skeleton" + Death)
        }
    };

    // Genie sounds
    public static readonly Sound[] GenieSounds = new Sound[]
    {
        // Attack
        new Sound
        {
            Name = Attack,
            Audio = Resources.Load<AudioClip>(Sounds + "Magic" + Attack)
        },
        // Hit
        new Sound
        {
            Name = Hit,
            Audio = Resources.Load<AudioClip>(Sounds + "Magic" + Hit)
        },
        // Grunt 0
        new Sound
        {
            Name = Grunt0 + "0",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "0")
        },
        // Grunt 1
        new Sound
        {
            Name = Grunt0 + "1",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "1")
        },
        // Grunt 2
        new Sound
        {
            Name = Grunt0 + "2",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "2")
        },
        // Death
        new Sound
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Death)
        }
    };

    // Demon sounds
    public static readonly Sound[] DemonSounds = new Sound[]
    {
        // Attack
        new Sound
        {
            Name = Attack,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Attack)
        },
        // Hit
        new Sound
        {
            Name = Hit,
            Audio = Resources.Load<AudioClip>(Sounds + "Weapon" + Hit)
        },
        // Grunt 0
        new Sound
        {
            Name = Grunt0 + "0",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "0")
        },
        // Grunt 1
        new Sound
        {
            Name = Grunt0 + "1",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "1")
        },
        // Grunt 2
        new Sound
        {
            Name = Grunt0 + "2",
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Grunt0 + "2")
        },
        // Death
        new Sound
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Sounds + "Demon" + Death)
        }
    };

    // Get proper sound from sounds
    public static AudioClip GetProperSound(string name, Sound[] sounds)
    {
        // Reset counter
        int cnt = 0;
        // Search proper sound
        for (; cnt < sounds.Length; cnt++)
            // Check sound name
            if (sounds[cnt].Name.Equals(name))
                // Break action
                break;
        // Return proper sound
        return sounds[cnt].Audio;
    }

    // Get grunts amount
    public static int GetGruntsAmt(Sound[] sounds)
    {
        // Reset amount
        int amt = 0;
        // Search grunts
        for (int cnt = 0; cnt < sounds.Length; cnt++)
            // Check if it is grunt
            if (sounds[cnt].Name.Contains(SoundDatabase.Grunt0))
                // Increment amount
                amt++;
        // Return proper amount
        return amt;
    }
}