using UnityEngine;

public class LocationManager : MonoBehaviour
{
    // Refugee Camp
    public static readonly string RefugeeCamp = "Refugee Camp";
    // Stony Plain
    public static readonly string StonyPlain = "Stony Plain";
    // Stony Plain
    public static readonly string DeathValley = "Death Valley";
    // Hell Pit
    public static readonly string HellPit = "Hell Pit";
    // Location start
    public static readonly string LocationStart = " Start";
    // Location end
    public static readonly string LocationEnd = " End";
    // New location text
    public static readonly string NewLocationText = "Entering the ";
    // Game interface
    private GameInterface _gameInterface;
    // Hero class
    private HeroClass _heroClass;
    // Refugee Camp transform
    private Transform _refugeeCamp;
    // Stony Plain transform
    private Transform _stonyPlain;
    // Death Valley transform
    private Transform _deathValley;
    // Hell Pit transform
    private Transform _hellPit;
    // First border height
    private int _firstBorder = 90;
    // Second border height
    private int _secondBorder = 78;
    // Third border height
    private int _thirdBorder = 66;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckLocationName();
        CheckLocationDist(ref _refugeeCamp, 105f);
        CheckLocationDist(ref _stonyPlain, 210f);
        CheckLocationDist(ref _deathValley, 110f);
        CheckLocationDist(ref _hellPit, 150f);
    }

    // Set basic parameters
    private void Init()
    {
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _refugeeCamp = GameObject.Find(RefugeeCamp).GetComponent<Transform>();
        _stonyPlain = GameObject.Find(StonyPlain).GetComponent<Transform>();
        _deathValley = GameObject.Find(DeathValley).GetComponent<Transform>();
        _hellPit = GameObject.Find(HellPit).GetComponent<Transform>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
    }

    // Check if hero is near from location
    private void CheckLocationDist(ref Transform location, float distance)
    {
        // Check distance from location
        if (Vector3.Distance(_heroClass.transform.position, location.position) > distance)
            // Deactivate location
            location.gameObject.SetActive(false);
        else
            // Activate location
            location.gameObject.SetActive(true);
    }

    // Check current location name
    private void CheckLocationName()
    {
        // Check hero position
        if (_heroClass.transform.position.y > _firstBorder)
            // This is Refugee Camp
            ChangeLocationName(RefugeeCamp);
        else if (_heroClass.transform.position.y <= _firstBorder && _heroClass.transform.position.y > _secondBorder)
            // This is Stony Plain
            ChangeLocationName(StonyPlain);
        else if (_heroClass.transform.position.y <= _secondBorder && _heroClass.transform.position.y > _thirdBorder)
            // This is Death Valley
            ChangeLocationName(DeathValley);
        else if (_heroClass.transform.position.y <= _thirdBorder)
            // This is Hell Pit
            ChangeLocationName(HellPit);
    }

    // Change current location name
    private void ChangeLocationName(string locationName)
    {
        // Check if hero go to new location
        if (!_heroClass.CurLocation.Equals(locationName))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = NewLocationText + locationName;
            // Display new text
            _gameInterface.ShowMainInfo();
            // Hide text with delay
            _gameInterface.DelayedHideMainInfo(2f);
        }
        // Change location
        _heroClass.CurLocation = locationName;
        // Adapt location name
        _gameInterface.SetLocationName(_heroClass.CurLocation);
    }
}
