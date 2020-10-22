using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    // Audio source
    public AudioSource AudioSrc { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        AudioSrc = GetComponent<AudioSource>();
        AudioSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.MainMenu, MusicDatabase.Songs);
        AudioSrc.PlayDelayed(1f);
    }
}