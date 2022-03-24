using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Planner.Views
{
    public partial class PlannerListView : UserControl
    {
        public PlannerListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
