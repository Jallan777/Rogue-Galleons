using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public bool vol;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private void Awake() {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.sr = this.GetComponent<SpriteRenderer>();
    }
    private void OnDisable() {
    }

    public void Disable(Vector3 launcherPosition){
        // this.enabled = false;
        this.sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0);
        this.rb.simulated = false;
        this.transform.position = launcherPosition +  new Vector3(0.5f,0.25f,0);

    }

    public void Enable(){
        this.enabled = true;
        this.rb.simulated = true;
        this.sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,1);
        rb.AddForce(new Vector2(7,4),ForceMode2D.Impulse);

    }
    
    private void Shoot(){

    }

    private void Update() {
        if(vol){
            vol = false;
            Enable();
        }
    }
}
