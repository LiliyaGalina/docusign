using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docusign.API.Services
{
    public class UserInfo
    {
        public string Email { get; set; }

        public string FullName { get; set; }

    }

    public class AuthorizedUserMock
    {

        private IEnumerable<UserInfo> _usersBank = new List<UserInfo>()
        {
            new UserInfo{
                Email = "lhalina@vector-software.com",
                FullName = "L Halina work email"
            },
            new UserInfo{
                Email = "lilianna.galina@ukr.net",
                FullName = "Lilya Galina main private email"
            },
            new UserInfo{
                Email = "lilianna.galina2@ukr.net",
                FullName = "Lilya Galina side private email"
            }
        };

        private UserInfo _currentUser = null;

        public AuthorizedUserMock()
        {
            _currentUser = _usersBank.ElementAt(1);
        }

        public string Email => _currentUser.Email;

        public string FullName => _currentUser.FullName;


    }
}
