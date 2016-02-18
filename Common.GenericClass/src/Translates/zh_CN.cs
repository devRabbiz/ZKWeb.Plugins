﻿using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Localize.Interfaces;
using ZKWeb.Utils.Extensions;

namespace ZKWeb.Plugins.Common.GenericClass.src.Translates {
	/// <summary>
	/// 中文翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class zh_CN : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "zh-CN" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "Generic Class", "通用分类" },
			{ "Generic class/catalog management", "通用分类的管理" },
			{ "ClassManage", "分类管理" },
			{ "DefaultClass", "默认分类" },
			{ "Try to access class that type not matched", "尝试操作类型不匹配的分类" },
			{ "DisplayOrder", "显示顺序" },
			{ "Order from small to large", "从小到大排列" },
			{ "ParentClass", "上级分类" },
			{ "Add Top Level Class", "添加顶级分类" },
			{ "Add Same Level Class", "添加同级分类" },
			{ "Add Child Class", "添加子分类" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}
