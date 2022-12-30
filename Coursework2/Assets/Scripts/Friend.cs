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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogic();
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance > maxDistance) {
            transform.position = target.position - offset;
        }
        animator.SetBool("Walking", Mathf.Abs(rb.velocity.x) > 0);
    }
}
