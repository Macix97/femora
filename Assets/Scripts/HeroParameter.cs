using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Executes additional actions related to the hero.
/// </summary>
public class HeroParameter : MonoBehaviour
{
    // Methods IDs
    public readonly string StudyId = "Study";
    // Constant modifiers
    private readonly float LevelMod01 = 1.6f;
    private readonly float LevelMod02 = 1.2f;
    private readonly float LevelMod03 = 1.1f;
    private readonly float LevelMod04 = 1.05f;
    private readonly float LevelMod05 = 1.01f;
    private readonly int LevelLimit = 99;
    private readonly int AttrPtsMod = 5;
    private readonly int SkillPtsMod = 1;
    public readonly int LearnValue = 1;
    // Skill modifiers
    private bool _isLeftSkillActive;
    private bool _isRightSkillActive;
    private bool _isRegenerativeSkill;
    // Variable modifiers
    public float ExpMod { get; set; }
    // Next regenerative skill use time
    private float _nextRegeneration;
    // Hero class
    private HeroClass _heroClass;
    // Hero skill
    private HeroSkill _heroSkill;
    // Hero sound
    private HeroSound _heroSound;
    // Game interface
    private GameInterface _gameInterface;
    // Hero animation controller
    private Animator _animator;
    // Attack animation speed;
    public float AttackAnimSpeed { get; set; }
    // Hero spine
    private Transform _heroSpine;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameInterface.IsGamePaused)
            return;
        AdaptExp(0);
        AdaptAnimationSpeed();
        AdaptAttackChance();
        AdaptDodgeChance();
    }

    // Set basic parameters
    private void Init()
    {
        _isLeftSkillActive = _isRightSkillActive = _isRegenerativeSkill = false;
        _nextRegeneration = Time.time;
        _heroSpine = transform.Find(ItemClass.Spine);
        _heroClass = GetComponent<HeroClass>();
        _heroSkill = GetComponent<HeroSkill>();
        _heroSound = GetComponent<HeroSound>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        _animator = GetComponent<Animator>();
        ExpMod = 100;
        AttackAnimSpeed = _heroClass.AttackRate;
        _animator.SetFloat(HeroClass.AttackRateFloat, AttackAnimSpeed);
    }

    /// <summary>
    /// Updates current experience of the hero.
    /// </summary>
    /// <param name="experience">The experience points to add.</param>
    public void AdaptExp(int experience)
    {
        // Add experience
        _heroClass.TotalExp += experience;
        // Check level upgrade
        if (_heroClass.TotalExp < _heroClass.NextLvLExp)
            // Break action
            return;
        // Check if hero achieved 99 level
        if (_heroClass.Level.Equals(LevelLimit))
        {
            // Set experience limit
            _heroClass.TotalExp = _heroClass.NextLvLExp;
            // Break action
            return;
        }
        // Hero is very weak
        if (_heroClass.Level < 10)
            // Set new level modifier
            ExpMod *= LevelMod01;
        // Hero is weak
        else if (_heroClass.Level >= 10 && _heroClass.Level < 25)
            // Set new level modifier
            ExpMod *= LevelMod02;
        // Hero is strong
        else if (_heroClass.Level >= 25 && _heroClass.Level < 40)
            // Set new level modifier
            ExpMod *= LevelMod03;
        // Hero is very strong
        else if (_heroClass.Level >= 40 && _heroClass.Level < 65)
            // Set new level modifier
            ExpMod *= LevelMod04;
        // Hero is powerful
        else if (_heroClass.Level >= 65)
            // Set new level modifier
            ExpMod *= LevelMod05;
        // Set new level start
        _heroClass.LvlStart = _heroClass.NextLvLExp;
        // Set next level experience
        _heroClass.NextLvLExp += (int)ExpMod;
        // Set new level end
        _heroClass.LvlEnd = _heroClass.NextLvLExp;
        // Increment parameters
        _heroClass.Level++;
        _heroClass.AttributePts += AttrPtsMod;
        _heroClass.SkillPts += SkillPtsMod;
        // Play level sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase
            .GetProperSound(SoundDatabase.Level, SoundDatabase.ItemSounds));
    }

    /// <summary>
    /// Updates proper attribute of the hero.
    /// </summary>
    /// <param name="attributeId">The label that represents the identifier of the attribute.</param>
    /// <param name="methodId">The label that represents the identifier of the used method.</param>
    /// <param name="attrValue">The number that represents the value of the attribute.</param>
    public void AdaptAttr(string attributeId, string methodId, float attrValue)
    {
        // Set proper attribute
        if (attributeId.Equals(HeroClass.VitalityId))
        {
            _heroClass.Vitality += (int)attrValue;
            _heroClass.MaxHealth += (int)attrValue * HeroClass.HealthMod;
        }
        if (attributeId.Equals(HeroClass.WisdomId))
        {
            _heroClass.Wisdom += (int)attrValue;
            _heroClass.MaxEnergy += (int)attrValue * HeroClass.EnergyMod;
        }
        if (attributeId.Equals(HeroClass.StrengthId))
        {
            _heroClass.Strength += (int)attrValue;
            _heroClass.Capacity += (int)attrValue * HeroClass.CapacityMod;
            // Set proper damage
            _heroClass.MinDamage += (int)attrValue * HeroClass.MinDamageMod;
            _heroClass.MaxDamage += (int)attrValue * HeroClass.MaxDamageMod;
            // Check limit
            if (_heroClass.MinDamage > _heroClass.MaxDamage)
                // Set upper limit
                _heroClass.MaxDamage = _heroClass.MinDamage;
        }
        if (attributeId.Equals(HeroClass.AgilityId))
        {
            _heroClass.Agility += (int)attrValue;
            _heroClass.AttackRate -= attrValue * HeroClass.AttackRateMod;
            _heroClass.ActionChanceBonus += attrValue * HeroClass.ActionChanceMod;
            // Change animation speed
            AttackAnimSpeed += attrValue * HeroClass.AttackRateMod;
        }
        // Check adapt method
        if (methodId.Equals(StudyId))
            _heroClass.AttributePts -= (int)attrValue;
    }

    /// <summary>
    /// Updates proper skill of the hero.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    /// <param name="skillValue">The number that represents the value of the skill.</param>
    /// <param name="effect">The number that represents the effect of the skill.</param>
    /// <param name="energyCost">The number that represents the energy cost of the skill.</param>
    public void AdaptSkill(ref HeroSkillDatabase.Skill skill, int skillValue, float effect,
        float energyCost)
    {
        // Set skill parameters
        skill.Level += skillValue;
        skill.Effect += effect;
        skill.EnergyCost += energyCost;
        // Check if selected skill is active now - left click
        if (_gameInterface.LeftSkill.Kind.Equals(skill.Kind))
        {
            _gameInterface.LeftSkill.Level += skillValue;
            _gameInterface.LeftSkill.Effect += effect;
            _gameInterface.LeftSkill.EnergyCost += energyCost;
        }
        // Check if selected skill is active now - right click
        if (_gameInterface.RightSkill.Kind.Equals(skill.Kind))
        {
            _gameInterface.RightSkill.Level += skillValue;
            _gameInterface.RightSkill.Effect += effect;
            _gameInterface.RightSkill.EnergyCost += energyCost;
        }
        // Search stats modifiers
        for (int cnt = 0; cnt < skill.Stats.Length; cnt++)
        {
            // Check if skill increase damage
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Damage)
                && skill.Type.Equals(HeroSkillDatabase.Passive))
            {
                AdaptStats(HeroClass.MinDamageId, (int)effect);
                AdaptStats(HeroClass.MaxDamageId, (int)effect);
            }
            // Check if skill increase health
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Health)
                && skill.Type.Equals(HeroSkillDatabase.Passive))
                AdaptStats(HeroClass.MaxHealthId, (int)effect);
        }
        // Decrement hero skill points
        _heroClass.SkillPts -= skillValue;
        // Increment spent skill points
        _heroClass.SpentSkillPts += skillValue;
        // Update graphical interface
        _gameInterface.AdaptSkillInfo(_gameInterface.SkillTxt, skill);
    }

    /// <summary>
    /// Updates current statistics of the hero
    /// </summary>
    /// <param name="statsId">The label that represents the identifier of the statistic.</param>
    /// <param name="statsValue">The number that represents the value of the statistic.</param>
    public void AdaptStats(string statsId, float statsValue)
    {
        // Set proper statistics
        if (statsId.Equals(HeroClass.MaxHealthId))
            _heroClass.MaxHealth += (int)statsValue;
        if (statsId.Equals(HeroClass.MaxEnergyId))
            _heroClass.MaxEnergy += (int)statsValue;
        if (statsId.Equals(HeroClass.MinDamageId))
        {
            _heroClass.MinDamage += (int)statsValue;
            // Check limit
            if (_heroClass.MinDamage > _heroClass.MaxDamage)
                // Set upper limit
                _heroClass.MaxDamage = _heroClass.MinDamage;
        }
        if (statsId.Equals(HeroClass.MaxDamageId))
            _heroClass.MaxDamage += (int)statsValue;
        if (statsId.Equals(HeroClass.AttackRateId))
        {
            _heroClass.AttackRate -= statsValue;
            // Change animation speed
            AttackAnimSpeed += statsValue;
            // Set change in animator
            _animator.SetFloat(HeroClass.AttackRateFloat, AttackAnimSpeed);
        }
        if (statsId.Equals(HeroClass.AttackDistId))
            _heroClass.AttackDist += statsValue;
        if (statsId.Equals(HeroClass.DefenceId))
            _heroClass.Defence += (int)statsValue;
        if (statsId.Equals(HeroClass.ResistMagicId))
            _heroClass.ResistMagic += (int)statsValue;
        if (statsId.Equals(HeroClass.CapacityId))
            _heroClass.Capacity += statsValue;
    }

    /// <summary>
    /// Calculates current health of the hero.
    /// </summary>
    /// <param name="health">The health points to add or subtract.</param>
    public void AdaptHealth(int health)
    {
        // Calculate health
        _heroClass.CurHealth += health;
        // Set lower limit (current health)
        if (_heroClass.CurHealth < 1)
        {
            // Kill hero
            _heroClass.CurHealth = 0;
            _heroClass.CurEnergy = 0;
            // Close all open windows
            _gameInterface.HideCharWindow();
            _gameInterface.HideSkillWindow();
            _gameInterface.HideInventoryWindow();
            // Disable hero scripts
            GetComponent<HeroBehavior>().enabled = false;
            GetComponent<HeroInventory>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            // Set dead animation
            GetComponent<Animator>()
                .SetTrigger(HeroClass.DeathMotion);
            // Break action
            return;
        }
        // Check upper limit
        if (_heroClass.CurHealth > _heroClass.MaxHealth)
            // Set upper limit
            _heroClass.CurHealth = _heroClass.MaxHealth;
        // Check lower limit
        if (_heroClass.MaxHealth < 1)
            // Set lower limit
            _heroClass.MaxHealth = 1;
    }

    /// <summary>
    /// Calculates current energy of the hero.
    /// </summary>
    /// <param name="energy">The energy points to add or subtract.</param>
    public void AdaptEnergy(int energy)
    {
        // Calculate energy
        _heroClass.CurEnergy += energy;
        // Set lower limit (current energy)
        if (_heroClass.CurEnergy < 1)
            _heroClass.CurEnergy = 0;
        // Set upper limit
        if (_heroClass.CurEnergy > _heroClass.MaxEnergy)
            _heroClass.CurEnergy = _heroClass.MaxEnergy;
        // Set lower limit (maximal energy)
        if (_heroClass.MaxEnergy < 1)
            _heroClass.MaxEnergy = 1;
    }

    /// <summary>
    /// Checks if hero knows selected skill.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    /// <returns>
    /// The boolean that is true if the hero knows the skill or false if not.
    /// </returns>
    public bool IsSkill(HeroSkillDatabase.Skill skill)
    {
        // Check if hero knows skill
        if (skill.Level > 0 && !skill.Type.Equals(HeroSkillDatabase.Passive))
            // It is true
            return true;
        // It is false
        return false;
    }

    /// <summary>
    /// Activates normal attack.
    /// </summary>
    /// <param name="mouseSkill">A structure that represents an active skill.</param>
    public void ActivateAttack(ref HeroSkillDatabase.Skill mouseSkill)
    {
        // Check if attack is active
        if (mouseSkill.Type.Equals(HeroSkillDatabase.Attack))
            // Break action
            return;
        // Check if some skill is active
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
            // Check skill ID
            if (mouseSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
            {
                // Deactivate skill
                DeactivateSkill(_heroSkill.HeroSkills[cnt], mouseSkill);
                // Break action
                break;
            }
        // Remember mouse skill ID
        int tmpId = mouseSkill.Id;
        // Set new mouse skill
        mouseSkill = HeroSkillDatabase.NormalAttack;
        // Set new mouse skill ID
        mouseSkill.Id = tmpId;
        // Check if mouse hover proper mouse click
        if ((_gameInterface.MouseSkillImg.transform.IsChildOf(_gameInterface.LeftClickImg.transform)
            && mouseSkill.Id.Equals(GameInterface.LeftClickId))
            || (_gameInterface.MouseSkillImg.transform.IsChildOf(_gameInterface.RightClickImg.transform)
            && mouseSkill.Id.Equals(GameInterface.RightClickId)))
            // Adapt info about skill
            _gameInterface.AdaptMouseSkillInfo(_gameInterface.MouseSkillTxt, mouseSkill);
    }

    /// <summary>
    /// Checks if selected skill may be activated at this time.
    /// </summary>
    /// <param name="skill">A structure that represents a skill to activate.</param>
    /// <param name="mouseSkill">A structure that represents a current chosen skill.</param>
    public void CheckSkill(HeroSkillDatabase.Skill skill, ref HeroSkillDatabase.Skill mouseSkill)
    {
        // Check if skill is already use
        if (_gameInterface.LeftSkill.Kind.Equals(_gameInterface.RightSkill.Kind)
            && !mouseSkill.Type.Equals(HeroSkillDatabase.Attack))
            // Break action
            return;
        // Check if selected click has same skill
        if (skill.Kind.Equals(_gameInterface.LeftSkill.Kind)
            || skill.Kind.Equals(_gameInterface.RightSkill.Kind))
            // Break action
            return;
        // Check if active skill or regenerative is already use and hero wants use another
        if ((skill.Type.Equals(HeroSkillDatabase.Active)
            || skill.Type.Equals(HeroSkillDatabase.Regenerative))
            && (_gameInterface.LeftSkill.Type.Equals(HeroSkillDatabase.Regenerative)
            || _gameInterface.LeftSkill.Type.Equals(HeroSkillDatabase.Active)
            || _gameInterface.RightSkill.Type.Equals(HeroSkillDatabase.Regenerative)
            || _gameInterface.RightSkill.Type.Equals(HeroSkillDatabase.Active)))
            // Break action
            return;
        // Search skill to activation
        for (int cnt1 = 0; cnt1 < _heroSkill.HeroSkills.Length; cnt1++)
            // Check skill ID
            if (skill.Kind.Equals(_heroSkill.HeroSkills[cnt1].Kind))
                // Search current active skill
                for (int cnt2 = 0; cnt2 < _heroSkill.HeroSkills.Length; cnt2++)
                {
                    // Check if it is the same skill
                    if (cnt1.Equals(cnt2))
                        // Skip action
                        continue;
                    // Check current skill ID and deactivate current skill
                    if (mouseSkill.Kind.Equals(_heroSkill.HeroSkills[cnt2].Kind))
                        // Deactivate skill
                        DeactivateSkill(_heroSkill.HeroSkills[cnt2], mouseSkill);
                }
        // Remember mouse skill ID
        int tmpId = mouseSkill.Id;
        // Set new mouse skill
        mouseSkill = skill;
        // Set new mouse skill ID
        mouseSkill.Id = tmpId;
        // Check if mouse hover proper mouse click
        if ((_gameInterface.MouseSkillImg.transform.IsChildOf(_gameInterface.LeftClickImg.transform)
            && mouseSkill.Id.Equals(GameInterface.LeftClickId))
            || (_gameInterface.MouseSkillImg.transform.IsChildOf(_gameInterface.RightClickImg.transform)
            && mouseSkill.Id.Equals(GameInterface.RightClickId)))
            // Adapt info about skill
            _gameInterface.AdaptMouseSkillInfo(_gameInterface.MouseSkillTxt, mouseSkill);
        // Check if it is active or regenerative skill
        if (!(skill.Type.Equals(HeroSkillDatabase.Active) || skill.Type.Equals(HeroSkillDatabase.Regenerative)))
            // Break action
            return;
        // Set new skill kind
        string kind = skill.Kind.Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace);
        // Play proper sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(kind, _heroSound.HeroSounds));
    }

    /// <summary>
    /// Activates selected skill.
    /// </summary>
    /// <param name="skill">A structure that represents a skill to activate.</param>
    /// <param name="mouseSkill">A structure that represents a current chosen skill.</param>
    public void ActivateSkill(HeroSkillDatabase.Skill skill, HeroSkillDatabase.Skill mouseSkill)
    {
        // Check if skill is active - left click
        if (skill.Type.Equals(HeroSkillDatabase.Active) && mouseSkill.Kind.Equals(skill.Kind)
            && _isLeftSkillActive)
            // Break action
            return;
        // Check if skill is active - right click
        if (skill.Type.Equals(HeroSkillDatabase.Active) && mouseSkill.Kind.Equals(skill.Kind)
            && _isRightSkillActive)
            // Break action
            return;
        // Check if active skill is support
        if (skill.Result.Equals(HeroSkillDatabase.Support))
            // Break action
            return;
        // Set aura
        SetAura(skill);
        // Check if active skill is releasable
        if (skill.Type.Equals(HeroSkillDatabase.Releasable))
            // Break action
            return;
        // Check use regenerative skill possibility 
        if (Time.time < _nextRegeneration && skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Break action
            return;
        // Check if regenerative skill is using
        if (skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Set next regenerative skill use time
            _nextRegeneration = Time.time + skill.Rate;
        // Check hero energy
        if (_heroClass.CurEnergy < 1 && skill.EnergyCost > 0
            && skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Break action
            return;
        // Set statistics
        SetStats(skill);
        // Set active skill
        SetActiveSkill(skill, mouseSkill);
    }

    /// <summary>
    /// Deactivates selected skill.
    /// </summary>
    /// <param name="skill">A structure that represents a skill to deactivate.</param>
    /// <param name="mouseSkill">A structure that represents a current chosen skill.</param>
    public void DeactivateSkill(HeroSkillDatabase.Skill skill, HeroSkillDatabase.Skill mouseSkill)
    {
        // Check if skill is support
        if (skill.Result.Equals(HeroSkillDatabase.Support))
            // Break action
            return;
        // Reset aura
        ResetAura(skill);
        // Check if skill is releasable
        if (skill.Type.Equals(HeroSkillDatabase.Releasable))
            // Break action
            return;
        // Reset stats
        ResetStats(skill);
        // Reset active skill
        ResetActiveSkill(skill, mouseSkill);
    }

    /// <summary>
    /// Sets proper aura around the hero.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    public void SetAura(HeroSkillDatabase.Skill skill)
    {
        // Check if it is regenerative skill and aura is active
        if (skill.Type.Equals(HeroSkillDatabase.Regenerative) && _isRegenerativeSkill)
            // Break action
            return;
        // Check if it is attack melee skill
        if (skill.Result.Equals(HeroSkillDatabase.AttackMelee))
            // Break action
            return;
        // Create proper aura
        GameObject skillAura = Instantiate(Resources.Load(ItemDatabase.Prefabs + skill.Kind),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity) as GameObject;
        // Change aura name
        skillAura.name = skill.Kind;
        // Change aura parent
        skillAura.transform.SetParent(transform);
        // Check if it is regenerative skill
        if (skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Set that regenerative skill is active
            _isRegenerativeSkill = true;
        // Change hierarchy
        skillAura.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Sets proper statistics that improving the hero.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    public void SetStats(HeroSkillDatabase.Skill skill)
    {
        // Search stats modifiers
        for (int cnt = 0; cnt < skill.Stats.Length; cnt++)
        {
            // Health skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Health))
            {
                // Check if skill is active
                if (skill.Type.Equals(HeroSkillDatabase.Active))
                    // Active skill
                    AdaptStats(HeroClass.MaxHealthId, skill.Effect);
                // Check if skill is releasable
                if (skill.Type.Equals(HeroSkillDatabase.Releasable))
                {
                    // Active skill
                    AdaptStats(HeroClass.MaxHealthId, skill.Effect);
                    AdaptEnergy((int)-skill.EnergyCost);
                }
                // Check actual hero health (regenerative skill)
                if (_heroClass.CurHealth < _heroClass.MaxHealth
                    && skill.Type.Equals(HeroSkillDatabase.Regenerative))
                {
                    // Active skill
                    AdaptHealth((int)skill.Effect);
                    AdaptEnergy((int)-skill.EnergyCost);
                }
            }
            // Attack rate skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.AttackRate))
                // Activate skill
                AdaptStats(HeroClass.AttackRateId, skill.Effect * HeroClass.AttackRateMod);
            // Defence skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Defence))
                // Activate skill
                AdaptStats(HeroClass.DefenceId, (int)skill.Effect);
            // Damage skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Damage))
            {
                // Activate skill
                AdaptStats(HeroClass.MinDamageId, (int)skill.Effect);
                AdaptStats(HeroClass.MaxDamageId, (int)skill.Effect);
            }
        }
    }

    /// <summary>
    /// Resets the aura around the hero.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    public void ResetAura(HeroSkillDatabase.Skill skill)
    {
        // Check if it is regenerative skill
        if (skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Set that regenerative skill is inactive
            _isRegenerativeSkill = false;
        // Destroy aura
        Destroy(GameObject.Find(gameObject.name + "/" + skill.Kind));
    }

    /// <summary>
    /// Resets the statistics that improving the hero.
    /// </summary>
    /// <param name="skill">A structure that represents a hero skill.</param>
    public void ResetStats(HeroSkillDatabase.Skill skill)
    {
        // Check if active skill is regenerative
        if (skill.Type.Equals(HeroSkillDatabase.Regenerative))
            // Break action
            return;
        // Search stats modifiers
        for (int cnt = 0; cnt < skill.Stats.Length; cnt++)
        {
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.AttackRate))
                // Deactivate skill
                AdaptStats(HeroClass.AttackRateId, -skill.Effect * HeroClass.AttackRateMod);
            // Damage skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Damage))
            {
                // Deactivate skill
                AdaptStats(HeroClass.MinDamageId, (int)-skill.Effect);
                AdaptStats(HeroClass.MaxDamageId, (int)-skill.Effect);
            }
            // Defence skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Defence))
                // Deactivate skill
                AdaptStats(HeroClass.DefenceId, (int)-skill.Effect);
            // Health skill
            if (skill.Stats[cnt].Equals(HeroSkillDatabase.Health))
            {
                // Deactivate skill
                AdaptStats(HeroClass.MaxHealthId, (int)-skill.Effect);
                // Decrease current health
                if (_heroClass.CurHealth > _heroClass.MaxHealth)
                    _heroClass.CurHealth = _heroClass.MaxHealth;
            }
        }
    }

    /// <summary>
    /// Sets the appropriate logical variables that activate skill.
    /// </summary>
    /// <param name="skill">A structure that represents a skill to activate.</param>
    /// <param name="mouseSkill">A structure that represents a current chosen skill.</param>
    private void SetActiveSkill(HeroSkillDatabase.Skill skill, HeroSkillDatabase.Skill mouseSkill)
    {
        // Set that active skill is using - left click
        if (skill.Type.Equals(HeroSkillDatabase.Active)
            && mouseSkill.Id.Equals(GameInterface.LeftClickId))
            _isLeftSkillActive = true;
        // Set that active skill is using - right click
        if (skill.Type.Equals(HeroSkillDatabase.Active)
            && mouseSkill.Id.Equals(GameInterface.RightClickId))
            _isRightSkillActive = true;
    }

    /// <summary>
    /// Sets the appropriate logical variables that deactivate skill.
    /// </summary>
    /// <param name="skill">A structure that represents a skill to activate.</param>
    /// <param name="mouseSkill">A structure that represents a current chosen skill.</param>
    private void ResetActiveSkill(HeroSkillDatabase.Skill skill, HeroSkillDatabase.Skill mouseSkill)
    {
        // Set that active skill is not using - left click
        if (skill.Type.Equals(HeroSkillDatabase.Active)
            && mouseSkill.Id.Equals(GameInterface.LeftClickId))
            _isLeftSkillActive = false;
        // Set that active skill is not using - right click
        if (skill.Type.Equals(HeroSkillDatabase.Active)
            && mouseSkill.Id.Equals(GameInterface.RightClickId))
            _isRightSkillActive = false;
    }

    /// <summary>
    /// Updates info about the hero dodge chance.
    /// </summary>
    private void AdaptDodgeChance()
    {
        // Set new dodge chance
        _heroClass.DodgeChance = _heroClass.StartDodgeChance + (int)_heroClass.ActionChanceBonus
            - _heroClass.LastEnemyLvl;
        // Check lower limit
        if (_heroClass.DodgeChance < HeroClass.MinActionChance)
            // Set proper value
            _heroClass.DodgeChance = HeroClass.MinActionChance;
        // Check upper limit
        else if (_heroClass.DodgeChance > HeroClass.MaxActionChance)
            // Set proper value
            _heroClass.DodgeChance = HeroClass.MaxActionChance;
    }

    /// <summary>
    /// Updates info about the hero attack chance.
    /// </summary>
    private void AdaptAttackChance()
    {
        // Set new dodge chacne
        _heroClass.AttackChance = _heroClass.StartAttackChance + (int)_heroClass.ActionChanceBonus
            - _heroClass.LastEnemyLvl;
        // Check lower limit
        if (_heroClass.AttackChance < HeroClass.MinActionChance)
            // Set proper value
            _heroClass.AttackChance = HeroClass.MinActionChance;
        // Check upper limit
        else if (_heroClass.AttackChance > HeroClass.MaxActionChance)
            // Set proper value
            _heroClass.AttackChance = HeroClass.MaxActionChance;
    }

    /// <summary>
    /// Updates speed of the hero animations.
    /// </summary>
    private void AdaptAnimationSpeed()
    {
        // Set change in animator
        _animator.SetFloat(HeroClass.AttackRateFloat, AttackAnimSpeed);
    }

    /// <summary>
    /// Calculates the attack power of the hero.
    /// </summary>
    /// <returns>
    /// The damage as a negative number.
    /// </returns>
    public int CalcDamage()
    {
        // Random some number
        int randomPercent = UnityEngine.Random.Range(0, 100);
        // Check drawn number
        if (_heroClass.AttackChance > randomPercent)
            // hit enemy
            return -UnityEngine.Random.Range(_heroClass.MinDamage, _heroClass.MaxDamage);
        // Hero missed enemy
        return 0;
    }

    /// <summary>
    /// Checks if the hero is dead.
    /// </summary>
    /// <returns>
    /// The boolean that is true if the hero is dead or false if not.
    /// </returns>
    public bool IsHeroDead()
    {
        // Check if hero is alive
        if (_heroClass.CurHealth > 0)
            // It is false
            return false;
        // It is true
        return true;
    }

    /// <summary>
    /// Generates blood stain after the hero death.
    /// </summary>
    public void GenerateStain()
    {
        // Generate stain
        GameObject stain = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.Stain),
            new Vector3(_heroSpine.position.x, transform.position.y, _heroSpine.position.z),
            Quaternion.identity) as GameObject;
        // Destroy stain
        Destroy(stain, ItemClass.StainTime);
    }

    /// <summary>
    /// Expires the aura effects after the hero death.
    /// </summary>
    public void ExpireParticleSystems()
    {
        // Get all hero particle systems
        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        // Search hero particle systems
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            // Get main module
            ParticleSystem.MainModule main = particleSystem.main;
            // Disable looping
            main.loop = false;
        }
    }

    /// <summary>
    /// Ignite the aura effects after the hero resurrection.
    /// </summary>
    public void IgniteParticleSystems()
    {
        // Prepare list for skill names
        List<string> skillNamesList = new List<string>();
        // Get all hero particle systems
        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        // Search hero particle systems
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            // Get name of skill
            string skillName = particleSystem.transform.parent.name;
            // Set proper name of skill
            string newSkillName = RemoveSpacesFromString(skillName);
            // Current skill name is not in list
            if (skillNamesList.Find(n => n.Equals(newSkillName)) == null)
                // Add skill name to list
                skillNamesList.Add(newSkillName);
            // Get main module
            ParticleSystem.MainModule main = particleSystem.main;
            // Enable looping
            main.loop = true;
            // Start particle system
            particleSystem.Play();
        }
        // Convert list to array
        string[] skillNames = skillNamesList.ToArray();
        // Search skill names
        foreach (string skillName in skillNames)
        {
            // Play proper skill sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(skillName, SoundDatabase.PaladinSounds));
        }
    }

    /// <summary>
    /// Removes white spaces from the selected string.
    /// </summary>
    /// <param name="oldString">A label that represents string to convert.</param>
    /// <returns>
    /// The converted string.
    /// </returns>
    private string RemoveSpacesFromString(string oldString)
    {
        // Create new string builder
        StringBuilder stringBuilder = new StringBuilder();
        // Search old string
        foreach (char c in oldString)
        {
            // Check if it is not space
            if (!c.Equals(' '))
                // Add character to string builder
                stringBuilder.Append(c);
        }
        // Return new string
        return stringBuilder.ToString();
    }
}