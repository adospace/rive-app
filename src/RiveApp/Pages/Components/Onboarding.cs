using MauiReactor;
using MauiReactor.Animations;
using MauiReactor.Canvas;
using MauiReactor.Shapes;
using Microsoft.Maui.Devices;
using RiveApp.Models;
using RiveApp.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiveApp.Pages.Components;

class OnboardingState
{
    public double TranslationY { get; set; }

    public bool ShowLogin { get; set; }
}

class Onboarding : Component<OnboardingState>
{
    private bool _show;
    private Action _onClose;

    public Onboarding Show(bool show)
    {
        _show = show;
        return this;
    }

    public Onboarding OnClose(Action onClose)
    {
        _onClose = onClose;
        return this;
    }

    protected override void OnMounted()
    {
        State.TranslationY = _show ? -50 : -DeviceDisplay.Current.MainDisplayInfo.Height;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.TranslationY = _show ? -50 : -DeviceDisplay.Current.MainDisplayInfo.Height;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid("*", "*")
        {
            RenderBody(),

            new Login()
                .Show(State.ShowLogin)
                .OnClose(()=>SetState(s => s.ShowLogin = false)),
        }
        .TranslationY(State.TranslationY)
        .WithAnimation(easing: ExtendedEasing.OutQuart, duration: 600);
    }

    VisualNode RenderBody()
    {
        return new Border
        {
            new Grid("100, Auto, *, 100, 76", "*")
            {
                new Image("onboarding_background.png")
                    .Aspect(Aspect.Fill)
                    .GridRowSpan(5),

                RenderCloseButton(),

                new Label("Learn design & code")
                    .GridRow(1)
                    .Margin(40, 32, 120, 30)
                    .FontFamily("Poppins")
                    .FontSize(60)
                    .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                    .TextColor(Colors.Black),

                new Label("Don’t skip design. Learn design and code, by building real apps with React and Swift. Complete courses about the best tools.")
                    .FontSize(17)
                    .GridRow(2)
                    .Margin(40, 0, 120, 0)
                    .TextColor(Colors.Black),

                new StartCourseButton()
                    .OnTapped(()=>SetState(s => s.ShowLogin = true))
                    .GridRow(3),

                new Label("Purchase includes access to 30+ courses, 240+ premium tutorials, 120+ hours of videos, source files and certificates.")
                    .FontSize(12)
                    .GridRow(4)
                    .TextColor(Colors.Black)
                    .Margin(40, 0, 50, 31)

            }
        }
        .StrokeShape(new RoundRectangle().CornerRadius(30))
        .Margin(7, 0, 7, 10)
        .Background(Colors.White);
    }

    ImageButton RenderCloseButton() =>
        new ImageButton("close.png")
                .Aspect(Aspect.Center)
                .CornerRadius(18)
                .Shadow(new Shadow().Brush(Theme.ShadowBrush)
                    .Opacity(0.1f).Offset(5, 5))
                .HeightRequest(36)
                .WidthRequest(36)
                .HEnd()
                .VEnd()
                .Margin(24, 0)
                .BackgroundColor(Colors.White)
                .OnClicked(_onClose);
}

class StartCourseButtonState
{
    public bool IsPressed { get; set; }
    public double MainScale { get; set; } = 1.0f;
    public double BorderScaleX { get; set; }
}

class StartCourseButton : Component<StartCourseButtonState>
{
    private Action _onTapped;

    public StartCourseButton OnTapped(Action action)
    {
        _onTapped = action;
        return this;
    }

