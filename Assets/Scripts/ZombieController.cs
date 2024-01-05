using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    [SerializeField] private float hp = 100f;
    public float Hp {
        get { return hp; }
        set { hp = value; }
    }

    public float speed = 1f;
    private bool isAttack = false;
    private bool isAttackCooldown = false;
    [SerializeField] private bool isDead = false;

    public bool IsDead { get { return isDead; } }

    private GameObject Player;
    private  Animator Animator;
    private GameController gameController;
    private PlayerMovement playerController;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Animator = GetComponent<Animator>();
        playerController = Player.GetComponent<PlayerMovement>();
        
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
                playerController.PlayerTakeDamage(5);
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

    public void ZombieTakeDamage(float damage) {
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
