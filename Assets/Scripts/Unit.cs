using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _reach≈argetError = 0.3f;

    private Target _target;
    private UnitPool _unitPool;

    public void Initialize(Target target, UnitPool unitPool)
    {
        _target = target;
        _unitPool = unitPool;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.transform.position - transform.position).normalized; 
            transform.position += direction * _speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.transform.position) < _reach≈argetError)
            {
                _unitPool.ReleaseUnit(this);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _unitPool.ReleaseUnit(this);
        }
    }
}
