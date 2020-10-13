using UnityEngine;
using UnityEngine.UI;

public class HeroInventory : MonoBehaviour
{
    // Action types
    public static readonly int DropItem = 0;
    public static readonly int ResetItem = 1;
    public static readonly int MoveItem = 2;
    public static readonly int LeaveItem = 3;
    // Inventory grid ID
    public static readonly string InventoryGridId = "InventoryGrid";
    // Potion grid ID
    public static readonly string PotionGridId = "PotionGrid";
    // Trade grid ID
    public static readonly string TradeGridId = "TradeGrid";
    // Inventory slot ID
    public static readonly string InvSlotId = "InvSlot0";
    // Potion slot ID
    public static readonly string PotionSlotId = "PotionSlot0";
    // Trade slot ID
    public static readonly string TradeSlotId = "TradeSlot0";
    // Count of inventory slots
    public static readonly int InvSlots = 36;
    // Count of potion slots
    public static readonly int PotionSlots = 8;
    // Count of trade slots
    public static readonly int TradeSlots = 72;
    // Maximal hero gold amount
    public static readonly int MaxGoldAmount = 100000;
    // Steal modifier
    public static readonly int stealMod = 25;
    // Minimal dropped item distance
    public static readonly int MinDropDist = -1;
    // Average dropped item dist
    public static readonly int AvgDropDist = 1;
    // Maximal dropped item distance
    public static readonly int MaxDropDist = 2;
    // Maximal count of item to sell (person inventory)
    public static readonly int MaxTradeItem = 40;
    // Minimal count of item to sell (person inventory)
    public static readonly int MinTradeItem = 4;
    // Count of active inventory slots
    public int ActiveInvSlots { get; set; }
    // Count of active potion slots
    public int ActivePotionSlots { get; set; }
    // Count of active trade slots
    public int ActiveTradeSlots { get; set; }
    // Array of inventory slots
    public Slot[] HeroInv { get; set; }
    // Array of potion slots
    public Slot[] HeroPotions { get; set; }
    // Array of trade slots
    public Slot[] PersonInv { get; set; }
    // Inventory grid rect
    private RectTransform _invGridRect;
    // Potion grid rect
    private RectTransform _potionGridRect;
    // Trade grid rect
    private RectTransform _tradeGridRect;
    // Hero behavior
    private HeroBehavior _heroBehavior;
    // Hero class
    private HeroClass _heroClass;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Hero sound
    private HeroSound _heroSound;
    // Game interface
    private GameInterface _gameInterface;
    // Bottom panel transform
    private Transform _bottomPanelTrans;
    // Hero gold
    public int Gold { get; set; }
    // Check if hair is active
    public bool IsHair { get; set; }
    // Check if hero has ordinary armor
    public bool IsOrdinary { get; set; }
    // Check if hero has elite armor
    public bool IsElite { get; set; }
    // Check if hero has legendary armor
    public bool IsLegendary { get; set; }

    //----- Equipment -----//

    // Equipment slots IDs
    public static readonly string HeadSlotId = "HeadSlot";
    public static readonly string TorsoSlotId = "TorsoSlot";
    public static readonly string RightHandSlotId = "RightHandSlot";
    public static readonly string LeftHandSlotId = "LeftHandSlot";
    public static readonly string FeetSlotId = "FeetSlot";
    // Equipment slots
    internal Slot HeadSlot;
    internal Slot TorsoSlot;
    internal Slot RightHandSlot;
    internal Slot LeftHandSlot;
    internal Slot FeetSlot;

