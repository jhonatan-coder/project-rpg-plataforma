using UnityEngine;
using UnityEngine.SceneManagement;

public class NovaFase : MonoBehaviour
{
    private AnimacaoCheckpoint animCheckpoint;
    public string novaFase;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animCheckpoint = GetComponent<AnimacaoCheckpoint>();
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nova fase");
            Invoke(nameof(MudaFase), 3f);
            animCheckpoint.CheckPointAnimation(true);
        }
    }

    [SerializeField]
    public void MudaFase()
    {
        SceneManager.LoadScene(novaFase);
    }
}
