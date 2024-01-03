using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    private static float hp = 100f;
    public static float Hp {
        get { return hp; }
        set { hp = value; }
    }

    public float speed = 1f;
    private bool IsAttack = false;
    private GameObject Player;
    private Animator Animator;
    bool isAttackCooldown = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAttack) {
            Vector3 delta = Player.transform.position - transform.position;
            delta.Normalize();
            // transform.position = new Vector3(transform.position.x, 0, transform.position.z) + (delta * speed * Time.deltaTime);
            transform.position += (delta * speed * Time.deltaTime);
            transform.forward = delta;
        }
        if(IsAttack && !isAttackCooldown) {
            isAttackCooldown = true;
            StartCoroutine(Coroutine());

        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            IsAttack = true;
            Animator.SetBool("IsAttack", true);
            
        }
    }

    private IEnumerator Coroutine() {
        yield return new WaitForSeconds(1f);
        PlayerMovement.Hp -= 5f;
        isAttackCooldown = false;
        Debug.Log(PlayerMovement.Hp);
    }
}
