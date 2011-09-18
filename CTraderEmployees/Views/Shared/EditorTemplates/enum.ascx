<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CTraderEmployees.Models" %>
<%
    var gender = EmployeeGender.Unspecified;
    if (Model != null)
    {
        gender = (EmployeeGender)Model;
    }
    var values = Enum.GetValues(typeof(EmployeeGender));
    var items = (from object value in values select new SelectListItem() { Value = value.ToString(), Text = value.ToString(), Selected = (EmployeeGender)value == gender }).ToList();
%>
<%: Html.DropDownList("", items)%>
