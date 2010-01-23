<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.ItemsModel<StackUnderflow.Model.Entities.Rich.RichQuestion>>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Recent Questions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Recent Questions</h2>

    <% foreach (var item in Model.Items) { %>
        <div id="<%= Html.Encode(item.Question.Id) %>" class="question-summary">
            <div class="votes">
                <div><%= Html.Encode(item.Votes) %></div>
                <div>votes</div>
            </div>
            <div class="answers">
                <div><%= Html.Encode(item.AnswerCount) %></div>
                <div>answers</div>
            </div>
            <div class="body">
                <h3><% Html.RenderPartial("QuestionLink", item.Question); %></h3>
                <div class="shortinfo">
                    <span class="reltime"><%= TimeUtils.ToRelativeTime(item.Question.UpdateDate) %></span>
                    <% Html.RenderPartial("UserName", item.Question.LastRelatedUser); %>
                    <span class="reputation"><%= item.Question.LastRelatedUser.Reputation %></span>
                </div>
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

