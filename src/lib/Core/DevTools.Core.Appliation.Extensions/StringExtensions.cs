namespace DevTools.Core.Application.Extensions
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        public static string ToBase64(this string str, Encoding encoding = default)
            => Convert.ToBase64String(str.GetBytes(encoding));

        public static string FromBase64(this string str, Encoding encoding = default)
            => encoding.GetString(Convert.FromBase64String(str));

        public static byte[] GetBytes(this string str, Encoding encoding = default)
            => encoding.GetBytes(str);
    }
}
