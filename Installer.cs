namespace CarControl;
public class Installer
{
    private int totalSensors => 
        Car.LeftInfraredSensors.Count + 
        Car.RightInfraredSensors.Count + 
        Car.FrontInfraredSensors.Count;
    private Car Car = null;
    public Installer(Car car)
        => this.Car = car;

    public void InstallAccelerometer(string name)
    {
        if (Car.Accelerometers.Count == 4)
            throw new System.Exception("Max of 4 accelerometers");
        Car.Accelerometers.Add(name, new Accelerometer());
    }

    public void InstallLeftInfraredSensor(string name)
    {
        if (totalSensors == 10)
            throw new System.Exception("Max of 9 infrared sensors");
        Car.LeftInfraredSensors.Add(name, new InfraredSensor());
    }

    public void InstallRightInfraredSensors(string name)
    {
        if (totalSensors == 10)
            throw new System.Exception("Max of 9 infrared sensors");
        Car.RightInfraredSensors.Add(name, new InfraredSensor());
    }

    public void InstallFrontInfraredSensors(string name)
    {
        if (totalSensors == 10)
            throw new System.Exception("Max of 9 infrared sensors");
        Car.FrontInfraredSensors.Add(name, new InfraredSensor());
    }
}