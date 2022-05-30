namespace CarControl;

public class Controller
{
    private Car Car = null;
    public Controller(Car car)
        => this.Car = car;

    public float EngineVoltage
    {
        get => Car.EngineVoltage;
        set => Car.EngineVoltage = value;
    }

    public float StepAngle
    {
        get => Car.StepAngle;
        set => Car.StepAngle = value;
    }
}