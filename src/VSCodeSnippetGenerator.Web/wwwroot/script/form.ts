let descriptionArea = document.getElementById('Description') as HTMLTextAreaElement;
descriptionArea.addEventListener('input', event => {
    let hasDescriptionCheckbox = document.getElementById('HasDescription') as HTMLInputElement
    hasDescriptionCheckbox.checked = descriptionArea.value.length > 0;
});

let tabLengthInput = document.getElementById('TabLength') as HTMLInputElement;
tabLengthInput.addEventListener('input', evnet => {
    let convertToTabsCheckbox = document.getElementById('ConvertToTabs') as HTMLInputElement;
    convertToTabsCheckbox.checked = !!tabLengthInput.value;
})