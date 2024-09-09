using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<Attack> onAttacking;
    [Header("Status")]
    public bool isAttacking;

    private void Update() {
        if(isAttacking){
            StartCoroutine(attackObject());
        }
    }

    public IEnumerator attackObject(){
            onAttacking.Invoke(new Attack(20));
            this.isAttacking = false;
            return null;
    }
}
