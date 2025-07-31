using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimacaoDeCaminhada(bool value)
    {
        anim.SetBool("isWalk", value);
    }

    public void AnimacaoDeCorrida(bool value)
    {
        anim.SetBool("isRun", value);
    }

    /*public void AnimacaoDeDano()
    {
        anim.set
    }*/
}
