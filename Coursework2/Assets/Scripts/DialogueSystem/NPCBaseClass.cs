using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseClass : MonoBehaviour
{
    private Dialogue currentDialogue;

    public void TriggerDialogue() {
        DialogueManager.current.StartDialogue(currentDialogue);
    }

    protected void SetDialogue(Dialogue dialogue) {
        currentDialogue = dialogue;
    }
}