using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Describes the behavior of the main hero.
/// </summary>
public class HeroBehavior : MonoBehaviour
{
    // Maximal click distance
    private readonly int MaxDist = 100;
    // Check if hero is moving
    private bool _isMoving;
    // Check if running mode is active 
    private bool _isRunningMode;
    // Next skills use time
    private float[] _nextSkillsUse;
    // Skills duration
    private float[] _skillsDuration;
    // Check if skills are active
    private bool[] _isSkillsActive;
    // Next attack time
    private float _nextAttack;
    // Next cast time
    private float _nextCast;
    // Check which mouse button is clicked
    private string _mouseClickButton;
    // Check object type
    private bool _isTerrain;
    private bool _isEnemy;
    private bool _isContainer;
    private bool _isPerson;
    private bool _isItem;
    // Check if hero uses skill
    private bool _isSkillUse;
    // Hero AI
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    // Raycast hit
    private RaycastHit _raycastHit;
    // Hero target
    private Transform _target;
    // Hero class
    private HeroClass _heroClass;
    // Hero inventory
    private HeroInventory _heroInventory;
    // Hero skill
    private HeroSkill _heroSkill;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Enemy parameter
    private EnemyParameter _enemyParameter;
    // Game interface
    private GameInterface _gameInterface;
    // Game mouse action
    private GameMouseAction _gameMouseAction;
    // Hero animation controller
    private Animator _heroAnimator;
    // Hero sound
    private HeroSound _heroSound;
    // Isometric camera
    private CameraManager _isoCam;
    // Person class
    public PersonClass PersonClass { get; set; }

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
        SwitchMovingMode();
        CheckSkillTimer();
        CheckHeroAction();
        SetProperMoveAnimation();
    }

    // Set basic parameters
    private void Init()
    {
        // Set references
        _heroAnimator = GetComponent<Animator>();
        _heroClass = GetComponent<HeroClass>();
        _heroInventory = GetComponent<HeroInventory>();
        _heroSkill = GetComponent<HeroSkill>();
        _heroParameter = GetComponent<HeroParameter>();
        _heroSound = GetComponent<HeroSound>();
        _agent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        _gameMouseAction = GetComponent<GameMouseAction>();
        _isoCam = Camera.main.GetComponent<CameraManager>();
        // Reset action possibility
        float time = Time.time;
        _nextAttack = _nextCast = time;
        _isRunningMode = true;
        _nextSkillsUse = _skillsDuration = new float[_heroSkill.HeroSkills.Length];
        _isSkillsActive = new bool[_heroSkill.HeroSkills.Length];
        // Set skills properties
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
        {
            _isSkillsActive[cnt] = false;
            _nextSkillsUse[cnt] = _skillsDuration[cnt] = time;
        }
    }

    /// <summary>
    /// Checks if mouse button is clicking.
    /// </summary>
    /// <param name="mouseButton">A text that represents the key code of the left mouse button.</param>
    /// <param name="mouseSkill">A structure that represents an active skill.</param>
    public void CheckMouseClick(string mouseButton, HeroSkillDatabase.Skill mouseSkill)
    {
        // Check mouse click (disable action for specific skills)
        if (!(Input.GetButtonDown(mouseButton) && !GameMouseAction.IsMouseOverUI()))
            // Break action
            return;
        // Finish conversation
        _heroClass.IsTalking = false;
        // Set new mouse click
        _mouseClickButton = mouseButton;
        // Check if releasable skill is using now
        CheckSkillUse(mouseSkill);
        // Check if hero is trying use skill
        if (_isSkillUse)
            // Break action
            return;
        // Check other hero behave
        CheckHeroBehave();
    }

    /// <summary>
    /// Checks what hero is doing now depending on recognized object.
    /// </summary>
    private void CheckHeroAction()
    {
        // Reset cast animation
        _heroAnimator.SetBool(HeroClass.SkillMotion, false);
        // Reset attack animation
        _heroAnimator.SetBool(HeroClass.AttackMotion, false);
        // Get current time
        float time = Time.time;
        // Check if hero is talking
        if (_heroClass.IsTalking)
        {
            // Look at person
            transform.LookAt(_target);
            // Break action
            return;
        }
        // Check action possibility
        if (time < _nextAttack || time < _nextCast)
            // Break action
            return;
        // Check mouse skill - left click
        CheckMouseClick(GameInterface.LeftMouseBtn,
            _gameInterface.LeftSkill);
        // Check mouse skill - right click
        CheckMouseClick(GameInterface.RightMouseBtn,
            _gameInterface.RightSkill);
        // Attack selected enemy
        if (_isEnemy)
            AttackEnemy();
        // Go to container and open it
        else if (_isContainer)
            OpenContainer();
        // Try pick up item
        else if (_isItem)
            PickUpItem();
        // Got to talk with person
        else if (_isPerson)
            GoToPerson();
        // Go to selected position
        else if (_isTerrain)
            MoveToPosition();
    }

    /// <summary>
    /// Switches current moving mode (walk or run).
    /// </summary>
    private void SwitchMovingMode()
    {
        // Check if hero is talking with person
        if (_heroClass.IsTalking)
            // Break action
            return;
        // Check if button is pressing
        if (Input.GetKeyDown(KeyCode.R))
            // Toogle running mode
            _isRunningMode = !_isRunningMode;
    }

    /// <summary>
    /// Sets proper move animation depending on moving mode.
    /// </summary>
    private void SetProperMoveAnimation()
    {
        // Hero is running
        if (_isRunningMode)
        {
            // Set run mode
            _agent.speed = _heroClass.RunSpeed;
            _heroAnimator.SetBool(HeroClass.RunMotion, _isMoving);
            _heroAnimator.SetBool(HeroClass.WalkMotion, false);
        }
        // Hero is walking
        else
        {
            // Set walk mode
            _agent.speed = _heroClass.WalkSpeed;
            _heroAnimator.SetBool(HeroClass.RunMotion, false);
            _heroAnimator.SetBool(HeroClass.WalkMotion, _isMoving);
        }
    }

    /// <summary>
    /// Targets specific enemy.
    /// </summary>
    private void TargetEnemy()
    {
        // Enemy is target
        _target = _raycastHit.transform;
        // Set logic variables
        _isTerrain = false;
        _isContainer = false;
        _isItem = false;
        _isPerson = false;
        _isEnemy = true;
    }

    /// <summary>
    /// Targets specific container.
    /// </summary>
    private void TargetContainer()
    {
        // Container is target
        _target = _raycastHit.transform;
        // Set logic variables
        _isTerrain = false;
        _isEnemy = false;
        _isItem = false;
        _isPerson = false;
        _isContainer = true;
    }

    /// <summary>
    /// Targets specific item.
    /// </summary>
    private void TargetItem()
    {
        // Container is target
        _target = _raycastHit.transform;
        // Set logic variables
        _isTerrain = false;
        _isContainer = false;
        _isEnemy = false;
        _isPerson = false;
        _isItem = true;
    }

    /// <summary>
    /// Targets specific person.
    /// </summary>
    private void TargetPerson()
    {
        // Container is target
        _target = _raycastHit.transform;
        // Set logic variables
        _isTerrain = false;
        _isContainer = false;
        _isEnemy = false;
        _isItem = false;
        _isPerson = true;
    }

    /// <summary>
    /// Targets specific position.
    /// </summary>
    private void MoveHero()
    {
        // Set logic variables
        _isEnemy = false;
        _isContainer = false;
        _isItem = false;
        _isPerson = false;
        _isTerrain = true;
    }

    /// <summary>
    /// Attacks specific enemy marked as target.
    /// </summary>
    private void AttackEnemy()
    {
        // Set target
        _agent.destination = _target.position;
        // Check distance
        if (!_agent.pathPending && _agent.remainingDistance > _heroClass.AttackDist)
        {
            // Move hero
            _isMoving = true;
            _agent.isStopped = false;
        }
        // Target is reachable
        else if (!_agent.pathPending && _agent.remainingDistance <= _heroClass.AttackDist)
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            // Set proper rotation
            transform.LookAt(_target);
            // Enable attack animation
            _heroAnimator.SetBool(HeroClass.AttackMotion, true);

            //--- Decrement enemy health (function invoking during animation) ---//

            // Set next attack time
            _nextAttack = Time.time + _heroClass.AttackRate;
            // End attack
            _isEnemy = false;
        }
    }

    /// <summary>
    /// Opens specific container marked as target.
    /// </summary>
    private void OpenContainer()
    {
        // Set target
        _agent.destination = _target.position;
        // Check distance
        if (!_agent.pathPending && _agent.remainingDistance > _heroClass.InteractDist)
        {
            // Move hero
            _isMoving = true;
            _agent.isStopped = false;
        }
        else if (!_agent.pathPending && _agent.remainingDistance <= _heroClass.InteractDist)
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            // Set proper rotation
            transform.LookAt(_target);
            // Get container animator
            Animator animator = _target.GetComponentInChildren<Animator>();
            // Check if container is close
            if (!animator.GetBool(ItemClass.OpenContainerMotion))
            {
                // Get container kind
                string kind = _target.GetComponent<ContainerClass>().Kind;
                // Get cursor hover
                CursorHover cursorHover = _target.GetComponent<CursorHover>();
                // Inactivate container
                cursorHover.IsObjectInactive = true;
                // Destroy panel
                cursorHover.DestroyPanel();
                // Change container tag
                _target.tag = ItemClass.Untagged;
                // Open container
                animator.SetBool(ItemClass.OpenContainerMotion, true);
                // Check if container kind contains more than one word
                if (kind.Contains(ItemClass.WhiteSpace))
                    // Play proper sound
                    _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(kind.Replace(ItemClass
                        .WhiteSpace, ItemClass.EmptySpace), SoundDatabase.ContainerSounds));
                // There is one word
                else
                    // Play proper sound
                    _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                        .GetProperSound(kind, SoundDatabase.ContainerSounds));
            }
            // End loot
            _isContainer = false;
        }
    }

    /// <summary>
    /// Picks up specific item marked as target.
    /// </summary>
    private void PickUpItem()
    {
        // Check if hero clicked hint
        if (_target.name.Equals(ItemClass.PanelId))
            // Set new target
            _target = _target.parent;
        // Set target
        _agent.destination = _target.position;
        // Check distance
        if (!_agent.pathPending && _agent.remainingDistance > _heroClass.InteractDist)
        {
            // Move hero
            _isMoving = true;
            _agent.isStopped = false;
        }
        else if (!_agent.pathPending && _agent.remainingDistance <= _heroClass.InteractDist)
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            // Set proper rotation
            transform.LookAt(_target);
            // Check if hero can pick up item
            _heroInventory.CheckHeroInventory(ref _target);
            // End item pickup
            _isItem = false;
        }
    }

    /// <summary>
    /// Moves the hero to specific person marked as target.
    /// </summary>
    private void GoToPerson()
    {
        // Set target
        _agent.destination = _target.position;
        // Destination is not reached
        if (!_agent.pathPending && _agent.remainingDistance > _heroClass.InteractDist)
        {
            // Move hero
            _isMoving = true;
            _agent.isStopped = false;
        }
        // Destination is reached
        else if (!_agent.pathPending && _agent.remainingDistance <= _heroClass.InteractDist)
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            // Set that hero looks at person
            transform.LookAt(_target);
            // Set that person looks at hero
            _target.LookAt(transform);
            // Start conversation
            _heroClass.IsTalking = true;
            // Remember last met person
            PersonClass = _target.GetComponent<PersonClass>();
            // She is Mirlanda and hero is wound
            if (PersonClass.name.Equals("Mirlanda")
                && (_heroClass.CurHealth < _heroClass.MaxHealth
                || _heroClass.CurEnergy < _heroClass.MaxEnergy))
            {
                // Renew hero vitality and energy
                _heroParameter.AdaptHealth(_heroClass.MaxHealth);
                _heroParameter.AdaptEnergy(_heroClass.MaxEnergy);
                // Play proper sound
                _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.Sanctuary, SoundDatabase.PaladinSounds));
            }
            // Prepare user interface to talk
            _gameInterface.PrepareUIToTalk();
            // Toogle camera
            _isoCam.ToggleCameraView();
            // End walk
            _isPerson = false;
        }
    }

    /// <summary>
    /// Moves the hero to specific position marked as target.
    /// </summary>
    private void MoveToPosition()
    {
        // Calculate new path
        _agent.CalculatePath(_raycastHit.point, _path);
        // Check if path is reachable
        if (_path.status.Equals(NavMeshPathStatus.PathPartial)
            || _path.status.Equals(NavMeshPathStatus.PathInvalid))
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            _isTerrain = false;
            // Break action
            return;
        }
        // Set target
        _agent.destination = _raycastHit.point;
        // Destination is not reached
        if (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance)
        {
            // Move hero
            _isMoving = true;
            _agent.isStopped = false;
        }
        // Destination is reached
        else if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            // Stop hero
            _isMoving = false;
            _agent.isStopped = true;
            _isTerrain = false;
        }
    }

    /// <summary>
    /// Checks if some skill is active and if mouse button is clicked.
    /// </summary>
    /// <param name="mouseSkill">A structure that represents an active skill.</param>
    private void CheckSkillUse(HeroSkillDatabase.Skill mouseSkill)
    {
        // Hero is not using skill
        _isSkillUse = false;
        // Check set skill
        if (!(mouseSkill.Type.Equals(HeroSkillDatabase.Releasable)
            && !mouseSkill.Result.Equals(HeroSkillDatabase.AttackMelee)))
            // Break action
            return;
        // Hero is trying use skill
        _isSkillUse = true;
        // Search proper skill
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
            // Check skill ID
            if (mouseSkill.Kind.Equals(_heroSkill.HeroSkills[cnt].Kind))
            {
                // Try use skill
                UseReleasableSkill(_heroSkill.HeroSkills[cnt], ref _nextSkillsUse[cnt],
                    ref _skillsDuration[cnt], ref _isSkillsActive[cnt]);
                // Break action
                return;
            }
    }

    /// <summary>
    /// Counts down until the activated skill expires.
    /// </summary>
    private void CheckSkillTimer()
    {
        // Search proper skill
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
        {
            // Check if skill is uses
            if (!_isSkillsActive[cnt])
                // Skip action
                continue;
            // Decrement skill duration
            _skillsDuration[cnt] -= Time.deltaTime;
            // Check if skill duration is over
            if (_skillsDuration[cnt] > 0)
                // Skip action
                continue;
            // Reset statistics
            _heroParameter.ResetStats(_heroSkill.HeroSkills[cnt]);
            // Set that skill is not using
            _isSkillsActive[cnt] = false;
            // Reset aura
            _heroParameter.ResetAura(_heroSkill.HeroSkills[cnt]);
        }
    }

    /// <summary>
    /// Checks what hero is doing now depending on clicked object.
    /// </summary>
    private void CheckHeroBehave()
    {
        // Reset navigation path
        ResetHeroPath();
        // Check hit
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _raycastHit, MaxDist))
            // Break action
            return;
        // Check if hero aim enemy
        if (_raycastHit.collider.tag.Equals(EnemyClass.EnemyTag))
            TargetEnemy();
        // Check if hero aim container
        else if (_raycastHit.collider.tag.Equals(ItemClass.ContainerTag))
            TargetContainer();
        // Check if hero aim item
        else if (_raycastHit.collider.tag.Equals(ItemClass.ItemTag))
            TargetItem();
        // Check if hero aim person
        else if (_raycastHit.collider.tag.Equals(PersonClass.PersonTag))
            TargetPerson();
        // Move to clicked position
        else
            MoveHero();
    }

    /// <summary>
    /// Activates the effect of the selected releasable skill.
    /// </summary>
    /// <param name="skill">A structure that represents an skill.</param>
    /// <param name="nextSkillUse">A number that contains time to next use in seconds.</param>
    /// <param name="skillDuration">A number that contains skill duration in seconds.</param>
    /// <param name="isSkillActive">A boolean that determines if a skill is active</param>
    private void UseReleasableSkill(HeroSkillDatabase.Skill skill, ref float nextSkillUse,
        ref float skillDuration, ref bool isSkillActive)
    {
        // Check use skill possibility
        if (Time.time < nextSkillUse || isSkillActive
            || _heroClass.CurEnergy < skill.EnergyCost)
            // Break action
            return;
        // Stop hero
        _isMoving = false;
        _agent.isStopped = true;
        _isTerrain = false;
        // Set casting animation
        _heroAnimator.SetBool(HeroClass.SkillMotion, true);
        // Set casting length
        _nextCast = Time.time + _heroClass.SkillRate;
        // Set next skill use time
        nextSkillUse = Time.time + skill.Rate;
        // Set new statistics
        _heroParameter.SetStats(skill);
        // Set skill duration
        skillDuration = skill.Rate;
        // Set skill uses
        isSkillActive = true;
        // Set aura
        _heroParameter.SetAura(skill);
        // Set new skill kind
        string kind = skill.Kind.Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace);
        // Play proper sound
        _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(kind, _heroSound.HeroSounds));
    }

    /// <summary>
    /// Disables releasable skills before saving the game.
    /// </summary>
    public void DisableReleasableSkill()
    {
        // Search proper skill
        for (int cnt = 0; cnt < _heroSkill.HeroSkills.Length; cnt++)
        {
            // Check if skill is uses
            if (!_isSkillsActive[cnt])
                // Skip action
                continue;
            // Reset statistics
            _heroParameter.ResetStats(_heroSkill.HeroSkills[cnt]);
            // Set that skill is not using
            _isSkillsActive[cnt] = false;
            // Reset aura
            _heroParameter.ResetAura(_heroSkill.HeroSkills[cnt]);
        }
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    public void DealDamage()
    {
        // Check if it is enemy
        if (tag.Equals(EnemyClass.EnemyTag))
            // Break action
            return;
        // Get enemy class
        EnemyClass enemyClass = _target.GetComponent<EnemyClass>();
        // Get enemy sound
        EnemySound enemySound = _target.GetComponent<EnemySound>();
        // Set new enemy type
        _heroClass.LastEnemyType = enemyClass.Type;
        // Set new enemy level
        _heroClass.LastEnemyLvl = enemyClass.Level;
        // Get enemy parameters
        _enemyParameter = _target.GetComponent<EnemyParameter>();
        // Calculate basic damage
        int damage = _heroParameter.CalcDamage();
        // Check damage
        if (!damage.Equals(0))
            // Disable enemy attack
            _target.GetComponent<Animator>().SetBool(EnemyClass.AttackMotion, false);
        // Get current distance
        float distance = Vector3.Distance(transform.position, _target.position);
        // Check attack effectivness
        if (distance > _heroClass.AttackDist || damage.Equals(0))
            // Break action
            return;
        // Check if hero is using melee attack skill - left click
        if (_gameInterface.LeftSkill.Result.Equals(HeroSkillDatabase.AttackMelee)
            && _mouseClickButton.Equals(GameInterface.LeftMouseBtn)
            && _heroClass.CurEnergy >= _gameInterface.LeftSkill.EnergyCost)
        {
            // Set new skill kind
            string kind = _gameInterface.LeftSkill.Kind.Replace(ItemClass.WhiteSpace,
                ItemClass.EmptySpace);
            // Calculate damage with bonus
            _enemyParameter.AdaptHealth(damage - (int)_gameInterface.LeftSkill.Effect);
            // Calculate current energy
            _heroParameter.AdaptEnergy((int)-_gameInterface.LeftSkill.EnergyCost);
            // Play extra sound during deal damage
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(kind, _heroSound.HeroSounds));
            // Display skill effect
            GameObject skillEffect =
                Instantiate(Resources.Load(ItemDatabase.Prefabs + _gameInterface.LeftSkill.Kind),
                new Vector3(_target.position.x, _target.position.y + ItemClass.SkillEffectPoint,
                _target.position.z), Quaternion.identity) as GameObject;
            // Destroy skill effect
            Destroy(skillEffect, ItemClass.SkillEffectTime);
        }
        // Check if hero is using melee attack skill - right click
        else if (_gameInterface.RightSkill.Result.Equals(HeroSkillDatabase.AttackMelee)
                 && _mouseClickButton.Equals(GameInterface.RightMouseBtn)
                 && _heroClass.CurEnergy >= _gameInterface.RightSkill.EnergyCost)
        {
            // Set new skill kind
            string kind = _gameInterface.RightSkill.Kind
                .Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace);
            // Calculate damage with bonus
            _enemyParameter.AdaptHealth(damage - (int)_gameInterface.RightSkill.Effect);
            // Calculate current energy
            _heroParameter.AdaptEnergy((int)-_gameInterface.RightSkill.EnergyCost);
            // Play extra sound during deal damage
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(kind, _heroSound.HeroSounds));
            // Display skill effect
            GameObject skillEffect =
                Instantiate(Resources.Load(ItemDatabase.Prefabs + _gameInterface.RightSkill.Kind),
                new Vector3(_target.position.x, _target.position.y + ItemClass.SkillEffectPoint,
                _target.position.z), Quaternion.identity) as GameObject;
            // Destroy skill effect
            Destroy(skillEffect, ItemClass.SkillEffectTime);
        }
        // Deal basic damage
        else
        {
            // Adapt enemy health
            _enemyParameter.AdaptHealth(damage);
            // Play hit sound during deal damage
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                    .GetProperSound(SoundDatabase.Hit, _heroSound.HeroSounds));
        }
        // Generate gush
        GameObject gush =
            Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.Gush),
            new Vector3(_target.position.x, _target.position.y + ItemClass.GushPoint,
            _target.position.z), Quaternion.identity) as GameObject;
        // Destroy gush with delay
        Destroy(gush, ItemClass.GushTime);
        // Enemy is alive
        if (_target.GetComponent<EnemyClass>().CurHealth > 0)
        {
            // Play some random grunt sound
            enemySound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(SoundDatabase.Grunt0
                + Random.Range(0, SoundDatabase.GetGruntsAmt(enemySound.EnemySounds)),
                enemySound.EnemySounds));
            // Play impact animation
            _target.GetComponent<Animator>().SetTrigger(EnemyClass.ImpactMotion);
        }
        // Enemy is dead
        else
            // Play death sound
            enemySound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Death, enemySound.EnemySounds));
    }

    /// <summary>
    /// Resets hero path in specific situation.
    /// </summary>
    public void ResetHeroPath()
    {
        // Reset raycast hit
        _raycastHit = new RaycastHit();
        // Reset current path
        _agent.ResetPath();
    }
}