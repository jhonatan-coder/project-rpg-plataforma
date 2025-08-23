using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    private CheckPointManager instance;
    private string checkpointID;
    private BoxCollider2D boxCol2D;
    private AnimacaoCheckpoint animCheckpoint;

    private void Awake()
    {
        enabled = true;
        Debug.Log($"CheckPointManager Awake - enabled? {enabled}, GameObject active? {gameObject.activeSelf}");
        boxCol2D = GetComponent<BoxCollider2D>();
        animCheckpoint = GetComponent<AnimacaoCheckpoint>();
        checkpointID = SceneManager.GetActiveScene().name + "_" + transform.name;
        
        
    }
    private void Start()
    {
        if (SaveSystem.dados != null && SaveSystem.dados.checkpointsAtivados.Contains(checkpointID))
        {
            boxCol2D.enabled = false;
            animCheckpoint.CheckPointAnimationPosLoading(true);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!SaveSystem.dados.checkpointsAtivados.Contains(checkpointID))
            {
                animCheckpoint.CheckPointAnimation(true);
                boxCol2D.enabled = false;

                //Salva checkpoint ativo
                SaveSystem.dados.checkpointsAtivados.Add(checkpointID);
                SaveSystem.dados.cenaAtual = SceneManager.GetActiveScene().name;
                SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(collision.transform.position);
                SaveSystem.Salvar();
                Debug.Log("Checkpoint salvo: " + checkpointID);
            }
        }
    }

}
