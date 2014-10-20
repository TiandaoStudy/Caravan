using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Finsa.Caravan.Extensions
{
   /// <summary>
   ///   TODO
   /// </summary>
   public static class ObjectExtensions
   {
      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static byte[] ToMd5Bytes<T>(this T obj)
      {
         using (var memoryStream = new MemoryStream())
         using (var bsonWriter = new BsonWriter(memoryStream))
         {
            var serializer = new JsonSerializer();
            serializer.Serialize(bsonWriter, obj);
            return MD5.Create().ComputeHash(memoryStream.GetBuffer());
         }
      }

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static string ToMd5String<T>(this T obj)
      {
         var hash = obj.ToMd5Bytes();
         var sb = new StringBuilder();
         foreach (var b in hash)
         {
            sb.Append(b.ToString("X2"));
         }
         return sb.ToString();
      }
   }
}
