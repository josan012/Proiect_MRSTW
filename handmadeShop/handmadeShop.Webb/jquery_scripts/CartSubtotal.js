export function calculateSubtotal(cartItems) {
    return cartItems.reduce(function (total, currentItem) {
        return total + (currentItem.Product.Price * currentItem.Quantity);
    }, 0);
}


export function updateSubtotal(subtotal) {
    $('#subtotal').text(subtotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
}

export function calculateFinalTotal(subtotal,delivery,discount) {
    var FinalTotal = subtotal + delivery + discount;
    return FinalTotal;
}