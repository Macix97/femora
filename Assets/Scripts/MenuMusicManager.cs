using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    // Audio source
    public AudioSource AudioSrc { get; set; }
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
        AudioSrc = GetComponent<AudioSource>();
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
            AudioSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.Credits, MusicDatabase.Songs);
            // Play song
            AudioSrc.Play();
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
            AudioSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.MainMenu, MusicDatabase.Songs);
            // Play song
            AudioSrc.Play();
            // Set that credits is active
            _isCredits = true;
            // Set that menu is inactive
            _isMenu = false;
        }
    }
}