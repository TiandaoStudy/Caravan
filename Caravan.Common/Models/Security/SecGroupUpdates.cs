using Finsa.CodeServices.Common;
using System;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable]
    public class SecGroupUpdates
    {
        public Option<string> Name { get; set; }

        public Option<string> Description { get; set; }

        public Option<string> Notes { get; set; }
    }
}