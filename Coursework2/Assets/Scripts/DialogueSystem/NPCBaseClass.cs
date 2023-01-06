using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCBaseClass : MonoBehaviour
{
    public AudioSource greetingAudio;

    public String characterName;


    private Dialogue currentDialogue;
    private bool canInteract = false;

    protected string playerPath = "Pacifist";

    protected virtual void Start() {
        StoryEngine.current.EventOccured += this.EventHappened;
        // Check which events have occured and set correct dialogue accordingly.
        string[] sentences = { "sentence 1", "sentence 2" };
        SetDialogue(new Dialogue(sentences, "default"));
    }

    public void TriggerDialogue() {
        DialogueManager.current.StartDialogue(currentDialogue);
        greetingAudio.Play();
        StoryEngine.current.TriggerEvent("TalkedTo" + characterName);
    }

    protected virtual void EventHappened(String eventType) {
        // Implement logic for figuring out which dialogue to present next based on current state and the event which occured
        if (eventType == "PacifistChoice") {
            playerPath = "Pacifist";
        } else if (eventType == "ViolentChoice") {
            playerPath = "Violent";
        }
    }

    protected void SetDialogue(Dialogue dialogue) {
        currentDialogue = dialogue;
    }

    private void Update() {
        if (canInteract && Input.GetButtonDown("Interact")) {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canInteract = true;
            InteractIcon.current.Bind(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canInteract = false;
            InteractIcon.current.UnBind();
        }
    }
}