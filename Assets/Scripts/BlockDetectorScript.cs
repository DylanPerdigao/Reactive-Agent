using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorScript : MonoBehaviour
{
    protected float angleOfSensors = 10f;
    protected float rangeOfSensors = 10f;
    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;
    protected float strength;
    protected float angleToClosestObj;
    protected int numObjects;
    public bool debugMode = true;

    public float std = 0.5f, mean = 0.12f, infLimitX = 0.25f, supLimitX = 0.75f, infLimitY = 0.05f, supLimitY = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward; ;
    }

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

    public float GetLinearOuput()
    {
        if (strength >= infLimitX && strength <= supLimitX)
        {
            if (strength >= supLimitY)
            {
                return supLimitY;
            }

            if (strength <= infLimitY)
            {
                return infLimitY;
            }

            return strength;
        }

        return infLimitY;
    }

    public virtual float GetGaussianOutput()
    {
        // YOUR CODE HERE
        if (strength >= infLimitX && strength <= supLimitX)
        {
            float res = (float) (1 / (std * Math.Sqrt(2 * Math.PI)) *
                                 Math.Exp(-Math.Pow(strength - mean, 2) / 2 * std * std));
            if (res >= supLimitY)
            {
                return supLimitY;
            }

            if (res <= infLimitY)
            {
                return infLimitY;
            }

            return res;
        }

        return infLimitY;
    }

    public virtual float GetLogaritmicOutput()
    {
        // YOUR CODE HERE
        if (strength >= infLimitX && strength <= supLimitX)
        {
            float res = (float) -Math.Log(strength);
            if (res >= supLimitY)
            {
                return supLimitY;
            }

            if (res <= infLimitY)
            {
                return infLimitY;
            }
            return res;
        }

        return infLimitY;
    }

    public ObjectInfo[] GetVisibleWall()
    {
        return (ObjectInfo[]) GetVisibleObjects("Wall").ToArray();
    }

    public ObjectInfo GetClosestWall()
    {
        ObjectInfo[] a = (ObjectInfo[]) GetVisibleObjects("Wall").ToArray();
        if (a.Length == 0)
        {
            return null;
        }

        return a[a.Length - 1];
    }

    public List<ObjectInfo> GetVisibleObjects(string objectTag)
    {
        RaycastHit hit;
        List<ObjectInfo> objectsInformation = new List<ObjectInfo>();

        for (int i = 0; i * angleOfSensors < 360f; i++)
        {
            if (Physics.Raycast(this.transform.position,
                Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd, out hit,
                rangeOfSensors))
            {
                if (hit.transform.gameObject.CompareTag(objectTag))
                {
                    if (debugMode)
                    {
                        Debug.DrawRay(this.transform.position,
                            Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd *
                            hit.distance, Color.red);
                    }

                    ObjectInfo info = new ObjectInfo(hit.distance, angleOfSensors * i + 90, hit.transform.gameObject);
                    info.Unpaint();
                    objectsInformation.Add(info);
                }
            }
        }

        objectsInformation.Sort();

        return objectsInformation;
    }
}