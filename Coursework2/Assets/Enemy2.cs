 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Character
{

    public GameObject bulletPrefab;

    public Animator animator;
    public int damage = 25;
    public Transform firePoint;
    public float bulletVelocity;

    private bool attacking = false;

    private void Start() {
        target = Player.current.transform;
        health = maxHealth;
        GameObject worldCanvas = GameObject.Find("WorldCanvas");
        healthBar = Instantiate(healthBarPrefab, worldCanvas.transform).GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogic();
        if (attacking) {
            reached = true;
        }
    }

    protected override void OnTargetReached() {
        attacking = true;
        animator.SetBool("Attacking", true);
    }

    void Fire() {
        GameObject gameObject = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * bulletVelocity, 0f);
        gameObject.GetComponent<Bullet>().SetDamage(damage);
        animator.SetBool("Attacking", false);
        attacking = false;
    }

    protected override void OnDeath() {
        animator.SetBool("Dead", true);
        gameObject.layer = LayerMask.NameToLayer("NPCLayer");
    }

    public void OnDeathComplete() {
        healthBar.Destroy();
        Destroy(gameObject);
    }
}
