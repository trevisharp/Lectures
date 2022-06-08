using VisualLogic;
using VisualLogic.Elements;

public class MaxLogicApp : LogicApp
{
    protected override DIBuilder DefineDependencyInjection(DIBuilder builder)
    {
        return builder
            .AddMethod("solution")
            .AddInstance(new VisualSurface(0, 5, 0, 5, 0.1, 2, 3));
    }

    protected override void LoadFromParams(params object[] args)
    {
        
    }

    protected override void SetRunHooks()
    {
        AddRunHook("solution", HookType.OnAppStart);
    }
}