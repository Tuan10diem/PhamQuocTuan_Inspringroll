using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour, IObserver
{

    public Text score;
    public GameObject replayButton;
    public GameObject homeButton;
    public GameObject final;
    public Text finalScore;
    public Text bestScore;
    public List<GameObject> heart;

    private int best;
    private int current;
    private int currentHeart=4;

    public Subjects subjects;

    private Dictionary<GameAction, System.Action> gameActionHandler;

    public void OnNotify(GameAction action)
    {
        if (gameActionHandler.ContainsKey(action))
        {
            gameActionHandler[action]();
        }
    }

    private void Awake()
    {
        best = PlayerPrefs.GetInt("BestScore", 0);
        current = 0;
        gameActionHandler = new Dictionary<GameAction, System.Action>()
        {
            { GameAction.catched,Catched },
            { GameAction.missed, Missed },
            { GameAction.gameover, GameOver }
        };
    }

    // Update is called once per frame
    void Update()
    {
        score.text = current.ToString();
    }

    void Catched()
    {
        current += 10;
    }

    void Missed()
    {
        if(currentHeart>=1) 
        {
            heart[currentHeart-1].SetActive(false); 
            currentHeart--;
        }
        else
        {
            heart[currentHeart - 1].SetActive(false);
            GameOver();
        }
    }

    void GameOver()
    {
        final.SetActive(true);
        SetFinal();
        finalScore.text = current.ToString();
        bestScore.text = best.ToString();

        Time.timeScale = 0;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetFinal()
    {
        best=Mathf.Max(best, current);
        PlayerPrefs.SetInt("BestScore", best); 
    }

    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnDisable()
    {
        subjects.RemoveObserver(this);
    }

    private void OnEnable()
    {
        subjects.AddObserver(this);
    }

    
}
