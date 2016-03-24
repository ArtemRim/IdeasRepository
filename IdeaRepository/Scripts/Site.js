
var IdeaId;
var IdeaEditing;
var DeletedByUser = new Object();
var DeletedByAdmin = new Object();
var ExistingIdeas = new Object();

var modalWindow,closeWindow,overlay;

var btnRemove = '<button name="btnDelete" class="btn btn-danger">Remove</button>';
var btnEdit = '<button name="btnEdit" class="btn btn-primary">Edit</button>';
var btnConfirm = '<button name="btnConfirm" class="btn btn-default">Yes</button>';
var btnRefuse = '<button name="btnRefuse" class="btn btn-default">No</button>';
var btnRestore = '<button name="btnRestore" class="btn btn-default">Restore</button>';
var btnRemoveByAdmin = '<button name="btnRemoveByAdmin" class="btn btn-danger">Remove</button>';
var btnRemoveByUser = '<button name="btnCancelDeleteByAdmin" class="btn btn-default">Cancel</button>';
var groupbtn = '<div class="btn-group" role="group">';
var confirmationToUser = 'Administrator want to remove this entry.Do you agree?';
var panel = '<div class="panel panel-default" id=';
var panelHeading = '<div class="panel-heading">';
var panelBody = '<div class="panel-body">';
var panelFooter = '<div class="panel-footer">';
var alignText = '<div class="alignText">';
var closingDiv = '</div>';
var closingQuote = '>';

var ElementByUser = '#IdeasDeletedByUser';
var ElementByAdmin = '#IdeasDeletedByAdmin';
var ExistingItems = '#IdeasOfUser';
var TitleExestItems = '<h4 id="TitleExistItems">All ideas user</h4>';
var TitleByAdmin = '<h4 id="TitleByAdmin">Ideas removed by admin</h4>';
var TitleByUser = '<h4 id="TitleByUser">Ideas removed by user</h4>';
var UserList = '#ListOfUsers';


var UrlSaveChanges = '/Ideas/SaveChanges';
var UrlRemoveIdea = '/Ideas/RemoveIdea';
var UrlAddIdea = '/Ideas/AddIdea';
var UrlRemoveConfirmed = '/Ideas/RemoveConfirmed';

var ListAllIdeas = '#ListAllIdeas';

$(document).ready(function () {
    updateIdeas();
    setInterval(updateIdeas, 10000);
});


$("#text").keyup(function () {
    var text = $("#text").val();
    if (CheckOnEmpty(text))
    {
        $("#btnAdd").removeClass("disabled");
        $("#btnSave").removeClass("disabled");
    }
    else
    {
        $("#btnAdd").addClass("disabled");
        $("#btnSave").addClass("disabled");
    }
        
});

function CheckOnEmpty(text)
{
    var countChar = text.replace(/\s+/g, '');
    if (countChar.length >= 1)
        return true;
    return false;
}



$("#btnAdd").click(function () {
    var idea = { 'TextIdea': $("#text").val() };
    AjaxRequestForUser(idea,UrlAddIdea);
    $("#text").val("");
});


$("#BtnConfirmDelete").click(function () {
    var id = { 'IdeaId': IdeaId };
    CloseModalWindow();
    AjaxRequestForUser(id, UrlRemoveIdea);
    $("#text").val("");
});

$("#BtnConfirmCancel").click(function () {
    IdeaId = null;
    CloseModalWindow();
});

$("#btnCancel").click(function () {
    IdeaId = null;
    IdeaEditing = null;
    $("#text").val("");
    ButtonsVisibality(false);
})

$("#btnSave").click(function () {
    IdeaEditing.Text = $("#text").val();
    var idea = { 'idea': IdeaEditing };
    AjaxRequestForUser(idea, UrlSaveChanges);
    ButtonsVisibality(false);
    $("#text").val("");
})

function ButtonsVisibality(isVisible)
{
    if (isVisible == true)
    {
        $("#btnSave").removeClass('hidden');
        $("#btnCancel").removeClass('hidden');
        $("#btnAdd").addClass('hidden');
    }
    else
    {
        $("#btnSave").addClass('hidden');
        $("#btnCancel").addClass('hidden');
        $("#btnAdd").removeClass('hidden');
    }
    
}

