<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StackUnderflow.Web.Ui.Model.ModelBase>" %>
<%
    if (Model.LoggedInUser != null) {
%>
        Welcome <b><%= Html.Encode(Model.LoggedInUser.Name) %></b>!
        [ <%= Html.ActionLink("Logout", "Logout", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Login", "Login", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
%>
