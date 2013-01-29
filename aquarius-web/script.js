function power(val) {
    $.ajax({
     url: 'http://localhost:8080/aquarius-web/rest/power',
     type: 'POST',
     data: 'value=' + val, // or $('#myform').serializeArray()
     success: function() { alert('POST completed'); }
    });
}
