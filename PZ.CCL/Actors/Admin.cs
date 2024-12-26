using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ.CCL.Actors
{
    public class Admin : BaseUser
    {
        public Admin(Guid id, string fullName) : base(id, fullName)
        {
        }
    }
}
