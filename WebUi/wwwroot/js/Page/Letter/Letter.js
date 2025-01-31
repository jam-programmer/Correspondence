CKEDITOR.replace("Body", {
    language: 'fa',
    toolbar: [
        { name: 'justify', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },

        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll'] },
        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', '-', 'RemoveFormat'] }
    ],
});
$('.select2').select2();

let filesArray = [];
const maxFileSize = 20 * 1024 * 1024;
const allowedExtensions = ['.xlsx', '.xls', '.txt', '.pdf', '.docx', '.png', '.jpg', '.jpeg'];

function addFiles() {
    const input = document.getElementById('fileInput');
    const errorMessage = document.getElementById('errorMessage');
    errorMessage.textContent = '';

    for (let i = 0; i < input.files.length; i++) {
        const file = input.files[i];

        if (file.size > maxFileSize) {
            errorMessage.textContent += `فایل "${file.name}" بیش از حد مجاز است. حداکثر حجم 20 مگابایت است.\n`;
            continue;
        }

        const fileExtension = file.name.slice(((file.name.lastIndexOf(".") - 1) >>> 0) + 2).toLowerCase();
        if (!allowedExtensions.includes('.' + fileExtension)) {
            errorMessage.textContent += `فرمت فایل "${file.name}" مجاز نیست. فرمت‌های مجاز: ${allowedExtensions.join(', ')}.\n`;
            continue;
        }

        // بررسی اینکه آیا فایل قبلاً اضافه شده است یا خیر
        if (!filesArray.some(existingFile => existingFile.name === file.name)) {
            filesArray.push(file);
        } else {
            errorMessage.textContent += `فایل "${file.name}" قبلاً اضافه شده است.\n`;
        }
    }

    displayFiles();
}

function displayFiles() {
    const tableBody = document.getElementById('fileTable').getElementsByTagName('tbody')[0];

    tableBody.innerHTML = '';

    for (const file of filesArray) {
        const row = tableBody.insertRow();

        const cellName = row.insertCell(0);
        const cellSize = row.insertCell(1);
        const cellAction = row.insertCell(2);

        cellName.textContent = file.name;
        cellSize.textContent = file.size;

        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'حذف';
        deleteButton.className = 'btn btn-sm btn-danger text-white';

        deleteButton.onclick = function () {
            removeFile(file);
            row.remove();
        };

        cellAction.appendChild(deleteButton);
    }
}

function removeFile(fileToRemove) {
    filesArray = filesArray.filter(file => file !== fileToRemove);
}

function removeFile(fileToRemove) {
    filesArray = filesArray.filter(file => file !== fileToRemove); 
}

function submitForm(type) {

    let title = document.getElementById("Title");
    let body =  CKEDITOR.instances.Body.getData();
    let number = document.getElementById("Number");
    let Priority = document.getElementById("Priority");
    let referrerCopy = $('#ReferrerCopy'); 
    let referrerOriginal = $('#ReferrerOriginal');
    if (title.value === "" || title.value === null) {
        Swal.fire({
            title: "عدم تکمیل نامه",
            text: "لطفا عنوان نامه را وارد کنید.",
            icon: "error",
            confirmButtonText:"متوجه شدم"
        });
        return;
    }
    if (number.value === "" || number.value === null) {
        Swal.fire({
            title: "عدم تکمیل نامه",
            text: "لطفا کلاسه پرونده نامه را وارد کنید.",
            icon: "error",
            confirmButtonText: "متوجه شدم"
        }); return;
    }
    if (body.value === "" || body.value === null) {
        Swal.fire({
            title: "عدم تکمیل نامه",
            text: "لطفا متن نامه را وارد کنید.",
            icon: "error",
            confirmButtonText: "متوجه شدم"
        }); return;
    }
    if (!referrerOriginal.val() || referrerOriginal.val().length === 0) {
        Swal.fire({
            title: "عدم تکمیل نامه",
            text: "لطفا یک یا چند گیرنده اصلی را انتخاب کنید.",
            icon: "error",
            confirmButtonText: "متوجه شدم"
        });
        return;
    } 

    let original = [];
    let copy = [];
    let formData = new FormData();
   
    formData.append("type", type);
    formData.append("number", number.value);
    formData.append("title", title.value);
    formData.append("text", body);
    formData.append("priority", Priority.value);

    let Original = [];
    let Copy = [];
   

    if (referrerCopy.val() || referrerCopy.val().length > 0) {
        referrerCopy.val().forEach(function (value) {
            var text = $('.ReferrerCopy option[value="' + value + '"]').text();
            Copy.push({ userName: value, fullName: text });
        });
    } 
    referrerOriginal.val().forEach(function (value) {
        var text = $('.ReferrerOriginal option[value="' + value + '"]').text();
        Original.push({ userName: value, fullName: text });
    });

    formData.append("referrerOriginal", JSON.stringify(Original));

    formData.append("referrerCopy", JSON.stringify(Copy));
    

    for (const file of filesArray) {
        
        formData.append('files', file);
    }
   

    fetch("?handler=RegisterLetter", {
        method: "POST",
        body: formData,
    })
        .then(response => response.json())
        .then(data => {
            if (data.isSuccess) {
                debugger
                console.log(type);
                if (type === "1") {
                    Swal.fire({
                        title: "ثبت موفق",
                        text: "نامه پیش نویس شد",
                        icon: "success",
                        confirmButtonText: "متوجه شدم"
                    }).then((result) => {
                        location.replace("/Index");
                    });
                } else {
                    Swal.fire({
                        title: "ثبت موفق",
                        text: "نامه ارسال شد",
                        icon: "success",
                        confirmButtonText: "متوجه شدم"
                    }).then((result) => {
                        location.replace("/Index");
                    });
                }
               
            } else {
                Swal.fire({
                    title: "ثبت ناموفق",
                    text: data.message,
                    icon: "warning",
                    confirmButtonText: "متوجه شدم"
                });
            }
        });
}