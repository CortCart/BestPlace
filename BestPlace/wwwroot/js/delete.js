var imgs = document.getElementsByClassName('img-fluid d-block small-preview');
var remove = document.getElementsByTagName('a')[5];
let Img = document.getElementsByClassName('zoomed-image')[0];
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
    let url = `/Image/DeleteItemImage/${id}`;
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Delete'
    }
    let $fetch = await fetch(url, method);

}