using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDenial : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Debug");
        gameObject.SetActive(false);
    }
}
