using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuMouseAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Menu interface
    private MenuInterface _menuInterface;
    // Menu music manager
    private MenuMusicManager _menuMusicManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        ShowClassInfo();
    }

    // Set basic parameters
    private void Init()
    {
        _menuInterface = GameObject.Find(MenuInterface.MenuInterfaceController).GetComponent<MenuInterface>();
        _menuMusicManager = GameObject.Find(MenuInterface.MenuController).GetComponent<MenuMusicManager>();
    }

    // Set action when mouse is over graphical interface elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if it is panel
        if (name.Contains(ItemClass.PanelId))
            // Set gold text
            _menuInterface.SetGoldText(transform);
        // New game
        if (name.Equals(MenuInterface.NewGame))
            // Set proper hit
            _menuInterface.MenuHintTxt.text = MenuInterface.NewGameText;
        // Load game
        if (name.Equals(MenuInterface.LoadGame))
            // Set proper hit
            _menuInterface.MenuHintTxt.text = MenuInterface.LoadGameText;
        // Credits
        if (name.Equals(MenuInterface.Credits))
            // Set proper hit
            _menuInterface.MenuHintTxt.text = MenuInterface.CreditsText;
        // Exit Femora
        if (name.Equals(MenuInterface.ExitFemora))
            // Set proper hit
            _menuInterface.MenuHintTxt.text = MenuInterface.ExitFemoraText;
        // New game back
        if (name.Equals(MenuInterface.NewBack))
            // Set proper hit
            _menuInterface.MenuHintTxt.text = MenuInterface.ReturnToMainMenu;
        // Saves
        if (name.Contains(MenuInterface.Save))
        {
            // Check if it is empty space
            if (GetComponentInChildren<Text>().text.Equals(ItemClass.EmptySpace)
                || _menuInterface.ErrorMenuImg.IsActive())
                // Break action
                return;
            // Show crosses
            _menuInterface.ShowMenuCrosses(transform);
        }
        // Paladin class
        if (!(name.Equals(MenuInterface.PaladinClass) && !_menuInterface.CreateMenuImg.IsActive()))
            // Break action
            return;
        // Show crosses
        _menuInterface.ShowMenuCrosses(transform);
        // Show paladin image
        _menuInterface.ActivateElement(_menuInterface.HeroImageImg.transform);
        // Show stats menu
        _menuInterface.ActivateElement(_menuInterface.StatsMenuImg.transform);
        // Set proper hint
        _menuInterface.MenuHintTxt.text = MenuInterface.PaladinDesc;
        // Search proper hero
        for (int cnt = 0; cnt < HeroDatabase.Heroes.Length; cnt++)
            // Check hero class
            if (HeroDatabase.Paladin.Equals(HeroDatabase.Heroes[cnt].Type))
            {
                // Set proper stats
                _menuInterface.StartingLevelTxt.text = HeroDatabase.Heroes[cnt].Level.ToString();
                _menuInterface.StartingVitalityTxt.text = HeroDatabase.Heroes[cnt].Vitality.ToString();
                _menuInterface.StartingWisdomTxt.text = HeroDatabase.Heroes[cnt].Wisdom.ToString();
                _menuInterface.StartingStrengthTxt.text = HeroDatabase.Heroes[cnt].Strength.ToString();
                _menuInterface.StartingAgilityTxt.text = HeroDatabase.Heroes[cnt].Agility.ToString();
                // Break action
                break;
            }
    }

