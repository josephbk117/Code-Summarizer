using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Threading;

namespace CodeSummarizerWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
   
    public partial class App : Application
    {
        private const int timeTillLoad = 4000;

        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen();
            splash.Show();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            base.OnStartup(e);
            MainWindow main = new MainWindow();
            watch.Stop();
            int remaingTime = timeTillLoad - (int)watch.ElapsedMilliseconds;
            if(remaingTime > 0)
            {
                Thread.Sleep(remaingTime);
            }
            splash.Close();
        }
    }
}
