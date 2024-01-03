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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAttack) {
            transform.Translate(new Vector3(transform.position.x, 0, transform.position.z) * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            IsAttack = true;
        }
    }
}
