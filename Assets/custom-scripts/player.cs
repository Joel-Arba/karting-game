using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
  public float BestLapTime { get; private set; } = Mathf.Infinity;

  public float LastLapTime { get; private set; } = 0;
  public float CurrentLapTime { get; private set; } = 0;
  public int CurrentLap { get; private set; } = 0;

  private float lapTimerTimestamp;
  private int lastCheckpointPassed = 0;

  private Transform checkpointsParent;
  private int checkpointCount;
  private int checkpointLayer;

    public GameObject lapObject;
    public Transform lapNumberPanel;
    public Transform lapTimePanel;

    public float spacing = 10f;
    public float numberOfLaps = 1;
    public GameObject lapTimes;
    public GameObject racePanel;
    public GameObject mobileUI;
    private float  tempLapTime;

  // Start is called before the first frame update
  void Awake() {

    checkpointsParent = GameObject.Find("Checkpoints").transform;
    checkpointCount = checkpointsParent.childCount;
    checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        // carController = GetComponent<carController>();

        numberOfLaps = GameManager.instance.GetLapNumber();
    }

  void StartLap()
    {
        if(tempLapTime != LastLapTime )
        {
            tempLapTime = LastLapTime;
        }
        else
        {
            tempLapTime = LastLapTime = 0;
        }
        
        AddLapTimeEntry(tempLapTime, CurrentLap);
        Debug.Log("StartLap!");
    CurrentLap++;
    lastCheckpointPassed = 1;
    lapTimerTimestamp = Time.time;

        if (CurrentLap == numberOfLaps + 1)
        {
            CurrentLap--;
            Time.timeScale = 0;
            racePanel.SetActive(false);
            lapTimes.SetActive(true);

#if UNITY_ANDROID || UNITY_IOS
            mobileUI.SetActive(false);
#endif
        }

    }

  void EndLap() {

    LastLapTime = Time.time - lapTimerTimestamp;
    BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    Debug.Log("Lap time was " + LastLapTime);
    Debug.Log("Best time is " + BestLapTime);
        
  }

  void OnTriggerEnter(Collider collider) {
    if(collider.gameObject.layer == checkpointLayer) {
      // only care about colliding with the layer that has checkpoints
      if(collider.gameObject.name == "1") {
        // crossed the finish line
        if(lastCheckpointPassed == checkpointCount)
          EndLap();
        // regardless of how the lap is finished, a new lap should start
        StartLap();
      }
      // any of the other checkpoints, make sure they're in order
      else if(collider.gameObject.name == (lastCheckpointPassed+1).ToString())
        lastCheckpointPassed++;
    }
  }

  // Update is called once per frame
  void Update() {
    CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

    }

    public void AddLapTimeEntry(float lapTime, int lapNumber)
    {
        if (lapNumber == 0)
        {
            return;
        }

        GameObject lapNumEntry = Instantiate(lapObject, lapNumberPanel);
        GameObject lapTimeEntry = Instantiate(lapObject, lapTimePanel);

        TMP_Text lapNumText = lapNumEntry.GetComponentInChildren<TMP_Text>();
        TMP_Text lapTimeText = lapTimeEntry.GetComponentInChildren<TMP_Text>();
        if(lapTime.ToString("F2") == "0.00")
        {
            lapNumText.text = "Lap " + lapNumber;
            lapTimeText.text ="DNF";
        }
        else
        {
            lapNumText.text = "Lap " + lapNumber;
            lapTimeText.text = lapTime.ToString("F2") + " seconds";
        }

        //int lapCount = lapNumberPanel.childCount;
        /*
        int lapCount = lapNumberPanel.childCount;

        // Adjust the position of the lapTimeEntry object
        RectTransform lapTimeEntryRectTransform = lapNumEntry.GetComponent<RectTransform>();
        Vector3 entryPosition = new Vector3(0, -lapCount * (lapTimeEntryRectTransform.rect.height + spacing), 0);
        entryPosition.y += 20f; // Move the entry higher
        lapTimeEntryRectTransform.anchoredPosition = entryPosition;


        // Adjust the scroll rect's content size to fit the new entry
        RectTransform contentRect = lapNumberPanel.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, (lapCount + 1) * (lapNumEntry.GetComponent<RectTransform>().rect.height + spacing));
    */

    }


    /*public void ResetCurrentLap()
    {
        CurrentLapTime = 0;
    }*/
}
