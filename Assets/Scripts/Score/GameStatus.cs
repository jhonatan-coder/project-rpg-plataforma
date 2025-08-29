using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameStatus : MonoBehaviour
{
    private static GameStatus instance;

    private ControlePontos _controlePontos;

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
    // PrimeiraVezJogando is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

/*    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scoreValue == null)
            scoreValue = GameObject.Find("txtScoreValue")?.GetComponent<TMP_Text>();
        if (vidaExtraValue == null)
            vidaExtraValue = GameObject.Find("txtVidaExtraValue")?.GetComponent<TMP_Text>();
    }*/
    void Start()
    {
        _controlePontos = GetComponent<ControlePontos>();
        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("txtScoreValue")?.GetComponent<TMP_Text>();
        }
        if (vidaExtraValue == null)
        {
            vidaExtraValue = GameObject.Find("txtVidaExtraValue")?.GetComponent<TMP_Text>();
        }
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
        if (ControleDeVidaDoPlayer.instance != null)
        {
            vidaExtraValue.text = ControleDeVidaDoPlayer.instance.life.ToString();
        }
        else
        {
            vidaExtraValue.text = SaveSystem.dados.vidasExtras.ToString();
        }
    }

    private IEnumerator DelayInit()
    {
        yield return null;
        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("txtScoreValue")?.GetComponent<TMP_Text>();
        }
        if (vidaExtraValue == null)
        {
            vidaExtraValue = GameObject.Find("txtVidaExtraValue")?.GetComponent<TMP_Text>();
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("txtScoreValue")?.GetComponent<TMP_Text>();
        }
        if (vidaExtraValue == null)
        {
            vidaExtraValue = GameObject.Find("txtVidaExtraValue")?.GetComponent<TMP_Text>();
        }

        if (ControleDeVidaDoPlayer.instance == null)
        {
            var vidaObj = FindFirstObjectByType<ControleDeVidaDoPlayer>();
            if (vidaObj != null)
            {
                ControleDeVidaDoPlayer.instance = vidaObj;
            }
        }
    }


}
