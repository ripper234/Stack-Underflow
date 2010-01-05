<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object[]>" %>
<tr>
<% foreach (var item in Model) {%>
<td><%= Html.Encode(item) %></td>
<% } %>
</tr>


