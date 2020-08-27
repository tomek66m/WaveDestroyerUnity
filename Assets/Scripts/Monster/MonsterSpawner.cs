using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject _casterMinionPrefab;
    public GameObject _meleeMinionPrefab;
    public Transform[] _movingWaypoints;

    public List<GameObject> _InstantiatedcasterMinions;
    public List<GameObject> _InstantiatedmeleeMinions;

    private float _casterMinionSpeed = 60.0f;

    private float CasterTimer = 0.0f;

    private float MeleeTimer = 0.0f;

    public int _producedMinions=0;

    public bool _shouldSpawn = true;




    // Start is called before the first frame update
    void Start()
    {
        _InstantiatedcasterMinions = new List<GameObject>();
        _InstantiatedmeleeMinions = new List<GameObject>();




    }

    // Update is called once per frame
    void Update()
    {

        Move();
        if(_shouldSpawn)
        {
            SpawnCasters();
            SpawnMelees();
        }
    }

    void SpawnCasters()
    {
        if (CasterTimer > 3.0f)
        {
            _InstantiatedcasterMinions.Add(Instantiate(_casterMinionPrefab, _movingWaypoints[0].position, _casterMinionPrefab.transform.rotation));
            _producedMinions++;
            foreach (var x in _InstantiatedcasterMinions)
            {
                if(x!=null)
                    x.SetActive(true);
            }
            CasterTimer = 0.0f;
        }
        else
        {
            CasterTimer += Time.deltaTime;
        }
    }

    void SpawnMelees()
    {
        if (MeleeTimer > 5.0f)
        {
            _InstantiatedmeleeMinions.Add(Instantiate(_meleeMinionPrefab, _movingWaypoints[0].position, _meleeMinionPrefab.transform.rotation));
            _producedMinions++;
            foreach (var x in _InstantiatedmeleeMinions)
            {
                if(x!=null)
                    x.SetActive(true);
            }
            MeleeTimer = 0.0f;
        }
        else
        {
            MeleeTimer += Time.deltaTime;
        }
    }

    void Fight()
    {

    }

    void Move()
    {
        foreach(var casterMinion in _InstantiatedcasterMinions)
        {
            // jezeli usuniety
            if(casterMinion == null)
            {
                continue;
            }

            if(casterMinion.gameObject.transform.GetChild(2).GetComponent<MinionCasterAttackScript>()._shouldAttacking == true)
            {

            }
            else
            {
                int currentCasterMinionWaypointToFollow = casterMinion.GetComponent<MinionParameters>()._currentWaypointIndex;

                if (currentCasterMinionWaypointToFollow < _movingWaypoints.Length)
                {
                    if (casterMinion.transform.position == _movingWaypoints[currentCasterMinionWaypointToFollow].transform.position)
                    {
                        casterMinion.GetComponent<MinionParameters>()._currentWaypointIndex++;
                    }

                    // jeżeli jeszcze nie osiągnął waypointa
                    else
                    {
                        casterMinion.transform.position = Vector3.MoveTowards(casterMinion.transform.position,
                            _movingWaypoints[currentCasterMinionWaypointToFollow].transform.position,
                            _casterMinionSpeed * Time.deltaTime);


                    }
                }
            }

        }

        foreach (var meleeMinion in _InstantiatedmeleeMinions)
        {
            if(meleeMinion == null)
            {
                continue;
            }

            if (meleeMinion.gameObject.transform.GetChild(2).GetComponent<MinionCasterAttackScript>()._shouldAttacking == true)
            {

            }
            else
            {
                int currentCasterMinionWaypointToFollow = meleeMinion.GetComponent<MinionParameters>()._currentWaypointIndex;

                if (currentCasterMinionWaypointToFollow < _movingWaypoints.Length)
                {
                    if (meleeMinion.transform.position == _movingWaypoints[currentCasterMinionWaypointToFollow].transform.position)
                    {
                        meleeMinion.GetComponent<MinionParameters>()._currentWaypointIndex++;
                    }

                    // jeżeli jeszcze nie osiągnął waypointa
                    else
                    {
                        meleeMinion.transform.position = Vector3.MoveTowards(meleeMinion.transform.position,
                            _movingWaypoints[currentCasterMinionWaypointToFollow].transform.position,
                            _casterMinionSpeed * Time.deltaTime);

                        // rotacja


                    }
                }
            }

        }
    }


}
