// Configure a few settings and attach camera
Webcam.set({
    width: 280,
    height: 210,
    crop_width: 200,
    crop_height: 210,
    margin: 0,
    image_format: 'jpg',
    jpeg_quality: 90
});

function setup() {
    $("#myCam").show();
    $("#myPhoto").hide();
    Webcam.reset();
    Webcam.attach('#myCam');

    // swap buttons back
    
    $("#btnTakePicture").show();
    $("#btnResetWebcam").show();
    $("#btnAdd").hide();
    $("#btnUploadPicture").hide();
    $("#btnRemovePhoto").hide();
}

function reset_webcam(sourceImage) {
    // swap divs back
    $("#myCam").hide();
    $("#myPhoto").show();
    $("#pre_take_buttons").show();
    $("#post_take_buttons").hide();

    // swap buttons back
    $("#btnTakePicture").hide();
    $("#btnResetWebcam").hide();
    $("#btnAdd").show();
    $("#btnUploadPicture").show();

    if (sourceImage != null && sourceImage.toString().indexOf('default-') < 0)
        $("#btnRemovePhoto").show();

    // turn webcam off
    Webcam.reset();
}

function ValidateImage(extension) {
    if (['bmp', 'gif', 'png', 'jpg', 'jpeg'].indexOf(extension) < 0) {
        toastr.error('Arquivo inválido. Por favor, selecione apenas arquivo com as extensões: .jpeg, .jpg, .png, .gif. e .bmp');
        return false;
    }
    else {
        return true;
    }
}

function preview_snapshot() {
    // freeze camera so user can preview pic
    Webcam.freeze();

    // swap buttons back
    $("#pre_take_buttons").hide();
    $("#post_take_buttons").show();
}

function cancel_preview() {
    // cancel preview freeze and return to live camera feed
    Webcam.unfreeze();

    // swap buttons back
    document.getElementById('pre_take_buttons').style.display = '';
    document.getElementById('post_take_buttons').style.display = 'none';

    // swap buttons back
    $("#pre_take_buttons").show();
    $("#post_take_buttons").hide();
}

function save_photo() {
    // actually snap photo (from preview freeze) and display it
    Webcam.snap(function (data_uri) {
        // display results in page
        document.getElementById('results').innerHTML =
            '<img id="base64image" src="' + data_uri + '"/>';
    });

    reset_webcam();
}

function ReloadPhoto(fileName, element, sourcePath) {
    $('#' + element).attr('src', sourcePath + fileName + '?t=' + new Date().getTime());
    return false;
}