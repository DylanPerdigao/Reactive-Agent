using UnityEngine;

public class BlockDetectorScript : DetectorScript
{

    public float angleToClosestObj;

    // Update is called once per frame
    void FixedUpdate()
    {
        // YOUR CODE HERE
        ObjectInfo anObject;
        anObject = GetClosestWall();
        if (anObject != null)
        {
            anObject.Paint();
            angleToClosestObj = anObject.angle;
            strength = 1.0f / (anObject.distance + 1.0f);
        }
    }

    public float GetAngleToClosestObstacle()
    {
        return angleToClosestObj;
    }

   
    public ObjectInfo[] GetVisibleWall()
    {
        return GetVisibleObjects("Wall", Color.red).ToArray();
    }

    public ObjectInfo GetClosestWall()
    {
        ObjectInfo[] a = GetVisibleWall();
        if (a.Length == 0)
        {
            return null;
        }

        return a[a.Length - 1];
    }
}