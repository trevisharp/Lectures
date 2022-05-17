using System.Drawing.Imaging;
using System.Runtime.InteropServices;

ApplicationConfiguration.Initialize();

Form form = new Form();
form.WindowState = FormWindowState.Maximized;
form.KeyPreview = true;
form.FormBorderStyle = FormBorderStyle.None;
form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

Bitmap bmp = new Bitmap("img.png");

#region  importante

var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
var array = new byte[data.Height * data.Stride];
Marshal.Copy(data.Scan0, array, 0, array.Length);

#region seu trabalho aqui

// for (int i = 0; i < array.Length; i += 3)
// {
//     byte b = array[i + 0],
//          g = array[i + 1],
//          r = array[i + 2];
        
//     byte temp = b;
//     b = g;
//     g = r;
//     r = temp;

        
//     array[i + 0] = b;
//     array[i + 1] = g;
//     array[i + 2] = r;
// }

Parallel.For(0, data.Height, j =>
{
    for (int i = j * data.Stride; i < (j + 1) * data.Stride; i += 3)
    {
        byte b = array[i + 0],
             g = array[i + 1],
            r = array[i + 2];
        
        byte temp = b;
        b = g;
        g = r;
        r = temp;
        
        array[i + 0] = b;
        array[i + 1] = g;
        array[i + 2] = r;
    }
});



#endregion


Marshal.Copy(array, 0, data.Scan0, array.Length);
bmp.UnlockBits(data);

#endregion

form.BackgroundImage = bmp;

Application.Run(form);