using UnityEngine;

public class Instanciamento : MonoBehaviour
{

    public GameObject snail;
    public GameObject shell;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void InstanciarSegundaEtapa()
    {
        Instantiate(snail, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(shell, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
    }

}
