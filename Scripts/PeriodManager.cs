using UnityEngine;
using System.Collections.Generic;

public class PeriodManager : MonoBehaviour
{
    // Day color
    public Color DayColor;
    // Night color
    public Color NightColor;
    // Time factor (the bigger the longer the day)
    [Range(10, 720)]
    public int TimeFac;
    // Sun light
    private Light _sun;
    // Skybox
    private Material _skybox;
    // Torches
    private ParticleSystem[] _torches;
    // Campfires
    private ParticleSystem[] _campfires;
    // Infernal lanterns
    private ParticleSystem[] _infernalLanterns;
    // Check if is day
    private bool _isDay;
    // Check if fire is active
    private bool _isFire;
    // Skybox exposure
    private float _exposure;
    // Light color differences
    private float _rDiff;
    private float _gDiff;
    private float _bDiff;
    // Hero lamp
    private Light _lamp;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        AdaptPeriod();
    }

    // Set basic parameters
    private void Init()
    {
        _sun = GameObject.Find("Sun").GetComponent<Light>();
        _sun.intensity = _exposure = 1f;
        _lamp = GameObject.FindGameObjectWithTag(HeroClass.HeroTag).GetComponentInChildren<Light>();
        _lamp.intensity = 0f;
        _skybox = Resources.Load<Material>("Skybox/Skybox");
        _skybox.SetFloat("_Exposure", _exposure);
        _rDiff = Mathf.Abs(DayColor.r - NightColor.r);
        _gDiff = Mathf.Abs(DayColor.g - NightColor.g);
        _bDiff = Mathf.Abs(DayColor.b - NightColor.b);
        List<ParticleSystem> torches = new List<ParticleSystem>();
        List<ParticleSystem> campfires = new List<ParticleSystem>();
        List<ParticleSystem> infernalLanterns = new List<ParticleSystem>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        for (int cnt = 0; cnt < allObjects.Length; cnt++)
        {
            if (allObjects[cnt].name.Equals(ItemClass.Torch))
            {
                ParticleSystem[] particles = allObjects[cnt].GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                    torches.Add(particle);
            }
            else if (allObjects[cnt].name.Equals(ItemClass.Campfire))
            {
                ParticleSystem[] particles = allObjects[cnt].GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                    campfires.Add(particle);
            }
            else if (allObjects[cnt].name.Equals(ItemClass.InfernalLantern))
            {
                ParticleSystem[] particles = allObjects[cnt].GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particles)
                    infernalLanterns.Add(particle);
            }
        }
        _torches = torches.ToArray();
        _campfires = campfires.ToArray();
        _infernalLanterns = infernalLanterns.ToArray();
        _isFire = false;
        _isDay = true;
    }

    // Adapt period to time
    private void AdaptPeriod()
    {
        // Get current sky color
        Color currentColor = RenderSettings.ambientSkyColor;
        // Is day
        if (_isDay)
        {
            // Check light color
            if (currentColor.r < NightColor.r || currentColor.g < NightColor.g || currentColor.b < NightColor.b)
            {
                // Set night color
                currentColor.r = NightColor.r;
                currentColor.g = NightColor.g;
                currentColor.b = NightColor.b;
            }
            // Check light intensity
            if (_sun.intensity < 0f || _exposure < 0.1f)
            {
                // Switch time of day
                _sun.intensity = 0;
                _exposure = 0.1f;
                _isDay = false;
            }
            // Check light intensity
            if (_sun.intensity < 0.5f)
            {
                // Check lamp intensity
                if (_lamp.intensity > 1f)
                    _lamp.intensity = 1f;
                // Increase lamp intensity
                _lamp.intensity += Time.deltaTime / (TimeFac / 2f);
            }
            // Decrease light intensity 
            _sun.intensity -= Time.deltaTime / TimeFac;
            // Decrease exposure
            _exposure -= Time.deltaTime / (TimeFac + (TimeFac * 0.1f));
            _skybox.SetFloat("_Exposure", _exposure);
            // Change sky color
            currentColor.r -= Time.deltaTime / (TimeFac / _rDiff);
            currentColor.g -= Time.deltaTime / (TimeFac / _gDiff);
            currentColor.b -= Time.deltaTime / (TimeFac / _bDiff);
        }
        // Is night
        else
        {
            // Check light color
            if (currentColor.r > DayColor.r || currentColor.g > DayColor.g || currentColor.b > DayColor.b)
            {
                // Set day color
                currentColor.r = DayColor.r;
                currentColor.g = DayColor.g;
                currentColor.b = DayColor.b;
            }
            // Check light intensity
            if (_sun.intensity > 1f || _exposure > 1f)
            {
                // Switch time of day
                _sun.intensity = 1;
                _isDay = true;
            }
            // Check light intensity
            if (_sun.intensity < 0.5f)
            {
                // Check lamp intensity
                if (_lamp.intensity < 0f)
                    _lamp.intensity = 0f;
                // Decrease lamp intensity
                _lamp.intensity -= Time.deltaTime / (TimeFac / 2f);
            }
            // Increase light intensity
            _sun.intensity += Time.deltaTime / TimeFac;
            // Increase exposure
            _exposure += Time.deltaTime / (TimeFac + (TimeFac * 0.1f));
            _skybox.SetFloat("_Exposure", _exposure);
            // Change sky color
            currentColor.r += Time.deltaTime / (TimeFac / _rDiff);
            currentColor.g += Time.deltaTime / (TimeFac / _gDiff);
            currentColor.b += Time.deltaTime / (TimeFac / _bDiff);
        }
        // Enable fire
        if (_sun.intensity < 0.25f)
        {
            // Check if fire is active
            if (!_isFire)
            {
                // Search torches
                foreach (ParticleSystem torch in _torches)
                {
                    // Get main module
                    ParticleSystem.MainModule main = torch.main;
                    // Restart particle system
                    torch.Simulate(0f, true, true);
                    // Enable looping
                    main.loop = true;
                    // Set torch
                    torch.Play();
                }
                // Search campfires
                foreach (ParticleSystem campfire in _campfires)
                {
                    // Get main module
                    ParticleSystem.MainModule main = campfire.main;
                    // Restart particle system
                    campfire.Simulate(0f, true, true);
                    // Enable looping
                    main.loop = true;
                    // Set campfire
                    campfire.Play();
                }
                // Search infernal lanterns
                foreach (ParticleSystem infernalLantern in _infernalLanterns)
                {
                    // Get main module
                    ParticleSystem.MainModule main = infernalLantern.main;
                    // Restart particle system
                    infernalLantern.Simulate(0f, true, true);
                    // Enable looping
                    main.loop = true;
                    // Set infernal Lantern
                    infernalLantern.Play();
                }
                // Set that fire is active
                _isFire = true;
            }
        }
        // Disable fire
        else
        {
            // Check if fire is active
            if (_isFire)
            {
                // Search torches
                foreach (ParticleSystem torch in _torches)
                {
                    // Get main module
                    ParticleSystem.MainModule main = torch.main;
                    // Disable looping
                    main.loop = false;
                }
                // Search campfires
                foreach (ParticleSystem campfire in _campfires)
                {
                    // Get main module
                    ParticleSystem.MainModule main = campfire.main;
                    // Disable looping
                    main.loop = false;
                }
                // Search infernal lanterns
                foreach (ParticleSystem infernalLantern in _infernalLanterns)
                {
                    // Get main module
                    ParticleSystem.MainModule main = infernalLantern.main;
                    // Disable looping
                    main.loop = false;
                }
                // Set that fire is inactive
                _isFire = false;
            }
        }
        // Set sky color
        RenderSettings.ambientSkyColor = new Color(currentColor.r, currentColor.g, currentColor.b);
    }
}