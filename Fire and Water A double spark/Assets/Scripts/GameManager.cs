using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TMP_Text _fireScore;
    [SerializeField] private TMP_Text _waterScore;
    [SerializeField] private GameObject mobileUI;
    [SerializeField] private GameObject _gamePause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _fireScore.text = "0";
        _waterScore.text = "0";
        MobileUIManager();
        GameStart();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int tag)
    {
        if (tag == 1)
        {
            int currentScore = int.Parse(_fireScore.text);
            currentScore++;
            _fireScore.text = currentScore.ToString();
        }
        else
        {
            int currentScore = int.Parse(_waterScore.text);
            currentScore++;
            _waterScore.text = currentScore.ToString();
        }
    }
    public void GameFail()
    {
        
    }
    public void GameStop()
    {
        Time.timeScale = 0f;
        _gamePause.SetActive(true);
    }
    public void GameStart()
    {
        Time.timeScale = 1f;
        _gamePause.SetActive(false);
    }
    public void GameRestart()
    {
        Scene currentScene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(currentScene.name); 
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void MobileUIManager()
    {
        if (Application.isMobilePlatform)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            mobileUI.SetActive(false);
        }
    }

}
