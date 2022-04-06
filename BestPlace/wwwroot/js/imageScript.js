var imgs = document.getElementsByClassName('img-fluid d-block small-preview');
let mainImg = document.getElementsByClassName('zoomed-image')[0];
mainImg.style = `background-image: url('/Image/GetItemImage/${imgs[0].id}'); background-size: contain; background-position: left center;`;
var remove = document.getElementsByTagName('a');

for (let x of imgs) {
    x.addEventListener('click',
        e => {
            e.preventDefault();

            mainImg.style =`background-image: url('/Image/GetItemImage/${x.id}'); background-size: contain; background-position: left center;`;
            remove.id = x.id;

        });
}
