namespace CarControl;

public class Logger
{
    private View view;
    public Logger(View view)
        => this.view = view;
    public void Print(object s)
    {
        view.Print(s.ToString());
    }

    public void Clear()
    {
        view.Clear();
    }
}