let descriptionArea = document.getElementById('Description') as HTMLTextAreaElement;
descriptionArea.addEventListener('input', event => {
    let hasDescriptionCheckbox = document.getElementById('HasDescription') as HTMLInputElement
    hasDescriptionCheckbox.checked = descriptionArea.value.length > 0;
});

let tabLengthInput = document.getElementById('TabLength') as HTMLInputElement;
tabLengthInput.addEventListener('input', event => {
    let convertToTabsCheckbox = document.getElementById('ConvertToTabs') as HTMLInputElement;
    convertToTabsCheckbox.checked = !!tabLengthInput.value;
});

let copyToClipboardButton = document.getElementById('CopyToClipboard') as HTMLButtonElement;
copyToClipboardButton.addEventListener('click', event => {
    let snippetArea = document.getElementById('Snippet') as HTMLTextAreaElement;
    snippetArea.select();
    document.execCommand('copy');
});