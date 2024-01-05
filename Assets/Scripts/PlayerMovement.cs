using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensivity = 50f;
    private GameObject Camera;
    private Vector3 currentEulerAngles;

    private ParticleSystem particleSystem;

    private bool isShoot = false;
    private bool isCooldown = false;
    private bool isDead = false;

    private float horizontalInput;
    private float verticalInput;

    private AudioSource audioPlayer;
    private GameObject Drum;
    private GameController gameController;

    private static float hp = 100f;
    public static float Hp {
        get { return hp; }
        set { hp = value; }
    }
    void Start()
    {
        Camera = GameObject.Find("Camera");
        particleSystem = GameObject.Find("WFX_Explosion Small").GetComponent<ParticleSystem>();
        audioPlayer = Camera.GetComponent<AudioSource>();
        Drum = GameObject.Find("pistol_drum_001");
        gameController = new GameController();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        PressButtonToShot();
    }

    private void FixedUpdate() {
        if(isShoot && !isCooldown) {
            Shoot();
            StartCoroutine(Coroutine());
        }

        Debug.DrawRay(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward) * 100f, Color.blue);
    }

    IEnumerator Coroutine() {

        yield return new WaitForSeconds(.4f);
        audioPlayer.Stop();
        isShoot = false;
        isCooldown = false;

    }

    void Shoot() {
        isCooldown = true;
        RaycastHit hit;
        Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out hit, 100f);

        if (hit.collider is not null && hit.collider.tag == "Zombie") {
            ZombieController zombieController = hit.collider.GetComponent<ZombieController>();
            zombieController.ZombieTakeDamage(25f);
            CoinsCounter(zombieController);
        }
    }

    void PressButtonToShot() {
        if (Input.GetKey(KeyCode.Mouse0) && !isShoot) {
            particleSystem.Play();
            audioPlayer.Play();
            RotateDrum();
            isShoot = true;
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

    public static void PlayerTakeDamage(int valueDamage) {
        Hp -= valueDamage;
        HealthBar.ReduceHpInHealthBar(valueDamage);
    }

    private void CoinsCounter(ZombieController zombie) {
        if(zombie.IsDead) {
            gameController.Coins++;
            Debug.Log(gameController.Coins);
        }
    }
}
