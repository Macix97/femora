using UnityEngine;

public static class CursorDatabase
{
    // Cursors path
    public static readonly string Cursors = "Cursors/";

    // Pointer structure
    public struct Pointer
    {
        public string[] Tag { get; set; }
        public Texture2D Texture { get; set; }
    }

    // Pointers
    public static readonly Pointer[] Pointers = new Pointer[]
    {
        // Standard
        new Pointer
        {
            Tag = new string[] { "UI" },
            Texture = Resources.Load<Texture2D>(Cursors + "Standard"),
        },
        // Attack
        new Pointer
        {
            Tag = new string[] { "Enemy" },
            Texture = Resources.Load<Texture2D>(Cursors + "Attack"),
        },
        // Move
        new Pointer
        {
            Tag = new string[] { "Terrain" },
            Texture = Resources.Load<Texture2D>(Cursors + "Move"),
        },
        // Cancel
        new Pointer
        {
            Tag = new string[] { "Untagged" },
            Texture = Resources.Load<Texture2D>(Cursors + "Cancel"),
        },
        // Pick Up
        new Pointer
        {
            Tag = new string[] { "Item" },
            Texture = Resources.Load<Texture2D>(Cursors + "PickUp"),
        },
        // Talk
        new Pointer
        {
            Tag = new string[] { "Person" },
            Texture = Resources.Load<Texture2D>(Cursors + "Talk"),
        },
        // Search
        new Pointer
        {
            Tag = new string[] { "Container" },
            Texture = Resources.Load<Texture2D>(Cursors + "Search"),
        },
    };
}