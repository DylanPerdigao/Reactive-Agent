using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDetectorScript : MonoBehaviour
{
    public float angleOfSensors = 10f;
    public float rangeOfSensors = 0.1f;
    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;
    public float strength;
    public float angle;
    public int numObjects;
    public float std = 0.5f, mean = 0.12f, infLimitX = 0.1f, supLimitX = 1f, infLimitY = 0.05f, supLimitY = 1f;

    public bool debug_mode;

    // Start is called before the first frame update
    void Start()
    {
        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward;
    }

    // FixedUpdate is called at fixed intervals of time
    void FixedUpdate()
    {
        ObjectInfo anObject;
        anObject = GetClosestPickup();
        if (anObject != null)
        {
            angle = anObject.angle;
            strength = 1.0f / (anObject.distance + 1.0f);
        }
        else
        {
            // no object detected
            strength = 0;
            angle = 0;
        }
    }

    public float GetAngleToClosestResource()
    {
        return angle;
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

    public ObjectInfo[] GetVisiblePickups()
    {
        return (ObjectInfo[]) GetVisibleObjects("Pickup").ToArray();
    }

    public ObjectInfo GetClosestPickup()
    {
        ObjectInfo[] a = (ObjectInfo[]) GetVisibleObjects("Pickup").ToArray();
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
                    if (debug_mode)
                    {
                        Debug.DrawRay(this.transform.position,
                            Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd *
                            hit.distance, Color.blue);
                    }

                    ObjectInfo info = new ObjectInfo(hit.distance, angleOfSensors * i + 90, hit.transform.gameObject);
                    objectsInformation.Add(info);
                }
            }
        }

        objectsInformation.Sort();

        return objectsInformation;
    }


    private void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}