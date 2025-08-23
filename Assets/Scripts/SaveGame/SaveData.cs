using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public List<string> cenasVisitadas = new List<string>();
    public List<string> itensColetados = new List<string>();
    public List<string> checkpointsAtivados = new List<string>();
    public int items;
    public int vidasExtras;
    public int score;
    
    public SerializableVector3 posicaoJogador;

    public string cenaAtual;

}
