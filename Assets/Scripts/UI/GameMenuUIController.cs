using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUIController : MonoBehaviour
{
    void Awake()
    {
        retryButton.onClick.AddListener(OnRetryButtonClick);
        tweetButton.onClick.AddListener(OnTweetButtonClick);
        menuButton.onClick.AddListener(OnMenuButtonClick);
        GameOverPanel.SetActive(false);
    }

    void Start()
    {
        GameController.OnEndGame += ShowGameOver;
    }

    private void OnRetryButtonClick()
    {
        ReloadScene();
    }

    private void OnTweetButtonClick()
    {

    }

    private void OnMenuButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        retryButton.onClick.RemoveListener(OnRetryButtonClick);
        tweetButton.onClick.RemoveListener(OnTweetButtonClick);
        menuButton.onClick.RemoveListener(OnMenuButtonClick);
        GameController.OnEndGame -= ShowGameOver;
    }

    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button tweetButton;
    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private GameObject GameOverPanel;

}
