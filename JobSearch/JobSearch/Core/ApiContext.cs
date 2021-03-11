using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace JobSearch.WEB.Core
{
    public class ApiContext
    {
        public class UserContext
        {
            private readonly Func<Controller> controller;

            internal UserContext(Func<Controller> controller)
            {
                this.controller = controller;
            }
            private ClaimsPrincipal Principal { get { return controller().User; } }
            public int? UserId
            {
                get
                {
                    var claim = Principal.FindFirstValue(ClaimTypes.Sid);
                    return string.IsNullOrEmpty(claim) ? null : (int?)Int32.Parse(claim);
                }
            }
            public string Email { get { return Principal.FindFirstValue(ClaimTypes.Email); } }
            public bool IsAuthenticated { get { return Principal.Identity.IsAuthenticated; } }
        }

        public UserContext CurrentUser { get; private set; }

        internal ApiContext(Func<Controller> controller)
        {
            this.CurrentUser = new UserContext(controller);
        }
    }
}
