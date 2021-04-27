import {sum} from '../../iCollections/wwwroot/js/sum'
//const sum = require('./sum');
// import {isValidPhotoName} from '../../iCollections/wwwroot/js/site'
// ReferenceError: $ is not defined, when i try using function from site.js

function isValidPhotoName(proposed) {
    if (proposed == null || proposed == "") {
        // User cancelled the prompt -> ignore
        return false;
    }

    if (!proposed.replace(/\s/g, '').length) {
        // User has either all whitespace -> tell user
        alert("Enter a non-empty name");
        return false;
    }

    return true;
}

test('Rename_GivenEmptyPhotoNameReturns_False', () => {
    expect(isValidPhotoName("")).toBe(false);
});

test('Rename_GivenNullPhotoNameReturns_False', () => {
    expect(isValidPhotoName(null)).toBe(false);
});

test('Rename_GivenValidPhotoNameReturns_True', () => {
    expect(isValidPhotoName("barcelona_vacation")).toBe(true);
    expect(isValidPhotoName("first_day")).toBe(true);
    expect(isValidPhotoName("screenshot.jpg")).toBe(true);
});