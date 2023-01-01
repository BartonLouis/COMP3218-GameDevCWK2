using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StoryEngine : MonoBehaviour
{

    public enum EventType
    {
        DefendedFriend,
        HidFromMonster,
        TestEvent
    }

    public static StoryEngine current;

    public event Action<EventType> EventOccured;
    private static List<EventType> OccuredEvents = new();
    private List<EventType> OccuredEventsSinceCheckpoint;

    private void Awake() {
        StoryEngine.current = this;
    }

    private void Start() {
        OccuredEventsSinceCheckpoint = new List<EventType>();
    }

    public void TriggerEvent(EventType eventType) {
        // If event hasn't already occured, add it to the list of events
        if (!OccuredEvents.Contains(eventType) && !OccuredEventsSinceCheckpoint.Contains(eventType)) {
            OccuredEventsSinceCheckpoint.Add(eventType);
        }
        // Trigger the event, so that any listening objects are notified that the event occured
        EventOccured(eventType);
    }

    public bool HasOccured(EventType eventType) {
        return (OccuredEvents.Contains(eventType) || OccuredEventsSinceCheckpoint.Contains(eventType));
    }

    public void TestEventOccured() {
        TriggerEvent(EventType.TestEvent);
    }

    public void RestartFromCheckpoint() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckPoint() {
        foreach (EventType e in OccuredEventsSinceCheckpoint) {
            if (!OccuredEvents.Contains(e)) {
                OccuredEvents.Add(e);
            }
        }
        OccuredEventsSinceCheckpoint = new List<EventType>();
    }
}
