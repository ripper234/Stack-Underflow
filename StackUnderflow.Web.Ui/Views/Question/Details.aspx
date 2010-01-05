<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Model.Entities.Question>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.ActionLink(Model.Title, "Details", new {Id = Model.Id, Class="question-self-link"})%></h2>
    <div class="question">
    <div class="text">
    <p>
    <%= Html.Encode(Model.Body) %>
    </p>
    </div>
    <div class="post-signature author">
        <div class="reltime">asked <span class="reltime-expanded"><%= TimeUtils.ToRelativeTimeDeatiled(Model.AskedOn) + " ago"%></span></div>
        <% Html.RenderPartial("SmallUserDetails", Model.Author); %>
    </div>
</asp:Content>
