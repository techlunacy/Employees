<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Employee.Master" Inherits="System.Web.Mvc.ViewPage<CTraderEmployees.Models.EmployeeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        create</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.FirstName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.FirstName) %>
            <%: Html.ValidationMessageFor(model => model.FirstName) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.LastName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.LastName) %>
            <%: Html.ValidationMessageFor(model => model.LastName) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Age) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Age) %>
            <%: Html.ValidationMessageFor(model => model.Age) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.IsCurrentEmployee) %>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.IsCurrentEmployee) %>
            <%: Html.ValidationMessageFor(model => model.IsCurrentEmployee) %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Gender) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Gender) %>
            <%: Html.ValidationMessageFor(model => model.Gender) %>
        </div>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
