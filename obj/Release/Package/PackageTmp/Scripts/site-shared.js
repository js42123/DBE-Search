function FormatCurrency(n) {
    if (typeof (n) === "number") {
        var c = 2,
                    d = '.',
                    t = ',',
                    s = n < 0 ? "-" : "",
                    i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
                    j = (j = i.length) > 3 ? j % 3 : 0;
        return s + '$' + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    }

    return n;
}
function FormatLatLong(lat, long) {
    return (lat ? lat : '') + ' ' + (long ? long : '');
}

Window.GetJobName = function (contractCode, callback) {
    //$.get('//ConsultantContracts/AllotmentsLookup/GetJobName?JobNo={0}'.format(contractCode), null,
    var aluUrl = $("#allotmentLookupUrl").val();
    $.get(aluUrl, { JobNo: contractCode }, function (data) { callback(data); })
};

function intToRoman(int) {

    // create 2-dimensional array, each inner array containing 
    // roman numeral representations of 1-9 in each respective 
    // place (ones, tens, hundreds, etc...currently this handles
    // integers from 1-3999, but could be easily extended)
    var romanNumerals = [
        ['', 'i', 'ii', 'iii', 'iv', 'v', 'vi', 'vii', 'viii', 'ix'], // ones
        ['', 'x', 'xx', 'xxx', 'xl', 'l', 'lx', 'lxx', 'lxxx', 'xc'], // tens
        ['', 'c', 'cc', 'ccc', 'cd', 'd', 'dc', 'dcc', 'dccc', 'cm'], // hundreds
        ['', 'm', 'mm', 'mmm'] // thousands
    ];

    // split integer string into array and reverse array
    var intArr = int.toString().split('').reverse(),
        len = intArr.length,
        romanNumeral = '',
        i = len;

    // starting with the highest place (for 3046, it would be the thousands 
    // place, or 3), get the roman numeral representation for that place 
    // and append it to the final roman numeral string
    while (i--) {
        romanNumeral += romanNumerals[i][intArr[i]];
    }

    return romanNumeral.toUpperCase();

}

// Production steps of ECMA-262, Edition 5, 15.4.4.19
// Reference: http://es5.github.io/#x15.4.4.19
if (!Array.prototype.map) {

    Array.prototype.map = function (callback, thisArg) {

        var T, A, k;

        if (this === null) {
            throw new TypeError(' this is null or not defined');
        }

        // 1. Let O be the result of calling ToObject passing the |this| 
        //    value as the argument.
        var O = Object(this);

        // 2. Let lenValue be the result of calling the Get internal 
        //    method of O with the argument "length".
        // 3. Let len be ToUint32(lenValue).
        var len = O.length >>> 0;

        // 4. If IsCallable(callback) is false, throw a TypeError exception.
        // See: http://es5.github.com/#x9.11
        if (typeof callback !== 'function') {
            throw new TypeError(callback + ' is not a function');
        }

        // 5. If thisArg was supplied, let T be thisArg; else let T be undefined.
        if (arguments.length > 1) {
            T = thisArg;
        }

        // 6. Let A be a new array created as if by the expression new Array(len) 
        //    where Array is the standard built-in constructor with that name and 
        //    len is the value of len.
        A = new Array(len);

        // 7. Let k be 0
        k = 0;

        // 8. Repeat, while k < len
        while (k < len) {

            var kValue, mappedValue;

            // a. Let Pk be ToString(k).
            //   This is implicit for LHS operands of the in operator
            // b. Let kPresent be the result of calling the HasProperty internal 
            //    method of O with argument Pk.
            //   This step can be combined with c
            // c. If kPresent is true, then
            if (k in O) {

                // i. Let kValue be the result of calling the Get internal 
                //    method of O with argument Pk.
                kValue = O[k];

                // ii. Let mappedValue be the result of calling the Call internal 
                //     method of callback with T as the this value and argument 
                //     list containing kValue, k, and O.
                mappedValue = callback.call(T, kValue, k, O);

                // iii. Call the DefineOwnProperty internal method of A with arguments
                // Pk, Property Descriptor
                // { Value: mappedValue,
                //   Writable: true,
                //   Enumerable: true,
                //   Configurable: true },
                // and false.

                // In browsers that support Object.defineProperty, use the following:
                // Object.defineProperty(A, k, {
                //   value: mappedValue,
                //   writable: true,
                //   enumerable: true,
                //   configurable: true
                // });

                // For best browser support, use the following:
                A[k] = mappedValue;
            }
            // d. Increase k by 1.
            k++;
        }

        // 9. return A
        return A;
    };
}

