using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float speed = 2f;

    private float _targetError = 0.3f;
    private int currentPointIndex = 0;
    private bool _isEnebled = true;

    private void Start()
    {
        transform.position = _wayPoints[currentPointIndex].position; 
        StartCoroutine(FollowPoints());
    }

    private IEnumerator FollowPoints()
    {
        while (_isEnebled) 
        {
            while (Vector3.Distance(transform.position, _wayPoints[currentPointIndex].position) > _targetError)
            {
                transform.position = Vector3.MoveTowards(transform.position, _wayPoints[currentPointIndex].position, speed * Time.deltaTime);
                yield return null;
            }

            currentPointIndex = (currentPointIndex + 1) % _wayPoints.Length; 
        }
    }
}
