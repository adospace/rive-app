using RiveApp.Models;
using RiveApp.Resources;
using System;
using MauiReactor;
using MauiReactor.Shapes;
using System.Linq;
using MauiReactor.Animations;
using Microsoft.Maui.Devices;

namespace RiveApp.Pages.Components;


class HomeMenuState
{
    public double TranslationX { get; set; } = 220;

    public double RotationY { get; set; } = -12;

    public double MarginLeft { get; set; } = -30.0;

    public double MainScale { get; set; } = 1.0;

    public double MainOpacity { get; set; } = 1.0;
}

partial class Home : Component<HomeMenuState>
{
    [Prop]
    private bool _isShown;

    [Prop]
    private Action _onShowOnboarding;

    [Prop]
    private bool _isMovedBack;

    protected override void OnMountedOrPropsChanged()
    {
        InitializeState();
        base.OnMountedOrPropsChanged();
    }

    void InitializeState()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            State.TranslationX = _isShown ? 0 : 220;
            State.MarginLeft = _isShown ? -30 : 0;
        }
        else
        {
            State.TranslationX = _isShown ? 0 : 300;
        }

        State.RotationY = _isShown ? 0.0 : -12;
        State.MainScale = _isMovedBack ? 0.95 : 1.0;
        State.MainOpacity = _isMovedBack ? 0.1 : 1.0;
    }

    public override VisualNode Render()
    {
        return Border(
            ScrollView(
                Grid("161, 309, 59, *", "*",
                    RenderUserButton(),

                    Label("Courses")
                        .FontAttributes(MauiControls.FontAttributes.Bold)
                        .FontSize(24)
                        .FontFamily("PoppinsBold")
                        .TextColor(Colors.Black)
                        .VEnd(),

                    ScrollView(
                        HStack(spacing: 20,
                            [.. CourseModel.Courses.Select(RenderCourse)]
                        )
                    )
                    .Orientation(ScrollOrientation.Horizontal)
                    .GridRow(1),

                    Label("Recent")
                        .FontAttributes(MauiControls.FontAttributes.Bold)
                        .FontSize(20)
                        .FontFamily("PoppinsBold")
                        .TextColor(Colors.Black)
                        .GridRow(2)
                        .VEnd(),

                    VStack(spacing: 20,
                        [.. CourseModel.CourseSections.Select(RenderCourseSection)]
                    )
                    .Margin(0,10,15,0)
                    .GridRow(3)
                )
            )
            .Orientation(ScrollOrientation.Vertical)
            .OniOS(_=>_.Margin(0, 50, 0, 0))
        )
        .Margin(State.MarginLeft, 0, 0, 0)
        .OniOS(_=>_.Margin(State.MarginLeft, -50, 0, -50))
        .RotationY(State.RotationY)
        .TranslationX(State.TranslationX)
        .Padding(-State.MarginLeft + 24, 0, 0, 0)
        .WithAnimation(easing: Easing.CubicIn, duration: 300)

        .AnchorX(0.5)
        .AnchorY(0.0)
        .Opacity(State.MainOpacity)
        .WithAnimation(easing: ExtendedEasing.InOutBack, duration: 300)

        .StrokeCornerRadius(30, 0, 30, 0)
        .Background(ApplicationTheme.Background)
        ;
    }

    ImageButton RenderUserButton() =>
        ImageButton("user_white.png")
            .Aspect(Aspect.Center)
            .CornerRadius(18)
            .Shadow(new Shadow().Brush(ApplicationTheme.ShadowBrush)
                .Opacity(0.1f).Offset(5, 5))
            .HeightRequest(36)
            .WidthRequest(36)
            .HEnd()
            .VStart()
            .Margin(24, 54)
            .BackgroundColor(Colors.White)
            .OnClicked(_onShowOnboarding);


    VisualNode RenderCourse(CourseModel model)
    {
        VisualNode RenderAvatar(string image)
            => Image(image)
                .Aspect(Aspect.AspectFit)
                .HeightRequest(44)
                .WidthRequest(44)
                .Clip(new EllipseGeometry().RadiusX(22).RadiusY(22).Center(22, 22))
                ;

        return Border(
            Grid("92, 44, *, 44", "*,44",
                Label(model.Title)
                    .FontAttributes (MauiControls.FontAttributes.Bold)
                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    .TextColor (Colors.White)
                    .VStart()
                    ,

                Image(model.Image)
                    .GridColumn(1)
                    .VStart(),

                Label(model.SubTitle)
                    .GridRow(1)
                    .TextColor(Colors.White.WithAlpha(0.5f))
                    .FontSize(15)
                    .Margin(0,6,0,0),

                Label(model.Caption.ToUpperInvariant())
                    .GridRow(2)
                    .GridColumnSpan(2)
                    .TextColor(Colors.White.WithAlpha(0.5f))
                    .FontSize(13)
                    .Margin(0,2,0,0)
                    .FontAttributes(MauiControls.FontAttributes.Bold)
                    .VStart(),

                HStack(spacing: -8,
                    RenderAvatar("avatar_4.png"),
                    RenderAvatar("avatar_5.png"),
                    RenderAvatar("avatar_6.png")
                )
                .GridColumnSpan(2)
                .GridRow(3)
            )
        )
        .Padding(30)
        .HeightRequest(309)
        .WidthRequest(260)
        .BackgroundColor(model.Color)
        .StrokeCornerRadius(DeviceInfo.Current.Platform == DevicePlatform.iOS ? 20 : 30)
        .Shadow(new Shadow().Opacity(0.2f).Offset(5, 5).Brush(ApplicationTheme.ShadowBrush));
    }

    VisualNode RenderCourseSection(CourseModel model)
    {
        return Border(
            Grid("*,*", "*,44",
                Label(model.Title)
                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    .TextColor (Colors.White)
                    ,

                Image(model.Image)
                    .GridColumn(1)
                    .GridRowSpan(2)
                    .VCenter(),

                Rectangle()
                    .VFill()
                    .HEnd()
                    .WidthRequest(1)
                    .GridRowSpan(2)
                    .Margin(15,5)
                    .Fill(ApplicationTheme.Background2.WithAlpha(0.5f))
                    ,

                Label(model.SubTitle)
                    .GridRow(1)
                    .GridColumnSpan(2)
                    .TextColor(Colors.White)
                    .FontSize(15)
                    .Margin(0,5,0,0)
            )
        )
        .Padding(30, 26)
        .HeightRequest(110)
        .BackgroundColor(model.Color)
        .StrokeCornerRadius(DeviceInfo.Current.Platform == DevicePlatform.iOS ? 15 : 20)
        ;
    }
}

