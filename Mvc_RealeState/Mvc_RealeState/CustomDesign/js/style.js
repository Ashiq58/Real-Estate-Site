$(document).ready(function(){
		$("#collapseExample").hover(
        function() {
            $('#collapseExample', this).not('.in #collapseExample').stop(true,true).slideDown("400");
            $(this).toggleClass('open');        
        },
        function() {
            $('#collapseExample', this).not('.in #collapseExample').stop(true,true).slideUp("400");
            $(this).toggleClass('open');       
        }
	);
});