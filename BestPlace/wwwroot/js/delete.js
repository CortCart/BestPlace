var  alllImgs = document.getElementsByClassName('img-fluid d-block small-preview');
var remove = document.getElementsByTagName('a')[7];
var Img = document.getElementsByClassName('zoomed-image')[0];
Img.style = `background-image: url('/Image/GetItemImage/${imgs[0].id}'); background-size: contain; background-position: left center;`;

remove.addEventListener('click', e => {
    e.preventDefault();
    if (imgs.length!=1) {
        for (let x of imgs) {
            if (x.id == remove.id) {
                x.remove();
                deleteImage(x.id);
            }

        }
        Img.style = `background-image: url('/Image/GetItemImage/${imgs[0].id}'); background-size: contain; background-position: left center;`;

    }
   
});


async function deleteImage(id) {
    var url = `/Image/DeleteItemImage/${id}`;
    var method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Delete'
    }
    var $fetch = await fetch(url, method);

}