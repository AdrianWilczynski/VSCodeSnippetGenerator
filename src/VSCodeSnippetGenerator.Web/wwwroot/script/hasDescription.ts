let descriptionArea = document.getElementById('Description') as HTMLTextAreaElement;
descriptionArea.addEventListener('input', event => {
    let hasDescriptionCheckbox = document.getElementById('HasDescription') as HTMLInputElement
    hasDescriptionCheckbox.checked = descriptionArea.value.length > 0;
});