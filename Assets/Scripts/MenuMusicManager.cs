using UnityEngine;
using UnityEngine.UI;

public class MenuMusicManager : MonoBehaviour
{
    // Audio sources (0 - sounds, 1 - music)
    public AudioSource[] AudioSources { get; set; }
    // Menu interface
    private MenuInterface _menuInterface;
    // Check if menu is active
    private bool _isMenu;
    // Check if credits is active
    private bool _isCredits;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayProperSong();
    }

    // Set basic parameters
    private void Init()
    {
        _isMenu = _isCredits = false;
        _menuInterface = GameObject.Find(MenuInterface.MenuInterfaceController).GetComponent<MenuInterface>();
        AudioSources = GetComponents<AudioSource>();
    }

    // Play proper song in menu
    private void PlayProperSong()
    {
        // Check if credits windows is active
        if (_menuInterface.CreditsMenuImg.IsActive())
        {
            // Check if menu is active
            if (_isMenu)
                // Break action
                return;
            // Set proper clip
            AudioSources[1].clip = MusicDatabase.GetProperSong(MusicDatabase.Credits, MusicDatabase.Songs);
            // Play song
            AudioSources[1].Play();
            // Set that menu is active
            _isMenu = true;
            // Set that credits is inactive
            _isCredits = false;
        }
        // Play main menu song
        else
        {
            // Check if menu is active
            if (_isCredits)
                // Break action
                return;
            // Set proper clip
            AudioSources[1].clip = MusicDatabase.GetProperSong(MusicDatabase.MainMenu, MusicDatabase.Songs);
            // Play song
            AudioSources[1].Play();
            // Set that credits is active
            _isCredits = true;
            // Set that menu is inactive
            _isMenu = false;
        }
    }

    // Change sound volume value
    public void AdaptSoundVolume()
    {
        // Seach audio sources
        AudioSources[0].volume = _menuInterface.SoundSliderSld.value;
    }

    // Change music volume value
    public void AdaptMusicVolume()
    {
        // Change music volume
        AudioSources[1].volume = _menuInterface.MusicSliderSld.value;
    }
}