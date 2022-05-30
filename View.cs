namespace CarControl;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class View
{
    public List<IDrawable> DrawableCollection { get; set; } = new List<IDrawable>();
    public Logger Logger { get; set; }

    Bitmap bmp = null;
    Graphics g = null;
    Timer tm = new Timer();

    public void Open(Action run)
    {
        ApplicationConfiguration.Initialize();

        var form = new Form();

        form.WindowState = FormWindowState.Maximized;
        form.KeyPreview = true;
        form.FormBorderStyle = FormBorderStyle.None;
        form.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        };

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pb.Image = bmp;
            tm.Interval = 25;
            tm.Start();
        };

        tm.Tick += delegate
        {
            run();
            
            g.Clear(Color.White);

            foreach (var drawable in this.DrawableCollection)
            {
                drawable.Draw(g);
            }
            g.DrawString(printtext, SystemFonts.CaptionFont, Brushes.Black, 
                new RectangleF(pb.Width * .8f, pb.Height * .6f, pb.Width * .2f, pb.Height * .4f));

            pb.Refresh();
        };

        Application.Run(form);
    }

    private string printtext = "";
    public void Print(string s)
    {
        printtext += s + "\n";
    }

    public void Clear()
    {
        printtext = string.Empty;
    }
}