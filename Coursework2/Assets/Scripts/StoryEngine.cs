using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private List<EventType> OccuredEvents;

    private void Awake() {
        StoryEngine.current = this;
        OccuredEvents = new List<EventType>();
    }

    public void TriggerEvent(EventType eventType) {
        // If event hasn't already occured, add it to the list of events
        if (!OccuredEvents.Contains(eventType)) {
            OccuredEvents.Add(eventType);
        }
        // Trigger the event, so that any listening objects are notified that the event occured
        EventOccured(eventType);
    }

    public bool HasOccured(EventType eventType) {
        return OccuredEvents.Contains(eventType);
    }

    public void TestEventOccured() {
        TriggerEvent(EventType.TestEvent);
    }
}
