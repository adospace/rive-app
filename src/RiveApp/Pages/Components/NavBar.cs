using MauiReactor;
using MauiReactor.Animations;
using MauiReactor.Canvas;
using MauiReactor.Shapes;
using RiveApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiveApp.Pages.Components;

class NavBarState
{
    public NavItem SelectedItem { get; set; }

    public double TranslationY { get; set; }
}

class NavBar : Component<NavBarState>
{
    private bool _show;

    public NavBar Shown(bool show)
    {
        _show = show;
        return this;
    }

    protected override void OnMounted()
    {
        State.TranslationY = _show ? 0 : 150;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.TranslationY = _show ? 0 : 150;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new CanvasView()//("59", "*")
        {
            new Box()
            {
                new Group()
                {
                    new Box()
                        .Background(new MauiControls.LinearGradientBrush(
                            new MauiControls.GradientStopCollection
                            {
                                new MauiControls.GradientStop(Colors.Transparent, 0.0f),
                                new MauiControls.GradientStop(Colors.White.WithAlpha(0.3f), 1.0f),
                            }, new Point(0.5,0.0), new Point(0.5, 1.0)))
                        .BorderColor(Colors.White.WithAlpha(0.5f))
                        .CornerRadius(22),

                    new Box
                    {
                        new Row()
                        {
                            new NavBarButtonIcon()
                                .Icon("home_img.png")
                                .IsSelected(State.SelectedItem == NavItem.Home)
                                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Home))
                                ,
                            new NavBarButtonIcon()
                                .Icon("search_img.png")
                                .IsSelected(State.SelectedItem == NavItem.Search)
                                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Search)),
                            new NavBarButtonIcon()
                                .Icon("favorites_img.png")
                                .IsSelected(State.SelectedItem == NavItem.Favorites)
                                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Favorites)),
                            new NavBarButtonIcon()
                                .Icon("billing_img.png")
                                .IsSelected(State.SelectedItem == NavItem.Billing)
                                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Billing)),
                            new NavBarButtonIcon()
                                .Icon("help_img.png")
                                .IsSelected(State.SelectedItem == NavItem.Help)
                                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Help)),
                        }
                    }
                    .Padding(22, 7)
                }
            }
            .BackgroundColor(Color.FromRgba(24, 34, 60, 256))            
            .CornerRadius(22)

            //new Border
            //{
            //    new FlexLayout
            //    {
            //        new NavBarButtonIcon()
            //            .Icon("home.png")
            //            .IsSelected(State.SelectedItem == NavItem.Home)
            //            .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Home))
            //            ,
            //        new NavBarButtonIcon()
            //            .Icon("search.png")
            //            .IsSelected(State.SelectedItem == NavItem.Search)
            //            .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Search)),
            //        new NavBarButtonIcon()
            //            .Icon("favorites.png")
            //            .IsSelected(State.SelectedItem == NavItem.Favorites)
            //            .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Favorites)),
            //        new NavBarButtonIcon()
            //            .Icon("billing.png")
            //            .IsSelected(State.SelectedItem == NavItem.Billing)
            //            .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Billing)),
            //        new NavBarButtonIcon()
            //            .Icon("help.png")
            //            .IsSelected(State.SelectedItem == NavItem.Help)
            //            .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Help)),
            //    }
            //    .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceAround)
            //}
            //.Padding(22, 7)
            //.StrokeShape(new RoundRectangle().CornerRadius(22))
            //.BackgroundColor(Color.FromRgba(24, 34, 60, 255)),

            //new Border()
            //    .StrokeShape(new RoundRectangle().CornerRadius(22))
            //    .Background(new MauiControls.LinearGradientBrush(
            //        new MauiControls.GradientStopCollection
            //        {
            //            new MauiControls.GradientStop(Colors.Transparent, 0.0f),
            //            new MauiControls.GradientStop(Colors.White.WithAlpha(0.3f), 1.0f),
            //        }, new Point(0.5,0.0), new Point(0.5, 1.0)))
            //    .Stroke(new MauiControls.LinearGradientBrush(
            //        new MauiControls.GradientStopCollection
            //        {
            //            new MauiControls.GradientStop(Colors.White.WithAlpha(0.5f), 0.0f),
            //            new MauiControls.GradientStop(Colors.Transparent, 1.0f),
            //        }, new Point(0.0,0.5), new Point(1.0, 0.5))),
        }
        .Margin(35, 0, 35, 30)
        .HeightRequest(59)
        .VEnd()
        .Shadow(new Shadow().Brush(Colors.White).Opacity(0.2f).Radius(200).Offset(0.0, 10.0))
        .TranslationY(State.TranslationY)
        .WithAnimation(ExtendedEasing.InOutBack, duration: 400)
        .BackgroundColor(Colors.Transparent)
        ;

        //return new Grid("59", "*")
        //{
        //    new Border
        //    {
        //        new FlexLayout
        //        {
        //            new NavBarButtonIcon()
        //                .Icon("home.png")
        //                .IsSelected(State.SelectedItem == NavItem.Home)
        //                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Home))
        //                ,
        //            new NavBarButtonIcon()
        //                .Icon("search.png")
        //                .IsSelected(State.SelectedItem == NavItem.Search)
        //                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Search)),
        //            new NavBarButtonIcon()
        //                .Icon("favorites.png")
        //                .IsSelected(State.SelectedItem == NavItem.Favorites)
        //                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Favorites)),
        //            new NavBarButtonIcon()
        //                .Icon("billing.png")
        //                .IsSelected(State.SelectedItem == NavItem.Billing)
        //                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Billing)),
        //            new NavBarButtonIcon()
        //                .Icon("help.png")
        //                .IsSelected(State.SelectedItem == NavItem.Help)
        //                .OnSelected(()=>SetState(s => s.SelectedItem = NavItem.Help)),
        //        }
        //        .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceAround)
        //    }
        //    .Padding(22, 7)
        //    .StrokeShape(new RoundRectangle().CornerRadius(22))
        //    .BackgroundColor(Color.FromRgba(24, 34, 60, 255)),

        //    new Border()
        //        .StrokeShape(new RoundRectangle().CornerRadius(22))
        //        .Background(new MauiControls.LinearGradientBrush(
        //            new MauiControls.GradientStopCollection
        //            {
        //                new MauiControls.GradientStop(Colors.Transparent, 0.0f),
        //                new MauiControls.GradientStop(Colors.White.WithAlpha(0.3f), 1.0f),
        //            }, new Point(0.5,0.0), new Point(0.5, 1.0)))
        //        .Stroke(new MauiControls.LinearGradientBrush(
        //            new MauiControls.GradientStopCollection
        //            {
        //                new MauiControls.GradientStop(Colors.White.WithAlpha(0.5f), 0.0f),
        //                new MauiControls.GradientStop(Colors.Transparent, 1.0f),
        //            }, new Point(0.0,0.5), new Point(1.0, 0.5))),
        //}
        //.Margin(35,0,35,30)
        //.HeightRequest(59)
        //.VEnd()
        //.Shadow(new Shadow().Brush(Colors.White).Opacity(0.2f).Radius(200).Offset(0.0, 10.0))
        //.TranslationY(State.TranslationY)
        //.WithAnimation(ExtendedEasing.InOutBack, duration: 400)
        //;
    }
}

