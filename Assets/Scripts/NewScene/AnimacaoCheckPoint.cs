using UnityEngine;

public class AnimacaoCheckPoint : MonoBehaviour
{
    private Animator animCheckPoint;
    
    void Start()
    {
        animCheckPoint = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPointAnimation()
    {
        animCheckPoint.SetTrigger("CheckPoint");
    }
}
