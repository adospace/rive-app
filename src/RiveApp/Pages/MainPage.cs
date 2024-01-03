using MauiReactor;
using MauiReactor.Shapes;
using RiveApp.Models;
using RiveApp.Pages.Components;
using RiveApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiveApp.Pages;

class MainPageState
{
    public bool IsSideMenuShown { get; set; }

    public bool ShowOnboarding { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render() 
        => ContentPage(
            Grid("*", "*",
                new Home()
                    .IsShown(!State.IsSideMenuShown)
                    .IsMovedBack(State.ShowOnboarding)
                    .OnShowOnboarding(() => SetState(s => s.ShowOnboarding = true)),

                new SideMenu()
                    .IsShown(State.IsSideMenuShown),

                new MenuButton()
                    .IsShown(State.IsSideMenuShown)
                    .OnToggle(() => SetState(s => s.IsSideMenuShown = !s.IsSideMenuShown)),

                new Onboarding()
                    .Show(State.ShowOnboarding)
                    .OnClose(() => SetState(s => s.ShowOnboarding = false)),

                new NavBar()
                    .Show(!State.IsSideMenuShown && !State.ShowOnboarding)
            )
        )
        .Set(MauiControls.NavigationPage.HasNavigationBarProperty, false)
        .BackgroundColor(Theme.Background2)
        ;

}
