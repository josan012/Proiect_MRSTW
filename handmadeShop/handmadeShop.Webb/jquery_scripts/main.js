import { updateCartItemCount } from './ItemCount.js';
import { fetchDiscountAndUpdateCart } from './AddDiscount.js';


$(document).ready(function () {
    let cartItemCount = localStorage.getItem("cartItemCount") || 0;
   

    updateCartItemCount(cartItemCount);

    function renderCartItems(cartItems) {
  
        var tbody = $("table tbody");
        tbody.empty();

        cartItems.forEach(function (item) {
            tbody.append(renderCartItem(item));
        });

        if (window.location.pathname === '/Buy/Cart' || window.location.pathname == '/Buy/Checkout') {
            fetchDiscountAndUpdateCart(cartItems);
        }
        
    }
  


    function renderCartItem(item) {
        return `<tr class="text-center">
        <td class="product-remove">
            <form id="remove-item-form-${item.Product.Id}" data-item-id="${item.Product.Id}" action="" method="post">
                <input type="hidden" name="itemId" value="${item.Product.Id}">
                <a class="remove-from-cart"><span class="icon-close"></span></a>
            </form>
        </td>
        <td class="image-prod"><div class="img" style="background-image:url(${item.Product.PathImage});"></div></td>
        <td class="product-name">
            <h3>${item.Product.Name}</h3>
            <p>Far far away, behind the word mountains, far from the countries</p>
        </td>
        <td class="price">${item.Product.Price.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</td>
        <td class="quantity">
            <div class="input-group mb-3">
                <input type="text" name="quantity" class="quantity form-control input-number col-5" value="${item.Quantity}" min="1" max="100">
            </div>
        </td>
        <td class="total">${(item.Product.Price * item.Quantity).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</td>
    </tr>`;
    }
    function loadCartItems() {
        var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        renderCartItems(cartItems);
     
    }

 


    loadCartItems();
     

    $(document).on('submit', '#add-item-form', function (e) {
        e.preventDefault();
        var productId = $(this).data('item-id');
        var category = $(this).data('item-category');
        $.ajax({
            url: '/Buy/AddToCart',
            type: 'POST',
            data: { ProductId: productId, category: category },
            success: function (response) {
                if (response.success) {
                    console.log("Item added to the cart successfully");
                    console.log("Coffee ID: ", productId);
                    console.log("Response: ", response);

                    cartItemCount++;
                    updateCartItemCount(cartItemCount);
                    localStorage.setItem("cartItemCount", cartItemCount);
                   
                
                    localStorage.setItem("cartItems", JSON.stringify(response.cartItems));
          
                   
                    renderCartItems(response.cartItems);
                    
                } else {
                    console.log("Failed to add item to the cart");
                }
            },
            error: function (xhr, status, error) {
                console.log("Error occurred while adding item to the cart");
                console.log("Status: ", status);
                console.log("Error: ", error);
            }
        });
    });
  
  


    $(document).on('click', '.remove-from-cart', function (e) {
        e.preventDefault();
        var productId = $(this).closest('form').data('item-id');

        $.ajax({
            url: '/Buy/RemoveFromTheCart',
            type: 'POST',
            data: { ProductId: productId },
            success: function (response) {
                if (response.success) {
                    console.log("Item removed from the cart successfully");
                    console.log("Coffee ID: ", productId);
                    console.log("Response: ", response);

                    renderCartItems(response.cartItems);
                   
                 
                    var totalQuantity = response.cartItems.reduce(function (total, currentItem) {
                        return total + currentItem.Quantity;
                    }, 0);

                    cartItemCount = totalQuantity;
                    updateCartItemCount(cartItemCount);
                    localStorage.setItem("cartItemCount", cartItemCount);

              
                    localStorage.setItem("cartItems", JSON.stringify(response.cartItems));
                } else {
                    console.log("Failed to remove item from the cart");
                }
            },
            error: function (xhr, status, error) {
                console.log("Error occurred while removing item from the cart");
                console.log("Status: ", status);
                console.log("Error: ", error);
            }
        });
    });
  
});