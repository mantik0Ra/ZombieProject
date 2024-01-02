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

    float horizontalInput;
    float verticalInput;

    AudioSource audioPlayer;
    GameObject Drum;
    void Start()
    {
        Camera = GameObject.Find("Camera");
        particleSystem = GameObject.Find("WFX_Explosion Small").GetComponent<ParticleSystem>();
        audioPlayer = Camera.GetComponent<AudioSource>();
        Drum = GameObject.Find("pistol_drum_001");
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        PressButtonToShot();
    }

    private void FixedUpdate() {
        if(CanShoot) {
            Shoot();
        }

        Debug.DrawRay(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward) * 100f, Color.blue);
    }

    IEnumerator Coroutine() {

        yield return new WaitForSeconds(.4f);
        audioPlayer.Stop();
        CanShoot = false;
    }

    void Shoot() {
        RaycastHit hit;
        Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out hit, 100f);

        if (hit.collider is not null && hit.collider.tag == "Zombie") {
            Debug.Log("Hit zombie");
        }
    }

    void PressButtonToShot() {
        if (Input.GetKey(KeyCode.Mouse0) && !CanShoot) {
            particleSystem.Play();
            audioPlayer.Play();
            RotateDrum();
            CanShoot = true;
            StartCoroutine(Coroutine());
        }
    }

    void CameraMovement() {
        horizontalInput = Input.GetAxis("Mouse X");
        verticalInput = Input.GetAxis("Mouse Y");

        currentEulerAngles += new Vector3(verticalInput * -1, horizontalInput, 0) * Time.deltaTime * sensivity;
        Camera.transform.eulerAngles = currentEulerAngles;
    }
    void RotateDrum() {
        Drum.transform.eulerAngles += new Vector3(0, 0, 60);
    }
}
