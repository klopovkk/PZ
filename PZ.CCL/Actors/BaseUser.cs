using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ.CCL.Actors
{
    public abstract class BaseUser
    {
        public BaseUser(Guid id, string fullName)
        {
            Id = id;
            fullName = FullName;
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
