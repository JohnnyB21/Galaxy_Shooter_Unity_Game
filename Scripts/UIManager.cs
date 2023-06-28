using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

 

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }


    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    IEnumerator GameOverFlicker()
    {
        int i = 10;
        while (i > 0)
        {
            //transform.GetChild(2).gameObject.GetComponent<Text>().enabled = true;
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);

            //transform.GetChild(2).gameObject.GetComponent<Text>().enabled = false;
            _gameOverText.text = " ";
            yield return new WaitForSeconds(0.5f);
            i--;
            Debug.Log(i);
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.enabled = true;
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }


}
