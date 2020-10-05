using UnityEngine;

public class HeroSound : MonoBehaviour
{
    // Audio source
    public AudioSource AudioSrc { get; set; }
    // Hero sounds
    public SoundDatabase.Sound[] HeroSounds { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        // Paladin
        if (name.Equals(HeroDatabase.Paladin))
            // Copy sounds from database
            HeroSounds = (SoundDatabase.Sound[])SoundDatabase.PaladinSounds.Clone();
        AudioSrc = GetComponent<AudioSource>();
    }

    // Play attack sound during attack
    private void PlayAttackSound()
    {
        // Play audio
        AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(SoundDatabase.Attack, HeroSounds));
    }
}