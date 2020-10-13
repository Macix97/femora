using UnityEngine;

public static class PeriodDatabase
{
    // Lighting path
    public static readonly string Lighting = "Lighting/";
    // Color
    public static readonly string Color = "Color";
    // Direction
    public static readonly string Direction = "Direction";
    // Reflection
    public static readonly string Reflection = "Reflection";
    // Morning
    public static readonly string Morning = "Morning";
    // Noon
    public static readonly string Noon = "Morning";
    // Afternoon
    public static readonly string Afternoon = "Afternoon";
    // Evening
    public static readonly string Evening = "Evening";
    // Midnight
    public static readonly string Midnight = "Midnight";

    // Period structure
    public struct Period
    {
        public string Name { get; set; }
        public Texture2D Color { get; set; }
        public Texture2D Direction { get; set; }
        public Cubemap Reflection { get; set; }
    }

    // Periods
    public static readonly Period[] Periods = new Period[]
    {
        // Morning
        new Period
        {
            Name = Morning,
            Color = Resources.Load<Texture2D>(Lighting + Morning + Color),
            Direction = Resources.Load<Texture2D>(Lighting + Morning + Direction),
            Reflection = Resources.Load<Cubemap>(Lighting + Morning + Reflection)
        },
        // Noon
        new Period
        {
            Name = Noon,
            Color = Resources.Load<Texture2D>(Lighting + Noon + Color),
            Direction = Resources.Load<Texture2D>(Lighting + Noon + Direction),
            Reflection = Resources.Load<Cubemap>(Lighting + Noon + Reflection)
        },
        // Afternoon
        new Period
        {
            Name = Afternoon,
            Color = Resources.Load<Texture2D>(Lighting + Afternoon + Color),
            Direction = Resources.Load<Texture2D>(Lighting + Afternoon + Direction),
            Reflection = Resources.Load<Cubemap>(Lighting + Afternoon + Reflection)
        },
        // Evening
        new Period
        {
            Name = Evening,
            Color = Resources.Load<Texture2D>(Lighting + Evening + Color),
            Direction = Resources.Load<Texture2D>(Lighting + Evening + Direction),
            Reflection = Resources.Load<Cubemap>(Lighting + Evening + Reflection)
        },
        // Midnight
        new Period
        {
            Name = Midnight,
            Color = Resources.Load<Texture2D>(Lighting + Midnight + Color),
            Direction = Resources.Load<Texture2D>(Lighting + Midnight + Direction),
            Reflection = Resources.Load<Cubemap>(Lighting + Evening + Reflection)
        }
    };

    // Get proper period by name
    public static Period GetProperPeriod(string name)
    {
        // Reset counter
        int cnt = 0;
        // Search proper period
        for (; cnt < Periods.Length; cnt++)
            // Check period name
            if (Periods[cnt].Name.Equals(name))
                // Break action
                break;
        // Return proper period
        return Periods[cnt];
    }
}