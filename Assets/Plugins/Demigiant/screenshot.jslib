var OpenWindowPlugin = {
    openWindow: function(link)
    {
        var url = Pointer_stringify(link);
        document.onmouseup = function()
        {
            window.open(url);
            document.onmouseup = null;
        }
    },

    saveScreenshot: function(url) {
        var formattedTimeStamp = new Date(Date.now() - 60000 * new Date().getTimezoneOffset()).toISOString().replace(/\..*/g, '').replace(/T/g, ' ').replace(/:/g, '-');

        var button = document.createElement("a");
        button.setAttribute("href", url);
        button.setAttribute("download", "Screenshot " + formattedTimeStamp + ".png");
        button.style.display = "none";
        document.body.appendChild(button);
        button.click();
        document.body.removeChild(button);
    },

    SaveScreenshotWebGL: function(filename, data)
    {
        const pageImage = new Image();
        filename = Pointer_stringify(filename);
       
        if(!filename.endsWith('.png'))
        {
            filename += '.png';
        }
       
        pageImage.src =  'data:image/png;base64,' + Pointer_stringify(data);
       
        pageImage.onload = function()
        {
            const canvas = document.createElement('canvas');
            canvas.width = pageImage.naturalWidth;
            canvas.height = pageImage.naturalHeight;
 
            const ctx = canvas.getContext('2d');
            ctx.imageSmoothingEnabled = false;
            ctx.drawImage(pageImage, 0, 0);
            saveScreenshot(canvas);
        }
 
        function saveScreenshot(canvas)
        {
            const link = document.createElement('a');
            link.download = filename;
           
            canvas.toBlob(function(blob)
            {
                link.href = URL.createObjectURL(blob);
                link.click();
            });
        };
    }
};
 
mergeInto(LibraryManager.library, OpenWindowPlugin);