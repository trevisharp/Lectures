using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public static class LogicApp
{
    public static void Run(int speed, int numbers)
    {
        MethodInfo solution = null;
        foreach (var type in typeof(LogicApp).Assembly.GetTypes())
        {
            if (type.Name.ToLower().Contains("program"))
            {
                foreach (var func in type.GetRuntimeMethods())
                {
                    if (func.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                    {
                        solution = func;
                        break;
                    }
                }
                break;
            }
        }

        Bitmap bmp = null;
        Graphics g = null;

        ApplicationConfiguration.Initialize();

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;

        Timer tm = new Timer();
        tm.Interval = 25;

        var form = new Form();
        form.FormBorderStyle = FormBorderStyle.None;
        form.WindowState = FormWindowState.Maximized;
        form.Controls.Add(pb);
        form.KeyPreview = true;
        form.KeyDown += (o, e) => 
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        };
        form.Load += async delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pb.Image = bmp;
            VisualArray array = new VisualArray(100, 1000, numbers, speed, g, bmp, pb);
            await Task.Factory.StartNew(() =>
                solution.Invoke(null, new object[] { array }));
        };
        Application.Run(form);
    }
}