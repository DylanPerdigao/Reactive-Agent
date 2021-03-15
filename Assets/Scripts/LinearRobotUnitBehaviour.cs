using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float resourceValue;


    public float weightWall;
    public float wallValue;

    void Update()
    {

        // get sensor data
        float resouceAngle = resourcesDetector.GetAngleToClosestResource();

        //resourceValue = weightResource * resourcesDetector.GetLinearOuput();
        resourceValue = weightResource * resourcesDetector.GetGaussianOutput();


        float wallAngle = blockDetector.GetAngleToClosestObstacle();

        //wallValue = weightWall * blockDetector.GetLinearOuput();
        //wallValue = weightWall * blockDetector.GetLogaritmicOutput();
        wallValue = weightWall * blockDetector.GetGaussianOutput();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards
        applyForce(wallAngle, wallValue);


    }


}






