using UnityEngine;

/// <summary>
/// Controls the sounds made by enemies.
/// </summary>
public class EnemySound : MonoBehaviour
{
    // Audio source
    public AudioSource AudioSrc { get; set; }
    // Enemy sounds
    public SoundDatabase.Sound[] EnemySounds { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        // Undeads
        if (name.Equals(EnemyDatabase.Zombie) || name.Equals(EnemyDatabase.Rotfiend))
            // Copy sounds from database
            EnemySounds = (SoundDatabase.Sound[])SoundDatabase.UndeadSounds.Clone();
        // Skeletons
        if (name.Equals(EnemyDatabase.Skeleton) || name.Equals(EnemyDatabase.DemonSkeleton))
            // Copy sounds from database
            EnemySounds = (SoundDatabase.Sound[])SoundDatabase.SkeletonSounds.Clone();
        // Genies
        if (name.Equals(EnemyDatabase.Efreeti))
            // Copy sounds from database
            EnemySounds = (SoundDatabase.Sound[])SoundDatabase.GenieSounds.Clone();
        // Demons
        if (name.Equals(EnemyDatabase.PitFiend) || name.Equals(EnemyDatabase.HellKnight))
            // Copy sounds from database
            EnemySounds = (SoundDatabase.Sound[])SoundDatabase.DemonSounds.Clone();
        AudioSrc = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays proper enemy sound during an attack.
    /// </summary>
    private void PlayAttackSound()
    {
        // Play audio
        AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(SoundDatabase.Attack, EnemySounds));
    }
}