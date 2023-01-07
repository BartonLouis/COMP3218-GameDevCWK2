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
            string minutes = Mathf.FloorToInt(timeRemaining / 60).ToString();
            string seconds = Mathf.FloorToInt(timeRemaining % 60).ToString();
            if (minutes.Length == 1) {
                minutes = "0" + minutes;
            }
            if (seconds.Length == 1) {
                seconds = "0" + seconds;
            }
            text.text = minutes + ":" + seconds;
        }
    }
}
