using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensivity = 50f;
    private GameObject Camera;
    Vector3 currentEulerAngles;

    ParticleSystem particleSystem;
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
        
        if(Input.GetKey(KeyCode.Mouse0)) {
            particleSystem.Play();
        }
    }
}
