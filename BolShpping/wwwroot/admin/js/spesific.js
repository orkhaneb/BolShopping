//for image file function
function previewFile() {
    const preview = document.getElementById('img1');
    const file = document.querySelector('#image1').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

// select option
$(document).ready(function () {
    $('#ingredients').multiselect();
});
// select option end

////for video file function
//function previewVideo() {
//    const preview = document.getElementById('video');
//    const file = document.querySelector('#video1 input[type=file]').files[0];
//    const reader = new FileReader();
//    reader.addEventListener("load", function () {

//        preview.src = reader.result;
//    }, false);

//    if (file) {
//        reader.readAsDataURL(file);
//    }
//}


// Json function that brings departments to the ClinicDoctor controller according to the selected clinic ID. 
$(document).ready(function () {
    $(document).on("change", "#Clinic_SpecialClinical_Departments", function (e) {
        $("#Department_Id").empty();
        var optionVal = $(this).val();
        console.log(optionVal);
        if (optionVal != 0) {
            $.ajax({
                url: "../ClinicDoctor/DemartmentForClinc",
                type: "Post",
                data: { id: optionVal },
                success: function (res) {
                    if (res.status == 200) {
                        $("#Department_Id").append('<option>Select</option>');
                        for (var db of res.data) {
                            $("#Department_Id").append(`<option value="${db.value}">${db.text}</option>`);
                        }
                    }
                    else if (res.status == 404) {
                        console.log("Not Found :(");
                    }
                }
            })
        }
    });
});


//At admin panel, in the Product part, for images to add is function
var selDiv = "";

document.addEventListener("DOMContentLoaded", init, false);

function init() {
    document.querySelector('#filess').addEventListener('change', handleFileSelect, false);
    selDiv = document.querySelector("#selectedFiles");

}
function handleFileSelect(e) {

    if (!e.target.files || !window.FileReader) return;

    selDiv.innerHTML = "";

    var files = e.target.files;
    var filesArr = Array.prototype.slice.call(files);
    let i = 0;
    filesArr.forEach(function (f) {
        var f = files[i++];
        if (!f.type.match("image.*")) {
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            var html = "<img id=xxx style='width: 200px;' src=\"" + e.target.result + "\">";
            selDiv.innerHTML += html;
        }
        reader.readAsDataURL(f);
            //document.getElementById("xxx").style.width = "200px";
    });


}