    // Slot structure
    public struct Slot
    {
        public string SlotId;
        public string ItemName;
        public bool IsSlotActive;
        public RectTransform SlotRect;
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        _heroBehavior = gameObject.GetComponent<HeroBehavior>();
        _heroClass = gameObject.GetComponent<HeroClass>();
        _heroParameter = gameObject.GetComponent<HeroParameter>();
        _heroSound = GetComponent<HeroSound>();
        _invGridRect = GameObject.Find(InventoryGridId).GetComponent<RectTransform>();
        _potionGridRect = GameObject.Find(PotionGridId).GetComponent<RectTransform>();
        _tradeGridRect = GameObject.Find(TradeGridId).GetComponent<RectTransform>();
        _bottomPanelTrans = GameObject.Find(GameInterface.BottomPanel).GetComponent<Transform>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        // Initialize slots
        HeroInv = new Slot[InvSlots];
        HeroPotions = new Slot[PotionSlots];
        PersonInv = new Slot[TradeSlots];
        HeadSlot = new Slot();
        TorsoSlot = new Slot();
        RightHandSlot = new Slot();
        LeftHandSlot = new Slot();
        FeetSlot = new Slot();
        // Set active slots
        ActiveInvSlots = InvSlots;
        ActivePotionSlots = PotionSlots;
        ActiveTradeSlots = TradeSlots;
        // Initialize inventory slots
        for (int cnt = 0; cnt < InvSlots; cnt++)
        {
            HeroInv[cnt].SlotId = InvSlotId + cnt;
            HeroInv[cnt].ItemName = null;
            HeroInv[cnt].IsSlotActive = true;
            HeroInv[cnt].SlotRect = GameObject.Find(InvSlotId + cnt).GetComponent<RectTransform>();
        }
        // Initialize potion slots
        for (int cnt = 0; cnt < PotionSlots; cnt++)
        {
            HeroPotions[cnt].SlotId = PotionSlotId + cnt;
            HeroPotions[cnt].ItemName = null;
            HeroPotions[cnt].IsSlotActive = true;
            HeroPotions[cnt].SlotRect = GameObject.Find(PotionSlotId + cnt).GetComponent<RectTransform>();
        }
        // Initialize trade slots
        for (int cnt = 0; cnt < TradeSlots; cnt++)
        {
            PersonInv[cnt].SlotId = TradeSlotId + cnt;
            PersonInv[cnt].ItemName = null;
            PersonInv[cnt].IsSlotActive = true;
            PersonInv[cnt].SlotRect = GameObject.Find(TradeSlotId + cnt).GetComponent<RectTransform>();
        }
        // Initialize equipment slots
        InitEquipmentSlot(ref HeadSlot, HeadSlotId);
        InitEquipmentSlot(ref TorsoSlot, TorsoSlotId);
        InitEquipmentSlot(ref RightHandSlot, RightHandSlotId);
        InitEquipmentSlot(ref LeftHandSlot, LeftHandSlotId);
        InitEquipmentSlot(ref FeetSlot, FeetSlotId);
        // Set gold amount
        Gold = 0;
        // Set hero properties
        IsHair = true;
        IsOrdinary = false;
        IsElite = false;
        IsLegendary = false;
        // Hide equipment
        transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = false;
        transform.Find(ItemClass.Elite).GetComponent<SkinnedMeshRenderer>().enabled = false;
        transform.Find(ItemClass.Legendary).GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    public void CheckHeroInventory(ref Transform target)
    {
        // Get item parameters
        ItemClass itemClass = target.GetComponent<ItemClass>();
        // Check if it is gold or item
        if (itemClass.Type.Equals(ItemDatabase.Gold))
        {
            // Check current hero gold amount
            if (Gold + itemClass.MaxVal > MaxGoldAmount)
            {
                // Play swoosh sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.Swoosh, SoundDatabase.ItemSounds));
                // Play leave animation
                itemClass.GetComponent<Animator>().SetTrigger(ItemClass.LeaveGoldMotion);
                // Play I can't carry anymore sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.ICantCarryAnymore, _heroSound.HeroSounds));
                // Break action
                return;
            }
            // Pick up gold
            AdaptHeroGold(itemClass.Value);
            // Destroy gold from scene
            Destroy(itemClass.transform.parent.gameObject);
            // Play pickup sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.PickUp, SoundDatabase.ItemSounds));
            // Break action
            return;
        }
        // Check if item is potion
        if (itemClass.Type.Equals(ItemDatabase.Potion))
        {
            // Search potion slots
            for (int cnt = 0; cnt < PotionSlots; cnt++)
            {
                // Check which slot is enabled
                if (HeroPotions[cnt].IsSlotActive)
                {
                    // Check if item is picked up
                    if (IsItemPickup(cnt, ref itemClass, HeroPotions, ref target))
                    {
                        // Decrement count of active potion slots
                        ActivePotionSlots--;
                        // Play pickup sound
                        _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                            .GetProperSound(SoundDatabase.PickUp, SoundDatabase.ItemSounds));
                        // Break action
                        return;
                    }
                }
            }
        }
        // Search inventory slots
        for (int cnt = 0; cnt < InvSlots; cnt++)
        {
            // Check which slot is enabled
            if (HeroInv[cnt].IsSlotActive)
            {
                // Check if item is picked up
                if (IsItemPickup(cnt, ref itemClass, HeroInv, ref target))
                {
                    // Decrement count of active inventory slots
                    ActiveInvSlots--;
                    // Play pickup sound
                    _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                        .GetProperSound(SoundDatabase.PickUp, SoundDatabase.ItemSounds));
                    // Break action
                    return;
                }
            }
        }
        // Play swoosh sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase
            .GetProperSound(SoundDatabase.Swoosh, SoundDatabase.ItemSounds));
        // Play leave animation
        itemClass.GetComponent<Animator>().SetTrigger(ItemClass.LeaveItemMotion);
        // Play I can't carry anymore sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase
            .GetProperSound(SoundDatabase.ICantCarryAnymore, _heroSound.HeroSounds));
    }

    // Adapt hero gold
    public void AdaptHeroGold(int amount)
    {
        // Change hero gold
        Gold += amount;
    }

    // Adapt person gold
    public void AdaptPersonGold(int amount)
    {
        // Change hero gold
        _heroBehavior.PersonClass.Gold += amount;
        // Update graphical interface
        _gameInterface.AdaptPeronGoldAmount();
    }

    // Check if item is picked up
    public bool IsItemPickup(int cnt, ref ItemClass itemClass, Slot[] slots, ref Transform target)
    {
        // Check hero capacity
        if (_heroClass.Capacity < itemClass.Weight)
            // It is false
            return false;
        // Create new item slot
        GameObject itemSlot = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ItemSlotId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Set proper parent of item in inventory
        itemSlot.transform.SetParent(slots[cnt].SlotRect.transform);
        // Set proper scale of item in inventory
        itemSlot.transform.localScale = Vector3.one;
        // Set proper position of item in inventory
        itemSlot.transform.localPosition = Vector3.zero;
        // Change item slot name
        itemSlot.name = itemClass.Kind;
        // Get item paremeters
        ItemClass itemSlotClass = itemSlot.GetComponent<ItemClass>();
        // Initialize item
        itemSlotClass.Init(itemSlot.name);
        // Delete object from scene
        Destroy(target.parent.gameObject);
        // Set that slot is disabled
        slots[cnt].IsSlotActive = false;
        // Set item name
        slots[cnt].ItemName = itemClass.Kind;
        // Decrement hero capacity
        _heroParameter.AdaptStats(HeroClass.CapacityId, -itemClass.Weight);
        // Change item view
        itemSlot.GetComponent<Image>().sprite = itemSlotClass.Sprite;
        // It is true
        return true;
    }

    // Check if item is moved
    public int IsItemMove(string gridId, int cnt1, string objSlot, ref ItemClass itemClass, Slot[] slots)
    {
        // Check if cursor hover slot and its active
        if (RectTransformUtility.RectangleContainsScreenPoint(slots[cnt1].SlotRect, Input.mousePosition)
            && (slots[cnt1].IsSlotActive || objSlot.Equals(slots[cnt1].SlotId)))
        {
            // Check if it is not potion and it is potion grid
            if (gridId.Equals(PotionGridId) && !itemClass.Type.Equals(ItemDatabase.Potion))
                // Check other option
                return DropItem;
            // Check if it is the same slot
            if (objSlot.Equals(slots[cnt1].SlotId))
                // Leave item in this slot
                return LeaveItem;
            // Set new parent of current item
            itemClass.transform.SetParent(GameObject.Find(slots[cnt1].SlotId).transform);
            // Set new position of current item
            itemClass.transform.localPosition = Vector3.zero;
            // Disable current slot
            slots[cnt1].IsSlotActive = false;
            // Set item name
            slots[cnt1].ItemName = itemClass.Kind;
            // Check if item is moving from equipment slot to inventory slot
            if ((CheckActiveItemMoveDrop(objSlot, HeadSlotId, ref HeadSlot, itemClass)
                || CheckActiveItemMoveDrop(objSlot, TorsoSlotId, ref TorsoSlot, itemClass)
                || CheckActiveItemMoveDrop(objSlot, RightHandSlotId, ref RightHandSlot, itemClass)
                || CheckActiveItemMoveDrop(objSlot, LeftHandSlotId, ref LeftHandSlot, itemClass)
                || CheckActiveItemMoveDrop(objSlot, FeetSlotId, ref FeetSlot, itemClass))
                && gridId.Equals(InventoryGridId))
            {
                // Search proper stats
                for (int cnt2 = 0; cnt2 < itemClass.Stats.Length; cnt2++)
                    // Decrement proper stats
                    _heroParameter.AdaptStats(itemClass.Stats[cnt2], -itemClass.Effect[cnt2]);
                // Decrement count of active inventory slots
                ActiveInvSlots--;
                // Move item
                return MoveItem;
            }
            // Check if item is moving from potion slot to potion slot
            else if (objSlot.Contains(ItemDatabase.Potion) && gridId.Equals(PotionGridId))
            {
                // Get ealier slot
                int ealierSlot = GetPotionSlotIndex(objSlot);
                // Activate earlier potion slot
                slots[ealierSlot].IsSlotActive = true;
                // Remove item name
                slots[ealierSlot].ItemName = null;
            }
            // Check if item is moving from potion slot to inventory slot
            else if (objSlot.Contains(ItemDatabase.Potion) && gridId.Equals(InventoryGridId))
            {
                // Get ealier slot
                int ealierSlot = GetPotionSlotIndex(objSlot);
                // Activate earlier potion slot
                HeroPotions[ealierSlot].IsSlotActive = true;
                // Remove item name
                HeroPotions[ealierSlot].ItemName = null;
                // Decrement count of active inventory slots
                ActiveInvSlots--;
                // Increment count of active potion slots
                ActivePotionSlots++;
            }
            // Check if item is moving from inventory slot to potion slot
            else if (!objSlot.Contains(ItemDatabase.Potion) && gridId.Equals(PotionGridId))
            {
                // Get ealier slot
                int ealierSlot = GetInvSlotIndex(objSlot);
                // Activate earlier potion slot
                HeroInv[ealierSlot].IsSlotActive = true;
                // Remove item name
                HeroInv[ealierSlot].ItemName = null;
                // Increment count of active inventory slots
                ActiveInvSlots++;
                // Decrement count of active potion slots
                ActivePotionSlots--;
            }
            // item is moving from inventory slot to inventory slot
            else if (!objSlot.Contains(ItemDatabase.Potion) && gridId.Equals(InventoryGridId))
            {
                // Get ealier slot
                int ealierSlot = GetInvSlotIndex(objSlot);
                // Activate earlier inventory slot
                slots[ealierSlot].IsSlotActive = true;
                // Remove item name
                slots[ealierSlot].ItemName = null;
            }
            // Move item
            return MoveItem;
        }
        // Check another slot
        return ResetItem;
    }

    // Move item from slot to other slot
    public int CheckItemMove(string objSlot)
    {
        // Check if item is moving from equipment slot to another equipment slot (it is impossible)
        if (ValidateItemMove(objSlot, HeadSlot)
            || ValidateItemMove(objSlot, TorsoSlot)
            || ValidateItemMove(objSlot, RightHandSlot)
            || ValidateItemMove(objSlot, LeftHandSlot)
            || ValidateItemMove(objSlot, FeetSlot))
            // Leave item in current slot
            return ResetItem;
        // Get old slot
        ItemClass itemClass = GameObject.Find(objSlot).GetComponentInChildren<ItemClass>();
        // Search potion slots
        for (int cnt = 0; cnt < PotionSlots; cnt++)
        {
            // Check if item is moved to potion slot
            int respond = IsItemMove(PotionGridId, cnt, objSlot, ref itemClass, HeroPotions);
            // Check if item is moving
            if (respond.Equals(MoveItem))
                // Move item
                return MoveItem;
            // Check if item is leaving in same slot
            else if (respond.Equals(LeaveItem))
                // Leave item
                return LeaveItem;
        }
        // Search inventory slots
        for (int cnt = 0; cnt < InvSlots; cnt++)
        {
            // Check if item is moved to inventory slot
            int respond = IsItemMove(InventoryGridId, cnt, objSlot, ref itemClass, HeroInv);
            // Check if item is moving
            if (respond.Equals(MoveItem))
                // Move item
                return MoveItem;
            // Check if item is leaving in same slot
            else if (respond.Equals(LeaveItem))
                // Leave item
                return LeaveItem;
        }
        // Check other action
        return DropItem;
    }

    // Drop item to ground
    public int CheckItemDrop(string objName)
    {
        // Check mouse position while item is dragging
        if (RectTransformUtility.RectangleContainsScreenPoint(_invGridRect, Input.mousePosition)
            || RectTransformUtility.RectangleContainsScreenPoint(_potionGridRect, Input.mousePosition))
            // Leave item in inventory
            return ResetItem;
        // Get item parameters (item slot)
        ItemClass itemSlotClass = GetProperGameObject(objName).GetComponentInChildren<ItemClass>();
        // Generate item position
        Vector3 itemPosition = RandomItemPosition();
        // Spawn item
        GameObject droppedItem = Instantiate(Resources.Load(ItemDatabase.Prefabs + itemSlotClass.Kind),
            itemPosition, Quaternion.identity) as GameObject;
        // Generate new name for item
        string newName = droppedItem.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
        // Change dropped item name
        droppedItem.name = newName;
        // Play drop animation
        droppedItem.GetComponentInChildren<Animator>().SetTrigger(ItemClass.DropItemMotion);
        // Get item paremeters (dropped item)
        ItemClass droppedItemClass = droppedItem.GetComponentInChildren<ItemClass>();
        // Increment hero capacity
        _heroParameter.AdaptStats(HeroClass.CapacityId, itemSlotClass.Weight);
        // Change panel parent
        _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
        // Change layout hierarchy
        _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        // Delete object from inventory
        Destroy(itemSlotClass.gameObject);
        // Check if item is dropping from equipment slot
        if (CheckActiveItemMoveDrop(objName, HeadSlotId, ref HeadSlot, itemSlotClass)
            || CheckActiveItemMoveDrop(objName, TorsoSlotId, ref TorsoSlot, itemSlotClass)
            || CheckActiveItemMoveDrop(objName, RightHandSlotId, ref RightHandSlot, itemSlotClass)
            || CheckActiveItemMoveDrop(objName, LeftHandSlotId, ref LeftHandSlot, itemSlotClass)
            || CheckActiveItemMoveDrop(objName, FeetSlotId, ref FeetSlot, itemSlotClass))
        {
            // Search proper stats
            for (int cnt = 0; cnt < itemSlotClass.Stats.Length; cnt++)
                // Decrement proper stats
                _heroParameter.AdaptStats(itemSlotClass.Stats[cnt], -itemSlotClass.Effect[cnt]);
            // Drop item
            return DropItem;
        }
        // Check if item is moving from potion slot
        if (objName.Contains(ItemDatabase.Potion))
        {
            // Get current slot
            int curSlot = GetPotionSlotIndex(objName);
            // Activate current potion slot
            HeroPotions[curSlot].IsSlotActive = true;
            // Remove item name
            HeroPotions[curSlot].ItemName = null;
            // Increment count of active potion slots
            ActivePotionSlots++;
        }
        // Change inventory slot
        else
        {
            // Get current slot
            int curSlot = GetInvSlotIndex(objName);
            // Activate current inventory slot
            HeroInv[curSlot].IsSlotActive = true;
            // Remove item name
            HeroInv[curSlot].ItemName = null;
            // Increment count of active slots
            ActiveInvSlots++;
        }
        // Drop item
        return DropItem;
    }

    // Show info about item in slot
    public void ShowItemSlotInfo(GameObject itemSlot)
    {
        // Get slot ID
        string objName = itemSlot.transform.parent.name;
        // Get item parameters
        ItemClass itemClass = GameObject.Find(objName).GetComponentInChildren<ItemClass>();
        // Check if mouse hover item in potion slot
        if (objName.Contains(ItemDatabase.Potion))
        {
            // Change pivot position of activity panel
            _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.5f, 0f);
            // Change panel parent
            _gameInterface.SlotPanelImg.transform.SetParent(itemSlot.transform);
            // Change layout hierarchy
            _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        }
        // Check if mouse hover item in head slot or torso slot
        else if (objName.Equals(HeadSlotId) || objName.Equals(TorsoSlotId))
        {
            // Change pivot position of activity panel
            _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.5f, 1f);
            // Change panel parent
            _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
            // Change layout hierarchy
            _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        }
        // Check if mouse hover item in inventory slot
        else if (objName.Contains(InvSlotId))
        {
            // Get slot ID
            string slotId = objName.Substring(objName.IndexOf("0") + 1);
            // Get slot index
            int slotIndex = int.Parse(slotId);
            // Check slot index
            if (slotIndex < 16)
                // Set pivot with right offset
                _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.3f, 0.5f);
            else if (slotIndex > 19)
                // Set pivot with left offset
                _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.7f, 0.5f);
            else
                // Change pivot position of activity panel
                _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            // Change panel parent
            _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
            // Change layout hierarchy
            _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        }
        // Set standard pivot
        else
        {
            // Change pivot position of activity panel
            _gameInterface.SlotPanelImg.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            // Change panel parent
            _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
            // Change layout hierarchy
            _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        }
        // Set new position of activity panel
        _gameInterface.SlotPanelImg.transform.position = itemSlot.transform.position;
        // Set item info in panel
        _gameInterface.AdaptSlotInfo(itemClass);
        // Show panel
        _gameInterface.ActivateElement(_gameInterface.SlotPanelImg.transform);
    }

    // Use potion by click hotkey
    public void UsePotionQuick(string objName, int index)
    {
        // Set start index
        int curSlot;
        // Try get proper slot in column
        curSlot = GetPotionSlotIndex((string)(objName
            + (index * GameInterface.PotionGridRow)));
        // Potion is in slot
        if (!HeroPotions[curSlot].IsSlotActive)
        {
            // Get potion slot object
            GameObject potionSlot = GameObject.Find((string)(objName
                + (index * GameInterface.PotionGridRow)));
            // Check if panel is in slot
            if (_gameInterface.SlotPanelImg.transform.IsChildOf(potionSlot.GetComponentInChildren<Transform>()))
                // Hide info panel
                _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
            // Use selected item
            IsItemUse((string)(objName
                + (index * GameInterface.PotionGridRow)));
            // Break action
            return;
        }
        // Try get another slot in column
        curSlot = GetPotionSlotIndex((string)(objName
            + (index * GameInterface.PotionGridRow + GameInterface.PotionGridColMod)));
        // Potion is in slot
        if (!HeroPotions[curSlot].IsSlotActive)
        {
            // Get potion slot object
            GameObject potionSlot = GameObject.Find((string)(objName
                + (index * GameInterface.PotionGridRow + GameInterface.PotionGridColMod)));
            // Check if panel is in slot
            if (_gameInterface.SlotPanelImg.transform.IsChildOf(potionSlot.GetComponentInChildren<Transform>()))
                // Hide info panel
                _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
            // Use selected item
            IsItemUse((string)(objName
                + (index * GameInterface.PotionGridRow + GameInterface.PotionGridColMod)));
            // Break action
            return;
        }
        // There is no potion
    }

    // Use clicked item in inventory window
    public bool IsItemUse(string objName)
    {
        // Check if item is in trade slot
        if (objName.Contains(TradeSlotId))
            // Break action
            return false;
        // Get item parameters
        ItemClass itemClass = GameObject.Find(objName).GetComponentInChildren<ItemClass>();
        // Check if in slot is potion
        if (!itemClass.Type.Equals(ItemDatabase.Potion))
            // Do nothing if there is not potion
            return false;
        // Search proper stats
        for (int cnt = 0; cnt < itemClass.Stats.Length; cnt++)
        {
            // Check if potion can heal hero
            if (itemClass.Stats[cnt].Equals(HeroClass.CurHealthId))
                // Heal hero
                _heroParameter.AdaptHealth((int)(_heroClass.MaxHealth / (100 / itemClass.Effect[cnt])));
            // Check if potion can recover hero energy
            if (itemClass.Stats[cnt].Equals(HeroClass.CurEnergyId))
                // Recover hero energy
                _heroParameter.AdaptEnergy((int)(_heroClass.MaxEnergy / (100 / itemClass.Effect[cnt])));
        }
        // Increment hero capacity
        _heroParameter.AdaptStats(HeroClass.CapacityId, itemClass.Weight);
        // Check if item is using from potion slot
        if (objName.Contains(ItemDatabase.Potion))
        {
            // Get current slot
            int curSlot = GetPotionSlotIndex(objName);
            // Activate current potion slot
            HeroPotions[curSlot].IsSlotActive = true;
            // Remove item name
            HeroPotions[curSlot].ItemName = null;
            // Increment count of active potion slots
            ActivePotionSlots++;
        }
        // Change inventory slot
        else
        {
            // Get current slot
            int curSlot = GetInvSlotIndex(objName);
            // Activate current inventory slot
            HeroInv[curSlot].IsSlotActive = true;
            // Remove item name
            HeroInv[curSlot].ItemName = null;
            // Increment count of active slots
            ActiveInvSlots++;
        }
        // Change panel parent
        _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
        // Change layout hierarchy
        _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        // Delete object from inventory
        Destroy(itemClass.gameObject);
        // Play gulp sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Gulp, SoundDatabase.ItemSounds));
        // Break action
        return true;
    }

    // Equip selected item from hero inventory
    public int CheckItemEquip(string objName)
    {
        // Get item parameters
        ItemClass itemClass = GetProperGameObject(objName).GetComponentInChildren<ItemClass>();
        // Validate proper equipment and calculate respond
        int respond = ValidateItemEquip(objName, ItemDatabase.Head, HeadSlotId, itemClass, ref HeadSlot)
            + ValidateItemEquip(objName, ItemDatabase.Torso, TorsoSlotId, itemClass, ref TorsoSlot)
            + ValidateItemEquip(objName, ItemDatabase.RightHand, RightHandSlotId, itemClass, ref RightHandSlot)
            + ValidateItemEquip(objName, ItemDatabase.LeftHand, LeftHandSlotId, itemClass, ref LeftHandSlot)
            + ValidateItemEquip(objName, ItemDatabase.Feet, FeetSlotId, itemClass, ref FeetSlot);
        // Check if item is dropping
        if (respond.Equals(DropItem))
            // go to drop
            return DropItem;
        // Check if item is staying in slot (wrong move)
        if (respond.Equals(ResetItem))
            // reset item position
            return ResetItem;
        // Check if item is staying in slot
        if (respond.Equals(LeaveItem))
            // leave item in slot
            return LeaveItem;
        // Get current slot
        int curSlot = GetInvSlotIndex(objName);
        // Search proper stats
        for (int cnt = 0; cnt < itemClass.Stats.Length; cnt++)
            // Add proper stats
            _heroParameter.AdaptStats(itemClass.Stats[cnt], itemClass.Effect[cnt]);
        // Activate earlier slot
        HeroInv[curSlot].IsSlotActive = true;
        // Remove item name
        HeroInv[curSlot].ItemName = null;
        // Increment count of active slots
        ActiveInvSlots++;
        // Active item
        return MoveItem;
    }

    // Get proper inventory slot index
    public int GetInvSlotIndex(string objName)
    {
        // Set start index
        int cnt = 0;
        // Search proper inventory slot
        for (; cnt < InvSlots; cnt++)
            // Check ID of current slot
            if (HeroInv[cnt].SlotId.Equals(objName))
                // Break loop
                break;
        // Return proper index
        return cnt;
    }

    // Get proper potion slot index
    public int GetPotionSlotIndex(string objName)
    {
        // Set start index
        int cnt = 0;
        // Search proper inventory slot
        for (; cnt < PotionSlots; cnt++)
            // Check ID of current slot
            if (HeroPotions[cnt].SlotId.Equals(objName))
                // Break loop
                break;
        // Return proper index
        return cnt;
    }

    // Get proper trade slot index
    public int GetTradeSlotIndex(string objName)
    {
        // Set start index
        int cnt = 0;
        // Search proper inventory slot
        for (; cnt < TradeSlots; cnt++)
            // Check ID of current slot
            if (PersonInv[cnt].SlotId.Equals(objName))
                // Break loop
                break;
        // Return proper index
        return cnt;
    }

    public void EnableEquipmentSlot(string objName, ItemClass itemClass)
    {
        // Check if it is head slot
        if (objName.Equals(HeadSlotId))
        {
            // Show hair
            transform.Find(ItemClass.Hair).GetComponent<SkinnedMeshRenderer>().enabled = true;
            // Activate hair
            IsHair = true;
            // Enable head slot
            HeadSlot.IsSlotActive = true;
            // Remove item name
            HeadSlot.ItemName = null;
            // Destroy item
            Destroy(GameObject.Find(ItemClass.HeadItemHolder + itemClass.Kind).transform.gameObject);
        }
        // Check if it is torso slot
        if (objName.Equals(TorsoSlotId))
        {
            // Check if it is ordinary armor
            if (itemClass.Rank.Equals(ItemDatabase.Ordinary))
            {
                // Disable ordinary part
                transform.Find(ItemClass.Ordinary).gameObject.SetActive(false);
                IsOrdinary = false;
            }
            // Check if it is elite armor
            else if (itemClass.Rank.Equals(ItemDatabase.Elite))
            {
                // Disable ordinary part
                transform.Find(ItemClass.Ordinary).gameObject.SetActive(false);
                IsOrdinary = false;
                // Disable elite part
                transform.Find(ItemClass.Elite).gameObject.SetActive(false);
                IsElite = false;
            }
            // It is legendary item
            else
            {
                // Disable ordinary part
                transform.Find(ItemClass.Ordinary).gameObject.SetActive(false);
                IsOrdinary = false;
                // Disable elite part
                transform.Find(ItemClass.Elite).gameObject.SetActive(false);
                IsElite = false;
                // Disable legendary part
                transform.Find(ItemClass.Legendary).gameObject.SetActive(false);
                IsLegendary = false;
            }
            // Enable torso slot
            TorsoSlot.IsSlotActive = true;
            // Remove item name
            TorsoSlot.ItemName = null;
        }
        // Check if it is right hand slot
        if (objName.Equals(RightHandSlotId))
        {
            // Enable right hand slot
            RightHandSlot.IsSlotActive = true;
            // Remove item name
            RightHandSlot.ItemName = null;
            // Destroy item
            Destroy(GameObject.Find(ItemClass.RightItemHolder + itemClass.Kind).transform.gameObject);
        }
        // Check if it is left hand slot
        if (objName.Equals(LeftHandSlotId))
        {
            // Enable left hand slot
            LeftHandSlot.IsSlotActive = true;
            // Remove item name
            LeftHandSlot.ItemName = null;
            // Destroy item
            Destroy(GameObject.Find(ItemClass.LeftItemHolder + itemClass.Kind).transform.gameObject);
        }
        // Check if it is feet slot
        if (objName.Equals(FeetSlotId))
        {
            // Enable feet slot
            FeetSlot.IsSlotActive = true;
            // Remove item name
            FeetSlot.ItemName = null;
        }
    }

    // Get proper object in game
    public GameObject GetProperGameObject(string objName)
    {
        // Get all objects
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();
        // Search proper object
        foreach (GameObject obj in objects)
        {
            // Check name of object
            if (obj.name.Equals(objName))
                // Return proper object
                return obj;
        }
        // Return null if object not found
        return null;
    }

    // Check if active item is moving from slot to another equipment slot
    public bool ValidateItemMove(string objName, Slot equipmentSlot)
    {
        // Check if item is moving from equipment slot to another equipment slot (it is impossible)
        if (RectTransformUtility.RectangleContainsScreenPoint(equipmentSlot.SlotRect, Input.mousePosition)
            && (objName.Equals(HeadSlotId) || objName.Equals(TorsoSlotId) || objName.Equals(RightHandSlotId)
            || objName.Equals(LeftHandSlotId) || objName.Equals(FeetSlotId)))
            // Leave item in slot
            return true;
        // Check other action
        return false;
    }

    // Check if selected item can by use
    public int ValidateItemEquip(string objName, string itemTypeId, string equiopmentSlotId,
        ItemClass itemClass, ref Slot slot)
    {
        // Check if cursor hover equipment slot
        if (!RectTransformUtility.RectangleContainsScreenPoint(slot.SlotRect, Input.mousePosition))
            // Go to drop item
            return DropItem;
        // Check if it is the same slot
        if (objName.Equals(slot.SlotId))
            // Leave item in this slot
            return LeaveItem;
        // Check if slot is active and item fit to slot
        if (!slot.IsSlotActive || !itemClass.Type.Equals(itemTypeId))
            // Leave item in slot
            return ResetItem;
        // Set item mesh
        SetItemMesh(itemClass);
        // Set new parent of current item
        itemClass.transform.SetParent(GameObject.Find(equiopmentSlotId).transform);
        // Set new position of current item
        itemClass.transform.localPosition = Vector3.zero;
        // Disable equipment slot
        slot.IsSlotActive = false;
        // Set item name
        slot.ItemName = itemClass.Kind;
        // Activate item
        return MoveItem;
    }

    // Check if active item is moving to inventory slot or dropping to ground
    public bool CheckActiveItemMoveDrop(string objName, string equipmentSlotId, ref Slot equipmentSlot,
        ItemClass itemClass)
    {
        if (objName.Equals(equipmentSlotId))
        {
            // Check if this item is for right hand
            if (equipmentSlotId.Equals(RightHandSlotId))
                // Delete 3D object
                Destroy(transform.Find(ItemClass.RightItemHolder).GetChild(0)
                    .GetComponent<Transform>().gameObject);
            // Check if this item is for left hand
            if (equipmentSlotId.Equals(LeftHandSlotId))
                // Delete 3D object
                Destroy(transform.Find(ItemClass.LeftItemHolder).GetChild(0)
                    .GetComponent<Transform>().gameObject);
            // Check if this item is for head
            if (equipmentSlotId.Equals(HeadSlotId))
            {
                // Show hair
                transform.Find(ItemClass.Hair).GetComponent<SkinnedMeshRenderer>().enabled = true;
                // Activate hair
                IsHair = true;
                // Delete 3D object
                Destroy(transform.Find(ItemClass.HeadItemHolder).GetChild(0)
                    .GetComponent<Transform>().gameObject);
            }
            // Check if this item is for torso
            if (equipmentSlotId.Equals(TorsoSlotId))
            {
                // Check if it is ordinary armor
                if (itemClass.Rank.Equals(ItemDatabase.Ordinary))
                {
                    // Disable ordinary part
                    transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsOrdinary = false;
                }
                // Check if it is elite armor
                else if (itemClass.Rank.Equals(ItemDatabase.Elite))
                {
                    // Disable ordinary part
                    transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsOrdinary = false;
                    // Disable elite part
                    transform.Find(ItemClass.Elite).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsElite = false;
                }
                // It is legendary item
                else
                {
                    // Disable ordinary part
                    transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsOrdinary = false;
                    // Disable elite part
                    transform.Find(ItemClass.Elite).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsElite = false;
                    // Disable legendary part
                    transform.Find(ItemClass.Legendary).GetComponent<SkinnedMeshRenderer>().enabled = false;
                    IsLegendary = false;
                }
            }
            // Enable equipment slot
            equipmentSlot.IsSlotActive = true;
            // Remove item name
            equipmentSlot.ItemName = null;
            // Move or drop item
            return true;
        }
        // Check other action
        return false;
    }

    // Check if hero tries move item to trade slot while trading
    public void CheckTradeGridCollision()
    {
        // Check mouse position while item is dragging
        if (RectTransformUtility.RectangleContainsScreenPoint(_tradeGridRect, Input.mousePosition))
        {
            // Show trade hint
            _gameInterface.ActivateElement(_gameInterface.TradeHintImg.transform);
            // Disable buttons action
            _gameInterface.IsTradeHint = true;
            // Disable inventory exit button
            _gameInterface.InvExitBtn.interactable = false;
        }
    }

    // Check if hero wants to buy some item
    public void CheckItemBuying(string objName, ItemClass itemClass)
    {
        // Check if hero has gold
        if (Gold < itemClass.Value)
        {
            // Update trade info
            _gameInterface.AdaptTradeInfo(GameInterface.HeroNoGold);
            // Break action
            return;
        }
        // Check if hero has capacity
        if (_heroClass.Capacity < itemClass.Weight
            || (ActiveInvSlots.Equals(0) && ActivePotionSlots.Equals(0)))
        {
            // Update trade info
            _gameInterface.AdaptTradeInfo(GameInterface.HeroNoCapacity);
            // Break action
            return;
        }
        // Check if item is potion
        if (itemClass.Type.Equals(ItemDatabase.Potion))
        {
            // Search potion slots
            for (int cnt = 0; cnt < PotionSlots; cnt++)
                // Check which slot is enabled
                if (HeroPotions[cnt].IsSlotActive)
                {
                    // Check if item was bought
                    BuyItem(objName, cnt, HeroPotions, ref itemClass);
                    // Decrement count of active potion slots
                    ActivePotionSlots--;
                    // Break action
                    return;
                }
        }
        // Search inventory slots
        for (int cnt = 0; cnt < InvSlots; cnt++)
            // Check which slot is enabled
            if (HeroInv[cnt].IsSlotActive)
            {
                // Buy item
                BuyItem(objName, cnt, HeroInv, ref itemClass);
                // Decrement count of active inventory slots
                ActiveInvSlots--;
                // Break action
                return;
            }
    }

    // Check if hero wants to sell some item
    public void CheckItemSelling(string objName, ItemClass itemClass)
    {
        // Check if person has gold
        if (_heroBehavior.PersonClass.Gold < itemClass.Value / ItemClass.TradeMod)
        {
            // Update trade info
            _gameInterface.AdaptTradeInfo(GameInterface.TraderNoGold);
            // Break action
            return;
        }
        // Check if person has capacity
        if (ActiveTradeSlots.Equals(0))
        {
            // Update trade info
            _gameInterface.AdaptTradeInfo(GameInterface.TraderNoCapacity);
            // Break action
            return;
        }
        // Search trade slots
        for (int cnt = 0; cnt < TradeSlots; cnt++)
            // Check which slot is enabled
            if (PersonInv[cnt].IsSlotActive)
            {
                // Sell item
                SellItem(objName, cnt, ref itemClass);
                // Break action
                return;
            }
    }

    // Buy some item
    public void BuyItem(string objName, int cnt, Slot[] slots, ref ItemClass itemClass)
    {
        // Create new item slot
        GameObject itemSlot = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ItemSlotId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Set proper parent of item in inventory
        itemSlot.transform.SetParent(slots[cnt].SlotRect.transform);
        // Set proper scale of item in inventory
        itemSlot.transform.localScale = Vector3.one;
        // Set proper position of item in inventory
        itemSlot.transform.localPosition = Vector3.zero;
        // Change slot item name
        itemSlot.name = itemClass.Kind;
        // Get item paremeters
        ItemClass itemSlotClass = itemSlot.GetComponent<ItemClass>();
        // Initialize item
        itemSlotClass.Init(itemSlot.name);
        // Set that slot is disabled
        slots[cnt].IsSlotActive = false;
        // Set item name
        slots[cnt].ItemName = itemClass.Kind;
        // Decrement hero capacity
        _heroParameter.AdaptStats(HeroClass.CapacityId, -itemClass.Weight);
        // Get current slot
        int curSlot = GetTradeSlotIndex(objName);
        // Enable trade slot
        PersonInv[curSlot].IsSlotActive = true;
        // Remove item name
        PersonInv[curSlot].ItemName = null;
        // Increment count of active trade slots
        ActiveTradeSlots++;
        // Decrement hero gold
        AdaptHeroGold(-itemClass.Value);
        // Increment person gold
        AdaptPersonGold(itemClass.Value);
        // Change item view
        itemSlot.GetComponent<Image>().sprite = itemSlotClass.Sprite;
        // Delete object from trader inventory
        Destroy(itemClass.gameObject);
        // Update trade info
        _gameInterface.AdaptTradeInfo(GameInterface.BuyItem);
        // Hide info panel
        _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
        // Play proper sound
        PlayProperSound(itemClass.Type);
    }

    // Sell some item
    public void SellItem(string objName, int cnt1, ref ItemClass itemClass)
    {
        // Create new item slot
        GameObject itemSlot = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ItemSlotId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Set proper parent of item in inventory
        itemSlot.transform.SetParent(PersonInv[cnt1].SlotRect.transform);
        // Set proper scale of item in inventory
        itemSlot.transform.localScale = Vector3.one;
        // Set proper position of item in inventory
        itemSlot.transform.localPosition = Vector3.zero;
        // Change slot item name
        itemSlot.name = itemClass.Kind;
        // Get item paremeters
        ItemClass itemSlotClass = itemSlot.GetComponent<ItemClass>();
        // Initialize item
        itemSlotClass.Init(itemSlot.name);
        // Set that slot is disabled
        PersonInv[cnt1].IsSlotActive = false;
        // Set item name
        PersonInv[cnt1].ItemName = itemClass.Kind;
        // Increment hero capacity
        _heroParameter.AdaptStats(HeroClass.CapacityId, itemClass.Weight);
        // Check if item is in inventory slot
        if (objName.Contains(InvSlotId))
        {
            int curSlot = GetInvSlotIndex(objName);
            // Enable inventory slot
            HeroInv[curSlot].IsSlotActive = true;
            // Remove item name
            HeroInv[curSlot].ItemName = null;
            // Increment count of active inventory slots
            ActiveInvSlots++;
        }
        // Check if item is in potion slot
        else if (objName.Contains(PotionSlotId))
        {
            int curSlot = GetPotionSlotIndex(objName);
            // Enable potion slot
            HeroPotions[curSlot].IsSlotActive = true;
            // Remove item name
            HeroPotions[curSlot].ItemName = null;
            // Increment count of active inventory slots
            ActivePotionSlots++;
            // Change layout hierarchy
            _gameInterface.SlotPanelImg.transform.SetParent(_bottomPanelTrans);
            // Change layout order
            _gameInterface.SlotPanelImg.transform.SetAsLastSibling();
        }
        // It is some equipment slot
        else
        {
            // Enable proper equipment slot
            EnableEquipmentSlot(objName, itemClass);
            // Search proper stats
            for (int cnt2 = 0; cnt2 < itemClass.Stats.Length; cnt2++)
                // Decrement hero stats
                _heroParameter.AdaptStats(itemClass.Stats[cnt2], -itemClass.Effect[cnt2]);
        }
        // Decrement count of active trade slots
        ActiveTradeSlots--;
        // Increment hero gold
        AdaptHeroGold(itemClass.Value / ItemClass.TradeMod);
        // Decrement person gold
        AdaptPersonGold(-itemClass.Value / ItemClass.TradeMod);
        // Change item view
        itemSlot.GetComponent<Image>().sprite = itemSlotClass.Sprite;
        // Delete object from hero inventory
        Destroy(itemClass.gameObject);
        // Update trade info
        _gameInterface.AdaptTradeInfo(GameInterface.SellItem);
        // Hide info panel
        _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
        // Play proper sound
        PlayProperSound(itemClass.Type);
    }

    // Set proper mesh for hero item
    public void SetItemMesh(ItemClass itemClass)
    {
        // Check if item has mesh
        if (itemClass.Type.Equals(ItemDatabase.Feet))
            // Break action
            return;
        // Check if item is for torso
        if (itemClass.Type.Equals(ItemDatabase.Torso))
        {
            // Check if it is ordinary armor
            if (itemClass.Rank.Equals(ItemDatabase.Ordinary))
            {
                // Enable ordinary part
                transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsOrdinary = true;
            }
            // Check if it is elite armor
            else if (itemClass.Rank.Equals(ItemDatabase.Elite))
            {
                // Enable ordinary part
                transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsOrdinary = true;
                // Enable elite part
                transform.Find(ItemClass.Elite).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsElite = true;
            }
            // It is legendary item
            else
            {
                // Enable ordinary part
                transform.Find(ItemClass.Ordinary).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsOrdinary = true;
                // Enable elite part
                transform.Find(ItemClass.Elite).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsElite = true;
                // Enable legendary part
                transform.Find(ItemClass.Legendary).GetComponent<SkinnedMeshRenderer>().enabled = true;
                IsLegendary = true;
            }
            // Break action
            return;
        }
        // Create new 3D object
        GameObject activatedItem = Instantiate(Resources.Load(ItemDatabase.Prefabs + itemClass.Kind),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity) as GameObject;
        // Get item paremeters (activated item)
        ItemClass activatedItemClass = activatedItem.GetComponentInChildren<ItemClass>();
        // Generate new name for item
        string newName = activatedItem.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
        // Change dropped item name
        activatedItem.name = newName;
        // Get cursor hover action
        CursorHover cursorHover = activatedItem.GetComponentInChildren<CursorHover>();
        // Inactivate object (destroy hint)
        cursorHover.IsObjectInactive = true;
        // Check if item is for right hand
        if (itemClass.Type.Equals(ItemDatabase.RightHand))
            // Set new parent of created object
            activatedItem.transform.SetParent(transform.Find(ItemClass.RightItemHolder)
                .GetComponent<Transform>());
        // Check if item is for left hand
        if (itemClass.Type.Equals(ItemDatabase.LeftHand))
            // Set new parent of created object
            activatedItem.transform.SetParent(transform.Find(ItemClass.LeftItemHolder)
                .GetComponent<Transform>());
        // Check if item is for head
        if (itemClass.Type.Equals(ItemDatabase.Head))
        {
            // Hide hair
            transform.Find(ItemClass.Hair).GetComponent<SkinnedMeshRenderer>().enabled = false;
            // Deactivate hair
            IsHair = false;
            // Set new parent of created object
            activatedItem.transform.SetParent(transform.Find(ItemClass.HeadItemHolder)
                .GetComponent<Transform>());
        }
        // Destroy collider
        Destroy(activatedItem.GetComponentInChildren<Collider>());
        // Change hierarchy
        activatedItem.transform.SetAsFirstSibling();
        // Change item tag
        activatedItem.transform.GetChild(0).tag = ItemClass.Untagged;
        // Change item layer
        activatedItem.transform.GetChild(0).gameObject.layer = GameInterface.IgnoreRaycastId;
        // Set new position of created object
        activatedItem.transform.localPosition = itemClass.Pos;
        // Set new rotation of created object
        activatedItem.transform.localRotation = Quaternion.Euler(itemClass.Rot);
    }

    // Initialize proper equipment slot
    public void InitEquipmentSlot(ref Slot slot, string slotId)
    {
        slot.SlotId = slotId;
        slot.IsSlotActive = true;
        slot.ItemName = null;
        slot.SlotRect = GameObject.Find(slotId).GetComponent<RectTransform>();
    }

    // Random position during dropping item
    public Vector3 RandomItemPosition()
    {
        // Random coordinates
        int xPos = Random.Range(MinDropDist, MaxDropDist);
        int zPos = Random.Range(MinDropDist, MaxDropDist);
        // Correct coordinates
        if (xPos.Equals(0) && zPos.Equals(0))
        {
            if (xPos.Equals(0))
                // Change coordinate
                xPos = MinDropDist;
            else
                // Change coordinate
                zPos = AvgDropDist;
        }
        // Return position of item
        return new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);
    }

    // Generate some items to sell when hero is trading
    public void GenerateTradeItems()
    {
        // Check if person can sell hero some item
        if (MinTradeItem.Equals(null) || MaxTradeItem.Equals(null))
            // Break action
            return;
        // Random some range
        int tradeItemCount = Random.Range(MinTradeItem, MaxTradeItem);
        // Create items
        for (int cnt = 0; cnt < tradeItemCount; cnt++)
        {
            // Random item
            string tradeItem = _heroBehavior.PersonClass
                .Items[Random.Range(0, _heroBehavior.PersonClass.Items.Length)];
            // Create new item slot
            GameObject itemSlot = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ItemSlotId),
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity) as GameObject;
            // Set proper parent of item in inventory
            itemSlot.transform.SetParent(PersonInv[cnt].SlotRect.transform);
            // Set proper scale of item in inventory
            itemSlot.transform.localScale = Vector3.one;
            // Set proper position of item in inventory
            itemSlot.transform.localPosition = Vector3.zero;
            // Change item slot name
            itemSlot.name = tradeItem;
            // Get item paremeters
            ItemClass itemSlotClass = itemSlot.GetComponent<ItemClass>();
            // Initialize item
            itemSlotClass.Init(itemSlot.name);
            // Set that slot is disabled
            PersonInv[cnt].IsSlotActive = false;
            // Set item name
            PersonInv[cnt].ItemName = itemSlotClass.Kind;
            // Decrement count of active slots
            ActiveTradeSlots--;
            // Change item view
            itemSlot.GetComponent<Image>().sprite = itemSlotClass.Sprite;
        }
    }

    // Destroy trade items after talk
    public void DestroyTradeItems()
    {
        // Destroy items
        for (int cnt = 0; cnt < TradeSlots; cnt++)
            // Check if slot is disabled
            if (!PersonInv[cnt].IsSlotActive)
            {
                // Destroy item from slot
                Destroy(PersonInv[cnt].SlotRect.GetChild(0).gameObject);
                // Set that slot is enabled
                PersonInv[cnt].IsSlotActive = true;
                // Remove item name
                PersonInv[cnt].ItemName = null;
                // Increment count of active slots
                ActiveTradeSlots++;
            }
    }

    // Initialize proper item slot
    public void InitSlotItem(Slot slot)
    {
        // Create new item slot
        GameObject itemSlot = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ItemSlotId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Set proper parent of item in inventory
        itemSlot.transform.SetParent(slot.SlotRect.transform);
        // Set proper scale of item in inventory
        itemSlot.transform.localScale = Vector3.one;
        // Set proper position of item in inventory
        itemSlot.transform.localPosition = Vector3.zero;
        // Change item slot name
        itemSlot.name = slot.ItemName;
        // Get item paremeters
        ItemClass itemSlotClass = itemSlot.GetComponent<ItemClass>();
        // Initialize item
        itemSlotClass.Init(itemSlot.name);
        // Change item view
        itemSlot.GetComponent<Image>().sprite = itemSlotClass.Sprite;
        // Check if item is in inventory slot or potion slot
        if (slot.SlotId.Contains(InvSlotId) || slot.SlotId.Contains(PotionSlotId))
            // Break action
            return;
        // Set proper item mesh
        SetItemMesh(itemSlotClass);
    }

    // Steal hero gold after death
    public int StealHeroGold()
    {
        // Copy hero gold
        int gold = Gold;
        // Calculate stolen gold
        int stolenGold = gold * stealMod / 100;
        // Check stolen value
        if (gold - stolenGold > 0)
        {
            // Decrement hero gold
            Gold -= stolenGold;
            // Return stolen gold
            return stolenGold;
        }
        // Nothing was stolen
        return 0;
    }

    // Play proper sound after action
    public void PlayProperSound(string name)
    {
        // Search proper sound
        for (int cnt = 0; cnt < SoundDatabase.ItemSounds.Length; cnt++)
            // Check sound name
            if (SoundDatabase.ItemSounds[cnt].Name.Equals(name))
            {
                // Play proper sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.ItemSounds[cnt].Name, SoundDatabase.ItemSounds));
                // Break action
                return;
            }
    }
}