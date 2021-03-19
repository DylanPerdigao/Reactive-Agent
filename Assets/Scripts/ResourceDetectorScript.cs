/**
 * Ana Rita Rodrigues - 2018284515
 * Bruno Faria - 2018295474
 * Dylan Perdigão - 2018233092	
 */
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDetectorScript : MonoBehaviour
{
    protected float angleOfSensors = 10f;
    protected float rangeOfSensors = 30f;
    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;
    protected float strength;
    protected float angle;
    protected int numObjects;
    public float std = 0.12f, mean = 0.5f, infLimitX = 0.25f, supLimitX = 0.75f, infLimitY = 0.05f, supLimitY = 0.6f;

    public bool debug_mode;

    // Start is called before the first frame update
    void Start()
    {
        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward;
    }

    /**
     * A cada update, calcula o angulo e a força
     */
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
    /**
     * Devolve o angulo do obstaculo mais proximo
     */
    public float GetAngleToClosestResource()
    {
        return angle;
    }
    /**
     * Devolve a forca linear, sem limites
     */
    public float NonLimitsLinear()
    {
        return strength;
    }
    /**
     * Devolve a forca logaritmica, sem limites
     */
    public float NonLimitsLog()
    {
        return (float) -Math.Log(strength);
    }
    /**
     * Devolve a forca gaussiana, sem limites
     */
    public float NonLimitsGauss()
    {
        return (float) (1 / (std * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(strength - mean, 2) / (2 * std * std)));
    }
    /**
     * Devolve a forca linear, com limite à esquerda e à direita
     */
    public float XLimitLinear()
    {
        if (strength >= infLimitX && strength <= supLimitX)
        {
            return strength;
        }

        return 0;
    }
    /**
     * Devolve a forca logaritmica, com limite à esquerda e à direita
     */
    public float XLimitLog()
    {
        float res = (float) -Math.Log(strength);
        if (strength >= infLimitX && strength <= supLimitX)
        {
            return res;
        }

        return 0;
    }
    /**
     * Devolve a forca gaussiana, com limite à esquerda e à direita
     */
    public float XLimitGauss()
    {
        float res = (float) (1 / (std * Math.Sqrt(2 * Math.PI)) *
                             Math.Exp(-Math.Pow(strength - mean, 2) / (2 * std * std)));
        if (strength >= infLimitX && strength <= supLimitX)
        {
            return res;
        }

        return 0;
    }
    /**
     * Devolve a forca liniear, com limite inferior e superior
     */
    public float YLimitLinear()
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
    /**
     * Devolve a forca logarimica, com limite inferior e superior
     */
    public float YLimitLog()
    {
        float res = (float) -Math.Log(strength);
        if (strength >= supLimitY)
        {
            return supLimitY;
        }

        if (strength <= infLimitY)
        {
            return infLimitY;
        }

        return res;
    }
    /**
     * Devolve a forca gaussiana, com limite inferior e superior
     */
    public float YLimitGauss()
    {
        float res = (float) (1 / (std * Math.Sqrt(2 * Math.PI)) *
                             Math.Exp(-Math.Pow(strength - mean, 2) / (2 * std * std)));
        if (strength >= supLimitY)
        {
            return supLimitY;
        }

        if (strength <= infLimitY)
        {
            return infLimitY;
        }

        return res;
    }
    /**
     * Devolve a forca linear, com limite inferior e superior
     * e com limite à esquerda e à direita
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
    /**
     * Devolve a forca logaritmica, com limite inferior e superior
     * e com limite à esquerda e à direita
     */
    public virtual float GetLogaritmicOutput()
    {
        if (strength >= infLimitX && strength <= supLimitX)
        {
            float res = (float)-Math.Log(strength);
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
    /**
     * Devolve a forca gaussiana, com limite inferior e superior
     * e com limite à esquerda e à direita
     */
    public virtual float GetGaussianOutput()
    {
        if (strength >= infLimitX && strength <= supLimitX)
        {
            float res = (float) (1 / (std * Math.Sqrt(2 * Math.PI)) *
                                 Math.Exp(-Math.Pow(strength - mean, 2) / (2 * std * std)));
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
    /**
     * Devolve um array com os recursos visiveis pelo agente
     */
    public ObjectInfo[] GetVisiblePickups()
    {
        return (ObjectInfo[]) GetVisibleObjects("Pickup").ToArray();
    }
    /**
     * Devolve o recurso visivel pelo agente mais proximo
     */
    public ObjectInfo GetClosestPickup()
    {
        ObjectInfo[] a = (ObjectInfo[]) GetVisibleObjects("Pickup").ToArray();
        if (a.Length == 0)
        {
            return null;
        }

        return a[a.Length - 1];
    }
    /**
     * Devolve uma lista com informacoes sobre os recursos mais proximos
     * Em debugMode, envia raios azuis contra os recursos visiveis
     */
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