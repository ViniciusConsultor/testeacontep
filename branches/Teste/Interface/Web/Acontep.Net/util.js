function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}

function navigatorID(id)
{
    var detect = navigator.userAgent.toLowerCase();
    if(detect.indexOf(id) != -1)
        return true;
    else
        return false;
}

function navigatorIE()
{        
    return navigatorID('msie');
}


