using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int coins = 0;

    public int Coins {
        get { return coins; }
        set { coins = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
