var flagAutoPostBack = null;
var flagInput = null;
var controlGrids = null;
var gridsInterval = setInterval(controlGridsCheck, 100);
var flagInterval = false;

function CheckAll(me,postback,start)
{
    flagAutoPostBack = true;
    var index = me.name.indexOf('_HeaderButton');  
    var prefix = me.name.substr(0,index);
    prefix = prefix.replace(/_/g,'$');
    //prefix = Replace(prefix,'$','_');
    for(var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix && o.name.substring(o.name.length-6,o.name.length) == "Custom")
                {
                    if(start)
                    {
                        
                    }
                    else
                    {
                        // Must be this way
                        o.checked = !me.checked;                     
                        o.click();
                    }
                }
           }
        }
    }
    flagAutoPostBack = false;
    if(postback)
	{
        __doPostBack(me.parentNode.parentNode.parentNode.parentNode.id,'Select$-1');
 	}
}

function gridThema(foreColor, backColor, bold, selectedForeColor, selectedBackColor, selectedBold, alternatForeColor, alternatBackColor, alternatBold)
{
    this.foreColor = foreColor;
    this.backColor = backColor;
    this.bold = bold;
    this.selectedForeColor = selectedForeColor;
    this.selectedBackColor = selectedBackColor;
    this.selectedBold = selectedBold;
    this.alternatForeColor = alternatForeColor;
    this.alternatBackColor = alternatBackColor;
    this.alternatBold = alternatBold;
}

function gridControl(me,thema,itemsControl)
{
    this.me = me;
    this.thema = thema;
    this.itemsControl = itemsControl;
}

function gridCheck(me,thema,itemsControl)
{
    if(controlGrids == null)
    {
        var items = new Array()
        items[0] = new gridControl(me,thema,itemsControl);
        return items
    }
    else
    {
		var items = new Array()
		items = controlGrids;
		items[items.length] = new gridControl(me,thema,itemsControl);
        return items;
    }
}

function controlGridsCheck()
{
    if(controlGrids != null)
    {
        for(var i = 0; i<controlGrids.length;i++)
        {
            if(VerifyCheckGrid(controlGrids[i].me, controlGrids[i].thema))
            {
                EnableItems(controlGrids[i].itemsControl.enable,true);
                VisibleItems(controlGrids[i].itemsControl.visible,true);            
            }
        }
    }
    if(!flagInterval)
    {
        flagInterval = true;
    }
    else
    {
        clearInterval(gridsInterval);
    }
}

function ApplyStyle(me, thema,  gridviewHeaderId, controls, row, postBack, alternat, rowsClickAble) 
{
    if(!flagAutoPostBack && !flagInput && !rowsClickAble)
	{
        return;
    }
    if (me.parentNode == null)
	{
		return;
	}
    var tt = null;
    if(navigatorIE())
	{
		tt = me.childNodes[0].children[0];
	}
    else
	{
		tt = me.childNodes[1].childNodes[0];
	}
    var parent = tt;
    do
    {
        if(parent.disabled)
		{
			return;
		}
        else
		{
			parent = parent.parentNode;
		}                
    }
    while(parent != null);
    if(tt.type == null)
    {
        for(var i = 0;i<=tt.children.length;i++)
        {
            if(tt.children[i].type != null)
            {
            	tt = tt.children[i];
            }
        }        
    }
    switch (tt.type)
    {
        case 'radio' :            
            flagAutoPostBack = postBack;
            ControleRadioButton(tt,thema);
            tt.checked = true;
            flagAutoPostBack = false;
            break;
        case 'checkbox' :
            if(!flagInput)
            {
            	tt.checked = !tt.checked;
            }
            break;        
    }
    if(controls != null && !postBack)
    {
        if(VerifyCheckGrid(me.parentNode.parentNode.id,thema))
        {
            EnableItems(controls.enable,true);
            VisibleItems(controls.visible,true);
        }
        else
        {
            EnableItems(controls.enable,false);
            VisibleItems(controls.visible,false);
        }
    }    
    ConfigThema(me, tt.checked, thema, alternat);
     if(!tt.checked)
     {
        if(document.getElementById(gridviewHeaderId) != null)
        {
			document.getElementById(gridviewHeaderId).checked = false;
        }
     }   
    flagInput = false;
    if(postBack && !flagAutoPostBack)
    {
    	__doPostBack(me.parentNode.parentNode.id.replace(/_/g,'$'),'Select$' + row);
    }
        //__doPostBack(document.getElementById(me.parentNode.parentNode.id),'Select$' + row);
}

