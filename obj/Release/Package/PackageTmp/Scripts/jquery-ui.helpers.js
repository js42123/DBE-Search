function addAutocompleters() 
{
    $(':input[data-autocomplete]').each(function () 
    {
        $(this).autocomplete({
            source: $(this).attr('data-autocomplete'),
            minLength: 3
        });
    });
}
function addDatepickers() 
{
    $('input[type="datetime"]').each(function () 
    {
        $(this).datepicker();
    });
}

