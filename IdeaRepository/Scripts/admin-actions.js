var UrlRemoveIdeaByAdmin = '/Ideas/RemoveIdeaByAdmin';
var UrlRestoreIdea = '/Ideas/RestoreIdea';
var UrlCancelDeletion = '/Ideas/CancelDeletionByAdmin';
var UrlGetUserIdeas = '/Ideas/GetUserIdeas';

var BtnConfirmDeletion = '#BtnConfirmDeletion';
var BtnCancelDelition = '#BtnCancelDelition';

$(document).ready(function () {
    updateUsers();
});


function AddUserToPage(Name, id) {
    if (Name != "admin") {
        $(UserList).prepend('<li class="list-group-item"><a href="#" id="'+ id + '">' + Name + '</a></li>');
        $('#' + id).first().click(linkUserClick);
    }

}


function linkUserClick() {  
    AjaxRequestForAdmin(GetSelectedUser(this), UrlGetUserIdeas);
}

function btnCancelDeleteByAdmin() {
    AjaxRequestForAdmin(GetSelectedItem(this), UrlCancelDeletion);
}

function btnRemoveByAdminClick() {
    InitModalWindowProperty();
    ShowModalWindow();
    IdeaId = GetSelectedItem(this);
}


$(BtnConfirmDeletion).click(function () {
    CloseModalWindow();
    AjaxRequestForAdmin(IdeaId, UrlRemoveIdeaByAdmin);
});

$(BtnCancelDelition).click(function () {
    IdeaId = null;
    CloseModalWindow();
});
function btnRestoreClick() {

    AjaxRequestForAdmin(GetSelectedItem(this), UrlRestoreIdea);
}

function AddIdeaOfUser(Author, datetime, id, text, confirm, isDeletedByUser, isDeletedByAdmin) {
    AddExistingItems(Author, datetime, id, text, confirm, isDeletedByUser, isDeletedByAdmin);
    AddRemovesItems(isDeletedByUser, Author, datetime, id, text, ElementByUser, DeletedByUser, TitleByUser);
    AddRemovesItems(isDeletedByAdmin, Author, datetime, id, text, ElementByAdmin, DeletedByAdmin, TitleByAdmin);

}


function AddExistingItems(Author, datetime, id, text, confirm, isDeletedByUser, isDeletedByAdmin) {
    if ((isDeletedByUser == false) && (isDeletedByAdmin == false)) {
        CheckOnTitle(ExistingItems, ExistingIdeas, TitleExestItems);
        $(ExistingItems).prepend(panel + id + closingQuote);
        if (confirm == true) {
            $('#' + id).prepend(panelFooter + 'Confirmation is expected..' + btnRemoveByUser + closingDiv);
            $("button[name='btnCancelDeleteByAdmin']").first().click(btnCancelDeleteByAdmin);
        }
        else {
            $('#' + id).prepend(panelFooter + btnRemoveByAdmin + closingDiv);
            $("button[name='btnRemoveByAdmin']").first().click(btnRemoveByAdminClick);
        }
        $('#' + id).prepend(panelBody + text + closingDiv + closingDiv);
        $('#' + id).prepend(panelHeading + alignText + 'Author:' + Author + closingDiv + alignText + 'Date:' + datetime + closingDiv + closingDiv);
    }
}

function AddRemovesItems(isDeleted, Author, datetime, id, text, Element, Deleted, Title) {
    if (isDeleted == true) {
        CheckOnTitle(Element, Deleted, Title);
        AddPanel(Element, Author, datetime, id, text);
    }
}



function CheckOnTitle(element, user, title) {
    if (user.count == 0) {
        $(element).before(title);
        user.count++;
    }
}

function AddPanel(element, Author, datetime, id, text) {
    $(element).prepend(panel + id + closingQuote);
    $('#' + id).prepend(panelFooter + btnRestore + closingDiv);
    $('#' + id).prepend(panelBody + text + closingDiv + closingDiv);
    //$('#' + id).prepend(panelHeading + Author + datetime + closingDiv);
    $('#' + id).prepend(panelHeading + alignText + 'Author:' + Author + closingDiv + alignText + 'Date:' + datetime + closingDiv + closingDiv);
    $('#' + id).find("button[name='btnRestore']").click(btnRestoreClick);
}




function GetSelectedItem(element)
{
    IdeaId = $(element).parent().parent().attr("id");
    return { 'IdeaId': IdeaId };
}

function GetSelectedUser(element) {
    var UserId = $(element).attr("id");
    return { 'UserId': UserId };
}


function AjaxRequestForAdmin(id, Url) {
    $.ajax({
        url: Url,
        type: 'POST',
        data: JSON.stringify(id),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            ClearPage();
            InitializeCountItems();
            msg.forEach(function (v, i) {
                AddIdeaOfUser(v.Author, DateTimeParse(v.Date), v.Id, v.Text, v.Confirm, v.DeletedByUser, v.DeletedByAdmin)
            });
        }
    });
}



function updateUsers() {
    $.ajax({
        url: '/Ideas/GetAllUsers',
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $(UserList).children().remove();
            msg.forEach(function (v, i) {
                AddUserToPage(v.Login, v.Id)
            });
        }
    });
}


function ClearPage() {
    $("#IdeasOfUser").children().remove();
    $("#IdeasDeletedByUser").children().remove();
    $("#IdeasDeletedByAdmin").children().remove();
    $("#TitleExistItems").remove();
    $("#TitleByUser").remove();
    $("#TitleByAdmin").remove();
}

function InitModalWindowProperty()
{
    modalWindow = '#modal_form_admin';
    closeWindow = '#modal_close_admin, #overlay_admin';
    overlay = '#overlay_admin';
}

function InitializeCountItems() {
    DeletedByUser.count = 0;
    DeletedByAdmin.count = 0;
    ExistingIdeas.count = 0;
}