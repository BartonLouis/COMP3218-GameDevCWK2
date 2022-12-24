using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEventColliderScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Triggering test event");
        StoryEngine.current.TestEventOccured();
    }
}
