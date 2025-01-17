using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _targetPositionError = 0.5f;
    private Vector3 _destination;
    

    public void InitializeDestination(Vector3 destination)
    {
        _destination = destination;
    }

    private void Update()
    {
        Vector3 direction = (_destination - transform.position).normalized;

        if (Vector3.Distance(transform.position, _destination) > _targetPositionError)
        {
            transform.position += direction * _speed * Time.deltaTime;
        }
        else
        {
            transform.position = _destination;
        }
    }
}
