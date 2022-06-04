using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

public class VisualArray
{
    public VisualArray(int min, int max, int values, int speed, Graphics g, Bitmap bmp, PictureBox pb)
    {
        data = new int[values];
        Random rand = new Random();
        for (int k = 0; k < values; k++)
            data[k] = rand.Next(min, max + 1);
        this.g = g;
        this.bmp = bmp;
        this.pb = pb;
        this.min = min;
        this.speed = speed;
        this.max = max;
    }
    private int[] data;
    private Graphics g;
    private Bitmap bmp;
    private PictureBox pb;
    private int min = 0;
    private int max = 0;
    
    private int speed = 50;

    public int this[int i]
    {
        get => data[i];
        set
        {
            data[i] = value;
            updated(i);
        }
    }

    public int Length => data.Length;

    private void updated(int index)
    {
        lock(g)
        {
            g.Clear(Color.White);
            var size = bmp.Width / (float)(3 * Length + 1);
            var ppv = (max - min) / (float)(bmp.Height - 100);
            int indx = 0;
            for (float i = size; i < bmp.Width && indx < Length; i += 3 * size, indx++)
            {
                var value = (data[indx] - min) / (float)(max - min);
                var color = new SolidBrush(Color.FromArgb(
                    (int)(255 * value),
                    0,
                    (int)(255 * (1 - value))
                ));
                g.FillRectangle(color, i, bmp.Height - data[indx] * ppv - 50, 2 * size, data[indx] * ppv + 5);
            }
            pb.Refresh();
            Thread.Sleep(speed);
        }
    }
}