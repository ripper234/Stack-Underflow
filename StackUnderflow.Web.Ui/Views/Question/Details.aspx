<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StackUnderflow.Web.Ui.Model.ItemModel<StackUnderflow.Model.Entities.Rich.RichQuestion>>" %>
<%@ Import Namespace="StackUnderflow.Web.Ui" %>
<%@ Import Namespace="StackUnderflow.Web.Ui.Utils" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Item.Question.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.ActionLink(Model.Item.Question.Title, "Details", new { Id = Model.Item.Question.Id, Class = "question-self-link" })%></h2>
    <div class="question">
        <table>
        <tr>
            <td class="votecol">
                <div class="votes">
                    <img title="This problem is interesting and well formed" alt="Upvote" src="/Content/plus.png" width="21" height="19" />
                    <span><%= Html.Encode(VoteUtil.VotesToString(Model.Item.Votes))%></span>
                    <img title="This problem is not interesting or confusing" alt="Downvote" src="/Content/minus.png" width="21" height="19" />
                </div>
            </td>
            <td class="question-body">
                <div class="text">
                    <p><%= Html.Encode(Model.Item.Question.Body)%></p>
                </div>
                <div class="post-signature author">
                    <div class="reltime">asked <span class="reltime-expanded">
                        <%= TimeUtils.ToRelativeTimeDeatiled(Model.Item.Question.AskedOn) + " ago"%></span>
                    </div>
                <% Html.RenderPartial("SmallUserDetails", Model.Item.Question.Author); %>
                </div>
            </td>
        </tr>
        </table>
    </div>
</asp:Content>
