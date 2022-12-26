using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerMovement.current.SetHiding(true);
            InteractIcon.current.Bind(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerMovement.current.SetHiding(false);
            InteractIcon.current.UnBind();
        }
    }
}
