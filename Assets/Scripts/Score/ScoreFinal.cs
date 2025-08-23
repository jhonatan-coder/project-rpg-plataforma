using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreFinal : MonoBehaviour
{
    //private static ScoreFinal instance;
    private ControlePontos _controleDePontos;
    public TMP_Text scoreFinal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _controleDePontos = FindFirstObjectByType<ControlePontos>();
        enabled = true;
        SaveSystem.Carregar();
        
    }

    private void Start()
    {
        //SaveSystem.Carregar();
        scoreFinal.text = _controleDePontos.totalScore.ToString();
        
    }
}
