﻿using ZKWeb.Plugins.Common.Admin.src.Domain.Entities;

namespace ZKWeb.Plugins.Common.Admin.src.Components.UserLoginHandlers.Interfaces {
	/// <summary>
	/// 用户登录处理器
	/// </summary>
	public interface IUserLoginHandler {
		/// <summary>
		/// 根据用户名查找用户
		/// 不支持此函数或没有查找到时返回null
		/// </summary>
		/// <param name="username">用户名</param>
		/// <returns></returns>
		User FindUser(string username);

		/// <summary>
		/// 登陆前的处理
		/// </summary>
		/// <param name="user">用户</param>
		void BeforeLogin(User user);

		/// <summary>
		/// 登陆后的处理
		/// </summary>
		/// <param name="user">用户</param>
		void AfterLogin(User user);
	}
}
