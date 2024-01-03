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


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
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
            Debug.Log(delta);
            

        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            IsAttack = true;
        }
    }
}
