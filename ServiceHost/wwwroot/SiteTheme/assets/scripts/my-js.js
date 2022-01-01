const open = document.getElementById("open");
const menu = document.getElementById("menu");
const close = document.getElementById("close");

open.addEventListener("click", () => {
    menu.style.display = "block";
})
close.addEventListener("click", () => {
    menu.style.display = "none";
})


AOS.init();

$('#categories').owlCarousel({
    loop: true,
    touchDrag: true,
    autoplay: true,
    autoplayTimeout: 4000,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 4
        },
        1000: {
            items: 5
        }
    }
})
$('#brands').owlCarousel({
    loop: true,
    touchDrag: true,
    autoplay: true,
    autoplayTimeout: 4000,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 4
        },
        1000: {
            items: 5
        }
    }
})
$('#products').owlCarousel({
    loop: true,
    loop: true,
    touchDrag: true,
    autoplay: true,
    autoplayTimeout: 4000,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 3
        },
        1300: {
            items: 4
        }
    }
})

$('#title').owlCarousel({
    rtl: true,
    loop: true,
    margin: 10,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 4
        }
    }
})