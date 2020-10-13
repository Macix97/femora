using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMouseAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Bar label
    public static readonly string Bar = "Bar";
    // Chance label
    public static readonly string Chance = "Chance";
    // Click label
    public static readonly string Click = "Click";
    // Orb label
    public static readonly string Orb = "Orb";
    // Hero class
    private HeroClass _heroClass;
    // Hero parameters
    private HeroParameter _heroParameter;
    // Hero skills
    private HeroSkill _heroSkill;
    // Hero sound
    private HeroSound _heroSound;
    // Game interface
    private GameInterface _gameInterface;

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
        _heroSkill = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroSkill>();
        _heroSound = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroSound>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
    }

    // Check if mouse is over graphical interface elements
    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Set action when mouse is over graphical interface elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
        {
            // Change text color
            _gameInterface.GoldTradeHint(name, transform);
            // Break action
            return;
        }
        // Attribute button
        if (name.Equals(GameInterface.AttrButton))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.AttrButton);
            _gameInterface.ShowHintPanel(_gameInterface.AttrBtn.transform);
        }
        // Skill button
        if (name.Equals(GameInterface.SkillButton))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.SkillButton);
            _gameInterface.ShowHintPanel(_gameInterface.SkillButtonBtn.transform);
        }
        // Character exit button
        if (name.Equals(GameInterface.CharExitButton))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.CharExitButton);
            _gameInterface.ShowHintPanel(_gameInterface.CharExitBtn.transform);
        }
        // Skill exit button
        if (name.Equals(GameInterface.SkillExitButton))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.SkillExitButton);
            _gameInterface.ShowHintPanel(_gameInterface.SkillExitBtn.transform);
        }
        // Inventory exit button
        if (name.Equals(GameInterface.InvExitButton))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.InvExitButton);
            _gameInterface.ShowHintPanel(_gameInterface.InvExitBtn.transform);
        }
        // Experience bar
        if (name.Equals(GameInterface.ExpBarFill))
        {
            _gameInterface.IsExpHint = true;
            _gameInterface.AdaptHintInfo(GameInterface.ExpBarFill);
            _gameInterface.ShowHintPanel(_gameInterface.ExpBarImg.transform);
        }
        // Dodge chance label
        if (name.Equals(GameInterface.DodgeChanceText))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.DodgeChanceText);
            _gameInterface.ShowHintPanel(_gameInterface.DodgeChanceTxt.transform);
        }
        // Attack chance label
        if (name.Equals(GameInterface.AttackChanceText))
        {
            _gameInterface.IsExpHint = false;
            _gameInterface.AdaptHintInfo(GameInterface.AttackChanceText);
            _gameInterface.ShowHintPanel(_gameInterface.AttackChanceTxt.transform);
        }
        // Health orb
        if (name.Equals(GameInterface.HealthOrbVoid))
            _gameInterface.ActivateElement(_gameInterface.HealthTxt.transform);
        // Energy orb
        if (name.Equals(GameInterface.EnergyOrbVoid))
            _gameInterface.ActivateElement(_gameInterface.EnergyTxt.transform);
        // Mouse skill image - left click
        if (name.Equals(GameInterface.LeftClickImage))
        {
            // Update mouse skill panel
            _gameInterface.AdaptMouseSkillInfo(_gameInterface.MouseSkillTxt, _gameInterface.LeftSkill);
            // Show mouse skill panel
            _gameInterface.ActivateElement(_gameInterface.MouseSkillImg.transform);
        }
        // Mouse skill image - right click
        if (name.Equals(GameInterface.RightClickImage))
        {
            // Update mouse skill panel
            _gameInterface.AdaptMouseSkillInfo(_gameInterface.MouseSkillTxt, _gameInterface.RightSkill);
            // Show mouse skill panel
            _gameInterface.ActivateElement(_gameInterface.MouseSkillImg.transform);
        }
        // Skill buttons
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
            // Check button ID
            if (name.Equals(GameInterface.SkillButtonId + cnt))
            {
                // Set pivot with right offset
                if ((cnt % 2).Equals(0))
                    // Change pivot position of skill panel
                    _gameInterface.SkillPanelImg.rectTransform.pivot = new Vector2(0.35f, 0.5f);
                // Set pivot with left offset
                else
                    // Change pivot position of skill panel
                    _gameInterface.SkillPanelImg.rectTransform.pivot = new Vector2(0.65f, 0.5f);
                // Update skill panel
                _gameInterface.AdaptSkillInfo(_gameInterface.SkillTxt, _heroSkill.HeroSkills[cnt]);
                // Show skill panel
                ShowSkillPanel(_gameInterface.SkillPanelImg, _gameInterface.SkillBtns[cnt].image);
                // Break action
                break;
            }
        // Change text color
        _gameInterface.GoldConvPanel(name, transform);
        // Main menu labels
        if (name.Contains(ItemClass.PanelId) && _gameInterface.IsGamePaused)
            // Show crosses
            _gameInterface.ShowMenuCrosses(transform);
    }

    // Set action when mouse is outside graphical interface elements
    public void OnPointerExit(PointerEventData eventData)
    {
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
        {
            // Change text color
            _gameInterface.WhiteTradeHint(name, transform);
            // Break action
            return;
        }
        _gameInterface.DeactivateElement(_gameInterface.HealthTxt.transform);
        _gameInterface.DeactivateElement(_gameInterface.EnergyTxt.transform);
        _gameInterface.DeactivateElement(_gameInterface.HintPanelImg.transform);
        _gameInterface.DeactivateElement(_gameInterface.MouseSkillImg.transform);
        _gameInterface.DeactivateElement(_gameInterface.SkillPanelImg.transform);
        _gameInterface.WhiteConvPanel(name, transform);
        // Main menu labels
        if (name.Contains(ItemClass.PanelId) && _gameInterface.IsGamePaused)
            _gameInterface.HideMenuCrosses(transform);
    }

    // Set action when mouse is clicked graphical interface elements
    public void OnPointerClick(PointerEventData eventData)
    {
        // Exit panel in main menu
        if (name.Equals(GameInterface.ExitPanel))
        {
            // Save game progress
            _gameInterface.SaveGameProgress();
            // Quit game
            _gameInterface.QuitGame();
        }
        // Video panel in main menu
        if (name.Equals(GameInterface.VideoPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Video;
            // Show video menu
            _gameInterface.ActivateElement(_gameInterface.VideoMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
            // Hide elements
            _gameInterface.DeactivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ReturnMenuPanelImg.transform);
        }
        // Audio panel in main menu
        if (name.Equals(GameInterface.AudioPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Audio;
            // Show audio menu
            _gameInterface.ActivateElement(_gameInterface.AudioMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
            // Hide elements
            _gameInterface.DeactivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ReturnMenuPanelImg.transform);
        }
        // Control panel in main menu
        if (name.Equals(GameInterface.ControlPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Control;
            // Show control menu
            _gameInterface.ActivateElement(_gameInterface.ControlMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
            // Hide elements
            _gameInterface.DeactivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.DeactivateElement(_gameInterface.ReturnMenuPanelImg.transform);
        }
        // Quality panel in video menu
        if (name.Equals(GameInterface.QualityPanel))
        {
            // Adjust graphics
            _gameInterface.AdaptGraphicsDetail(_gameInterface.QualityLevel);
        }
        // Return panel in main menu
        if (name.Equals(GameInterface.ReturnMenuPanel))
        {
            // Resume game
            _gameInterface.IsGamePaused = false;
            // Resume action
            Time.timeScale = Time.fixedDeltaTime = 1f;
            // Show location name
            _gameInterface.ActivateElement(_gameInterface.LocationNameTxt.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
            // Hide menu text
            _gameInterface.HideMainInfo();
            // Hide main menu
            _gameInterface.DeactivateElement(_gameInterface.MainMenuImg.transform);
        }
        // Return panel in video menu
        if (name.Equals(GameInterface.ReturnVideoPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Femora;
            // Show elements
            _gameInterface.ActivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ReturnMenuPanelImg.transform);
            // Hide video menu
            _gameInterface.DeactivateElement(_gameInterface.VideoMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
        }
        // Return panel in audio menu
        if (name.Equals(GameInterface.ReturnAudioPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Femora;
            // Show elements
            _gameInterface.ActivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ReturnMenuPanelImg.transform);
            // Hide audio menu
            _gameInterface.DeactivateElement(_gameInterface.AudioMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
        }
        // Return panel in control menu
        if (name.Equals(GameInterface.ReturnControlPanel))
        {
            // Adapt main text
            _gameInterface.MainInfoTxt.text = GameInterface.Femora;
            // Show elements
            _gameInterface.ActivateElement(_gameInterface.VideoPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.AudioPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ControlPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ExitPanelImg.transform);
            _gameInterface.ActivateElement(_gameInterface.ReturnMenuPanelImg.transform);
            // Hide control menu
            _gameInterface.DeactivateElement(_gameInterface.ControlMenuImg.transform);
            // Hide crosses
            _gameInterface.HideMenuCrosses(transform);
        }
        // Check if trade hint is active
        if (_gameInterface.IsTradeHint)
        {
            // Trade hint button
            if (name.Equals(GameInterface.TradeHintButton))
            {
                // Change text color
                _gameInterface.WhiteTradeHint(name, transform);
                // Hide trade hint
                _gameInterface.DeactivateElement(_gameInterface.TradeHintImg.transform);
                // Enable buttons action
                _gameInterface.IsTradeHint = false;
                // Enable inventory exit button
                _gameInterface.InvExitBtn.interactable = true;
                // Play click sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.Click, SoundDatabase.ItemSounds));
            }
            // Break action
            return;
        }
        // Character window exit button
        if (name.Equals(GameInterface.CharExitButton))
            _gameInterface.HideCharWindow();
        // Skill window exit button
        if (name.Equals(GameInterface.SkillExitButton))
            _gameInterface.HideSkillWindow();
        // Inventory window exit button
        if (name.Equals(GameInterface.InvExitButton))
        {
            // Check if hero is talking
            if (_heroClass.IsTalking)
                // Prepare UI after trade
                _gameInterface.PrepareUIAfterTrade();
            else
                // Hide inventory window
                _gameInterface.HideInventoryWindow();
        }
        // Attribute buttons
        if (_gameInterface.IsAttrButtonClickable())
        {
            // Character window
            if (name.Equals(GameInterface.AttrButton))
                _gameInterface.ShowCharWindow();
            // Vitality
            if (name.Equals(GameInterface.VitalityButton))
                _heroParameter.AdaptAttr(HeroClass.VitalityId, _heroParameter.StudyId,
                    _heroParameter.LearnValue);
            // Wisdom
            if (name.Equals(GameInterface.WisdomButton))
                _heroParameter.AdaptAttr(HeroClass.WisdomId, _heroParameter.StudyId,
                    _heroParameter.LearnValue);
            // Strength
            if (name.Equals(GameInterface.StrengthButton))
                _heroParameter.AdaptAttr(HeroClass.StrengthId, _heroParameter.StudyId,
                    _heroParameter.LearnValue);
            // Agility
            if (name.Equals(GameInterface.AgilityButton))
                _heroParameter.AdaptAttr(HeroClass.AgilityId, _heroParameter.StudyId,
                    _heroParameter.LearnValue);
        }
        // Skill buttons
        if (_gameInterface.IsSkillButtonClickable())
        {
            // Skill window
            if (name.Equals(GameInterface.SkillButton))
            {
                // Check if inventory window is active
                if (_gameInterface.InventoryWindowImg.IsActive())
                    // Hide inventory window
                    _gameInterface.DeactivateElement(_gameInterface.InventoryWindowImg.transform);
                // Show skill window
                _gameInterface.ShowSkillWindow();
            } 
            // Skills
            for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
            {
                // Check if button click is possible
                if (!name.Equals(GameInterface.SkillButtonId + cnt)
                    || !_gameInterface.SkillBtns[cnt].interactable)
                    // Check other action
                    continue;
                // Check hero class
                if (_heroClass.Type.Equals(HeroDatabase.Paladin))
                {
                    // Disable old skill bonus
                    DisableOldSkill(_heroSkill.HeroSkills[cnt]);
                    // Adapt skill
                    _heroParameter.AdaptSkill(ref _heroSkill.HeroSkills[cnt], _heroParameter.LearnValue,
                        HeroSkillDatabase.PaladinSkills[cnt].Effect,
                        HeroSkillDatabase.PaladinSkills[cnt].EnergyCost);
                    // Enable new skill bonus
                    EnableNewSkill(_heroSkill.HeroSkills[cnt]);
                }
            }
        }
        // Conversation panels
        for (int cnt = 0; cnt < _gameInterface.HeroTextCount; cnt++)
            // Check conversation panel ID
            if (name.Equals(ItemClass.ConvPanelId + cnt))
            {
                // Set proper person speech
                _gameInterface.SetPersonSpeech(cnt);
                // Break action
                break;
            }
        // Try get button component
        Button button = GetComponent<Button>();
        // Check if button exist
        if (button == null)
        {
            // Check specific labels
            if (name.Contains(Orb) || name.Contains(Click) || name.Contains(Bar) || name.Contains(Chance))
                // Break action
                return;
            // Play click sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Click, SoundDatabase.ItemSounds));
        }
        // Check if button is interactable
        else if (button.interactable.Equals(true))
            // Play click sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Click, SoundDatabase.ItemSounds));
    }

    // Disable old skill bonus when it is updated
    private void DisableOldSkill(HeroSkillDatabase.Skill skill)
    {
        // Left click
        if (_gameInterface.LeftSkill.Kind.Equals(skill.Kind))
            // Deactivate old statistics - left click
            _heroParameter.DeactivateSkill(skill, _gameInterface.LeftSkill);
        // Right click
        if (_gameInterface.RightSkill.Kind.Equals(skill.Kind))
            // Deactivate old statistics - right click
            _heroParameter.DeactivateSkill(skill, _gameInterface.RightSkill);
    }

    // Enable new skill bonus when it is updated
    private void EnableNewSkill(HeroSkillDatabase.Skill skill)
    {
        // Left click
        if (_gameInterface.LeftSkill.Kind.Equals(skill.Kind))
            // Activate new statistics - left click
            _heroParameter.ActivateSkill(skill, _gameInterface.LeftSkill);
        // Right click
        if (_gameInterface.RightSkill.Kind.Equals(skill.Kind))
            // Activate new statistics - right click
            _heroParameter.ActivateSkill(skill, _gameInterface.RightSkill);
    }

    // Update and show skill panel
    private void ShowSkillPanel(Image panel, Image image)
    {
        // Set new parent of activity panel
        panel.transform.SetParent(image.transform);
        // Set new position of activity panel
        panel.transform.position = image.transform.position;
        // Set new hierarchy in layout
        panel.transform.parent.SetAsLastSibling();
        // Show panel
        _gameInterface.ActivateElement(panel.transform);
    }
}