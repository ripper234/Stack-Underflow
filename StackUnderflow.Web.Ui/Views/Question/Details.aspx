<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.ItemModel<StackUnderflow.Model.Entities.Question>>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Item.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.ActionLink(Model.Item.Title, "Details", new {Id = Model.Item.Id, Class="question-self-link"})%></h2>
    <div class="question">
    <div class="text">
    <p>
    <%= Html.Encode(Model.Item.Body) %>
    </p>
    </div>
    <div class="post-signature author">
        <div class="reltime">asked <span class="reltime-expanded">
        <%= TimeUtils.ToRelativeTimeDeatiled(Model.Item.AskedOn) + " ago"%></span></div>
        <% Html.RenderPartial("SmallUserDetails", Model.Item.Author); %>
    </div>
</asp:Content>
