using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float resourceValue;
    public float resouceAngle;


    public float weightWall;
    public float wallValue;
    public float wallAngle;

    public float anglep45;
    public float anglem45;

    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource() % 360f;
        resourceValue = weightResource * resourcesDetector.GetLinearOuput();
        //resourceValue = weightResource * resourcesDetector.GetGaussianOutput();


        wallAngle = blockDetector.GetAngleToClosestObstacle() % 360f;
        wallValue = weightWall * blockDetector.GetLinearOuput();
        //wallValue = weightWall * blockDetector.GetLogaritmicOutput();
        //wallValue = weightWall * blockDetector.GetGaussianOutput();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards
        applyForce(wallAngle, wallValue);



        
         



    }


}






