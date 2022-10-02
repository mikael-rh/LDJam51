using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public void LoadScene(int index) => SceneManager.LoadScene(index);

	public void QuitGame() => Application.Quit();
}
