window.printLyrics = function (html) {
    const printWindow = window.open('', '_blank');
    printWindow.document.write(`<pre style="font-family: SFMono-Regular,Menlo,Monaco,Consolas,"Liberation Mono","Courier New",monospace;">${html}</pre>`);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
};

window.downloadPdf = async function (text, title) {
    if (!window.jspdf || !window.jspdf.jsPDF) {
        alert("jsPDF is not loaded!");
        return;
    }

    const jsPDF = window.jspdf.jsPDF;
    const doc = new jsPDF();
    const lines = doc.splitTextToSize(text, 180);
    doc.setFont("courier", "normal");
    doc.setFontSize(12);
    doc.text(lines, 10, 10);

    // Use the title as filename, fallback to "LyricsAndChords"
    const filename = (title && title.trim() !== "") ? `${title}.pdf` : "LyricsAndChords.pdf";
    doc.save(filename);
};