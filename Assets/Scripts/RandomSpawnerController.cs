using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class RandomSpawnerController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _minSpawnTime = 2f;
    [SerializeField] private float _maxSpawnTime = 10f;

    private WorldController _worldController;
    private (float xMin, float xMax) _worldBoundaries; 
    private (float xMin, float xMax) _houseBoundaries;
    private List<(float xMin, float xMax)> _spawnAreas = new List<(float xMin, float xMax)>();

    // Start is called before the first frame update
    void Start()
    {
        _worldController = FindObjectOfType<WorldController>();
        _worldBoundaries = _worldController.GetWorldBoundaries();
        _houseBoundaries = _worldController.GetHouseBoundaries();

        var leftSideMin = _worldBoundaries.xMin;
        var leftSideMax = _houseBoundaries.xMin;
        var rightSideMin = _houseBoundaries.xMax;
        var rightSideMax = _worldBoundaries.xMax;
        
        _spawnAreas.Add((xMin: leftSideMin, xMax: leftSideMax));
        _spawnAreas.Add((xMin: rightSideMin, xMax: rightSideMax));

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
        var enemyIndex = Random.Range(0, _enemies.Count);

        var spawnAreaIndex = Random.Range(0, _spawnAreas.Count);
        var spawnArea = _spawnAreas[spawnAreaIndex];
        var randomX = Random.Range(spawnArea.xMin, spawnArea.xMax);
        
        Instantiate(_enemies[enemyIndex], new Vector3(randomX, -1.552f, 0), transform.rotation);
        StartCoroutine(Spawn());
    }
}
