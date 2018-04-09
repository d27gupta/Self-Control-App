﻿using Xamarin.Forms;

namespace SelfControl
{
    public class ReviewPage : TabbedPage
    {
        public ReviewPage()
        {
            var chartsPage = new NavigationPage(new Page1());
            chartsPage.Title = "Charts";

            var dailyPage = new NavigationPage(new Helpers.Pages.DailyReviewPage());
            dailyPage.Title = "Daily";

            var weeklyPage = new NavigationPage(new Page1());
            weeklyPage.Title = "Weekly";

            Children.Add(chartsPage);
            Children.Add(dailyPage);
            Children.Add(weeklyPage);
        }
    }
}