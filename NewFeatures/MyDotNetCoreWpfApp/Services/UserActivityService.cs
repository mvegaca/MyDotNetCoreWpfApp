using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AdaptiveCards;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.UserActivities;
using Windows.UI;
using Windows.UI.Shell;

namespace MyDotNetCoreWpfApp.Services
{
    public class UserActivityService : IUserActivityService
    {
        private UserActivitySession _currentUserActivitySession;

        public async Task CreateUserActivityAsync(UserActivityData activityData)
        {
            var activity = await activityData.ToUserActivity();

            // Cleanup any content assigned earlier
            activity.VisualElements.Content = null;
            await SaveAsync(activity);
        }

        public async Task CreateUserActivityAsync(UserActivityData activityData, IAdaptiveCard adaptiveCard)
        {
            var activity = await activityData.ToUserActivity();

            activity.VisualElements.Content = adaptiveCard;
            await SaveAsync(activity);
        }

        public async Task AddSampleUserActivityAsync()
        {
            var activityId = nameof(SchemeActivationSamplePage);
            var displayText = "Sample Activity";
            var description = $"Sample UserActivity added from Application '{Package.Current.DisplayName}' at {DateTime.Now.ToShortTimeString()}";
            var imageUrl = "http://adaptivecards.io/content/cats/2.png";

            var activityData = new UserActivityData(activityId, CreateActivationDataSample(), displayText, Colors.DarkRed);
            var adaptiveCard = CreateAdaptiveCardSample(displayText, description, imageUrl);

            await CreateUserActivityAsync(activityData, adaptiveCard);
        }

        private async Task SaveAsync(UserActivity activity)
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                await activity.SaveAsync();

                // Dispose of any current UserActivitySession, and create a new one.
                _currentUserActivitySession?.Dispose();
                _currentUserActivitySession = activity.CreateSession();
            });
        }

        private SchemeActivationData CreateActivationDataSample()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "paramName1", "paramValue1" },
                { "ticks", DateTime.Now.Ticks.ToString() }
            };
            return new SchemeActivationData(typeof(ViewModels.SchemeActivationSampleViewModel).FullName, parameters);
        }

        // TODO WTS: Change this to configure your own adaptive card
        // For more info about adaptive cards see http://adaptivecards.io/
        private IAdaptiveCard CreateAdaptiveCardSample(string displayText, string description, string imageUrl)
        {
            var adaptiveCard = new AdaptiveCard("1.0");
            var columns = new AdaptiveColumnSet();
            var firstColumn = new AdaptiveColumn() { Width = "auto" };
            var secondColumn = new AdaptiveColumn() { Width = "*" };

            firstColumn.Items.Add(new AdaptiveImage()
            {
                Url = new Uri(imageUrl),
                Size = AdaptiveImageSize.Medium
            });

            secondColumn.Items.Add(new AdaptiveTextBlock()
            {
                Text = displayText,
                Weight = AdaptiveTextWeight.Bolder,
                Size = AdaptiveTextSize.Large
            });

            secondColumn.Items.Add(new AdaptiveTextBlock()
            {
                Text = description,
                Size = AdaptiveTextSize.Medium,
                Weight = AdaptiveTextWeight.Lighter,
                Wrap = true
            });

            columns.Columns.Add(firstColumn);
            columns.Columns.Add(secondColumn);
            adaptiveCard.Body.Add(columns);

            return AdaptiveCardBuilder.CreateAdaptiveCardFromJson(adaptiveCard.ToJson());
        }
    }
}
