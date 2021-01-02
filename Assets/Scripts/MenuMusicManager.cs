using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    // Sounds source
    public AudioSource SoundsSrc { get; set; }
    // Music source
    public AudioSource MusicSrc { get; set; }
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
        SoundsSrc = GameObject.Find("SoundsSource").GetComponent<AudioSource>();
        MusicSrc = GameObject.Find("MusicSource").GetComponent<AudioSource>();
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
            MusicSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.Credits, MusicDatabase.Songs);
            // Play song
            MusicSrc.Play();
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
            MusicSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.MainMenu, MusicDatabase.Songs);
            // Play song
            MusicSrc.Play();
            // Set that credits is active
            _isCredits = true;
            // Set that menu is inactive
            _isMenu = false;
        }
    }

    // Change sound volume value
    public void AdaptSoundVolume()
    {
        // Prepare volume label
        int soundsValue = (int)Mathf.Round(_menuInterface.SoundSliderSld.value * 100f);
        // Set proper label
        _menuInterface.CurSoundsTxt.text = soundsValue + "%";
        // Seach audio sources
        SoundsSrc.volume = _menuInterface.SoundSliderSld.value;
    }

    // Change music volume value
    public void AdaptMusicVolume()
    {
        // Prepare volume label
        int musicValue = (int)Mathf.Round(_menuInterface.MusicSliderSld.value * 100f);
        // Set proper label
        _menuInterface.CurMusicTxt.text = musicValue + "%";
        // Change music volume
        MusicSrc.volume = _menuInterface.MusicSliderSld.value;
    }
}