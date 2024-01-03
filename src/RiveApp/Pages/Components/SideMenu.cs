using RiveApp.Models;
using RiveApp.Resources;
using System;
using MauiReactor;
using MauiReactor.Shapes;
using MauiReactor.Animations;
using MauiReactor.Canvas;

namespace RiveApp.Pages.Components;

class SideMenuState
{
    public double Opacity { get; set; } = 0.0;

    public double RotationY { get; set; } = 10;

    public double TranslationX { get; set; } = -250;

    public CommandMenuItem SelectedMenuItem { get; set; }
}

partial class SideMenu : Component<SideMenuState>
{
    [Prop]
    private bool _isShown;

    protected override void OnMountedOrPropsChanged()
    {
        State.TranslationX = _isShown ? 0 : -250;
        State.Opacity = _isShown ? 1.0 : 0.0;
        State.RotationY = _isShown ? 0.0 : 10;
        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return Grid("39, 300, *, 120", "250",
            SideMenu.RenderHeader(),

            RenderBrowse(),

            RenderHistory(),

            RenderBottom()
        )
        .Padding(0, 60)
        .RotationY(State.RotationY)
        .TranslationX(State.TranslationX)
        .Opacity(State.Opacity)
        .WithAnimation(easing: Easing.CubicIn, duration: 300)
        .BackgroundColor(Theme.Background2)
        .HStart();
    }

    static Grid RenderHeader() 
        => Grid("20, 18", "36, *",
            Image("user_dark.png")
                .Aspect(Aspect.Center)
                .GridRowSpan(2)
                .HeightRequest(36)
                .WidthRequest(36)
                .VStart()
                .Clip(new EllipseGeometry().RadiusX(18).RadiusY(18).Center(18, 18)),

            Label("Ado")
                .FontSize(17)
                .TextColor(Colors.White)
                .VEnd()
                .GridColumn(1)
                .Margin(5, 0),

            Label("Software Engineer")
                .FontSize(13)
                .TextColor(Colors.White.WithAlpha(0.5f))
                .VStart()
                .GridColumn(1)
                .GridRow(2)
                .Margin(5, 0)
        )
        .Margin(18, 0);

    Grid RenderBrowse() 
        => Grid("16, *", "*",
            Label("BROWSE")
                .FontSize(12)
                .TextColor(Colors.White.WithAlpha(0.6f))
                .FontAttributes(MauiControls.FontAttributes.Bold)
                .VEnd(),

            VStack(spacing: 0,
                RenderMenuItem("Home", "home_img.png", CommandMenuItem.Home, true),
                RenderMenuItem("Search", "search_img.png", CommandMenuItem.Search),
                RenderMenuItem("Favorites", "favorites_img.png", CommandMenuItem.Favorites),
                RenderMenuItem("Help", "help_img.png", CommandMenuItem.Help)
            )
            .Margin(20, 8, 0, 0)
            .GridRow(1)
        )
        .Margin(30, 37)
        .GridRow(1);

    Grid RenderHistory() 
        => Grid("16, *", "*",
            Label("HISTORY")
                .FontSize(12)
                .TextColor(Colors.White.WithAlpha(0.6f))
                .FontAttributes(MauiControls.FontAttributes.Bold)
                .VEnd(),

            VStack(spacing: 0,
                RenderMenuItem("History", "billing_img.png", CommandMenuItem.History, true),
                RenderMenuItem("Notification", "videos_img.png", CommandMenuItem.Notification)
            )
            .Margin(20, 8, 0, 0)
            .GridRow(1)
        )
        .Margin(30, 50)
        .GridRow(2);

    Grid RenderBottom() 
        => Grid()
            .GridRow(3);

    VisualNode RenderMenuItem(string title, string icon, CommandMenuItem command, bool firstItem = false)
    {
        return new SideMenuItem()
            .Label(title)
            .Icon(icon)
            .FirstItem(firstItem)
            .IsSelected(State.SelectedMenuItem == command)
            .OnSelect(() => SetState(s => s.SelectedMenuItem = command));
    }
}

