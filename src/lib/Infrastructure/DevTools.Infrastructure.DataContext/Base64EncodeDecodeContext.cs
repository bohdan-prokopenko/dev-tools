namespace DevTools.Infrastructure.DataContext
{
    using System.IO;
    using System.Text;

    using DevTools.Core.Application.Extensions;

    public class Base64EncodeDecodeContext : DataContextBase
    {
        public string Result { get; private set; }

        public string Base64 { get; set; }
        public string PlainText { get; set; }

        public void ConvertToBase64()
        {
            if (string.IsNullOrWhiteSpace(this.PlainText)) return;
            this.Result = this.Base64 = this.PlainText.ToBase64(encoding: Encoding.Default);
            this.Notify(nameof(this.Base64));
        }

        public void ConvertFromBase64()
        {
            if (string.IsNullOrWhiteSpace(this.Base64)) return;
            this.Result = this.PlainText = this.Base64.FromBase64(encoding: Encoding.Default);
            this.Notify(nameof(this.PlainText));
        }

        public void SaveAsFile(string path)
        {
            File.WriteAllText(path, this.Result);
        }

        public void Clear()
        {
            this.Base64 = string.Empty;
            this.PlainText = string.Empty;
            this.Notify(this.Base64);
            this.Notify(this.PlainText);
        }
    }
}
