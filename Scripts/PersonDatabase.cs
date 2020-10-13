using UnityEngine;

public static class PersonDatabase
{
    // Person nature
    public static readonly string PersonHuman = "Human";
    // Statment types
    // No statment (Reset conversation)
    public static readonly int NoStatment = -1;
    // Information (No needed click, only text)
    public static readonly int InfoStatment = 0;
    // Trade (Needed click, display windows)
    public static readonly int TradeStatment = 1;
    // Exit (Needed click, exit from dialogue)
    public static readonly int ExitStatment = 2;

    // Person structure
    public struct Person
    {
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
    }

    // People
    public static readonly Person[] People = new Person[]
    {
        // Mirlanda
        new Person()
        {
            Nature = PersonHuman,
            Type = "Mirlanda",
            MaxHealth = 100,
            CurHealth = 100,
            Interval = 3f,
            FirstMeetText = "Welcome, stranger. I am Mirlanda, Priestess of the " +
                "Order of the Dawn. I make elixirs and essences. I could sell you some potions, " +
                "if you have enough gold. May you have some questions about surrounding areas? " +
                "I could tell you something about local customs. Feel free and ask whatever you want.",
            EnterText = "What can I do for you?",
            HeroTexts = new string[] { "Show me your wares.", "What do I need to know about this place?",
                "Could you tell me something about elixirs?", "Goodbye." },
            PersonTexts = new string[] { "I could sell you a few things.",
                "Our order tries to fight with prime evil that is growing in power. " +
                "We have less and less people who could to resist whole this situation. " +
                "Every brave warrior goes to kill devilish spawn. " +
                "We need to new knights who are able to use weapon. You look strong. We need your help. " +
                "Take a walk around the area, destroy the enemies and get some experience" +
                "before the final encounter.",
                "There are several types of mixtures. Some potions can heal your wounds, " +
                "others restore magical energy. Maybe you can find more powerful elixir that regenerate " +
                "both life and energy, but they are extremly rare. If you ever find such a mixture, " +
                "bring it to me, I will repay you.",
                "May you return in one piece!" },
            StatmentTypes = new int[] { TradeStatment, InfoStatment, InfoStatment, ExitStatment },
            Route = new Vector3[] { new Vector3(41f, 100, 24f), new Vector3(38f, 100, 19f),
                new Vector3(45f, 100, 18f), new Vector3(40f, 100, 29f) },
            IsVisited = false,
            Gold = 0,
            Items = new string[] { "Small Healing Potion", "Small Energy Potion" }
        },
        // Orik
        new Person()
        {
            Nature = PersonHuman,
            Type = "Orik",
            MaxHealth = 200,
            CurHealth = 200,
            Interval = 2f,
            FirstMeetText = "Ah! A fresh face. I don't think I saw you here before. My name is Orik, " +
                "member of Wolf Clan, a great warrior and skillful blacksmith. I could tell you something " +
                "about fighting or sell some equipment. I also know hunting. I think you will need some tips " +
                "before you go to fight.",
            EnterText = "What do you want?",
            HeroTexts = new string[] { "Let's trade.", "Tell me something about creatures.", "Teach me how to fight.",
                "Farewell." },
            PersonTexts = new string[] { "Let's see what I have to sell.",
                "During your journey you will come across different types of enemies. " +
                "You will probably meet many undead. They are slow and weak, at least most of them. " +
                "Demons are much stronger. You'd better be prepared before you face them. " +
                "If you ever see a Efreeti, run as fast as you can. He will turn you into human torch!",
                "There are a few things you should know about. Try to use your advantages. You look agile. " +
                "Attack your opponent and dodge. Use the area to limit the number of enemies. " +
                "If there are too many creatures, try to kill them one by one. Get some good equipment, " +
                "buy some potions and use many skills at the same time.",
                "See you later." },
            StatmentTypes = new int[] { TradeStatment, InfoStatment, InfoStatment, ExitStatment },
            Route = new Vector3[] { new Vector3(31f, 100, 64f), new Vector3(27f, 100, 66f),
                new Vector3(29f, 100, 60f), new Vector3(38f, 100, 60f) },
            IsVisited = false,
            Gold = 0,
            Items = new string[] { "Heavy Boots", "Steel Sword", "Wooden Shield", "Banded Armor", "Bascinet" } 
        }
    };
}