function ControleRadioButton(me,thema)
{
    var posfixo = '$SelectRadioButtonCustom';  
    var me_prefixo = PegarPrefixo(me.name,posfixo);
	var prefixo = '';
	var alternat = false;
    for(var i=0; i<document.forms[0].length; i++)
    {
        var o = document.forms[0][i];
        if (o.type == 'radio')
        {
			prefixo = PegarPrefixo(o.name,posfixo);
			if(me_prefixo == prefixo)
			{
			    if(!flagAutoPostBack)
			    {
		            if(alternat)
		            {
			            o.parentNode.parentNode.style.fontWeight = thema.alternatBold;
                        o.parentNode.parentNode.style.color = thema.alternatForeColor;
                        o.parentNode.parentNode.style.backgroundColor = thema.alternatBackColor;
                        alternat = false;
		            }
		            else
		            {
			            o.parentNode.parentNode.style.fontWeight = thema.bold;
                        o.parentNode.parentNode.style.color = thema.foreColor;
                        o.parentNode.parentNode.style.backgroundColor = thema.backColor;
                        alternat = true;
                    }
                }
			    o.checked = false;
			}
        }
    }
}

function ConfigThema(me, checked, thema, alternat, type, controls)
{
    var parent = me;
    if(type == 'mouseover' || type == 'mouseout')
    {
        do
        {
            if(parent.disabled)
            {
            	return;
            }
            else
			{
				parent = parent.parentNode;
			}
        }
        while(parent != null);
    }
    if(navigatorIE())
    {
        if(me.childNodes[0].childNodes[0].checked && type != null && controls != null)
        {
            EnableItems(controls.enable,true);
            VisibleItems(controls.visible,true);
        }
    }
    else
    {
        if(me.childNodes[1].childNodes[0].checked && type != null && controls != null)
        {
            EnableItems(controls.enable,true);
            VisibleItems(controls.visible,true);
        }
    }
    if(checked)
    {
        if(type != 'mouseover')
        {
            me.style.fontWeight = thema.selectedBold;// bold
            me.style.color = thema.selectedForeColor;
        }
        me.style.backgroundColor = thema.selectedBackColor;
    }
    else
    {
        if(alternat)
        {
            if(type != 'mouseout')
            {                
                me.style.fontWeight = thema.alternatBold;
                me.style.color = thema.alternatForeColor;
                me.style.backgroundColor = thema.alternatBackColor;
            }
            else if(!VerifyCheckRow(me,thema,alternat))
            {
            	me.style.backgroundColor = thema.alternatBackColor;
            }
        }
        else
        {
            if(type != 'mouseout')
            {
                me.style.fontWeight = thema.bold;
                me.style.color = thema.foreColor;
                me.style.backgroundColor = thema.backColor;
            }
            else if(!VerifyCheckRow(me,thema,alternat))
            {
            	me.style.backgroundColor = thema.backColor;
            }
        }
    } 
}

function VerifyCheckGrid(me, thema)
{    
    //Necessario devido a chamado feito a esse metodo dinamicamente pelo sistema 
    //imposibilitado de referenciar o objeto ja que nao e uma variavel
    if(me.childNode == null)
    {
    	me = document.getElementById(me);
    }
    if(me == null)
    {
    	return false;
    }
    var control = null;
    var retorno = false;
    var item = null;
	var i = 0;
	var alternat = false;
    if(navigatorIE())
    {       
         control = me.childNodes[0];
         for(i=1;i<control.childNodes.length;i++)
         {            
            if(VerifyCheckRow(control.childNodes[i],thema,alternat))
                retorno = true;
            alternat = !alternat;
            
//            if(control.childNodes[i].childNodes[0].childNodes[0].name == null)
//            {
//            	item = control.childNodes[i].childNodes[0].childNodes[0].childNodes[0];
//            }
//            else
//			{
//				item = control.childNodes[i].childNodes[0].childNodes[0];
//			}                
//            if(item != null)
//			{
//                if(item.checked)
//                {
//                    retorno = true;
//                    control.childNodes[i].style.fontWeight = thema.selectedBold;
//                    control.childNodes[i].style.color = thema.selectedForeColor;
//                    control.childNodes[i].style.backgroundColor = thema.selectedBackColor;
//                }
//                else
//                {
//                	item.checked = false;
//                }
//			}
         }
     }
     else
     {
         control = me.childNodes[1];
         for(i=1;i<control.childNodes.length-1;i++)
         {
            if(VerifyCheckRow(control.childNodes[i],thema,alternat))
                retorno = true;
            alternat = !alternat;            
//            if(control.childNodes[i].childNodes[1].childNodes[0].checked)
//            {
//                retorno = true;
//                control.childNodes[i].style.fontWeight = thema.selectedBold;
//                control.childNodes[i].style.color = thema.selectedForeColor;
//                control.childNodes[i].style.backgroundColor = thema.selectedBackColor;                
//            }
         }
     }
     return retorno;
}

