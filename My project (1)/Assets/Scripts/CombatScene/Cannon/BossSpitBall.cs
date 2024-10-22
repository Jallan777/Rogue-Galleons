using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossSpitBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public CannonBallStat stat;
    public ShipType shipType;
    public Vector3 startingGlobalPosition;
    private BossLauncher launcher;
    public UnityEvent<Attack> onHit;
    public Attack attack;

    private void Awake() {
        this.launcher = this.GetComponentInParent<BossLauncher>();
         this.rb = this.GetComponent<Rigidbody2D>();
         this.sr = this.GetComponent<SpriteRenderer>();
         this.shipType = this.GetComponentInParent<BossLauncher>().GetComponentInParent<Ship>().shipType;
        this.stat = CannonBallStat.SELF_SIDE;

    }

    public void BallReset(){
        this.GetComponent<CircleCollider2D>().enabled = false;
        Awake();
        // this.stat = CannonBallStat.SELF_SIDE;
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.rb.velocity = new Vector2(0,0);
        this.sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0);
        if(shipType == ShipType.PLAYER){
            this.rb.rotation = 30;
            this.transform.localPosition = new Vector3(0.5f,0.1f,0);

        }
        else{
            this.rb.rotation = - 13.5f;
            this.transform.localPosition = new Vector3(- 0.5f,0.1f,0);
        }
        this.GetComponent<CircleCollider2D>().enabled = true;

    }

    public void Shoot(Attack attack){
        this.attack = attack;
        rb.totalForce = new Vector2(0,0);
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,1);
        if(this.shipType == ShipType.PLAYER)
            rb.AddForceAtPosition(new Vector2(9,6),this.transform.localPosition,ForceMode2D.Impulse);
        else
            rb.AddForceAtPosition(new Vector2(- 9,6),this.transform.localPosition,ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D other) {

        // if on opposite side, then result damage
        if(this.stat == CannonBallStat.OPPOSITE_SIDE){
            this.onHit.Invoke(this.attack);
            this.BallReset();
            return;
        }
        
        //if not on opposite side (which means this contact is with the mid-collider for teleport)

        if(this.shipType == ShipType.PLAYER){
            this.transform.localPosition = new Vector3(7,2.5f,0);
            this.rb.velocity = new Vector2(1,- 2);
        }
        else{
            this.rb.velocity = Vector2.zero;
            this.transform.localPosition = new Vector3(- 2.8f,3f,0);
            rb.AddForce(new Vector2(- 4,5),ForceMode2D.Impulse);
        }   

        this.rb.rotation = -90;
        this.stat = CannonBallStat.OPPOSITE_SIDE;
    }
}
