using System;

namespace Nice_Admin_Panal.BAL
{
	public class CV
	{
		private static IHttpContextAccessor _httpContextAccessor;

		static CV()
		{
			_httpContextAccessor = new HttpContextAccessor();
		}

		public static String? UserName()
		{
			string? UserName = null;

			if (_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
			{
				UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
			}
			return UserName;
		}
		public static int? UserId()
		{
			int? UserId = null;

			if (_httpContextAccessor.HttpContext.Session.GetString("UserId") != null)
			{
				UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserId").ToString());
			}
			return UserId;
		}


	}
}
