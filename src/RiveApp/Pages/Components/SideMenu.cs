using RiveApp.Models;
using RiveApp.Resources;
using System;
using MauiReactor;
using MauiReactor.Shapes;
using MauiReactor.Animations;

namespace RiveApp.Pages.Components;

class SideMenuState
{
    public double Opacity { get; set; } = 0.0;

    public double RotationY { get; set; } = 10;

    public double TranslationX { get; set; } = -250;

    public CommandMenuItem SelectedMenuItem { get; set; }
}

class SideMenu : Component<SideMenuState>
{
    private bool _isShown;

    public SideMenu IsShown(bool isShown)
    {
        _isShown = isShown;
        return this;
    }

    protected override void OnMounted()
    {
        State.TranslationX = _isShown ? 0 : -250;
        State.Opacity = _isShown ? 1.0 : 0.0;
        State.RotationY = _isShown ? 0.0 : 10;

        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.TranslationX = _isShown ? 0 : -250;
        State.Opacity = _isShown ? 1.0 : 0.0;
        State.RotationY = _isShown ? 0.0 : 10;

        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid("39, Auto, *, Auto", "250")
        {
            RenderHeader(),

            RenderBrowse(),

            RenderHistory(),

            RenderBottom()
        }
        .Padding(0, 60)
        .RotationY(State.RotationY)
        .TranslationX(State.TranslationX)
        .Opacity(State.Opacity)
        .WithAnimation(easing: Easing.CubicIn, duration: 300)
        .BackgroundColor(Theme.Background2)
        .HStart();
    }

    VisualNode RenderHeader()
    {
        return new Grid("20, 18", "36, *")
        {
            new Image("user_dark.png")
                .Aspect(Aspect.Center)
                .GridRowSpan(2)
                .HeightRequest(36)
                .WidthRequest(36)
                .VStart()
                .Clip(new EllipseGeometry().RadiusX(18).RadiusY(18).Center(18,18)),

            new Label("Ado")
                .FontSize(17)
                .TextColor(Colors.White)
                .VEnd()
                .GridColumn(1)
                .Margin(5,0),

            new Label("Software Engineer")
                .FontSize(13)
                .TextColor(Colors.White.WithAlpha(0.5f))
                .VStart()
                .GridColumn(1)
                .GridRow(2)
                .Margin(5,0)
        }
        .Margin(18, 0);
    }

    VisualNode RenderBrowse()
    {
        return new Grid("16, *", "*")
        {
            new Label("BROWSE")
                .FontSize(12)
                .TextColor(Colors.White.WithAlpha(0.6f))
                .FontAttributes(MauiControls.FontAttributes.Bold)
                .VEnd(),

            new VStack(spacing: 0)
            {
                RenderMenuItem("Home", "home.png", CommandMenuItem.Home),
                RenderMenuItem("Search", "search.png", CommandMenuItem.Search),
                RenderMenuItem("Favorites", "favorites.png", CommandMenuItem.Favorites),
                RenderMenuItem("Help", "help.png", CommandMenuItem.Help),
            }
            .Margin(0,8,0,0)
            .GridRow(1)
        }
        .Margin(30, 37)
        .GridRow(1);
    }

    VisualNode RenderHistory()
    {
        return new Grid("16, *", "*")
        {
            new Label("HISTORY")
                .FontSize(12)
                .TextColor(Colors.White.WithAlpha(0.6f))
                .FontAttributes(MauiControls.FontAttributes.Bold)
                .VEnd(),

            new VStack(spacing: 0)
            {
                RenderMenuItem("History", "billing.png", CommandMenuItem.History),
                RenderMenuItem("Notification", "video.png", CommandMenuItem.Notification),
            }
            .Margin(0,8,0,0)
            .GridRow(1)
        }
        .Margin(30, 50)
        .GridRow(2);
    }

    VisualNode RenderBottom()
    {
        return new Grid("*", "*")
        {

        }
        .GridRow(3);
    }

    VisualNode RenderMenuItem(string title, string icon, CommandMenuItem command)
    {
        return new SideMenuItem()
            .Label(title)
            .Icon(icon)
            .IsSelected(State.SelectedMenuItem == command)
            .OnSelect(() => SetState(s => s.SelectedMenuItem = command));
    }
}

class SideMenuItemState
{
    public double ScaleX { get; set; } = 0.5;
    public bool IsSelected { get; set; }
}

class SideMenuItem : Component<SideMenuItemState>
{
    private string _label;
    private string _icon;
    private bool _selected;
    private Action _selectAction;

    public SideMenuItem Label(string label)
    {
        _label = label;
        return this;
    }

    public SideMenuItem Icon(string icon)
    {
        _icon = icon;
        return this;
    }

    public SideMenuItem IsSelected(bool selected)
    {
        _selected = selected;
        return this;
    }

    public SideMenuItem OnSelect(Action selectAction)
    {
        _selectAction = selectAction;
        return this;
    }

    protected override void OnMounted()
    {
        State.ScaleX = _selected ? 1.0 : 0.0;
        State.IsSelected = _selected;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.ScaleX = _selected ? 1.0 : 0.0;
        State.IsSelected = _selected;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid("52", "24, *")
        {
            new Rectangle()
                .HeightRequest(1)
                .HFill()
                .VStart()
                .GridColumnSpan(2)
                .BackgroundColor(Theme.Background.WithAlpha(0.1f))
                ,

            new Border
            {
                new Border()
                    .StrokeShape(new RoundRectangle().CornerRadius(12))
                    .StrokeThickness(0)
                    .BackgroundColor(Color.FromUint(0xFF6792FF).WithLuminosity(0.6f))
                    ,
            }
            .BackgroundColor(Colors.Transparent)
            .GridColumnSpan(2)
            .HStart()
            .Margin(-8,-2)
            .WidthRequest(State.ScaleX * 225.0)
            .WithAnimation(duration: 200)
            ,

            new AnimatedIcon()
                .Icon(_icon)
                .IsSelected(State.IsSelected)
                ,

            new Label(_label)
                .FontSize(17)
                .TextColor(Colors.LightGray)
                .VCenter()
                .Margin(8,0)
                .GridColumn(1),
        }
        .OnTapped(_selectAction);
    }
}

class AnimatedIconState
{
    public Point TranslatePoint { get; set; }
    public bool IsAnimating { get; set; }
    public bool IsSelected { get; set; }
}

class AnimatedIcon : Component<AnimatedIconState>
{
    private string _icon;
    private bool _isSelected;

    public AnimatedIcon Icon(string icon)
    {
        _icon = icon;
        return this;
    }

    public AnimatedIcon IsSelected(bool selected)
    {
        _isSelected = selected;
        return this;
    }

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
        return new Grid("24", "24")
        {
            new Image(_icon),

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
        .TranslationX(() => State.TranslatePoint.X)
        .TranslationY(() => -State.TranslatePoint.Y)
        .HCenter()
        .VCenter()
        ;
    }
}