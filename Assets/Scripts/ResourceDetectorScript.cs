using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDetectorScript : DetectorScript
{
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

    public ObjectInfo[] GetVisiblePickups()
    {
        return (ObjectInfo[])GetVisibleObjects("Pickup",Color.blue).ToArray();
    }

    public ObjectInfo GetClosestPickup()
    {
        ObjectInfo[] a = (ObjectInfo[])GetVisibleObjects("Pickup",Color.blue).ToArray();
        if (a.Length == 0)
        {
            return null;
        }

        return a[a.Length - 1];
    }

    private void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}