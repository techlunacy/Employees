<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Employee.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CTraderEmployees.Models.EmployeeModel>>" %>

<%@ Import Namespace="CTraderEmployees.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <% using (Html.BeginForm())
       {
           var filter = ListSearchFilters.All;
           var values = Enum.GetValues(typeof(ListSearchFilters));
           var items = (from object value in values select new SelectListItem() { Value = value.ToString(), Text = value.ToString(), Selected = (ListSearchFilters)value == filter }).ToList();
    %>
    Filter By Employment Status:<%: Html.DropDownList("filterList", items)%>
    <input type="submit" value="search" />
    <%
        }%>
    <table>
        <tr>
            <th>
            </th>
            <th>
                First Name
            </th>
            <th>
                Last Name
            </th>
            <th>
                Age
            </th>
            <th>
                Is Current Employee?
            </th>
            <th>
                Gender
            </th>
        </tr>
        <%
            foreach (var item in Model)
            {%>
        <tr>
            <td>
                <%:Html.ActionLink("Edit", "Edit", new {id = item.Id})%>
                |
                <%:Html.ActionLink("Details", "Details", new {id = item.Id})%>
                |
                <%:Html.ActionLink("Delete", "Delete", new {id = item.Id})%>
            </td>
            <td>
                <%:item.FirstName%>
            </td>
            <td>
                <%:item.LastName%>
            </td>
            <td>
                <%:item.Age%>
            </td>
            <td>
                <%:Html.DisplayFor(model => item.IsCurrentEmployee)%>
            </td>
            <td>
                <%:item.Gender%>
            </td>
        </tr>
        <%
            }%>
    </table>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>