class SideMenuItemState
{
    public float ScaleX { get; set; } = 0.5f;
    public bool IsSelected { get; set; }
}

partial class SideMenuItem : Component<SideMenuItemState>
{
    [Prop]
    private string _label;
    [Prop]
    private string _icon;
    [Prop]
    private bool _isSelected;
    [Prop]
    private Action _onSelect;
    [Prop]
    private bool _firstItem;

    protected override void OnMountedOrPropsChanged()
    {
        State.ScaleX = _isSelected ? 1.0f : 0.0f;
        State.IsSelected = _isSelected;
        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return new CanvasView
        {
            new Group
            {
                !_firstItem ?
                new Align()
                {
                    new Box()
                        .BackgroundColor(Theme.Background.WithAlpha(0.1f))
                }
                .Margin(5,0)
                .Height(1)
                .VStart() : null,

                new Align
                {
                    new Align
                    {
                        new Box()
                            .CornerRadius(12)
                            .BackgroundColor(Color.FromUint(0xFF6792FF).WithLuminosity(0.6f))
                    }
                    .HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Start)
                    .Width(State.ScaleX * 225.0f)
                    .WithAnimation(duration: 200)
                }
                .HStart()
                
                .Width(225.0f),

                new Row("24,*")
                {
                    new AnimatedIcon()
                        .Icon(_icon)
                        .IsSelected(_isSelected)
                    ,

                    new Text(_label)
                        .FontSize(17)
                        .FontColor(Colors.LightGray)
                        .VerticalAlignment (VerticalAlignment.Center)
                        .Margin(8,0)

                }
                .Margin(8,0,8,0)
            }
        }
        .Margin(-8, -2)
        .BackgroundColor(Colors.Transparent)
        .OnTapped(_onSelect)
        .WidthRequest(225.0)
        .HeightRequest(52);
    }
}

class AnimatedIconState
{
    public PointF TranslatePoint { get; set; }
    public bool IsAnimating { get; set; }
    public bool IsSelected { get; set; }
}

partial class AnimatedIcon : Component<AnimatedIconState>
{
    [Prop]
    string _icon;

    [Prop]
    bool _isSelected;

    protected override void OnMounted()
    {
        State.IsSelected = _isSelected;
        State.IsAnimating = _isSelected;

        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        if (_isSelected && !State.IsSelected)
        {
            State.IsAnimating = true;
            State.IsSelected = true;
        }
        else if (!_isSelected && State.IsSelected)
        {
            State.IsAnimating = false;
            State.IsSelected = false;
        }

        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Align
        {
            new Picture($"RiveApp.Resources.Images.{_icon}")
                .Aspect(Aspect.Fill),

            new AnimationController
            {
                new SequenceAnimation
                {
                    new CubicBezierPathAnimation()
                        .StartPoint(0,0)
                        .EndPoint(0,5)
                        .ControlPoint1(5,0)
                        .ControlPoint2(5,5)
                        .OnTick(v => SetState(s => s.TranslatePoint = v))
                        .Duration(200),

                    new CubicBezierPathAnimation()
                        .StartPoint(0,5)
                        .EndPoint(0,0)
                        .ControlPoint1(-5,5)
                        .ControlPoint2(-5,0)
                        .OnTick(v => SetState(s => s.TranslatePoint = v))
                        .Duration(200),
                }
                .IterationCount(1)
            }
            .IsEnabled(State.IsAnimating)
            .OnIsEnabledChanged(animating => State.IsAnimating = animating)
        }
        .Height(24)
        .Width(24)
        .TranslationX(() => State.TranslatePoint.X)
        .TranslationY(() => State.TranslatePoint.Y)
        .HCenter()
        .VCenter();
    }
}