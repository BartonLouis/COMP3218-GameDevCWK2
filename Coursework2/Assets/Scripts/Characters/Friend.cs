using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Character
{

    public Animator animator;
    public float maxDistance = 100f;
    public Vector3 offset;
    private Rigidbody2D rb;

    private void Start() {
        target = Player.current.transform;
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        GameObject worldCanvas = GameObject.Find("WorldCanvas");
        healthBar = Instantiate(healthBarPrefab, worldCanvas.transform).GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogic();
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance > maxDistance) {
            transform.position = target.position - offset;
        }
        animator.SetBool("Walking", Mathf.Abs(rb.velocity.x) > 0.01);
    }

    protected override void StopChasingTarget() {
        ChaseTarget();
    }

    protected override void ChaseTarget() {
        if (target.position.x < transform.position.x) {
            controller.FaceLeft();
            direction = -1f;
        } else {
            controller.FaceRight();
            direction = 1f;
        }
        float distance = Mathf.Abs(target.position.x - transform.position.x);
        if (distance < targetDistance) {
            OnTargetReached();
            reached = true;
        }
        // Check that can move
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundAheadCheck.position, groundRadius, whatIsGround);
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(wallAheadCheck.position, groundRadius, whatIsGround);
        if (colliders.Length > 0 && colliders2.Length==0) {
            chasing = true;
        } else {
            chasing = false;
            reached = true;
        }
    }
}
