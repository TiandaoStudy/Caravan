namespace Finsa.Caravan.Common.Models.Linking
{
    /// <summary>
    ///   Conveys an identifier for the link's context.
    /// </summary>
    public class SelfLink : Link
    {
        public const string Relation = "self";

        public SelfLink(string href, string method, string title = null)
            : base(Relation, href, method, title)
        {
        }
    }
}