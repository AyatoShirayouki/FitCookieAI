import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import html2pdf from 'html2pdf.js';
import 'jspdf-autotable';


function usePdfExport(elementId, options) {

    const getHighestTable = (table1, table2, table3) => {
        return typeof table3 === 'number' ? Math.max(table1, table2, table3) : Math.max(table1, table2);
    };

    const exportToPDF = async () => {
        const element = document.getElementById(elementId);

        try {
            await new Promise(resolve => setTimeout(resolve, 100));
            const children1Height = parseFloat(getComputedStyle(document.getElementById(elementId).children[0]).height)
            const children2Height = parseFloat(getComputedStyle(document.getElementById(elementId).children[2]).height)
            const children3Height = document.getElementById(elementId).children.length === 5 ? parseFloat(getComputedStyle(document.getElementById(elementId).children[4]).height) : null;
            const heighestChildren = typeof children3Height === 'number' ? getHighestTable(children1Height, children2Height, children3Height) : getHighestTable(children1Height, children2Height);
            const contentHeight = parseFloat(getComputedStyle(element).height);

            //   const fixedMargin = 100;
            const pdfMargins = 25.4 * 2;
            const maxPageHeight = Math.ceil(heighestChildren * 0.2645833333) + pdfMargins;

            //   const dynamicPageHeight = Math.min(contentHeight + fixedMargin, maxPageHeight);
            const dynamicPageHeight = maxPageHeight <= 392.8 ? Math.min(contentHeight, maxPageHeight) : 392.8;
            const pdf = await html2pdf(element, {
                margin: 10,
                filename: options.filename,
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: options.html2canvas.scale || 2 },
                jsPDF: { unit: 'mm', format: [210, dynamicPageHeight], orientation: 'portrait' },
            });

            pdf.save();
        } catch (error) {
            console.error('Error generating PDF:', error);
        }
    };


    return { exportToPDF };
};

export default usePdfExport;