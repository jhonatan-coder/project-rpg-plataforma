using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimacaoPulo()
    {
        anim.SetTrigger("isJumping");
    }
    public void AnimacaoPuloDuplo()
    {  
        anim.SetTrigger("PuloDuplo");
    }
    public void AnimacaoCorrer(bool value)
    {
        anim.SetBool("isRun", value);
    }
    public void AnimacaoCaindo(bool value)
    {
        anim.SetBool("isFalling", value);
    }
    /*public void ControleDeAnimacaoAgarrarNaParede()
    {
        if (instance.IsGrudando == true)
        {
            anim.SetBool("isGrudando", instance.IsGrudando);
        }
    }*/
}
