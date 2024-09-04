using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Events/ShipTakeDamageSO")]
public class TakeDamageEventSO : ScriptableObject
{
    
    public UnityAction<Ship> shipTakingDamage;
    
    public void OnEventRaised(Ship ship){
        shipTakingDamage.Invoke(ship);
    }

}
