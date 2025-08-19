using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.dat";
    public static SaveData dados = new SaveData();

    public static void Salvar()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, dados);
        file.Close();
    }

    public static void Carregar()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            dados = (SaveData)bf.Deserialize(file);
            file.Close();
            Debug.Log("[SaveSystem] Carregado! Score: " + dados.score);
        }
        else
        {
            dados = new SaveData();
            dados.vidasExtras = 3;
            Salvar();
            Debug.Log("[SaveSystem] Novo save criado.");
        }
    }

    public static void DeletarSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            dados = new SaveData();
        }
    }
}
