using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ.CCL.Actors
{
    public class EmployeeActor : BaseUser
    {
        public EmployeeActor(Guid id, string fullName) : base(id, fullName)
        {
        }
    }
}
