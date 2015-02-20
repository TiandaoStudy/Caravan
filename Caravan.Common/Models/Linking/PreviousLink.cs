namespace Finsa.Caravan.Common.DataModel.Links
{
    /// <summary>
    ///   Indicates that the link's context is a part of a series, and that the previous in the
    ///   series is the link target.
    /// </summary>
    public class PreviousLink : Link
    {
        public const string Relation = "prev";

        public PreviousLink(string href, string method, string title = null)
            : base(Relation, href, method, title)
        {
        }
    }
}