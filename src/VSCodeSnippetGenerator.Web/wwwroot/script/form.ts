const descriptionArea = document.getElementById('SnippetInput_Description') as HTMLTextAreaElement;
const hasDescriptionCheckbox = document.getElementById('SnippetInput_HasDescription') as HTMLInputElement
const tabLengthInput = document.getElementById('SnippetInput_TabLength') as HTMLInputElement;
const convertToTabsCheckbox = document.getElementById('SnippetInput_ConvertToTabs') as HTMLInputElement;
const copyToClipboardButton = document.getElementById('CopyToClipboard') as HTMLButtonElement;
const snippetArea = document.getElementById('SnippetOutput') as HTMLTextAreaElement;
const tabToIndentCheckbox = document.getElementById('TabToIndent') as HTMLInputElement;
const bodyArea = document.getElementById('SnippetInput_Body') as HTMLTextAreaElement;

descriptionArea.addEventListener('input', event => {
    hasDescriptionCheckbox.checked = descriptionArea.value.length > 0;
});

copyToClipboardButton.addEventListener('click', event => {
    snippetArea.select();
    document.execCommand('copy');
});

bodyArea.addEventListener('keydown', event => {
    if (event.key === 'Tab' && tabToIndentCheckbox.checked) {
        event.preventDefault();

        const before = bodyArea.value.substring(0, bodyArea.selectionStart);
        const after = bodyArea.value.substring(bodyArea.selectionEnd);

        let tabLength = parseInt(tabLengthInput.value);
        tabLength = !isNaN(tabLength) && tabLength >= 2 && tabLength <= 8
            ? tabLength
            : 4;

        const indentation = convertToTabsCheckbox.checked
            ? '\t'
            : ' '.repeat(tabLength);

        bodyArea.value = before + indentation + after;
        bodyArea.selectionEnd = bodyArea.selectionStart = before.length + indentation.length;
    }
})