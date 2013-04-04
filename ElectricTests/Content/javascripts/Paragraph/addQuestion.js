window.onload = function() {
    //Show paragraph menu on paragraph hover
    $("tr").hover(
        function() {
            $(this).children("td").eq(1).children("div").eq(1).show("slow");
        },
        function() {
            $(this).children("td").eq(1).children("div").eq(1).hide("fast");
        }
    );
};

//Empty form and add paragraph number and text to form

function openForm(paragraphId, paragraphNumber) {
    //Foundation function to show modal window
    $("#addQuestionForm").reveal({
        "open": function() {
            emptyForm();
            addParagraphToForm(paragraphId);
        }
    });
}

//Empty all form input fields

function emptyForm() {
    $("#formParagraphText").val("");
    $("#Title").val("");
    $("#Option1").val("");
    $("#Option2").val("");
    $("#Option3").val("");
    $("#Option4").val("");
    $("#AnswerDescription").val("");
}


function addParagraphToForm(paragraphId) {
    $("#ParagraphId").val(paragraphId);
    //Get and trim paragraph number
    var fullParagraphNumber = trim1($("#number_" + paragraphId).text());
    //Get paragraph from JSON object
    var addedParagraph = searchParagraphInJSON(paragraphs, paragraphId);
    //Show paragraph and their child paragraphs with numbers in the form
    if(addedParagraph != null)
        $("#formParagraphText").html(getParagraph(addedParagraph, fullParagraphNumber));
}

function searchParagraphInJSON(paragraphsJSON, paragraphId) {
    var searchedParagraph = null;
    for (var i = 0; i < paragraphsJSON.length; i++) {
        // Just return paragraph by Id
        if (paragraphsJSON[i]["Id"] == paragraphId) {
            return paragraphsJSON[i];
        }
        //If Id not the same, search for paragraph in child paragraphs
        if (paragraphsJSON[i]["Paragraphs"] != undefined)
            searchedParagraph = searchParagraphInJSON(paragraphsJSON[i]["Paragraphs"], paragraphId);

        //If we found paragraph, stop iteration an return result
        if (searchedParagraph != null)
            return searchedParagraph;
    }
    return searchedParagraph;
}

function getParagraph(paragraph, fullParagraphNumber) {
    var result = "<p>" + fullParagraphNumber + " " + paragraph["Text"] + "</p>";

    if (paragraph["Paragraphs"] != null) {
        for (var i = 0; i < paragraph["Paragraphs"].length; i++) {
            var childParagraphNumber = fullParagraphNumber + paragraph["Paragraphs"][i]["Number"] + ".";
            result += getParagraph(paragraph["Paragraphs"][i], childParagraphNumber);
        }
    }
    return result;
}


function validateAndSendObject(object, withDescription) {
    if (!HaveValidationErrors(withDescription))
        sendQuestion(object);
}

function HaveValidationErrors(withDescription) {
    // There are some required input fields with these id
    var requiredFields = new Array("Title", "Option1", "Option2");
    if (withDescription)
        requiredFields.push("AnswerDescription");

    for (var i = 0; i < requiredFields.length; i++) {

        //If input field with these id empty then we have an input error
        if (checkForEmpty(requiredFields[i]))
            return true;
    }
    return false;
}

function checkForEmpty(elementId) {
    var element = $("#" + elementId);

    //If these field empty then mark field as error
    if (element.val() == "") {
        element.addClass("error");
        return true;
    }
    element.removeClass("error");
    return false;
}

function openFormWithoutParagraph() {
    $("#addQuestionFormWithoutParagraph").reveal({
        "open": function() {
            emptyForm();
        }
    });
}

function withNewLines(text) {
    return text.replace("\r\n", "<br />");
}

function trim1(str) {
    return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}
