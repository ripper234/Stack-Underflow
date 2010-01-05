<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="StackUnderflow.Model.Entities" %>
<%@ Import Namespace="StackUnderflow.Web.Ui.Utils" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= 
                       Html.Encode(UserContainer.GetUser(ViewData, Page.User.Identity.Name).Name)
                       %></b>!
        [ <%= Html.ActionLink("Logout", "Logout", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Login", "Login", "Authentication", new { returnUrl = ViewContext.HttpContext.Request.Url.PathAndQuery }, null)%> ]
<%
    }
%>
