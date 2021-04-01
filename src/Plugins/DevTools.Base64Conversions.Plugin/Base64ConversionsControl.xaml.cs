namespace DevTools.Base64Conversions.Plugin
{
    using System.Windows.Controls;

    public partial class Base64ConversionsControl : UserControl
    {
        private readonly Base64ConversionsViewModel viewModel;
        public Base64ConversionsControl(Base64ConversionsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = this.viewModel = viewModel;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            switch (textbox.Tag)
            {
                case "decoded":
                    this.viewModel.EncodeCommand.Execute(textbox.Text);
                    break;
                case "encoded":
                    this.viewModel.DecodeCommand.Execute(textbox.Text);
                    break;
            }
        }
    }
}
