using UnityEngine;

public class AnimacaoCorrenteSerra : MonoBehaviour
{
    public bool Angulo90;
    public bool Angulo180;
    [SerializeReference] private Animator animAngulacaoSerra;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        animAngulacaoSerra = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animacao();
    }

    public void Animacao()
    {
        if (Angulo90)
        {
            animAngulacaoSerra.SetBool("Ativado_90", Angulo90);

        }
        if (Angulo180)
        {
            animAngulacaoSerra.SetBool("Ativado_180", Angulo180);

        }
    }
}