class NavBarButtonIconState
{
    public float SelectionScaleX { get; set; }
}

class NavBarButtonIcon : Component<NavBarButtonIconState>
{
    private string _icon;
    private bool _isSelected;
    private Action _onSelected;

    public NavBarButtonIcon Icon(string icon)
    {
        _icon = icon;
        return this;
    }

    public NavBarButtonIcon IsSelected(bool isSelected)
    {
        _isSelected = isSelected;
        return this;
    }

    public NavBarButtonIcon OnSelected(Action action)
    {
        _onSelected = action;
        return this;
    }

    protected override void OnMounted()
    {
        State.SelectionScaleX = _isSelected ? 1.0f : 0.0f;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.SelectionScaleX = _isSelected ? 1.0f : 0.0f;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new PointInterationHandler
        {
            new Align
            {
                new Column("11, 24") //new Grid("11, 24", "46")
                {
                    new Align()
                    {
                        new Box()
                            .BackgroundColor(Color.FromArgb("#81B4FF"))
                            //.BackgroundColor(Colors.Red)
                            .CornerRadius(3)
                    }
                    .VCenter()
                    .HCenter()
                    .Height(5)
                    .Width(20)
                    .AnchorX(0.5f)
                    .ScaleX(State.SelectionScaleX) 
                    .WithAnimation(duration: 100),

                    //new Border()
                    //    .HeightRequest(5)
                    //    .WidthRequest(20)
                    //    .HCenter()
                    //    .VCenter()
                    //    .BackgroundColor(Color.FromArgb("#81B4FF"))
                    //    .StrokeShape(new RoundRectangle().CornerRadius(3))
                    //    .AnchorX(0.5)
                    //    .ScaleX(State.SelectionScaleX)
                    //    .WithAnimation(duration: 200)
                    //    ,

                    new AnimatedIcon()
                        .Icon(_icon)
                        .IsSelected(_isSelected)
                        ,
                }
            }
            .Width(46)
        }
        .OnTap(_onSelected)
        ;
    }
}
