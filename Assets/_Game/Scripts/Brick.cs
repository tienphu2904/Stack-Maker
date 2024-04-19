using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private LayerMask ignoreRaycastLayer;


    public bool IsEnable
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
        set
        {
            transform.parent.gameObject.layer = value ? ignoreRaycastLayer : raycastLayer;
            transform.gameObject.SetActive(value);
        }
    }

    private void Start()
    {
        IsEnable = transform.gameObject.activeInHierarchy;
    }

    public void EnableBrick(bool isEnable)
    {
        IsEnable = isEnable;
    }
}
