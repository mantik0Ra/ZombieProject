using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            healthBar.value -= 5;
        }
    }
}
