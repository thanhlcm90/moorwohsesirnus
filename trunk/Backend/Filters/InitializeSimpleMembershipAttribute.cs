using System;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using PharmacyStore.Models;
using System.Web.Security;
using System.Linq;

namespace PharmacyStore.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {

                    WebSecurity.InitializeDatabaseConnection("PharmacyStoreConnection", "SY_USER", "Id", "UserName", autoCreateTables: true);

                    if (!Roles.RoleExists("Administrator"))
                    {
                        Roles.CreateRole("Administrator");
                    }
                    if (!WebSecurity.UserExists("thanhlcm"))
                        WebSecurity.CreateUserAndAccount(
                            "thanhlcm",
                            "P@$$w0rd!", new { Fullname = "Lê Cao Minh Thành", Birthdate = new DateTime(1990, 03, 23), Gender = 1, Email = "thanhlcm90@gmail.com", Identification = "225414257" });
                    if (!Roles.GetRolesForUser("thanhlcm").Contains("Administrator"))
                        Roles.AddUsersToRoles(new[] { "thanhlcm" }, new[] { "Administrator" });
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
