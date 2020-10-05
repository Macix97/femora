using UnityEngine;

public class ItemClass : MonoBehaviour
{
    // White space
    public static readonly string WhiteSpace = " ";
    // Empty space
    public static readonly string EmptySpace = "";
    // Panel ID
    public static readonly string PanelId = "Panel";
    // Conversation panel ID
    public static readonly string ConvPanelId = "ConversationPanel0";
    // Item slot ID
    public static readonly string ItemSlotId = "ItemSlot";
    // Text ID
    public static readonly string TextId = "Text";
    // Clone name
    public static readonly string Clone = "(Clone)";
    // Dead label
    public static readonly string Dead = "Dead";
    // Untagged item
    public static readonly string Untagged = "Untagged";
    // Terrain item
    public static readonly string Terrain = "Terrain";
    // Item tag
    public static readonly string ItemTag = "Item";
    // Gold tag
    public static readonly string GoldTag = "Gold";
    // Container tag
    public static readonly string ContainerTag = "Container";
    // Item value
    public static readonly string ItemVal = "Value: ";
    // Motions
    public static readonly string DropItemMotion = "dropItem";
    public static readonly string LeaveItemMotion = "leaveItem";
    public static readonly string LootItemMotion = "lootItem";
    public static readonly string DropGoldMotion = "dropGold";
    public static readonly string LeaveGoldMotion = "leaveGold";
    public static readonly string LootGoldMotion = "lootGold";
    public static readonly string OpenContainerMotion = "isAction";
    // Right hand holder path
    public static readonly string RightItemHolder = "Armature/Hips/Spine/Spine1/Spine2/RightShoulder/" +
        "RightArm/RightForeArm/RightHand/";
    // Left hand holder path
    public static readonly string LeftItemHolder = "Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/" +
        "LeftArm/LeftForeArm/LeftHand/";
    // Head item holder
    public static readonly string HeadItemHolder = "Armature/Hips/Spine/Spine1/Spine2/Neck/Head/";
    // Hair
    public static readonly string Hair = "Hair";
    // Ordinary
    public static readonly string Ordinary = "Ordinary";
    // Elite
    public static readonly string Elite = "Elite";
    // Legendary
    public static readonly string Legendary = "Legendary";
    // Spine
    public static readonly string Spine = "Armature/Hips/Spine/Spine1";
    // Stain
    public static readonly string Stain = "Stain";
    // Gush
    public static readonly string Gush = "Gush";
    // Torch
    public static readonly string Torch = "Torch";
    // Campfire
    public static readonly string Campfire = "Campfire";
    // Infernal lantern
    public static readonly string InfernalLantern = "Infernal Lantern";
    // Stain time
    public static readonly float StainTime = 60f;
    // Skill effect time
    public static readonly float SkillEffectTime = 1f;
    // Skill effect point
    public static readonly float SkillEffectPoint = 1.2f;
    // Portal time
    public static readonly float PortalTime = 6f;
    // Gush time
    public static readonly float GushTime = 1f;
    // Gush point
    public static readonly float GushPoint = 1.2f;
    // Panel rotation
    public static readonly int xRotPanel = 30;
    public static readonly int yRotPanel = 45;
    // Hint Y axis
    public static readonly int yPosHint = 1;
    // UI character width
    public static readonly float TextCharWidth = 2f;
    // UI panel width modifier
    public static readonly float PanelWidthMod = 0.1f;
    // Trade modifier (reduce item value)
    public static readonly int TradeMod = 3;

    // Item properties
    public string Rank { get; set; }
    public string Type { get; set; }
    public string Kind { get; set; }
    public string Desc { get; set; }
    public string[] Bonus { get; set; }
    public string[] Stats { get; set; }
    public float[] Effect { get; set; }
    public float Weight { get; set; }
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public int Value { get; set; }
    public Sprite Sprite { get; set; }
    public Vector3 Pos { get; set; }
    public Vector3 Rot { get; set; }
    public Color HintColor { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init(gameObject.name);
    }

    // Set basic parameters
    public void Init(string name)
    {
        // Search proper gold
        for (int cnt = 0; cnt < ItemDatabase.Money.Length; cnt++)
            // Check gold name
            if (name.Equals(ItemDatabase.Money[cnt].Kind))
            {
                // Initialize item
                InitItem(ItemDatabase.Money[cnt]);
                // Break action
                return;
            }
        // Search proper ordinary item
        for (int cnt = 0; cnt < ItemDatabase.OrdinaryItems.Length; cnt++)
            // Check item name
            if (name.Equals(ItemDatabase.OrdinaryItems[cnt].Kind))
            {
                // Initialize item
                InitItem(ItemDatabase.OrdinaryItems[cnt]);
                // Break action
                return;
            }
        // Search proper elite item
        for (int cnt = 0; cnt < ItemDatabase.EliteItems.Length; cnt++)
            // Check item name
            if (name.Equals(ItemDatabase.EliteItems[cnt].Kind))
            {
                // Initialize item
                InitItem(ItemDatabase.EliteItems[cnt]);
                // Break action
                return;
            }
        // Search proper legendary item
        for (int cnt = 0; cnt < ItemDatabase.LegendaryItems.Length; cnt++)
            // Check item name
            if (name.Equals(ItemDatabase.LegendaryItems[cnt].Kind))
            {
                // Initialize item
                InitItem(ItemDatabase.LegendaryItems[cnt]);
                // Break action
                return;
            }
    }

    // Initialize proper item
    public void InitItem(ItemDatabase.Item item)
    {
        Rank = item.Rank;
        Type = item.Type;
        Kind = item.Kind;
        Desc = item.Desc;
        Bonus = item.Bonus;
        Stats = item.Stats;
        Effect = item.Effect;
        Weight = item.Weight;
        MinVal = item.MinVal;
        MaxVal = item.MaxVal;
        // Check if it is gold
        if (Type.Equals(ItemDatabase.Gold))
            Value = (int)Random.Range(MinVal, MaxVal);
        // It is item
        else
            Value = item.Value;
        Sprite = item.Sprite;
        Pos = item.Pos;
        Rot = item.Rot;
        HintColor = item.HintColor;
    }
}
