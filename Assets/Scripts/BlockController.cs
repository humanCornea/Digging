using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    bool Exist = true;
    public float Point { set; get; }
    // float waittime = 1f;

    public float TakePoint() {
        float returnPoint = 0;
        if (Exist == true ) {
            returnPoint = this.Point;
            Exist = false;
        }

        return returnPoint;
    }
}