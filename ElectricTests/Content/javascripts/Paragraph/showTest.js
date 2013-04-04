//Question quantity
var questionNumber;

//User answers with option numbers
var answerArray;

//Progress line with animation
var progressElement;

//Add this part to progress for one question
var addToWidth;

var pageWidth;

//Element with all questions
var questionContainer;

//Element with current question and options
var currentQuestion;

//Question slide in/out animation time
var animationSpeed;

window.onload = function () {
    answerArray = new Array();
    
    //Progress indicator 
    progressElement = $("#progress");
    pageWidth = 920;
    addToWidth = Math.round($('.row').width() / window.questions.length);
    addToWidth = "+=" + addToWidth;

    //Get question container
    questionContainer = $('.questionContainer');

    //Open current question
    questionNumber = 0;
    animationSpeed = 500;
    openElement(questionNumber);
};

function GetQuestionByNumber() {
    var id = questions[questionNumber]["Id"];
    return $("#" + id);
}

function selectOption(optionNumber) {
    //Save answer to answer array
    answerArray[questionNumber] = optionNumber;
    hideElement(currentQuestion);

    if (questionNumber < questions.length - 1) {
        //Open new modal with next question
        openElement(questionNumber++);
        progressElement.animate({ width: addToWidth }, animationSpeed);

    } else {
        hideElement(currentQuestion);
        progressElement.animate({ width: pageWidth }, animationSpeed);
        progressElement.hide(animationSpeed);
        showResults(answerArray);
    }
}

function prepareQuestion(currentQuestion) {
    //Shuffle question options
    shuffle(currentQuestion.find("#options div"));

    

    //Position state for current question
    currentQuestion.css("position", "absolute");
    var left = questionContainer.offset().left - pageWidth - 1000;
    currentQuestion.css("left", left);
    currentQuestion.css("top", questionContainer.top);
    currentQuestion.css("opacity", "0.8");
    currentQuestion.css("display", "block");

    equalAllOptionsHeight(currentQuestion.find("#options"));
}

function openElement(questionNumber) {
    currentQuestion = GetQuestionByNumber(questionNumber);
    prepareQuestion(currentQuestion);
    showQuestion(currentQuestion);
}

function showQuestion(currentQuestion) {
    var left = questionContainer.offset().left;
    currentQuestion.animate({ left: left }, animationSpeed);
    currentQuestion.animate({ opacity: "1" }, animationSpeed, function () {
    });
}

function hideElement(currentQuestion) {
    var left = currentQuestion.offset().left + pageWidth + 1000;
    currentQuestion.animate({ left: left }, animationSpeed);
    currentQuestion.animate({ opacity: "0.5" }, animationSpeed, function () {
        currentQuestion.css("display", "none");
    });
}

function showResults(answerArray) {
    var resultTable = "";
    var questionNumber;

    for (var i = 0; i < answerArray.length; i++) {

        //Show question with theis number
        questionNumber = i + 1;
        resultTable += "<tr>";
        resultTable += "<td width='30'>" + questionNumber + "</td>";
        resultTable += "<td>" + questions[i]["Title"] + "</td>";
        resultTable += "</tr>";

        //Show answer, if incorrect we will add description 
        if (answerArray[i] == 1) {
            resultTable += "<tr>";
            resultTable += " <td colspan='2' class='correctAnswer'><div>" + withNewLines(questions[i]["Option1"]) + "</div></td>";
            resultTable += "</tr>";
        } else {
            var correctAnswer = questions[i]["Option1"];
            var incorrectAnswer = questions[i]["Option" + answerArray[i]];
            resultTable += "<tr>";
            resultTable += " <td colspan='2' class='incorrectAnswer'>" + withNewLines(incorrectAnswer) + "</td>";
            resultTable += "</tr>";

            
            if (questions[i]["Paragraph"] != undefined) {
                resultTable += "<tr>";
                resultTable += "<td colspan='2' class='descriptionToAnswer'>"
                    + questions[i]['FormattedDocument']['Title'] + "<br />";
                resultTable += getParagraph(questions[i]["Paragraph"]);
                resultTable += "</td></tr>";
            }
            
            if (questions[i]["AnswerDescription"] != undefined) {
                resultTable += "<tr>";
                resultTable += "<td colspan='2' class='descriptionToAnswer'>" + withNewLines(questions[i]["AnswerDescription"]) + "</td>";
                resultTable += "</tr>";
            }
        }
    }
    $("#resultTable").append(resultTable);
    animateResultTableIn();
}
function getParagraph(questionParagraph, parentParagraphNumber) {
    var parentNumber = (parentParagraphNumber === undefined) ? "" : parentParagraphNumber + ".";
    result = "<br>" +parentNumber + questionParagraph["Number"] + ". " + withNewLines(questionParagraph["Text"]);
    if (questionParagraph["Paragraphs"] != null) {
        for(var i = 0; i < questionParagraph["Paragraphs"].length; i++) {
            result += getParagraph(questionParagraph["Paragraphs"][i], questionParagraph["Number"]);     
        }    
    }
    return result;
}

function withNewLines(text) {
    return text.replace("\r\n", "<br />");
}


function animateResultTableIn() {
    var results = $("#results");
    results.css("position", "absolute");
    var top = 130;
    results.css("top", -(results.height()));
    results.css("display", "block");
    setTimeout(function () {
        results.animate({ top: top }, animationSpeed);
    }, animationSpeed + 100, function () {
        results.css("position", "relative");
    });
}


function shuffle(e) {               // pass the divs to the function
    var replace = $('<div>');
    var size = e.size();

    while (size >= 1) {
        var rand = Math.floor(Math.random() * size);
        var temp = e.get(rand);      // grab a random div from our set
        replace.append(temp);        // add the selected div to our new set
        e = e.not(temp); // remove our selected div from the main set
        size--;
    }
    currentQuestion.find("#options").html(replace.html());// update our container div with the
    // new, randomized divs
}

function equalAllOptionsHeight(elementArray) {
    var option1 = elementArray.find("#option1").height();
    var option2 = elementArray.find("#option2").height();
    var option3 = elementArray.find("#option3").height();
    var option4 = elementArray.find("#option4").height();

    var max = Math.max(option1, option2, option3, option4);
    elementArray.find("div").height(max);
}