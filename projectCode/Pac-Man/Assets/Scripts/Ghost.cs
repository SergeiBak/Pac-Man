using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private int points = 200;

    public int GetPoints()
    {
        return points;
    }
}
