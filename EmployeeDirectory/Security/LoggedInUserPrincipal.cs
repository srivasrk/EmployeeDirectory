using System.Security.Principal;
using System.Web.Security;

namespace EmployeeDirectory.Security
{
	public class LoggedInUserPrincipal : IPrincipal
	{
		public IIdentity Identity { get; private set; }
		public bool IsInRole(string role)
		{
			return false;
		}

		public LoggedInUserPrincipal(FormsAuthenticationTicket ticket)
		{
			Identity = new FormsIdentity(ticket);
		}
	}
}