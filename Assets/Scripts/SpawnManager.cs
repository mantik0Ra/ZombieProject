using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject zombieObject;
    private bool isSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawn) {
            SpawnZombieInRandomPlace();
            isSpawn = true;
        }
        
    }

    private IEnumerator waitForSpawn(float x, float y) {
        yield return new WaitForSeconds(3f);
        Instantiate(zombieObject, new Vector3(x, 0, y), new Quaternion(0f, 0f, 0f, 0f));
        isSpawn = false;
    }

    private void SpawnZombieInRandomPlace() {
        float randomX = Mathf.Sin(UnityEngine.Random.Range(60, 89)) * 20f;
        float randomY = Mathf.Cos(UnityEngine.Random.Range(60, 89)) * 20f;
        StartCoroutine(waitForSpawn(randomX, randomY));
    }
    
    
}
