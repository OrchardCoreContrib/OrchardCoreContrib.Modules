function initGrapesJs(htmlId) {
    const editor = grapesjs.init({
        container: '.grapes-editor',
        storageManager: {
            id: 'gjs-',             // Prefix identifier that will be used inside storing and loading
            type: 'local',          // Type of the storage
            autosave: true,         // Store data automatically
            autoload: true,         // Autoload stored data on init
            stepsBeforeSave: 1,     // If autosave enabled, indicates how many changes are necessary before store method is triggered
            storeComponents: true,  // Enable/Disable storing of components in JSON format
            storeStyles: true,      // Enable/Disable storing of rules in JSON format
            storeHtml: true,        // Enable/Disable storing of components as HTML string
            storeCss: true,         // Enable/Disable storing of rules as CSS string
        },
        plugins: ['gjs-preset-webpage'],
        pluginsOpts: {
            'gjs-preset-webpage': {
                // options
            }
        },
    });

    editor.on('update', () => {
        const html = editor.getHtml();
        const css = editor.getCss();
        parent.document.getElementById(htmlId).value = '<style>' + css + '</style>' + '<body>' + html + '</body>';
    });

    editor.on('load', () => {
        const container = document.createElement('html');
        container.innerHTML = parent.document.getElementById(htmlId).value;
        const stylePart = container.querySelector('style');
        const bodyPart = container.querySelector('body');
        editor.editor.setComponents(bodyPart.outerHTML);
        editor.setStyle(stylePart.innerHTML);
    });
};
