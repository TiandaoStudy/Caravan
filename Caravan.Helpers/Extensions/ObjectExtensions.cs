using System.Diagnostics.Contracts;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Finsa.Caravan.Extensions
{
    /// <summary>
    ///   TODO
    /// </summary>
    public static class ObjectExtensions
    {
        #region Hashing

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToMD5Bytes<T>(this T obj)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 16);

            byte[] bytes;

            var maybeBytes = obj as byte[];
            if (maybeBytes != null) {
                bytes = maybeBytes;
                goto computeHash;
            }

            var maybeString = obj as string;
            if (maybeString != null) {
                bytes = Encoding.Default.GetBytes(maybeString);
                goto computeHash;
            }

            using (var memoryStream = new MemoryStream()) {
                using (var streamWriter = new StreamWriter(memoryStream)) {
                    using (var jsonWriter = new JsonTextWriter(streamWriter)) {
                        var serializer = new JsonSerializer {
                            Formatting = Formatting.None,
                            NullValueHandling = NullValueHandling.Ignore,
                        };
                        serializer.Serialize(jsonWriter, obj);
                    }
                }
                bytes = memoryStream.GetBuffer();
            }

computeHash:
            return MD5.Create().ComputeHash(bytes);
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToMD5String<T>(this T obj)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length == 32);
            
            var hash = obj.ToMD5Bytes();
            var sb = new StringBuilder();
            foreach (var b in hash) {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion

      public static string ToJavaScriptString<T>(this T obj)
      {
         return ReferenceEquals(obj, null) ? null : obj.ToString().ToJavaScriptString();
      }
    }
}