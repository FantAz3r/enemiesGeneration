using UnityEngine;
using UnityEngine.Pool;

public class UnitPool
{
    private ObjectPool<Unit> _pool;

    public UnitPool(Unit prefab, int poolCapacity, int poolMaxSize)
    {
        _pool = new ObjectPool<Unit>(
            createFunc: () => Object.Instantiate(prefab),
            actionOnGet: (unit) => ActionOnGet(unit),
            actionOnRelease: (unit) => ActionOnRelease(unit),
            actionOnDestroy: (unit) => Object.Destroy(unit),
            collectionCheck: true,
            defaultCapacity: poolCapacity,
            maxSize: poolMaxSize);
    }

    public Unit GetUnit()
    {
        return _pool.Get();
    }

    public void ReleaseUnit(Unit unit)
    {
        _pool.Release(unit);
    }

    private void ActionOnGet(Unit unit)
    {
        unit.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Unit unit)
    {
        unit.gameObject.SetActive(false);
        unit.transform.position = Vector3.zero;
    }

    public int CountAll => _pool.CountAll; 
}
