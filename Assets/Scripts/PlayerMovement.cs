using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensivity = 50f;
    private GameObject Camera;
    Vector3 currentEulerAngles;

    ParticleSystem particleSystem;

    bool CanShoot = false;
    void Start()
    {
        Camera = GameObject.Find("Camera");
        particleSystem = GameObject.Find("WFX_Explosion Small").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");
        currentEulerAngles += new Vector3(verticalInput * -1, horizontalInput, 0) * Time.deltaTime * sensivity;
        Camera.transform.eulerAngles = currentEulerAngles;
        
        if(Input.GetKey(KeyCode.Mouse0) && !CanShoot) {
            particleSystem.Play();
            CanShoot = true;
            Debug.Log(CanShoot);
            StartCoroutine(Coroutine());
        }
    }

    private void FixedUpdate() {
        if(CanShoot) {
            Shoot();
        }
        Debug.DrawRay(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward) * 100f, Color.blue);
    }

    IEnumerator Coroutine() {
        yield return new WaitForSeconds(.4f);
        CanShoot = false;
    }
    void Shoot() {
        RaycastHit hit;
        Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out hit, 100f);
        if (hit.collider is not null && hit.collider.tag == "Zombie") {
            Debug.Log("Hit zombie");
        }
    }
    
    


}
