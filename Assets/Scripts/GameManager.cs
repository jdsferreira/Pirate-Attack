using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance {get; private set;}
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    //Constants
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    //API
    public bool isGameOver {get; private set;}
    [Header ("Audio")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource gameOverSfx;
    
    [Header("Score")] 
    [SerializeField] private float score;
    [SerializeField] private int highestScore;

    
    void Awake()
    {
        //Singleton
        if(Instance != null && Instance != this){
            Destroy(this);
        } else{
            Instance = this;
        }

        //Score
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }
    void Update()
    {
        if(!isGameOver){
            //increment score
            score += Time.deltaTime;
            //update highest score
            if (GetScore() > GetHighestScore()){
                highestScore = GetScore();
            }
        }
        
    }

    public int GetScore(){
        return (int) Mathf.Floor(score);
    }

    public int GetHighestScore(){
        return highestScore;
    }
    
    
   public void EndGame(){
    if (isGameOver) return;

    isGameOver = true;

    //stop music
    musicPlayer.Stop();

    //play SFX
    gameOverSfx.Play();

    GamePlayUI gamePlayUI = FindAnyObjectByType<GamePlayUI>();
    if(gamePlayUI != null){
        gamePlayUI.SetGameOverText("GAME OVER !!!");
    }

    //close in the player
    CinemachineTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    if(transposer != null){
        transposer.m_FollowOffset = new Vector3(0, 5, 5);
    }

    //save highest Score
    PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, GetHighestScore());

    //Reload scene
    StartCoroutine(ReloadScene(7));


   }
   private IEnumerator ReloadScene(float delay){
    yield return new WaitForSeconds(delay);
    
 
    string sceneName = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(sceneName);
   }
}
