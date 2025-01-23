using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Vector3 _direction;
    private UnitPool _unitPool;

    public void InitializeDirection(Vector3 direction, UnitPool unitPool)
    {
        _direction = direction;
        _unitPool = unitPool; 
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _unitPool.ReleaseUnit(this);
        }
    }
}
