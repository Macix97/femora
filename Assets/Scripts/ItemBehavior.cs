using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Describes the behavior of the items in the game.
/// </summary>
public class ItemBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Hero class
    private HeroClass _heroClass;
    // Hero inventory
    private HeroInventory _heroInventory;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Hero sound
    private HeroSound _heroSound;
    // Game interface
    private GameInterface _gameInterface;
    // Slot transform
    private Transform _slotTrans;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _heroParameter = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroParameter>();
        _heroInventory = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroInventory>();
        _heroSound = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroSound>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
            // Break action
            return;
        // Show info panel
        _heroInventory.ShowItemSlotInfo(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if clicking is possible
        if (_heroParameter.IsHeroDead() || _gameInterface.IsTradeHint)
            // Break action
            return;
        // Check if hero is talking
        if (_heroClass.IsTalking)
        {
            // Check left mouse button double click (sell item)
            if (eventData.button.Equals(PointerEventData.InputButton.Left) && eventData.clickCount.Equals(2)
                && !transform.parent.name.Contains(HeroInventory.TradeSlotId))
                // Check if hero can sell item
                _heroInventory.CheckItemSelling(transform.parent.name, GetComponent<ItemClass>());
            // Check left mouse button double click (buy item)
            if (eventData.button.Equals(PointerEventData.InputButton.Left) && eventData.clickCount.Equals(2)
                && transform.parent.name.Contains(HeroInventory.TradeSlotId))
                // Check if hero can buy item
                _heroInventory.CheckItemBuying(transform.parent.name, GetComponent<ItemClass>());
        }
        // Check if right mouse button is clicked
        if (!eventData.button.Equals(PointerEventData.InputButton.Right))
            // Break action
            return;
        // Use selected item
        if (_heroInventory.IsItemUse(eventData.pointerCurrentRaycast.gameObject.transform.parent.name))
            // Hide info panel
            _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
            // Break action
            return;
        // Hide info panel
        _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Set that some item is dragging
        _gameInterface.IsDrag = true;
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
            // Break action
            return;
        // Check if item is in trade slot
        if (transform.parent.name.Contains(HeroInventory.TradeSlotId))
            // Break action
            return;
        // Get slot transform
        _slotTrans = transform.parent.GetComponent<Transform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
            // Break action
            return;
        // Check if item is in trade slot
        if (transform.parent.name.Contains(HeroInventory.TradeSlotId))
            // Break action
            return;
        // Set new layout hierarchy
        transform.SetParent(transform.root);
        // Hide info panel
        _gameInterface.DeactivateElement(_gameInterface.SlotPanelImg.transform);
        // Change hierarchy in graphical layout
        transform.parent.SetAsLastSibling();
        // Set new position depends on cursor position
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Set that any item is not dragging
        _gameInterface.IsDrag = false;
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
            // Break action
            return;
        // Check if item is in trade slot
        if (transform.parent.name.Contains(HeroInventory.TradeSlotId))
            // Break action
            return;
        // Set default layout hierarchy
        transform.SetParent(_slotTrans);
        // Reset position of dragged object
        transform.localPosition = Vector3.zero;
        // Check item move
        int respond1 = _heroInventory.CheckItemMove(transform.parent.name);
        // Check item equip
        int respond2 = _heroInventory.CheckItemEquip(transform.parent.name);
        // Move item to other slot or leave in same slot
        if (respond1.Equals(HeroInventory.MoveItem) || respond1.Equals(HeroInventory.LeaveItem)
            || respond2.Equals(HeroInventory.MoveItem) || respond2.Equals(HeroInventory.LeaveItem))
        {
            // Show info panel
            _heroInventory.ShowItemSlotInfo(eventData.pointerCurrentRaycast.gameObject);
            // Play proper sound
            PlayProperSound();
            // Break action
            return;
        }
        // Reset item (wrong move)
        if (respond1.Equals(HeroInventory.ResetItem) || respond2.Equals(HeroInventory.ResetItem))
        {
            // Play proper sound
            PlayProperSound();
            // Play impossible sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Impossible, _heroSound.HeroSounds));
            // Break action
            return;
        }
        // Wrong move while hero is talking
        if (_heroClass.IsTalking)
        {
            // Check if hero tries move item to trade slot
            _heroInventory.CheckTradeGridCollision();
            // Play proper sound
            PlayProperSound();
            // Play impossible sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Impossible, _heroSound.HeroSounds));
            // Break action
            return;
        }
        // Item stay in inventory
        if (_heroInventory.CheckItemDrop(transform.parent.name).Equals(HeroInventory.ResetItem))
        {
            // Play proper sound
            PlayProperSound();
            // Play impossible sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Impossible, _heroSound.HeroSounds));
            // Break action
            return;
        }
        // Item is dropping
        else
            // Play swoosh sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Swoosh, SoundDatabase.ItemSounds));
    }

    /// <summary>
    /// Plays proper item sound during an action.
    /// </summary>
    public void PlayProperSound()
    {
        // Get item type
        string type = GetComponent<ItemClass>().Type;
        // Search proper sound
        for (int cnt = 0; cnt < SoundDatabase.ItemSounds.Length; cnt++)
            // Check sound name
            if (SoundDatabase.ItemSounds[cnt].Name.Equals(type))
            {
                // Play proper sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.ItemSounds[cnt].Name, SoundDatabase.ItemSounds));
                // Break action
                return;
            }
    }
}