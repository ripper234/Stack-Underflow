<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.ItemsModel<StackUnderflow.Model.Entities.Question>>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Recent Questions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Recent Questions</h2>

    <% foreach (var item in Model.Items) { %>
        <div id="<%= Html.Encode(item.Id) %>" class="question-summary">
            <h3><% Html.RenderPartial("QuestionLink", item); %></h3>
            <div class="shortinfo">
                <span class="reltime"><%= TimeUtils.ToRelativeTime(item.UpdateDate) %></span>
                <% Html.RenderPartial("UserName", item.LastRelatedUser); %>
                <span class="reputation"><%= item.LastRelatedUser.Reputation %></span>
            </div>
        </div>
    <% } %>
        
        <%--<tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Text) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.UpdateDate)) %>
            </td>
        </tr>--%>
    
<%--
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>--%>

</asp:Content>

