<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Employee.Master" Inherits="System.Web.Mvc.ViewPage<CTraderEmployees.Models.EmployeeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">First Name</div>
        <div class="display-field"><%: Model.FirstName %></div>
        
        <div class="display-label">Last Name</div>
        <div class="display-field"><%: Model.LastName %></div>
        
        <div class="display-label">Age</div>
        <div class="display-field"><%: Model.Age %></div>
        
        <div class="display-label">Is Current Employee</div>
        <div class="display-field"><%: Html.DisplayFor(model => Model.IsCurrentEmployee)%></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