class MenuButtonState
{
    public double TranslationX { get; set; } = 0;
}

partial class MenuButton : Component<MenuButtonState>
{
    [Prop]
    private Action _onToggle;

    [Prop]
    private bool _isShown;

    protected override void OnPropsChanged()
    {
        State.TranslationX = _isShown ? 180 : 0;
        base.OnPropsChanged();
    }

    public override VisualNode Render() 
        => Grid("44", "44",
            ContentView(
                RenderButton("menu_black.png", !_isShown)
            )
            .Opacity(!_isShown ? 1.0 : 0.0)
            .WithAnimation(easing: Easing.CubicIn, duration: 300),


            ContentView(
                RenderButton("close_black.png", _isShown)
            )
            .Opacity(_isShown ? 1.0 : 0.0)
            .WithAnimation(easing: Easing.CubicIn, duration: 300)
        )
        .VStart()
        .HStart()
        .Margin(24, 54)
        .TranslationX(State.TranslationX)
        .WithAnimation(easing: Easing.CubicIn, duration: 300);

    ImageButton RenderButton(string image, bool show) 
        => ImageButton(image)
            .Aspect(Aspect.Center)
            .CornerRadius(18)
            .Shadow(new Shadow().Brush(show ? ApplicationTheme.ShadowBrush : ApplicationTheme.ShadowDarkBrush)
                .Opacity(0.1f).Offset(5, 5))
            .HeightRequest(36)
            .WidthRequest(36)
            .BackgroundColor(Colors.White)
            .OnClicked(_onToggle);
}