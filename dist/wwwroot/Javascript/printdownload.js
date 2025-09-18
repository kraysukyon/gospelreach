window.printLyrics = function (html) {
    const printWindow = window.open('', '_blank');
    printWindow.document.write(`<pre style="font-family: SFMono-Regular,Menlo,Monaco,Consolas,"Liberation Mono","Courier New",monospace;">${html}</pre>`);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
};

//window.downloadPdf = async function (text, title) {
//    if (!window.jspdf || !window.jspdf.jsPDF) {
//        alert("jsPDF is not loaded!");
//        return;
//    }

//    const jsPDF = window.jspdf.jsPDF;
//    const doc = new jsPDF();
//    const lines = doc.splitTextToSize(text, 180);
//    doc.setFont("courier", "normal");
//    doc.setFontSize(12);
//    doc.text(lines, 10, 10);

//    // Use the title as filename, fallback to "LyricsAndChords"
//    const filename = (title && title.trim() !== "") ? `${title}.pdf` : "LyricsAndChords.pdf";
//    doc.save(filename);
//};

window.downloadPdf = async function (text, title) {
    if (!window.jspdf || !window.jspdf.jsPDF) {
        alert("jsPDF is not loaded!");
        return;
    }

    const jsPDF = window.jspdf.jsPDF;
    const doc = new jsPDF();

    // Set font and size
    doc.setFont("courier", "normal");
    doc.setFontSize(10);

    // Split the text into lines that fit the page width
    const lines = doc.splitTextToSize(text, 180);

    // Initial Y position
    let yPosition = 10;

    // Line height factor (1.2 = comfortable spacing)
    const lineHeight = 6 * 1.0; // fontSize × spacing

    // Add lines to the PDF
    lines.forEach(line => {
        // Check if the next line will go past the page height
        if (yPosition + lineHeight > doc.internal.pageSize.height) {
            doc.addPage();
            yPosition = 10; // Reset for new page
        }

        doc.text(line, 10, yPosition);
        yPosition += lineHeight;
    });

    // Use the title as filename, fallback to "LyricsAndChords.pdf"
    const filename = (title && title.trim() !== "")
        ? `${title}.pdf`
        : "LyricsAndChords.pdf";
    doc.save(filename);
};


window.scrollToSection = (id) => {
    const el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: 'smooth' });
    }
};


window.printReport = function (divId) {
    var divContents = document.getElementById(divId).innerHTML;

    // open a blank popup window
    var printWindow = window.open('', '', 'height=800,width=1200');

    // add Bootstrap + custom CSS so tables look the same
    printWindow.document.write(`
<html>
<head>
    <title>Print</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        /* Apply color adjustment everywhere */
        * {
            -webkit-print-color-adjust: exact !important;
            print-color-adjust: exact !important;
            color-adjust: exact !important;
        }

        @media print {
            /* Also reassert inside @media print */
            * {
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
                color-adjust: exact !important;
            }

            /* Improve table look on paper */
            table {
                border-collapse: collapse !important;
            }
            table td, table th {
                border: 1px solid #000 !important;
            }
        }
    </style>
</head>
<body>
    ${divContents}
</body>
</html>
`);

    printWindow.document.close();

    // wait for styles/images/canvases to load, then print
    printWindow.onload = function () {
        // Convert all canvases to images in the new window
        printWindow.document.querySelectorAll('canvas').forEach(function (canvas) {
            var img = document.createElement('img');
            img.src = canvas.toDataURL(); // capture the canvas
            img.width = canvas.width;
            img.height = canvas.height;
            canvas.replaceWith(img);
        });
        // If using charts, you may want to convert canvases to images here
        printWindow.print();
    };
};

window.saveDivAsPdf = async function (divId, fileName) {
    const element = document.getElementById(divId);
    if (!element) {
        alert("Element not found: " + divId);
        return;
    }

    // Default filename if none supplied
    fileName = fileName || 'FinancialReport.pdf';

    // Convert <canvas> charts to <img> so they render in PDF
    element.querySelectorAll('canvas').forEach(function (canvas) {
        const img = document.createElement('img');
        img.src = canvas.toDataURL();
        img.width = canvas.width;
        img.height = canvas.height;
        canvas.replaceWith(img);
    });

    // Snapshot the element as canvas
    const canvas = await html2canvas(element, {
        scale: 2,
        useCORS: true
    });

    const imgData = canvas.toDataURL('image/png');

    const { jsPDF } = window.jspdf;
    const pdf = new jsPDF('p', 'pt', 'a4'); // portrait, points, A4

    // PDF size in points
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = pdf.internal.pageSize.getHeight();

    // Margins (points)
    const marginLeft = 30;
    const marginTop = 30;
    const usableWidth = pdfWidth - marginLeft * 2;
    const usableHeight = pdfHeight - marginTop * 2;

    // Image properties
    const imgProps = pdf.getImageProperties(imgData);
    const imgHeight = (imgProps.height * usableWidth) / imgProps.width;

    if (imgHeight <= usableHeight) {
        pdf.addImage(imgData, 'PNG', marginLeft, marginTop, usableWidth, imgHeight);
    } else {
        let remainingHeight = imgHeight;
        const canvasHeightPx = canvas.height;
        const canvasWidthPx = canvas.width;
        const pxPerPt = canvasWidthPx / usableWidth;
        const pageHeightPx = usableHeight * pxPerPt;

        let page = 0;
        while (remainingHeight > 0) {
            const pageCanvas = document.createElement('canvas');
            pageCanvas.width = canvasWidthPx;
            pageCanvas.height = Math.min(pageHeightPx, canvasHeightPx - pageHeightPx * page);

            const ctx = pageCanvas.getContext('2d');
            ctx.drawImage(
                canvas,
                0,
                pageHeightPx * page,
                canvasWidthPx,
                pageCanvas.height,
                0,
                0,
                canvasWidthPx,
                pageCanvas.height
            );

            const pageImgData = pageCanvas.toDataURL('image/png');
            const pageImgHeight = (pageCanvas.height * usableWidth) / canvasWidthPx;

            if (page > 0) pdf.addPage();
            pdf.addImage(pageImgData, 'PNG', marginLeft, marginTop, usableWidth, pageImgHeight);

            remainingHeight -= usableHeight;
            page++;
        }
    }

    pdf.save(fileName.endsWith('.pdf') ? fileName : `${fileName}.pdf`);
};
