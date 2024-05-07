using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{

    public float hexSize;
    public float spacingX;
    public float spacingY;
    public float yPoss;
    private float offsetX;
    private float offsetY;

    void Start()
    {
        offsetX = transform.position.x; offsetY = transform.position.y;
        ArrangeGameObjectsInHexGrid();
    }

    void ArrangeGameObjectsInHexGrid()
    {
        int count = 0;
        int maxInColumn = 5;
        for (int y = 0; y < maxInColumn; y++)
        {
            for (int x = 0; x < transform.childCount; x += maxInColumn)
            {
                if (count >= transform.childCount)
                    break;

                Transform obj = transform.GetChild(count);
                float xPos = offsetX + hexSize * Mathf.Sqrt(spacingX) * (x / maxInColumn);
                float yPos = offsetY + hexSize * spacingY * y;

                if ((x / maxInColumn) % 2 == 1)
                {
                    yPos += hexSize * yPoss;//0.5
                }

                obj.position = new Vector3(xPos, yPos, 0);
                count++;
            }
        }
    }

}
