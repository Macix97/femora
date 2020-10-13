using UnityEngine;
using UnityEngine.AI;

public class EnemyParameter : MonoBehaviour
{
    // Enemy damage
    private int _damage;
    // Enemy class
    private EnemyClass _enemyClass;
    // Hero class
    private HeroClass _heroClass;
    // Hero parameters
    private HeroParameter _heroParameter;
    // Game interface
    private GameInterface _gameInterface;
    // Enemy items
    private MeshRenderer[] _enemyItems;
    // Enemy particle systems
    private ParticleSystem[] _enemyParticleSystems;
    // Enemy spine
    private Transform _enemySpine;
    // Dead material
    private Material _deadMaterial;
    // Channel alpha value
    private float _alpha;
    // Check if enemy is spawning
    private bool _isSpawning;
    // Check if particle systems are running
    private bool _isParticle;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsEnemyDead() && !_isSpawning)
            DecayEnemy();
        else if (_isSpawning)
            SpawnEnemy();
    }

    // Set basic parameters
    private void Init()
    {
        _enemyClass = GetComponent<EnemyClass>();
        _enemySpine = transform.Find(ItemClass.Spine);
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _heroParameter = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroParameter>();
        _gameInterface = GameObject
            .Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
        _enemyItems = GetComponentsInChildren<MeshRenderer>();
        _enemyParticleSystems = GetComponentsInChildren<ParticleSystem>();
        _deadMaterial = Resources.Load<Material>(ItemDatabase.Materials +
            name.Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace) + ItemClass.Dead);
        _alpha = 1f;
        _isSpawning = false;
        _isParticle = true;
    }

    // Calculate actual health
    public void AdaptHealth(int health)
    {
        // Calculate health
        _enemyClass.CurHealth += health;
        // Set lower limit (current health)
        if (_enemyClass.CurHealth < 1)
        {
            // Correct enemy health
            _enemyClass.CurHealth = 0;
            // Set death time
            _enemyClass.DeathTime = Time.time;
            // Change material
            GetComponent<SkinnedMeshRenderer>().material = _deadMaterial;
            // Add enemy experience to hero
            _heroParameter.AdaptExp(_enemyClass.Experience);
            // Generate loot
            GenerateEnemyItem();
            // Play dead animation
            GetComponent<Animator>().SetTrigger(EnemyClass.DeathMotion);
            // Break action
            return;
        }
        // Set upper limit
        if (_enemyClass.CurHealth > _enemyClass.MaxHealth)
            _enemyClass.CurHealth = _enemyClass.MaxHealth;
        // Set lower limit (maximal health)
        if (_enemyClass.MaxHealth < 1)
            _enemyClass.MaxHealth = 1;
        // Update graphical interface
        _gameInterface.AdaptHealthBar(_enemyClass.transform, EnemyClass.EnemyTag);
    }

    // Calculate actual damage
    public int CalcDamage()
    {
        // Random some number
        int randomPercent = Random.Range(0, 100);
        // Check drawn number
        if (_heroClass.DodgeChance <= randomPercent)
        {
            // Calculate physical damage
            if (_enemyClass.AttackType
                .Equals(EnemyDatabase.PhysicalAttack))
                _damage = -Random.Range(_enemyClass.MinDamage
                    - _heroClass.Defence, _enemyClass.MaxDamage
                    - _heroClass.Defence);
            // Calculate magical damage
            if (_enemyClass.AttackType
                .Equals(EnemyDatabase.MagicalAttack))
                _damage = -Random.Range(_enemyClass.MinDamage
                    - _heroClass.ResistMagic, _enemyClass.MaxDamage
                    - _heroClass.ResistMagic);
        }
        else
            // enemy missed hero
            return 0;
        // Check final damage
        if (_damage >= 0)
            return -1;
        return _damage;
    }

    // Generate items after kill enemy
    public void GenerateEnemyItem()
    {
        // Generated item amount
        int itemAmt = Random.Range(_enemyClass.MinItemAmt, _enemyClass.MaxItemAmt);
        // Dropped item amount
        int droppedItemAmt = 0;
        // Get enemy position
        Vector3 enemyPos = transform.position;
        // Get enemy Y axis rotation
        float yRot = transform.rotation.eulerAngles.y;
        // Set second counter
        int cnt2 = 0;
        // Set third counter
        int cnt3 = 0;
        // Generate some items
        for (int cnt1 = 0; cnt1 < itemAmt; cnt1++)
        {
            // Random item rank
            int itemRank = Random.Range(0, _enemyClass.ItemPool.Length);
            // Random item chance
            int itemChance = Random.Range(0, 100);
            // Check if item chance
            if (itemChance >= _enemyClass.ItemChancePercent[itemRank])
                // Check another option
                continue;
            // Increase dropped item amount
            droppedItemAmt++;
            // Get some item from pool
            string itemKind = _enemyClass.ItemPool[itemRank]
                [Random.Range(0, _enemyClass.ItemPool[itemRank].Length)].Kind;
            // Create item
            GameObject generatedItem = Instantiate(Resources.Load(ItemDatabase.Prefabs + itemKind),
                new Vector3(enemyPos.x, enemyPos.y, enemyPos.z), Quaternion.identity) as GameObject;
            // Generate new name for item
            string newName = generatedItem.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
            // Change generated item name
            generatedItem.name = newName;
            // Check item sequence
            if ((cnt1 % 3).Equals(0) && !cnt1.Equals(0))
            {
                // Increment third counter
                cnt3++;
                // Reset second counter
                cnt2 = 0;
            }
            // Check enemy rotation
            if (yRot >= 0 && yRot < 90)
                // Set item position
                generatedItem.transform.position = new Vector3(enemyPos.x - cnt2, enemyPos.y, enemyPos.z - cnt3);
            // Check enemy rotation
            else if (yRot >= 90 && yRot < 180)
                // Set item position
                generatedItem.transform.position = new Vector3(enemyPos.x - cnt2, enemyPos.y, enemyPos.z + cnt3);
            // Check enemy rotation
            else if (yRot >= 180 && yRot < 270)
                // Set item position
                generatedItem.transform.position = new Vector3(enemyPos.x + cnt2, enemyPos.y, enemyPos.z + cnt3);
            // Check enemy rotation
            else if (yRot >= 270 && yRot <= 360)
                // Set item position
                generatedItem.transform.position = new Vector3(enemyPos.x + cnt2, enemyPos.y, enemyPos.z - cnt3);
            // Increment second counter
            cnt2++;
            // Get item class
            ItemClass itemClass = generatedItem.GetComponentInChildren<ItemClass>();
            // Play drop animation
            generatedItem.GetComponentInChildren<Animator>().SetTrigger(ItemClass.DropItemMotion);
        }
        // Check if some item is dropped
        if (droppedItemAmt.Equals(0))
        {
            // Random gold rank
            string goldRank = _enemyClass.ItemPool[Random.Range(0, _enemyClass.ItemPool.Length)][0].Rank;
            // Get random indexes
            int[] indexes = ItemDatabase.GetMoneyIndex(goldRank);
            // Random some gold
            string goldKind = ItemDatabase.Money[indexes[Random.Range(0, indexes.Length)]].Kind;
            // Create item
            GameObject generatedItem = Instantiate(Resources.Load(ItemDatabase.Prefabs + goldKind),
                new Vector3(enemyPos.x, enemyPos.y, enemyPos.z), Quaternion.identity) as GameObject;
            // Generate new name for item
            string newName = generatedItem.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
            // Change generated item name
            generatedItem.name = newName;
            // Get item class
            ItemClass itemClass = generatedItem.GetComponentInChildren<ItemClass>();
            // Play drop animation
            generatedItem.GetComponentInChildren<Animator>().SetTrigger(ItemClass.DropGoldMotion);
        }
    }

    // Check if enemy is dying
    public bool IsEnemyDead()
    {
        // Check if enemy is alive
        if (_enemyClass.CurHealth > 0)
        {
            // Activate Nav mesh agent
            GetComponent<NavMeshAgent>().enabled = true;
            // Activate collider
            GetComponent<Collider>().enabled = true;
            // It is false
            return false;
        }
        // Deactivate Nav mesh agent
        GetComponent<NavMeshAgent>().enabled = false;
        // Deactivate collider
        GetComponent<Collider>().enabled = false;
        // It is true
        return true;
    }

    public void CheckRespawn()
    {
        // Check if enemy is spawning
        if (_isSpawning)
            // Break action
            return;
        // Get current time
        float time = Time.time;
        // Check death time - hide corpse
        if (time >= _enemyClass.DeathTime + EnemyClass.PutrefactionTime
            && time < _enemyClass.DeathTime + EnemyClass.RespawnTime)
        {
            // Hide corpse
            GetComponent<SkinnedMeshRenderer>().enabled = false;
            // Search enemy items
            foreach (MeshRenderer enemyItem in _enemyItems)
                // Hide proper enemy item
                enemyItem.enabled = false;
            // Play respawn animation
            GetComponent<Animator>().SetTrigger(EnemyClass.RespawnMotion);
        }
        // Check death time - respawn enemy
        else if (time >= _enemyClass.DeathTime + EnemyClass.RespawnTime)
        {
            // Spawn portal
            GameObject portal = Instantiate(Resources.Load(ItemDatabase.Prefabs + EnemyClass.DevilishPortal),
                _enemyClass.RespawnPosition, Quaternion.identity) as GameObject;
            // Disable respawn animation
            GetComponent<Animator>().ResetTrigger(EnemyClass.RespawnMotion);
            // Set respawn position
            transform.position = _enemyClass.RespawnPosition;
            // Reset death time
            _enemyClass.DeathTime = 0f;
            // Show enemy
            GetComponent<SkinnedMeshRenderer>().enabled = true;
            // Set that enemy is spawning
            _isSpawning = true;
            // Set spawn time
            _enemyClass.SpawnTime = Time.time;
            // Destroy portal
            Destroy(portal, ItemClass.PortalTime);
        }
    }

    // Generate stain after enemy death
    private void GenerateStain()
    {
        // Generate stain
        GameObject stain = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.Stain),
            new Vector3(_enemySpine.position.x, transform.position.y, _enemySpine.position.z),
            Quaternion.identity) as GameObject;
        // Destroy stain
        Destroy(stain, ItemClass.StainTime);
    }

    // Decay enemy after his death
    private void DecayEnemy()
    {
        // Check decay time
        if (Time.time < _enemyClass.DeathTime + EnemyClass.DecayTime)
            // Break action
            return;
        // Create new property block
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        // Update alpha
        _alpha -= Time.deltaTime / EnemyClass.DecayTime;
        // Check alpha
        if (_alpha < 0f)
            // Correct alpha
            _alpha = 0f;
        // Set color properties
        materialPropertyBlock.SetColor("_BaseColor", new Color(0.8f, 0.8f, 0.8f, _alpha));
        // Confirm properties
        GetComponent<SkinnedMeshRenderer>().SetPropertyBlock(materialPropertyBlock);
    }

    // Spawn and show enemy
    private void SpawnEnemy()
    {
        // Check if particle systems are disabled
        if (!_isParticle)
        {
            // Search enemy particle systems
            foreach (ParticleSystem enemyParticleSystem in _enemyParticleSystems)
            {
                // Get main module
                ParticleSystem.MainModule main = enemyParticleSystem.main;
                // Enable looping
                main.loop = true;
                // Restart particle system
                enemyParticleSystem.Simulate(0f, true, true);
                // Play particle system
                enemyParticleSystem.Play();
            }
            // Set that particle systems are enabled
            _isParticle = true;
        }
        // Check spawn time
        if (Time.time < _enemyClass.SpawnTime + ItemClass.PortalTime)
        {
            // Create new property block
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            // Update alpha
            _alpha += Time.deltaTime / ItemClass.PortalTime;
            // Check alpha
            if (_alpha > 1f)
                // Correct alpha
                _alpha = 1f;
            // Set color properties
            materialPropertyBlock.SetColor("_BaseColor", new Color(0.8f, 0.8f, 0.8f, _alpha));
            // Confirm properties
            GetComponent<SkinnedMeshRenderer>().SetPropertyBlock(materialPropertyBlock);
        }
        else
        {
            // Set new material
            GetComponent<SkinnedMeshRenderer>().material =
                Resources.Load<Material>(ItemDatabase.Materials +
                name.Replace(ItemClass.WhiteSpace, ItemClass.EmptySpace));
            // Respawn enemy
            _enemyClass.CurHealth = _enemyClass.MaxHealth;
            // Search enemy items
            foreach (MeshRenderer enemyItem in _enemyItems)
                // Show proper enemy item
                enemyItem.enabled = true;
            // Set that enemy is spawned
            _isSpawning = false;
        }
    }

    // Hide particle systems
    public void ExpireParticleSystems()
    {
        // Search enemy particle systems
        foreach (ParticleSystem enemyParticleSystem in _enemyParticleSystems)
        {
            // Get main module
            ParticleSystem.MainModule main = enemyParticleSystem.main;
            // Disable looping
            main.loop = false;
            // Set that particle systems are disabled
            _isParticle = false;
        }
    }
}