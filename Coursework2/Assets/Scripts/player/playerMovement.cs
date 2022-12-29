using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public static playerMovement current;
    public CharacterController2D controller;
    public Animator animator;

    public AudioSource landAudio;
    public AudioSource backgroundMusic;

    public int levelToLoad = 1;

    public float runSpeed = 40f;

    private void Awake() {
        current = this;
    }

    private float horizontal = 0f;
    private bool jump = false;
    private bool jumpedThisUpdate = false;
    private bool canHide = false;
    private bool hidden = false;
    
    void Start()
    {
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hidden) {
            horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            if (Input.GetButtonDown("Jump")) {
                jump = true;
                jumpedThisUpdate = true;
                animator.SetBool("Jump", true);
            } else if (Input.GetButtonUp("Jump")) {
                jump = false;
            }
        } else {
            horizontal = 0f;
            jump = false;
            jumpedThisUpdate = false;
        }

        if (canHide && Input.GetButtonDown("Interact") && !hidden) {
            hidden = true;
            animator.SetBool("Hiding", true);
        } else if (Input.GetButtonDown("Interact") && hidden) {
            hidden = false;
            animator.SetBool("Hiding", false);
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontal * Time.fixedDeltaTime, jump, jumpedThisUpdate);
        jumpedThisUpdate = false;
    }

    public void OnLanding() {
        animator.SetBool("Jump", false);
        jump = false;
        landAudio.Play();
    }

    public void OnJump() {
        animator.SetBool("Jump", true);
    }

    public void OnClimbStart() {
        animator.SetFloat("Speed", 1f);
    }

    public void OnClimbStop() {
        animator.SetFloat("Speed", 1f);
    }

    public void SetHiding(bool canHide) {
        this.canHide = canHide;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("LoadNextScene")){
            //SceneManager.LoadScene(1);
            Application.LoadLevel(levelToLoad);
        } 
    }

}
