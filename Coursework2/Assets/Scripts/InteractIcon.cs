using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    public static InteractIcon current;
    public Animator animator;

    private GameObject boundObject;
    private void Awake() {
        current = this;
    }

    public void Bind(GameObject o) {
        boundObject = o;
        animator.SetBool("Bound", true);
    }

    public void UnBind() {
        boundObject = null;
        animator.SetBool("Bound", false);
    }

    public void Update() {
        if (boundObject != null) {
            transform.position = boundObject.transform.position;
        }
    }
}
