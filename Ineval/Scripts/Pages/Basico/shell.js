
$.fn.formToObject = function() {
    var a = this.formToArray(), i, obj = {};
    for(i = 0; i < a.length; i++ ){
            obj[a[i]['name']] = a[i]['value'];
    }
    return obj;
};

var shell = {
	mostrar_video: false,
    init : function(){

        /*Eventos Aparecer y Desaparecer menú usuario */
        $("#li_profile").mouseenter(function() { $("#items_menu").show(); });
        $("#li_profile").mouseleave(function() { $("#items_menu").hide(); });

        $('#menu-principal ul.nav > li.dropdown  a').removeClass('dropdown-toggle');

        $("select, input[type='file']").addClass('form-control input-sm');
        $(".tablesorter").tablesorter(); 

        var el_error = $('ul.errorlist');
        var el = el_error.parent('div,td');
        el.addClass('has_error');

        $("#header-dd-notification").click(function(){
            $("#header-dd-notification .dropdown-menu").toggle();
        });

        $(document).click(shell.ocultar_notificaciones);
        $(document).click(shell.ocultar_videos);
        shell.bloquear_collapse_filtros();

    },

    ocultar_popover : function (e) {
        $('[rel="popover"]').each(function () {
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    },

    ocultar_notificaciones : function(e){
        var container = $("#contenedor_notif");
        if (!container.is(e.target) && container.has(e.target).length === 0)
        {
            container.hide();
        }
    },

    ocultar_videos : function(e){
        var container = $("#contenedor_videos");
        if (!container.is(e.target) && container.has(e.target).length === 0)
        {
            container.hide();
            shell.mostrar_video = false;
        }
    },

    bloquear_collapse_filtros: function(){

        $('.panel-collapse input:text').each(function(index){
            var $element = $(this);
            $element.keypress(function(e) {
                if(e.which == 10 || e.which == 13) { 
                    e.preventDefault();
                    this.form.submit();
                }
            });
        });
    }
};

$(function(){
    shell.init();
    // if(window.scrollY > 53){
    //         $('.top-menu-bar').css('top', '-53px');
    // }
    // $(window).scroll(function(e){
    //         var scrol = $(e.target).scrollTop();
    //         if(scrol < 53){
    //                 $('.top-menu-bar').css('top', '-'+scrol+'px');
    //                 $('.logo-contifico-menu').hide();
    //         }else{
    //                 if(scrol > 53){
    //                         $('.top-menu-bar').css('top', '-53px');
    //                         $('.logo-contifico-menu').fadeIn();
    //                 }
    //         }
    // });
    
    
    // $('[rel="tooltip"]').tooltip();
    
    // $("#zenbox_tab").addClass('hidden-xs');
    
    // var el_error = $('ul.errorlist');
    // var el = el_error.parent('div,td');
    // el.addClass('has_error');
    // asterisco en tabs
    // var tabs = $('.content_tabs');
    // $.each(tabs, function(k,v){
    //     var ff=$(v).find('ul.errorlist:first'); 
    //     var cl=$(ff).closest('.content_tabs');
    //     var idd = cl.attr('id');///glyphicon .glyphicon-asterisk
    //     $('#tabs ul li a[href="#'+idd+'"]').append("&nbsp;&nbsp;<span style='color:red;' class='glyphicon glyphicon-exclamation-sign'></span>");
    // });
    
    
});
