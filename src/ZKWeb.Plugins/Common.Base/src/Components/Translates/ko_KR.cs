﻿using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Plugins.Common.Base.src.Components.Translates {
	/// <summary>
	/// 韩语翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class ko_KR : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "ko-KR" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "ZKWeb Default Website", "ZKWeb 기본 사이트" },
			{ "Captcha", "코드" },
			{ "Click to change captcha image", "확인 코드 이미지를 대체 하려면 클릭" },
			{ "Please enter captcha", "제발 입력 검증 코드" },
			{ "Incorrect captcha", "코드 오류를 확인, 입력 해 주세요는" },
			{ "{0} is required", "제발 {0}" },
			{ "Length of {0} must be {1}", "{0}에서 있어야 합니다는 길이 {1}" },
			{ "Length of {0} must between {1} and {2}", "길이 {0}의 {1}와 {2} 사이 해야 합니다" },
			{ "HomePage", "홈페이지" },
			{ "Index", "홈페이지" },
			{ "Refresh", "새로 고침" },
			{ "Fullscreen", "전체 화면" },
			{ "Operations", "작업" },
			{ "Export to excel", "테이블 내보내기" },
			{ "Print", "인쇄" },
			{ "Pagination Settings", "페이지 설정" },
			{ "[0] Records per page", "페이지 [0]" },
			{ "Please enter keyword", "키워드를 입력 해 주십시오" },
			{ "Search", "수색" },
			{ "AdvanceSearch", "고급 검색" },
			{ "Data with id {0} cannot be found", "데이터 Id가 {0}를 찾을 수 없습니다" },
			{ "True", "예" },
			{ "False", "여부" },
			{ "Yes", "예" },
			{ "No", "여부" },
			{ "Ok", "확인" },
			{ "Cancel", "취소" },
			{ "Actions", "작업" },
			{ "Deleted", "삭제 된" },
			{ "Select All", "모두 선택" },
			{ "Select/Unselect All", "모두 선택/모두 선택 해제" },
			{ "Submit", "전송" },
			{ "Please Select", "선택 하십시오" },
			{ "Only {0} files are allowed", "{0} 파일을 업로드 허용" },
			{ "Please upload file size not greater than {0}", "업로드 파일 크기 {0}를 초과 하지 않도록 하시기 바랍니다" },
			{ "Basic Information", "기본 정보" },
			{ "Base Functions", "기본 기능" },
			{ "Base functions and template pages", "기능 및 서식 파일을 기반으로 페이지" },
			{ "{0} format is incorrect", "{0}가 올바른 형식이" },
			{ "Expand/Collapse All", "모두 확장/축소" },
			{ "Type", "유형" },
			{ "Menu", "메뉴" },
			{ "BatchActions", "일괄 작업" },
			{ "FirstPage", "첫" },
			{ "PrevPage", "이전" },
			{ "NextPage", "다음" },
			{ "LastPage", "마지막" },
			{ "Saved Successfully", "성공적으로 저장" },
			{ "WebsiteIndexPage", "웹 사이트 색인 페이지" },
			{ "Copyright", "저작권" },
			{ "IndexHelp", "색인 도움말" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}
