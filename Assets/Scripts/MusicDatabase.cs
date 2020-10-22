﻿using UnityEngine;

public static class MusicDatabase
{
    // Music path
    public static readonly string Music = "Music/";
    // Main Menu
    public static readonly string MainMenu = "MainMenu";
    // Refugee Camp
    public static readonly string RefugeeCamp = "RefugeeCamp";
    // Stony Plain
    public static readonly string StonyPlain = "StonyPlain";
    // Death Valley
    public static readonly string DeathValley = "DeathValley";
    // Hell Pit
    public static readonly string HellPit = "HellPit";
    // Death
    public static readonly string Death = "Death";

    // Song structure
    public struct Song
    {
        public string Name { get; set; }
        public AudioClip Audio { get; set; }
    }

    // Songs
    public static readonly Song[] Songs = new Song[]
    {
        // Main Menu
        new Song
        {
            Name = MainMenu,
            Audio = Resources.Load<AudioClip>(Music + MainMenu)
        },
        // Refugee
        new Song
        {
            Name = RefugeeCamp,
            Audio = Resources.Load<AudioClip>(Music + RefugeeCamp)
        },
        // Stony plain
        new Song
        {
            Name = StonyPlain,
            Audio = Resources.Load<AudioClip>(Music + StonyPlain)
        },
        // Death Valley
        new Song
        {
            Name = DeathValley,
            Audio = Resources.Load<AudioClip>(Music + DeathValley)
        },
        // Hell Pit
        new Song
        {
            Name = HellPit,
            Audio = Resources.Load<AudioClip>(Music + HellPit)
        },
        // Death
        new Song
        {
            Name = Death,
            Audio = Resources.Load<AudioClip>(Music + Death)
        }
    };

    // Get proper song from songs
    public static AudioClip GetProperSong(string name, Song[] songs)
    {
        // Reset counter
        int cnt = 0;
        // Search proper song
        for (; cnt < songs.Length; cnt++)
            // Check song name
            if (songs[cnt].Name.Equals(name))
                // Break action
                break;
        // Return proper song
        return songs[cnt].Audio;
    }
}