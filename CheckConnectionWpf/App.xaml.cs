using CheckConnectionWpf.Presenters;
using CheckConnectionWpf.Methods;
using CheckConnectionWpf.Models;
using Common;
using System;
using System.Windows;

namespace CheckConnectionWpf
{
    //public partial class App : Application
    //{
    //    protected override void OnStartup(StartupEventArgs e)
    //    {
    //        IocKernel.Initialize(new Bindings());

    //        base.OnStartup(e);
    //    }
    //}

    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
            IocKernel.Initialize(new Bindings());
        }

        [STAThread]
        static void Main()
        {
            App app = new App();

            var modeForm = new ModeForm();
            var modeModel = new ModeModel(Mode.Simple);
            var modePresenter = new ModePresenter(modeForm, modeModel);

            app.Run(modeForm);
        }
    }
}