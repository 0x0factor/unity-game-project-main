using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameProject.ProjectAssets.Control.BarracksMenu;

public class CountdownTimer : MonoBehaviour
{
    private SpawnTangible spawnTangible;

    private bool timerIsRunning = false;

    private float duration = 0f;
    private Text timeText = null;

    private GameObject currentBarracks;
    public bool isUnit;

    private void Awake()
    {
        spawnTangible = GetComponent<SpawnTangible>();
    }

    //setter
    public void SetTimerRunning(bool timerBool)
    {
        timerIsRunning = timerBool;
    }

    //setter
    public void SetDurationaAndTextObject(float _duration, Text _timeText)
    {
        duration = _duration;
        timeText = _timeText;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                DisplayTime(duration, timeText);
            }
            else
            {
                //method call to spawn blueprint counterpart
                spawnTangible.SpawnActualPrefab(gameObject);
                Debug.Log("Building/training has finished!");
                duration = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float duration, Text timeText)
    {
        duration += 1.0f;
        float minutes = Mathf.FloorToInt(duration / 60);
        float seconds = Mathf.FloorToInt(duration % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnDestroy()
    {
        if (isUnit)
        {
            if (currentBarracks != null)
            {
                Barracks barracks = currentBarracks.GetComponent<Barracks>();
                barracks.SetCurrentlyBuilding(false);
            }
        }
    }

    public void SetCurrentBarracks(GameObject _currentBarracks)
    {
        currentBarracks = _currentBarracks;
    }
}