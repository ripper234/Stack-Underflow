StackUnderflow.VoteResultType = {
            OK: 0,
            NOT_LOGGED_IN: 1,
        }
StackUnderflow.VoteType = {
            ThumbUp: 0,
            ThumbDown: 1,
        }
        
function recalcVotesDiff(wasOn, voteType)
{
    switch (voteType)
    {
        case StackUnderflow.VoteType.ThumbUp:
            if (wasOn)
                return -1;
            return +1;
            
        case StackUnderflow.VoteType.ThumbDown:    
            if (wasOn)
                return +1;
            return -1;
            
        default:
            throw new "Bad vote type";
    }
}

function toggleButton(button)
{
    var src = button.attributes["src"].nodeValue;
    if (src.indexOf("_on") != -1)
    {
        button.attributes["src"].nodeValue = src.replace("_on", "_off");
    }
    else
    {
        button.attributes["src"].nodeValue = src.replace("_off", "_on");
    }
}

function handleVote(element, voteType) {
    try {

        var questionId = element.parent().find("input").val();
        var wasOn = element.attr("src").indexOf("_on") != -1;
        var wasOff = element.attr("src").indexOf("_off") != -1;
        if (!wasOn && !wasOff)
        {
            alert("Wasn''t or nor off, something is wrong");
            return;
        }
        var diffMultiplier = 1;
        var src;
        if (wasOn)
        {    
            toggleButton(element.context);
        }
        else
        {
            otherOnElement = element.siblings("img").get(0);
            if (otherOnElement != null && otherOnElement.attributes["src"].nodeValue.indexOf("_on") != -1)
            {
                toggleButton(otherOnElement);
                diffMultiplier = 2;
            }
            toggleButton(element.context);
        }
        
        var totalVotesText = element.parent().find(".total-votes");
        var currentVotes = parseInt(totalVotesText.html(), 10);
        currentVotes += recalcVotesDiff(wasOn, voteType) * diffMultiplier;
        totalVotesText.html(currentVotes);
            
        $.post("/Vote/ProcessVote/", { questionId: questionId, voteType: voteType, wasOn: wasOn}, function (data) {
            // undo bad vote-up
            switch (data)
            {
            case StackUnderflow.VoteResultType.OK:
                // all is well, nothing to do
                break;
                
            case StackUnderflow.VoteResultType.NOT_LOGGED_IN:
                // TODO
                alert("not logged in");
                break;
                
            default:
                alert("Unexpected data: " + data);
                break;
            }
        }, "json");
    }
    catch (err)
    {
        alert("Failed: " + err);
    }
}
$(document).ready(function () {
    $("img.vote-up").click(function () { handleVote($(this), StackUnderflow.VoteType.ThumbUp); });
    $("img.vote-down").click(function () { handleVote($(this), StackUnderflow.VoteType.ThumbDown); });
});