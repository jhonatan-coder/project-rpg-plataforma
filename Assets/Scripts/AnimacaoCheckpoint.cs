using UnityEngine;

public class AnimacaoCheckpoint : MonoBehaviour
{
    private Animator animCheckPoint;
    private void Start()
    {
        animCheckPoint = GetComponent<Animator>();
    }
    public void CheckPointAnimation(bool value)
    {
        animCheckPoint.SetBool("Ativado", value);

    }
    public void CheckPointAnimationPosLoading(bool value)
    {
        animCheckPoint.SetBool("AtivadoPosLoad", value);

    }
}
