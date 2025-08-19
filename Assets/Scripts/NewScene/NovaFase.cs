using UnityEngine;

public class NovaFase : MonoBehaviour
{
    private AnimacaoCheckPoint animCheckPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animCheckPoint = GetComponent<AnimacaoCheckPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nova fase");
            animCheckPoint.CheckPointAnimation();
        }
    }
}
