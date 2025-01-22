using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private float _repairRate = 2f;
    [SerializeField] private float _moveDistance = 5f;
    [SerializeField] private Unit _prefab;
    [SerializeField] private Transform[] _spawnPoints;

    private WaitForSeconds _wait;
    private UnitPool _unitPool;
    private bool _enabled = true;

    private void Awake()
    {
        _unitPool = new UnitPool(_prefab, _poolCapacity, _poolMaxSize);
    }

    private Transform GetSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }

    private Vector3 GetRandomDirection(Transform spawnPoint)
    {
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
        return spawnPoint.position + direction * _moveDistance;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_repairRate);
        StartCoroutine(SpawnDelay());
    }

    private void SpawnEnemy()
    {
        if (_unitPool.CountAll < _poolMaxSize)
        {
            Unit enemy = _unitPool.GetUnit();
            Transform spawnPoint = GetSpawnPoint();
            enemy.transform.position = spawnPoint.position;

            Vector3 randomDestination = GetRandomDirection(spawnPoint);
            enemy.InitializeDestination(randomDestination);
        }
    }

    private IEnumerator SpawnDelay()
    {
        while (_enabled)
        {
            yield return _wait;
            SpawnEnemy();
        }
    }
}


