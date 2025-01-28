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

        filesArray.push(file); 
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