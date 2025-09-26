using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using CoreFramework.Platform;
using CoreFramework.Backend;
using GuiFramework.Backend;
using GuiFramework.Event;

namespace GuiFramework.Core;

public class EGEngine : EZEngine
{
    private static EGEngine? _instance;
    private EZBackendGL? _backend;

    public static EGEngine Instance => _instance ??= new EGEngine();

    public EGEngine()
    {
        var nativeSettings = new NativeWindowSettings()
        {
            Size = new OpenTK.Mathematics.Vector2i(800, 600),
            Title = "EZEngine OpenGL Backend",
            Flags = ContextFlags.ForwardCompatible,
            TransparentFramebuffer = true
        };
        _backend = new EZBackendGL(nativeSettings,
            new Updater(Update), new Renderer(Render), new UIRenderer(UIRender), new ResizedWindow(ResizedWindow));

    }

    public override void Initialize()
    {

    }

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Render()
    {
        base.Render();
    }

    protected override void UIRender()
    {
        base.UIRender();
    }

    protected override void OnRun()
    {
        _backend.Run();
    }
    
    private void ResizedWindow(float width, float height)
    {
        EventBus.Publish(new ResizedWindowEvent
        {
            width = width,
            height = height
        });
    }
}
