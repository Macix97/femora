using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Controls actions related to the game graphics interface and initialize GUI.
/// </summary>
public class GameInterface : MonoBehaviour
{
    // General
    public static readonly string GameInterfaceController = "GameInterfaceController";
    public static readonly string GameController = "Game Controller";
    public static readonly int IgnoreRaycastId = 2;

    // Colors
    public static readonly Color Gold = new Color(0.53f, 0.47f, 0.29f);
    public static readonly Color White = new Color(1f, 1f, 1f);

    // Menu scene
    public static readonly string MenuScene = "Menu";

    // Clicks IDs
    public static readonly int LeftClickId = -1;
    public static readonly int RightClickId = -2;

    // Skill buttons ID
    public static readonly string SkillButtonId = "SkillButton0";

    // Virtual buttons IDs
    public static readonly string Pause = "Pause";
    public static readonly string LeftMouseBtn = "Left Click";
    public static readonly string RightMouseBtn = "Right Click";
    public static readonly string CharWindowBtn = "Show/Hide Character Window";
    public static readonly string SkillsWindowBtn = "Show/Hide Skills Window";
    public static readonly string InvWindowBtn = "Show/Hide Inventory Window";
    public static readonly string AttackBtn = "Attack";
    public static readonly string UseSkillBtn = "Skill0";
    public static readonly string PotionBtn = "PotionSlot0";
    public static readonly string SkillModBtn = "Skill Modifier";
    public static readonly string ShowHintBtn = "Show Hint";

    // GUI Texts
    public static readonly string Femora = "Femora";
    public static readonly string Dead = "You have died.\nPress ESC to continue.\n";
    public static readonly string Video = "Video Quality";
    public static readonly string Audio = "Audio Volume";
    public static readonly string Control = "Game Control";
    public static readonly string CharacterText = "Character";
    public static readonly string SkillsText = "Skills";
    public static readonly string ExitText = "Exit";
    public static readonly string HitChance = "Avarge chance to attack ";
    public static readonly string DuckChance = "Avarge chance to dodge ";

    // Trade events
    public static readonly string StartTrading = "Sell or buy some item!";
    public static readonly string HeroNoGold = "Hero has not enough gold!";
    public static readonly string HeroNoCapacity = "Hero is overloaded!";
    public static readonly string TraderNoGold = "Trader has not enough gold!";
    public static readonly string TraderNoCapacity = "Trader is overloaded!";
    public static readonly string SellItem = "You sold item!";
    public static readonly string BuyItem = "You bought item!";

    //--- Different ---//

    // Potion grid columns
    public static readonly int PotionGridCol = 4;
    // Potion grid column modifier
    public static readonly int PotionGridColMod = 1;
    // Potion grid rows
    public static readonly int PotionGridRow = 2;
    // Camera split view
    public static readonly float CameraSplit = 480f;
    // Camera normal view
    public static readonly float CameraNormal = 0f;
    // Panel margin
    public static readonly float PanelMargin = 32f;
    // Maximal click distance
    public static readonly int MaxDist = 100;
    // Attack rate label modifiers
    public static readonly int AttackRateMod1000 = 1000;
    public static readonly int AttackRateMod100 = 100;
    // Health bar margin (px)
    public static readonly int HealthBarMargin = 48;
    // Space between conversation panels (px)
    public static readonly int ConvPanelSpace = 64;
    // Check if info about experience is active
    public bool IsExpHint { get; set; }
    // Check if trade hint is active
    public bool IsTradeHint { get; set; }
    // Check if some item is dragging
    public bool IsDrag { get; set; }
    // Conversation options count
    public int HeroTextCount { get; set; }
    // Current statment index
    public int CurStatment { get; set; }
    // Mouse hit
    private RaycastHit _hit;
    // Check if left window is open
    private bool _isLeftWindow;
    // Check if right window is open
    private bool _isRightWindow;
    // Isometric camera
    private CameraManager _isoCam;
    // Check if game is paused
    public bool IsGamePaused { get; set; }
    // Quality level
    public int QualityLevel { get; set; }
    // List of black transition states
    public enum TransitionState
    {
        BlackToTrans,
        TransToBlack,
        NoTrans
    };
    // Transition state
    public TransitionState TransState { get; set; }
    // Black screen alpha channel
    private float _blackAlpha;
    // Alpha channel modifier
    private const float _alphaMod = 7.5f;

    // Sliders
    public static readonly string SoundSlider = "SoundSlider";
    public static readonly string MusicSlider = "MusicSlider";

    // Images
    public static readonly string HealthOrbFill = "HealthOrbFill";
    public static readonly string HealthOrbVoid = "HealthOrbVoid";
    public static readonly string EnergyOrbFill = "EnergyOrbFill";
    public static readonly string EnergyOrbVoid = "EnergyOrbVoid";
    public static readonly string ExpBarFill = "ExpBarFill";
    public static readonly string LeftClickImage = "LeftClickImage";
    public static readonly string RightClickImage = "RightClickImage";
    public static readonly string HealthBarFill = "HealthBarFill";
    public static readonly string HealthBarVoid = "HealthBarVoid";
    public static readonly string CreatureType = "CreatureType";
    public static readonly string CreatureNature = "CreatureNature";
    public static readonly string BlackScreen = "BlackScreen";

    // Buttons
    public static readonly string AttrButton = "AttrButton";
    public static readonly string VitalityButton = "VitalityButton";
    public static readonly string WisdomButton = "WisdomButton";
    public static readonly string StrengthButton = "StrengthButton";
    public static readonly string AgilityButton = "AgilityButton";
    public static readonly string CharExitButton = "CharExitButton";
    public static readonly string SkillButton = "SkillButton";
    public static readonly string SkillExitButton = "SkillExitButton";
    public static readonly string InvExitButton = "InvExitButton";
    public static readonly string TradeHintButton = "TradeHintButton";

    // Panels
    public static readonly string MainMenu = "MainMenu";
    public static readonly string VideoMenu = "VideoMenu";
    public static readonly string AudioMenu = "AudioMenu";
    public static readonly string ControlMenu = "ControlMenu";
    public static readonly string CharWindow = "CharWindow";
    public static readonly string SkillWindow = "SkillWindow";
    public static readonly string InventoryWindow = "InventoryWindow";
    public static readonly string MouseSkillPanel = "MouseSkillPanel";
    public static readonly string SkillPanel = "SkillPanel";
    public static readonly string SlotPanel = "SlotPanel";
    public static readonly string HintPanel = "HintPanel";
    public static readonly string DialogueWindow = "DialogueWindow";
    public static readonly string TradeWindow = "TradeWindow";
    public static readonly string BottomPanel = "BottomPanel";
    public static readonly string PotionGrid = "PotionGrid";
    public static readonly string TradeHint = "TradeHint";
    public static readonly string VideoPanel = "VideoPanel";
    public static readonly string AudioPanel = "AudioPanel";
    public static readonly string QualityPanel = "QualityPanel";
    public static readonly string ControlPanel = "ControlPanel";
    public static readonly string ExitPanel = "ExitPanel";
    public static readonly string ReturnMenuPanel = "ReturnMenuPanel";
    public static readonly string ReturnAudioPanel = "ReturnAudioPanel";
    public static readonly string ReturnVideoPanel = "ReturnVideoPanel";
    public static readonly string ReturnControlPanel = "ReturnControlPanel";

    // Texts
    public static readonly string TypeText = "TypeText";
    public static readonly string SexText = "SexText";
    public static readonly string NameText = "NameText";
    public static readonly string LevelText = "LevelText";
    public static readonly string TotalExpText = "TotalExpText";
    public static readonly string NextLvlExpText = "NextLvlExpText";
    public static readonly string VitalityText = "VitalityText";
    public static readonly string WisdomText = "WisdomText";
    public static readonly string StrengthText = "StrengthText";
    public static readonly string AgilityText = "AgilityText";
    public static readonly string AttrPtsText = "AttrPtsText";
    public static readonly string LifeText = "LifeText";
    public static readonly string MagicText = "MagicText";
    public static readonly string DamageText = "DamageText";
    public static readonly string AttackRateText = "AttackRateText";
    public static readonly string DefenceText = "DefenceText";
    public static readonly string ResistMagicText = "ResistMagicText";
    public static readonly string SkillPtsText = "SkillPtsText";
    public static readonly string CapacityText = "CapacityText";
    public static readonly string DodgeChanceText = "DodgeChanceText";
    public static readonly string AttackChanceText = "AttackChanceText";
    public static readonly string MouseSkillText = "MouseSkillText";
    public static readonly string SkillText = "SkillText";
    public static readonly string SlotText = "SlotText";
    public static readonly string HeroGoldText = "HeroGoldText";
    public static readonly string HintText = "HintText";
    public static readonly string PersonNameText = "PersonNameText";
    public static readonly string PersonSpeechText = "PersonSpeechText";
    public static readonly string ButtonPressText = "ButtonPressText";
    public static readonly string TraderNameText = "TraderNameText";
    public static readonly string TraderGoldText = "TraderGoldText";
    public static readonly string TradeInfoText = "TradeInfoText";
    public static readonly string HealthText = "HealthText";
    public static readonly string EnergyText = "EnergyText";
    public static readonly string LocationName = "LocationName";
    public static readonly string MainInfo = "MainInfo";
    public static readonly string CurSoundsText = "CurSoundsText";
    public static readonly string CurMusicText = "CurMusicText";

    //--- Texts ---//

