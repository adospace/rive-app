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

class Home : Component<HomeMenuState>
{
    private bool _isShown;
    private Action _onShowOnboarding;
    private bool _isMovedBack;

    public Home IsHidden(bool isHidden)
    {
        _isShown = !isHidden;
        return this;
    }

    public Home IsMovedBack(bool isMovedBack)
    {
        _isMovedBack = isMovedBack;
        return this;
    }

    public Home OnShowOnboarding(Action action)
    {
        _onShowOnboarding = action;
        return this;
    }

    protected override void OnMounted()
    {
        InitializeState();
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        InitializeState();

        base.OnPropsChanged();
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
        return new Border
        {
            new Grid("161, 309, 59, *", "*")
            {
                RenderUserButton(),

                new Label("Courses")
                    .FontAttributes(MauiControls.FontAttributes.Bold)
                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    .TextColor(Colors.Black)
                    .VEnd(),

                new ScrollView
                {
                    new HStack(spacing: 20)
                    {
                        CourseModel.Courses.Select(RenderCourse)
                    }
                }
                .Orientation(ScrollOrientation.Horizontal)
                .GridRow(1),

                new Label("Recent")
                    .FontAttributes(MauiControls.FontAttributes.Bold)
                    .FontSize(20)
                    .FontFamily("PoppinsBold")
                    .TextColor(Colors.Black)
                    .GridRow(2)
                    .VEnd(),

                new ScrollView
                {
                    new VStack(spacing: 20)
                    {
                        CourseModel.CourseSections.Select(RenderCourseSection)
                    }
                }
                .Margin(0,10,15,0)
                .Orientation(ScrollOrientation.Vertical)
                .GridRow(3),
            }
            .OniOS(_=>_.Margin(0, 50, 0, 0))
        }
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

        .StrokeShape(new RoundRectangle().CornerRadius(30, 0, 30, 0))
        .Background(Theme.Background)
        ;
    }

    ImageButton RenderUserButton() =>
        new ImageButton("user_white.png")
            .Aspect(Aspect.Center)
            .CornerRadius(18)
            .Shadow(new Shadow().Brush(Theme.ShadowBrush)
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
            => new Image(image)
                .Aspect(Aspect.AspectFit)
                .Clip(new EllipseGeometry().RadiusX(22).RadiusY(22).Center(22, 22))
                ;

        return new Border
        {
            new Grid("64, 44, *, 44", "*,44")
            {
                new Label(model.Title)
                    .FontAttributes (MauiControls.FontAttributes.Bold)
                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    .TextColor (Colors.White)
                    ,

                new Image(model.Image)
                    .GridColumn(1)
                    .VStart(),

                new Label(model.SubTitle)
                    .GridRow(1)
                    .TextColor(Colors.White.WithAlpha(0.5f))
                    .FontSize(15)
                    .Margin(0,6,0,0),

                new Label(model.Caption.ToUpperInvariant())
                    .GridRow(2)
                    .GridColumnSpan(2)
                    .TextColor(Colors.White.WithAlpha(0.5f))
                    .FontSize(13)
                    .Margin(0,2,0,0)
                    .FontAttributes(MauiControls.FontAttributes.Bold)
                    .VStart(),

                new HStack(spacing: -8)
                {
                    new []
                    {
                        RenderAvatar("avatar_4.jpg"),
                        RenderAvatar("avatar_5.jpg"),
                        RenderAvatar("avatar_6.jpg"),
                    }
                }
                .GridColumnSpan(2)
                .GridRow(3)
            }
        }
        .Padding(30)
        .HeightRequest(309)
        .WidthRequest(260)
        .BackgroundColor(model.Color)
        .StrokeShape(new RoundRectangle().CornerRadius(30))
        .Shadow(new Shadow().Opacity(0.2f).Offset(5, 5).Brush(Theme.ShadowBrush));
    }

    VisualNode RenderCourseSection(CourseModel model)
    {
        return new Border
        {
            new Grid("*,*", "*,44")
            {
                new Label(model.Title)
                    //.FontAttributes (MauiControls.FontAttributes.Bold)
                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    .TextColor (Colors.White)                    .FontSize(24)
                    .FontFamily("PoppinsBold")
                    ,

                new Image(model.Image)
                    .GridColumn(1)
                    .GridRowSpan(2)
                    .VCenter(),

                new Rectangle()
                    .VFill()
                    .HEnd()
                    .WidthRequest(1)
                    .GridRowSpan(2)
                    .Margin(15,5)
                    .Fill(Theme.Background2.WithAlpha(0.5f))
                    ,

                new Label(model.SubTitle)
                    .GridRow(1)
                    .GridColumnSpan(2)
                    .TextColor(Colors.White)
                    .FontSize(15)
                    .Margin(0,5,0,0)
            }
        }
        .Padding(30, 26)
        .HeightRequest(110)
        .BackgroundColor(model.Color)
        .StrokeShape(new RoundRectangle().CornerRadius(20));
    }
}

class MenuButtonState
{
    public double TranslationX { get; set; } = 0;
}

class MenuButton : Component<MenuButtonState>
{
    private Action _toggleAction;
    private bool _isShown;

    public MenuButton OnToggle(Action action)
    {
        _toggleAction = action;
        return this;
    }

    public MenuButton IsSideMenuShown(bool isShown)
    {
        _isShown = isShown;
        return this;
    }

    protected override void OnPropsChanged()
    {
        State.TranslationX = _isShown ? 180 : 0;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid("44", "44")
        {
            new ContentView
            {
                RenderButton("menu_black.png", !_isShown)
            }
            .Opacity(!_isShown ? 1.0 : 0.0)
            .WithAnimation(easing: Easing.CubicIn, duration: 300),


            new ContentView
            {
                RenderButton("close_black.png", _isShown)
            }
            .Opacity(_isShown ? 1.0 : 0.0)
            .WithAnimation(easing: Easing.CubicIn, duration: 300),
        }
        .VStart()
        .HStart()
        .Margin(24, 54)
        .TranslationX(State.TranslationX)
        .WithAnimation(easing: Easing.CubicIn, duration: 300);

    }

    ImageButton RenderButton(string image, bool show) =>
        new ImageButton(image)
                .Aspect(Aspect.Center)
                .CornerRadius(18)
                .Shadow(new Shadow().Brush(show ? Theme.ShadowBrush : Theme.ShadowDarkBrush)
                    .Opacity(0.1f).Offset(5, 5))
                .HeightRequest(36)
                .WidthRequest(36)
                .BackgroundColor(Colors.White)
                .OnClicked(_toggleAction);
}