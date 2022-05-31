namespace CarControl;

using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public static class GameManager
{
    public static void Run()
    {
        var program = typeof(GameManager).Assembly.GetTypes()
            .FirstOrDefault(t => t.Name == "Program");
        
        var run = program.GetRuntimeMethods()
            .FirstOrDefault(t => t.Name.ToLower().Contains("main") && t.Name.ToLower().Contains("run"));
        if (run == null)
        {
            showError("This program need a run function in Program.cs inside Main function");
            return;
        }

        var init = program.GetRuntimeMethods()
            .FirstOrDefault(t => t.Name.ToLower().Contains("main") && t.Name.ToLower().Contains("init"));
        if (init == null)
        {
            showError("This program need a init function in Program.cs inside Main function");
            return;
        }

        View view = new View();
        Car car = new Car();
        view.DrawableCollection.Add(car);
        
        // Initializate car
        Installer installer = new Installer(car);

        // Log
        Logger log = new Logger(view);
        view.Logger = log;
        
        // Prepare parameters
        List<object?> parametercall = new List<object?>();
        bool initializated = false;

        // Start application
        view.Open(() =>
        {
            if (!initializated)
            {
                foreach (var param in init.GetParameters())
                {
                    if (param.ParameterType == typeof(Installer))
                    {
                        parametercall.Add(installer);
                    }
                    else if (param.ParameterType == typeof(Installer))
                    {
                        parametercall.Add(log);
                    }
                }
                init.Invoke(null, parametercall.ToArray());
                parametercall.Clear();
                initializated = true;
                
                // Load run parameters
                foreach (var param in run.GetParameters())
                {
                    if (param.ParameterType == typeof(Controller))
                    {
                        parametercall.Add(new Controller(car));
                    }
                    else if (param.ParameterType == typeof(Accelerometer))
                    {
                        if (car.Accelerometers.ContainsKey(param.Name))
                            parametercall.Add(car.Accelerometers[param.Name]);
                        else parametercall.Add(null);
                    }
                    else if (param.ParameterType == typeof(InfraredSensor))
                    {
                        if (car.FrontInfraredSensors.ContainsKey(param.Name))
                            parametercall.Add(car.FrontInfraredSensors[param.Name]);
                        else if (car.LeftInfraredSensors.ContainsKey(param.Name))
                            parametercall.Add(car.LeftInfraredSensors[param.Name]);
                        else if (car.RightInfraredSensors.ContainsKey(param.Name))
                            parametercall.Add(car.RightInfraredSensors[param.Name]);
                        else parametercall.Add(null);
                    }
                    else if (param.ParameterType == typeof(Logger))
                    {
                        parametercall.Add(log);
                    }
                }
            }
            else
            {
                car.Update(0.025f);
                run.Invoke(null, parametercall.ToArray());
            }
        });
    }

    private static void showError(string message)
    {
        System.Windows.Forms.MessageBox.Show(message, "Error", 
            System.Windows.Forms.MessageBoxButtons.OK, 
            System.Windows.Forms.MessageBoxIcon.Error);
    }
}