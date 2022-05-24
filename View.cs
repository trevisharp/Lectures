namespace CarControl;

using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class View
{
    public List<IDrawable> DrawableCollection { get; set; } = new List<IDrawable>();
    public void Open()
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

        Bitmap bmp = null;
        Graphics g = null;

        Timer tm = new Timer();
        tm.Interval = 25;

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pb.Image = bmp;
            tm.Start();
        };

        tm.Tick += delegate
        {
            g.Clear(Color.White);

            foreach (var drawable in this.DrawableCollection)
            {
                drawable.Draw(g);
            }

            pb.Refresh();
        };

        Application.Run(form);
    }
}