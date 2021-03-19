/**
 * Ana Rita Rodrigues - 2018284515
 * Bruno Faria - 2018295474
 * Dylan Perdigão - 2018233092	
 */

public class LinearRobotUnitBehaviour : RobotUnit
{
    public enum Function{Linear,Logarithmic,Gaussian};

    public Function wallFunction;
    public Function resourceFunction;

    public float weightResource=1;
    public float resourceValue;


    public float weightWall=-1;
    public float wallValue;

    /**
     * A cada frame, aplica força com a funcao especificada no Unity e o angulo calculado
     * para as resources e os obstaculos
     */
    void Update()
    {
        float resouceAngle = resourcesDetector.GetAngleToClosestResource();
        float wallAngle = blockDetector.GetAngleToClosestObstacle();

        switch (resourceFunction)
        {
            case Function.Linear:
                resourceValue = weightResource * resourcesDetector.GetLinearOuput();
                break;
            case Function.Logarithmic:
                resourceValue = weightResource * resourcesDetector.GetLogaritmicOutput();
                break;
            case Function.Gaussian:
                resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
                break;
        }
        switch (wallFunction)
        {
            case Function.Linear:
                wallValue = weightWall * blockDetector.GetLinearOuput();
                break;
            case Function.Logarithmic:
                wallValue = weightWall * blockDetector.GetLogaritmicOutput();
                break;
            case Function.Gaussian:
                wallValue = weightWall * blockDetector.GetGaussianOutput();
                break;
        }

        applyForce(resouceAngle, resourceValue);
        applyForce(wallAngle, wallValue);
    }
}






