using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float resourceValue;
    public float resouceAngle;


    public float wallValue;
    public float wallAngle;

    public float directionAngle;
    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource() % 360f;

        resourceValue = weightResource * resourcesDetector.GetLinearOuput();


        wallAngle = blockDetector.GetAngleToClosestObstacle() % 360f;

        wallValue = weightResource * blockDetector.GetLinearOuput();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards
        applyForce((wallAngle + 180) % 360, wallValue); 



    }


}






