using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
{
    public float weightResource=1;
    public float resourceValue;


    public float weightWall=-1;
    public float wallValue;

    void Update()
    {

        // get sensor data
        float resouceAngle = resourcesDetector.GetAngleToClosestResource();

        //resourceValue = weightResource * resourcesDetector.GetLinearOuput();
        //resourceValue = weightResource * resourcesDetector.GetLogaritmicOutput();
        //resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
        resourceValue = weightResource * resourcesDetector.NonLimitsGauss();

        float wallAngle = blockDetector.GetAngleToClosestObstacle();

        //wallValue = weightWall * blockDetector.GetLinearOuput();
        //wallValue = weightWall * blockDetector.GetLogaritmicOutput();
        //wallValue = weightWall * blockDetector.GetGaussianOutput();
        wallValue = weightWall * blockDetector.NonLimitsGauss();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards
        applyForce(wallAngle, wallValue);


    }


}






