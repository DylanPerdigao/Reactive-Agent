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


        wallAngle = blockDetector.GetAngleToClosestObstacle() % 360f;

        wallValue = weightWall * blockDetector.GetLinearOuput();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        anglem45 = Mathf.DeltaAngle(wallAngle + 135, resouceAngle);
        anglep45 = Mathf.DeltaAngle(wallAngle + 225, resouceAngle);

        if (Mathf.DeltaAngle(wallAngle + 135, resouceAngle) > Mathf.DeltaAngle(wallAngle + 225, resouceAngle))
        {
            applyForce(wallAngle + 225, wallValue);
        }
        else
        {
            applyForce(wallAngle + 135, wallValue);
        }



        
         



    }


}






