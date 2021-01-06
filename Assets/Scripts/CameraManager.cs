using UnityEngine;

/// <summary>
/// Controls the isometric camera movement.
/// </summary>
public class CameraManager : MonoBehaviour
{
    // Camera parameters
    public static readonly string CameraTag = "Camera";
    private Transform _target;
    private Vector3 _camPos;
    private Camera _iso;
    private Camera _fpc;
    private const int SmoothSpeed = 5;
    private const float CamDist = 15f;
    // Starting camera position
    public Vector3 StartPos { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveIsometricCamera();
    }

    // Set basic parameters
    private void Init()
    {
        _target = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).transform;
        _fpc = GameObject.FindGameObjectWithTag(CameraTag).GetComponent<Camera>();
        _iso = GetComponent<Camera>();
        _fpc.enabled = false;
        StartPos = transform.position;
    }

    /// <summary>
    /// Moves the camera smoothly when the hero is walking.
    /// </summary>
    private void MoveIsometricCamera()
    {
        // Calculate position
        _camPos = _target.position;
        // Set distance
        _camPos -= transform.forward * CamDist;
        // Set position
        transform.position = Vector3.Slerp(transform.position, _camPos, SmoothSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Switches the view from the isometric camera to the first person camera or vice versa
    /// </summary>
    public void ToggleCameraView()
    {
        // toggle cameras
        _iso.enabled = !_iso.enabled;
        _fpc.enabled = !_fpc.enabled;
    }
}