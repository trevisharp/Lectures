using VisualLogic;
using VisualLogic.Elements;

public class SortLogicApp : LogicApp
{
    public int ArrayLength { get; set; }
    protected override DIBuilder DefineDependencyInjection(DIBuilder builder)
    {
        return builder
            .AddMethod("solution")
            .AddInstance(new VisualArray(100, 1100, ArrayLength));
    }

    protected override void LoadFromParams(params object[] args)
    {
        this.ArrayLength = (int)args[0];
    }

    protected override void SetRunHooks()
    {
        AddRunHook("solution", HookType.OnAppStart);
    }
}