    // Hero health
    public Text HealthTxt { get; set; }
    // Hero energy
    public Text EnergyTxt { get; set; }
    // Creature type
    public Text CreatureTypeTxt { get; set; }
    // Creature nature
    public Text CreatureNatureTxt { get; set; }
    // Hero type
    public Text TypeTxt { get; set; }
    // Hero Sex
    public Text SexTxt { get; set; }
    // Hero Name
    public Text NameTxt { get; set; }
    // Hero level
    public Text LevelTxt { get; set; }
    // Hero total experience
    public Text TotalExpTxt { get; set; }
    // Hero next level experience
    public Text NextLvlExpTxt { get; set; }
    // Hero vitality
    public Text VitalityTxt { get; set; }
    // Hero wisdom
    public Text WisdomTxt { get; set; }
    // Hero strength
    public Text StrengthTxt { get; set; }
    // Hero agility
    public Text AgilityTxt { get; set; }
    // Hero attribute points
    public Text AttrPtsTxt { get; set; }
    // Hero skill points
    public Text SkillPtsTxt { get; set; }
    // Hero life
    public Text LifeTxt { get; set; }
    // Hero magic
    public Text MagicTxt { get; set; }
    // Hero damage
    public Text DamageTxt { get; set; }
    // Hero attack rate
    public Text AttackRateTxt { get; set; }
    // Hero defence
    public Text DefenceTxt { get; set; }
    // Hero resist magic
    public Text ResistMagicTxt { get; set; }
    // Hero dodge chance
    public Text DodgeChanceTxt { get; set; }
    // Hero attack chance
    public Text AttackChanceTxt { get; set; }
    // Hero capacity
    public Text CapacityTxt { get; set; }
    // Hero gold
    public Text HeroGoldTxt { get; set; }
    // Hint
    public Text HintTxt { get; set; }
    // Mouse skill
    public Text MouseSkillTxt { get; set; }
    // Skill
    public Text SkillTxt { get; set; }
    // Slot
    public Text SlotTxt { get; set; }
    // Person name
    public Text PersonNameTxt { get; set; }
    // Person speech
    public Text PersonSpeechTxt { get; set; }
    // Button press
    public Text ButtonPressTxt { get; set; }
    // Trader name
    public Text TraderNameTxt { get; set; }
    // Trader gold
    public Text PersonGoldTxt { get; set; }
    // Trade info
    public Text TradeInfoTxt { get; set; }
    // Location name
    public Text LocationNameTxt { get; set; }
    // Main info
    public Text MainInfoTxt { get; set; }
    // Current sounds text
    public Text CurSoundsTxt { get; set; }
    // Current music text
    public Text CurMusicTxt { get; set; }

    //--- Buttons ---//

    // Vitality button
    public Button VitalityBtn { get; set; }
    // Wisdom button
    public Button WisdomBtn { get; set; }
    // Strength button
    public Button StrengthBtn { get; set; }
    // Agility button
    public Button AgilityBtn { get; set; }
    // Skill buttons
    public Button[] SkillBtns { get; set; }
    // Attribute button
    public Button AttrBtn { get; set; }
    // Skill button
    public Button SkillButtonBtn { get; set; }
    // Character exit button
    public Button CharExitBtn { get; set; }
    // Skill exit button
    public Button SkillExitBtn { get; set; }
    // Inventory exit button
    public Button InvExitBtn { get; set; }

    //--- Images ---//

    // Health orb image
    public Image HealthOrbImg { get; set; }
    // Empty health orb image
    public Image HealthOrbVoidImg { get; set; }
    // Energy orb image
    public Image EnergyOrbImg { get; set; }
    // Empty energy orb image
    public Image EnergyOrbVoidImg { get; set; }
    // Health bar fill image
    public Image HealthBarFillImg { get; set; }
    // Health bar void image
    public Image HealthBarVoidImg { get; set; }
    // Character window image
    public Image CharWindowImg { get; set; }
    // Skill window
    public Image SkillWindowImg { get; set; }
    // Inventory window image
    public Image InventoryWindowImg { get; set; }
    // Dialogue window
    public Image DialogueWindowImg { get; set; }
    // Trade window
    public Image TradeWindowImg { get; set; }
    // Bottom panel image
    public Image BottomPanelImg { get; set; }
    // Hint panel image
    public Image HintPanelImg { get; set; }
    // Left mouse click image
    public Image LeftClickImg { get; set; }
    // Right mouse click image
    public Image RightClickImg { get; set; }
    // Mouse skill panel image
    public Image MouseSkillImg { get; set; }
    // Skill panel image
    public Image SkillPanelImg { get; set; }
    // Slot panel image
    public Image SlotPanelImg { get; set; }
    // Potion grid image
    public Image PotionGridImg { get; set; }
    // Trade hint image
    public Image TradeHintImg { get; set; }
    // Experience bar
    public Image ExpBarImg { get; set; }
    // Empty experience bar
    public Image EmptyExpBarImg { get; set; }
    // Main menu
    public Image MainMenuImg { get; set; }
    // Video menu
    public Image VideoMenuImg { get; set; }
    // Audio menu
    public Image AudioMenuImg { get; set; }
    // Control menu
    public Image ControlMenuImg { get; set; }
    // Video panel
    public Image VideoPanelImg { get; set; }
    // Audio panel
    public Image AudioPanelImg { get; set; }
    // Control panel
    public Image ControlPanelImg { get; set; }
    // Exit panel
    public Image ExitPanelImg { get; set; }
    // Quality panel
    public Image QualityPanelImg { get; set; }
    // Return menu panel
    public Image ReturnMenuPanelImg { get; set; }
    // Return video panel
    public Image ReturnVideoPanelImg { get; set; }
    // Return audio panel
    public Image ReturnAudioPanelImg { get; set; }
    // Return control panel
    public Image ReturnControlPanelImg { get; set; }
    // Black screen
    public Image BlackScreenImg { get; set; }

    //--- Sliders ---//

    public Slider SoundSliderSld { get; set; }
    public Slider MusicSliderSld { get; set; }

    //--- Audio ---//

    public AudioSource[] Sounds { get; set; }
    public AudioSource Music { get; set; }

    //--- Own ---//

    // Hero behavior
    private HeroBehavior _heroBehavior;
    // Hero class
    private HeroClass _heroClass;
    // Hero skill
    private HeroSkill _heroSkill;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Hero inventory
    private HeroInventory _heroInventory;
    // Mouse skills
    internal HeroSkillDatabase.Skill LeftSkill;
    internal HeroSkillDatabase.Skill RightSkill;

    //--- Post processing ---//

