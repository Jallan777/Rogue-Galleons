using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
       // Health health = ship.GetComponent<Health>();
       // if (health != null)
          //  health.AddHealth((int)val);
    }
}
