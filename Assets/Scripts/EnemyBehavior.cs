using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // Enemy AI
    private NavMeshAgent _navMeshAgent;
    // Next attack time
    private float _nextAttack;
    // Distance from enemy to hero
    private float _dist;
    // Enemy animation controller
    private Animator _animator;
    // Check if enemy is moving
    private bool _isMoving;
    // Enemy target
    private Transform _target;
    // Enemy class
    private EnemyClass _enemyClass;
    // Enemy parameter
    private EnemyParameter _enemyParameter;
    // Enemy sound
    private EnemySound _enemySound;
    // Hero class
    private HeroClass _heroClass;
    // Hero parameter
    private HeroParameter _heroParameter;
    // Hero sound
    private HeroSound _heroSound;
    // Location manager
    private LocationManager _locationManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_enemyParameter.IsEnemyDead())
        {
            _enemyParameter.CheckRespawn();
            return;
        }
        if (!_heroParameter.IsHeroDead())
            CheckDist();
        else
            StopEnemy();
        SetProperAnimation();
    }

    // Set basic parameters
    private void Init()
    {
        _target = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).transform;
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _heroSound = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroSound>();
        _heroParameter = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroParameter>();
        _locationManager = GameObject.Find(GameInterface.GameController).GetComponent<LocationManager>();
        _enemyClass = GetComponent<EnemyClass>();
        _enemyParameter = GetComponent<EnemyParameter>();
        _enemySound = GetComponent<EnemySound>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _nextAttack = Time.time;
    }

    // Check actual distance between enemy and hero
    private void CheckDist()
    {
        // Disable attack animation
        _animator.SetBool(EnemyClass.AttackMotion, false);
        // Check action possibility
        if (Time.time < _nextAttack)
            // Break action
            return;
        // Set distance to target
        _dist = Vector3.Distance(transform.position, _target.position);
        // Check distance
        if (_dist <= _enemyClass.DetectRay)
            AttackHero();
        else
            StopEnemy();
    }

    // Play proper animation
    private void SetProperAnimation()
    {
        // Play proper animation
        _animator.SetBool(EnemyClass.MoveMotion, _isMoving);
    }

    // Attack target in range
    private void AttackHero()
    {
        // Set position
        _navMeshAgent.destination = _target.position;
        // Check distance
        if (!_navMeshAgent.pathPending
            && _navMeshAgent.remainingDistance
            > _enemyClass.AttackRay)
        {
            // Check if hero is in save area
            if (_locationManager.IsHeroInProtectedArea(_target))
            {
                // Stop enemy
                _isMoving = false;
                _navMeshAgent.isStopped = true;
                // Break action
                return;
            }
            // Move to hero
            _isMoving = true;
            _navMeshAgent.isStopped = false;
        }
        else if (!_navMeshAgent.pathPending
                && _navMeshAgent.remainingDistance
                <= _enemyClass.AttackRay)
        {
            // Stop enemy
            _isMoving = false;
            _navMeshAgent.isStopped = true;
            // Set proper rotation
            transform.LookAt(_target);
            // Enable attack animation
            _animator.SetBool(EnemyClass.AttackMotion, true);
            // Set next attack time
            _nextAttack = Time.time + _enemyClass.AttackRate;
            //--- Decrement hero health ---//
            // (function invoking during animation)
        }
    }

    // Stop enemy in actual position
    private void StopEnemy()
    {
        // Reset navigation path
        _navMeshAgent.ResetPath();
        // Stop enemy in place
        _isMoving = false;
        _navMeshAgent.isStopped = true;
    }

    // Calculate proper damage and hurt hero
    public void DealDamage()
    {
        // Check if it is hero
        if (tag.Equals(HeroClass.HeroTag) || _heroClass.CurHealth < 1)
            // Break action
            return;
        // Get current distance
        float distance = Vector3.Distance(transform.position, _target.position);
        // Check distance
        if (distance > _enemyClass.AttackRay)
            // Break action
            return;
        // Calculate damage
        int damage = _enemyParameter.CalcDamage();
        // Check damage value
        if (damage > -1)
            // Break action
            return;
        // Decrement hero health
        _heroParameter.AdaptHealth(damage);
        // Generate gush
        GameObject gush = Instantiate(Resources.Load(ItemDatabase.Prefabs + ItemClass.Gush),
            new Vector3(_target.position.x, _target.position.y + ItemClass.GushPoint, _target.position.z),
            Quaternion.identity) as GameObject;
        // Destroy gush with delay
        Destroy(gush, ItemClass.GushTime);
        // Hero is alive
        if (_heroClass.CurHealth > 0)
            // Play some random grunt sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(SoundDatabase.Grunt0
                + Random.Range(0, SoundDatabase.GetGruntsAmt(_heroSound.HeroSounds)), _heroSound.HeroSounds));
        // Hero is dead
        else
            // Play death sound
            _heroSound.AudioSrc.PlayOneShot(SoundDatabase
                .GetProperSound(SoundDatabase.Death, _heroSound.HeroSounds));
        // Play hit sound during deal damage
        _enemySound.AudioSrc.PlayOneShot(SoundDatabase.GetProperSound(SoundDatabase.Hit, _enemySound.EnemySounds));
    }
}