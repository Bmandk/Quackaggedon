using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Common : MonoBehaviour
{
    private static Common _instance;
    public static Common Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public float maxDistToSwim;
    public float maxDistanceToFood;

    public Collider2D pondEdges;

    public float duckRelaxSpeed;
    public float duckGetFoodSpeed;

    public Vector2 GetRandomPositionWithinPond(Vector3 currentPos)
    {
        return PointInArea();
        /*
        var minX = (currentPos.x - maxDistToSwim) < left.transform.position.x ? left.transform.position.x : (currentPos.x - maxDistToSwim);
        var maxX = (currentPos.x + maxDistToSwim) > right.transform.position.x ? right.transform.position.x : (currentPos.x + maxDistToSwim);

        var minY = (currentPos.y - maxDistToSwim) < bottom.transform.position.y ? bottom.transform.position.y : (currentPos.y - maxDistToSwim);
        var maxY = (currentPos.y + maxDistToSwim) > bottom.transform.position.y ? upper.transform.position.y : (currentPos.y + maxDistToSwim);

        float moveToX = UnityEngine.Random.Range(minX, maxX);
        float moveToY = UnityEngine.Random.Range(minY, maxY);

        Vector2 randomPos = new Vector2(moveToX, moveToY);
        return randomPos;
        */
    }

    public Vector3 PointInArea()
    {
        var bounds = pondEdges.bounds;
        var center = bounds.center;

        float x = 0;
        float y = 0;
        int attempt = 0;
        do
        {
            x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            attempt++;
        } while (!pondEdges.OverlapPoint(new Vector2(x, y)) || attempt <= 100);

        return new Vector3(x, y, 0);
    }

    public Vector3 GetCanvasPosFromWorldPos(Vector3 worldPos)
    {
        //Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(References.Instance.mainCam, worldPos);
        //return localCanvasPosition;
        return Vector3.zero;
    }
}
