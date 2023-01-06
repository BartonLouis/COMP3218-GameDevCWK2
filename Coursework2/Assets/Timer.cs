using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public float startTime = 600f;
    public TextMeshProUGUI text;
    private float timeRemaining;
    private bool hasFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinished) {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0) {
                hasFinished = true;
                StoryEngine.current.TriggerEvent("TimerRanOut");
            }
            text.text = "" + Mathf.FloorToInt(timeRemaining / 60) + ":" + Mathf.FloorToInt(timeRemaining % 60);
        }
    }
}