// Production steps of ECMA-262, Edition 5, 15.4.4.18
// Reference: http://es5.github.io/#x15.4.4.18
if (!Array.prototype.forEach) {

    Array.prototype.forEach = function (callback, thisArg) {

        var T, k;

        if (this === null) {
            throw new TypeError(' this is null or not defined');
        }

        // 1. Let O be the result of calling ToObject passing the |this| value as the argument.
        var O = Object(this);

        // 2. Let lenValue be the result of calling the Get internal method of O with the argument "length".
        // 3. Let len be ToUint32(lenValue).
        var len = O.length >>> 0;

        // 4. If IsCallable(callback) is false, throw a TypeError exception.
        // See: http://es5.github.com/#x9.11
        if (typeof callback !== "function") {
            throw new TypeError(callback + ' is not a function');
        }

        // 5. If thisArg was supplied, let T be thisArg; else let T be undefined.
        if (arguments.length > 1) {
            T = thisArg;
        }

        // 6. Let k be 0
        k = 0;

        // 7. Repeat, while k < len
        while (k < len) {

            var kValue;

            // a. Let Pk be ToString(k).
            //   This is implicit for LHS operands of the in operator
            // b. Let kPresent be the result of calling the HasProperty internal method of O with argument Pk.
            //   This step can be combined with c
            // c. If kPresent is true, then
            if (k in O) {

                // i. Let kValue be the result of calling the Get internal method of O with argument Pk.
                kValue = O[k];

                // ii. Call the Call internal method of callback with T as the this value and
                // argument list containing kValue, k, and O.
                callback.call(T, kValue, k, O);
            }
            // d. Increase k by 1.
            k++;
        }
        // 8. return undefined
    };
}

if (!Array.prototype.find) {
    Array.prototype.find = function (predicate) {
        if (this === null) {
            throw new TypeError('Array.prototype.find called on null or undefined');
        }
        if (typeof predicate !== 'function') {
            throw new TypeError('predicate must be a function');
        }
        var list = Object(this);
        var length = list.length >>> 0;
        var thisArg = arguments[1];
        var value;

        for (var i = 0; i < length; i++) {
            value = list[i];
            if (predicate.call(thisArg, value, i, list)) {
                return value;
            }
        }
        return undefined;
    };
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

// First, checks if it isn't implemented yet.
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/\{(\d+)\}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

function processpaste(elem, savedcontent, handleparse) {
    var pasteddata = elem.innerHTML;
    //^^Alternatively loop through dom (elem.childNodes or elem.getElementsByTagName) here
    elem.innerHTML = savedcontent;
    var parser = new DOMParser();
    doc = parser.parseFromString(pasteddata, "text/html");
    var rows = doc.getElementsByTagName("tr");
    var rates = [];
    var i;
    for (i = 0; i < rows.length; i++) {
        var cells = null;

        if (rows[i].getElementsByTagName('font').length > 0) {
            cells = rows[i].getElementsByTagName('font');
        } else {
            cells = rows[i].getElementsByTagName('td');
        }
        rates.push({ name: cells[0] !== undefined ? cells[0].innerHTML : "", min: cells[1] !== undefined ? cells[1].innerHTML : "", max: cells[2] !== undefined ? cells[2].innerHTML : "" });
    }
    // Do whatever with gathered data;
    handleparse(rates);
}


Window.test = {};
function handlepaste(elem, e, processpaste) {
    var savedcontent = elem.innerHTML;
    if (e && e.clipboardData && e.clipboardData.getData) {// Webkit - get data from clipboard, put into editdiv, cleanup, then cancel event
        if (/text\/html/.test(e.clipboardData.types)) {
            elem.innerHTML = e.clipboardData.getData('text/html');
        }
        else if (/text\/plain/.test(e.clipboardData.types)) {
            elem.innerHTML = e.clipboardData.getData('text/plain');
        }
        else {
            elem.innerHTML = "";
        }
        waitforpastedata(elem, savedcontent, processpaste);
        if (e.preventDefault) {
            e.stopPropagation();
            e.preventDefault();
        }
        return false;
    }
    else {// Everything else - empty editdiv and allow browser to paste content into it, then cleanup
        elem.innerHTML = "";
        waitforpastedata(elem, savedcontent, processpaste);
        return true;
    }
}

function waitforpastedata(elem, savedcontent, processpaste) {
    if (elem.childNodes && elem.childNodes.length > 0 && (elem.firstChild.nodeValue || elem.firstChild.innerHTML)) {
        processpaste(elem, savedcontent, processpaste);
    }
    else {
        that = {
            e: elem,
            s: savedcontent
        };
        that.callself = function () {
            waitforpastedata(that.e, that.s, processpaste);
        };
        setTimeout(that.callself, 20);
    }
}

determineNegativeClass = function (elem) {
    var elVal = $(elem).is('input') ? elem.value : elem.innerText;
    var isNegative = elVal.length > 0 ? (elVal.indexOf('-') === 0 ? true : false) : false;
    if (elem.classList) {
        var hasNegative = $(elem).hasClass('negative');
        if (isNegative) {
            if (!hasNegative)
                $(elem).addClass('negative');
        } else {
            if (hasNegative) {
                $(elem).removeClass('negative');
            }
        }
    } else { //IE 9
        var classes = elem.classes.split(" ");
        var i = classes.indexOf('negative');
        if (isNegative) {
            if (i < 0)
                classes.push('negative');
        } else {
            if (i >= 0)
                classes.splice(i, 1);
        }
        elem.classes = classes.join(' ');
    }
};

makeNegativeRed = function () {
    TDs = $('.money');
    for (var i = 0; i < TDs.length; i++) {
        var temp = TDs[i];
        determineNegativeClass(temp);
    }
};

initializeNegativeHandlers = function () {
    $('.money,.allotmentAmt').change(function (e) {
        var temp = e.target;
        determineNegativeClass(temp);
    });
};

$(function () {
    initializeNegativeHandlers();
    makeNegativeRed();
});
