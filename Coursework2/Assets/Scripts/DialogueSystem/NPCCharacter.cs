using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : NPCBaseClass
{

    private bool canInteract = false;
    public Collider NPCCollider;

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
