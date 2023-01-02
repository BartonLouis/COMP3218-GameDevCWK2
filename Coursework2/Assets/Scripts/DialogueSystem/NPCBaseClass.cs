using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseClass : MonoBehaviour
{
    private Dialogue currentDialogue;
    public AudioSource greetingAudio;

    public void TriggerDialogue() {
        DialogueManager.current.StartDialogue(currentDialogue);
        greetingAudio.Play();
    }

    protected void SetDialogue(Dialogue dialogue) {
        currentDialogue = dialogue;
    }
}