using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MyMonoBehaviour
{
    [SerializeField] protected Animator anim;
    protected override void LoadComponents()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            OnFinishEffect();
    }

    public void OnFinishEffect() {
        Destroy(gameObject);
    }
}
