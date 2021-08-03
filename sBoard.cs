using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sBoard : MonoBehaviour
{
    [NamedArray(typeof(ePos))] public Transform[] location;
    private void Awake()
    {
        //GameManager.gm.s_Board = this;
    }
}
