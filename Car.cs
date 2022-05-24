namespace CarControl;

using System.Drawing;

public class Car : IDrawable
{
    public PointF Location { get; set; }
    public SizeF Size { get; set; }
    public float Angle { get; set; }
    public float RoadAngle { get; set; }
    public PointF Center => Location + Size / 2f;

    public void Draw(Graphics g)
    {
        rotate(Center, Angle);
        g.FillRectangle(Brushes.Red, new RectangleF(Location, Size));
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10, 
            Location.Y + Size.Width / 8, 
            Size.Width / 5, 
            Size.Height / 4), RoadAngle);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10 + Size.Width, 
            Location.Y + Size.Width / 8, 
            Size.Width / 5, 
            Size.Height / 4), RoadAngle);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10 + Size.Width, 
            Location.Y + Size.Width / 8, 
            Size.Width / 5, 
            Size.Height / 4), 0f);
        
        drawroad(new RectangleF(
            Location.X - Size.Width / 10 + Size.Width, 
            Location.Y + Size.Width / 8, 
            Size.Width / 5, 
            Size.Height / 4), 0f);

            
        rotate(Center, -Angle);

        Angle++;
        
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
    }
}