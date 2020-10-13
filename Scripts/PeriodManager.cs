using UnityEngine;
using System.Collections.Generic;

public class PeriodManager : MonoBehaviour
{
    // Sun
    public static readonly string Sun = "Sun";
    // Day skybox
    public static readonly string Day = "Day";
    // Night skybox
    public static readonly string Night = "Night";
    // Period modifier
    private float _periodMod = 360f;
    // No light
    private float _noLight = 0f;
    // Poor light
    private float _poorLight = 0.2f;
    // Weak light
    private float _weakLight = 0.4f;
    // Average light
    private float _averageLight = 0.6f;
    // Strong light
    private float _strongLight = 0.8f;
    // Mighty light
    private float _mightyLight = 1f;
    // Sun light
    private Light _sun;
    // Lightmap data
    private LightmapData[] _lightMaps;
    // Check if is day
    private bool _isDay;
    // Day skybox
    private Material _daySkybox;
    // Night skybox
    private Material _nightSkybox;
    // Torches
    private ParticleSystem[] _torches;
    // Campfires
    private ParticleSystem[] _campfires;
    // Infernal lanterns
    private ParticleSystem[] _infernalLanterns;
    // Check if fire is active
    private bool _isFire;

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
        _sun = GameObject.Find(Sun).GetComponent<Light>();
        _lightMaps = new LightmapData[1];
        _lightMaps[0] = new LightmapData();
        AdaptLightmaps(PeriodDatabase.Noon);
        _sun.intensity = 1f;
        _daySkybox = Resources.Load<Material>(ItemDatabase.Materials + Day);
        _nightSkybox = Resources.Load<Material>(ItemDatabase.Materials + Night);
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
        RenderSettings.skybox = _daySkybox;
        _isDay = true;
        _isFire = false;
    }

    // Adapt period to time
    private void AdaptPeriod()
    {
        // Is day
        if (_isDay)
        {
            // Is day
            if (_sun.intensity > 0)
                // Decrease sun intensity
                _sun.intensity -= Time.deltaTime / _periodMod;
            // Is night
            else
            {
                // disable light
                _sun.intensity = 0;
                // Set that is night
                _isDay = false;
            }
        }
        // Is night
        else
        {
            // Is night
            if (_sun.intensity < 1)
                // Increase sun intensity
                _sun.intensity += Time.deltaTime / _periodMod;
            // Is night
            else
            {
                // enable light
                _sun.intensity = 1;
                // Set that is day
                _isDay = true;
            }
        }
        // Set proper skybox
        if (_sun.intensity < _weakLight)
            // Set night skybox
            RenderSettings.skybox = _nightSkybox;
        // Set day skybox
        else
            RenderSettings.skybox = _daySkybox;
        // Midnight
        if (_sun.intensity >= _noLight && _sun.intensity <= _poorLight)
            // Set proper lightmaps
            AdaptLightmaps(PeriodDatabase.Midnight);
        // Evening
        else if (_sun.intensity > _poorLight && _sun.intensity <= _weakLight)
            // Set proper lightmaps
            AdaptLightmaps(PeriodDatabase.Evening);
        // Afternoon
        else if (_sun.intensity > _weakLight && _sun.intensity <= _averageLight)
            // Set proper lightmaps
            AdaptLightmaps(PeriodDatabase.Afternoon);
        // Morning
        else if (_sun.intensity > _averageLight && _sun.intensity <= _strongLight)
            // Set proper lightmaps
            AdaptLightmaps(PeriodDatabase.Morning);
        // Noon
        else if (_sun.intensity > _strongLight && _sun.intensity <= _mightyLight)
            // Set proper lightmaps
            AdaptLightmaps(PeriodDatabase.Noon);
        // Enable fire
        if (_sun.intensity >= _noLight && _sun.intensity <= _poorLight)
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
    }

    // Adapt lightmaps
    private void AdaptLightmaps(string period)
    {
        // Get proper lightmaps color
        _lightMaps[0].lightmapColor = PeriodDatabase.GetProperPeriod(period).Color;
        // Get proper lightmaps direction
        _lightMaps[0].lightmapDir = PeriodDatabase.GetProperPeriod(period).Direction;
        // Set new lightmaps
        LightmapSettings.lightmaps = _lightMaps;
        // Set new reflection
        RenderSettings.customReflection = PeriodDatabase.GetProperPeriod(period).Reflection;
    }
}