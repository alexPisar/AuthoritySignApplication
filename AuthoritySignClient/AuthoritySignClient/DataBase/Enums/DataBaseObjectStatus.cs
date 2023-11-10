using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.DataBase.Enums
{
    public enum DataBaseObjectStatus
    {
        Deleted = -1,
        New,
        Added,
        AddedAndUpdated,
        Updated,
        Saved
    }
}
