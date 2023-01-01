using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;
    public CharacterController2D controller;
    public Animator animator;
    public BoxCollider2D attackCollider;

    public AudioSource landAudio;
    public AudioSource backgroundMusic;
    public AudioSource attackAudio;

    public int maxHealth = 100;
    public int health = 0;
    public int levelToLoad = 1;

    public HealthBar healthBar;

    public float runSpeed = 40f;

    private void Awake() {
        current = this;
    }

    private float horizontal = 0f;
    private bool jump = false;
    private bool grounded = true;
    private bool attacking = false;
    private bool jumpedThisUpdate = false;
    private bool hideableObjectClose = false;
    private bool hidden = false;

    private bool canDoubleJump = false;
    private bool canAttack = false;
    private bool canWallClimb = false;
    private bool canHide = false;
    
    void Start()
    {
        backgroundMusic.Play();
        ViolentMode();
        attackCollider.enabled = false;
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }

    public void PacifistMode() {
        Debug.Log("Going Pacifist Mode");
        canDoubleJump = false;
        canAttack = false;
        canWallClimb = true;
        canHide = true;
        controller.changeSettings(canDoubleJump, canWallClimb);
    }

    public void ViolentMode() {
        Debug.Log("Going Violent Mode");
        canDoubleJump = true;
        canAttack = true;
        canWallClimb = false;
        canHide = false;
        controller.changeSettings(canDoubleJump, canWallClimb);
    }

    public void Damage(int amount) {
        health -= amount;
        if (health < 0) {
            health = 0;
        }
        if (health == 0) {
            OnDeath();
        }
        healthBar.SetHealth(health);
        animator.SetBool("GettingHit", true);
    }

    private void OnDeath() {
        animator.SetBool("Death", true);
        SceneController.current.PlayerDied();
    }

    private void DeathAnimationComplete() {
        animator.SetBool("Dead", true);
    }

    public void StopGettingHit() {
        animator.SetBool("GettingHit", false);
        if (health == 0) {
            animator.SetBool("Death", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Damage(10);
        }
        if (health == 0) {
            return;
        }
        if (!hidden) {
            if (!attacking || !grounded) {
                horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
            } else {
                horizontal = 0f;
            }
            if (canAttack && Input.GetButtonDown("Attack")) {
                horizontal = 0f;
                attacking = true;
                animator.SetBool("Attacking", true);
            }
            if (Input.GetButtonDown("Jump")) {
                jump = true;
                jumpedThisUpdate = true;
                animator.SetBool("Jump", true);
            } else if (Input.GetButtonUp("Jump")) {
                jump = false;
            }
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
        } else {
            horizontal = 0f;
            jump = false;
            jumpedThisUpdate = false;
        }

        if (canHide && hideableObjectClose && Input.GetButtonDown("Interact") && !hidden) {
            hidden = true;
            animator.SetBool("Hiding", true);
        } else if (Input.GetButtonDown("Interact") && hidden) {
            hidden = false;
            animator.SetBool("Hiding", false);
        }

        healthBar.SetPosition(transform.position);
    }

    private void FixedUpdate() {
        if (health > 0) {
            controller.Move(horizontal * Time.fixedDeltaTime, jump, jumpedThisUpdate);
            jumpedThisUpdate = false;
        } else {
            controller.Move(0, false, false);
        }
    }

    public void OnLanding() {
        animator.SetBool("Jump", false);
        jump = false;
        grounded = true;
        landAudio.Play();
    }

    public void OnJump() {
        grounded = false;
        animator.SetBool("Jump", true);
    }

    public void OnClimbStart() {
        animator.SetFloat("Speed", 1f);
    }

    public void OnClimbStop() {
        animator.SetFloat("Speed", 1f);
    }

    public void SetHiding(bool canHide) {
        this.hideableObjectClose = canHide;
    }

    public void OnAttackStart() {
        attackAudio.Play();
        attackCollider.enabled = true;
    }

    public void OnAttackFinish() {
        attacking = false;
        animator.SetBool("Attacking", false);
        attackCollider.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("LoadNextScene")){
            //SceneManager.LoadScene(1);
            SceneManager.LoadScene(levelToLoad);
        } 
    }

}
