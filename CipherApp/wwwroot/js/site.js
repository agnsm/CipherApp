document.addEventListener('DOMContentLoaded', function () {
    AOS.init();
    manageCookies();
    setScrollPosition();

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

    //lable style on focus
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach((input) => {
        input.addEventListener('focus', event => {
            let label = event.target.previousElementSibling;
            label.style.color = '#7e9673';
        });
        input.addEventListener('blur', event => {
            let label = event.target.previousElementSibling;
            label.style.color = '#cecfe5';
        });
    });
});

function manageCookies() {
    const cookieInfo = document.querySelector('.cookie-info');
    const cookieInfoClose = document.querySelector('.info-close');
    const cookies = document.cookie.split('; ');
    let b = false;

    cookies.forEach((cookie) => {
        let name = cookie.split('=')[0];
        let value = cookie.split('=')[1];
        if (name == 'cookies_allowed' && value == 'true') {
            b = true;
        }
    });

    if (!b) {
        cookieInfo.setAttribute('style', 'display: flex !important');
    }

    cookieInfoClose.addEventListener('click', event => {
        cookieInfo.setAttribute('style', 'display: none !important');
        let date = new Date();
        date.setTime(date.getTime() + (30 * 24 * 60 * 60 * 1000));
        //date.setTime(date.getTime() + (10 * 1000));
        document.cookie = 'cookies_allowed=true; expires=' + date.toUTCString();
    });
}

function setScrollPosition() {
    const buttonEnc = document.querySelector('.encoder .button-primary');
    const encoder = document.querySelector('#encoder');
    const positionEnc = encoder.getBoundingClientRect().top;

    buttonEnc.addEventListener('click', event => {
        sessionStorage.setItem('scroll-position', positionEnc);
    });

    const buttonDec = document.querySelector('.decoder .button-primary');
    const decoder = document.querySelector('#decoder');
    const positionDec = decoder.getBoundingClientRect().top;

    buttonDec.addEventListener('click', event => {
        sessionStorage.setItem('scroll-position', positionDec);
    });


    if (sessionStorage.getItem('scroll-position') != null) {
        window.scrollTo(0, sessionStorage.getItem('scroll-position'));
        sessionStorage.removeItem('scroll-position');
    }
}
