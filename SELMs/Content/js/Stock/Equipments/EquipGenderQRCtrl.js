var app = angular.module("myApp", []);

app.controller('EquipGenderQRCtrl', function ($scope, $http, $sce) {
    const qrcodesContainer = document.getElementById('qrcodes');

    // Danh sách mã và giới tính
    const codes = ["Code1", "Code2", "Code3"];
    const genders = ["Nam", "Nữ", "Khác"];

    // Hàm tạo mã QR
    function generateQRCode(data) {
        const qr = new QRCode(document.createElement('div'), {
            text: data,
            width: 128,
            height: 128,
        });

        return qr;
    }

    // Tạo danh sách mã QR
    for (let i = 0; i < codes.length; i++) {
        const qrCode = generateQRCode(`Mã: ${codes[i]}\nGiới tính: ${genders[i]}`);
        qrcodesContainer.appendChild(qrCode._el);
    }

    // Lưu vào tệp DOC
    const doc = new Docxtemplater();
    doc.loadFile("template.docx");

    const data = {
        codes,
        genders,
    };

    doc.setData(data);

    try {
        doc.render();
        const output = doc.getZip().generate({ type: "blob" });
        saveAs(output, "output.docx");
    } catch (error) {
        console.error(error);
    }

});