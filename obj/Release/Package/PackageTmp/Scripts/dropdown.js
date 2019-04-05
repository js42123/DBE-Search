// dropdown
$(function(){   
    $("li.dropdown").hover(function() {
        if ($("this:has(.subNav)")){
            $(".subNav").css({'display':'none'});
            $(".subNav", this).css({'display':'block'});
            $("nav ul li").css({'position':'relative', 'z-index':'91'});    
            $(this).addClass("active");
        }
    }, function(){
        $(".subNav").css({'display':'none'});
        $(this).removeClass("active");
        $("nav ul li").css({'position':'relative', 'z-index':'1'});      
    });


    $(".overlay").mouseover(function() {
        $(".subNav").css({'display':'none'});
        $(this).removeClass("active");
        $("nav.dropdown ul li").css({ 'position': 'relative', 'z-index': '1' });      
    });
});