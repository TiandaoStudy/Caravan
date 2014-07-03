namespace FLEX.Common
{
   public static class Constants
   {
      public const string XmlExtension = ".xml";
   }

   public sealed class Pair<T1, T2>
   {
      internal Pair(T1 first, T2 second)
      {
         First = first;
         Second = second;
      }

      public T1 First { get; set; }

      public T2 Second { get; set; }
   }

   public static class Pair
   {
      public static Pair<T1, T2> Create<T1, T2>(T1 first, T2 second)
      {
         return new Pair<T1, T2>(first, second);
      }
   }
}