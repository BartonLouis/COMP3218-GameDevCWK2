using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : NPCBaseClass
{
    public Dialogue d1;
    public Dialogue d2;
    private void Start() {
        StoryEngine.current.EventOccured += this.EventHeppened;
        SetDialogue(d1);
    }

    private void EventHeppened(StoryEngine.EventType eventType) {
        if (eventType == StoryEngine.EventType.Button1Clicked) {
            SetDialogue(d2);
        }
    }

}
