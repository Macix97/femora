using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    // Audio source
    private AudioSource _audioSrc;
    // Hero class
    private HeroClass _heroClass;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Hero inventory
    private HeroInventory _heroInventory;
    // Game interface
    private GameInterface _gameInterface;
    // Check if death song is playing
    private bool _isDeath;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        SetProperSong();
    }

    // Set basic parameters
    private void Init()
    {
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _heroParameter = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroParameter>();
        _heroInventory = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroInventory>();
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.RefugeeCamp, MusicDatabase.Songs);
        _audioSrc.PlayDelayed(1f);
        _isDeath = false;
    }

    // Set proper song
    public void SetProperSong()
    {
        // Get current hero location
        string location = _heroClass.CurLocation.Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace);
        // Check if hero is dead
        if (_heroParameter.IsHeroDead())
        {
            // Check if death song is already playing
            if (_isDeath)
                // Break action
                return;
            // Adapt main text
            _gameInterface.MainInfoTxt.text =
                string.Format(GameInterface.Dead + "You have lost {0} gold.", _heroInventory.StealHeroGold());
            // Display new text
            _gameInterface.ShowMainInfo();
            // turn off loop
            _audioSrc.loop = false;
            // Stop playing music
            _audioSrc.Stop();
            // Set proper song
            _audioSrc.clip = MusicDatabase.GetProperSong(MusicDatabase.Death, MusicDatabase.Songs);
            // Start playing music
            _audioSrc.PlayDelayed(1f);
            // Set that death song is playing
            _isDeath = true;
            // Break action
            return;
        }
        // turn on loop
        _audioSrc.loop = true;
        // Set that death song is not playing
        _isDeath = false;
        // Check if current music is correct
        if (location.Equals(_audioSrc.clip.name))
            // Break action
            return;
        // Stop playing music
        _audioSrc.Stop();
        // Set proper song
        _audioSrc.clip = MusicDatabase.GetProperSong(location, MusicDatabase.Songs);
        // Start playing music
        _audioSrc.PlayDelayed(1f);
    }
}