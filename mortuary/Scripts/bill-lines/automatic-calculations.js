function gatherProductData(clonedRow) {
    setPrice(clonedRow);
    setTaxes(clonedRow);
}

function setPrice(clonedRow) {
    var rowProductSelect = clonedRow.querySelector(`#row-${clonedRow.id}-product-select`);
    var rowPriceInput = clonedRow.querySelector(`#row-${clonedRow.id}-price-input`);

    var selectedProduct = rowProductSelect.options[rowProductSelect.selectedIndex];
    rowPriceInput.value = selectedProduct.getAttribute("price");

    setSubtotal(clonedRow);
}

function setTaxes(clonedRow) {
    var rowProductSelect = clonedRow.querySelector(`#row-${clonedRow.id}-product-select`);
    var rowTaxesInput = clonedRow.querySelector(`#row-${clonedRow.id}-taxes-input`);

    var selectedProduct = rowProductSelect.options[rowProductSelect.selectedIndex];
    rowTaxesInput.value = selectedProduct.getAttribute("taxes");

    setSubtotal(clonedRow);
}

function setSubtotal(clonedRow) {
    var rowAmountInput = clonedRow.querySelector(`#row-${clonedRow.id}-amount-input`);
    var rowPriceInput = clonedRow.querySelector(`#row-${clonedRow.id}-price-input`);
    var rowTaxesInput = clonedRow.querySelector(`#row-${clonedRow.id}-taxes-input`);
    var rowSubtotalInput = clonedRow.querySelector(`#row-${clonedRow.id}-subtotal-input`);

    var amount = rowAmountInput.value;
    var price = rowPriceInput.value;
    var taxesMultiplier = rowTaxesInput.value / 100 + 1;
    var totalPrice = price * taxesMultiplier;
    var subtotal = amount * totalPrice;
    rowSubtotalInput.value = subtotal.toFixed(2);

    setTotal();
}

function setTotal() {
    var totalInput = $('#total-input')[0];
    totalInput.value = 0;
    for (var currentRowNumber = 0; currentRowNumber <= row_number; currentRowNumber++) {
        currentRowSubtotal = $(`#row-${currentRowNumber}-subtotal-input`)[0];
        if (currentRowSubtotal) {
            var totalSoFar = parseFloat(totalInput.value);
            totalSoFar += parseFloat(currentRowSubtotal.value);
            totalInput.value = totalSoFar.toFixed(2);
        }
    }
}