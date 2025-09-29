using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using CoreFramework.Backend;

namespace GuiFramework.Backend;

public class EZBackendGL : EZBackend
{
    public EZBackendGL(NativeWindowSettings nativeWindowSettings, Updater updater, Renderer renderer, UIRenderer uiRenderer, ResizedWindow resizedWindow)
        : base(nativeWindowSettings, updater, renderer, uiRenderer, resizedWindow)
    {
        _updater = updater;
        _renderer = renderer;
        _uiRenderer = uiRenderer;
        _resizedWindow = resizedWindow;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0f, 0f, 0f, 0f);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        Dispose();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        _resizedWindow(e.Width, e.Height);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        BeginFrame();
        _renderer();
        _uiRenderer();
        EndFrame();

        Context.SwapBuffers(); // ✅ 보통 여기서 호출
    }

    protected override void BeginFrame()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }
    
    protected override void EndFrame()
    {
        
    }
}