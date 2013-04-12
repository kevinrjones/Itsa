/* 
jquery.checkbox.js
jQuery checkbox styling plugin

Styles a checkbox with a sprite, supporting hover, checked, disabled, active.  
*/
(function ($){
	$.fn.checkbox = function(method) {
		method = method || "new";
		var elements = $(this);
		for (var i = 0; i < elements.length; i++) {
			var element = $(elements[i]);
			if (method == "refresh") {
				var bgElement = element.parent().parent();
				if(element.is(':disabled')) {
					bgElement.addClass("disabled"); 
					bgElement.removeClass("enabled");
				} else {
					bgElement.addClass("enabled");
					bgElement.removeClass("disabled");
				}
				var fgElement = element.parent();
				if(element.is(':checked')) {
					fgElement.addClass("checked");
				} else {
					fgElement.removeClass("checked");
				}
			} else {
				element.addClass("checkbox-control");

				var bgElement = $("<span>");
				bgElement.addClass("background");
				bgElement.addClass("checkbox");
				if (element.hasClass("large")) {
				    bgElement.addClass("large");
				}

				if(element.is(':disabled')) {
					bgElement.addClass("disabled");
				} else {
					bgElement.addClass("enabled");
					bgElement.hover(function() {
						$(this).addClass("hover");
					}, function() {
						$(this).removeClass("hover");
					});
				}
				element.wrap(bgElement);

				var fgElement = $("<span>");
				fgElement.addClass("foreground");
				fgElement.addClass("checkbox");
				if (element.hasClass("large")) {
				    fgElement.addClass("large");
				}

				if (element.attr("data-active")) {
					fgElement.mousedown(function() {
						$(this).addClass("active");
					});
					fgElement.mouseup(function() {
						$(this).removeClass("active");
					});
					fgElement.mouseout(function() {
						$(this).removeClass("active");
					});
				}

				if(element.is(':checked')) {
					fgElement.addClass("checked");
				}
				element.wrap(fgElement);

				element.click(function() {
					if($(this).is(':checked')) {
						$(this).parent().addClass("checked");
					} else {
						$(this).parent().removeClass("checked");
					}
				});
			
			}
			
		};
	};
})(jQuery);