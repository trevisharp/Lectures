using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

CollisionManager colman = new CollisionManager();

PointF[] polygon = new PointF[]
{
    new PointF(300, 300),
    new PointF(400, 400),
    new PointF(400, 500),
    new PointF(200, 500),
    new PointF(200, 400),
    new PointF(300, 300),
};
PointF p = new PointF(0, 0);

ApplicationConfiguration.Initialize();

Bitmap? bmp = null;
Graphics? g = null;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;

Timer tm = new Timer();
tm.Interval = 20;
tm.Tick += delegate
{
    g?.Clear(Color.White);
    run();
    draw();
    pb.Refresh();
};

Form form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;
form.KeyPreview = true;
form.KeyDown += delegate (object? sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};
form.Controls.Add(pb);
form.Load += delegate
{
    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    g.Clear(Color.White);
    pb.Image = bmp;
    tm.Start();
};
pb.MouseMove += (o, e) =>
{
    p = e.Location;
};

Application.Run(form);

void run()
{

}

void draw()
{
    g.DrawPolygon(Pens.Black, polygon);
    if (inpolygon(p, polygon))
    {
        var bs = bestside(polygon, p);
        g.DrawLine(new Pen(Color.Red, 5f), bs.Item1, bs.Item2);
    }
}

void write(string txt)
{
    g.DrawString(txt, SystemFonts.CaptionFont, Brushes.Black, p);
}



    bool linecollision(PointF p1, PointF q1, PointF p2, PointF q2)
    {
        float a1 = (q1.Y - p1.Y) / (q1.X - p1.X),
              a2 = (q2.Y - p2.Y) / (q2.X - p2.X),
              b1 = q1.Y - a1 * q1.X,
              b2 = q2.Y - a2 * q2.X;
        float x = 0, y = 0;
        x = -(b1 - b2) / (a1 - a2);
        y = a1 * x + b1;

        if (float.IsNaN(x))
        {
            if (float.IsInfinity(a1))
            {
                x = q1.X;
                y = a2 * x + b2;
            }
            else
            {
                x = q2.X;
                y = a1 * x + b1;
            }
        }
        
            
        float maxx1 = p1.X > q1.X ? p1.X : q1.X,
              minx1 = p1.X < q1.X ? p1.X : q1.X,
              maxx2 = p2.X > q2.X ? p2.X : q2.X,
              minx2 = p2.X < q2.X ? p2.X : q2.X,
              maxy1 = p1.Y > q1.Y ? p1.Y : q1.Y,
              miny1 = p1.Y < q1.Y ? p1.Y : q1.Y,
              maxy2 = p2.Y > q2.Y ? p2.Y : q2.Y,
              miny2 = p2.Y < q2.Y ? p2.Y : q2.Y;
            
        return minx1 <= x && x <= maxx1 && minx2 <= x && x <= maxx2 &&
               miny1 <= y && y <= maxy1 && miny2 <= y && y <= maxy2;
    }

    bool inpolygon(PointF p, PointF[] pts)
    {
        PointF q = new PointF(float.MaxValue, p.Y);
        int count = 0;
        for (int i = 0; i < pts.Length - 1; i++)
        {
            if (linecollision(p, q, pts[i], pts[i + 1]))
                count++;
        }
        return count % 2 == 1;
    }

    float distance(PointF p, PointF q, PointF r)
        => (float)(Math.Abs((q.X - p.X) * (p.Y - r.Y) - (p.X - r.X) * (q.Y - p.Y)) /
            Math.Sqrt((q.X - p.X)*(q.X - p.X) + (q.Y - p.Y) * (q.Y - p.Y)));
    
    (PointF, PointF) bestside(PointF[] polygon, PointF p)
    {
        float min = float.MaxValue;
        int index = -1;
        for (int i = 0; i < polygon.Length - 1; i++)
        {
            float newdist = distance(polygon[i], polygon[i + 1], p);
            if (newdist < min)
            {
                min = newdist;
                index = i;
            }
        }
        return (polygon[index], polygon[index + 1]);
    }
    