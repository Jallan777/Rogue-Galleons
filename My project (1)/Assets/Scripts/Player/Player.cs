using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<Attack> onAttacking;
    [Header("Status")]
    public bool isAttacking;
    public CannonLauncher launcher;

    private void Awake() {
        this.launcher = this.GetComponentInChildren<CannonLauncher>();
    }
    private void Update() {
        if(isAttacking){
            launcher.isActive = true;
        }else{
            launcher.isActive = false;
        }
    }
}
