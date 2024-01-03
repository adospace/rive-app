using MauiReactor;
using MauiReactor.Animations;
using MauiReactor.Canvas;
using MauiReactor.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Platform;
using RiveApp.Controls;
using RiveApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiveApp.Pages.Components;

[Scaffold(typeof(SkiaSharp.Extended.UI.Controls.SKSurfaceView))]
partial class SKSurfaceView { }

[Scaffold(typeof(SkiaSharp.Extended.UI.Controls.SKAnimatedSurfaceView))]
partial class SKAnimatedSurfaceView { }

[Scaffold(typeof(SkiaSharp.Extended.UI.Controls.SKLottieView))]
partial class SKLottieView { }

class LoginState
{
    public string Email { get; set; }

    public string Password { get; set; }

    public double TranslationY { get; set; }

    public bool LoggingIn { get; set; }

    public bool LoginFailing { get; set; }
}

partial class Login : Component<LoginState>
{
    [Prop]
    private Action _onClose;

    [Prop]
    private bool _show;

    protected override void OnMountedOrPropsChanged()
    {
        State.TranslationY = _show ? 0 : -DeviceDisplay.Current.MainDisplayInfo.Height;
        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return Grid("*", "*",
            Border(
                Grid("Auto, 78, Auto, Auto, *, Auto, Auto, 88", "*",
                    Label("Sign in")
                        .FontFamily("Poppins")
                        .FontSize(34)
                        .TextColor(Colors.Black)
                        .FontAttributes(MauiControls.FontAttributes.Bold)
                        .HCenter(),

                    new CanvasView()
                    {
                        new Text("Access to 240+ hours of content. \r\nLearn design and code, by building real apps with React and Swift.")
                            .FontSize(15)
                            .FontColor(Colors.Black.WithAlpha(0.5f))
                            .FontWeight(700)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                    }
                    .Margin(0, 24, 0, 0)
                    .GridRow(1),

                    RenderEntry("Email", State.Email, v => State.Email = v, !State.LoggingIn)
                        .GridRow(2)
                        .Margin(0, 24, 0, 0),

                    RenderEntry("Password", State.Password, v => State.Password = v, !State.LoggingIn, "Forgot password")
                        .GridRow(3)
                        .Margin(0, 24, 0, 24),

                    RenderSeparator()
                        .GridRow(5),

                    new LoginButton()
                        .OnTapped(OnSignin)
                        .GridRow(4),

                    Label("Sign up with Email, Apple or Google")
                        .FontSize(13)
                        .TextColor(Colors.Black.WithAlpha(0.5f))
                        .Margin(0, 24, 0, 0)
                        .HCenter()
                        .GridRow(6),

                    FlexLayout(
                        Image("logo_email.png"),
                        Image("logo_apple.png"),
                        Image("logo_google.png")
                    )
                    .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceBetween)
                    .Margin(0, 24, 0, 0)
                    .GridRow(7)
                )
            )
            .StrokeCornerRadius(20)
            .Shadow(Shadow()
                .Brush(Colors.Black)
                .Opacity(1f)
                .Offset(5, 5)
                .Radius(200))
            .Background(new LinearGradient(135.0, (Colors.White, 0), (Colors.White.WithAlpha(0.9f), 1.0f)))
            .Padding(30, 48, 30, 41)
            .Margin(0, 0, 0, 18)
            ,

            RenderCloseButton(),

            RenderLoggingIn()
        )
        .TranslationY(State.TranslationY)
        .WithAnimation(easing: ExtendedEasing.InOutQuart, duration: 600)
        .Margin(16, 76, 16, 18);
    }

    ImageButton RenderCloseButton() =>
        ImageButton("close_black.png")
            .Aspect(Aspect.Center)
            .CornerRadius(18)
            .Shadow(new Shadow().Brush(Colors.Black)
                .Opacity(0.2f)
                .Offset(0, 5)
                .Radius(10)
            )
            .HeightRequest(36)
            .WidthRequest(36)
            .HCenter()
            .VEnd()
            .BackgroundColor(Colors.White)
            .OnClicked(_onClose);

    static Grid RenderEntry(string label, string value, Action<string> onSetValueAction, bool isEnabled, string secondaryLabel = null)
        => Grid("26, 50", "*, *",
            Label(label)
                .FontSize(15)
                .TextColor(Colors.Black.WithAlpha(0.5f))
                .VStart()
                ,

            Label(secondaryLabel)
                .FontSize(15)
                .VStart()
                .HEnd()
                .TextColor(Colors.Black.WithAlpha(0.3f))
                .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                .GridColumnSpan(2)
                ,

            Border()
                .GridRow(1)
                .GridColumnSpan(2)
                .StrokeShape(new RoundRectangle().CornerRadius(15))
                .Stroke(
                    new MauiControls.LinearGradientBrush(
                        new MauiControls.GradientStopCollection
                        {
                            new MauiControls.GradientStop(Color.FromArgb("#C2CFF0").WithAlpha(0.55f), 0),
                            new MauiControls.GradientStop(Color.FromArgb("#98A4C1").WithAlpha(0.20f), 1),
                        })),

            Image("email.png")
                .WidthRequest(44)
                .GridRow(1)
                .Margin(8)
                .HStart(),

            new BorderlessEntry()
                .Text(value)
                .IsEnabled(isEnabled)
                .OnTextChanged(onSetValueAction)
                .Margin(8 + 24 + 12, 8, 8, 8)
                .GridRow(1)
                .GridColumnSpan(2)
        );

    static Grid RenderSeparator()
        => Grid("16", "*,Auto,*",
            Border()
                .HeightRequest(2)
                .VCenter()
                .BackgroundColor(Colors.Black.WithAlpha(0.1f)),

            Label("OR")
                .FontSize(13)
                .TextColor(Colors.Black.WithAlpha(0.5f))
                .Margin(8, 0)
                .VCenter()
                .GridColumn(1),

            Border()
                .HeightRequest(2)
                .VCenter()
                .BackgroundColor(Colors.Black.WithAlpha(0.1f))
                .GridColumn(2)
        );

    SKLottieView RenderLoggingIn()
        => new SKLottieView()
            .Source(new SkiaSharp.Extended.UI.Controls.SKFileLottieImageSource()
            {
                File = State.LoginFailing ? "login_failed.json" : "login_successful.json"
            })
            .IsAnimationEnabled(State.LoggingIn)
            .IsVisible(State.LoggingIn)
            .RepeatCount(0)
            .HeightRequest(200)
            .WidthRequest(200)
            .HeightRequest(200)
            .VCenter()
            .HCenter();

    void OnSignin()
    {
        if (string.IsNullOrWhiteSpace(State.Email) ||
            string.IsNullOrWhiteSpace(State.Password))
        {
            SetState(s => s.LoggingIn = s.LoginFailing = true);
            MauiControls.Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(2000),
                () => SetState(s =>
                {
                    s.LoggingIn = s.LoginFailing = false;
                    s.Email = s.Password = string.Empty;
                }));
        }
        else
        {
            SetState(s =>
            {
                s.LoggingIn = true;
                s.LoginFailing = false;
            });

            MauiControls.Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1000),
            () =>
            {
                State.LoggingIn = State.LoginFailing = false;
                State.Email = State.Password = string.Empty;
                _onClose?.Invoke();
            });
        }

    }
}

