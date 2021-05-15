document.addEventListener('DOMContentLoaded', function () {
    //remember scroll position
    const buttonEnc = document.querySelector('.encoder .button-primary');
    const encoder = document.querySelector('#encoder');
    const positionEnc = encoder.getBoundingClientRect().top;
    buttonEnc.addEventListener('click', event => {
        sessionStorage.setItem("scroll-position", positionEnc);
    });

    const buttonDec = document.querySelector('.decoder .button-primary');
    const decoder = document.querySelector('#decoder');
    const positionDec = decoder.getBoundingClientRect().top;
    buttonDec.addEventListener('click', event => {
        sessionStorage.setItem("scroll-position", positionDec);
    });


    if (sessionStorage.getItem("scroll-position") != null) {
        console.log(sessionStorage.getItem("scroll-position"));
        window.scrollTo(0, sessionStorage.getItem("scroll-position"));
        sessionStorage.removeItem("scroll-position");
    }

    //animation
    let divs = document.querySelectorAll('.chars');

    const rand = (multi) => {
        return parseInt(multi * Math.random(), 10);
    }

    function move() {

        let ww = window.innerWidth;
        let wh = window.innerHeight;

        divs.forEach((div) => {

            let w = 300;

            let x = rand((ww - w));
            let y = rand((wh - w));

            div.style.top = w / 2 + y + 'px';
            div.style.left = w / 2 + x + 'px';
            div.style.opacity = '0.1';

            div.style.transition = (rand(1000) + 7000) + 'ms';
        });
    }

    move();
    window.setTimeout(move, 1000);
    window.setInterval(move, 7000);
    
});
