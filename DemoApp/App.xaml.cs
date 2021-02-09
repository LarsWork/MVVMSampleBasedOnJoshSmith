using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using DemoApp.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Collections;

namespace DemoApp
{
    public partial class App : Application
    {
        static App()
        {
            // This code is used to test the app when using other cultures.
            //
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("it-IT");


            // Ensure the current culture passed into bindings is the OS culture.
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            //
            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();

            //load databindings and resources in local BC's
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream(asm.GetName().Name + ".g.resources");
            using (ResourceReader reader = new ResourceReader(stream))
            {
                foreach (DictionaryEntry resource in reader)
                {
                    var keyString = resource.Key.ToString();
                    if (keyString.Contains("databinding.baml"))
                    {
                        Uri uri = new Uri("/" + asm.GetName().Name + ";component/" + resource.Key.ToString().Replace(".baml", ".xaml"), UriKind.Relative);
                        ResourceDictionary skin = Application.LoadComponent(uri) as ResourceDictionary;
                        this.Resources.MergedDictionaries.Add(skin);

                        Console.WriteLine("Adding resourcedictionary: " + keyString);
                    }
                }
            }

            // Create the ViewModel to which 
            // the main window binds.
            string path = "Data/customers.xml";
            var viewModel = new MainWindowViewModel(path);

            // When the ViewModel asks to be closed, 
            // close the window.
            EventHandler handler = null;
            handler = delegate
            {
                viewModel.RequestClose -= handler;
                window.Close();
            };
            viewModel.RequestClose += handler;

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;

            window.Show();
        }
    }
}