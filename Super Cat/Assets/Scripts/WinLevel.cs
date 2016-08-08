using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour {

    public void winLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
