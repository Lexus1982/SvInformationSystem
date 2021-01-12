function OnTownsSelectionChange(townUrl, addressUrl) {
    var id = $('#TownId').val();
    $.ajax({
        type: 'GET',
        url: townUrl + '/' + id,
        success: function (data) {
            $('#StreetId').replaceWith(data);
            OnStreetsSelectionChange(addressUrl);
        }
    });
}

function OnStreetsSelectionChange(addressUrl) {
    var id = $('#StreetId').val();
    $.ajax({
        type: 'GET',
        url: addressUrl + '/' + id,
        success: function (data) {
            $('#AddressId').replaceWith(data);
        }
    });
}