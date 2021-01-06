using UnityEngine;

/// <summary>
/// Stores information about individual songs and their parameters.
/// </summary>
public static class MusicDatabase
{
    // Music path
    public static readonly string Music = "Music/";
    // Main Menu
    public static readonly string MainMenu = "MainMenu";
    // Credits
    public static readonly string Credits = "Credits";
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
        // Credits
        new Song
        {
            Name = Credits,
            Audio = Resources.Load<AudioClip>(Music + Credits)
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

    /// <summary>
    /// Gets proper song from the database.
    /// </summary>
    /// <param name="name">A label that represents the name of the song.</param>
    /// <param name="songs">The structures that represent the songs.</param>
    /// <returns>
    /// The obtained audio clip.
    /// </returns>
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