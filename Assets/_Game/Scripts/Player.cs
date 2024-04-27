using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum DirectType { Forward, Back, Right, Left, None }

    [SerializeField] float speed = 10f;
    [SerializeField] Transform jiao;
    [SerializeField] LayerMask raycastLayer, ignoreRaycastLayer;

    private Vector3 startTouchPosition, lastTouchPosition, targetPosition;
    private Vector3 direction;
    private bool isMoving = false;
    private Collider lastCollider;
    private List<Object> listPlayerBrick;

    private static float unitSize = 1f;

    private void Start()
    {
        targetPosition = transform.position;
        listPlayerBrick = new List<Object>();
        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
        ControlPlayer();
    }

    private void Update()
    {
        Debug.Log("Update");
        if (!isMoving)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    lastTouchPosition = touch.position;
                    GetFinishPoint(GetDirectType(startTouchPosition, lastTouchPosition));
                }
            }
        }
    }

    //Moving Player
    private void ControlPlayer()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0)
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            ManageBrick();
        }
        else
        {
            isMoving = false;
        }
    }

    private void GetFinishPoint(DirectType direct)
    {
        switch (direct)
        {
            case DirectType.Forward:
                direction = Vector3.forward;
                break;
            case DirectType.Back:
                direction = Vector3.back;
                break;
            case DirectType.Right:
                direction = Vector3.right;
                break;
            case DirectType.Left:
                direction = Vector3.left;
                break;
            case DirectType.None:
                direction = Vector3.zero;
                break;
        }
        targetPosition = CheckFinishPoint(direction, transform.position);
    }

    private DirectType GetDirectType(Vector3 startTouchPosition, Vector3 lastTouchPosition)
    {
        if (startTouchPosition != lastTouchPosition && startTouchPosition != Vector3.zero && lastTouchPosition != Vector3.zero)
        {
            Vector3 direction = (lastTouchPosition - startTouchPosition);
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? DirectType.Right : DirectType.Left;
            }
            else
            {
                return direction.y > 0 ? DirectType.Forward : DirectType.Back;
            }
        }

        return DirectType.None;
    }

    private Vector3 CheckFinishPoint(Vector3 direction, Vector3 startPosition)
    {
        RaycastHit hit;
        Vector3 point = startPosition;
        for (int i = 0; i < 50; i++)
        {
            if (Physics.Raycast(point + direction * unitSize + Vector3.up * 2f, Vector3.down, out hit, 5f))
            {
                if (hit.collider.CompareTag("Brick")
                    || hit.collider.CompareTag("UnBrick")
                    || hit.collider.CompareTag("StartPoint")
                    || hit.collider.CompareTag("FinishPoint"))
                {
                    point += direction * unitSize;
                }
                else break;
            }
        }
        return point;
    }

    //Manager Brick
    private void ManageBrick()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up - direction * 0.4f, Vector3.down);
        if (Physics.Raycast(ray, out hit, 5f, raycastLayer))
        {
            if (lastCollider != hit.collider)
            {
                if (hit.collider.CompareTag("Brick"))
                {
                    hit.collider.transform.GetComponent<Brick>().EnableBrick(false);
                    AddBrick();
                }
                if (hit.collider.CompareTag("UnBrick"))
                {
                    if (listPlayerBrick.Count > 0)
                    {
                        hit.collider.transform.GetChild(0).GetComponent<Brick>().EnableBrick(true);
                        hit.collider.gameObject.layer = ignoreRaycastLayer;
                        RemoveBrick();
                    }
                    else
                    {
                        GameManager.Instance.SetState(GameManager.GameStatus.Lose);
                    }
                }
                if (hit.collider.CompareTag("FinishPoint"))
                {
                    ClearBrick();
                    GameManager.Instance.SetState(GameManager.GameStatus.Victory);
                }
            }
            lastCollider = hit.collider;
        }
    }

    private void AddBrick()
    {
        GameManager.Instance.AddCoin(1);
        Object playerBrick = Resources.Load("Prefabs/Objects/Brick2");
        jiao.position += new Vector3(0, 0.3f, 0);
        var brick = Instantiate(playerBrick, new Vector3(jiao.position.x, 2.5f, jiao.position.z), Quaternion.Euler(-90, 180, 0), jiao);
        listPlayerBrick.Add(brick);
    }

    private void RemoveBrick()
    {
        int index = listPlayerBrick.Count - 1;
        jiao.position -= new Vector3(0, 0.3f, 0);
        Object playerBrick = listPlayerBrick[index];
        listPlayerBrick.Remove(playerBrick);
        Destroy(playerBrick);
    }

    private void ClearBrick()
    {
        if (listPlayerBrick.Count > 0)
        {
            while (listPlayerBrick.Count > 0)
            {
                RemoveBrick();
            }
        }
    }
}
