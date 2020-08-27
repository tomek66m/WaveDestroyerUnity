using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionCasterAttackScript : MonoBehaviour
{
    public GameObject _attackBulletPrefab;
    public GameObject _shootingPoint;
    public float _damage = 10.0f;
    public bool _shouldAttacking = false;
    public List<Collider> _allColliders;
    private GameObject _shootingTarget;
    private float _timer = 2.0f;
    private float _delay = 2.0f;
    public List<GameObject> _bulletsList;
    private float _bulletSpeed = 400.0f;

    private Collider[] _collidersList;
    // Start is called before the first frame update
    void Start()
    {
        _allColliders = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldAttacking)
        {
            ChoseEnemy();
            if(_timer <= _delay)
                _timer += Time.deltaTime;
            else
            {
                _timer = 0.0f;
                Attack();
            }
        }

        ManageAttacks();

    }

    void ChoseEnemy()
    {
        if(_allColliders.Count != 0)
        {
            _shootingTarget = _allColliders[0].gameObject;
            foreach (var _target in _allColliders)
            {
                if(Vector3.Distance(gameObject.transform.position, _shootingTarget.transform.position) >
                    Vector3.Distance(gameObject.transform.position, _target.transform.position))
                {
                    _shootingTarget = _target.gameObject;
                }
            }
        }
    }
    void Attack()
    {
        var tempBullet = Instantiate(_attackBulletPrefab, _shootingPoint.transform.position, Quaternion.identity);
        _bulletsList.Add(tempBullet);
    }
    void ManageAttacks()
    {

        // TODO exception
        foreach(var x in _bulletsList.ToList())
        {
            if(x!=null)
            {
                if (Vector3.Distance(x.transform.position, _shootingTarget.transform.position) < 10.0f)
                {
                    if (x != null)
                    {
                        Destroy(x);
                        _bulletsList.Remove(x);
                    }
                }
                else
                {
                    x.transform.position = Vector3.MoveTowards(x.transform.position,
                        _shootingTarget.transform.position, _bulletSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Crystal")
        {
            _shouldAttacking = true;
            if(!_allColliders.Contains(collision))
                _allColliders.Add(collision);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        _shouldAttacking = false;
        if (_allColliders.Contains(collision))
            _allColliders.Remove(collision);
    }
}
