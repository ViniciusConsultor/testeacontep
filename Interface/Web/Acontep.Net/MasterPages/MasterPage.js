//function controleMenu(load)
//{
//    var menuStatusID = "ctl00_menuStatus";

//    var campoControle =   document.getElementById(menuStatusID);
//    //Invcert o status para cousar a atribuição do valor repassado pelo servidor    
//    if(load)
//        campoControle.value == 'true' ? 'false' : 'true';
//    
//    if(campoControle.value == 'true')
//    {
//        if(!load)
//            campoControle.value = "false";
//        document.getElementById('colunaSistema').style.width = '182px';
//        document.getElementById('divMenuSistema').style.left = '0px';
//        //document.getElementById('tableMenuSistema').style.width = '182px';        
//        //document.getElementById('colunaConteudo').style.left = '593px';        
//    }
//    else
//    {
//        if(!load)
//            campoControle.value = "true";
//        document.getElementById('colunaSistema').style.width = '5px';
//        document.getElementById('divMenuSistema').style.left = '-200px';
//        //document.getElementById('tableMenuSistema').style.width = '0px';
//        //document.getElementById('colunaConteudo').style.left = '755px';
//    }
//}
//function esconderMenu(load)
//{
//    if(load != true)
//        document.getElementById('ctl00_menuStatus').value = document.getElementById('ctl00_menuStatus').value == 'true' ? 'false' : 'true';        
//    if(document.getElementById('ctl00_menuStatus').value != 'true')
//    {
//        document.getElementById('colunaSistema').style.width = '5px';
//        document.getElementById('divMenuSistema').style.left = '-200px';
//        document.getElementById('tableMenuSistema').style.width = '-200px';        
//        //document.getElementById('colunaConteudo').style.left = '755px';
//    }
//    else
//    {
//        document.getElementById('colunaSistema').style.width = '182px';
//        document.getElementById('divMenuSistema').style.left = '0px';
//        //document.getElementById('colunaConteudo').style.left = '593px';
//    }
//}


