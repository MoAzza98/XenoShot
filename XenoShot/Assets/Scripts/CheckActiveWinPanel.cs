using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckActiveWinPanel : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
