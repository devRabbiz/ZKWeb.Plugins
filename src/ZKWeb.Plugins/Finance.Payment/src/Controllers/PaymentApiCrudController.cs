﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKWeb.Database;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Admin.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Admin.src.UIComponents.AjaxTable.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Bases;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Interfaces;
using ZKWeb.Plugins.Common.Base.src.UIComponents.BaseTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Enums;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Interfaces;
using ZKWeb.Plugins.Common.Base.src.UIComponents.ScriptStrings;
using ZKWeb.Plugins.Finance.Payment.src.Components.ListItemProviders;
using ZKWeb.Plugins.Finance.Payment.src.Domain.Entities;
using ZKWeb.Plugins.Finance.Payment.src.UIComponents.Forms;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Plugins.Finance.Payment.src.Controllers {
	/// <summary>
	/// 支付接口管理
	/// </summary>
	[ExportMany]
	public class PaymentApiCrudController : CrudAdminAppControllerBase<PaymentApi, Guid> {
		public override string Group { get { return "Finance Manage"; } }
		public override string GroupIconClass { get { return "fa fa-money"; } }
		public override string Name { get { return "PaymentApiManage"; } }
		public override string Url { get { return "/admin/payment_apis"; } }
		public override string TileClass { get { return "tile bg-yellow"; } }
		public override string IconClass { get { return "fa fa-arrow-circle-o-down"; } }
		protected override bool UseTransaction { get { return true; } }
		protected override IAjaxTableHandler<PaymentApi, Guid> GetTableHandler() { return new TableHandler(); }
		protected override IModelFormBuilder GetAddForm() {
			var type = Request.Get<string>("type"); //支付接口类型，没有传入时需要先选择
			if (string.IsNullOrEmpty(type)) {
				return new SelectTypeForAddForm();
			}
			return new PaymentApiEditForm();
		}
		protected override IModelFormBuilder GetEditForm() { return new PaymentApiEditForm(); }

		/// <summary>
		/// 获取权限列表
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<string> GetPrivileges() {
			var extraPrivileges = new[] {
				Name + ":Test" // 测试支付接口的权限
			};
			return base.GetPrivileges().Concat(extraPrivileges);
		}

		/// <summary>
		/// 表格处理器
		/// </summary>
		public class TableHandler : AjaxTableHandlerBase<PaymentApi, Guid> {
			/// <summary>
			/// 构建表格
			/// </summary>
			public override void BuildTable(
				AjaxTableBuilder table, AjaxTableSearchBarBuilder searchBar) {
				table.StandardSetupFor<PaymentApiCrudController>();
				searchBar.StandardSetupFor<PaymentApiCrudController>("Name/Owner/Remark");
			}

			/// <summary>
			/// 查询数据
			/// </summary>
			public override void OnQuery(
				AjaxTableSearchRequest request, ref IQueryable<PaymentApi> query) {
				// 按关键字
				if (!string.IsNullOrEmpty(request.Keyword)) {
					query = query.Where(q =>
						q.Name.Contains(request.Keyword) ||
						q.Owner.Username.Contains(request.Keyword) ||
						q.Remark.Contains(request.Keyword));
				}
			}

			/// <summary>
			/// 排序数据
			/// </summary>
			public override void OnSort(
				AjaxTableSearchRequest request, ref IQueryable<PaymentApi> query) {
				query = query.OrderBy(q => q.DisplayOrder).ThenByDescending(q => q.Id);
			}

			/// <summary>
			/// 选择数据
			/// </summary>
			public override void OnSelect(
				AjaxTableSearchRequest request, IList<EntityToTableRow<PaymentApi>> pairs) {
				foreach (var pair in pairs) {
					var owner = pair.Entity.Owner;
					pair.Row["Id"] = pair.Entity.Id;
					pair.Row["Name"] = new T(pair.Entity.Name);
					pair.Row["Type"] = new T(pair.Entity.Type);
					pair.Row["Owner"] = owner?.Username;
					pair.Row["OwnerId"] = owner?.Id;
					pair.Row["CreateTime"] = pair.Entity.CreateTime.ToClientTimeString();
					pair.Row["UpdateTime"] = pair.Entity.UpdateTime.ToClientTimeString();
					pair.Row["DisplayOrder"] = pair.Entity.DisplayOrder;
					pair.Row["Deleted"] = pair.Entity.Deleted ? EnumDeleted.Deleted : EnumDeleted.None;
				}
			}

			/// <summary>
			/// 添加列和操作
			/// </summary>
			public override void OnResponse(
				AjaxTableSearchRequest request, AjaxTableSearchResponse response) {
				response.Columns.AddIdColumn("Id").StandardSetupFor<PaymentApiCrudController>(request);
				response.Columns.AddNoColumn();
				response.Columns.AddMemberColumn("Name", "35%");
				response.Columns.AddMemberColumn("Type");
				response.Columns.AddEditColumnFor<PaymentApiCrudController>("Owner", "OwnerId");
				response.Columns.AddMemberColumn("CreateTime");
				response.Columns.AddMemberColumn("UpdateTime");
				response.Columns.AddMemberColumn("DisplayOrder");
				response.Columns.AddEnumLabelColumn("Deleted", typeof(EnumDeleted));
				var actionColumn = response.Columns.AddActionColumn("155");
				var deleted = request.Conditions.GetOrDefault<bool>("Deleted");
				if (!deleted) {
					actionColumn.AddButtonForOpenLink(
						new T("TestPayment"), "btn btn-xs btn-warning", "fa fa-edit",
						"/admin/payment_apis/test_payment?id=<%-row.Id%>");
				}
				actionColumn.StandardSetupFor<PaymentApiCrudController>(request);
			}
		}

		/// <summary>
		/// 用于选择添加时使用的支付接口类型的表单
		/// </summary>
		[Form("SelectTypeForAddForm", SubmitButtonText = "NextStep")]
		public class SelectTypeForAddForm : ModelFormBuilder {
			/// <summary>
			/// 类型
			/// </summary>
			[Required]
			[RadioButtonsField("PaymentApiType", typeof(PaymentApiTypeListItemProvider))]
			public string Type { get; set; }

			/// <summary>
			/// 绑定表单
			/// </summary>
			protected override void OnBind() {
				var provider = new PaymentApiTypeListItemProvider();
				var firstItem = provider.GetItems().FirstOrDefault();
				Type = firstItem == null ? null : firstItem.Value;
			}

			/// <summary>
			/// 提交表单
			/// </summary>
			/// <returns></returns>
			protected override object OnSubmit() {
				var app = new PaymentApiCrudController();
				var url = string.Format("{0}?type={1}", app.AddUrl, Type);
				return new { script = BaseScriptStrings.RedirectModal(url) };
			}
		}
	}
}
