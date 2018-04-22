using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    void Awake()
    {
        // Buttons listeners
        howToPlayButton.onClick.AddListener(OnHowToPlayButtonClick);
        playButton.onClick.AddListener(OnPlayButtonClick);
        creditsButton.onClick.AddListener(OnCreditsButtonClick);
        creditsObject.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

  void Start()
  {
    RenderSettings.skybox.SetFloat("_DayFactor", 0);
    DynamicGI.UpdateEnvironment();
  }

  private void OnHowToPlayButtonClick()
    {
        howToPlayPanel.SetActive(true);
    }

    private void OnPlayButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnCreditsButtonClick()
    {
        creditsObject.SetActive(true);

        List<string> teamNames = new List<string>();

        teamNames.Add("Carlota Esteban (Coding)\n@CarlotaEsCa");
        teamNames.Add("Javier Agüera (Coding)\n@AgueraJs");
        teamNames.Add("Marcos Díez (Coding)\n@Marcos10Tweets");
        teamNames.Add("Juan Antonio Sánchez (GD, SFX)\n@Selbrynsucks");
        teamNames.Add("Miguel Martín (Art)\n@mmibarreta");

        for (int i = 0; i < creditLabelsList.Count; i++)
        {
            int next = Random.Range(0, teamNames.Count);
            creditLabelsList[i].text = teamNames[next];
            teamNames.RemoveAt(next);
        }
    }

    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private GameObject creditsObject;
    [SerializeField]
    private GameObject howToPlayPanel;
    [SerializeField]
    private List<Text> creditLabelsList;
}
