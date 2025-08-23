using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
