using UnityEngine;

/// <summary>
/// Manages actions performed with the use of the cursor.
/// </summary>
public class CursorHover : MonoBehaviour
{
    // Check if object is inactive
    public bool IsObjectInactive { get; set; }
    // Check if panel is active
    private bool _isShowPanel;
    // Check if panel is exist
    private bool _isPanelExist;
    // Hero class
    private HeroClass _heroClass;
    // Game interface
    private GameInterface _gameInterface;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchPanel();
    }

    // Set basic parameters
    private void Init()
    {
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _gameInterface = GameObject
            .Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        IsObjectInactive = _isShowPanel = _isPanelExist = false;
    }

    /// <summary>
    /// Checks if the cursor hover the object.
    /// </summary>
    private void OnMouseOver()
    {
        // Check if mouse is over UI
        if ((GameMouseAction.IsMouseOverUI() || _gameInterface.IsGamePaused)
            && !_isShowPanel)
        {
            // Destroy panel
            DestroyPanel();
            // Break action
            return;
        }
        // Check if show panel is possible
        if (_isPanelExist || IsObjectInactive || tag.Equals(PersonClass.PersonTag)
            || _heroClass.IsTalking)
            // Break action
            return;
        // Generate panel
        GeneratePanel();
    }

    /// <summary>
    /// Checks if the cursor has left the object.
    /// </summary>
    private void OnMouseExit()
    {
        // Check if destroy panel is possible
        if (!_isPanelExist || _isShowPanel || IsObjectInactive
            || tag.Equals(PersonClass.PersonTag))
            // Break action
            return;
        // Destroy panel
        DestroyPanel();
    }

    /// <summary>
    /// Shows or hides hints about objects on the ground.
    /// </summary>
    private void SwitchPanel()
    {
        // Check if hero is talking
        if (_heroClass.IsTalking || _gameInterface.IsGamePaused)
        {
            // Destroy panel
            DestroyPanel();
            // Break action
            return;
        }
        // Check if showing panel is possible
        if (IsObjectInactive || tag.Equals(ItemClass.ContainerTag)
            || tag.Equals(PersonClass.PersonTag))
            // break action
            return;
        // Check if button is pressed
        if (Input.GetButtonDown(GameInterface.ShowHintBtn))
        {
            // Set that panel is showing
            _isShowPanel = true;
            // Check if panel is exist or check if it is container
            if (_isPanelExist || gameObject.tag.Equals(ItemClass.ContainerTag))
                // Break action
                return;
            // Generate panel
            GeneratePanel();
        }
        // Check if button is released
        if (Input.GetButtonUp(GameInterface.ShowHintBtn))
        {
            // Set that panel is not showing
            _isShowPanel = false;
            // Check if panel exist
            if (!_isPanelExist)
                // Break action
                return;
            // Destroy panel
            DestroyPanel();
        }
    }

    /// <summary>
    /// Generates interactable panel with some hint.
    /// </summary>
    public void GeneratePanel()
    {
        // Check if panel is exist
        if (_isPanelExist)
            // Break action
            return;
        // Generate hint panel
        GameObject panel = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.PanelId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Generate text
        GameObject text = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.TextId),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Change text parent
        text.transform.SetParent(panel.transform);
        // Generate new name for panel
        string panelName = panel.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
        // Change panel name
        panel.name = panelName;
        // Generate new name for text
        string textName = text.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
        // Change text name
        text.name = textName;
        // Get text mesh
        TextMesh textMesh = text.GetComponent<TextMesh>();
        // Change parent
        panel.transform.SetParent(transform);
        // Change hierarchy
        panel.transform.SetAsFirstSibling();
        // Check if object is item
        if (tag.Equals(ItemClass.ItemTag))
        {
            // Get item class
            ItemClass itemClass = GetComponent<ItemClass>();
            // Check if it is gold
            if (itemClass.Type.Equals(ItemClass.GoldTag))
                // Change panel text
                textMesh.text = itemClass.Value + " " + itemClass.Type;
            // It is item
            else
                // Change panel text
                textMesh.text = itemClass.Kind;
            // Set new tag
            panel.tag = ItemClass.ItemTag;
            // Change hint color
            textMesh.color = itemClass.HintColor;
        }
        // Check if object is container
        if (tag.Equals(ItemClass.ContainerTag))
        {
            // Change panel text
            textMesh.text = transform.parent.name;
            // Set new tag
            panel.tag = ItemClass.ContainerTag;
        }
        // Change roatation
        panel.transform.localEulerAngles = new Vector3(ItemClass.xRotPanel,
            ItemClass.yRotPanel - transform.parent.eulerAngles.y, panel.transform.localEulerAngles.z);
        // Change position
        panel.transform.localPosition = new Vector3(panel.transform.localPosition.x,
            ItemClass.yPosHint, panel.transform.localPosition.z);
        // Get sprite renderer
        SpriteRenderer spriteRenderer = panel.GetComponent<SpriteRenderer>();
        // Change panel size
        spriteRenderer.size = new Vector2(ItemClass.TextCharWidth * ItemClass.PanelWidthMod
            * textMesh.text.Length, spriteRenderer.size.y);
        // Get box collider
        BoxCollider boxCollider = panel.GetComponent<BoxCollider>();
        // Change panel collider
        boxCollider.size = new Vector3(ItemClass.TextCharWidth * ItemClass.PanelWidthMod
            * textMesh.text.Length, boxCollider.size.y, boxCollider.size.z);
        // Check if it is container
        if (tag.Equals(ItemClass.ContainerTag))
            // Destroy box collider
            Destroy(boxCollider);
        // Set that panel is exist
        _isPanelExist = true;
    }

    /// <summary>
    /// Destroys created panel with some hint.
    /// </summary>
    public void DestroyPanel()
    {
        // Check if panel is exist
        if (!_isPanelExist)
            // Break action
            return;
        // Destroy panel object
        Destroy(transform.GetChild(0).gameObject);
        // Set that panel is not exist
        _isPanelExist = false;
    }

    /// <summary>
    /// Disables some item and hides the hint when it is falling on the ground.
    /// </summary>
    public void DisableDroppedItem()
    {
        // Inactivate object (destroy panel)
        IsObjectInactive = true;
        // Destroy panel
        DestroyPanel();
    }

    /// <summary>
    /// Enables some object after playing the falling animation.
    /// </summary>
    public void EnableDroppedItem()
    {
        // Activate object (generate panel)
        IsObjectInactive = false;
    }
}