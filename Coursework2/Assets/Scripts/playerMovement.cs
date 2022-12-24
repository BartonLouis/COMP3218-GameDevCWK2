using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;


    private float horizontal = 0f;
    private bool jump = false;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void onLanding() {
        animator.SetBool("Jump", false);
    }
}
