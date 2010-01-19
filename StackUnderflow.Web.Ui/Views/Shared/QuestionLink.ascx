<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StackUnderflow.Model.Entities.DB.Question>" %>
<%= Html.ActionLink(Model.Title, "Details", "Question", new { Id = Model.Id },
        new { Title = Model.Body, Class = "question-link" })%>