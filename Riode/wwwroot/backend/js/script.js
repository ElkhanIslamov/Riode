const loadMoreBtn = document.getElementById("loadMoreBtn");
const productBox = document.getElementById("productBox");
const productCount = document.getElementById("productCount");
const productDetails = document.querySelectorAll(".product-detail");
const modalDetails = document.querySelector(".modal-content");


console.log(productCount)

let skip = 6;
loadMoreBtn = addEventListener("click", function () {
    let url = `/Category/LoadMore?skip=${skip}`;

    fetch(url).then(responce => responce.text())
        .then(data => productBox.innerHTML += data);

    skip += 6;
    if (skip >= loadMoreBtn) {
        loadMoreBtn.remove();
    }
})
productDetails.forEach(productDetail => {
    productDetail.addEventListener("click", function (e) {
        e.preventDefault();
        console.log(productModal);

       
        let url = this.getAttribute("href");
        fetch(url).then(response => response.text())
            .then(data => {
                producModal.innerHTML = data
            });
               
    })
})
