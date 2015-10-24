namespace Finsa.Caravan.Common.Models.Linking
{
    /// <summary>
    ///   Indicates that the link's context is a part of a series, and that the next in the series
    ///   is the link target.
    /// </summary>
    public class NextLink : Link
    {
        public const string Relation = "next";

        public NextLink(string href, string method, string title = null)
            : base(Relation, href, method, title)
        {
        }
    }
}