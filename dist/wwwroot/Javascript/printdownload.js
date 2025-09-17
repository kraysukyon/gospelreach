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