    public override VisualNode Render()
    {
        return new CanvasView()
        {
            new Align
            {
                new Group
                {
                    new Align
                    {
                        new Box()
                            .CornerRadius(10,20,20,20)
                            .Background(
                                new MauiControls.LinearGradientBrush(
                                    new MauiControls.GradientStopCollection
                                    {
                                        new MauiControls.GradientStop(Color.FromArgb("#F6AAA2"), 0.1535f),
                                        new MauiControls.GradientStop(Color.FromArgb("#FF557C"), 0.8795f),
                                    }))
                    }
                    .HStart()
                    .VStart()
                    .Height(63)
                    .Width(69),

                    new Picture("RiveApp.Resources.Images.start_course_button.png")
                        .Margin(8,8,0,0),

                    new Align ()
                    {
                        new ClipRectangle
                        {
                            new Box
                            {
                                new Align
                                {
                                    new Box()
                                        .CornerRadius(25)
                                        .BorderColor(Color.FromUint(0xFF6792FF).WithLuminosity(0.6f))
                                        .BackgroundColor(Colors.Transparent)
                                        .BorderSize(2)
                                }
                                .HCenter()
                                .Width(228)
                            }
                            .Padding(9,10,2,2)
                            .IsVisible(State.IsPressed)
                            ,
                        }
                            
                    }
                    .Width(() => 236.0f * (float)State.BorderScaleX)
                    .HCenter()
                    ,

                    new AnimationController
                    {
                        new SequenceAnimation
                        {
                            new DoubleAnimation()
                                .StartValue(1.0)
                                .TargetValue(0.8)
                                .Duration(200)
                                .Easing(Easing.CubicIn)
                                .OnTick(v=>SetState(s => s.MainScale = v, false)),

                            new DoubleAnimation()
                                .StartValue(0.8)
                                .TargetValue(1.0)
                                .Duration(200)
                                .Easing(Easing.CubicOut)
                                .OnTick(v=>SetState(s => s.MainScale = v, false)),

                            new DoubleAnimation()
                                .StartValue(0.0)
                                .TargetValue(1.0)
                                .Easing(Easing.CubicIn)
                                .Duration(300)
                                .OnTick(v=>SetState(s => s.BorderScaleX = v, false)),

                            new DoubleAnimation()
                                .StartValue(1.0)
                                .TargetValue(0.0)
                                .Easing(Easing.CubicIn)
                                .Duration(20)
                                .OnTick(v=>SetState(s => s.BorderScaleX = v, false))
                        }
                        .IterationCount(1)
                    }
                    .IsEnabled(State.IsPressed)
                    .OnIsEnabledChanged(isEnabled => SetState(s => s.IsPressed = isEnabled))
                }
            }
            .ScaleX(() => (float)State.MainScale)
            .ScaleY(() => (float)State.MainScale)
        }
        .HeightRequest(66)
        .WidthRequest(236)
        .BackgroundColor(Colors.Transparent)
        .OnTapped(() =>
        {
            SetState(s => s.IsPressed = true);
            MauiControls.Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(800), _onTapped);
        })
        .Margin(40, 23)
        .HStart();

        return new Grid("64", "236")
        {
            new Border()
                .StrokeShape(new RoundRectangle().CornerRadius(10, 20, 20, 20))
                .Background(
                    new MauiControls.LinearGradientBrush(
                        new MauiControls.GradientStopCollection
                        {
                            new MauiControls.GradientStop(Color.FromArgb("#F6AAA2"), 0.1535f),
                            new MauiControls.GradientStop(Color.FromArgb("#FF557C"), 0.8795f),
                        }))
                .HStart()
                .VStart()
                .HeightRequest(63)
                .WidthRequest(69),

            new Border()
                .StrokeShape(new RoundRectangle().CornerRadius(25))
                .Stroke(Colors.White)
                .BackgroundColor(Colors.Transparent)
                .Margin(8,8,0,0).Shadow(
                    new Shadow()
                        .Offset(5,5)
                        .Radius(15)
                .Brush(Theme.ShadowDark))
                ,

            new Image("start_course_button.png")
                .Margin(8,8,0,0),

            new Border
            {
                new Border()
                    .StrokeShape(new RoundRectangle().CornerRadius(25))
                    .Stroke(Color.FromUint(0xFF6792FF).WithLuminosity(0.6f))
                    .BackgroundColor(Colors.Transparent)
                    .WidthRequest(236)
                    .StrokeThickness(2)
                    ,
            }
            .BackgroundColor(Colors.Transparent)
            .Margin(3,3,-4,-4)
            .WidthRequest(() => 236 * State.BorderScaleX)
            .IsVisible(State.IsPressed),

            new AnimationController
            { 
                new SequenceAnimation
                {
                    new DoubleAnimation()
                        .StartValue(1.0)
                        .TargetValue(0.8)
                        .Duration(200)
                        .Easing(Easing.CubicIn)
                        .OnTick(v=>SetState(s => s.MainScale = v, false)),

                    new DoubleAnimation()
                        .StartValue(0.8)
                        .TargetValue(1.0)
                        .Duration(200)
                        .Easing(Easing.CubicOut)
                        .OnTick(v=>SetState(s => s.MainScale = v, false)),

                    new DoubleAnimation()
                        .StartValue(0.0)
                        .TargetValue(1.0)
                        .Easing(Easing.CubicIn)
                        .Duration(300)
                        .OnTick(v=>SetState(s => s.BorderScaleX = v, false)),

                    new DoubleAnimation()
                        .StartValue(1.0)
                        .TargetValue(0.0)
                        .Easing(Easing.CubicIn)
                        .Duration(20)
                        .OnTick(v=>SetState(s => s.BorderScaleX = v, false))
                }
                .IterationCount(1)
            }
            .IsEnabled(State.IsPressed)
            .OnIsEnabledChanged(isEnabled => SetState(s => s.IsPressed = isEnabled))
        }
        .Scale(()=> State.MainScale)
        .OnTapped(()=>
        {
            SetState(s => s.IsPressed = true);
            MauiControls.Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(800), _onTapped);
        })
        .Margin(40,23)
        .HStart();
    }
}
