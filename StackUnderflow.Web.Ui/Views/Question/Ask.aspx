<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.EmptyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ask
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ask a question</h2>
    <div class="ask-question-form">
        <% using (Html.BeginForm("Ask", "Question", FormMethod.Post)) {%>
        <fieldset>
            <div class="title">
                <label>Title</label>
                <input name="title"/>
            </div>
            <div><label>What's your math problem? Be specific and concise.</label></div>
            <div class="question-body">
                <textarea name="body" rows="20" cols="100"></textarea>
            </div>
            <input type="submit" value="Submit your question" />
        </fieldset>
        <% }%>
    </div>
</asp:Content>
