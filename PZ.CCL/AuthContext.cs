using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ.CCL.Actors;

namespace PZ.CCL
{
    public static class AuthContext
    {
        private static BaseUser? _user;

        public static void SetUser(BaseUser user)
        {
            _user = user;
        }
        public static BaseUser getUser()
        {
            if (_user == null)
            {
                throw new NullReferenceException("Не знайдено поточного користувача.");
            }
            return _user;
        }
    }
}
