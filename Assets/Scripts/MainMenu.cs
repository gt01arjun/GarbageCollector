using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;
    public GameObject CreditsPanel;

    public void OnMainMenu()
    {
        MainMenuPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void OnControlsPanel()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void OnCreditsPanel()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}