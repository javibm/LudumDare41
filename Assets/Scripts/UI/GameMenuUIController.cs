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
    menuButton.onClick.AddListener(OnMenuButtonClick);
    GameOverPanel.SetActive(false);
  }

  void Start()
  {
    GameController.OnEndGame += ShowGameOver;
    GameController.OnPlayerWin += ShowWin;
  }

  private void OnRetryButtonClick()
  {
    GameController.Instance.ButtonClick();
    ReloadScene();
  }

  private void OnMenuButtonClick()
  {
    GameController.Instance.ButtonClick();
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
  }

  private void ShowGameOver()
  {
    GameOverPanel.SetActive(true);
    winText.SetActive(false);
    loseText.SetActive(true);
    newGame.sprite = replay;
  }

  private void ShowWin()
  {
    GameOverPanel.SetActive(true);
    winText.SetActive(true);
    loseText.SetActive(false);
    newGame.sprite = next;
  }

  private void ReloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  void OnDestroy()
  {
    retryButton.onClick.RemoveListener(OnRetryButtonClick);
    menuButton.onClick.RemoveListener(OnMenuButtonClick);
    GameController.OnEndGame -= ShowGameOver;
    GameController.OnPlayerWin -= ShowWin;
  }

  [SerializeField]
  private GameObject winText;
  [SerializeField]
  private GameObject loseText;
  [SerializeField]
  private Button retryButton;
  [SerializeField]
  private Button menuButton;

  [Space]
  [SerializeField]
  private Image newGame;
  [SerializeField]
  private Sprite next;
  [SerializeField]
  private Sprite replay;


  [SerializeField]
  private GameObject GameOverPanel;

}
