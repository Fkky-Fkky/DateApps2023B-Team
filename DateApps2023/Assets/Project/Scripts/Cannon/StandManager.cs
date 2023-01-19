using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    [SerializeField]
    private CannonStand[] stands = new CannonStand[3];

    public bool IsConectingStand()
    {
        bool isConectingdStand = false;
        for (int i = 0; i < stands.Length; ++i)
        {
            if (stands[i].IsConnect)
            {
                isConectingdStand = true;
            }
        }
        return isConectingdStand;
    } 
}
