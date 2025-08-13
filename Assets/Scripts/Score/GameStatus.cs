using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{

    private ControleDeVidaDoPlayer vidaDoPlayer;
    [SerializeField]private TMP_Text scoreValue;
    [SerializeField]private TMP_Text vidaExtraValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaDoPlayer = FindFirstObjectByType<ControleDeVidaDoPlayer>();

        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("txtScoreValue").GetComponent<TMP_Text>();
        }
        if (vidaExtraValue == null)
        {
            vidaExtraValue = GameObject.Find("txtVidaExtraValue").GetComponent<TMP_Text>();
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
        scoreValue.text = ControlePontos.instance.totalScore.ToString();
    }

    public void VidaExtra()
    {
        vidaExtraValue.text = vidaDoPlayer.life.ToString();
    }
}
