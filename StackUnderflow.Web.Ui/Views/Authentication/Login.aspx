<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login or Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login or Register using OpenID</h2>
    <p>Please enter your openID to login or register</p>
    <div>
    <% using (Html.BeginForm("Authenticate", "Authentication"))
       { %>
        
            <fieldset>
                <p>
                    <%= Html.TextBox("openId", null, new {autofocus=true}) %>
                    <%= Html.ValidationMessage("openId")%>
                </p>
                <p>
                    <input type="submit" value="Login" />
                </p>
            </fieldset>
    <% } %>
    </div>

</asp:Content>
