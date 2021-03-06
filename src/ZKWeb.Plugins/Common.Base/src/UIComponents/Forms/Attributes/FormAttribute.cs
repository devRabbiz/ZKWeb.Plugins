﻿using System;
using ZKWeb.Web;

namespace ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes {
	/// <summary>
	/// 表单属性
	/// 用于指定表单名称，可省略
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class FormAttribute : Attribute {
		/// <summary>
		/// 表单名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 提交到的url
		/// </summary>
		public string Action { get; set; }
		/// <summary>
		/// 提交类型
		/// </summary>
		public string Method { get; set; }
		/// <summary>
		/// 提交按钮的文本，默认等于"Submit"
		/// </summary>
		public string SubmitButtonText { get; set; }
		/// <summary>
		/// 提交按钮的css类，默认是btn btn-submit
		/// </summary>
		public string SubmitButtonCssClass { get; set; }
		/// <summary>
		/// 启用Ajax提交，默认等于true
		/// </summary>
		public bool EnableAjaxSubmit { get; set; }
		/// <summary>
		/// 启用Csrf校验，默认等于true
		/// </summary>
		public bool EnableCsrfToken { get; set; }
		/// <summary>
		/// 表单的css类，默认是form-horizontal
		/// </summary>
		public string CssClass { get; set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="name">表单名称</param>
		/// <param name="action">提交到的url，默认和请求的url相同</param>
		/// <param name="method">提交类型，默认是POST</param>
		public FormAttribute(string name, string action = null, string method = HttpMethods.POST) {
			Name = name;
			Action = action;
			Method = method;
			SubmitButtonText = "Submit";
			SubmitButtonCssClass = "btn btn-submit";
			EnableAjaxSubmit = true;
			EnableCsrfToken = true;
			CssClass = "form-horizontal";
		}
	}
}
