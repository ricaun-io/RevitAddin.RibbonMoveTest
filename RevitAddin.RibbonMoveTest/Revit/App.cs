using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.RibbonMoveTest.Revit
{
    [ApplicationLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanelA;
        private static RibbonPanel ribbonPanelB;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanelA = application.CreatePanel("Panel A");

            for (int i = 0; i < 3; i++)
            {
                ribbonPanelA.CreatePushButton<Commands.CommandA>()
                    .SetText($"A{i}")
                    .SetLargeImage(Properties.Resources.Revit.GetBitmapSource());
            }

            ribbonPanelB = application.CreatePanel("Panel B");
            for (int i = 0; i < 3; i++)
            {
                ribbonPanelB.CreatePushButton<Commands.CommandB>()
                    .SetText($"B{i}")
                    .SetLargeImage(Properties.Resources.Revit.GetBitmapSource());
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanelA?.Remove();
            ribbonPanelB?.Remove();
            return Result.Succeeded;
        }

        public static void MoveFirstRibbonItemAtoB() => MoveFirstRibbonItem(ribbonPanelA, ribbonPanelB);
        public static void MoveFirstRibbonItemBtoA() => MoveFirstRibbonItem(ribbonPanelB, ribbonPanelA);
        private static void MoveFirstRibbonItem(RibbonPanel ribbonPanelSource, RibbonPanel ribbonPanelMoveTo)
        {
            var ribbonItemsSource = ribbonPanelSource.GetRibbonPanel().Source.Items;
            var ribbonItemsMoveTo = ribbonPanelMoveTo.GetRibbonPanel().Source.Items;

            if (ribbonItemsSource.Count == 0) return;
            var first = ribbonItemsSource[0];
            ribbonItemsSource.Remove(first);
            ribbonItemsMoveTo.Add(first);
        }
    }

}