
/* For header Fixed top */

jQuery(window).on("scroll", function () {

    if (jQuery(this).scrollTop() > 200) {

    	if($(window).width() > 319) {

	        jQuery("header").addClass('header-top');

	    }

    }

    else {

        jQuery("header").removeClass('header-top');

    }

});



/* For header Fixed top */





/* scroll section when scroll down */

$(document).ready(function(){

var $animation_elements = $('.animation-element');

var $window = $(window);



function check_if_in_view() {

  var window_height = $window.height();

  var window_top_position = $window.scrollTop();

  var window_bottom_position = (window_top_position + window_height);



  $.each($animation_elements, function() {

    var $element = $(this);

    var element_height = $element.outerHeight();

    var element_top_position = $element.offset().top;

    var element_bottom_position = (element_top_position + element_height);



    //check to see if this current container is within viewport

    if ((element_bottom_position >= window_top_position) &&

      (element_top_position <= window_bottom_position)) {

      $element.addClass('in-view');

    } else {

      $element.removeClass('in-view');

    }

  });

}



$window.on('scroll resize', check_if_in_view);

$window.trigger('scroll');

});

/* scroll section when scroll down */





/* For responsive menu */

	function openNav() {

		document.getElementById("mySidenav").style.width = "250px";

	}



	function closeNav() {

		document.getElementById("mySidenav").style.width = "0";

	}

