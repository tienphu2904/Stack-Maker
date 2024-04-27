using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool IsEnable
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
        set
        {
            transform.gameObject.SetActive(value);
        }
    }

    public void EnableBrick(bool isEnable)
    {
        IsEnable = isEnable;
    }
}
