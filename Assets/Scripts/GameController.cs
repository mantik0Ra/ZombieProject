using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private int coins = 0;

    public int Coins {
        get { return coins; }
        set { coins = value; }
    }

    GameObject gameUI;
    GameObject menuUI;
    GameObject deadUI;
    GameObject mainUI;

    public static bool restartBtnClicked = false;

    // Start is called before the first frame update

    private void Awake() {
        gameUI = GameObject.Find("GameUI");
        menuUI = GameObject.Find("MenuUI");
        mainUI = GameObject.Find("MainUI");
        deadUI = GameObject.Find("DeadUI");
        Time.timeScale = 0;
    }
    void Start()
    {
        if(restartBtnClicked) {
            menuUI.SetActive(false);
            Time.timeScale = 1;
            gameUI.SetActive(true);
            
        }
        else {
            gameUI.SetActive(false);
            deadUI.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PlayBtnClicked() {
        Time.timeScale = 1;
        gameUI.SetActive(true);
        menuUI.SetActive(false);
        
    }

    public void RestartBtnClicked() {
        restartBtnClicked = true;
        GameRestart();

    }

    public void PlayerIsDead() {
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        mainUI.SetActive(false);
        deadUI.SetActive(true);
    }
}
