using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHudHandler : MonoBehaviour
{
    public void OnRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
