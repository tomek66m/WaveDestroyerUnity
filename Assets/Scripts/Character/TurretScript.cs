using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject _TurretPrefab;
    public List<GameObject> _TurretList;

    private int _ActiveTurretsCount = 0;

    public TextMeshProUGUI _turretCounterText;

    private bool _PlayerPrefHUDDecreased;

    public GameObject _UI_CompletedWavesResult;

    public GameObject _UI_TurretsNumber;


    // Start is called before the first frame update
    void Start()
    {
        // HUD decreasing
        int _temp_PP_HUD_Decreased = PlayerPrefs.GetInt("Decreased_HUD");
        if (_temp_PP_HUD_Decreased == 0)
            _PlayerPrefHUDDecreased = false;
        else
            _PlayerPrefHUDDecreased = true;

        if(_PlayerPrefHUDDecreased)
        {
            _UI_CompletedWavesResult.SetActive(false);
            _UI_TurretsNumber.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        int tempTurretsCount = 0;
        foreach(var x in _TurretList)
        {
            if(x.transform.GetChild(1).gameObject.active == true)
            {
                tempTurretsCount++;
            }
        }

        _ActiveTurretsCount = tempTurretsCount;

        _turretCounterText.text = "Postawione wieże: \n" + _ActiveTurretsCount;

    }

    public void SetOnTurretOnSelectedPlatform(GameObject platform)
    {
        GameObject childTurret = platform.transform.GetChild(1).gameObject;
        childTurret.SetActive(true);
    }
    // TODO next sprint
}
