using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class absorberPartWhereLiquidDown : MonoBehaviour
{
    public void AbsorberTurnedOn()
    {
        TurnedOn?.Invoke();
    }
    public Action TurnedOn;


}
