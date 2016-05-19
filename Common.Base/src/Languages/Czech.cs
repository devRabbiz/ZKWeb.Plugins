﻿using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Localize.Interfaces;

namespace ZKWeb.Plugins.Common.Base.src.Languages {
	/// <summary>
	/// 捷克语
	/// </summary>
	[ExportMany]
	public class Czech : ILanguage {
		public string Name { get { return "cs-CZ"; } }
	}
}