using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum DirectType { Forward, Back, Right, Left, None }

    [SerializeField] float speed = 10f;
    [SerializeField] Transform jiao;
    [SerializeField] LayerMask raycastLayer;

    private Vector3 startTouchPosition, lastTouchPosition, targetPosition;
    private Vector3 direction;
    private bool isMoving = false;
    private Collider lastCollider;
    private List<Collider> listBrick;
    private List<Object> listPlayerBrick;

    private static float unitSize = 1f;

    private void Start()
    {
        targetPosition = transform.position;
        listPlayerBrick = new List<Object>();
        listBrick = new List<Collider>();
        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        ControlPlayer();
    }

    private void Update()
    {
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
            if (Physics.Raycast(point + direction * unitSize + Vector3.up, Vector3.down, out hit, 5f))
            {
                if (hit.collider.CompareTag("Brick") || hit.collider.CompareTag("UnBrick"))
                {
                    point += direction * unitSize;
                }
                else break;
            }
        }

        return point;
    }

    private void ManageBrick()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down, Color.red);
        if (Physics.Raycast(ray, out hit, 5f, raycastLayer))
        {
            if (lastCollider != null
                && lastCollider != hit.collider)
            {
                if (lastCollider.CompareTag("UnBrick"))
                {
                    lastCollider.transform.GetChild(0).GetComponent<Brick>().EnableBrick(true);
                    RemoveBrick();
                }
                if (lastCollider.CompareTag("Brick"))
                {
                    lastCollider.transform.GetComponent<Brick>().EnableBrick(false);
                    AddBrick(lastCollider);
                }
            }
            lastCollider = hit.collider;
        }
    }

    private void AddBrick(Collider brick)
    {
        Debug.Log("Add Brick");
        jiao.position += new Vector3(0, 0.3f, 0);
        Object playerBrick = Resources.Load("Prefabs/Objects/Brick");
        Instantiate(playerBrick, new Vector3(jiao.position.x, 2.5f, jiao.position.z), Quaternion.Euler(-90, 0, 0), jiao);
        listPlayerBrick.Add(playerBrick);
        listBrick.Add(brick);

    }

    private void RemoveBrick()
    {
        if (listPlayerBrick.Count > 0)
        {
            int index = listPlayerBrick.Count - 1;
            jiao.position -= new Vector3(0, 0.3f, 0);
            //DestroyImmediate(listBrick[index]);
            //listBrick.RemoveAt(index);
            //listBrick.Remove(listBrick[index]);
        }
    }

    private void ClearBrick()
    {

    }
}
