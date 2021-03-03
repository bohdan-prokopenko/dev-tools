namespace DevTools
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using DevTools.Pages;
    using DevTools.Properties;

    public partial class MainWindow : Window
    {
        private readonly IEnumerable<Page> pages;
        private readonly RuntimeSettings runtimeSettings;

        public MainWindow()
        {
            this.InitializeComponent();
            this.pages = new List<Page>
            {
                new Base64EncodeDecodePage(),
                new XPathEvaluationPage(),
            };
            this.runtimeSettings = new RuntimeSettings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var navBtn = (Button)sender;
            this.NavigateTo(this.GetPageByTag(navBtn.Tag.ToString()));
            this.runtimeSettings.SelectedPage = navBtn.Tag.ToString();
            this.runtimeSettings.Save();
        }

        private void NavigateTo(object content)
            => _ = this.pageView.Navigate(content);

        private Page GetPageByTag(string tag)
            => this.pages.FirstOrDefault(page => page.Tag.Equals(tag));

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.runtimeSettings.SelectedPage))
                this.NavigateTo(this.GetPageByTag(this.pages.FirstOrDefault().Tag.ToString()));
            else 
                this.NavigateTo(this.GetPageByTag(this.runtimeSettings.SelectedPage));
        }
    }
}
