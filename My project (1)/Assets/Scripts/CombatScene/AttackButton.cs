using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    private Animator animator;
    private TMP_Text text;
    private bool isAttack; // only affects logic here in this class. Not relavant to other invokes.
    private void Awake() {
        this.animator = this.GetComponent<Animator>();
        this.isAttack = false;
        this.text = this.GetComponentInChildren<TMP_Text>();
    }
    public void buttonOnClickEvent(){
        this.isAttack = ! isAttack;
        this.animator.SetBool("isAttack",isAttack);
        if(isAttack)
            text.SetText("Attacking");
        else    
            text.SetText("Not Attacking");
    }
}
