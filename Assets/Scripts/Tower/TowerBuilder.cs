using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerBuilder : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private int _levelCount;
    [SerializeField] private GameObject _beam;
    [SerializeField] private float _additionalTowerScaleUp;
    [Header("Platforms")]
    [SerializeField] private StartPlatform _startPlatform;
    [SerializeField] private FinishPlatform _finishPlatform;
    [SerializeField] private Platform[] _platform;
    
    private float _startAndFinishAdditionalScale = 0.5f;
    private float BeamScale => _levelCount / 2f + _startAndFinishAdditionalScale+_additionalTowerScaleUp/2f;
   
    private void Awake()
    {
        Build();   
    }

    private void Build()
    {
        GameObject beam = Instantiate(_beam, transform);
        beam.transform.localScale = new Vector3(1, BeamScale, 1);

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y-_additionalTowerScaleUp;

        SpawnPlatforms(_startPlatform, ref spawnPosition, beam.transform);
        for (int i = 0; i < _levelCount; i++)
        {
            SpawnPlatforms(_platform[Random.Range(0, _platform.Length)], ref spawnPosition, beam.transform);
        }
        SpawnPlatforms(_finishPlatform, ref spawnPosition, beam.transform);
    }

    private void SpawnPlatforms(Platform platform,ref Vector3 spawnPosition, Transform parent)
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(platform, spawnPosition, rotation, parent);
        spawnPosition.y -= 1;
    }
}
