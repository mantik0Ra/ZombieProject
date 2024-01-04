using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private static Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ReduceHpInHealthBar(int value) {
        healthBar.value -= value;
    }


}
