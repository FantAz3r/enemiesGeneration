using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private float _repairRate = 2f; 
    [SerializeField] private float _moveDistance = 5f;
    [SerializeField] private Unit _prefab; 
    [SerializeField] private Transform[] _spawnPoints;

    private WaitForSeconds wait;
    private ObjectPool<Unit> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Unit>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (unit) => ActionOnGet(unit),
            actionOnRelease: (unit) => ActionOnRelease(unit),
            actionOnDestroy: (unit) => Destroy(unit),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private Transform GetSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }

    private Vector3 GetRandomDirection(Transform spawnPoint)
    {
        float minAngle = 0f;
        float maxAngle = 360f;
        float randomAngle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));

        return spawnPoint.position + direction * _moveDistance;
    }

    void Start()
    {
        wait = new WaitForSeconds(_repairRate); 
        StartCoroutine(SwawnDelay());
    }

    private void SpawnEnemy()
    {
        if (_pool.CountAll < _poolMaxSize)
        {
            Unit enemy = _pool.Get();
            Transform spawnPoint = GetSpawnPoint();
            enemy.transform.position = spawnPoint.position;

            Vector3 randomDestination = GetRandomDirection(spawnPoint);
            enemy.InitializeDestination(randomDestination);
        }
    }

    private IEnumerator SwawnDelay()
    {
        while (true) 
        {
            yield return wait;
            SpawnEnemy();
        }
    }

    private void ActionOnGet(Unit unit)
    {
        unit.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Unit unit)
    {
        unit.gameObject.SetActive(false);
    }
}