function btnEditClick() {
    IdeaId = $(this).parent().parent().parent().attr("id");
    ButtonsVisibality(true);
    var id = { 'IdeaId': IdeaId };
    GetIdea(id);
}

function btnDeleteClick() {
    IdeaId = $(this).parent().parent().parent().attr("id");
    InitModalWindowPropertyUsr();
    ShowModalWindow();
}

  
function DateTimeParse(jsondate) {
    jsondate = jsondate.replace(/[^0-9 +]/g, '');
    var DateTime = new Date(parseInt(jsondate));
    var options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric'
    };
    return DateTime.toLocaleString("en-US", options);
}


function AddIdeaToPage(Author, datetime, id, text,confirm) {
   
    $(ListAllIdeas).prepend(panel + id + closingQuote);
    if (confirm == true)
    {
        $('#' + id).prepend(panelFooter + confirmationToUser + groupbtn + btnConfirm + btnRefuse + closingDiv + closingDiv);
        $("button[name='btnConfirm']").first().click(ConfirmRemove);
        $("button[name='btnRefuse']").first().click(RefuseRemove);
    }
    else
        $('#' + id).prepend(panelFooter + groupbtn + btnRemove + btnEdit + closingDiv);
    $('#' + id).prepend(panelBody + text + closingDiv + closingDiv);
    $('#' + id).prepend(panelHeading + alignText + 'Author:' + Author + closingDiv + alignText + 'Date:' + datetime + closingDiv + closingDiv);
    $("button[name='btnDelete']").first().click(btnDeleteClick);
    $("button[name='btnEdit']").first().click(btnEditClick);
}

function ConfirmRemove(){
    AjaxRequestForUser(GetConfirmationResult(this, true), UrlRemoveConfirmed);
}

function RefuseRemove() {
    AjaxRequestForUser(GetConfirmationResult(this,false), UrlRemoveConfirmed);
}

function GetConfirmationResult(element,flag){
    IdeaId = $(element).parent().parent().parent().attr("id");
    return { IdeaId: IdeaId, isConfirmed: flag };
}

function AjaxRequestForUser(Data,Url){
    $.ajax({
        url: Url,
        type: 'POST',
        data: JSON.stringify(Data),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $(ListAllIdeas).children().remove();
            msg.forEach(function (v, i) {
                AddIdeaToPage(v.Author, DateTimeParse(v.Date), v.Id, v.Text, v.Confirm)
            });
        }
    });
}


function updateIdeas() {
    $.ajax({
        url: '/Ideas/GetAllIdeas',
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $(ListAllIdeas).children().remove();
            msg.forEach(function (v, i) {
                AddIdeaToPage(v.Author, DateTimeParse(v.Date), v.Id, v.Text, v.Confirm)
            });
        }
    });
}

function GetIdea(id){
    $.ajax({
        url: '/Ideas/GetIdea',
        type: 'POST',
        data: JSON.stringify(id),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $("#text").val(msg.Text);
            IdeaEditing = msg;
        }
    });
}

function InitModalWindowPropertyUsr(){
    modalWindow = '#modal_form';
    closeWindow = '#modal_close, #overlay';
    overlay = '#overlay';
}


function ShowModalWindow() {
    $(overlay).fadeIn(400, // снaчaлa плaвнo пoкaзывaем темную пoдлoжку
    function () { // пoсле выпoлнения предъидущей aнимaции
        $(modalWindow)
            .css('display', 'block') // убирaем у мoдaльнoгo oкнa display: none;
            .animate({ opacity: 1, top: '50%' }, 200); // плaвнo прибaвляем прoзрaчнoсть oднoвременнo сo съезжaнием вниз
    });
    /* Зaкрытие мoдaльнoгo oкнa, тут делaем тo же сaмoе нo в oбрaтнoм пoрядке */
    $(closeWindow).click(CloseModalWindow);
}

function CloseModalWindow() {
    $(modalWindow)
         .animate({ opacity: 0, top: '45%' }, 200,  // плaвнo меняем прoзрaчнoсть нa 0 и oднoвременнo двигaем oкнo вверх
             function () { // пoсле aнимaции
                 $(this).css('display', 'none'); // делaем ему display: none;
                 $(overlay).fadeOut(400); // скрывaем пoдлoжку
             }
         );
}

