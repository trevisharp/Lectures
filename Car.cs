namespace CarControl;

using System;
using System.Collections.Generic;
using System.Drawing;

public class Car : IDrawable
{
    public Dictionary<string, Accelerometer> Accelerometers { get; private set; } 
        = new Dictionary<string, Accelerometer>();
    public Dictionary<string, InfraredSensor> LeftInfraredSensors { get; private set; }
        = new Dictionary<string, InfraredSensor>();
    public Dictionary<string, InfraredSensor> RightInfraredSensors { get; private set; }
        = new Dictionary<string, InfraredSensor>();
    public Dictionary<string, InfraredSensor> FrontInfraredSensors { get; private set; }
        = new Dictionary<string, InfraredSensor>();
    
    public PointF Location { get; private set; } = new PointF(100, 500);
    public SizeF Velocity { get; private set; } = SizeF.Empty;
    public SizeF Size { get; private set; } = new SizeF(50f, 100f);
    public float Angle { get; private set; } = 0f;
    public float RoadAngle { get; private set; } = 0f;
    public PointF Center => Location + Size / 2f;
    public float EngineVoltage { get; set; }
    public float StepAngle { get; set; }

    public void Draw(Graphics g)
    {
        rotate(Center, Angle);
        g.FillRectangle(Brushes.Red, new RectangleF(Location, Size));
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10, 
            Location.Y + Size.Height / 8, 
            Size.Width / 5, 
            Size.Height / 4), RoadAngle);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10 + Size.Width, 
            Location.Y + Size.Height / 8, 
            Size.Width / 5, 
            Size.Height / 4), RoadAngle);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10 + Size.Width, 
            Location.Y + 5 * Size.Height / 8, 
            Size.Width / 5,
            Size.Height / 4), 0f);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10, 
            Location.Y + 5 * Size.Height / 8, 
            Size.Width / 5,
            Size.Height / 4), 0f);

        int i = 0;
        foreach (var acc in Accelerometers)
            drawaccelerometer(i++);
        
        rotate(Center, -Angle);
        
        void rotate(PointF p, float angle)
        {
            g.TranslateTransform(p.X, p.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-p.X, -p.Y);
        }

        void drawroad(RectangleF rect, float angle)
        {
            rotate(rect.Location + rect.Size / 2f, angle);
            g.FillRectangle(Brushes.Black, rect);
            rotate(rect.Location + rect.Size / 2f, -angle);
        }

        void drawaccelerometer(int count)
        {
            g.FillRectangle(Brushes.Blue, 
                new RectangleF(
                    Location.X + Size.Width / 2 - 12.5f + (count % 2) * 15f,
                    Location.Y + Size.Height / 2 - 12.5f + (count / 2) * 15f,
                    10f,
                    10f
                    ));
        }
    }

    public void Update(float dt)
    {
        
    }
}