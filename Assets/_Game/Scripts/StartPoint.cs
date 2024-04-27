using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.Instance.InitPlayer(transform);
    }
}
