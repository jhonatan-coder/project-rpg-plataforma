using UnityEngine;
using TMPro;
using System.Collections;

public class GameStatus : MonoBehaviour
{
    private static GameStatus instance;

    private ControlePontos _controlePontos;

    private ControleDeVidaDoPlayer vidaDoPlayer;
    [SerializeField]private TMP_Text scoreValue;
    [SerializeField]private TMP_Text vidaExtraValue;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(DelayInit());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaDoPlayer = FindFirstObjectByType<ControleDeVidaDoPlayer>();
        _controlePontos = FindFirstObjectByType<ControlePontos>();
        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("txtScoreValue").GetComponent<TMP_Text>();
        }
        if (vidaExtraValue == null)
        {
            vidaExtraValue = GameObject.Find("txtVidaExtraValue").GetComponent<TMP_Text>();
        }
        Debug.Log("Total score salvo: "+SaveSystem.dados.score);
    }

    // Update is called once per frame
    void Update()
    {
        Score();
        VidaExtra();
    }

    public void Score()
    {
        
        scoreValue.text = SaveSystem.dados.score.ToString();
    }

    public void VidaExtra()
    {
        vidaExtraValue.text = vidaDoPlayer.life.ToString();
    }

    private IEnumerator DelayInit()
    {
        yield return null;
    }
}
