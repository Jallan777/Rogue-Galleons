using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonLauncher : MonoBehaviour
{
    private CannonBall ball;

   [Header("Basic Parameters")]
    public CannonType cannonType;
    public float cannonAttack;

    [Header("Attack Parameters")]
    public bool isActive;
    public bool isCoolingDown;
    public float coolDownTime;


    private void Awake() {
        this.ball = this.GetComponentInChildren<CannonBall>();
        ball.Disable(this.transform.position);
    }
}
