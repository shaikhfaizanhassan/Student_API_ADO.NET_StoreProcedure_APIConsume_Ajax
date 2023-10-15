$(document).ready(function () {
    getStudents();
});


// Get All Students Using AJAX
function getStudents() {
    $.ajax({
        // API endpoint URL
        url: "https://localhost:7289/api/Students/GetAllStudent",
        type: "GET",
        success: OSgetStudents,
        error: function (error) {
            // Handle any errors here
            console.error(error);
        }
    });
}
// Function to populate the table with student data
function OSgetStudents(data, status) {


    $.each(data, function (i, option) {
        $('#studentTable tbody').append(

            "<tr>" +
            "<td>" + option.stID + "</td>" +
            "<td>" + option.stName + "</td>" +
            "<td>" + option.stEmail + "</td>" +
            "<td>" + option.stPassword + "</td>" +
            "<td>" + option.stContact + "</td>" +
            "<td> "+
            "<button class='btn btn-primary edit-button' data-id='" + option.stID + "'>Edit</button> " +
            "<button class='btn btn-danger delete-button' data-id='" + option.stID + "'>Delete</button> " +
            "<button class='btn btn-success detail-button' data-id='" + option.stID + "'>Detail</button> " +


            "</td > " +

            "</tr>"
        );
    });
}

$(document).on('click', '.delete-button', function () {
    event.preventDefault(); // Page refresh ko prevent karega
    var studentId = $(this).data('id');

    // Confirm with the user before deleting
    if (confirm("Are you sure you want to delete this record?")) {
        // If the user confirms, send a delete request to the API
        $.ajax({
            url: "https://localhost:7289/api/Students/DeleteStudent/" + studentId, // Replace with your API endpoint
            type: "DELETE",
            success: function () {
                // If the record is successfully deleted, you can update the UI or reload the data
                alert("Record deleted successfully.");
                location.reload(); // Page reload karega
                // Reload data or update the UI as needed
            },
            error: function (error) {
                console.error(error);
                alert("Error deleting record.");
            }
        });
    }
});



$(document).on('click', '.detail-button', function () {
    var studentId = $(this).data("id");

    $.ajax({
        url: "https://localhost:7289/api/Students/GetStudentById/" + studentId,
        type: "GET",
        success: function (data) {
            // Populate modal with student data
            $("#studentId").text(data.stID);
            $("#studentName").text(data.stName);
            $("#studentEmail").text(data.stEmail);
            $("#StudentPassword").text(data.stPassword);
            $("#StudentContact").text(data.stContact);
            // Add more lines to populate other details
            // Show the modal
            $("#studentModal").show();
        },
        error: function (xhr, status, error) {
            console.error(xhr);
            alert("Error fetching student details: " + xhr.responseText);
        }
    });
});

$(".close").click(function () {
    $("#studentModal").hide();
});




$(document).ready(function () {
    // Display the modal when the "Create Student" button is clicked
    $("#showCreateModalButton").click(function () {
        $("#createStudentModal").show();
    });

    // Close the modal when the close button is clicked
    $("#closeCreateModal").click(function () {
        $("#createStudentModal").hide();
    });

    // Capture form submission inside the modal
    $("#createStudentForm").submit(function (event) {
        event.preventDefault();
        var formData = {
            stName: $("#stName").val(),
            stEmail: $("#stEmail").val(),
            stPassword: $("#stPassword").val(),
            stContact: $("#stContact").val(),

        };

        // Send data to the server for creating a new student
        $.ajax({
            url: "https://localhost:7289/api/Students/CreateStudent",
            type: "POST",
            contentType: "application/json", // Set the content type to JSON
            data: JSON.stringify(formData), // Convert the data object to a JSON string
            success: function (response) {
                console.log(formData);

                alert("Data saved successfully.");
                $("#createStudentModal").hide();
                location.reload();
            },
            error: function (error) {
                console.error(error);
                alert("Error creating student.");
            }
        });
    });
});


$(document).on('click', '.edit-button', function () {
    var studentID = $(this).data("id");

    // Set the student ID in a data attribute of the form
    var formElement = document.getElementById("editStudentForm");
    formElement.setAttribute("data-id", studentID);

    // Fetch and display student data
    $.ajax({
        url: "https://localhost:7289/api/Students/GetStudentById/" + studentID,
        type: "GET",
        success: function (data) {
            $("#studentId1").val(data.stID);
            $("#studentName1").val(data.stName);
            $("#studentEmail1").val(data.stEmail);
            $("#StudentPassword1").val(data.stPassword);
            $("#StudentContact1").val(data.stContact);

            console.log(data);


            $("#editStudentModal").show();
        },
        error: function (error) {
            console.error(error);
            alert("Error fetching student details for editing.");
        }
    });
});

$(".close").click(function () {
    $("#editStudentModal").hide();
});



// JavaScript code to handle the form submission
$(document).ready(function () {
    // Event handler for the modal's close button
    $("#closeEditModal").click(function () {
        $("#editStudentModal").hide();
    });

    // Event handler for the form submission
    $("#editStudentForm").submit(function (event) {
        event.preventDefault();
        var studentID = $(this).data("id"); // Fetch the student ID from the form data attribute

        var formData = {
            stName: $("#studentName1").val(),
            stEmail: $("#studentEmail1").val(),
            stPassword: $("#StudentPassword1").val(),
            stContact: $("#StudentContact1").val(),
        };

        $.ajax({
            url: "https://localhost:7289/api/Students/EditStudent/" + studentID,
            type: "PUT",
            data: JSON.stringify(formData),
            contentType: "application/json", // Set the content type to JSON
            success: function (response) {
                location.reload();
                alert("Data updated successfully. " + response);
                $("#editStudentModal").hide();
               
            },
            error: function (error) {
                console.error(error);
                alert("Error updating data.");
            }
        });
    });
});
