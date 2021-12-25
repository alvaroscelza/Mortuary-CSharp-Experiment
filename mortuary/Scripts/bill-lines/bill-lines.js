var clonableRow = $('#clonable-row')[0];
var totalRowsInput = $('#total-rows')[0];
var row_number = 0;
totalRowsInput.value = 0;

$(document).ready(function () {
    addRow();
});

$('.table-add').click(addRow);

function addRow() {
    var newRow = createRow();
    addRowToTable(newRow);
    setTotal();
    row_number++;
    totalRowsInput.value ++;
}

function createRow() {
    var clonedRow = clonableRow.cloneNode(true);
    clonedRow.removeAttribute("hidden");
    clonedRow.removeAttribute("id");
    clonedRow.setAttribute("id", row_number);
    setIdsAndNamesToChildren(clonedRow);
    setBehaviourToChildren(clonedRow);
    return clonedRow;
}

function setIdsAndNamesToChildren(clonedRow) {
    setIdAndName(clonedRow, "product-select");
    setIdAndName(clonedRow, "notes-input");
    setIdAndName(clonedRow, "amount-input");
    setIdAndName(clonedRow, "price-input");
    setIdAndName(clonedRow, "taxes-input");
    setIdAndName(clonedRow, "subtotal-input");
}

function setIdAndName(clonedRow, htmlElement) {
    var elementInRow = clonedRow.querySelector(`#clonable-${htmlElement}`);
    elementInRow.setAttribute("id", `row-${clonedRow.id}-${htmlElement}`);
    elementInRow.setAttribute("name", `row-${clonedRow.id}-${htmlElement}`);
}

function setBehaviourToChildren(clonedRow) {
    var rowProductSelect = clonedRow.querySelector(`#row-${clonedRow.id}-product-select`);
    var rowAmountInput = clonedRow.querySelector(`#row-${clonedRow.id}-amount-input`);
    var rowPriceInput = clonedRow.querySelector(`#row-${clonedRow.id}-price-input`);
    var rowTaxesInput = clonedRow.querySelector(`#row-${clonedRow.id}-taxes-input`);
    rowProductSelect.onchange = function () { gatherProductData(clonedRow); };
    rowAmountInput.onchange = function () { setSubtotal(clonedRow); };
    rowPriceInput.onchange = function () { setSubtotal(clonedRow); };
    rowTaxesInput.onchange = function () { setSubtotal(clonedRow); };
    gatherProductData(clonedRow);
}

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

function addRowToTable(newRow) {
    var removeButton = newRow.querySelector(".table-remove");
    removeButton.onclick = function () { removeRow(newRow); };
    table.append(newRow);
}

function removeRow(row) {
    row.remove();
    setTotal();
    totalRowsInput.value --;
}