    [SerializeField]
    public PostProcessProfile[] PostProcessings;
    private PostProcessVolume _postProcessVolume;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckExitButtonHit();
        if (IsGamePaused)
            return;
        AnimateBlackTransition();
        AdaptMouseSkill();
        AdaptVitalityParameter();
        AdaptWisdomParameter();
        AdaptStrengthParameter();
        AdaptAgilityParameter();
        AdaptExpParameter();
        AdaptHeroGoldAmount();
        IsAttrButtonClickable();
        IsSkillButtonClickable();
        CheckMouseHover();
        CheckMouseSkill();
        if (_heroParameter.IsHeroDead())
            return;
        CheckButtonHit();
    }

    // Set basic parameters
    private void Init()
    {
        // Hero
        _heroBehavior = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroBehavior>();
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _heroParameter = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroParameter>();
        _heroSkill = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroSkill>();
        _heroInventory = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroInventory>();
        // Camera
        _isoCam = Camera.main.GetComponent<CameraManager>();
        // Graphical elements
        HealthOrbImg = GameObject.Find(HealthOrbFill).GetComponent<Image>();
        HealthOrbVoidImg = GameObject.Find(HealthOrbVoid).GetComponent<Image>();
        EnergyOrbImg = GameObject.Find(EnergyOrbFill).GetComponent<Image>();
        EnergyOrbVoidImg = GameObject.Find(EnergyOrbVoid).GetComponent<Image>();
        ExpBarImg = GameObject.Find(ExpBarFill).GetComponent<Image>();
        HealthBarFillImg = GameObject.Find(HealthBarFill).GetComponent<Image>();
        HealthBarVoidImg = GameObject.Find(HealthBarVoid).GetComponent<Image>();
        CharWindowImg = GameObject.Find(CharWindow).GetComponent<Image>();
        SkillWindowImg = GameObject.Find(SkillWindow).GetComponent<Image>();
        InventoryWindowImg = GameObject.Find(InventoryWindow).GetComponent<Image>();
        DialogueWindowImg = GameObject.Find(DialogueWindow).GetComponent<Image>();
        TradeWindowImg = GameObject.Find(TradeWindow).GetComponent<Image>();
        BottomPanelImg = GameObject.Find(BottomPanel).GetComponent<Image>();
        HintPanelImg = GameObject.Find(HintPanel).GetComponent<Image>();
        MouseSkillImg = GameObject.Find(MouseSkillPanel).GetComponent<Image>();
        SkillPanelImg = GameObject.Find(SkillPanel).GetComponent<Image>();
        LeftClickImg = GameObject.Find(LeftClickImage).GetComponent<Image>();
        RightClickImg = GameObject.Find(RightClickImage).GetComponent<Image>();
        SlotPanelImg = GameObject.Find(SlotPanel).GetComponent<Image>();
        PotionGridImg = GameObject.Find(PotionGrid).GetComponent<Image>();
        TradeHintImg = GameObject.Find(TradeHint).GetComponent<Image>();
        MainMenuImg = GameObject.Find(MainMenu).GetComponent<Image>();
        VideoMenuImg = GameObject.Find(VideoMenu).GetComponent<Image>();
        ControlMenuImg = GameObject.Find(ControlMenu).GetComponent<Image>();
        AudioMenuImg = GameObject.Find(AudioMenu).GetComponent<Image>();
        VideoPanelImg = GameObject.Find(VideoPanel).GetComponent<Image>();
        AudioPanelImg = GameObject.Find(AudioPanel).GetComponent<Image>();
        ControlPanelImg = GameObject.Find(ControlPanel).GetComponent<Image>();
        QualityPanelImg = GameObject.Find(QualityPanel).GetComponent<Image>();
        ExitPanelImg = GameObject.Find(ExitPanel).GetComponent<Image>();
        ReturnMenuPanelImg = GameObject.Find(ReturnMenuPanel).GetComponent<Image>();
        ReturnVideoPanelImg = GameObject.Find(ReturnVideoPanel).GetComponent<Image>();
        ReturnAudioPanelImg = GameObject.Find(ReturnAudioPanel).GetComponent<Image>();
        ReturnControlPanelImg = GameObject.Find(ReturnControlPanel).GetComponent<Image>();
        BlackScreenImg = GameObject.Find(BlackScreen).GetComponent<Image>();
        // Sliders
        SoundSliderSld = GameObject.Find(SoundSlider).GetComponent<Slider>();
        MusicSliderSld = GameObject.Find(MusicSlider).GetComponent<Slider>();
        // Texts
        HealthTxt = GameObject.Find(HealthText).GetComponent<Text>();
        EnergyTxt = GameObject.Find(EnergyText).GetComponent<Text>();
        CreatureTypeTxt = GameObject.Find(CreatureType).GetComponent<Text>();
        CreatureNatureTxt = GameObject.Find(CreatureNature).GetComponent<Text>();
        TypeTxt = GameObject.Find(TypeText).GetComponent<Text>();
        SexTxt = GameObject.Find(SexText).GetComponent<Text>();
        NameTxt = GameObject.Find(NameText).GetComponent<Text>();
        LevelTxt = GameObject.Find(LevelText).GetComponent<Text>();
        VitalityTxt = GameObject.Find(VitalityText).GetComponent<Text>();
        WisdomTxt = GameObject.Find(WisdomText).GetComponent<Text>();
        StrengthTxt = GameObject.Find(StrengthText).GetComponent<Text>();
        AgilityTxt = GameObject.Find(AgilityText).GetComponent<Text>();
        AttrPtsTxt = GameObject.Find(AttrPtsText).GetComponent<Text>();
        SkillPtsTxt = GameObject.Find(SkillPtsText).GetComponent<Text>();
        LifeTxt = GameObject.Find(LifeText).GetComponent<Text>();
        MagicTxt = GameObject.Find(MagicText).GetComponent<Text>();
        DamageTxt = GameObject.Find(DamageText).GetComponent<Text>();
        AttackRateTxt = GameObject.Find(AttackRateText).GetComponent<Text>();
        DefenceTxt = GameObject.Find(DefenceText).GetComponent<Text>();
        DodgeChanceTxt = GameObject.Find(DodgeChanceText).GetComponent<Text>();
        AttackChanceTxt = GameObject.Find(AttackChanceText).GetComponent<Text>();
        ResistMagicTxt = GameObject.Find(ResistMagicText).GetComponent<Text>();
        CapacityTxt = GameObject.Find(CapacityText).GetComponent<Text>();
        HeroGoldTxt = GameObject.Find(HeroGoldText).GetComponent<Text>();
        PersonNameTxt = GameObject.Find(PersonNameText).GetComponent<Text>();
        PersonSpeechTxt = GameObject.Find(PersonSpeechText).GetComponent<Text>();
        ButtonPressTxt = GameObject.Find(ButtonPressText).GetComponent<Text>();
        TraderNameTxt = GameObject.Find(TraderNameText).GetComponent<Text>();
        PersonGoldTxt = GameObject.Find(TraderGoldText).GetComponent<Text>();
        TradeInfoTxt = GameObject.Find(TradeInfoText).GetComponent<Text>();
        TotalExpTxt = GameObject.Find(TotalExpText).GetComponent<Text>();
        NextLvlExpTxt = GameObject.Find(NextLvlExpText).GetComponent<Text>();
        MouseSkillTxt = GameObject.Find(MouseSkillText).GetComponent<Text>();
        SkillTxt = GameObject.Find(SkillText).GetComponent<Text>();
        HintTxt = GameObject.Find(HintText).GetComponent<Text>();
        SlotTxt = GameObject.Find(SlotText).GetComponent<Text>();
        LocationNameTxt = GameObject.Find(LocationName).GetComponent<Text>();
        MainInfoTxt = GameObject.Find(MainInfo).GetComponent<Text>();
        CurSoundsTxt = GameObject.Find(CurSoundsText).GetComponent<Text>();
        CurMusicTxt = GameObject.Find(CurMusicText).GetComponent<Text>();
        // Buttons
        AttrBtn = GameObject.Find(AttrButton).GetComponent<Button>();
        SkillButtonBtn = GameObject.Find(SkillButton).GetComponent<Button>();
        VitalityBtn = GameObject.Find(VitalityButton).GetComponent<Button>();
        WisdomBtn = GameObject.Find(WisdomButton).GetComponent<Button>();
        StrengthBtn = GameObject.Find(StrengthButton).GetComponent<Button>();
        AgilityBtn = GameObject.Find(AgilityButton).GetComponent<Button>();
        SkillBtns = new Button[_heroSkill.HeroSkills.Length];
        CharExitBtn = GameObject.Find(CharExitButton).GetComponent<Button>();
        SkillExitBtn = GameObject.Find(SkillExitButton).GetComponent<Button>();
        InvExitBtn = GameObject.Find(InvExitButton).GetComponent<Button>();
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
            SkillBtns[cnt] = GameObject.Find(SkillButtonId + cnt).GetComponent<Button>();
        // Add event listeners
        SoundSliderSld.onValueChanged.AddListener(delegate { AdaptSoundsVolume(); });
        MusicSliderSld.onValueChanged.AddListener(delegate { AdaptMusicVolume(); });
        // Mouse skills
        LeftSkill = RightSkill = HeroSkillDatabase.NormalAttack;
        LeftSkill.Id = LeftClickId;
        RightSkill.Id = RightClickId;
        // Sounds
        Sounds = GameObject.FindObjectsOfType<AudioSource>();
        // Music
        Music = GameObject.Find(GameController).GetComponent<AudioSource>();
        // Reset parameters
        IsExpHint = IsTradeHint = _isLeftWindow = _isRightWindow = IsGamePaused = IsDrag = false;
        TransState = TransitionState.NoTrans;
        CurStatment = PersonDatabase.NoStatment;
        QualityPanelImg.GetComponentInChildren<Text>().text = QualitySettings.names[0];
        QualityLevel = 0;
        _blackAlpha = 0f;
        // Post processing
        _postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        _postProcessVolume.profile = PostProcessings[0];
        // Prepare game
        PrepareGame();
        // Adapt values
        AdaptHeroClass();
        AdaptVitalityParameter();
        AdaptWisdomParameter();
        AdaptStrengthParameter();
        AdaptAgilityParameter();
        AdaptExpParameter();
        AdaptHeroGoldAmount();
        AdaptSoundsVolume();
        AdaptMusicVolume();
        // Hide elements
        HideHealthBar();
        HideCharWindow();
        HideSkillWindow();
        HideInventoryWindow();
        HideMainInfo();
        HideMenuCrosses(VideoPanelImg.transform);
        HideMenuCrosses(AudioPanelImg.transform);
        HideMenuCrosses(ControlPanelImg.transform);
        HideMenuCrosses(ExitPanelImg.transform);
        HideMenuCrosses(QualityPanelImg.transform);
        HideMenuCrosses(ReturnMenuPanelImg.transform);
        HideMenuCrosses(ReturnVideoPanelImg.transform);
        HideMenuCrosses(ReturnAudioPanelImg.transform);
        HideMenuCrosses(ReturnControlPanelImg.transform);
        DeactivateElement(HealthTxt.transform);
        DeactivateElement(EnergyTxt.transform);
        DeactivateElement(DialogueWindowImg.transform);
        DeactivateElement(TradeWindowImg.transform);
        DeactivateElement(HintPanelImg.transform);
        DeactivateElement(MouseSkillImg.transform);
        DeactivateElement(SkillPanelImg.transform);
        DeactivateElement(SlotPanelImg.transform);
        DeactivateElement(TradeHintImg.transform);
        DeactivateElement(VideoMenuImg.transform);
        DeactivateElement(AudioMenuImg.transform);
        DeactivateElement(ControlMenuImg.transform);
        DeactivateElement(MainMenuImg.transform);
        // Disable buttons
        IsAttrButtonClickable();
        IsSkillButtonClickable();
    }

    /// <summary>
    /// Controls displaying and hiding the dark fade.
    /// </summary>
    public void AnimateBlackTransition()
    {
        // Transition is inactive
        if (TransState.Equals(TransitionState.NoTrans))
            // Break action
            return;
        // Is transparent to black transition
        else if (TransState.Equals(TransitionState.TransToBlack))
        {
            // Increase alpha
            _blackAlpha += Time.deltaTime * _alphaMod;
            // Check current alpha
            if (_blackAlpha >= 1f)
            {
                // Correct alpha
                _blackAlpha = 1f;
                // Update black image
                BlackScreenImg.color = new Color(0f, 0f, 0f, _blackAlpha);
                // Change state
                TransState = TransitionState.BlackToTrans;
                // Break action
                return;
            }
            // Update black image
            BlackScreenImg.color = new Color(0f, 0f, 0f, _blackAlpha);
        }
        // Is black to transparent transition
        else if (TransState.Equals(TransitionState.BlackToTrans))
        {
            // Decrease alpha
            _blackAlpha -= Time.deltaTime * _alphaMod;
            // Check current alpha
            if (_blackAlpha <= 0f)
            {
                // Correct alpha
                _blackAlpha = 0f;
                // Update black image
                BlackScreenImg.color = new Color(0f, 0f, 0f, _blackAlpha);
                // Change state
                TransState = TransitionState.NoTrans;
                // Break action
                return;
            }
            // Update black image
            BlackScreenImg.color = new Color(0f, 0f, 0f, _blackAlpha);
        }
    }

    /// <summary>
    /// Checks if mouse hover some specific UI element.
    /// </summary>
    private void CheckMouseHover()
    {
        // Check if do action is possible
        if (_heroClass.IsTalking || GameMouseAction.IsMouseOverUI())
        {
            // Hide health bar
            HideHealthBar();
            // Break action
            return;
        }
        // Check mouse hover position
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, MaxDist))
            // Break action
            return;
        // Check if mouse hover enemy
        if (_hit.collider.tag.Equals(EnemyClass.EnemyTag))
            ShowHealthBar(EnemyClass.EnemyTag);
        else if (_hit.collider.tag.Equals(PersonClass.PersonTag))
            ShowHealthBar(PersonClass.PersonTag);
        // Hide elements
        else
            HideHealthBar();
    }

    /// <summary>
    /// Checks if some button is hit during conversation.
    /// </summary>
    public void CheckButtonHitOnTalk()
    {
        // Check current statment
        if (!Input.GetButtonDown(LeftMouseBtn))
            // Break action
            return;
        // Check if hero talk with person first time
        if (!_heroBehavior.PersonClass.IsVisited)
        {
            // Set that person is visited
            _heroBehavior.PersonClass.IsVisited = true;
            // Set proper speech
            PersonSpeechTxt.text = _heroBehavior.PersonClass.EnterText;
            // Search conversation panels
            for (int cnt = 0; cnt < HeroTextCount; cnt++)
                // Show conversation panel
                DialogueWindowImg.transform.GetChild(cnt).GetComponent<Transform>().gameObject.SetActive(true);
            // Hide press button text
            ButtonPressTxt.gameObject.SetActive(false);
            // Break action
            return;
        }
        // Check if statment is active
        if (CurStatment.Equals(PersonDatabase.NoStatment))
            // Break action
            return;
        // Check if hero is trading
        if (_heroBehavior.PersonClass.StatmentTypes[CurStatment].Equals(PersonDatabase.TradeStatment))
        {
            // Hide dialogue window
            DeactivateElement(DialogueWindowImg.transform);
            // Show bottom panel
            ActivateElement(BottomPanelImg.transform);
            // Show trade window
            ActivateElement(TradeWindowImg.transform);
            // Show inventory window
            ShowInventoryWindow();
            // Update trade info
            AdaptTradeInfo(StartTrading);
            // Adapt person trade name
            TraderNameTxt.text = _heroBehavior.PersonClass.Type;
            // Adapt person gold amount
            AdaptPeronGoldAmount();
            // Change current statment
            CurStatment = PersonDatabase.NoStatment;
            // Break action
            return;
        }
        // Check if hero ends conversation
        if (_heroBehavior.PersonClass.StatmentTypes[CurStatment].Equals(PersonDatabase.ExitStatment))
        {
            // Exit from conversation
            _heroClass.IsTalking = false;
            // Prepare user interface to action
            PrepareUIToAction();
            // Toogle camera view
            _isoCam.ToggleCameraView();
            // Refresh camera
            SetIsoCameraView();
        }
    }

    /// <summary>
    /// Checks if exit button is hit.
    /// </summary>
    public void CheckExitButtonHit()
    {
        // Check action possibility
        if (!Input.GetButtonDown(Pause))
            // Break action
            return;
        // Check if hero is dead
        if (_heroParameter.IsHeroDead())
        {
            // Activate black transition
            TransState = TransitionState.TransToBlack;
            // Set rise animation
            _heroClass.gameObject.GetComponent<Animator>().SetTrigger(HeroClass.RiseMotion);
            // Set proper camera postion
            _isoCam.transform.position = _isoCam.StartPos;
            // Move hero to start postion
            _heroClass.transform.position = _heroClass.StartPos;
            // Set proper hero rotation
            _heroClass.transform.eulerAngles = _heroClass.StartRot;
            // Ignite particle systems
            _heroParameter.IgniteParticleSystems();
            // Resurrect hero
            _heroClass.CurHealth = _heroClass.MaxHealth;
            _heroClass.CurEnergy = _heroClass.MaxEnergy;
            // Enable hero scripts
            _heroClass.gameObject.GetComponent<HeroBehavior>().enabled = true;
            _heroClass.gameObject.GetComponent<HeroInventory>().enabled = true;
            _heroClass.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            // Reset hero path
            _heroBehavior.ResetHeroPath();
            // Break action
            return;
        }
        // Check if hero is talking
        if (_heroClass.IsTalking)
            // Break action
            return;
        // Check if some hero window is active
        if (CharWindowImg.IsActive() || SkillWindowImg.IsActive() || InventoryWindowImg.IsActive())
        {
            // Hide windows
            HideCharWindow();
            HideSkillWindow();
            HideInventoryWindow();
            // Break action
            return;
        }
        // Game is going on
        if (!IsGamePaused && !IsDrag)
        {
            // Pause game
            IsGamePaused = true;
            // Stop action
            Time.timeScale = Time.fixedDeltaTime = 0f;
            // Adapt main text
            MainInfoTxt.text = Femora;
            // Display new text
            ShowMainInfo();
            // Show main menu
            ActivateElement(MainMenuImg.transform);
            // Show elements
            ActivateElement(VideoPanelImg.transform);
            ActivateElement(AudioPanelImg.transform);
            ActivateElement(ControlPanelImg.transform);
            ActivateElement(ExitPanelImg.transform);
            ActivateElement(ReturnMenuPanelImg.transform);
            // Hide elements
            DeactivateElement(VideoMenuImg.transform);
            DeactivateElement(AudioMenuImg.transform);
            DeactivateElement(ControlMenuImg.transform);
            // Hide health bar
            HideHealthBar();
            // Hide location name
            DeactivateElement(LocationNameTxt.transform);
            // Hide crosses
            HideMenuCrosses(ControlPanelImg.transform);
            HideMenuCrosses(ExitPanelImg.transform);
            // Break action
            return;
        }
        // Game is paused
        else
        {
            // Resume game
            IsGamePaused = false;
            // Resume action
            Time.timeScale = Time.fixedDeltaTime = 1f;
            // Show location name
            ActivateElement(LocationNameTxt.transform);
            // Hide menu text
            HideMainInfo();
            // Hide main menu
            DeactivateElement(MainMenuImg.transform);
            // Break action
            return;
        }
    }

    /// <summary>
    /// Checks if any button is hit during the game.
    /// </summary>
    public void CheckButtonHit()
    {
        // Check if hero is talking with person
        if (_heroClass.IsTalking)
        {
            // Check button hit on talk
            CheckButtonHitOnTalk();
            // Break action
            return;
        }
        // Character window
        if (Input.GetButtonDown(CharWindowBtn))
        {
            if (CharWindowImg.IsActive())
                HideCharWindow();
            else
                ShowCharWindow();
        }
        // Skill window
        if (Input.GetButtonDown(SkillsWindowBtn))
        {
            if (SkillWindowImg.IsActive())
                HideSkillWindow();
            else
            {
                HideInventoryWindow();
                ShowSkillWindow();
            }
        }
        // Inventory window
        if (Input.GetButtonDown(InvWindowBtn))
        {
            if (InventoryWindowImg.IsActive())
                HideInventoryWindow();
            else
            {
                HideSkillWindow();
                ShowInventoryWindow();
            }
        }
        // Shift modifier - left click
        if (!Input.GetButton(SkillModBtn))
        {
            // Attack
            if (Input.GetButtonDown(AttackBtn))
                _heroParameter.ActivateAttack(ref LeftSkill);
            // Skills
            for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
                // Check button click
                if (Input.GetButtonDown(UseSkillBtn + cnt))
                {
                    // Check if hero knows skill
                    if (!_heroParameter.IsSkill(_heroSkill.HeroSkills[cnt]))
                        // Break action
                        return;
                    _heroParameter.CheckSkill(_heroSkill.HeroSkills[cnt], ref LeftSkill);
                    // Break action
                    break;
                }
        }
        // Shift modifier - right click
        if (Input.GetButton(SkillModBtn))
        {
            // Attack
            if (Input.GetButtonDown(AttackBtn))
                _heroParameter.ActivateAttack(ref RightSkill);
            // Skills
            for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
                // Check button click
                if (Input.GetButtonDown(UseSkillBtn + cnt))
                {
                    // Check if hero knows skill
                    if (!_heroParameter.IsSkill(_heroSkill.HeroSkills[cnt]))
                        // Break action
                        return;
                    _heroParameter.CheckSkill(_heroSkill.HeroSkills[cnt], ref RightSkill);
                    // Break action
                    break;
                }
        }
        // Check potion use
        for (int cnt = 0; cnt < PotionGridCol; cnt++)
            // Check button click
            if (Input.GetButtonDown(PotionBtn + cnt))
                // Use potion
                _heroInventory.UsePotionQuick(PotionBtn, cnt);
    }

    /// <summary>
    /// Checks if main button for skills is interactable.
    /// </summary>
    /// <returns>
    /// The state of the button.
    /// </returns>
    public bool IsSkillButtonClickable()
    {
        // Check if hero is talking
        if (_heroClass.IsTalking)
        {
            // Disable skill button
            SkillButtonBtn.interactable = false;
            // Break action
            return false;
        }
        // Check skill points amount
        if (_heroClass.SkillPts > 0 && !_heroParameter.IsHeroDead())
        {
            // Enable skill button
            SkillButtonBtn.interactable = true;
            // Set buttons
            for (int cnt1 = 0; cnt1 < _heroSkill.HeroSkills.Length; cnt1++)
            {
                // Check if hero meets the requirements for skill
                if (_heroSkill.HeroSkills[cnt1].ReqLvl <= _heroClass.Level
                    && _heroSkill.HeroSkills[cnt1].ReqLvl - 2 <= _heroClass.SpentSkillPts)
                {
                    // Check if proper skill do not need specific skill
                    if (_heroSkill.HeroSkills[cnt1].ReqSkill.Equals(HeroSkillDatabase.None))
                        // Enable proper skill
                        SkillBtns[cnt1].interactable = true;
                    // Search needed skill
                    else for (int cnt2 = 0; cnt2 < _heroSkill.HeroSkills.Length; cnt2++)
                        {
                            // Check if it is same skill
                            if (cnt1.Equals(cnt2))
                                // Skip action
                                continue;
                            // Check if proper skill need specific skill and hero knows this skill
                            if (_heroSkill.HeroSkills[cnt1].ReqSkill
                                .Equals(_heroSkill.HeroSkills[cnt2].Kind)
                                && _heroSkill.HeroSkills[cnt2].Level > 0)
                                // Enable proper skill
                                SkillBtns[cnt1].interactable = true;
                            // Hero do not meet requirements
                            else if (_heroSkill.HeroSkills[cnt1].ReqSkill
                                    .Equals(_heroSkill.HeroSkills[cnt2].Kind)
                                    && _heroSkill.HeroSkills[cnt2].Level.Equals(0))
                                // Disable proper skill
                                SkillBtns[cnt1].interactable = false;
                        }
                }
                // Disable button
                else
                    // Disable proper skill
                    SkillBtns[cnt1].interactable = false;
            }
        }
        else
            // Reset buttons
            for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
                SkillBtns[cnt].interactable = SkillButtonBtn.interactable = false;
        // Return button state
        return SkillButtonBtn.interactable;
    }

    /// <summary>
    /// Checks if main button for skills is interactable.
    /// </summary>
    /// <returns>
    /// The state of the button.
    /// </returns>
    public bool IsAttrButtonClickable()
    {
        // Check if hero is talking
        if (_heroClass.IsTalking)
        {
            // Disable attribute button
            AttrBtn.interactable = false;
            // Break action
            return false;
        }
        // Check attribute points amount
        if (_heroClass.AttributePts > 0 && !_heroParameter.IsHeroDead())
            // Set buttons
            AttrBtn.interactable = VitalityBtn.interactable = WisdomBtn.interactable
                = StrengthBtn.interactable = AgilityBtn.interactable = true;
        else
            // Reset buttons
            AttrBtn.interactable = VitalityBtn.interactable = WisdomBtn.interactable
                = StrengthBtn.interactable = AgilityBtn.interactable = false;
        // Return button state
        return AttrBtn.interactable;
    }

    /// <summary>
    /// Shows character window.
    /// </summary>
    public void ShowCharWindow()
    {
        // Set visibility
        CharWindowImg.gameObject.SetActive(true);
        // Set window activity
        _isLeftWindow = true;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Shows skill window.
    /// </summary>
    public void ShowSkillWindow()
    {
        // Set visibility
        SkillWindowImg.gameObject.SetActive(true);
        // Set window activity
        _isRightWindow = true;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Shows inventory window.
    /// </summary>
    public void ShowInventoryWindow()
    {
        // Set visibility
        InventoryWindowImg.gameObject.SetActive(true);
        // Set window activity
        _isRightWindow = true;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Activates selected UI element.
    /// </summary>
    /// <param name="trans">A transform from selected UI object.</param>
    public void ActivateElement(Transform trans)
    {
        // Set visibility
        trans.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows specific hint panel.
    /// </summary>
    /// <param name="trans">A transform from the selected UI object.</param>
    public void ShowHintPanel(Transform transform)
    {
        // Change pivot position of hint panel
        HintPanelImg.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        // Set new location of hint panel
        HintPanelImg.transform.position = transform.position;
        // Change pivot position of hint panel
        HintPanelImg.rectTransform.pivot = new Vector2(0.5f, 0f);
        // Check if mouse hover experience bar
        if (transform.name.Equals(ExpBarFill))
            // Correct location of hint panel
            HintPanelImg.transform.position = new Vector3(transform.position.x,
                transform.position.y + transform.GetComponent<RectTransform>()
                .rect.height / 2, transform.position.z);
        // Set visibility
        HintPanelImg.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows health bar at the top of the screen.
    /// </summary>
    /// <param name="tag">A tag from the selected object.</param>
    public void ShowHealthBar(string tag)
    {
        // Check if it is enemy
        if (tag.Equals(EnemyClass.EnemyTag))
        {
            // Set enemy properties
            EnemyClass enemyClass = _hit.transform.GetComponent<EnemyClass>();
            // Update health bar content
            AdaptHealthBar(enemyClass.transform, EnemyClass.EnemyTag);
            // Set text
            CreatureTypeTxt.text = enemyClass.Type;
            CreatureNatureTxt.text = enemyClass.Nature;
        }
        // Check if it is person
        if (tag.Equals(PersonClass.PersonTag))
        {
            // Set person properties
            PersonClass personClass = _hit.transform.GetComponent<PersonClass>();
            // Update health bar content
            AdaptHealthBar(personClass.transform, PersonClass.PersonTag);
            // Set text
            CreatureTypeTxt.text = personClass.Type;
            CreatureNatureTxt.text = personClass.Nature;
        }
        // Set health bar width
        HealthBarFillImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            CreatureTypeTxt.preferredWidth + HealthBarMargin);
        // Set empty health bar width
        HealthBarVoidImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            CreatureTypeTxt.preferredWidth + HealthBarMargin);
        // Set visibility
        HealthBarFillImg.gameObject.SetActive(true);
        HealthBarVoidImg.gameObject.SetActive(true);
        CreatureTypeTxt.gameObject.SetActive(true);
        CreatureNatureTxt.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides character window.
    /// </summary>
    public void HideCharWindow()
    {
        // Check hint panel parent
        if (HintPanelImg.transform.IsChildOf(CharWindowImg.transform))
            // Hide hint panel
            DeactivateElement(HintPanelImg.transform);
        // Set visibility
        CharWindowImg.gameObject.SetActive(false);
        // Set window activity
        _isLeftWindow = false;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Hides skill window.
    /// </summary>
    public void HideSkillWindow()
    {
        // Check hint panel parent
        if (HintPanelImg.transform.IsChildOf(SkillWindowImg.transform))
            // Hide hint panel
            DeactivateElement(HintPanelImg.transform);
        // Hide skill panel
        DeactivateElement(SkillPanelImg.transform);
        // Set visibility
        SkillWindowImg.gameObject.SetActive(false);
        // Set window activity
        _isRightWindow = false;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Hides inventory window.
    /// </summary>
    public void HideInventoryWindow()
    {
        // Check slot panel parent
        if (!SlotPanelImg.transform.IsChildOf(PotionGridImg.transform))
            // Hide slot panel
            DeactivateElement(SlotPanelImg.transform);
        // Check hint panel parent
        if (HintPanelImg.transform.IsChildOf(InventoryWindowImg.transform))
            // Hide hint panel
            DeactivateElement(HintPanelImg.transform);
        // Set visibility
        InventoryWindowImg.gameObject.SetActive(false);
        // Set window activity
        _isRightWindow = false;
        // Set proper camera view
        SetIsoCameraView();
    }

    /// <summary>
    /// Deactivates selected UI element.
    /// </summary>
    /// <param name="trans">A transform from selected UI object.</param>
    public void DeactivateElement(Transform trans)
    {
        // Set visibility
        trans.gameObject.SetActive(false);
    }

    /// <summary>
    /// Hides health bar at the top of the screen.
    /// </summary>
    public void HideHealthBar()
    {
        // Set visibility
        HealthBarFillImg.gameObject.SetActive(false);
        HealthBarVoidImg.gameObject.SetActive(false);
        CreatureTypeTxt.gameObject.SetActive(false);
        CreatureNatureTxt.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the proper labels in the character window.
    /// </summary>
    public void AdaptHeroClass()
    {
        TypeTxt.text = _heroClass.Type;
        SexTxt.text = _heroClass.Sex;
        NameTxt.text = _heroClass.Name;
    }

    /// <summary>
    /// Updates the graphics and labels that show info about hero vitality.
    /// </summary>
    public void AdaptVitalityParameter()
    {
        // Set health orb
        HealthOrbImg.fillAmount = _heroClass.CurHealth / (float)_heroClass.MaxHealth;
        // Set text above orb
        HealthTxt.text = "Health: " + _heroClass.CurHealth + "/" + _heroClass.MaxHealth;
        // Set vitality text in character window
        VitalityTxt.text = _heroClass.Vitality.ToString();
        // Set life text in character window
        LifeTxt.text = _heroClass.CurHealth + "/" + _heroClass.MaxHealth;
    }

    /// <summary>
    /// Updates the graphics and labels that show info about hero wisdom.
    /// </summary>
    public void AdaptWisdomParameter()
    {
        // Set energy orb
        EnergyOrbImg.fillAmount = _heroClass.CurEnergy / (float)_heroClass.MaxEnergy;
        // Set text above orb
        EnergyTxt.text = "Energy: " + _heroClass.CurEnergy + "/" + _heroClass.MaxEnergy;
        // Set wisdom text in character window
        WisdomTxt.text = _heroClass.Wisdom.ToString();
        // Set magic text in character window
        MagicTxt.text = _heroClass.CurEnergy + "/" + _heroClass.MaxEnergy;
        // Set resist magic text in character window
        ResistMagicTxt.text = _heroClass.ResistMagic.ToString();
    }

    /// <summary>
    /// Updates the graphics and labels that show info about hero strength.
    /// </summary>
    public void AdaptStrengthParameter()
    {
        // Set strength text in character window
        StrengthTxt.text = _heroClass.Strength.ToString();
        // Set capacity text in character window
        CapacityTxt.text = _heroClass.Capacity.ToString("0.0");
        // Check if selected skill increases damage - left click
        if (LeftSkill.Type.Equals(HeroSkillDatabase.Releasable)
            && LeftSkill.Result.Equals(HeroSkillDatabase.AttackMelee))
            // Set damage text in character window
            DamageTxt.text = _heroClass.MinDamage + "-" + _heroClass.MaxDamage + " ("
                                  + (int)(_heroClass.MinDamage + LeftSkill.Effect) + "-"
                                  + (int)(_heroClass.MaxDamage + LeftSkill.Effect) + ")";
        // Check if selected skill increases damage - right click
        else if (RightSkill.Type.Equals(HeroSkillDatabase.Releasable)
                 && RightSkill.Result.Equals(HeroSkillDatabase.AttackMelee))
            // Set damage text in character window
            DamageTxt.text = _heroClass.MinDamage + "-" + _heroClass.MaxDamage + " ("
                                  + (int)(_heroClass.MinDamage + RightSkill.Effect) + "-"
                                  + (int)(_heroClass.MaxDamage + RightSkill.Effect) + ")";
        // Set normal damage
        else
            DamageTxt.text = _heroClass.MinDamage + "-" + _heroClass.MaxDamage;
    }

    /// <summary>
    /// Updates the graphics and labels that show info about hero agility.
    /// </summary>
    public void AdaptAgilityParameter()
    {
        // Set agility text in character window
        AgilityTxt.text = _heroClass.Agility.ToString();
        // Set defence text in character window
        DefenceTxt.text = _heroClass.Defence.ToString();
        // Set dodge chance text in character window
        DodgeChanceTxt.text = _heroClass.DodgeChance + "%";
        // Set attack chance text in character window
        AttackChanceTxt.text = _heroClass.AttackChance + "%";
        // Set attack rate text in character window
        if (_heroClass.AttackRate * AttackRateMod1000 >= AttackRateMod1000)
        {
            // Calculate current attack rate
            float attackRateVal = (AttackRateMod1000 - Mathf.
                Abs(_heroClass.AttackRate * AttackRateMod1000 - AttackRateMod1000));
            // Set proper value
            AttackRateTxt.text = Mathf.Ceil(AttackRateMod100 - Mathf.
                Abs(attackRateVal - AttackRateMod1000)).ToString();
        }
        else
        {
            // Calculate current attack rate
            float attackRateVal = (AttackRateMod1000 + Mathf.
                Abs(_heroClass.AttackRate * AttackRateMod1000 - AttackRateMod1000));
            // Set proper value
            AttackRateTxt.text = Mathf.Ceil(AttackRateMod100 + Mathf.
                Abs(attackRateVal - AttackRateMod1000)).ToString();
        }
    }

    /// <summary>
    /// Updates the graphics and labels that show info about hero experience.
    /// </summary>
    public void AdaptExpParameter()
    {
        // Set experience bar
        ExpBarImg.fillAmount = (_heroClass.TotalExp - _heroClass.LvlStart)
                             / (float)(_heroClass.NextLvLExp - _heroClass.LvlStart);
        // Check if experience hint is active
        if (IsExpHint)
        {
            // Set text above experience bar
            HintTxt.text = "Experience\n" + _heroClass.TotalExp + "/" + _heroClass.NextLvLExp;
            // Change panel size
            HintPanelImg.rectTransform.SetSizeWithCurrentAnchors
                (RectTransform.Axis.Vertical, HintTxt.preferredHeight + PanelMargin);
            HintPanelImg.rectTransform.SetSizeWithCurrentAnchors
                (RectTransform.Axis.Horizontal, HintTxt.preferredWidth + PanelMargin);
        }
        // Set text in character window
        TotalExpTxt.text = "Experience\n" + _heroClass.TotalExp;
        // Set level in character window
        LevelTxt.text = "Level\n" + _heroClass.Level;
        // Set next level experience in character window
        NextLvlExpTxt.text = "Next level\n" + _heroClass.NextLvLExp;
        // Set attribute points in character window
        AttrPtsTxt.text = _heroClass.AttributePts.ToString();
        // Set skill points in skill window
        SkillPtsTxt.text = _heroClass.SkillPts.ToString();
    }

    /// <summary>
    /// Updates the label that show info about hero gold.
    /// </summary>
    public void AdaptHeroGoldAmount()
    {
        // Set proper value
        HeroGoldTxt.text = _heroInventory.Gold.ToString();
    }

    /// <summary>
    /// Updates the label that show info about person gold.
    /// </summary>
    public void AdaptPeronGoldAmount()
    {
        // Set proper value
        PersonGoldTxt.text = _heroBehavior.PersonClass.Gold.ToString();
    }

    /// <summary>
    /// Updates health bar at the top of the screen.
    /// </summary>
    /// <param name="target">A transform that represents human or enemy.</param>
    /// <param name="tag">A tag from the selected object.</param>
    public void AdaptHealthBar(Transform target, string tag)
    {
        // Check if it is enemy
        if (tag.Equals(EnemyClass.EnemyTag))
        {
            // Set creature parameters
            EnemyClass enemyClass = target.GetComponent<EnemyClass>();
            // Set health bar
            HealthBarFillImg.fillAmount = enemyClass.CurHealth / (float)enemyClass.MaxHealth;
        }
        // Check if it is person
        if (tag.Equals(PersonClass.PersonTag))
        {
            // Set person parameters
            PersonClass personClass = target.GetComponent<PersonClass>();
            // Set health bar
            HealthBarFillImg.fillAmount = personClass.CurHealth / (float)personClass.MaxHealth;
        }
    }

    /// <summary>
    /// Updates info about selected skill in the skill window.
    /// </summary>
    /// <param name="skillTxt">A text with proper a description.</param>
    /// <param name="skill">A structure that represents a selected skill.</param>
    public void AdaptSkillInfo(Text skillTxt, HeroSkillDatabase.Skill skill)
    {
        // Change text color
        skillTxt.color = skill.Color;
        // Set text value
        skillTxt.text = skill.Kind + "\n\n" + skill.Type + "\n\n" + skill.Desc + "\n\n" + "Require skill: "
                        + skill.ReqSkill + "\n" + "Require level: " + skill.ReqLvl + "\n" + "Skill level: "
                        + skill.Level + "\n" + "Skill effect: " + (int)skill.Effect + "\n" + "Energy cost: "
                        + (int)skill.EnergyCost;
    }

    /// <summary>
    /// Updates info about skill that is active at time.
    /// </summary>
    /// <param name="mouseSkillTxt">A text with proper a description.</param>
    /// <param name="mouseSkill">A structure that represents an active skill.</param>
    public void AdaptMouseSkillInfo(Text mouseSkillTxt, HeroSkillDatabase.Skill mouseSkill)
    {
        // Check mouse skill type - left click
        if (mouseSkill.Id.Equals(LeftClickId))
        {
            // Set new parent for panel
            MouseSkillImg.transform.SetParent(LeftClickImg.transform);
            // Set new postion for panel
            MouseSkillImg.transform.position = LeftClickImg.transform.position;
        }
        // Check mouse skill type - right click
        if (mouseSkill.Id.Equals(RightClickId))
        {
            // Set new parent for panel
            MouseSkillImg.transform.SetParent(RightClickImg.transform);
            // Set new postion for panel
            MouseSkillImg.transform.position = RightClickImg.transform.position;
        }
        // Change pivot position of mouse skill panel
        MouseSkillImg.rectTransform.pivot = new Vector2(0.5f, 0f);
        // Change text color
        MouseSkillTxt.color = mouseSkill.Color;
        // Check if attack is selected
        if (mouseSkill.Type.Equals(HeroSkillDatabase.Attack))
            MouseSkillTxt.text = mouseSkill.Kind + "\n\n" + mouseSkill.Type + "\n\n"
             + mouseSkill.Desc;
        // Check if other skill is selected
        else
            MouseSkillTxt.text = mouseSkill.Kind + "\n\n" + mouseSkill.Type + "\n\n"
                                + mouseSkill.Desc + "\n\n" + "Require skill: " + mouseSkill.ReqSkill
                                + "\n" + "Require level: " + mouseSkill.ReqLvl + "\n" + "Skill level: "
                                + mouseSkill.Level + "\n" + "Skill effect: " + (int)mouseSkill.Effect
                                + "\n" + "Energy cost: " + (int)mouseSkill.EnergyCost;
    }

    /// <summary>
    /// Updates hint that appears in UI interface.
    /// </summary>
    /// <param name="objName">A name of object that mouse is hovering.</param>
    public void AdaptHintInfo(string objName)
    {
        // It is attribute button
        if (objName.Equals(AttrButton))
            // Update text
            HintTxt.text = CharacterText;
        // It is skill button
        if (objName.Equals(SkillButton))
            // Update text
            HintTxt.text = SkillsText;
        // It is experience bar
        if (objName.Equals(ExpBarFill))
            // Set text above experience bar
            HintTxt.text = "Experience\n" + _heroClass.TotalExp + "/" + _heroClass.NextLvLExp;
        // Change parent
        HintPanelImg.transform.SetParent(BottomPanelImg.transform);
        // Character exit button
        if (objName.Equals(CharExitButton))
        {
            // Update text
            HintTxt.text = ExitText;
            // Change parent
            HintPanelImg.transform.SetParent(CharExitBtn.transform);
        }
        // Skill exit button
        if (objName.Equals(SkillExitButton))
        {
            // Update text
            HintTxt.text = ExitText;
            // Change parent
            HintPanelImg.transform.SetParent(SkillExitBtn.transform);
        }
        // Inventory exit button
        if (objName.Equals(InvExitButton))
        {
            // Update text
            HintTxt.text = ExitText;
            // Change parent
            HintPanelImg.transform.SetParent(InvExitBtn.transform);
        }
        // Dodge chance label
        if (objName.Equals(DodgeChanceText))
        {
            // Update text
            HintTxt.text = DuckChance + _heroClass.LastEnemyType;
            // Change parent
            HintPanelImg.transform.SetParent(DodgeChanceTxt.transform);
        }
        // Attack chance label
        if (objName.Equals(AttackChanceText))
        {
            // Update text
            HintTxt.text = HitChance + _heroClass.LastEnemyType;
            // Change parent
            HintPanelImg.transform.SetParent(AttackChanceTxt.transform);
        }
        // Change panel size
        HintPanelImg.rectTransform.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Vertical, HintTxt.preferredHeight + PanelMargin);
        HintPanelImg.rectTransform.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Horizontal, HintTxt.preferredWidth + PanelMargin);
    }

    /// <summary>
    /// Updates info about selected item in the inventory slot.
    /// </summary>
    /// <param name="itemClass">An object that represents a specific item.</param>
    public void AdaptSlotInfo(ItemClass itemClass)
    {
        // Create new label
        SlotTxt.text = itemClass.Kind + "\n\n" + itemClass.Rank + "\n\n" + itemClass.Type + "\n\n"
            + itemClass.Desc + "\n\n";
        // Change text color
        SlotTxt.color = itemClass.HintColor;
        // Update item info
        for (int cnt = 0; cnt < itemClass.Stats.Length; cnt++)
        {
            // Check if item increases attack rate
            if (itemClass.Stats[cnt].Equals(HeroClass.AttackRateId))
                SlotTxt.text += itemClass.Bonus[cnt] + (itemClass.Effect[cnt] * AttackRateMod1000) + "\n";
            // Check if item increase current health or energy
            else if (itemClass.Stats[cnt].Equals(HeroClass.CurHealthId)
                || itemClass.Stats[cnt].Equals(HeroClass.CurEnergyId))
                SlotTxt.text += itemClass.Bonus[cnt] + itemClass.Effect[cnt] + "%\n";
            else
                SlotTxt.text += itemClass.Bonus[cnt] + itemClass.Effect[cnt] + "\n";
        }
        // Check if item is in trade slot
        if (itemClass.transform.parent.name.Contains(HeroInventory.TradeSlotId) && _heroClass.IsTalking)
            // Set item value
            SlotTxt.text += "\n" + ItemClass.ItemVal + itemClass.Value;
        else if (_heroClass.IsTalking)
            // Set item value
            SlotTxt.text += "\n" + ItemClass.ItemVal + (itemClass.Value / ItemClass.TradeMod);
        else
            // Set item value
            SlotTxt.text += "\n" + ItemClass.ItemVal + itemClass.Value;
    }

    /// <summary>
    /// Updates info about events in the trade window.
    /// </summary>
    /// <param name="eventText">A text that should be display.</param>
    public void AdaptTradeInfo(string eventText)
    {
        // Set proper info
        TradeInfoTxt.text = eventText;
    }

    /// <summary>
    /// Checks current selected skills and activate them.
    /// </summary>
    public void CheckMouseSkill()
    {
        // Search skills
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
        {
            // Check left button skill
            if (LeftSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
                // Activate skill on left mouse click
                _heroParameter.ActivateSkill(_heroSkill.HeroSkills[cnt], LeftSkill);
            // Check right button skill
            if (RightSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
                // Activate skill on right mouse click
                _heroParameter.ActivateSkill(_heroSkill.HeroSkills[cnt], RightSkill);
        }
    }

    /// <summary>
    /// Updates graphics that represent current active skills.
    /// </summary>
    public void AdaptMouseSkill()
    {
        // Attack - left button
        if (LeftSkill.Type.Equals(HeroSkillDatabase.Attack))
            // Set new sprite
            LeftClickImg.sprite = HeroSkillDatabase.NormalAttack.Sprite;
        // Attack - right button
        if (RightSkill.Type.Equals(HeroSkillDatabase.Attack))
            // Set new sprite
            RightClickImg.sprite = HeroSkillDatabase.NormalAttack.Sprite;
        // Skills
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
        {
            // Check left button skill
            if (LeftSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
                // Set new sprite
                LeftClickImg.sprite = _heroSkill.HeroSkills[cnt].Sprite;
            // Check right button skill
            if (RightSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
                // Set new sprite
                RightClickImg.sprite = _heroSkill.HeroSkills[cnt].Sprite;
        }
    }

    /// <summary>
    /// Sets proper camera view depending on active UI windows.
    /// </summary>
    public void SetIsoCameraView()
    {
        // Check if hero is talking
        if (_heroClass.IsTalking)
            // Break action
            return;
        // Split screen and set view on left side
        if (_isLeftWindow && !_isRightWindow)
        {
            // Set new camera rectangle
            Camera.main.rect = new Rect(0.5f, 0f, 1f, 1f);
            HealthBarFillImg.transform.localPosition =
                new Vector2(CameraSplit, HealthBarFillImg.transform.localPosition.y);
            HealthBarVoidImg.transform.localPosition =
                new Vector2(CameraSplit, HealthBarVoidImg.transform.localPosition.y);
            CreatureTypeTxt.transform.localPosition =
                new Vector2(CameraSplit, CreatureTypeTxt.transform.localPosition.y);
            CreatureNatureTxt.transform.localPosition =
                new Vector2(CameraSplit, CreatureNatureTxt.transform.localPosition.y);
        }
        // Split screen and set view on right side
        if (_isRightWindow && !_isLeftWindow)
        {
            // Set new camera rectangle
            Camera.main.rect = new Rect(0f, 0f, 0.5f, 1f);
            HealthBarFillImg.transform.localPosition =
                new Vector2(-CameraSplit, HealthBarFillImg.transform.localPosition.y);
            HealthBarVoidImg.transform.localPosition =
                new Vector2(-CameraSplit, HealthBarVoidImg.transform.localPosition.y);
            CreatureTypeTxt.transform.localPosition =
                new Vector2(-CameraSplit, CreatureTypeTxt.transform.localPosition.y);
            CreatureNatureTxt.transform.localPosition =
                new Vector2(-CameraSplit, CreatureNatureTxt.transform.localPosition.y);
        }
        // Set normal view
        if (!_isLeftWindow && !_isRightWindow)
        {
            // Set new camera rectangle
            Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
            // Set position of health bar (default position)
            HealthBarFillImg.transform.localPosition =
                new Vector2(CameraNormal, HealthBarFillImg.transform.localPosition.y);
            HealthBarVoidImg.transform.localPosition =
                new Vector2(CameraNormal, HealthBarVoidImg.transform.localPosition.y);
            CreatureTypeTxt.transform.localPosition =
                new Vector2(CameraNormal, CreatureTypeTxt.transform.localPosition.y);
            CreatureNatureTxt.transform.localPosition =
                new Vector2(CameraNormal, CreatureNatureTxt.transform.localPosition.y);
        }
    }

    /// <summary>
    /// Prepares graphical elements before beginning the conversation.
    /// </summary>
    public void PrepareUIToTalk()
    {
        // Hide health bar for person
        HideHealthBar();
        // Generate some gold
        _heroBehavior.PersonClass.gameObject.GetComponent<PersonBehavior>().GeneratePersonGold();
        // Set proper person name
        PersonNameTxt.text = _heroBehavior.PersonClass.Type + ":";
        // Move slot panel to dialogue window
        SlotPanelImg.transform.SetParent(DialogueWindowImg.transform);
        // Generate dialogue options
        CreateConversationPanel(_heroBehavior.PersonClass);
        // Check if hero visits person first time
        if (!_heroBehavior.PersonClass.IsVisited)
        {
            // Set first meet text
            PersonSpeechTxt.text = _heroBehavior.PersonClass.FirstMeetText;
            // Search conversation panels
            for (int cnt = 0; cnt < HeroTextCount; cnt++)
                // Hide conversation panel
                DialogueWindowImg.transform.GetChild(cnt).GetComponent<Transform>()
                .gameObject.SetActive(false);
            // Show press button text
            ButtonPressTxt.gameObject.SetActive(true);
        }
        else
            // Set proper person enter text
            PersonSpeechTxt.text = _heroBehavior.PersonClass.EnterText;
        // Hide active items
        SetActiveItems(true);
        // Hide panels
        DeactivateElement(MouseSkillImg.transform);
        DeactivateElement(HintPanelImg.transform);
        // Close open tabs
        HideCharWindow();
        HideInventoryWindow();
        HideSkillWindow();
        DeactivateElement(BottomPanelImg.transform);
        // Show dialogue window
        ActivateElement(DialogueWindowImg.transform);
        // Generate some random items in trade window (if it is possible)
        _heroInventory.GenerateTradeItems();
        // Hide location name
        LocationNameTxt.gameObject.SetActive(false);
        // Hide main info
        HideMainInfo();
    }

    /// <summary>
    /// Prepares graphical elements after ending the trading.
    /// </summary>
    public void PrepareUIAfterTrade()
    {
        // Hide trade window
        DeactivateElement(TradeWindowImg.transform);
        // Hide inventory window
        HideInventoryWindow();
        // Hide bottom panel
        DeactivateElement(BottomPanelImg.transform);
        // Show dialogue window
        ActivateElement(DialogueWindowImg.transform);
        // Search conversation panels
        for (int cnt = 0; cnt < HeroTextCount; cnt++)
            // Show conversation panel
            DialogueWindowImg.transform.GetChild(cnt).GetComponent<Transform>()
            .gameObject.SetActive(true);
        // Hide press button text
        ButtonPressTxt.gameObject.SetActive(false);
        // Adapt person text
        PersonSpeechTxt.text = _heroBehavior.PersonClass.EnterText;
    }

    /// <summary>
    /// Prepares graphical elements after ending the conversation.
    /// </summary>
    public void PrepareUIToAction()
    {
        // Destroy random items in trade window (if it is possible)
        _heroInventory.DestroyTradeItems();
        // Reset current statment index
        CurStatment = PersonDatabase.NoStatment;
        // Search propr conversation panel
        for (int cnt = 0; cnt < HeroTextCount; cnt++)
            // Destroy conversation panels
            Destroy(DialogueWindowImg.transform.GetChild(cnt).gameObject);
        // Show bottom panel
        ActivateElement(BottomPanelImg.transform);
        // Move slot panel to bottom panel
        SlotPanelImg.transform.SetParent(BottomPanelImg.transform);
        // Set slot panel as last sibling
        SlotPanelImg.transform.SetAsLastSibling();
        // Show active items
        SetActiveItems(false);
        // Hide dialogue window
        DeactivateElement(DialogueWindowImg.transform);
        // Show location name
        LocationNameTxt.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the visibility of active items when the hero begins or ends conversation.
    /// </summary>
    /// <param name="visibility">A boolean that specifies whether the item is visible or not.</param>
    public void SetActiveItems(bool visibility)
    {
        // Check if hero wears head item
        if (!_heroInventory.HeadSlot.IsSlotActive)
        {
            // Get item renderer
            MeshRenderer meshRenderer = GameObject
                .Find(_heroInventory.name + ItemClass.HeadItemHolder)
                .transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
            // Item is visible
            if (visibility.Equals(true))
                // Disable item
                meshRenderer.enabled = false;
            // Item is invisible
            else
                // Activate item
                meshRenderer.enabled = true;
        }
        // Check if hero wears right hand item
        if (!_heroInventory.RightHandSlot.IsSlotActive)
        {
            // Get item renderer
            MeshRenderer meshRenderer = GameObject
                .Find(_heroInventory.name + ItemClass.RightItemHolder)
                .transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
            // Item is visible
            if (visibility.Equals(true))
                // Disable item
                meshRenderer.enabled = false;
            // Item is invisible
            else
                // Activate item
                meshRenderer.enabled = true;
        }
        // Check if hero wears left hand item
        if (!_heroInventory.LeftHandSlot.IsSlotActive)
        {
            // Get item renderer
            MeshRenderer meshRenderer = GameObject
                .Find(_heroInventory.name + ItemClass.LeftItemHolder)
                .transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
            // Item is visible
            if (visibility.Equals(true))
                // Disable item
                meshRenderer.enabled = false;
            // Item is invisible
            else
                // Activate item
                meshRenderer.enabled = true;
        }
    }

    /// <summary>
    /// Creates proper conversation panel before start the conversation.
    /// </summary>
    /// <param name="personClass">An object specifies the type of the person.</param>
    public void CreateConversationPanel(PersonClass personClass)
    {
        // Hide press button text
        ButtonPressTxt.gameObject.SetActive(false);
        // Set new space between panels
        float ySpace = ConvPanelSpace;
        // Generate proper panels
        for (int cnt = 0; cnt < personClass.HeroTexts.Length; cnt++)
        {
            // Create new 3D object
            GameObject convPanel =
                Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.ConvPanelId),
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity) as GameObject;
            // Change panel name
            convPanel.name = convPanel.name.Replace(ItemClass.Clone, ItemClass.EmptySpace) + cnt;
            // Get conversation panel text
            Text convText = convPanel.GetComponentInChildren<Text>();
            // Set proper conversation text
            convText.text = personClass.HeroTexts[cnt];
            // Set new parent
            convPanel.transform.SetParent(DialogueWindowImg.transform, false);
            // Set as first sibling
            convPanel.transform.SetAsFirstSibling();
            // Set new position
            convPanel.transform.localPosition = new Vector3(Vector3.zero.x,
                -DialogueWindowImg.rectTransform.localPosition.y - ySpace, Vector3.zero.z);
            // Update space between panels
            ySpace += ConvPanelSpace;
        }
        // Set option count
        HeroTextCount = personClass.HeroTexts.Length;
    }

    /// <summary>
    /// Creates proper conversation panel before start the conversation.
    /// </summary>
    /// <param name="objName">A name of the UI element.</param>
    /// <param name="panel">A transform that represents a panel.</param>
    public void WhiteConvPanel(string objName, Transform panel)
    {
        // Check if hero is talking
        if (!_heroClass.IsTalking)
            // Break action
            return;
        // Conversation panels
        for (int cnt = 0; cnt < HeroTextCount; cnt++)
            // Check conversation panel ID
            if (objName.Equals(ItemClass.ConvPanelId + cnt))
                // Change text color
                panel.GetComponentInChildren<Text>().color = White;
    }

    /// <summary>
    /// Sets white text for the selected trade hint.
    /// </summary>
    /// <param name="objName">A name of the UI element.</param>
    /// <param name="panel">A transform that represents a panel.</param>
    public void WhiteTradeHint(string objName, Transform panel)
    {
        // Check if hero is talking
        if (!_heroClass.IsTalking)
            // Break action
            return;
        // Trade hint
        if (objName.Equals(TradeHintButton))
            // Change text color
            panel.GetComponentInChildren<Text>().color = White;
    }

    /// <summary>
    /// Sets gold text for the selected conversation panel.
    /// </summary>
    /// <param name="objName">A name of the UI element.</param>
    /// <param name="panel">A transform that represents a panel.</param>
    public void GoldConvPanel(string objName, Transform panel)
    {
        // Check if hero is talking
        if (!_heroClass.IsTalking)
            // Break action
            return;
        // Conversation panels
        for (int cnt = 0; cnt < HeroTextCount; cnt++)
            // Check conversation panel ID
            if (objName.Equals(ItemClass.ConvPanelId + cnt))
                // Change text color
                panel.GetComponentInChildren<Text>().color = Gold;
    }

    /// <summary>
    /// Sets gold text for the selected trade hint.
    /// </summary>
    /// <param name="objName">A name of the UI element.</param>
    /// <param name="panel">A transform that represents a panel.</param>
    public void GoldTradeHint(string objName, Transform panel)
    {
        // Check if hero is talking
        if (!_heroClass.IsTalking)
            // Break action
            return;
        // Trade hint
        if (objName.Equals(TradeHintButton))
            // Change text color
            panel.GetComponentInChildren<Text>().color = Gold;
    }

    /// <summary>
    /// Sets proper dialogue text after clicking selected conversation option.
    /// </summary>
    /// <param name="index">An ID that identifies proper text to display.</param>
    public void SetPersonSpeech(int index)
    {
        // Set current statment index
        CurStatment = index;
        // Set proper speech
        PersonSpeechTxt.text = _heroBehavior.PersonClass.PersonTexts[index];
        // Check statment type
        if (_heroBehavior.PersonClass.StatmentTypes[index]
            .Equals(PersonDatabase.InfoStatment))
            // Break action
            return;
        // Search conversation panels
        for (int cnt = 0; cnt < HeroTextCount; cnt++)
        {
            // Change text color
            DialogueWindowImg.transform.GetChild(cnt).GetComponentInChildren<Text>()
                .color = White;
            // Hide conversation panel
            DialogueWindowImg.transform.GetChild(cnt).GetComponent<Transform>()
                .gameObject.SetActive(false);
        }
        // Show press button text
        ButtonPressTxt.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets current location name at the left-up corner of the screen.
    /// </summary>
    /// <param name="locationName">A name of current location.</param>
    public void SetLocationName(string locationName)
    {
        // Set location name
        LocationNameTxt.text = locationName;
    }

    /// <summary>
    /// Shows the main info that occurs e.g. after changing location.
    /// </summary>
    public void ShowMainInfo()
    {
        // Set visibility
        MainInfoTxt.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows crosses next to the selected label in the menu.
    /// </summary>
    /// <param name="panel">A transform that represents a panel.</param>
    public void ShowMenuCrosses(Transform panel)
    {
        // Show left cross
        panel.GetChild(0).gameObject.SetActive(true);
        // Show right cross
        panel.GetChild(1).gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the main info that occurs e.g. after changing location.
    /// </summary>
    public void HideMainInfo()
    {
        // Set visibility
        MainInfoTxt.gameObject.SetActive(false);
    }

    /// <summary>
    /// Hides the main info that occurs e.g. after changing location with some delay.
    /// </summary>
    /// <param name="time">A delay time in seconds.</param>
    public void DelayedHideMainInfo(float time)
    {
        // Invoke method with delay
        Invoke("HideMainInfo", time);
    }

    /// <summary>
    /// Hides crosses next to the selected label in the menu.
    /// </summary>
    /// <param name="panel">A transform that represents a panel.</param>
    public void HideMenuCrosses(Transform panel)
    {
        // Hide left cross
        panel.GetChild(0).gameObject.SetActive(false);
        // Hide right cross
        panel.GetChild(1).gameObject.SetActive(false);
    }

    /// <summary>
    /// Changes current quality settings.
    /// </summary>
    public void AdaptGraphicsDetail()
    {
        // Get quality levels
        string[] qualityLevels = QualitySettings.names;
        // Decrease quality
        if (QualityLevel.Equals(qualityLevels.Length - 1))
        {
            // Reset quality
            QualityLevel = 0;
            _postProcessVolume.profile = PostProcessings[QualityLevel];
        }
        // Increase quality
        else
        {
            // Set new quality level
            QualityLevel++;
            _postProcessVolume.profile = PostProcessings[QualityLevel];
        }
        // Change label
        QualityPanelImg.GetComponentInChildren<Text>().text = qualityLevels[QualityLevel];
        // Adjust graphics
        QualitySettings.SetQualityLevel(QualityLevel);
    }

    /// <summary>
    /// Changes sounds volume in the game.
    /// </summary>
    public void AdaptSoundsVolume()
    {
        // Seach audio sources
        foreach (AudioSource audioSource in Sounds)
        {
            // Check if it is music audio source
            if (audioSource.name.Equals(GameController))
                // Continue
                continue;
            // Change sound volume
            audioSource.volume = SoundSliderSld.value;
        }
        // Prepare volume label
        int soundsValue = (int)Mathf.Round(SoundSliderSld.value * 100f);
        // Set proper label
        CurSoundsTxt.text = soundsValue + "%";
    }

    /// <summary>
    /// Changes music volume in the game
    /// </summary>
    public void AdaptMusicVolume()
    {
        // Change music volume
        Music.volume = MusicSliderSld.value;
        // Prepare volume label
        int musicValue = (int)Mathf.Round(MusicSliderSld.value * 100f);
        // Set proper label
        CurMusicTxt.text = musicValue + "%";
    }

    /// <summary>
    /// Saves game progress before exit the application.
    /// </summary>
    public void SaveGameProgress()
    {
        // Deactivate hero skills
        _heroParameter.ActivateAttack(ref LeftSkill);
        _heroParameter.ActivateAttack(ref RightSkill);
        _heroBehavior.DisableReleasableSkill();
        // Initialize game save variable
        SettingsDatabase.GameSave = new SettingsDatabase.Save();
        // Copy all needed data
        SettingsDatabase.CopyClassToSave(_heroClass);
        SettingsDatabase.CopyCharactersToSave();
        SettingsDatabase.CopySkillsToSave(_heroSkill.HeroSkills);
        SettingsDatabase.CopyEquipmentToSave(_heroInventory);
        SettingsDatabase.CopyOtherParameters(_heroInventory, _heroParameter, this);
        SettingsDatabase.CopyInventoryToSave(_heroInventory.HeroInv);
        SettingsDatabase.CopyPotionsToSave(_heroInventory.HeroPotions);
        // Try save game progress to file
        SettingsDatabase.TrySaveGameToFile(Application.persistentDataPath, _heroClass.Name);
    }

    /// <summary>
    /// Quits the game to the main menu.
    /// </summary>
    public void QuitGame()
    {
        // Resume action
        Time.timeScale = Time.fixedDeltaTime = 1f;
        // Load main menu scene
        SceneManager.LoadScene(MenuScene, LoadSceneMode.Single);
    }

    /// <summary>
    /// Prepares the game state before running the scene.
    /// </summary>
    public void PrepareGame()
    {
        // New game
        if (SettingsDatabase.HeroName != null)
        {
            // Set proper hero name
            _heroClass.Name = SettingsDatabase.HeroName;
            // Reset hero name variable
            SettingsDatabase.HeroName = null;
            // Reset game save variable
            SettingsDatabase.GameSave = new SettingsDatabase.Save();
            // Break action
            return;
        }
        // Loaded game
        if (SettingsDatabase.GameSave.Name != null)
        {
            // Read hero class from file
            SettingsDatabase.ReadClassFromSave(ref _heroClass);
            // Read people classes from file
            SettingsDatabase.ReadCharactersFromSave();
            // Read hero skills from file
            SettingsDatabase.ReadSkillsFromSave(ref _heroSkill);
            // Read hero inventory from file
            SettingsDatabase.ReadInventoryFromSave(ref _heroInventory);
            // Read hero potions from file
            SettingsDatabase.ReadPotionsFromSave(ref _heroInventory);
            // Read hero equipment fro file
            SettingsDatabase.ReadEquipmentFromSave(ref _heroInventory);
            // Read other parameters
            SettingsDatabase.ReadOtherParameters(ref _heroInventory, ref _heroParameter);
            // Adapt sliders
            SoundSliderSld.value = SettingsDatabase.GameSave.SoundVolume;
            MusicSliderSld.value = SettingsDatabase.GameSave.MusicVolume;
            // Adapt graphics detail
            QualityLevel = SettingsDatabase.GameSave.QualityLevel;
            // Get quality levels
            string[] qualityLevels = QualitySettings.names;
            // Change label
            QualityPanelImg.GetComponentInChildren<Text>().text = qualityLevels[QualityLevel];
            // Adjust graphics
            QualitySettings.SetQualityLevel(QualityLevel);
            _postProcessVolume.profile = PostProcessings[QualityLevel];
            // Reset hero name variable
            SettingsDatabase.HeroName = null;
            // Reset game save variable
            SettingsDatabase.GameSave = new SettingsDatabase.Save();
        }
    }
}