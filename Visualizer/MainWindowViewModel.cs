using System;
using System.Linq;
using System.Windows.Media;
using Domain.Core;
using Brush = System.Drawing.Brush;

namespace Visualizer
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public string Name { get; }

        public string Icon { get; }

        public string FavoriteColor { get; }

        public SolidColorBrush Color { get; }

        public MainWindowViewModel(Person person)
        {
            Name = $"{person.FirstName} {person.LastName}";
            
            Icon = (DateTime.UtcNow - person.BirthDate).TotalDays / 365 < 70 
                ? "/Visualizer;component/Images/middle.png" 
                : "/Visualizer;component/Images/old.png";

            FavoriteColor = person.FavoriteColor;

            if (FavoriteColor != "Unassigned")
            {
                var allColors = typeof (Colors).GetProperties().Where(x => x.PropertyType == typeof (Color));

                var closestMatchingColor = allColors.Where(x => x.Name.IndexOf(FavoriteColor, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    .OrderBy(x => Math.Abs(FavoriteColor.Length - x.Name.Length))
                    .FirstOrDefault();


                Color = closestMatchingColor != null 
                    ? new SolidColorBrush((Color)closestMatchingColor.GetValue(null))
                    : new SolidColorBrush(Colors.LightGray);
            }
        }
    }
}
