import {updateSubtotal,calculateSubtotal } from './CartSubtotal.js';

export function fetchDiscountAndUpdateCart(cartItems) {
    $.ajax({
        url: 'SetDiscount',
        type: 'POST',
        success: function (response) {
            handleDiscount(response, cartItems);
        },
        error: function (xhr, status, error) {
            console.log("Error occurred while setting discount: " + error);
        }

    });
}
function handleDelivery(response, cartItems, subtotal) {
    if (response.Success) {
        console.log("Delivery set successfully");
        var delivery = response.delivery.Cost;
        var FinalTotal = subtotal + delivery;

        UpdateDelivery(delivery);
        UpdateFinalTotal(FinalTotal);
    } else {
        console.log("Delivery was not set!");
        UpdateDelivery(0);
        UpdateFinalTotal(subtotal);
    }
}
export function fetchDelivery(cartItems, FinalTotal) {
    $.ajax({
        url: 'SetDeliveyRequest',
        type: 'POST',
        success: function (response) {
            handleDelivery(response, cartItems, FinalTotal);
        },
        error: function (xhr, status, error) {
            console.log("Error occurred while setting discount: " + error);
        }
    });
}
export function handleDiscount(response, cartItems) {
    if (response.Success) {
        console.log("Discount set successfully");
        var discount = response.discount.Percentage;
        var subtotal = calculateSubtotal(cartItems);
        var discountAmount = (subtotal * discount) / 100;
        updateSubtotal(subtotal);
        UpdateDiscount(discountAmount);
        var FinalTotal = subtotal - discountAmount;
        fetchDelivery(cartItems, FinalTotal);
        UpdateFinalTotal(FinalTotal);
    } else {
        console.log("Discount expired!");
        UpdateDiscount(0);
        subtotal = calculateSubtotal(cartItems);
        fetchDelivery(cartItems, subtotal);
        updateSubtotal(subtotal);
        UpdateFinalTotal(subtotal);

        $('#alertContainer').fadeIn();
    }
}

export const UpdateDiscount = (discount) => {
    
  $('#discount').text(discount.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
 
}

export const UpdateFinalTotal = (FinalTotal) => {
    $('#finalTotal').text(FinalTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
}
export const SetDelivery = (subtotal, deliveryPrice) => {
    var totalPrice = subtotal + deliveryPrice;
    return totalPrice;
}
export const UpdateDelivery = (priceDelivery) => {
    $('#delivery').text(priceDelivery.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
}