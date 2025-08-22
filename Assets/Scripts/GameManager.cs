using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Paddle paddle1;
    [SerializeField] private Paddle paddle2;
    [SerializeField] private Ball ball;

    private int paddle1Score = 0;
    private int paddle2Score = 0;

    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(paddle1ScoreText == null || paddle2ScoreText == null)
        {
            Debug.LogError("Paddle score texts are not assigned in the GameManager.");
            return;
        }

        if(paddle1 == null || paddle2 == null || ball == null)
        {
            Debug.LogError("Paddles or ball are not assigned in the GameManager.");
            return;
        }
    }

    public void OnEnable()
    {
        Ball.OnPlayer1Scored += Paddle1Scored;
        Ball.OnPlayer2Scored += Paddle2Scored;
    }

    public void OnDisable()
    {
        Ball.OnPlayer1Scored -= Paddle1Scored;
        Ball.OnPlayer2Scored -= Paddle2Scored;
    }

    public void Paddle1Scored()
    {
        paddle1Score++;
        paddle1ScoreText.text = paddle1Score.ToString();
        Restart();
    }

    public void Paddle2Scored()
    {
        paddle2Score++;
        paddle2ScoreText.text = paddle2Score.ToString();
        Restart();
    }

    public void Restart()
    {
        if(ball != null && paddle1 != null && paddle2 != null)
        {
            ball.Restart();
            paddle1.Restart();
            paddle2.Restart();
        }
    }
}
