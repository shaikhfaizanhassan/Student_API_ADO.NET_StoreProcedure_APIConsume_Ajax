$(document).ready(function () {
    GetBranchZone();
});
function GetBranchZone() {
    $.ajax({
        url: "https://localhost:7041/api/Forms/GetBranchZone",
        type: "GET",
        contentType: "application/json",      
        success: function (data) {
            console.log("API Response:", data); // Log the API response
            
            GetAllBranchZone(data);
        },
        error: function (error) {
            console.error("API Error:", error);
        }   
    });
}


function GetAllBranchZone(data) {
    // Assuming data is an array of objects
    $.each(data, function (i, option) {
        if (option.isDeleted == 0) {
            $("#tabledata tbody").append(
                '<tr>' +
                '<td>' + option.id + '</td>' +
                '<td>' + option.zone_name + '</td>' +
                '<td> ' +
                '<button class= "btn btn-primary" onclick = "EditBranchZone(' + option.id + ')" > Edit</button > ' +
                '<button class= "btn btn-danger" onclick = "DeleteBranchZone(' + option.id + ')" > Delete</button > ' +

                '</td > ' +

                '</tr>'
            );
        }
    });
}
function SaveOrUpdateBranchZone() {
 
    var ZoneName = $("#zone_name").val();
    var model = JSON.stringify({
        zone_name: ZoneName
    });
 
 

    $.ajax({
        url: "https://localhost:7041/api/Forms/SaveBranchZone",
        type: "POST",
        contentType: "application/json",   
        data: model,
        success: function (data) {
            console.log("API Response:", data); // Log the API response
            alert("Data Save");
            location.reload();
        },
        error: function (error) {
            console.error("API Error:", error);
        }
    });
}


function EditBranchZone(id) {
    var url = "https://localhost:7041/api/Forms/GetBranchZoneID?id=" + id;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            if (data.length > 0) {
                $("#id").val(data[0].id);
                $("#zone_name").val(data[0].zone_name);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}


function UpdateBranchZones() {
    var id = $("#id").val();
    var zone = $("#zone_name").val();

    var model = {
        id: id, // Ensure id is parsed as an integer
        zone_name: zone,
    };

    $.ajax({
        url: "https://localhost:7041/api/Forms/UpdateBranchZone",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(model),
        success: function (result) {
            console.log("API Response:", result);

            if (result) {
                alert("Data Updated");
                location.reload(); // Reload the page or update the UI as needed
            } else {
                alert("Data Update Failed");
            }
        },
        error: function (error) {
            console.error("API Error:", error);
            alert("Data Update Failed");
        },
    });
}


function DeleteBranchZone(id) {
    // No need to redeclare 'id' as a variable

    var ZoneID = id;
    var deleteData = JSON.stringify({
        id: ZoneID,
    });

    $.ajax({
        // Use the correct API endpoint
        url: "https://localhost:7041/api/Forms/DeleteCities",
        type: "POST",  // Check if it should be DELETE
        contentType: "application/json",
        data: deleteData,
        success: function (result) {
            alert("Data Deleted");
            location.reload();
        },
        error: function (error) {
            console.log("API Error:", error);
        },
    });
}
