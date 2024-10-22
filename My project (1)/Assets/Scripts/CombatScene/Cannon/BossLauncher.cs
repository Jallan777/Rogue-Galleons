using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossLauncher : MonoBehaviour
{
    private CannonBall ball;

   [Header("Basic Parameters")]
    public float cannonAttack;

    [Header("Attack Parameters")]
    public bool isActive;
    public float bossAttackModifier;
    public bool isCoolingDown;
    public float setCoolDownTime;
    public float currentCoolDownTime;
    public Attack attack;
    private int countDownResetMarker;


    private void Awake() {
        this.attack = new Attack(20 * bossAttackModifier);
        this.ball = this.GetComponentInChildren<CannonBall>();
        this.currentCoolDownTime = setCoolDownTime;
        this.isCoolingDown = true;
        this.countDownResetMarker = 0;
        ball.BallReset();
        
    }

    private void Update() {
        if(isActive){
            if(!isCoolingDown){
                ball.Shoot(attack);
                this.isCoolingDown = true;
            }else{
                CountDown();
            }
        }else{
            currentCoolDownTime = setCoolDownTime;
        }
    }

    private void CountDown(){
        currentCoolDownTime -= Time.deltaTime;
        if(currentCoolDownTime<=0){
            isCoolingDown = false;
            currentCoolDownTime = setCoolDownTime;
        }
    }
}
