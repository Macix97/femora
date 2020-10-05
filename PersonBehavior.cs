using UnityEngine;
using UnityEngine.AI;

public class PersonBehavior : MonoBehaviour
{
    // Person AI
    private NavMeshAgent _navMeshAgent;
    // Check if person is moving
    private bool _isMoving;
    // Check if person is on tour
    private bool _isOnTour;
    // Next tour time
    private float _nextTour;
    // Person animation controller
    private Animator _animator;
    // Person class
    private PersonClass _personClass;
    // Hero class
    private HeroClass _heroClass;
    // Current locatation
    private Vector3 _targetLocation;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        GetSomeRandomPosition();
        GoToPosition();
    }

    // Set basic parameters
    private void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _personClass = GetComponent<PersonClass>();
        _heroClass = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponent<HeroClass>();
        _nextTour = Time.time;
    }

    // Generate some path
    private void GetSomeRandomPosition()
    {
        // Check if person is talking
        if (_heroClass.IsTalking)
        {
            // Look at hero
            transform.LookAt(_heroClass.GetComponent<Transform>());
            // Break action
            return;
        }
        // Check if person is on tour
        if (_isOnTour)
            // Break action
            return;
        // Check if person is waiting some time
        if (Time.time < _nextTour)
            // Break action
            return;
        // Set new target location
        _targetLocation = _personClass.Route[(Random.Range(0, _personClass.Route.Length))];
        // Set that person is on tour
        _isOnTour = true;
    }

    // Go to some position
    private void GoToPosition()
    {
        // Get hero distance
        float distToHero = Vector3.Distance(transform.position, _heroClass.transform.position);
        // Check if person is on tour
        if (!_isOnTour)
            // Break action
            return;
        // Set position
        _navMeshAgent.destination = _targetLocation;
        // Stop person after tour or when hero is talking with person
        if ((!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= 0) || _heroClass.IsTalking
            || distToHero < 2f)
        {
            // Stop person
            _isMoving = _isOnTour = false;
            _navMeshAgent.isStopped = true;
            // Stop person in position some time
            _nextTour = Time.time + _personClass.Interval;
            // Check if person is talking
            if (_heroClass.IsTalking)
                // Look at hero
                transform.LookAt(_heroClass.transform);
        }
        // Check distance
        else if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance > 0)
        {
            // Move person to position
            _isMoving = true;
            _navMeshAgent.isStopped = false;
        }
        // Play proper animation
        _animator.SetBool(PersonClass.PersonMove, _isMoving);
    }

    // Generate some gold when hero meets person
    public void GeneratePersonGold()
    {
        _personClass.Gold = Random.Range(PersonClass.MinGold, PersonClass.MaxGold);
    }
}