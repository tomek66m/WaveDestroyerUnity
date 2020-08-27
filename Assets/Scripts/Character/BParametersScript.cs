using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BParametersScript : MonoBehaviour
{
    public Vector3 _Direction;
    private float _BulletSpeed = 800.0f;
    private float _MaxDistance = 400.0f;
    private float _BulletDmg;

    private float _traveledDistance;
    public Vector3 _startPosition;

    void Start()
    {
        _traveledDistance = 0.0f;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        _traveledDistance = Vector3.Distance(_startPosition, transform.position);
        if (_traveledDistance >= _MaxDistance)
        {
            Destroy(gameObject);
        }
    }
    public float getSpeed()
    {
        return _BulletSpeed;
    }

}
