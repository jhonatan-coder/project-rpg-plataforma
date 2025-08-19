using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Start(string nameScena)
    {
        SceneManager.LoadScene(nameScena);
    }
}
