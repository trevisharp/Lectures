using CarControl;

using System.Drawing;

View view = new View();


Car car = new Car();
car.Location = new PointF(100, 500);
car.Size = new Size(50, 100);
car.Angle = 0f;

view.DrawableCollection.Add(car);

view.Open();