using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Direct { Forward, Back, Right, Left, None }

    public Direct direct = Direct.None;
    private Vector3 startDirect, finishDirect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startDirect = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            finishDirect = Input.mousePosition;
            direct = GetDirect(startDirect, finishDirect);
            Debug.Log(direct);
        }
        ControlPlayer(direct);
    }

    private void ControlPlayer(Direct direct)
    {
        switch (direct)
        {
            case Direct.Forward:
                transform.Translate(Vector3.forward * 2f);
                ResetDirect();
                break;
            case Direct.Back:
                transform.Translate(Vector3.back * 2f);
                ResetDirect();
                break;
            case Direct.Right:
                transform.Translate(Vector3.right * 2f);
                ResetDirect();
                break;
            case Direct.Left:
                transform.Translate(Vector3.left * 2f);
                ResetDirect();
                break;
            case Direct.None:
                break;
            default:
                break;
        }
    }

    private Direct GetDirect(Vector3 startDirect, Vector3 finishDirect)
    {
        Debug.Log($"startDirect: {startDirect},finishDirect: {finishDirect} ");
        if (startDirect != null && finishDirect != null)
        {
            Vector3 direction = (finishDirect - startDirect);
            Debug.Log($"direction: {direction}");
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? Direct.Right : Direct.Left;
            }
            else
            {
                return direction.y > 0 ? Direct.Forward : Direct.Back;
            }
        }

        return Direct.None;
    }

    private void ResetDirect()
    {
        direct = Direct.None;
    }
}
