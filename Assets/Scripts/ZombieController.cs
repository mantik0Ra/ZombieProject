using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    private static float hp = 100f;
    public static float Hp {
        get { return hp; }
        set { hp = value; }
    }

    public float speed = 1f;
    private bool isAttack = false;
    private bool isAttackCooldown = false;
    private static bool isDead = false;

    private GameObject Player;
    private static Animator Animator;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead) {
            if (!isAttack) {
                Vector3 delta = Player.transform.position - transform.position;
                delta.Normalize();
                // transform.position = new Vector3(transform.position.x, 0, transform.position.z) + (delta * speed * Time.deltaTime);
                transform.position += (delta * speed * Time.deltaTime);
                transform.forward = delta;
            }
            if (isAttack && !isAttackCooldown) {
                isAttackCooldown = true;
                PlayerMovement.PlayerTakeDamage(5);
                StartCoroutine(CooldownAttack());
            }
        } else {
            StartCoroutine(RemoveBodyAfterDead());
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            isAttack = true;
            Animator.SetBool("IsAttack", true);
            
        }
    }

    private IEnumerator CooldownAttack() {
        yield return new WaitForSeconds(1f);
        isAttackCooldown = false;
    }

    public static void ZombieTakeDamage(float damage) {
        Hp -= damage;

        if(Hp <= 0) {
            Animator.SetBool("IsDead", true);
            isDead = true;
        }
    }

    private IEnumerator RemoveBodyAfterDead() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    
}
