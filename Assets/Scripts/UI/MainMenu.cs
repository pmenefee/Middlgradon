using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        SceneManager.LoadScene("Midgard");
    }

    public void LoadGame()
    {
        Debug.Log("Has not been implemented.");
    }

    public void SaveGame()
    {
        Debug.Log("Has not been implemented.");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