function VerifyCheckRow(me, thema, alternat)
{
    var control = null;
    var retorno = false;
    if(navigatorIE())
    {
        control = me;
        if(control.childNodes[0].childNodes[0].name != null)
        {
            if(control.childNodes[0].childNodes[0].name.indexOf('CheckBoxCustom') != -1 || control.childNodes[0].childNodes[0].name.indexOf('RadioButtonCustom') != -1)
            {
                if(control.childNodes[0].childNodes[0].checked)
                {
                    retorno = true;
                    control.style.fontWeight = thema.selectedBold;
                    control.style.color = thema.selectedForeColor;
                    control.style.backgroundColor = thema.selectedBackColor;
                }
                else
                {
                    if(alternat != null && alternat == false)
                    {
                        control.style.fontWeight = thema. bold;
                        control.style.color = thema.foreColor;
                        control.style.backgroundColor = thema.backColor;
                    }
                    else
                    {
                        control.style.fontWeight = thema.alternatBold;                    
                        control.style.color = thema.alternatForeColor;
                        control.style.backgroundColor = thema.alternatBackColor;
                    }
                }
            }
         }
         else
         {
            if(control.childNodes[0].childNodes[0].childNodes[0] != null)
            {
                if(control.childNodes[0].childNodes[0].childNodes[0].checked)
                {
                    retorno = true;
                    control.style.fontWeight = thema.selectedBold;
                    control.style.color = thema.selectedForeColor;
                    control.style.backgroundColor = thema.selectedBackColor;
                }
                else
                {
                    if(alternat != null && alternat == false)
                    {
                        control.style.fontWeight = thema. bold;
                        control.style.color = thema.foreColor;
                        control.style.backgroundColor = thema.backColor;
                    }
                    else
                    {
                        control.style.fontWeight = thema.alternatBold;                    
                        control.style.color = thema.alternatForeColor;
                        control.style.backgroundColor = thema.alternatBackColor;
                    }
                }
            }
         }
     }
     else
     {
         control = me;
         for(var i=1;i<control.childNodes.length-1;i++)
         {
            if(control.childNodes[1].childNodes[0].checked)
            {
                retorno = true;
                control.style.fontWeight = thema.selectedBold;
                control.style.color = thema.selectedForeColor;
                control.style.backgroundColor = thema.selectedBackColor;                
            }
            else
            {
                if(alternat != null && alternat == false)
                    {
                        control.style.fontWeight = thema. bold;
                        control.style.color = thema.foreColor;
                        control.style.backgroundColor = thema.backColor;
                    }
                    else
                    {
                        control.style.fontWeight = thema.alternatBold;                    
                        control.style.color = thema.alternatForeColor;
                        control.style.backgroundColor = thema.alternatBackColor;
                    }
            }
         }
     }
     return retorno;
}

//Nao conseguir Fazer Recurcivo nao esta passando os objetos dos controle filhos
// tentativa em codigo comentado
function VerifyCheck(me, setStyle, thema)
{
    if(me.childNode == null)
	{
		me = document.getElementById(me);
	}                
    if(me == null)
	{
		return false;
	}        
    var cont = 0;
    var control = null;
	var i = 0;
    if(navigatorIE())
    {       
         control = me.childNodes[0];
         for(i=1;i<control.childNodes.length;i++)
         {
            if(control.childNodes[i].childNodes[0].childNodes[0].checked)
            {
                if(setStyle)
                {
                    control.childNodes[i].style.fontWeight = thema.selectedBold;
                    control.childNodes[i].style.color = thema.selectedForeColor;
                    control.childNodes[i].style.backgroundColor = thema.selectedBackColor;
                }
                else
				{
					return true;
				}
            }
         }
     }
     else
     {
         control = me.childNodes[1];
         for(i=1;i<control.childNodes.length-1;i++)
         {
            if(control.childNodes[i].childNodes[1].childNodes[0].checked)
            {
                if(setStyle)
                {
                    control.childNodes[i].style.fontWeight = thema.selectedBold;
                    control.childNodes[i].style.color = thema.selectedForeColor;
                    control.childNodes[i].style.backgroundColor = thema.selectedBackColor;                
                }
                else
                {
                	return true;
                }
            }
         }
     }
     return false;
//    for(i=0; i<me.childNodes.length;i++)
//    {
//        if(navigatorIE())
//            control = me.childNodes[i];
//        else
//            control = me.childNodes[i+1];
//              
//        if(control.childNodes.length > 0)
//        {
//            for(j=0; j<control.childNodes.length;j++)
//            {
//                if(VerifyCheck(control.childNodes[j]))
//                    return true;            
//            }
//        }
//        if(control.name != null)
//        {
//            if (control.name.substring(control.name.length-6,control.name.length) == "Custom")
//                if(control.checked)
//                    return true;
//        }
//    }
//    return false;    
}

function FlagInput()
{
    flagInput = true;
}

function Replace(value, oldchar, newchar)
{
    for(var i =0;i<value.length;i++)
    {
        if(value[i] == oldchar)
        {
        	value[i] = newchar;
        }		
    }
    return value;
}

function PegarPrefixo(name, posfixo)
{
	var prefixo = '';
	if(name.length > posfixo.length)
	{
		if(name.substring(name.length - posfixo.length,name.length) == posfixo)
		{
			prefixo = name.substring(0,name.length - posfixo.length);
			var controle = 0;
			for(controle = prefixo.length;controle>0; controle--)
			{
				if(prefixo.substring(controle-1,controle) == '$')
				{
					return prefixo.substring(0,controle-1);
				}				                        
			}
		}
	}
}

function showPopUp(name, visibled)
{
    document.getElementById(name).style.visibility = (visibled == true) ? 'visible' : 'hidden';   
}