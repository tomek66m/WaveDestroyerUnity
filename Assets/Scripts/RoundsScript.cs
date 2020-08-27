using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundsScript : MonoBehaviour
{

    private int _currentRound=0;
    public static float _killedMinions=0;
    public TextMeshProUGUI _wavetSummaryText;

    public GameObject _completedWavesResult;

    private MonsterSpawner _monsterSpawnersScript;
    public GameObject _gateModel1_SpawnerObject;

    private float _gameTime = 0.0f;
    public TextMeshProUGUI _timeText;
    private List<int> _roundsMobs;
    public TextMeshProUGUI _nextWaveInfo;
    public TextMeshProUGUI _gameTimeText;
    public static float _roundProgress = 0.0f;
    public float _maxProgress;
    public TextMeshProUGUI _progressText;

    private float _saveScoreTimer = 0.0f;

    public TextMeshProUGUI ScoreText;


    private void Awake()
    {
        _killedMinions = 0;
        _currentRound = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _completedWavesResult.SetActive(true);
        _roundsMobs = new List<int> { 5, 10, 15 };

        _maxProgress = 30;

        _monsterSpawnersScript=_gateModel1_SpawnerObject.GetComponent<MonsterSpawner>();

        if (!PlayerPrefs.HasKey("ScoreKilled1"))
        {
            PlayerPrefs.SetFloat("ScoreKilled1", -1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("real: " + _killedMinions);
        Debug.Log("pref: " + PlayerPrefs.GetFloat("ScoreKilled"));
        _roundProgress = _killedMinions / _maxProgress * 100.0f;
        _progressText.text = "Postep: " + System.Math.Round(_roundProgress, 0) + "%";

        _gameTime += Time.deltaTime;
        _completedWavesResult.SetActive(true);

        if(_monsterSpawnersScript._producedMinions < _roundsMobs[_currentRound])
        {
            _monsterSpawnersScript._shouldSpawn = true;
        }
        else
        {
            _monsterSpawnersScript._shouldSpawn = false;
        }


        if(_monsterSpawnersScript._shouldSpawn == false)
        {
            _nextWaveInfo.gameObject.SetActive(true);
            if (_currentRound != 2)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    _monsterSpawnersScript._producedMinions = 0;
                        _currentRound++;
                        _monsterSpawnersScript._shouldSpawn = true;
                        _nextWaveInfo.gameObject.SetActive(false);
                }
            }

            if(_currentRound == 2)
                _nextWaveInfo.gameObject.SetActive(false);
        }


        _wavetSummaryText.text = "Fala: " + (_currentRound + 1) +
        "\nZabite: " + _killedMinions;
        _gameTimeText.text = "Czas: " + System.Math.Round(_gameTime, 0) + "s";

        _saveScoreTimer += Time.deltaTime;


        _saveScoreTimer = 0.0f;


         if (PlayerPrefs.GetFloat("ScoreKilled1") < _killedMinions)
         {
              PlayerPrefs.SetFloat("ScoreKilled1", _killedMinions);
         }


        if (Input.GetKey(KeyCode.Tab))
            ScoreText.gameObject.SetActive(true);
        else
            ScoreText.gameObject.SetActive(false);

        ScoreText.text = "Najlepszy wynik: " + PlayerPrefs.GetFloat("ScoreKilled1") + " zabitych";

    }

    void ShowTableScore()
    {

    }
}
