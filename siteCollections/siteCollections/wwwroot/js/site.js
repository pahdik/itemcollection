const loadMoreBlock = document.querySelector('.load-more')

window.addEventListener("scroll", lazyScroll);  
function lazyScroll() {
    if (!loadMoreBlock.classList.contains('_loading')) {
        loadMore();
    }
}

function loadMore() {
    const loadMoreBlockPos = loadMoreBlock.getBoundingClientRect().top + pageYOffset;
    const loadMoreBlockHeight = loadMoreBlock.offsetHeight;
    if (pageYOffset > (loadMoreBlockPos - loadMoreBlockHeight) - window.innerHeight) {
        getContent();
    }
}
async function getContent() {
    loadMoreBlock.classList.add('_loading');
    let url = "/Item/GetItemsPage";
    let response = await fetch(url);
    if (response.ok) {
        let result = await response.text();
        loadMoreBlock.insertAdjacentHTML('beforeend', result);
        loadMoreBlock.classList.remove('_loading');
    } else {
        alert('error'); 
    }
}
