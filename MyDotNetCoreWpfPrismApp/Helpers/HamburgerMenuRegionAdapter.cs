using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.Helpers
{
    public class HamburgerMenuRegionAdapter : RegionAdapterBase<HamburgerMenu>
    {
        public HamburgerMenuRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, HamburgerMenu regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement element in e.NewItems)
                    {
                        regionTarget.Content = element;
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
            => new SingleActiveRegion();
    }
}