// Set action when mouse is outside graphical interface elements
public void OnPointerExit(PointerEventData eventData)
{
    // Hide paladin image
    _menuInterface.DeactivateElement(_menuInterface.HeroImageImg.transform);
    // Hide stats menu
    _menuInterface.DeactivateElement(_menuInterface.StatsMenuImg.transform);
    // Check if it is panel
    if (name.Contains(ItemClass.PanelId))
        // Set white text
        _menuInterface.SetWhiteText(transform);
    // Check if it is some class or save slot
    if (name.Contains(MenuInterface.ClassNameId) || name.Contains(MenuInterface.Save))
        // Hide crosses
        _menuInterface.HideMenuCrosses(transform);
    // Check if new game window is active
    if (_menuInterface.HeroBackgroundImg.IsActive())
        // Select hint text
        _menuInterface.MenuHintTxt.text = MenuInterface.ChooseYourClass;
    // Check if main menu is active
    else if (_menuInterface.FemoraTxt.IsActive())
        // Select hint text
        _menuInterface.MenuHintTxt.text = MenuInterface.SelectSomeOption;
}

    // Set action when mouse is clicked graphical interface elements
    public void OnPointerClick(PointerEventData eventData)
    {
        // New game
        if (name.Equals(MenuInterface.NewGame))
        {
            // Show proper elements
            _menuInterface.ActivateElement(_menuInterface.ClassMenuImg.transform);
            _menuInterface.ActivateElement(_menuInterface.HeroBackgroundImg.transform);
            _menuInterface.ActivateElement(_menuInterface.NewBackImg.transform);
            _menuInterface.ActivateElement(_menuInterface.PaladinClassImg.transform);
            // Hide proper elements
            _menuInterface.DeactivateElement(_menuInterface.NewGameImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.LoadGameImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.CreditsImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.ExitFemoraImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.FemoraTxt.transform);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.ChooseYourClass;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Load game
        if (name.Equals(MenuInterface.LoadGame))
        {
            // Activate load menu
            _menuInterface.ActivateLoadMenu();
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Credits
        if (name.Equals(MenuInterface.Credits))
        {
            // Show proper elements
            _menuInterface.ActivateElement(_menuInterface.CreditsMenuImg.transform);
            _menuInterface.ActivateElement(_menuInterface.CreditsBackImg.transform);
            // Hide proper elements
            _menuInterface.DeactivateElement(_menuInterface.NewGameImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.LoadGameImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.CreditsImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.ExitFemoraImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.FemoraTxt.transform);
            _menuInterface.DeactivateElement(_menuInterface.MenuHintTxt.transform.parent);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.SelectSomeOption;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // New game back
        if (name.Equals(MenuInterface.NewBack))
        {
            // Show proper elements
            _menuInterface.ActivateElement(_menuInterface.NewGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.LoadGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.CreditsImg.transform);
            _menuInterface.ActivateElement(_menuInterface.ExitFemoraImg.transform);
            _menuInterface.ActivateElement(_menuInterface.FemoraTxt.transform);
            _menuInterface.ActivateElement(_menuInterface.MenuHintTxt.transform.parent);
            // Hide proper elements
            _menuInterface.DeactivateElement(_menuInterface.ClassMenuImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.HeroBackgroundImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.NewBackImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.PaladinClassImg.transform);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.SelectSomeOption;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Load game back
        if (name.Equals(MenuInterface.LoadBack))
        {
            // Show proper elements
            _menuInterface.ActivateElement(_menuInterface.NewGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.LoadGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.CreditsImg.transform);
            _menuInterface.ActivateElement(_menuInterface.ExitFemoraImg.transform);
            _menuInterface.ActivateElement(_menuInterface.FemoraTxt.transform);
            _menuInterface.ActivateElement(_menuInterface.MenuHintTxt.transform.parent);
            // Hide proper elements
            _menuInterface.DeactivateElement(_menuInterface.LoadMenuImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.LoadBackImg.transform);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.SelectSomeOption;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Error load back
        if (name.Equals(MenuInterface.ErrorBack))
        {
            // Hide proper elements
            _menuInterface.ActivateElement(_menuInterface.LoadBackImg.transform);
            // Show proper elements
            _menuInterface.DeactivateElement(_menuInterface.ErrorMenuImg.transform);
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Credits back
        if (name.Equals(MenuInterface.CreditsBack))
        {
            // Show proper elements
            _menuInterface.ActivateElement(_menuInterface.NewGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.LoadGameImg.transform);
            _menuInterface.ActivateElement(_menuInterface.CreditsImg.transform);
            _menuInterface.ActivateElement(_menuInterface.ExitFemoraImg.transform);
            _menuInterface.ActivateElement(_menuInterface.FemoraTxt.transform);
            _menuInterface.ActivateElement(_menuInterface.MenuHintTxt.transform.parent);
            // Hide proper elements
            _menuInterface.DeactivateElement(_menuInterface.CreditsMenuImg.transform);
            _menuInterface.DeactivateElement(_menuInterface.CreditsBackImg.transform);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.SelectSomeOption;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Exit Femora
        if (name.Equals(MenuInterface.ExitFemora))
        {
            // Terminate program
            Application.Quit();
        }
        // Annul creating hero
        if (name.Equals(MenuInterface.AnnulHero))
        {
            // Activate new game back button
            _menuInterface.ActivateElement(_menuInterface.NewBackImg.transform);
            // Hide create menu
            _menuInterface.DeactivateElement(_menuInterface.CreateMenuImg.transform);
            // Hide hero image
            _menuInterface.DeactivateElement(_menuInterface.HeroImageImg.transform);
            // Hide hero stats
            _menuInterface.DeactivateElement(_menuInterface.StatsMenuImg.transform);
            // Set proper hint
            _menuInterface.MenuHintTxt.text = MenuInterface.ChooseYourClass;
            // Clear input field
            _menuInterface.NameInputField.text = ItemClass.EmptySpace;
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Accept creating hero
        if (name.Equals(MenuInterface.AcceptHero))
        {
            // Set white text
            _menuInterface.SetWhiteText(transform);
            // Check data corectness
            _menuInterface.CheckNameCorrectness(_menuInterface.NameInputField.text);
        }
        // Create hero back
        if (name.Equals(MenuInterface.CreateBack))
        {
            // Hide back to create menu button
            _menuInterface.DeactivateElement(_menuInterface.CreateBackImg.transform);
            // Show annul create hero button
            _menuInterface.ActivateElement(_menuInterface.AnnulHeroImg.transform);
            // Show input field
            _menuInterface.ActivateElement(_menuInterface.NameInputField.transform);
            // Set proper info text
            _menuInterface.CreateTxt.text = MenuInterface.TypeNameForHero;
            // Change info text postion
            _menuInterface.CreateTxt.transform.localPosition = new Vector3
                (_menuInterface.CreateTxt.transform.localPosition.x,
                _menuInterface.CreateTxt.transform.localPosition.y * 2,
                _menuInterface.CreateTxt.transform.localPosition.z);
            // Set white text
            _menuInterface.SetWhiteText(transform);
        }
        // Paladin class
        if (name.Equals(MenuInterface.PaladinClass))
        {
            // Check if menu is active
            if (!_menuInterface.CreateMenuImg.IsActive())
                // Play click sound
                _menuMusicManager.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.Click, SoundDatabase.ItemSounds));
            // Show create menu
            _menuInterface.ActivateElement(_menuInterface.CreateMenuImg.transform);
            // Deactivate new game back button
            _menuInterface.DeactivateElement(_menuInterface.NewBackImg.transform);
            // Hide crosses
            _menuInterface.HideMenuCrosses(transform);
            // Break action
            return;
        }
        // Saves
        if (name.Contains(MenuInterface.Save))
        {
            // Get save contents
            string contents = GetComponentInChildren<Text>().text;
            // Check if it is empty space
            if (contents.Equals(ItemClass.EmptySpace) || _menuInterface.ErrorMenuImg.IsActive())
                // Break action
                return;
            // Try load save
            _menuInterface.LoadGameProgress(contents);
            // Hide crosses
            _menuInterface.HideMenuCrosses(transform);
        }
        // Play click sound
        _menuMusicManager.AudioSrc.PlayOneShot(SoundDatabase
            .GetProperSound(SoundDatabase.Click, SoundDatabase.ItemSounds));
    }

    // Show class stats and image when user is creating character
    public void ShowClassInfo()
    {
        // Check if create menu is active
        if (!_menuInterface.CreateMenuImg.IsActive())
            // Break action
            return;
        // Show hero image
        _menuInterface.ActivateElement(_menuInterface.HeroImageImg.transform);
        // Show hero stats
        _menuInterface.ActivateElement(_menuInterface.StatsMenuImg.transform);
        // Check if data are correct
        if (!_menuInterface.CreateBackImg.IsActive())
            // Set type name hint
            _menuInterface.MenuHintTxt.text = MenuInterface.UseOnlyAlphanumericSymbols;
        // Something gone wrong
        else
        {
            // Check if user reached heroes limit
            if (_menuInterface.CreateTxt.text.Equals(MenuInterface.TooManyHeroes))
                // Set hint about heroes limit
                _menuInterface.MenuHintTxt.text = MenuInterface.YouHaveTooManyCharacters;
            // Check if user inserted wrong name
            if (_menuInterface.CreateTxt.text.Equals(MenuInterface.NameIsAlreadyTaken))
                // Set hint about wrong hero name
                _menuInterface.MenuHintTxt.text = MenuInterface.YouHaveTypedWrongName;
        }
    }
}