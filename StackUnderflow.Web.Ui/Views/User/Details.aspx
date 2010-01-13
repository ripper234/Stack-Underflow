<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.ItemModel<StackUnderflow.Model.Entities.User>>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Item.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Item.Name) %></h2>
    <div class="user-details">
        <div class="user-details-image">
            <div class="gravatar"></div>
            <div class="reputation">
                <p class="large-number"><%= Html.Encode(Model.Item.Reputation)%></p>
                <p>reputation</p>
            </div>
        </div>
        <table class="user-details-info">
        <tbody>
            <% Html.RenderPartial("TableRow", new[] { "name", Model.Item.Name }); %>
            <% Html.RenderPartial("TableRow", new[] { "member for", TimeUtils.ToRelativeTimeDeatiled(Model.Item.SignupDate) }); %>
        </tbody>
        </table>
    </div>
</asp:Content>
