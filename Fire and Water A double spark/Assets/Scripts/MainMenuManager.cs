using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Text _scoreFire;
    [SerializeField] private Text _scoreWater;
    [SerializeField] private Text _scoreTotal;
    void Start()
    {
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadScore()
    {
        int fireScore = PlayerPrefs.GetInt("FireScore");
        int waterScore = PlayerPrefs.GetInt("WaterScore");
        int totalScore = PlayerPrefs.GetInt("TotalScore");
        _scoreFire.text = fireScore.ToString();
        _scoreWater.text = waterScore.ToString();
        _scoreTotal.text = totalScore.ToString();
    }
}
