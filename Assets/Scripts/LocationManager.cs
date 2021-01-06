using UnityEngine;

/// <summary>
/// Manages the events that occur in the locations.
/// </summary>
public class LocationManager : MonoBehaviour
{
    // New location text
    public static readonly string NewLocationText = "Entering ";
    // Locations center
    [SerializeField]
    public GameObject[] LocationsCenter;
    // Locations size
    [SerializeField]
    public Vector3[] LocationsSize;
    // Protected areas center
    [SerializeField]
    public GameObject[] ProtAreasCenter;
    // Protected areas size
    [SerializeField]
    public Vector3[] ProtAreasSize;
    // Protected areas
    private Bounds[] _protAreas;
    // Locations
    private Location[] _locations;
    // Game interface
    private GameInterface _gameInterface;
    // Hero class
    private HeroClass _heroClass;

    // Location structure
    public struct Location
    {
        public string Name { get; set; }
        public Bounds Space { get; set; }
    };

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckLocationName();
    }

    // Set basic parameters
    private void Init()
    {
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        // Initialize protected areas
        _protAreas = new Bounds[ProtAreasCenter.Length];
        // Initialize locations
        _locations = new Location[LocationsCenter.Length];
        // Search protected areas
        for (int cnt = 0; cnt < ProtAreasCenter.Length; cnt++)
        {
            // Set new area
            _protAreas[cnt] = new Bounds(ProtAreasCenter[cnt].transform.position,
                new Vector3(ProtAreasSize[cnt].x, ProtAreasSize[cnt].y, ProtAreasSize[cnt].z));
        }
        // Search locations
        for (int cnt = 0; cnt < LocationsCenter.Length; cnt++)
        {
            // Set area name
            _locations[cnt].Name = LocationsCenter[cnt].name;
            // Set area space
            _locations[cnt].Space = new Bounds(LocationsCenter[cnt].transform.position,
                new Vector3(LocationsSize[cnt].x, LocationsSize[cnt].y, LocationsSize[cnt].z));
        }
    }

    /// <summary>
    /// Checks the name of the current location.
    /// </summary>
    private void CheckLocationName()
    {
        // Search locations
        for (int cnt = 0; cnt < _locations.Length; cnt++)
            // Check space
            if (_locations[cnt].Space.Contains(_heroClass.transform.position))
            {
                // Change location name
                ChangeLocationName(_locations[cnt].Name);
                // Break action
                break;
            }
    }

    /// <summary>
    /// Checks the name of the current location.
    /// </summary>
    /// <param name="locationName">A label that represents the name of the location.</param>
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

    /// <summary>
    /// Checks if the hero is in the protected area.
    /// </summary>
    /// <param name="hero">A transform that represents the hero.</param>
    /// <returns>
    /// The boolean that is true if the hero is safe or false if not.
    /// </returns>
    public bool IsHeroInProtectedArea(Transform hero)
    {
        // Search protected areas
        foreach (Bounds protArea in _protAreas)
            // Check if hero is inside
            if (protArea.Contains(hero.position))
                // Hero is safe
                return true;
        // Hero is in danger
        return false;
    }

    private void OnDrawGizmos()
    {
        // Search protected areas
        for (int cnt = 0; cnt < ProtAreasCenter.Length; cnt++)
        {
            // Change color
            Gizmos.color = Color.green;
            // Draw cube
            Gizmos.DrawWireCube(ProtAreasCenter[cnt].transform.position,
                new Vector3(ProtAreasSize[cnt].x, ProtAreasSize[cnt].y, ProtAreasSize[cnt].z));
        }
        // Search locations
        for (int cnt = 0; cnt < LocationsCenter.Length; cnt++)
        {
            // Change color
            Gizmos.color = Color.blue;
            // Draw cube
            Gizmos.DrawWireCube(LocationsCenter[cnt].transform.position,
                new Vector3(LocationsSize[cnt].x, LocationsSize[cnt].y, LocationsSize[cnt].z));
        }
    }
}