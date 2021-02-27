﻿using System;
using UnityEngine;
public class ObjectInfo : IEquatable<ObjectInfo>, IComparable<ObjectInfo>
{
    public float distance {get;}
    public float angle { get; }
    public GameObject obj;

    public ObjectInfo(float distance, float angle, GameObject obj)
    {
        this.distance = distance;
        this.angle = angle;
        this.obj = obj;
    }

    public bool Equals(ObjectInfo other)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(ObjectInfo other)
    {
        if (this.distance < other.distance)
        {
            return 1;
        }
        else if (this.distance == other.distance)
        {
            return 0;
        }
        return -1;
    }


    public void Paint()
    {
        obj.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    public void Unpaint()
    {
        obj.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}
