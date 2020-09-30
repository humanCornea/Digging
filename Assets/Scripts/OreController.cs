using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--- Oreに関する動作は"PlayerController2.cs"に記述 ---*/
public class OreController : MonoBehaviour
{
    bool Exist = true;
    public float Point { set; get; }

    public float TakePoint() {
        float returnPoint = 0;
        if (Exist == true ) {
            returnPoint = this.Point;
            Exist = false;
        }

        return returnPoint;
    }
}
