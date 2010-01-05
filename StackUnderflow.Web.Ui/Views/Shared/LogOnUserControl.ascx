<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink("Logout", "Logout", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Login", "Login", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
%>
