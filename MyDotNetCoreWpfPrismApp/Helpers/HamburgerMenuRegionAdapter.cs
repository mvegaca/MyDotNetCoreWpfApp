using System.Linq;
using MahApps.Metro.Controls;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.Helpers
{
    public class HamburgerMenuRegionAdapter : RegionAdapterBase<HamburgerMenu>
    {
        public HamburgerMenuRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, HamburgerMenu regionTarget)
        {
            region.ActiveViews.CollectionChanged += (s, e) =>
            {
                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };
        }

        protected override IRegion CreateRegion()
            => new SingleActiveRegion();
    }
}
