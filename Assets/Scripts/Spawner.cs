using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private float _repairRate = 2f;

    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Mesh[] _unitMeshes;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Target[] _targets;

    private WaitForSeconds _wait;
    private UnitPool _unitPool;
    private bool _enabled = true;

    private void Awake()
    {
        _unitPool = new UnitPool(_unitPrefab, _poolCapacity, _poolMaxSize); 
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_repairRate);
        StartCoroutine(SpawnDelay());
    }

    private Transform GetSpawnPoint(out int spawnPointIndex)
    {
        spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[spawnPointIndex];
    }

    public void SpawnUnit()
    {

        if (_unitPool.CountAll < _poolMaxSize)
        {
            Unit unit = _unitPool.GetUnit();
            Transform spawnPoint = GetSpawnPoint(out int spawnPointIndex);
            MeshRenderer unitRenderer = unit.GetComponent<MeshRenderer>();
            unit.transform.position = spawnPoint.position;

            if (unitRenderer != null && spawnPointIndex < _unitMeshes.Length)
            {
                unitRenderer.material = new Material(unitRenderer.material); 
                unit.GetComponent<MeshFilter>().mesh = _unitMeshes[spawnPointIndex]; 
            }

            unit.Initialize(_targets[spawnPointIndex], _unitPool);
        }
    }

    private IEnumerator SpawnDelay()
    {
        while (_enabled)
        {
            yield return _wait;
            SpawnUnit();
        }
    }
}



