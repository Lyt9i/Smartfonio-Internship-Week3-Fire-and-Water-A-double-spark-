using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TMP_Text _fireScore;
    [SerializeField] private TMP_Text _waterScore;
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
}
