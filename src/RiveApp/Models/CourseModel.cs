using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiveApp.Models;

record CourseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; init; }
    public string SubTitle { get; init; }
    public string Caption { get; init; }
    public Color Color { get; init; }
    public string Image { get; init; }

    public static CourseModel[] Courses { get; } = new[]
    {
        new CourseModel
        {
            Title = "Animations in SwiftUI",
            SubTitle = "Build and animate an iOS app from scratch",
            Caption= "20 sections - 3 hours",
            Color = Color.FromUint(0xFF7850F0),
            Image ="topic_1.png"
        },
        new CourseModel
        {
            Title = "Build Quick Apps with SwiftUI",
            SubTitle = "Apply your Swift and SwiftUI knowledge by building real, quick and various applications from scratch",
            Caption= "47 sections - 11 hours",
            Color = Color.FromUint(0xFF6792FF),
            Image ="topic_2.png"
        },
        new CourseModel
        {
            Title = "Build a SwiftUI app for iOS 15",
            SubTitle = "Design and code a SwiftUI 3 app with custom layouts, animations and gestures using Xcode 13, SF Symbols 3, Canvas, Concurrency, Searchable and a whole lot more",
            Caption= "21 sections - 4 hours",
            Color = Color.FromUint(0xFF005FE7),
            Image ="topic_1.png"
        },
    };

    public static CourseModel[] CourseSections { get; } = new[]
    {
        new CourseModel
        {
            Title = "State Machine",
            SubTitle = "Watch video - 15 mins",
            Caption= "21 sections - 4 hours",
            Color = Color.FromUint(0xFF9CC5FF),
            Image ="topic_2.png"
        },
        new CourseModel
        {
            Title = "Animated Menu",
            SubTitle = "Watch video - 10 mins",
            Caption= "21 sections - 4 hours",
            Color = Color.FromUint(0xFF6E6AE8),
            Image ="topic_1.png"
        },
        new CourseModel
        {
            Title = "Tab Bar",
            SubTitle = "Watch video - 8 mins",
            Caption= "21 sections - 4 hours",
            Color = Color.FromUint(0xFF005FE7),
            Image ="topic_2.png"
        },
        new CourseModel
        {
            Title = "Button",
            SubTitle = "Watch video - 9 mins",
            Caption= "21 sections - 4 hours",
            Color = Color.FromUint(0xFFBBA6FF),
            Image ="topic_1.png"
        },
    };
}
/*
 class CourseModel {
  CourseModel(
      {this.id,
      this.title = "",
      this.subtitle = "",
      this.caption = "",
      this.color = Colors.white,
      this.image = ""});

  UniqueKey? id = UniqueKey();
  String title, caption, image;
  String? subtitle;
  Color color;

  static List<CourseModel> courses = [
    CourseModel(
        Title =  "Animations in SwiftUI",
        subTitle =  "Build and animate an iOS app from scratch",
        caption: "20 sections - 3 hours",
        color: const Color(0xFF7850F0),
        image: app_assets.topic_1),
    CourseModel(
        Title =  "Build Quick Apps with SwiftUI",
        subTitle = 
            "Apply your Swift and SwiftUI knowledge by building real, quick and various applications from scratch",
        caption: "47 sections - 11 hours",
        color: const Color(0xFF6792FF),
        image: app_assets.topic_2),
    CourseModel(
        Title =  "Build a SwiftUI app for iOS 15",
        subTitle = 
            "Design and code a SwiftUI 3 app with custom layouts, animations and gestures using Xcode 13, SF Symbols 3, Canvas, Concurrency, Searchable and a whole lot more",
        caption: "21 sections - 4 hours",
        color: const Color(0xFF005FE7),
        image: app_assets.topic_1),
  ];

  static List<CourseModel> courseSections = [
    CourseModel(
        Title =  "State Machine",
        caption: "Watch video - 15 mins",
        color: const Color(0xFF9CC5FF),
        image: app_assets.topic_2),
    CourseModel(
        Title =  "Animated Menu",
        caption: "Watch video - 10 mins",
        color: const Color(0xFF6E6AE8),
        image: app_assets.topic_1),
    CourseModel(
        Title =  "Tab Bar",
        caption: "Watch video - 8 mins",
        color: const Color(0xFF005FE7),
        image: app_assets.topic_2),
    CourseModel(
        Title =  "Button",
        caption: "Watch video - 9 mins",
        color: const Color(0xFFBBA6FF),
        image: app_assets.topic_1),
  ];
}*/