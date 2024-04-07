using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform topRight;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Vector2 mapBottomLeft, mapSize;

    Rect border
    {
        get
        {
            return new Rect(mapBottomLeft, mapSize);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (CheckInBound())
        {
            transform.position = new Vector3(player.position.x, player.position.y,transform.position.z);
        }
    }

    bool CheckInBound()
    {
        Vector2 newBottomLeft = player.position + bottomLeft.position;
        Vector2 newTopRight = player.position + topRight.position;
        if(!border.Contains(newTopRight) || !border.Contains(newBottomLeft))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
