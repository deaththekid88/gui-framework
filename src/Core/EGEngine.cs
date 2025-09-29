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

    public abstract void Initialize()
    {

    }

    public override void Run()
    {
        Initialize(); // 중복 호출 방지
        
        Thread engineThread = new Thread(() =>
        {
            base.Run(); // 고정된 FPS로 module 호출
        });
        engineThread.IsBackground = true;
        engineThread.Start();

        _backend?.Run(); // OpenTK GameWindow의 VSync에 따라 렌더링 루프 실행
    }

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        /*shader update*/
    }

    protected override void Render()
    {
        base.Render();

        /*shader render*/
    }

    private void UIRender()
    {
        
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
