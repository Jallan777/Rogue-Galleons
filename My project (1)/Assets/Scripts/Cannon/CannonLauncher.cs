using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonLauncher : MonoBehaviour
{
    private CannonBall ball;

   [Header("Basic Parameters")]
    public float cannonAttack;

    [Header("Attack Parameters")]
    public bool isActive;
    public bool isCoolingDown;
    public float setCoolDownTime;
    public float currentCoolDownTime;
    public Attack attack;


    private void Awake() {
        this.attack = new Attack(20);
        this.ball = this.GetComponentInChildren<CannonBall>();
        this.currentCoolDownTime = setCoolDownTime;
        this.isCoolingDown = true;
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
