using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private Chair[] chairs;

    public Chair GetAvailableChair()
    {
        foreach(Chair c in chairs)
        {
            if (!c.ocupied)
            {
                return c;
            }
        }
        return null;
    }
}
