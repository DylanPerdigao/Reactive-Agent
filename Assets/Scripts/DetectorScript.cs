using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public bool debugMode = true;

    public float strength;
    public float angle;

    public float angleOfSensors = 10f;
    public float rangeOfSensors = 10f;

    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;

    public int numObjects;

    public float std, mean;
    //limits of the detector
    public float infLimitX, supLimitX, infLimitY, supLimitY;

    /*
     * Start is called before the first frame update.
     */
    void Start()
    {
        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward;
    }
    /*
     * Calculates the strength for bounded linear graph
     * @return strength of the agent
     */
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
    /*
     * Calculates the strength for bounded gaussian graph
     * @return strength of the agent
     */
    public float GetGaussianOutput()
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

            return (float)(1 / (std * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(strength - mean, 2) / 2 * std * std));
        }

        return infLimitY;
    }
    /*
     * Calculates the strength for bounded logaritmic graph
     * @return strength of the agent
     */
    public float GetLogaritmicOutput()
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
            return (float)-Math.Log(strength);
        }
        return infLimitY;
    }
    /*
     * Gets the visible objects near the agent, if in debug mode there are colored rays that represents the objects seen by the agent
     * @param objectTag: x inferior limit 
     * @param color: x superior limit
     * @return Ordered list with nearest visible objects
     */
    public List<ObjectInfo> GetVisibleObjects(string objectTag,Color color)
    {
        RaycastHit hit;
        List<ObjectInfo> objectsInformation = new List<ObjectInfo>();

        for (int i = 0; i * angleOfSensors < 360f; i++)
        {
            if (Physics.Raycast(this.transform.position,
                Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd, out hit, rangeOfSensors))
            {
                if (hit.transform.gameObject.CompareTag(objectTag))
                {
                    if (debugMode)
                    {
                        Debug.DrawRay(this.transform.position,
                            Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd * hit.distance, color);
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

    /* 
     * Sets the limits of the detector
     * @param infLimitX: x inferior limit 
     * @param supLimitX: x superior limit 
     * @param infLimitY: y inferior limit 
     * @param supLimitY: y superior limit 
     */
    public void SetLimits(float infLimitX, float supLimitX, float infLimitY, float supLimitY)
    {
        this.infLimitX = infLimitX;
        this.supLimitX = supLimitX;
        this.infLimitY = infLimitY;
        this.supLimitY = supLimitY;
    }
}
