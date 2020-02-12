using Domain;
using System.Windows;

namespace BestAcademy
{
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            DIMock.AcademyContext.Dispose();
            base.OnExit(e);
        }
    }
}