<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StackUnderflow.Model.Entities.User>" %>

<div class="user-details">
<div class="gravatar-small"></div>
<% Html.RenderPartial("UserName", Model); %>
</div>