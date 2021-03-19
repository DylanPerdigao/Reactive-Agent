/**
 * Ana Rita Rodrigues - 2018284515
 * Bruno Faria - 2018295474
 * Dylan Perdigão - 2018233092	
 */

public class LinearRobotUnitBehaviour : RobotUnit
{
    public enum Function{Linear,Logarithmic,Gaussian};
    public enum Limits{ AllLimits, XLimits,YLimits , NoLimits };

    public Function wallFunction;
    public Limits wallLimits;
    public Function resourceFunction;
    public Limits resourceLimits;

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
        //
        // ESCOLHE A FUNCAO DE ATIVACAO PARA OS RECURSOS
        //
        switch (resourceFunction)
        {
            case Function.Linear:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO LINEAR (PARA OS RECURSOS)
                //
                switch (resourceLimits)
                {
                    case Limits.NoLimits:
                        resourceValue = weightResource * resourcesDetector.NonLimitsLinear();
                        break;
                    case Limits.XLimits:
                        resourceValue = weightResource * resourcesDetector.XLimitLinear();
                        break;
                    case Limits.YLimits:
                        resourceValue = weightResource * resourcesDetector.YLimitLinear();
                        break;
                    case Limits.AllLimits:
                        resourceValue = weightResource * resourcesDetector.GetLinearOuput();
                        break;
                }
                break;
            case Function.Logarithmic:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO LOGARITMICA (PARA OS RECURSOS)
                //
                switch (resourceLimits)
                {
                    case Limits.NoLimits:
                        resourceValue = weightResource * resourcesDetector.NonLimitsLog();
                        break;
                    case Limits.XLimits:
                        resourceValue = weightResource * resourcesDetector.XLimitLog();
                        break;
                    case Limits.YLimits:
                        resourceValue = weightResource * resourcesDetector.YLimitLog();
                        break;
                    case Limits.AllLimits:
                        resourceValue = weightResource * resourcesDetector.GetLogaritmicOutput();
                        break;
                }
                break;
            case Function.Gaussian:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO GAUSSIANA (PARA OS RECURSOS)
                //
                switch (resourceLimits)
                {
                    case Limits.NoLimits:
                        resourceValue = weightResource * resourcesDetector.NonLimitsGauss();
                        break;
                    case Limits.XLimits:
                        resourceValue = weightResource * resourcesDetector.XLimitGauss();
                        break;
                    case Limits.YLimits:
                        resourceValue = weightResource * resourcesDetector.YLimitGauss();
                        break;
                    case Limits.AllLimits:
                        resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
                        break;
                }
                break;
        }
        //
        // ESCOLHE A FUNCAO DE ATIVACAO PARA OS OBSTACULOS
        //
        switch (wallFunction)
        {
            case Function.Linear:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO LINEAR (PARA OS OBSTACULOS)
                //
                switch (wallLimits)
                {
                    case Limits.NoLimits:
                        wallValue = weightWall * blockDetector.NonLimitsLinear();
                        break;
                    case Limits.XLimits:
                        wallValue = weightWall * blockDetector.XLimitLinear();
                        break;
                    case Limits.YLimits:
                        wallValue = weightWall * blockDetector.YLimitLinear();
                        break;
                    case Limits.AllLimits:
                        wallValue = weightWall * blockDetector.GetLinearOuput();
                        break;
                }
                break;
            case Function.Logarithmic:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO LOGARITMICA (PARA OS OBSTACULOS)
                //
                switch (wallLimits)
                {
                    case Limits.NoLimits:
                        wallValue = weightWall * blockDetector.NonLimitsLog();
                        break;
                    case Limits.XLimits:
                        wallValue = weightWall * blockDetector.XLimitLog();
                        break;
                    case Limits.YLimits:
                        wallValue = weightWall * blockDetector.YLimitLog();
                        break;
                    case Limits.AllLimits:
                        wallValue = weightWall * blockDetector.GetLogaritmicOutput();
                        break;
                }
                break;
            case Function.Gaussian:
                //
                // ESCOLHE OS LIMITES PARA A FUNCAO GAUSSIANA (PARA OS OBSTACULOS)
                //
                switch (wallLimits)
                {
                    case Limits.NoLimits:
                        wallValue = weightWall * blockDetector.NonLimitsGauss();
                        break;
                    case Limits.XLimits:
                        wallValue = weightWall * blockDetector.XLimitGauss();
                        break;
                    case Limits.YLimits:
                        wallValue = weightWall * blockDetector.YLimitGauss();
                        break;
                    case Limits.AllLimits:
                        wallValue = weightWall * blockDetector.GetGaussianOutput();
                        break;
                }
                break;
        }

        applyForce(resouceAngle, resourceValue);
        applyForce(wallAngle, wallValue);
    }
}






