using UnityEngine;

public class TribalAnimation : MonoBehaviour
{
    private Animator tri_Anim;
    public TribalAI tribal;

    private int throwLayerIndex;

    private float targetLayerValue = 1;

    private void Start()
    {
        tri_Anim = GetComponent<Animator>();
        throwLayerIndex = tri_Anim.GetLayerIndex("UpperBody");
    }

    private void Update()
    {
        tri_Anim.SetBool("isRun", tribal.IsSprinting());

        tri_Anim.SetBool("isAttacking", tribal.DoingAttack());

        //tri_Anim.SetBool("isDancing", tribal.isDancing);
    }
}