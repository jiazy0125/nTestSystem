using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nTestSystem.Framework.Commons
{

	[Flags]
	public enum ColumnFlags { }

    public enum Sequence
    {
        Desc,
        Asc
    }

    public enum Connection
    {
        Default,
        And,
        OR
    }

    public enum SqlLike
    {
        StartWith = 1,
        EndWith = 2,
        AnyWhere = 3
    }
}
