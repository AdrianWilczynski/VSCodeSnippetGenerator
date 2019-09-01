const descriptionArea = document.getElementById('SnippetInput_Description') as HTMLTextAreaElement;
descriptionArea.addEventListener('input', event => {
    const hasDescriptionCheckbox = document.getElementById('SnippetInput_HasDescription') as HTMLInputElement
    hasDescriptionCheckbox.checked = descriptionArea.value.length > 0;
});

const tabLengthInput = document.getElementById('SnippetInput_TabLength') as HTMLInputElement;
tabLengthInput.addEventListener('input', event => {
    const convertToTabsCheckbox = document.getElementById('SnippetInput_ConvertToTabs') as HTMLInputElement;
    convertToTabsCheckbox.checked = !!tabLengthInput.value;
});

const copyToClipboardButton = document.getElementById('CopyToClipboard') as HTMLButtonElement;
copyToClipboardButton.addEventListener('click', event => {
    const snippetArea = document.getElementById('SnippetOutput') as HTMLTextAreaElement;
    snippetArea.select();
    document.execCommand('copy');
});