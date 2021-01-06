using UnityEngine;

/// <summary>
/// Describes the basic parameters of the person.
/// </summary>
public class PersonClass : MonoBehaviour
{
    // Person tag
    public static readonly string PersonTag = "Person";
    // Random gold range
    public static readonly int MinGold = 0;
    public static readonly int MaxGold = 3000;
    // Motions
    public static readonly string PersonMove = "isMoving";

    // Person properties
    public string Nature { get; set; }
    public string Type { get; set; }
    public int MaxHealth { get; set; }
    public int CurHealth { get; set; }
    public float Interval { get; set; }
    public string FirstMeetText { get; set; }
    public string EnterText { get; set; }
    public string[] HeroTexts { get; set; }
    public string[] PersonTexts { get; set; }
    public int[] StatmentTypes { get; set; }
    public Vector3[] Route { get; set; }
    public bool IsVisited { get; set; }
    public int Gold { get; set; }
    public string[] Items { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init(gameObject.name);
    }

    // Set basic parameters
    public void Init(string name)
    {
        // Search proper person
        for (int cnt = 0; cnt < PersonDatabase.People.Length; cnt++)
            // Check person name
            if (name.Equals(PersonDatabase.People[cnt].Type))
            {
                // Initialize person
                InitPerson(PersonDatabase.People[cnt]);
                // Break action
                return;
            }
    }

    /// <summary>
    /// Initiates person parameters according to appropriate criteria.
    /// </summary>
    /// <param name="person">A Type of person from database.</param>
    public void InitPerson(PersonDatabase.Person person)
    {
        // Set person parameters
        Nature = person.Nature;
        Type = person.Type;
        MaxHealth = person.MaxHealth;
        CurHealth = person.CurHealth;
        Interval = person.Interval;
        FirstMeetText = person.FirstMeetText;
        EnterText = person.EnterText;
        HeroTexts = person.HeroTexts;
        PersonTexts = person.PersonTexts;
        StatmentTypes = person.StatmentTypes;
        Route = person.Route;
        IsVisited = person.IsVisited;
        Items = person.Items;
        Gold = person.Gold;
    }
}