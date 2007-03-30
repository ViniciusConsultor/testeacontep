var webControlCustomsControl
function webControlCustom(enable,visible,name)
{
    this.enable = enable;
    this.visible = visible;
    this.name = name;
}

function webControlCustoms(webControlCustom)
{
    if(webControlCustomsControl == null)
    {
        var items = new Array()
        items[0] = webControlCustom;    
    }
    else
    {
	    var items = new Array();
	    items = webControlCustomsControl;
	    items[items.length] = webControlCustom         
	}
}


function EnableItems(items,enabled)
{
    for(var i = 0; i<items.length;i++)
    {            
        if(document.getElementById(items[i]) != null)
        {
        	document.getElementById(items[i]).disabled = !enabled;
        }
    }
}

function VisibleItems(items,visibled)
{
    for(var i = 0; i<items.length;i++)
    {            
        if(document.getElementById(items[i]) != null)
        {
            var vista = (visibled == true) ? 'visible' : 'hidden';            
            document.getElementById(items[i]).style.visibility = vista;
        }
    }
}

