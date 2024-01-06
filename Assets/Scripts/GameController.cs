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

    // Start is called before the first frame update

    private void Awake() {
        gameUI = GameObject.Find("GameUI");
        menuUI = GameObject.Find("MenuUI");
        Time.timeScale = 0;
    }
    void Start()
    {
        gameUI.SetActive(false);
        
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
}