class LoginButtonState
{
    public bool IsPressed { get; set; }
    public double Scale { get; set; } = 1.0;
    public double Opacity { get; set; } = 1.0;
}

partial class LoginButton : Component<LoginButtonState>
{
    [Prop]
    private Action _onTapped;

    public override VisualNode Render()
    {
        return Grid("56", "*",
            Border(
                Grid("*", "24, *",
                    Image("arrow_right.png")
                        ,
                    Label("Sign in")
                        .Margin(8, 0, 0, 0)
                        .FontSize(17)
                        .GridColumn(1)
                        .TextColor(Colors.White)
                )
                .HCenter()
            )
            .StrokeShape(new RoundRectangle().CornerRadius(10, 25, 25, 25))
            .Shadow(new Shadow()
                .Brush(Color.FromRgba(247, 125, 152, (int)(255.0 * 0.3)))
                .Offset(0, 10)
                .Radius(20))
            .BackgroundColor(Color.FromArgb("#F77D8E"))
            .Padding(20, 16)
            .Opacity(() => State.Opacity)
            .Scale(() => State.Scale),

            new AnimationController
            {
                new ParallelAnimation
                {
                    new SequenceAnimation
                    {
                        new DoubleAnimation()
                            .StartValue(1.0)
                            .TargetValue(0.8)
                            .Duration(60)
                            .Easing(Easing.CubicIn)
                            .OnTick(v=>SetState(s => s.Scale = v, false)),

                        new DoubleAnimation()
                            .StartValue(0.8)
                            .TargetValue(1.0)
                            .Duration(60)
                            .Easing(Easing.CubicOut)
                            .OnTick(v=>SetState(s => s.Scale = v, false)),
                    },

                    new SequenceAnimation
                    {
                        new DoubleAnimation()
                            .StartValue(1.0)
                            .TargetValue(0.5)
                            .Easing(Easing.CubicIn)
                            .Duration(60)
                            .OnTick(v=>SetState(s => s.Opacity = v, false)),

                        new DoubleAnimation()
                            .StartValue(0.5)
                            .TargetValue(1.0)
                            .Easing(Easing.CubicIn)
                            .Duration(60)
                            .OnTick(v=>SetState(s => s.Opacity = v, false))
                    }
                }
                .IterationCount(1)
            }
            .IsEnabled(State.IsPressed)
            .OnIsEnabledChanged(isEnabled => SetState(s => s.IsPressed = isEnabled))

        )
        .OnTapped(() =>
        {
            MauiControls.Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), _onTapped);
            SetState(s => s.IsPressed = true);
        })
        ;
    }
}