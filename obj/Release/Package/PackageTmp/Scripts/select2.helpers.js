function SetupSelectList(parentSelector, elementSelector, url, width, change, completed) {
	$.getJSON(url, function (data) {
		var selectFilter = $(parentSelector).find(elementSelector).select2({
			width: width,
			data: { results: data, text: 'text' },
			initSelection: function (elem, callback) {
				SetInitialValue(elem, callback, data);
			},
			allowClear: true
		});
		if (change)
			selectFilter.on("change", change);
		if (completed && typeof(completed) === "function")
			completed();
	});
}
function SetupSelectSearchList(parentSelector, elementSelector, checkboxSelector, url, width, change, initialValue) {
	var element = $(parentSelector).find(elementSelector);
	var selectFilter = element.select2({
		width: width,
		minimumInputLength: 2,
		ajax: {
			url: url,
			dataType: 'json',
			data: function (term, page) {
				return { term: term };
			},
			results: function (data, page) {
				return { results: data, text: 'text' };
			}
		},
		initSelection: function (elem, callback) {
			SetInitialValue(elem, callback, initialValue);
		},
		allowClear: true
	});
	if (change)
		selectFilter.on("change", change);

	if (checkboxSelector) {
		$(parentSelector).find(checkboxSelector).click(function () {
			if ($(this).is(':checked'))
				element.select2("disable");
			else
				element.select2("enable");

			if (change)
				change();
		});
	}
}
function SetInitialValue(elem, callback, data) {
	var isfound = false;

	if ($.isArray(data)) {
		$.each(data, function (i, val) {
			if (val.children) {
				if (SetInitialValue(elem, callback, val.children))
					return false; // break jQuery each loop
			}
			else if (val.id == elem.val()) {
				callback(val);
				isfound = true;
				return false; // break jQuery each loop
			}
		});
	}
	else {
		if (data.id && elem.val() == data.id) {
			callback(data);
			isfound = true;
		}
	}

	return isfound;
}